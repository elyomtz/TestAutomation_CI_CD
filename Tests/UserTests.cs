using System.Net;
using TestAutomation_CI_CD.Business;
using TestAutomation_CI_CD.Core;
using TestAutomation_CI_CD.Core.API_Test_Core.Models;

namespace TestAutomation_CI_CD.Tests
{
    [TestFixture]
    [Category("API")]
    public class UsersTests : TestBase
    {
        /*Task 1*/
        [Test]
        public async Task GetUsers_ShouldReturn200Ok()
        {
            // Act
            var response = await UserService.GetUsersAsync();

            // Assert
            Assertions.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response status code should be 200 OK");
        }

        [Test]
        public async Task GetUsers_ShouldReturnListOfUsers()
        {
            // Act
            var response = await UserService.GetUsersAsync();

            // Assert
            Assertions.That(response.Data, Is.Not.Null, "Response is null");
            Assertions.That(response.Data, Is.Not.Empty, "Response is empty");
            Assertions.That(response.Data, Is.TypeOf<List<User>>(), "Response is not a list");
        }

        [Test]
        public async Task GetUsers_ShouldContainRequiredUserInformation()
        {
            // Act
            var response = await UserService.GetUsersAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data, Is.Not.Null);

            foreach (var user in response.Data!)
            {
                Assert.Multiple(() =>
                {
                    Assertions.That(user.Id, Is.GreaterThan(0), "User Id is less than 0");
                    Assertions.That(user.Name, Is.Not.Null.And.Not.Empty, "User name is null or empty");
                    Assertions.That(user.Username, Is.Not.Null.And.Not.Empty, "Username is null or empty");
                    Assertions.That(user.Email, Is.Not.Null.And.Not.Empty, "Email is null or empty");
                    Assertions.That(user.Address, Is.Not.Null, "Address is null or empty");
                    Assertions.That(user.Phone, Is.Not.Null.And.Not.Empty, "Phone is null or empty");
                    Assertions.That(user.Website, Is.Not.Null.And.Not.Empty, "Website is null or empty");
                    Assertions.That(user.Company, Is.Not.Null, "Company is null");
                });
            }
        }

        [Test]
        public async Task GetUsers_ShouldNotReturnErrors()
        {
            // Act
            var response = await UserService.GetUsersAsync();

            // Assert
            Assertions.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response is not OK");
            Assertions.That(response.IsSuccessful, Is.True, "Response is False");
            Assertions.That(response.ErrorMessage, Is.Null, "Error message is not null");
            Assertions.That(response.ErrorException, Is.Null, "Error exception is not null");
        }

        /*Task 2*/
        [Test]
        public async Task GetUsers_ShouldReturnApplicationJsonContentType()
        {
            // Act
            var response = await UserService.GetUsersAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assertions.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response is not OK");
                Assertions.That(response.ErrorMessage, Is.Null, "Error message is not null");
            });

            var contentType = response.ContentHeaders?
                .FirstOrDefault(x =>
                    x.Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase));

            Assertions.That(contentType, Is.Not.Null, "contentType is null");
            Assertions.That(contentType!.Value?.ToString(), Is.EqualTo("application/json; charset=utf-8"), "header i not application/json or charset != utf-8");
        }

        /*Task 3*/
        [Test]
        public async Task GetUsers_ShouldReturn10UsersWithValidData()
        {
            // Act
            var response = await UserService.GetUsersAsync();

            // Assert response
            Assert.Multiple(() =>
            {
                Assertions.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Response is not OK");
                Assertions.That(response.ErrorMessage, Is.Null, "Error message is not null");
                Assertions.That(response.ErrorException, Is.Null, "Error exception is not null");
                Assertions.That(response.Data, Is.Not.Null, "Data is null");
            });

            var users = response.Data!;

            // Validate number of users
            Assert.That(users, Has.Count.EqualTo(10));

            // Validate unique IDs
            var userIds = users.Select(user => user.Id).ToList();

            Assertions.That(userIds.Distinct().Count(), Is.EqualTo(users.Count), "Every user should have a unique ID.");

            // Validate individual users
            foreach (var user in users)
            {
                Assert.Multiple(() =>
                {
                    Assertions.That(user.Name, Is.Not.Null.And.Not.Empty, "Name should not be empty.");
                    Assertions.That(user.Username, Is.Not.Null.And.Not.Empty, "Username should not be empty.");
                    Assertions.That(user.Company, Is.Not.Null, "Company should exist.");
                    Assertions.That(user.Company.Name, Is.Not.Null.And.Not.Empty, "Company name should not be empty.");
                });
            }
        }

        /*Task 4*/
        [Test]
        public async Task CreateUser_ShouldReturnCreatedUser()
        {
            // Arrange
            var newUser = new CreateUserRequest
            {
                Name = "Elyoenai Martinez",
                Username = "emartinez"
            };

            // Act
            var response = await UserService.CreateUserAsync(newUser);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "User could not be created");
                Assert.That(response.ErrorMessage, Is.Null, "Error message is not null");
                Assert.That(response.ErrorException, Is.Null, "Error exception is not null");
                Assert.That(response.Data, Is.Not.Null, "Data is null");
                Assert.That(response.Content, Is.Not.Null.And.Not.Empty, "Content is null");
            });

            Assert.That(response.Data!.Id, Is.GreaterThan(0));
        }

        /*Task 5*/
        [Test]
        public async Task GetInvalidEndpoint_ShouldReturn404()
        {
            var response = await UserService.GetInvalidEndpointAsync();

            Assertions.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Error 404 should be retrieved");

        }

    }
}