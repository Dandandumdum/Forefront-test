namespace ShoeService.Services;
using ShoeService.Models;
using ShoeService.Db;
using ShoeService.Utilities;
using ShoeService.Enums;


public class CustomerShoeService
{
    private readonly CustomerRepository _customerRepository;
    private readonly StatisticsService _statisticsService;

     private readonly ILogger<CustomerShoeService> _logger;
        private const string DATE_FORMAT = "yyyy-MM-dd";

    public CustomerShoeService(CustomerRepository customerRepository, StatisticsService statisticsService, ILogger<CustomerShoeService> logger)
    {
        _customerRepository = customerRepository;
        _statisticsService = statisticsService;
        _logger = logger;
    }

    public async Task<CustomerShoeSize?> GetCustomerShoeSize(CustomerRequest request)
    {
        int? groupId = null;
        int? inGroupIdx = null;
        int? customerId = null;


        if (request.CustomerIdentification.StartsWith("G"))
        {
            // parse in-group identifiers, e.g. G123-4
            string[] parts = request.CustomerIdentification.Split("-");
            string groupIdStr = parts[0].Substring(1);
            string inGroupIndexStr = parts[1];
            groupId = int.Parse(groupIdStr);
            inGroupIdx = int.Parse(inGroupIndexStr);
        }
        else
        {
            customerId = int.Parse(request.CustomerIdentification);
        }

        ShoeSizeType sizeType;
        try
        {
            sizeType = Enum.Parse<ShoeSizeType>(request.SizeFormat);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return null;
        }

        Customer customer = null;
        if (customerId != null)
        {
            customer = await _customerRepository.FindCustomerById(customerId.Value);
        }
        else
        {
            if(groupId == null || inGroupIdx == null)
            {
                throw new ArgumentException("Invalid group identifier");
            }

            Task<List<Customer>> groupCustomers = _customerRepository.FindCustomersByGroupId(groupId.Value);
            int inGroupIdxValue = inGroupIdx.Value;
            customer = (await groupCustomers).Where(c => c.InGroupIndex == inGroupIdxValue).First();
        }

        if (request.RecentEmail != null && request.RecentEmail.Length > 0)
        {
            if (Utilities.IsValidEmail(request.RecentEmail))
            {
                customer.Email = request.RecentEmail;
                await _customerRepository.Save(customer);
            }
            else
            {
                //No real logging implemented, so just write to console
                _logger.LogWarning($"Invalid email provided: {request.RecentEmail}");
            }
        } else
        {    
            _logger.LogInformation("No recent email provided");
        }

        // get the foot last length, in centimeters
        double footLastLengthCm = customer.FootLastLength;
        double size = 0;
        // Source:
        // https://en.wikipedia.org/wiki/Shoe_size
        switch (sizeType)
        {
            case ShoeSizeType.UK_ADULT:
                size = 3 * Utilities.Convert(footLastLengthCm) - 25;
                break;
            case ShoeSizeType.UK_CHILD:
                size = 3 * Utilities.Convert(footLastLengthCm) - 12;
                break;
            case ShoeSizeType.US_CHILD:
                throw new NotImplementedException("US_CHILD format not yet implemented");
            case ShoeSizeType.US_CUSTOMARY_MALE:
                size = 3 * Utilities.Convert(footLastLengthCm) - 24;
                break;
            case ShoeSizeType.US_CUSTOMARY_FEMALE:
                throw new NotImplementedException("US_CUSTOMARY_FEMALE format not yet implemented");
        }

        if ((sizeType == ShoeSizeType.UK_ADULT && size > 35) || (sizeType == ShoeSizeType.UK_CHILD && size > 20) || (sizeType == ShoeSizeType.US_CUSTOMARY_MALE && size > 36))
        {
            await _statisticsService.RegisterStatistic("big_shoe_size", 1);
        }

        DateTime birthDate = customer.BirthDate;
        string dateString = birthDate.ToString(DATE_FORMAT);

        return new CustomerShoeSize(customerId.Value, size, customer.Fullname, dateString);

    }

}        