namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    internal class NotExpression : Expression
    {
        private readonly Expression _innerExpression;

        public NotExpression(Expression expression)
        {
            _innerExpression = expression;
            ReturnType = typeof (bool);
        }

        public Expression InnerExpression
        {
            get { return _innerExpression; }
        }

        public override string ToString()
        {
            return string.Format(" not {0}", InnerExpression);
        }
    }
}