#nullable enable
using System;
using System.Collections.Generic;
using ToyLang.Parser;
using static ToyLang.Parser.Expr;
using static ToyLang.Parser.Stmt;

namespace ToyLang.Interpreter
{
    public class Interpreter : Expr.IVisitor<object>, Stmt.IVisitor<object>
    {
        private Environment _environment = new Environment();

        public void Interpret(List<Stmt> statements)
        {
            try
            {
                foreach (var stmt in statements)
                {
                    Execute(stmt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Runtime error: {ex.Message}");
            }
        }

        private void Execute(Stmt stmt)
        {
            stmt.Accept(this);
        }

        // ---- EXPRESSIONS ----

        public object VisitLiteralExpr(Literal expr)
        {
            return expr.Value;
        }

        public object VisitVariableExpr(Variable expr)
        {
            return _environment.Get(expr.Name);
        }

        public object VisitAssignExpr(Assign expr)
        {
            object value = expr.Value.Accept(this);
            _environment.Assign(expr.Name, value);
            return value;
        }

        public object VisitBinaryExpr(Binary expr)
        {
            object left = expr.Left.Accept(this);
            object right = expr.Right.Accept(this);

            return expr.Operator switch
            {
                "+" => (int)left + (int)right,
                "-" => (int)left - (int)right,
                "*" => (int)left * (int)right,
                "/" => (int)left / (int)right,
                "==" => (int)left == (int)right ? 1 : 0,
                "!=" => (int)left != (int)right ? 1 : 0,
                "<"  => (int)left <  (int)right ? 1 : 0,
                "<=" => (int)left <= (int)right ? 1 : 0,
                ">"  => (int)left >  (int)right ? 1 : 0,
                ">=" => (int)left >= (int)right ? 1 : 0,
                _ => throw new Exception($"Unknown operator '{expr.Operator}'"),
            };
        }

        // ---- STATEMENTS ----

        public object VisitExprStmt(ExprStmt stmt)
        {
            stmt.Expression.Accept(this);
            return null!;
        }

        public object VisitPrintStmt(Print stmt)
        {
            object value = stmt.Expression.Accept(this);
            Console.WriteLine(value);
            return null!;
        }

        public object VisitVarStmt(Var stmt)
        {
            object value = stmt.Initializer.Accept(this);
            _environment.Define(stmt.Name, value);
            return null!;
        }

        public object VisitBlockStmt(Block stmt)
        {
            ExecuteBlock(stmt.Statements, new Environment(_environment));
            return null!;
        }

        public object VisitIfStmt(If stmt)
        {
            object condition = stmt.Condition.Accept(this);
            if (IsTruthy(condition))
            {
                Execute(stmt.ThenBranch);
            }
            else if (stmt.ElseBranch != null)
            {
                Execute(stmt.ElseBranch);
            }
            return null!;
        }

        public object VisitWhileStmt(While stmt)
        {
            while (IsTruthy(stmt.Condition.Accept(this)))
            {
                Execute(stmt.Body);
            }
            return null!;
        }

        private void ExecuteBlock(List<Stmt> statements, Environment newEnv)
        {
            var previous = _environment;
            try
            {
                _environment = newEnv;
                foreach (var stmt in statements)
                {
                    Execute(stmt);
                }
            }
            finally
            {
                _environment = previous;
            }
        }

        private bool IsTruthy(object value)
        {
            if (value is int i)
                return i != 0;
            return false;
        }
    }
}
