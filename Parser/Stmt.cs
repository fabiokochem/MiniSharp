namespace ToyLang.Parser
{
    public abstract class Stmt
    {
        public interface IVisitor<T>
        {
            T VisitPrintStmt(Print stmt);
            T VisitExprStmt(ExprStmt stmt);
            T VisitVarStmt(Var stmt);
            T VisitBlockStmt(Block stmt);
            T VisitIfStmt(If stmt);
            T VisitWhileStmt(While stmt);
        }

        public class Print : Stmt
        {
            public Expr Expression;
            public Print(Expr expr) { Expression = expr; }

            public override T Accept<T>(IVisitor<T> visitor) => visitor.VisitPrintStmt(this);
        }

        public class ExprStmt : Stmt
        {
            public Expr Expression;
            public ExprStmt(Expr expr) { Expression = expr; }

            public override T Accept<T>(IVisitor<T> visitor) => visitor.VisitExprStmt(this);
        }

        public class Var : Stmt
        {
            public string Name;
            public Expr Initializer;

            public Var(string name, Expr init)
            {
                Name = name;
                Initializer = init;
            }

            public override T Accept<T>(IVisitor<T> visitor) => visitor.VisitVarStmt(this);
        }

        public class Block : Stmt
        {
            public List<Stmt> Statements;
            public Block(List<Stmt> statements) { Statements = statements; }

            public override T Accept<T>(IVisitor<T> visitor) => visitor.VisitBlockStmt(this);
        }

        public class If : Stmt
        {
            public Expr Condition;
            public Stmt ThenBranch;
            public Stmt? ElseBranch;

            public If(Expr condition, Stmt thenBranch, Stmt? elseBranch = null)
            {
                Condition = condition;
                ThenBranch = thenBranch;
                ElseBranch = elseBranch;
            }

            public override T Accept<T>(IVisitor<T> visitor) => visitor.VisitIfStmt(this);
        }

        public class While : Stmt
        {
            public Expr Condition;
            public Stmt Body;

            public While(Expr condition, Stmt body)
            {
                Condition = condition;
                Body = body;
            }

            public override T Accept<T>(IVisitor<T> visitor) => visitor.VisitWhileStmt(this);
        }

        public abstract T Accept<T>(IVisitor<T> visitor);
    }
}
