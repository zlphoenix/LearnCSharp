using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Allen.Design.QueryPlan.NewSpecification
{
    class NotSpecification : Specification
    {
        private Specification _originalSpecification;

        public NotSpecification(Specification specification)
        {
            // TODO: Complete member initialization
            _originalSpecification = specification;
        }
        public override Expression SatisfiedBy()
        {
            throw new NotImplementedException();
        }
    }
}
