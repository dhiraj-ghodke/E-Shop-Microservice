using Catalog.API.Products.GetProducts;
using System.Threading;

namespace Catalog.API.Products.GetProductByCategroy
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryHandler 
        (IDocumentSession session)
        : IQueryHandler <GetProductByCategoryQuery, GetProductByCategoryResult> 
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            

            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync();

            return new GetProductByCategoryResult(products);

        }
    }
}
