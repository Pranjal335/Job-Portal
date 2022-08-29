using FluentAssertions;
using JobPortal.Web.Areas.Portal.Controllers;
using JobPortal.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Xunit.Abstractions;
using JobPortal.Web.Controllers;


namespace JobPortal.xUnitTestProject
{
  
    public partial class CategoriesApiTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CategoriesApiTest(
            ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]

        public void GetCategories_OkResult()
        {
            // 1. ARRANGE
            var dbName = nameof(CategoriesApiTest.GetCategories_OkResult);
            var logger = Mock.Of<ILogger<CategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new CategoriesController(dbContext, logger);

            // 2. ACT
            IActionResult actionResultGet = controller.GetCategories().Result;

            // 3. ASSERT
            // ---- Check if the IActionResult is OK (HTTP 200)
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ---- If the Status Cose is HTTP 200 "OK"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultGet as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetCategories_CheckCorrectResult()
        {
            // ARRANGE
            var dbName = nameof(CategoriesApiTest.GetCategories_CheckCorrectResult);
            var logger = Mock.Of<ILogger<CategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new CategoriesController(dbContext, logger);

            // ACT
            IActionResult actionResultGet = controller.GetCategories().Result;

            // ASSERT if the IActionResult is OK (HTTP 200)
            Assert.IsType<OkObjectResult>(actionResultGet);

            // Extract the result from the IActionResut object
            var okResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT if OkResult contains an object of the correct type
            Assert.IsAssignableFrom<List<JobCategory>>(okResult.Value);

            // Extract the Categories from the result of the action.
            var categories = okResult.Value.Should().BeAssignableTo<List<JobCategory>>().Subject;

            // ASSERT if categories is NOT NULL
            Assert.NotNull(categories);

            // ASEERT if number of categories matches with the TEST Data
            Assert.Equal(expected: DbContextMocker.TestData_Categories.Length,
                         actual: categories.Count);

            // ASSERT if data is correct
            int ndx = 0;
            foreach (JobCategory category in DbContextMocker.TestData_Categories)
            {
                
                

                Assert.Equal<int>(expected: category.JobCategoryId,
                                  actual: categories[ndx].JobCategoryId);
                Assert.Equal(expected: category.JobCategoryName,
                             actual: categories[ndx].JobCategoryName);

                

                _testOutputHelper.WriteLine($"Row # {ndx} OKAY");
                ndx++;
            }
        }

    }
}
