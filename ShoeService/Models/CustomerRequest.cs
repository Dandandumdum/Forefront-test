namespace ShoeService.Models;

public class CustomerRequest
{
    public string CustomerIdentification { get; set; }
    public string SizeFormat { get; set; }
    public string RecentEmail { get; set; }

    public CustomerRequest(string customerIdentification, string sizeFormat, string recentEmail)
    {
        CustomerIdentification = customerIdentification;
        SizeFormat = sizeFormat;
        RecentEmail = recentEmail;
    }
}