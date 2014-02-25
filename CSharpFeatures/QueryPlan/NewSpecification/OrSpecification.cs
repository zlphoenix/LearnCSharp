using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Allen.Design.QueryPlan.NewSpecification
{
    class OrSpecification : CompositeSpecification
    {

        public OrSpecification(Specification leftSideSpecification, params Specification[] rightSideSpecification)
        {

            base.InnerSpecifications.Add(leftSideSpecification);
            base.InnerSpecifications.AddRange(rightSideSpecification);
        }

        public override Expression SatisfiedBy()
        {
            Expression result = null;
            foreach (var innerSpecification in InnerSpecifications)
            {
                result = result == null
                    ? innerSpecification.SatisfiedBy()
                    : result.Or(innerSpecification.SatisfiedBy());
            }
            return result;
        }
    }
}
