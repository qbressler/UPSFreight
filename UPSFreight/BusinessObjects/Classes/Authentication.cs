using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPSFreight.UPSRateReference;

namespace UPSFreight.BusinessObjects.Classes
{
    public class Authentication
    {
        #region constructor
        public Authentication(string uname, string pword, string lnumber)
        {
            this.Username = uname;
            this.Password = pword;
            this.LicenseNumber = lnumber;
        }
        #endregion

        #region methods
        public UPSSecurity Authenticate()
        {
            UPSSecurity upss = new UPSSecurity();
            UPSSecurityServiceAccessToken upsSvcToken = new UPSSecurityServiceAccessToken();
            upsSvcToken.AccessLicenseNumber = this.LicenseNumber;
            upss.ServiceAccessToken = upsSvcToken;
            upss.UsernameToken = SecureToken(this.Username, this.Password);
            return upss;
        }

        private UPSSecurityUsernameToken SecureToken(string username, string password)
        {
            UPSSecurityUsernameToken upsSecUsrnameToken = new UPSSecurityUsernameToken();
            upsSecUsrnameToken.Username = "ahrepair";
            upsSecUsrnameToken.Password = "Technician13";
            return upsSecUsrnameToken;
        }
        #endregion

        #region properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string LicenseNumber { get; set; }
        #endregion
    }
}
