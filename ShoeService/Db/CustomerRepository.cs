namespace ShoeService.Db;

using ShoeService.Models;
/// <summary>
/// Fake data access layer for customer information
/// </summary>
public class CustomerRepository
{
    public async Task<Customer> FindCustomerById(int customerId)
    {
        return await Task.FromResult(new Customer
        {
            Id = customerId,
            Fullname = "Test Customer",
            Email = "test@example.com",
            BirthDate = new DateTime(1990, 1, 1),
            FootLastLength = 27.5
        });
    }

    public async Task<List<Customer>> FindCustomersByGroupId(int groupId)
    {
        return await Task.FromResult<List<Customer>>(
        [
            new() {
                Id = 1,
                GroupId = groupId,
                InGroupIndex = 1,
                Fullname = "Group Customer 1",
                Email = "group1@example.com",
                BirthDate = new DateTime(1990, 1, 1),
                FootLastLength = 26.5
            },
            new() {
                Id = 2,
                GroupId = groupId,
                InGroupIndex = 2,
                Fullname = "Group Customer 2",
                Email = "group2@example.com",
                BirthDate = new DateTime(1990, 1, 1),
                FootLastLength = 28.5
            }
        ]);
    }

    public async Task Save(Customer customer)
    {
        await Task.CompletedTask;
    }
}