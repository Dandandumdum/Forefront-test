namespace ShoeService.Models;
    public class CustomerShoeSize
    {
    private int? id;
    private double size;
    private string dateString;

    public int CustomerId { get; set; }

        public double ShoeSize { get; set; }

        public string Fullname { get; set; }

        public string BirthDate { get; set; }

        public CustomerShoeSize(int customerId, double shoeSize, string fullname, string birthDate)
        {
            CustomerId = customerId;
            ShoeSize = shoeSize;
            Fullname = fullname;
            BirthDate = birthDate;
        }

    public CustomerShoeSize(int? id, double size, string fullname, string dateString)
    {
        this.id = id;
        this.size = size;
        Fullname = fullname;
        this.dateString = dateString;
    }
}
    