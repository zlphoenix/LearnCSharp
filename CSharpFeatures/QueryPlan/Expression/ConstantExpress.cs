using System;

namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    public class ConstantExpression : Expression
    {
        private readonly object _value;

        public ConstantExpression(object value, Type ofType = null)
        {
            if (ofType != null)
            {
                ReturnType = ofType;
            }
            else
            {
                ReturnType = value.GetType();
            }
            _value = value;
        }

        public override string ToString()
        {
            return string.Format(" '{0}' "
                , _value != null
                    ? _value.ToString()
                    : string.Empty);
        }
    }
}