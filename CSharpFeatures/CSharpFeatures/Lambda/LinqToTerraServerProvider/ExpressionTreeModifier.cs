using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace CSharpFeatures.Lambda.LinqToTerraServerProvider
{
    internal class ExpressionTreeModifier : ExpressionVisitor
    {
        private IQueryable<Place> queryablePlaces;

        internal ExpressionTreeModifier(IQueryable<Place> places)
        {
            this.queryablePlaces = places;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // Replace the constant QueryableTerraServerData arg with the queryable Place collection.
            if (c.Type == typeof(QueryableTerraServerData<Place>))
                return Expression.Constant(this.queryablePlaces);
            else
                return c;
        }
    }
}
