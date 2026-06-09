using System.Threading.Tasks;
using Xunit;
using ShoeService.Services;
using ShoeService.Models;
using ShoeService.Db;
using Microsoft.Extensions.Logging;

namespace ShoeService.Tests
{
    public class CustomerShoeServiceTests
    {
        [Fact]
        public void IsValidEmail_ValidAndInvalid()
        {
            Assert.True(ShoeService.Utilities.Utilities.IsValidEmail("test@example.com"));
            Assert.False(ShoeService.Utilities.Utilities.IsValidEmail("not-an-email"));
        }

        [Fact]
        public async Task GetCustomerShoeSize_ById_ReturnsExpected()
        {
            var repo = new CustomerRepository();
            var stats = new StatisticsService();
            var logger = new LoggerFactory().CreateLogger<CustomerShoeService>();
            var svc = new CustomerShoeService(repo, stats, logger);

            var req = new CustomerRequest("1", "UK_ADULT", "");
            var result = await svc.GetCustomerShoeSize(req);

            Assert.NotNull(result);
            Assert.Equal(1, result.CustomerId);
            Assert.Equal("Test Customer", result.Fullname);
            Assert.Equal("1990-01-01", result.BirthDate);
            Assert.InRange(result.ShoeSize, 7.3, 7.6);
        }

        [Fact]
        public async Task GetCustomerShoeSize_InvalidSizeFormat_ReturnsNull()
        {
            var repo = new CustomerRepository();
            var stats = new StatisticsService();
            var logger = new LoggerFactory().CreateLogger<CustomerShoeService>();
            var svc = new CustomerShoeService(repo, stats, logger);

            var req = new CustomerRequest("1", "INVALID_FORMAT", "");
            var result = await svc.GetCustomerShoeSize(req);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCustomerShoeSize_Group_ThrowsWhenReturningCustomerId()
        {
            var repo = new CustomerRepository();
            var stats = new StatisticsService();
            var logger = new LoggerFactory().CreateLogger<CustomerShoeService>();
            var svc = new CustomerShoeService(repo, stats, logger);

            var req = new CustomerRequest("G123-2", "UK_ADULT", "");

            await Assert.ThrowsAsync<System.InvalidOperationException>(async () => await svc.GetCustomerShoeSize(req));
        }
    }
}
