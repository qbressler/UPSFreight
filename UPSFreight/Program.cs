using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPSFreight.BusinessObjects.Classes;
using UPSFreight.UPSRateReference;

namespace UPSFreight
{
    class Program
    {
        static void Main(string[] args)
        {
            RateCalculator calculator = new RateCalculator();
            string[] addressLine1 = { "9074 WALL ST NW"};

            #region ship from
            FreightRateResponse shipFromResponse = calculator.CalculateShipFrom(addressLine1, 
                "Massillon", 
                "OH", 
                "44646", 
                "+1", 
                "Quintin Bressler", 
                "John Bressler");
            #endregion

            #region Payment
            // Calculate Payment
            AddressType payerAddress = new AddressType();
            string[] addressLine =  { "9074 Wall ST NW"};
            payerAddress.AddressLine = addressLine;
            payerAddress.City = "Massillon";
            payerAddress.CountryCode = "+1";
            payerAddress.PostalCode = "44646";
            payerAddress.StateProvinceCode = "OH";
            payerAddress.Town = "Massillon";

            PayerType payer = new PayerType();
            payer.AttentionName = "John Bressler";
            payer.Name = "John Bressler";
            payer.ShipperNumber = "99";
            FreightRateResponse paymentResponse = calculator.CalculatePaymentInfo(payerAddress, payer, "9", "Some Description");
            #endregion

        }
    }
}
