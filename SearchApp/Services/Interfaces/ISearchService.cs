using SearchApp.Models;

namespace SearchApp.Services.Interfaces
{
    public interface ISearchService
    {
        public List<DataSourceObject> Search(string searchString);
    }
}
