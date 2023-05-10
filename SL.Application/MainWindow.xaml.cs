using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using SL.Parser;
using SL.Parser.Api;
using SL.Parser.Common;
using SL.Interpreter;

namespace SL.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly bool ignoreEvent = false;
        private CancellationTokenSource _source;
        private bool autoCompiler = true;

        public MainWindow()
        {
            InitializeComponent();
            Input.TextChanged += onTextChangedNew;
            //  Input.Background = new SolidColorBrush(Colors.Gray);
            Input.Margin = new Thickness(0);
            Output_token.FontSize = 25;
            Output_tree.FontSize = 12;
            _source = new CancellationTokenSource();
            
            InputBindings.Add(new KeyBinding( new SaveFileCommand(() =>
            {
                RunProgram();
            }),  new KeyGesture(Key.S, ModifierKeys.Control)));
            
            InputBindings.Add(new KeyBinding( new SaveFileCommand(() =>
            {
                autoCompiler = !autoCompiler;
            }),  new KeyGesture(Key.D, ModifierKeys.Control)));
            
        }

        public void onTextChangedNew(object a, TextChangedEventArgs args)
        {
            if (ignoreEvent)
            {
                return;
            }

            if (!autoCompiler)
            {
                return;
            }

            RunProgram();
        }

        public void RunProgram()
        {
            _source.Cancel();
            _source = new CancellationTokenSource();
         


            var input = StringFromRichTextBox(Input);
            Task.Run(async () =>
            {
                try
                {
                    InvokeAction(() => Clear(Output_program));
                    
                    var lexer = LexerFactory.CreateLexer(input);
                    var tokenIterator = await lexer.LexAllToInterator(_source.Token);
                    InvokeAction(() => DisplayTokens(tokenIterator.ToList()));
                    
                    
                    var parser = ParserFactory.CreateParser(tokenIterator, _source.Token);
                    var programTree = await parser.ParseNew();
                    InvokeAction(() => SetText(Output_tree, programTree.ToJson()));
                    
                    
                    var evaluator = EvaluatorFactory.CreateEvaluator(_source.Token);
                    evaluator.OnConsoleUpdate(list =>
                    {
                        InvokeAction(() => DisplayConsole(list));
                    });
                    await evaluator.ExecuteProgram(programTree);
                  
                }
                catch (Exception e)
                {
                    InvokeAction(() => SetText(Output_program, e.Message));
                }
            }, _source.Token);
        }

        private void DisplayConsole(List<string> tokens)
        {
            var builder = new StringBuilder();
            foreach (var token in tokens)
            {
                builder.AppendLine(token);
            }
          
            SetText(Output_program, builder.ToString());
        }

        private void DisplayTokens(List<Token> tokens)
        {
            var builder = new StringBuilder();
            foreach (var token in tokens)
            {
                builder.AppendLine(token.ToString());
            }

            SetText(Output_token, builder.ToString());
        }

        private void InvokeAction(Action action)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(action.Invoke);
        }

        private string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                rtb.Document.ContentStart,
                rtb.Document.ContentEnd
            );
            return textRange.Text;
        }

        private void Clear(RichTextBox rtb)
        {
            rtb.Document.Blocks.Clear();
        }

        private void SetText(RichTextBox rtb, String text)
        {
            rtb.Document.Blocks.Clear();
            rtb.Document.Blocks.Add(new Paragraph(new Run(text)));
        }

        private void AddText(RichTextBox rtb, String text)
        {
            rtb.Document.Blocks.Add(new Paragraph(new Run(text)));
        }
    }
}