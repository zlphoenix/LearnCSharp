using System;

namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    public class ParameterExpression : Expression
    {
        public string Name { get; set; }
        public string Alia { get; set; }
        public Type Type { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}