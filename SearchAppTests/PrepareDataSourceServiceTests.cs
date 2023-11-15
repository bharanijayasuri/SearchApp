using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using SearchApp.Services;
using SearchApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAppTests
{
    public class PrepareDataSourceServiceTests
    {
        private IPrepareDataSourceService _prepareDataSourceService;
        private Mock<ILogger<PrepareDataSourceService>> _logger;

        [SetUp]
        public void Setup()
        { 
            _logger = new Mock<ILogger<PrepareDataSourceService>>();
            _prepareDataSourceService = new PrepareDataSourceService(_logger.Object);
        }

        [Test]
        public async Task PrepareSuccessTest()
        {
            await _prepareDataSourceService.Prepare("DataSource.json");

            Assert.IsTrue(_prepareDataSourceService.GetDataSource().Any());
        }

        [Test]
        public void PrepareExceptionTest()
        {
            Assert.ThrowsAsync<JsonSerializationException>(async ()=>await _prepareDataSourceService.Prepare("Empty.json"));
            
        }

        [Test]
        public async Task FlattenDataSourceSuccessTest()
        {
            await _prepareDataSourceService.Prepare("DataSource.json");
            _prepareDataSourceService.FlattenDataSource();

            Assert.IsTrue(_prepareDataSourceService.GetFlattenedDataSource().Any());
            //Assert.ThrowsAsync<JsonSerializationException>(async () => await _prepareDataSourceService.Prepare("Empty.json"));

        }

        [Test]
        public async Task FlattenDataSourceExceptionTest()
        {
            await _prepareDataSourceService.Prepare("NullItem.json");

            Assert.Throws<NullReferenceException>(() => _prepareDataSourceService.FlattenDataSource());

        }
    }
}
