using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SearchApp.Models;
using SearchApp.Services;
using SearchApp.Services.Interfaces;
using System.Collections.Generic;

namespace SearchAppTests
{
    public class SearchServiceTests
    {

        private ISearchService _searchService;
        Mock<IPrepareDataSourceService> _prepareDataSourceService;
        Mock<ILogger<SearchService>> _logger;

        [SetUp]
        public void Setup()
        {
            _prepareDataSourceService = new Mock<IPrepareDataSourceService>();
            _logger = new Mock<ILogger<SearchService>>();
            _searchService = new SearchService(_prepareDataSourceService.Object, _logger.Object);
        }

        [TestCase("James",2)]
        [TestCase("Jam", 3)]
        [TestCase("Katey Soltan", 1)]
        [TestCase("Jasmine Duncan", 0)]
        public void Search(string searchTerm, int resultCount)
        {
            _prepareDataSourceService.Setup(x => x.GetFlattenedDataSource()).Returns(GetFlattenedDataSource());
            _prepareDataSourceService.Setup(x => x.GetDataSource()).Returns(GetDataSource());

            var results = _searchService.Search(searchTerm.ToLower());

            Assert.IsNotNull(results);
            Assert.AreEqual(resultCount, results.Count);
        }

        private List<FlattenedDataSource> GetFlattenedDataSource()
        {
            return new List<FlattenedDataSource> {
            new FlattenedDataSource{
             Id = "8",
             FlattenedText = "james;kubu;hkubu7@craigslist.org;male"
            },
            new FlattenedDataSource{
             Id = "11",
             FlattenedText = "james;pfeffer;bpfeffera@amazon.com;male"
            },
            new FlattenedDataSource{
             Id = "14",
             FlattenedText = "chalmers;longfut;clongfujam@wp.com;male"
            },
            new FlattenedDataSource{
             Id = "18",
             FlattenedText = "katey;soltan;ksoltanh@simplemachines.org;female"
            }
            };
        }

        private List<DataSourceObject> GetDataSource()
        {
            return new List<DataSourceObject> {
            new DataSourceObject{
             Id = "8",
             FirstName = "James",
             LastName = "Kubu",
             Email = "hkubu7@craigslist.org",
             Gender = "Male"
            },
            new DataSourceObject{
             Id = "11",
             FirstName = "James",
             LastName = "Pfeffer",
             Email = "bpfeffera@amazon.com",
             Gender = "Male"
            },
            new DataSourceObject{
             Id = "14",
             FirstName = "Chalmers",
             LastName = "Longfut",
             Email = "clongfujam@wp.com",
             Gender = "Male"
            },
            new DataSourceObject{
             Id = "18",
             FirstName = "Katey",
             LastName = "Soltan",
             Email = "ksoltanh@simplemachines.org",
             Gender = "Female"
            }
            };
        }
    }
}