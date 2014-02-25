

using System.Collections;
using System.Linq;

namespace Allen.Design.QueryPlan.NewSpecification
{
    class AndSpecification : CompositeSpecification
    {
        private readonly Specification _leftSideSpecification;
        private readonly Specification[] _rightSideSpecifications;

        public AndSpecification(Specification leftSideSpecification, params Specification[] rightSideSpecification)
        {
            // TODO: Complete member initialization
            _leftSideSpecification = leftSideSpecification;
            _rightSideSpecifications = rightSideSpecification;
        }

        public override Expression SatisfiedBy()
        {
            return _leftSideSpecification.SatisfiedBy()
                .And(_rightSideSpecifications
                    .Select(s => s.SatisfiedBy()).ToList());
        }
    }
}
