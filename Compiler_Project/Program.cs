using Antlr4.Runtime;
using System;
using System.Text;

namespace Compiler_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                try
                {
                    string input = "";
                    StringBuilder text = new StringBuilder();
                    Console.WriteLine("Input the chat.");
                    input = Console.ReadLine();
                    text.AppendLine(input);
                    AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
                    JasonLexer JasonLexer = new JasonLexer(inputStream);
                    CommonTokenStream commonTokenStream = new CommonTokenStream(JasonLexer);
                    JasonParser JasonParser = new JasonParser(commonTokenStream);

                    JasonParser.StatmentContext statementContext = JasonParser.statment();
                    JasonVisitor visitor = new JasonVisitor();
                    visitor.Visit(statementContext);

                    foreach (var line in visitor.Lines)
                    {
                        Console.WriteLine('{' + input + '}' + "   " + '{' +  line.Rule  + '}');
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }

            }
        }
    }
}
