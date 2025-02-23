using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BL.Abstractions
{
    public interface IEmailService
    {
        void SendWelcome(string toUser);
        void SendConfirmEmail(string toUser, string confirmUrl);
        void VacancyMessage(string toUser);
        void SendAcceptedEmail(string toUser);
        void SendRejectedEmail(string toUser);
    }
}
