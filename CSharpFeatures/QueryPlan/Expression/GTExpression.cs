namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    /// <summary>
    ///     大于 Great Than
    /// </summary>
    public class GreaterThanExpression : Expression
    {
        public GreaterThanExpression(Expression left, Expression right)
        {
            Operands.Add(left);
            Operands.Add(right);
            //TODO validate ParamType
            ReturnType = typeof (bool);
        }

        public override string ToString()
        {
            return string.Format(" {0} > {1}", Operands[0], Operands[1]);
        }
    }
}