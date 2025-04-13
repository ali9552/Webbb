using System.Net;
using System.Net.Mail;

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
                    


                return true; // Return true if the email was sent successfully
            }
            catch (Exception e)
            {
                // Handle exceptions (e.g., log the error)
                return false; // Return false if there was an error
            }
        }


    }
}
