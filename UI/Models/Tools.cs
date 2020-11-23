using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UI.Models
{
    public class Tools
    {
        public static string toSlug(string data)
        {
            string result = data.ToLowerInvariant();

            result = result
                .Replace('ş', 's')
                .Replace('ç', 'c')
                .Replace('ı', 'i')
                .Replace('ğ', 'g')
                .Replace('ö', 'o')
                .Replace('ü', 'u')
                .Replace("\'", "")
                .Replace("\"", "");

            result = Regex.Replace(result, @"\s", "-", RegexOptions.Compiled);
            result = Regex.Replace(result, @"[^\w\s\p{Pd}]", "", RegexOptions.Compiled);
            result = result.Trim('-', '_');
            result = Regex.Replace(result, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return result;
        }

        public static double toDouble(string data)
        {
            return Convert.ToDouble(data.Replace('.', ','));
        }

        public static string toString(double data)
        {
            return data.ToString().Replace(',', '.');
        }

        public static string generateActivationCode()
        {
            string aCode = Guid.NewGuid().ToString().Trim().Replace(" ", "").Replace("-", "");
            return aCode;
        }

        public static bool sendMail(string to, string subject, string body)
        {
            bool status = false;

            MailMessage eMailObject = new MailMessage();

            eMailObject.From = new MailAddress("ilkeryolundagerek@gmail.com");
            eMailObject.To.Add(new MailAddress(to));
            eMailObject.Subject = subject;
            eMailObject.Body = body;
            eMailObject.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("ilkeryolundagerek@gmail.com", "Ders2020!!");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(eMailObject);

            return status;
        }
    }
}
