using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OODPrinciples.SRP
{
    public class Membership
    { 
        private readonly EmailSender _emailSender;
        private readonly EncryptionUtility _encryptionUtility;
        private readonly DataUtlity _dataUtlity;

        public Membership()
        {
            _emailSender = new EmailSender();
            _encryptionUtility = new EncryptionUtility();
            _dataUtlity = new DataUtlity();
        }
        public void CreateAccount(string username,  string password, string email)
        {
            if (!_dataUtlity.CheckDuplicateUserName(username))
            {
                password = _encryptionUtility.EncryptPassword(password);
                if (_dataUtlity.SaveAccont(username, password, email))
                {
                  _emailSender.SendNewAccountEmail(email);
                }
            }
            else
                throw new InvalidOperationException();
        }
    }
}
