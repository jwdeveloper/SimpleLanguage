using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleLangInterpreter;
using SimpleLangInterpreter.ColorStyle;
using SimpleLangInterpreter.Core;
using SimpleLangInterpreter.Evaliating;


namespace SimpleLangGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread thread;
        private bool ignoreEvent = false;
        private CancellationTokenSource source = new CancellationTokenSource();
        private CancellationToken token;
        private Task _task;

        public MainWindow()
        {
            InitializeComponent();
            Input.TextChanged += onTextChangedNew;
            //  Input.Background = new SolidColorBrush(Colors.Gray);
            Input.Margin = new Thickness(0);
            Output_token.FontSize = 25;
            Output_tree.FontSize = 25;
            token = source.Token;
        }

        public void onTextChangedNew(object a, TextChangedEventArgs args)
        {
            if (ignoreEvent)
            {
                return;
            }
            if (thread != null)
            {
                source.Cancel();
                thread.Interrupt();
            }

            source = new CancellationTokenSource();
            token = source.Token;
            var input = StringFromRichTextBox(Input);
            thread = new Thread(o =>
            {
                try
                {
                    var resulver = new Lexer("test", input);
                    var tokens = resulver.CreateTokens();

                    var postProcessr = new LexerPostProcessor(tokens);
                    tokens = postProcessr.FilterTokens();
                  
                    var interpreter = new Parser(tokens);
                    var program = interpreter.Parse();
                    var evaluator = new Evaluator(null, token);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        Clear(Output_program);
                        setTokensColors(tokens);
                        SetText(Output_tree,program.Print());
                    }));

                    evaluator.OnConsole(s =>
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            SetText(Output_program, evaluator.GetConsole());
                        }));
                    });

                    evaluator.runProgram(program);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine("Canceled!");
                }
                catch (OperationCanceledException e)
                {
                    Console.WriteLine("Canceled!");
                }
                catch (Exception e)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        SetText(Output_token, e.Message);
                    }));
                }
               
            });
            thread.Start(token);
        }


        public void setTokensColors(List<SyntaxToken> tokens)
        {
            ignoreEvent = true;
            Output_token.Document.Blocks.Clear();
            var paragraf = new Paragraph();
            foreach (var token in tokens)
            {
                if (token.TokenType == TokenType.WhiteSpace)
                    continue;

                var run = new Run(token.ToString());
                run.Foreground = new SolidColorBrush(getColor(token));
                paragraf.Inlines.Add(run);
            }

            Output_token.Document.Blocks.Add(paragraf);
            ignoreEvent = false;
        }

        public void setInputColors(List<SyntaxToken> tokens)
        {
            ignoreEvent = true;
            var cursor = Input.CaretPosition;
            Input.Document.Blocks.Clear();
            var paragraf = new Paragraph();
            foreach (var token in tokens)
            {
                if (token.Symbol == "\n" || token.Symbol == "\r")
                {
                    continue;
                }

                var run = new Run(token.Symbol);
                run.Foreground = new SolidColorBrush(getColor(token));
                paragraf.Inlines.Add(run);
            }

            Input.Document.Blocks.Add(paragraf);
            Input.CaretPosition = Input.CaretPosition.DocumentEnd;
            ignoreEvent = false;
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

        public static Color getColor(SyntaxToken token)
        {
            switch (token.TokenType)
            {
                case TokenType.Comment:
                {
                    return Colors.Green;
                }
                case TokenType.BinaryToken:
                {
                    return Colors.Blue;
                }
                case TokenType.NumberToken:
                {
                    return Colors.Pink;
                }
                case TokenType.StringToken:
                {
                    return Colors.Orange;
                }
                case TokenType.TypeToken:
                {
                    return Colors.DarkBlue;
                }
                case TokenType.Undefined:
                {
                    return Colors.Green;
                }
            }

            return Colors.Black;
        }
    }
}