namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    /// <summary>
    /// 相等
    /// </summary>
    public class EqualExpression : Expression
    {

        public EqualExpression(Expression left, Expression right)
        {
            this.Operands.Add(left);
            this.Operands.Add(right);
            //TODO validate ParamType
            this.ReturnType = typeof(bool);
        }

        public override string ToString()
        {
            return string.Format(" {0} = {1}", Operands[0], Operands[1]);
        }
    }
}