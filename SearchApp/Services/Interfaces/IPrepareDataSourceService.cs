using SearchApp.Models;

namespace SearchApp.Services.Interfaces
{
    public interface IPrepareDataSourceService
    {
        public List<DataSourceObject> GetDataSource();

        public Task<IPrepareDataSourceService> Prepare();

        public IPrepareDataSourceService FlattenDataSource();

        public List<FlattenedDataSource> GetFlattenedDataSource();
    }
}
