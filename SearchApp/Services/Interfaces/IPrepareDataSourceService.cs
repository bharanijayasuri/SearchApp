using SearchApp.Models;

namespace SearchApp.Services.Interfaces
{
    public interface IPrepareDataSourceService
    {
        public List<DataSourceObject> GetDataSource();

        public Task<IPrepareDataSourceService> Prepare(string fileName);

        public IPrepareDataSourceService FlattenDataSource();

        public List<FlattenedDataSource> GetFlattenedDataSource();
    }
}
