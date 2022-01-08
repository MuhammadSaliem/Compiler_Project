using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Text;
using static Compiler_Project.JasonParser;

namespace Compiler_Project
{
    class JasonVisitor : JasonBaseVisitor<object>
    {
        public List<parseCLS> Lines = new List<parseCLS>();
        public override object VisitStatment(JasonParser.StatmentContext context)
        {
            Declare_statementContext Declarestatment = context.declare_statement();
            Write_statementContext writeStatement = context.write_statement();
            Call_statementContext CallStatement = context.call_statement();
            Assignment_statementContext AssignmentStatement = context.assignment_statement();
            Read_statementContext ReadStatement = context.read_statement();
            Write_statementContext WriteStatement = context.write_statement();
            While_statementContext WhileStatement= context.while_statement();
            If_statementContext IfStatement = context.if_statement();
            Declare_functionContext DeclareFunction = context.declare_function();


            parseCLS line = null;

            if (Declarestatment != null && Declarestatment.Stop.Text == ";")
            {
                if (Declarestatment.GetText().Substring(Declarestatment.GetText().Length - 1) == ";")
                {
                    if (Declarestatment.ChildCount == 3)
                    {
                        line = new parseCLS() { Text = Declarestatment.GetText(), Rule = "declare_statement: data_type IDENTIFIER SIMICOLON" };

                    }
                    else
                    {
                        line = new parseCLS() { Text = Declarestatment.GetText(), Rule = "declare_statement: data_type IDENTIFIER ASSIGNMENT_OPERATOR input SIMICOLON" };

                    }
                }
            }

            if (writeStatement != null && writeStatement.Stop.Text == ";")
            {
                 line = new parseCLS() { Text = writeStatement.GetText(), Rule = "WRITE IDENTIFIER SIMICOLON"};
            }

            if (CallStatement != null && CallStatement.Stop.Text == ";")
            {
                if(CallStatement.ChildCount == 3)
                {
                    line = new parseCLS() { Text = CallStatement.GetText(), Rule = "Call_statement: CALL IDENTIFIER SIMICOLON" };

                }
                else
                {
                    line = new parseCLS() { Text = CallStatement.GetText(), Rule = "Call_statement: CALL IDENTIFIER OPEN_PAREN parameter CLOSE_PAREN SIMICOLON" };

                }
            }

            if (AssignmentStatement != null && AssignmentStatement.Stop.Text == ";")
            {
                line = new parseCLS() { Text = AssignmentStatement.GetText(), Rule = "Assignment_statement: IDENTIFIER ASSIGNMENT_OPERATOR input SIMICOLON" };
            }

            if (ReadStatement != null && ReadStatement.Stop.Text == ";")
            {
                line = new parseCLS() { Text = ReadStatement.GetText(), Rule = "Read_statement: READ IDENTIFIER SIMICOLON" };
            }

            if (WriteStatement != null && WriteStatement.Stop.Text == ";")
            {
                line = new parseCLS() { Text = WriteStatement.GetText(), Rule = "Write_statement: WRITE IDENTIFIER SIMICOLON" };
            }

            if (WhileStatement != null && WriteStatement.Stop.Text == "}")
            {
                line = new parseCLS() { Text = WhileStatement.GetText(), Rule = "While_statement: WHILE OPEN_PAREN logical_expression CLOSE_PAREN OBRACE  statment_sequence CBRACE" };
            }

            if (IfStatement != null && IfStatement.Stop.Text == "}")
            {
                line = new parseCLS() { Text = IfStatement.GetText(), Rule = "IF_statement: IF OPEN_PAREN logical_expression CLOSE_PAREN OBRACE  statment_sequence CBRACE else_part?" };
            }
            if (DeclareFunction != null && DeclareFunction.Stop.Text == "}")
            {
                line = new parseCLS() { Text = DeclareFunction.GetText(), Rule = "Declare_Function: FUNC IDENTIFIER OPEN_PAREN parameter? CLOSE_PAREN OBRACE statment_sequence function_return? CBRACE" };
            }
            Lines.Add(line);
            return Lines;
        }

    }
}
