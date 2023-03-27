using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using SimpleLangInterpreter.Core;

namespace SimpleLangGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Input.TextChanged += onTextChangedNew;
        }
        
        public void onTextChangedNew(object a, TextChangedEventArgs args)
        {
            try
            {
                var input = StringFromRichTextBox(Input);
                var resulver = new Lexer("test",input);
                var tokens = resulver.CreateTokens();

                var postProcessr = new LexerPostProcessor(tokens);
                tokens = postProcessr.FilterTokens();


                var message = "Tokens: " + tokens.Count + "\n";
                foreach (var token in tokens)
                {
                    message += token.ToString();
                }

                var interpreter = new Parser();
                var operations = interpreter.Interpet(tokens);
                foreach (var operation in operations)
                {
                    message += operation.ToString();
                }
                
                
                SetText(Output, message);
            }
            catch (Exception e)
            {
                SetText(Output, e.Message);
            }
        }
        
        private string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                rtb.Document.ContentStart,
                rtb.Document.ContentEnd
            );
            return textRange.Text;
        }
        
        
        private void SetText(RichTextBox rtb, String text)
        {
            rtb.Document.Blocks.Clear();
            rtb.Document.Blocks.Add(new Paragraph(new Run(text)));
        }
    }
}