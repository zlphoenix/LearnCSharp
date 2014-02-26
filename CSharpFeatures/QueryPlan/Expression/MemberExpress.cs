using System;
using System.Linq;

namespace Allen.Design.QueryPlan.NewSpecification.Expression
{
    public class MemberExpress : Expression
    {
        public MemberExpress(Expression parameter, string memberName, Type ofType = null)
        {
            if (ofType != null)
                ReturnType = ofType;
            else
            {
                ReturnType = typeof (object);
            }
            Operands.Add(parameter);
            MemberName = memberName;
        }

        private object MemberName { get; set; }

        public override string ToString()
        {
            Expression param = Operands.FirstOrDefault();
            return string.Format("({0}).{1}", param, MemberName);
        }
    }
}