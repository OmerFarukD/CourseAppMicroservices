using AutoMapper;
using CourseApp.Service.Catalog.Dtos;
using CourseApp.Service.Catalog.Models;
using CourseApp.Service.Catalog.Settings;
using CourseApp.Shared.Dtos;
using MongoDB.Driver;

namespace CourseApp.Service.Catalog.Services;

public class CategoryManager : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryManager(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.Connection);
        var db = client.GetDatabase(databaseSettings.DatabaseName);
        _categoryCollection = db.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(ct => true).ToListAsync();
        var result = _mapper.Map<List<CategoryDto>>(categories);
            return Response<List<CategoryDto>>.Success(result,200);
    }

    public async Task<Response<CategoryDto>> CreateAsync(Category category)
    { 
        await _categoryCollection.InsertOneAsync(category);
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
        var category = await _categoryCollection.Find(x => x.Id.Equals(id)).SingleOrDefaultAsync();

        if (category is null)
        {
            return Response<CategoryDto>.Fail("Category Not Found",400);
        }
        
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);
    }
    
    
}