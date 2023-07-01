namespace CourseApp.Service.Catalog.Settings;

public interface IDatabaseSettings
{
    public string CourseCollectionName { get; set; }
    public string CategoryCollectionName { get; set; }
    public string Connection { get; set; }
    public string DatabaseName { get; set; }
}