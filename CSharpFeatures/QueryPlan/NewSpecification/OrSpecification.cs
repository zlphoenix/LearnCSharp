using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Allen.Design.QueryPlan.NewSpecification
{
    class OrSpecification : Specification
    {
        private Specification leftSideSpecification;
        private Specification rightSideSpecification;

        public OrSpecification(Specification leftSideSpecification, Specification rightSideSpecification)
        {
            // TODO: Complete member initialization
            this.leftSideSpecification = leftSideSpecification;
            this.rightSideSpecification = rightSideSpecification;
        }
        public override Expression SatisfiedBy()
        {
            throw new NotImplementedException();
        }
    }
}
