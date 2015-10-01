﻿using System;
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
            string[] addressLine =  { "123 Main ST NE"};
            payerAddress.AddressLine = addressLine;
            payerAddress.City = "City";
            payerAddress.CountryCode = "+1";
            payerAddress.PostalCode = "11111";
            payerAddress.StateProvinceCode = "FL";
            payerAddress.Town = "Town";

            PayerType payer = new PayerType();
            payer.AttentionName = "ATTN Namer";
            payer.Name = "Payer Name";
            payer.ShipperNumber = "99";
            FreightRateResponse paymentResponse = calculator.CalculatePaymentInfo(payerAddress, payer, "9", "Some Description");
            #endregion

        }
    }
}
