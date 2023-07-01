using AutoMapper;
using CourseApp.Service.Catalog.Dtos;
using CourseApp.Service.Catalog.Models;
using CourseApp.Service.Catalog.Settings;
using CourseApp.Shared.Dtos;
using MongoDB.Driver;

namespace CourseApp.Service.Catalog.Services;

public class CourseManager : ICourseService
{
    private readonly IMongoCollection<Course> _courseCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CourseManager(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.Connection);
        var db = client.GetDatabase(databaseSettings.DatabaseName);
        _courseCollection = db.GetCollection<Course>(databaseSettings.CourseCollectionName);
        _categoryCollection = db.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<Response<List<CourseDto>>> GetAllAsync()
    {
        var courses = await _courseCollection.Find(x => true).ToListAsync();
        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection
                    .Find(x => x.Id != null && x.Id.Equals(course.CategoryId))
                    .FirstAsync();
            }
        }
        else
        {
            courses = new List<Course>();
        }
        
        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses),200);
    }

    public async Task<Response<CourseDto>> GetByIdAsync(string id)
    {
        var course = await _courseCollection.Find(x => x.Id.Equals(id)).SingleOrDefaultAsync();

        if (course is not null)
        {
            course.Category =await _categoryCollection.Find(x => x.Id.Equals(course.CategoryId)).SingleAsync();
        }
        else
        {
            return Response<CourseDto>.Fail("Course Not Found",400);
        }
        
        return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course),200);
    }

    public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
    {
        var courses = await _courseCollection.Find(x => x.UserId != null && x.UserId.Equals(userId)).ToListAsync();
        if (courses.Any())
        {
            foreach (var course in courses)
            {
                course.Category = await _categoryCollection
                    .Find(x => x.Id != null && x.Id.Equals(course.CategoryId))
                    .FirstAsync();
            }
        }
        else
        {
            courses = new List<Course>();
        }
        
        return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses),200);
    }

    public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
    {
        var course = _mapper.Map<Course>(courseCreateDto);
        course.CreatedTime=DateTime.Now;
        await _courseCollection.InsertOneAsync(course);

        var result = _mapper.Map<CourseDto>(course);
        
        return Response<CourseDto>.Success(result,200);
    }

    public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
    {
        
        var updateCourse = _mapper.Map<Course>(courseUpdateDto);
        var result = await _courseCollection
            .ReplaceOneAsync(x => x.Id.Equals(courseUpdateDto.Id), updateCourse);
        if (result is null)
        {
            return Response<NoContent>.Fail("Course Not Found",400);
        }
        
        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var result = await _courseCollection.DeleteOneAsync(x => x.Id.Equals(id));
        if (result is null)
        {
            return Response<NoContent>.Fail("Course Not Found",400);
        }
        return Response<NoContent>.Success(204);
    }
}