namespace ShoeService.Utilities;
using System.Net.Mail;

public static class Utilities
{
     public static double Convert(double x)
    {
        return x * 0.393701;
    }

    public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
}