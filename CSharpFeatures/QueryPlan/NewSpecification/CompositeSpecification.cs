using System.Collections.Generic;

namespace Allen.Design.QueryPlan.NewSpecification
{
    public abstract class CompositeSpecification : Specification
    {
        protected CompositeSpecification()
        {
            _innerSpecifications = new List<Specification>();
        }
        private readonly IList<Specification> _innerSpecifications;
        public virtual IList<Specification> InnerSpecifications
        {
            get { return _innerSpecifications; }
        }
    }
}