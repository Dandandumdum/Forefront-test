namespace ShoeService.Models;
    public class CustomerShoeSize
    {
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
    }
    