using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Project.BL.Abstractions
{
    public class EmailService : IEmailService
    {
        IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void SendAcceptedEmail(string toUser)
        {
            SmtpClient smtp = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration["Email:Login"], _configuration["Email:Passcode"]);

            MailAddress from = new MailAddress("raultagizad9@gmail.com");
            MailAddress to = new MailAddress(toUser);

            MailMessage message = new MailMessage(from, to);


            message.Subject = "Təbrik edirik! İşə qəbul olundunuz 🎉";
            message.IsBodyHtml = true;

            message.Body = "Hörmətli Namizəd,Sizinlə müsahibəmiz çox xoş oldu və peşəkar bacarıqlarınızı yaxından tanımaq bizim üçün dəyərli idi. Sevinclə bildirmək istəyirik ki, bizim sirketde  işə qəbul olunmusunuz!İlk iş gününüzdə sizi qarşılayacaq və bütün lazımi məlumatları paylaşacaq komandamız hazır olacaq.Sizi komandamızda görmək üçün səbirsizliklə gözləyirik! Uğurlu və məhsuldar əməkdaşlıq arzusuyla.";
            smtp.Send(message);
        }

        public void SendConfirmEmail(string toUser, string confirmUrl)
        {

            SmtpClient smtp = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration["Email:Login"], _configuration["Email:Passcode"]);

            MailAddress from = new MailAddress("raultagizad9@gmail.com");
            MailAddress to = new MailAddress(toUser);

            MailMessage message = new MailMessage(from, to);


            message.Subject = "Confirm Email";
            message.Body = $"<a href={confirmUrl}>Click here to confirm your account</a>";
            message.IsBodyHtml = true;
            smtp.Send(message);

        }

        public void SendRejectedEmail(string toUser)
        {
            SmtpClient smtp = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration["Email:Login"], _configuration["Email:Passcode"]);

            MailAddress from = new MailAddress("raultagizad9@gmail.com");
            MailAddress to = new MailAddress(toUser);

            MailMessage message = new MailMessage(from, to);


            message.Subject = "İşə qəbul nəticəsi haqqında məlumat";
            message.IsBodyHtml = true;

            message.Body = "Hörmətli Namized. Əvvəlcə Restoran komandasına göstərdiyiniz maraq və bizimlə keçirdiyiniz müsahibə üçün sizə təşəkkür edirik.Sizin bilik və bacarıqlarınızı yaxından tanımaq bizim üçün xoş oldu. Lakin, təəssüf ki, bu mərhələdə sizin namizədliyinizə davam edə bilməyəcəyimizi bildirmək istəyirik. Bu qərar çoxsaylı uyğun namizədlər arasından seçim etmək məcburiyyətində olduğumuz üçün verilmişdir.Sizin gələcək karyeranızda uğurlar arzulayırıq və uyğun imkanlar yarandıqda sizinlə yenidən əməkdaşlıq etməkdən məmnun olarıq.Əgər əlavə sualınız olarsa, bizimlə əlaqə saxlaya bilərsiniz.";
            smtp.Send(message);
        }

        public void SendWelcome(string toUser)
        {
            SmtpClient smtp = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration["Email:Login"], _configuration["Email:Passcode"]);

            MailAddress from = new MailAddress("raultagizad9@gmail.com");
            MailAddress to = new MailAddress(toUser);

            MailMessage message = new MailMessage(from, to);


            message.Subject = "You have come to our restaurant";
            message.IsBodyHtml = true;

            message.Body = "Hörmətli Musterimiz,Restoranımıza qeydiyyatdan keçdiyiniz üçün təşəkkür edirik! 🎉  Bizimlə unudulmaz dad təcrübələri yaşamağa hazırsınız. İndi hesabınıza daxil olub restoranımızda otaq rezervasiya edə və menyumuzdan sifariş verə bilərsiniz.Daha çox məlumat üçün veb saytımızı ziyarət edin:  Sizə gözəl günlər arzulayırıq! 😊  ";
            smtp.Send(message);
        }
        public void VacancyMessage(string toUser)
        {
            SmtpClient smtp = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration["Email:Login"], _configuration["Email:Passcode"]);

            MailAddress from = new MailAddress("raultagizad9@gmail.com");
            MailAddress to = new MailAddress(toUser);

            MailMessage message = new MailMessage(from, to);


            message.Subject = "Başvurunuz için Teşekkürler";
            message.IsBodyHtml = true;

            message.Body = "Restoranımıza göstermiş olduğunuz ilgi ve başvurunuz için teşekkür ederiz. Göndermiş olduğunuz CV'nizi aldık ve başvurunuzun detaylarını incelemeye başladık.Ekip olarak, başvurunuzun uygunluğunu değerlendirecek ve kısa süre içinde sizinle iletişime geçeceğiz. Başvurunuzun sonucu hakkında bilgilendirme yapılacaktır.Şu an için başka bir sorunuz veya eklemek istediğiniz bir şey varsa, lütfen bizimle iletişime geçmekten çekinmeyin.İlginiz için tekrar teşekkür eder, iyi günler dileriz. ";
            smtp.Send(message);
        }
    }
}
