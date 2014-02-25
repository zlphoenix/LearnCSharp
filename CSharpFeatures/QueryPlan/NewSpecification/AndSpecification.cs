

using System.Collections;
using System.Linq;

namespace Allen.Design.QueryPlan.NewSpecification
{
    class AndSpecification : CompositeSpecification
    {

        public AndSpecification(Specification leftSideSpecification, params Specification[] rightSideSpecification)
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
                    : result.And(innerSpecification.SatisfiedBy());
            }
            return result;
        }
    }
}
