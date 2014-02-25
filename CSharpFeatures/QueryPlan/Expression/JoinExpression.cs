namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    public class JoinExpression : Expression
    {
        public JoinExpression(Expression src, Expression dest,
            Expression onCriteria)
        {
            Operands.Add(src);
            Operands.Add(dest);
            Operands.Add(onCriteria);
            //No  Return Type ,Can't Excute!
        }

        public override string ToString()
        {
            return string.Format("{0} join {1} on {2} "
                , WithAlia(Operands[0]), WithAlia(Operands[1]), Operands[2])
                ;
        }

        private string WithAlia(Expression expression)
        {
            var p = expression as ParameterExpression;
            if (p == null) return expression.ToString();
            else
            {
                return string.Format(" {0} as {1} ", p.Name, p.Alia);
            }
        }
    }
}