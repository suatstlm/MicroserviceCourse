using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Settings;
using MongoDB.Driver;
using Shared.Dtos;

namespace Catalog.API.Services
{
    public class CategoryService : ICategoryService
    {
        readonly IMongoCollection<Category> _categoryCollection;
        readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection =database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = _categoryCollection.Find(_ => true).ToListAsync();

            if (categories is null || !categories.Result.Any())
            {
                return Response<List<CategoryDto>>.Fail("Categories not found",400);
            }

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories.Result), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var newCategory = _mapper.Map<Category>(categoryCreateDto);
            await _categoryCollection.InsertOneAsync(newCategory);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(newCategory), 201);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
