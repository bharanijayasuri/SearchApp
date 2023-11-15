using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SearchApp.Controllers;
using SearchApp.Models;
using SearchApp.Services.Interfaces;
using System.Collections.Generic;

namespace SearchAppTests
{
    public class SearchControllerTests
    {
        private SearchController _searchController;
        private Mock<ISearchService> _searchService;
        private Mock<ILogger<SearchController>> _logger;

        [SetUp]
        public void Setup()
        { 
            _searchService = new Mock<ISearchService>();
            _logger = new Mock<ILogger<SearchController>>();
            _searchController = new SearchController(_searchService.Object, _logger.Object);
        }

        [Test]
        public void SearchTestReturnsBadRequest()
        {
            var actionResult = _searchController.Search(string.Empty);

            Assert.IsInstanceOf(typeof(BadRequestResult), actionResult);
        }

        [Test]
        public void SearchTestReturnsOk()
        {
            _searchService.Setup(x => x.Search(It.IsAny<string>())).Returns(new List<DataSourceObject>());
            var actionResult = _searchController.Search("James");

            _searchService.Verify(x => x.Search(It.IsAny<string>()), Times.Once);
            
            Assert.IsInstanceOf(typeof(OkObjectResult), actionResult);
        }
    }
}
