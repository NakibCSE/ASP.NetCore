using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODPrinciples.DIP
{
    public interface IEmailSender
    {
        void SendNewAccountEmail(string email);
    }
}
