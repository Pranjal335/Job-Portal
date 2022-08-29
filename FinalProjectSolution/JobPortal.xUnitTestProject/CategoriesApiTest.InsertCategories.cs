using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using Xunit.Abstractions;
using Moq;
using Microsoft.Extensions.Logging;
using JobPortal.Web.Controllers;
using JobPortal.Web.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using JobPortal.xUnitTestProject;


namespace JobPortal.xUnitTestProject
{


    public partial class CategoriesApiTest
    {
        [Fact]
        public void InsertCategory_OkResult()
        {
            // ARRANGE
            var dbName = nameof(CategoriesApiTest.InsertCategory_OkResult);
            var logger = Mock.Of<ILogger<CategoriesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!

            var controller = new CategoriesController(dbContext, logger);
            JobCategory categoryToAdd = new JobCategory
            {
                JobCategoryId = 5,
                JobCategoryName = null             // INVALID!  CategoryName is REQUIRED
            };

            // ACT
            IActionResult actionResultPost = controller.PostCategory(categoryToAdd).Result;

            // ASSERT - check if the IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultPost);

            // ASSERT - check if the Status Code is (HTTP 200) "Ok", (HTTP 201 "Created")
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultPost as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);

            // Extract the result from the IActionResult object.
            var postResult = actionResultPost.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT - if the result is a CreatedAtActionResult
            Assert.IsType<CreatedAtActionResult>(postResult.Value);

            // Extract the inserted Category object
            JobCategory actualCategory = (postResult.Value as CreatedAtActionResult).Value
                                      .Should().BeAssignableTo<JobCategory>().Subject;

            // ASSERT - if the inserted Category object is NOT NULL
            Assert.NotNull(actualCategory);

            Assert.Equal(categoryToAdd.JobCategoryId, actualCategory.JobCategoryId);
            Assert.Equal(categoryToAdd.JobCategoryName, actualCategory.JobCategoryName);
        }
    }
}
