using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPSFreight.UPSRateReference;

namespace UPSFreight.BusinessObjects.Classes
{
    public class RateCalculator
    {

        #region constructor
        public RateCalculator()
        {
            freightRateService = new FreightRateService();
            freightRateRequest = new FreightRateRequest();
            string userName = ConfigurationManager.AppSettings["UPSUserName"];
            string password = ConfigurationManager.AppSettings["UPSPassword"];
            string LicenseNumber = ConfigurationManager.AppSettings["UPSLicenseNumber"];
            auth = new Authentication(userName, password, LicenseNumber);
        }
        #endregion
        #region methods
        /// <summary>
        /// Calculate Ship From
        /// </summary>
        /// <param name="addressLine"></param>
        /// <param name="city"></param>
        /// <param name="stateProvinceCode"></param>
        /// <param name="postalCode"></param>
        /// <param name="countryCode"></param>
        /// <param name="attentionName"></param>
        /// <param name="shipFromName"></param>
        /// <returns></returns>
        public FreightRateResponse CalculateShipFrom(string[] addressLine, string city, string stateProvinceCode, string postalCode, string countryCode, string attentionName, string shipFromName)
        {
            ShipFromType shipFrom = new ShipFromType();
            AddressType shipFromAddress = new AddressType();
            String[] shipFromAddressLines = addressLine;
            shipFromAddress.AddressLine = shipFromAddressLines;
            shipFromAddress.City = city;
            shipFromAddress.StateProvinceCode = stateProvinceCode;
            shipFromAddress.PostalCode = postalCode;
            shipFromAddress.CountryCode = countryCode;
            shipFrom.Address = shipFromAddress;
            shipFrom.AttentionName = attentionName;
            shipFrom.Name = shipFromName;
            freightRateRequest.ShipFrom = shipFrom;
            freightRateService.UPSSecurityValue = auth.Authenticate();
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            freightRateRequest.Request = BuildRequestType();
            FreightRateResponse freightRateResponse = freightRateService.ProcessFreightRate(freightRateRequest);
            return freightRateResponse;
           
        }

        /// <summary>
        /// Calculate Payment Info
        /// </summary>
        /// <param name="payerAddress"></param>
        /// <param name="payer"></param>
        /// <param name="billOptionCode"></param>
        /// <param name="billOptionDesc"></param>
        public FreightRateResponse CalculatePaymentInfo(AddressType payerAddress, PayerType payer, string billOptionCode, string billOptionDesc)
        {
            PaymentInformationType paymentInfo = new PaymentInformationType();
          
            payer.Address = payerAddress;
            paymentInfo.Payer = payer;
            RateCodeDescriptionType shipBillOption = new RateCodeDescriptionType();
            shipBillOption.Code = billOptionCode;
            shipBillOption.Description = billOptionDesc;
            paymentInfo.ShipmentBillingOption = shipBillOption;
            freightRateRequest.PaymentInformation = paymentInfo;
            freightRateRequest.Request = BuildRequestType();
            FreightRateResponse freightRateResponse = freightRateService.ProcessFreightRate(freightRateRequest);
            return freightRateResponse;
        }
        /// <summary>
        /// Calculate Ship To
        /// </summary>
        /// <param name="addressLine"></param>
        /// <param name="city"></param>
        /// <param name="stateProvinceCode"></param>
        /// <param name="postalCode"></param>
        /// <param name="countryCode"></param>
        /// <param name="attentionName"></param>
        /// <param name="shipToName"></param>
        /// <returns></returns>
        public FreightRateResponse CalculateShipTo(string[] addressLine, string city, string stateProvinceCode, string postalCode, string countryCode, string attentionName, string shipToName)
        {
            ShipToType shipTo = new ShipToType();
            AddressType shipToAddress = new AddressType();
            String[] shipToAddressLines = addressLine;
            shipToAddress.AddressLine = shipToAddressLines;
            shipToAddress.City = city;
            shipToAddress.StateProvinceCode = stateProvinceCode;
            shipToAddress.PostalCode = postalCode;
            shipToAddress.CountryCode = countryCode;
            shipTo.Address = shipToAddress;
            shipTo.AttentionName = attentionName;
            shipTo.Name = shipToName;
            freightRateRequest.ShipTo = shipTo;
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            freightRateRequest.Request = BuildRequestType();
            FreightRateResponse freightRateResponse = freightRateService.ProcessFreightRate(freightRateRequest);
            return freightRateResponse;
        }

        /// <summary>
        /// Retrieve Rate Code Description
        /// </summary>
        /// <returns></returns>
        public FreightRateResponse Service(string code, string description)
        {
            RateCodeDescriptionType service = new RateCodeDescriptionType();
            service.Code =code;
            service.Description = description;
            freightRateRequest.Service = service;
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            freightRateRequest.Request = BuildRequestType();
            FreightRateResponse freightRateResponse = freightRateService.ProcessFreightRate(freightRateRequest);
            return freightRateResponse;
        }

        /// <summary>
        /// Build Request Type
        /// </summary>
        /// <returns></returns>
        private RequestType BuildRequestType()
        {
            RequestType request = new RequestType();
            String[] requestOption = { "RateChecking Option" };
            request.RequestOption = requestOption;
            return request;
        }

        #endregion

        #region properties
        FreightRateService freightRateService = null;
        FreightRateRequest freightRateRequest = null;
        Authentication auth = null;
        FreightRateResponse freightRateResponse = null;
        #endregion
    }
}
