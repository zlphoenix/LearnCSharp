using System;
using System.Linq;

namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    public class MemberExpress : Expression
    {
        private object MemberName { get; set; }

        public MemberExpress(Expression parameter, string memberName, Type ofType = null)
        {
            if (ofType != null)
                ReturnType = ofType;
            else
            {
                ReturnType = typeof(object);
            }
            Operands.Add(parameter);
            MemberName = memberName;
        }

        public override string ToString()
        {
            var param = Operands.FirstOrDefault();
            return string.Format("({0}).{1}", param.ToString(), MemberName);
        }
    }
}