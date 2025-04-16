using System.Net;
using System.Net.Mail;
using System.Text;

namespace website.pl.helpers
{
    public static class emailsetting
    {
        public static bool sendemail(email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("aliabrahem955@gmail.com", "xtgqizbqysevadow");
                client.Send("aliabrahem955@gmail.com", email.to, email.subject, email.body);
                    


                return true; 
            }
            catch (Exception e)
            {
                
                return false; 
            }
        }
        public static string GenerateOTP(int length = 6)
        {
            var rng = new Random();
            var otp = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                otp.Append(rng.Next(0, 10)); 
            }

            return otp.ToString();
        }


    }
}
