using System.Collections.Generic;

namespace Allen.Design.QueryPlan.NewSpecification
{
    public abstract class CompositeSpecification : Specification
    {
        protected CompositeSpecification()
        {
            _innerSpecifications = new List<Specification>();
        }
        private readonly List<Specification> _innerSpecifications;
        public virtual List<Specification> InnerSpecifications
        {
            get { return _innerSpecifications; }
        }
    }
}