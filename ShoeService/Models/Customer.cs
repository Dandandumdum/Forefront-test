namespace ShoeService.Models;

public class Customer
{
    public int? Id { get; set; }
    public int? GroupId { get; set; }
    public int? InGroupIndex { get; set; }
    public string Email { get; set; }
    public string Fullname { get; set; }
    public DateTime BirthDate { get; set; }
    public double FootLastLength { get; set; }

}