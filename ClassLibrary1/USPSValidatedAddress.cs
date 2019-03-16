using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ValidatedAddressLib
{
    public class USPSValidatedAddress : IValidatedAddressService
    {
        //get address info to generate URL API
        public string GetAddressURL(string street, string aptNumber, string city, string state, string zip5)
        {
            string path = "http://production.shippingapis.com/ShippingAPITest.dll?API=Verify &XML=<AddressValidateRequest USERID='409198901214'><Address ID='0'><Address1>"
                                + aptNumber + "</Address1> <Address2>" + street + "</Address2><City>"
                                + city + "</City><State>" + state + "</State><Zip5>"
                                + zip5 + "</Zip5><Zip4></Zip4></Address></AddressValidateRequest>";

            return path;
        }

        //Get XML file from Service
        public XmlDocument GetValidatedAddress(string street, string aptNumber, string city, string state, string zip5)
        {
            string path = GetAddressURL(street, aptNumber, city, state, zip5);
            XmlDocument _xmlDocument = new XmlDocument();
            _xmlDocument.Load(path);

            return _xmlDocument;

        }

       
    }
}
