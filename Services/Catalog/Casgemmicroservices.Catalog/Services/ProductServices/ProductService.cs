using AutoMapper;
using CasgemMicroservices.Catalog.Dtos.ProductDtos;
using CasgemMicroservices.Catalog.Models;
using CasgemMicroservices.Catalog.Settings.Abstract;
using CasgemMicroservices.Shared.Dtos;
using MongoDB.Driver;

namespace CasgemMicroservices.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }

        public async Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto product)
        {
            var values = _mapper.Map<Product>(product);
            await _productCollection.InsertOneAsync(values);
            return Response<CreateProductDto>.Success(_mapper.Map<CreateProductDto>(values), 200);
        }

        public async Task<Response<NoContent>> DeleteProductAsync(string id)
        {
            var values = await _productCollection.DeleteOneAsync(x=>x.ProductId==id);
            if (values.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Silinecek veri bulunamadı", 404);
            }
        }

        public async Task<Response<ResultProductDto>> GetProductByIdAsync(string id)
        {
            var values = await _productCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            if (values == null)
            {
                return Response<ResultProductDto>.Fail("Product bulunamadı!", 404);
            }

            else
            {
                return Response<ResultProductDto>.Success(_mapper.Map<ResultProductDto>(values), 200);
            }
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListsAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values), 200);
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListWithCategoryAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            if (values.Any())
            {
                foreach (var item in values)
                {
                    item.Category = await _categoryCollection.Find(x => x.CategoryId == item.CategoryId).FirstOrDefaultAsync();
                }
            }
            else
            {
                values = new List<Product>();
            }
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values), 200);
        }

        public async Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto product)
        {
            var values = _mapper.Map<Product>(product);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == product.ProductId, values);

            if (result != null)
            {
                return Response<UpdateProductDto>.Success(204);
            }

            else
            {
                return Response<UpdateProductDto>.Fail("Güncellenecek Product bulunamadı", 404);
            }
        }
    }
}
