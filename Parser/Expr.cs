namespace ToyLang.Parser
{
    public abstract class Expr
    {
        public interface IVisitor<T>
        {
            T VisitLiteralExpr(Literal expr);
            T VisitVariableExpr(Variable expr);
            T VisitBinaryExpr(Binary expr);
        }

        public class Literal : Expr
        {
            public object Value;
            public Literal(object value) { Value = value; }

            public override T Accept<T>(IVisitor<T> visitor) => visitor.VisitLiteralExpr(this);
        }

        public class Variable : Expr
        {
            public string Name;
            public Variable(string name) { Name = name; }

            public override T Accept<T>(IVisitor<T> visitor) => visitor.VisitVariableExpr(this);
        }

        public class Binary : Expr
        {
            public Expr Left;
            public string Operator;
            public Expr Right;

            public Binary(Expr left, string op, Expr right)
            {
                Left = left;
                Operator = op;
                Right = right;
            }

            public override T Accept<T>(IVisitor<T> visitor) => visitor.VisitBinaryExpr(this);
        }

        public abstract T Accept<T>(IVisitor<T> visitor);
    }
}
