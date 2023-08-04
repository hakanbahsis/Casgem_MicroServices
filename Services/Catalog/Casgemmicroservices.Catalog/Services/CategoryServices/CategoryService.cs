using AutoMapper;
using CasgemMicroservices.Catalog.Dtos.CategoryDtos;
using CasgemMicroservices.Catalog.Models;
using CasgemMicroservices.Catalog.Settings.Abstract;
using CasgemMicroservices.Shared.Dtos;
using MongoDB.Driver;

namespace CasgemMicroservices.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }

        public async Task<Response<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto category)
        {
            var values = _mapper.Map<Category>(category);
            await _categoryCollection.InsertOneAsync(values);
            return Response<CreateCategoryDto>.Success(_mapper.Map<CreateCategoryDto>(values), 200);
        }

        public async Task<Response<NoContent>> DeleteCategoryAsync(string id)
        {
            var values = await _categoryCollection.DeleteOneAsync(id);
            if (values.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Silinecek Kategori Bulunamadı!", 404);
            }
        }

        public async Task<Response<ResultCategoryDto>> GetCategoryByIdAsync(string id)
        {
            var values = await _categoryCollection.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
            if (values == null)
            {
                return Response<ResultCategoryDto>.Fail("Böyle bir Id Bulunamadı", 404);
            }

            else
            {
                return Response<ResultCategoryDto>.Success(_mapper.Map<ResultCategoryDto>(values), 200);
            }

        }

        public async Task<Response<List<ResultCategoryDto>>> GetCategoryListAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            return Response<List<ResultCategoryDto>>.Success(_mapper.Map<List<ResultCategoryDto>>(values), 200);
        }

        public async Task<Response<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto category)
        {
            var values=_mapper.Map<Category>(category);
            var result = await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == category.CategoryId,values);

            if (result == null)
            {
                return Response<UpdateCategoryDto>.Fail("Böyle bir Id Bulunamadı", 404);

            }

            else
            {
                return Response<UpdateCategoryDto>.Success(204);
            }
        }
    }
}
