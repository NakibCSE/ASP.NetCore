using OODPrinciples.SRP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODPrinciples.DIP
{
    public class Membership
    {
        private IEmailSender _emailSender;
        private readonly EncryptionUtility _encryptionUtility;
        private readonly DataUtility _dataUtility;

        public Membership(IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _encryptionUtility = new EncryptionUtility();
            _dataUtility = new DataUtility();
        }

        public void CreateAccount(string username, string password, string email)
        {
            if (!_dataUtility.CheckDuplicateUsername(username))
            {
                password = _encryptionUtility.EncryptPassword(password);
                if (_dataUtility.SaveAccount(username, password, email))
                {
                    _emailSender.SendNewAccountEmail(email);
                }
            }
            else
                throw new InvalidOperationException();
        }
    }
}
