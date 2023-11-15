using SearchApp.Models;
using SearchApp.Services.Interfaces;

namespace SearchApp.Services
{
    public class SearchService : ISearchService
    {
        private readonly IPrepareDataSourceService _prepareDataSource;
        private readonly ILogger _logger;

        public SearchService(IPrepareDataSourceService prepareDataSource, ILogger<SearchService> logger)
        {
            _prepareDataSource = prepareDataSource;
            _logger = logger;
        }

        public List<DataSourceObject> Search(string searchString)
        {
            var returnList = new List<DataSourceObject>();
            try
            {
                var searchSplit = searchString.Split(' ');

                foreach (var item in _prepareDataSource.GetFlattenedDataSource())
                {
                    bool found = true;
                    foreach (var searchTerm in searchSplit)
                    {
                        if (!IsStringPresent(item,searchTerm)) {
                            found = false;
                            break;
                        }
                    }

                    if (found)
                        returnList.Add(_prepareDataSource.GetDataSource().FirstOrDefault(x => x.Id == item.Id));
                }

                return returnList;

            }
            catch (Exception msg)
            {
                _logger.LogError(msg.Message);
            }
            return new List<DataSourceObject>();
        }

        private bool IsStringPresent(FlattenedDataSource flattenedObject, string searchString)
        { 
            return flattenedObject.FlattenedText.Contains(searchString);
        }
    }
}
