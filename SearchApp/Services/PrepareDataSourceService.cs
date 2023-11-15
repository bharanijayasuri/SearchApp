using SearchApp.Models;
using SearchApp.Services.Interfaces;
using Newtonsoft.Json;

namespace SearchApp.Services
{
    public class PrepareDataSourceService : IPrepareDataSourceService
    {
        private List<FlattenedDataSource> _flattenedDataSource { get; set; }
        private List<DataSourceObject> _dataSource { get; set; }
        private readonly ILogger _logger;

        public PrepareDataSourceService(ILogger<IPrepareDataSourceService> logger)
        {
            _logger = logger;
        }

        public IPrepareDataSourceService FlattenDataSource()
        {
            try
            {
                if (this._dataSource != null && this._dataSource.Any())
                {
                    this._flattenedDataSource = new List<FlattenedDataSource>();
                    foreach (var dataSource in this._dataSource)
                    {
                        this._flattenedDataSource.Add(GetFlattenedDataSourceItem(dataSource));
                    }
                }
            }
            catch (Exception msg)
            {
                _logger.LogError(msg.Message);
            }
            return this;
        }

        public List<DataSourceObject> GetDataSource()
        {
            return _dataSource;
        }

        public List<FlattenedDataSource> GetFlattenedDataSource()
        {
            return _flattenedDataSource;
        }

        public async Task<IPrepareDataSourceService> Prepare()
        {
            try
            {
                var file = await File.ReadAllTextAsync("DataSource.json");

                if (!string.IsNullOrWhiteSpace(file))
                {
                    this._dataSource = JsonConvert.DeserializeObject<List<DataSourceObject>>(file);
                }
            }
            catch (Exception msg)
            {
                _logger.LogError(msg.Message);
            }
            return this;
        }

        #region Private functions

        private FlattenedDataSource GetFlattenedDataSourceItem(DataSourceObject dataSource)
        { 
            var itemToReturn = new FlattenedDataSource();
            itemToReturn.Id = dataSource.Id;
            itemToReturn.FlattenedText = GetFlattenedText(dataSource);

            return itemToReturn;
        }

        private string GetFlattenedText(DataSourceObject dataSource)
        {
            return $"{dataSource.FirstName.ToLowerInvariant()};{dataSource.LastName.ToLowerInvariant()};{dataSource.Email.ToLowerInvariant()};{dataSource.Gender.ToLowerInvariant()}";
        }

        #endregion
    }
}
