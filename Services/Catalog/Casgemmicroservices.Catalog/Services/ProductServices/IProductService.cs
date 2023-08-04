using CasgemMicroservices.Catalog.Dtos.ProductDtos;
using CasgemMicroservices.Shared.Dtos;

namespace CasgemMicroservices.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<Response<List<ResultProductDto>>> GetProductListsAsync();
        Task<Response<ResultProductDto>> GetProductByIdAsync(string id);
        Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto product);
        Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto product);
        Task<Response<NoContent>> DeleteProductAsync(string id);
        Task<Response<List<ResultProductDto>>> GetProductListWithCategoryAsync();
    }
}
