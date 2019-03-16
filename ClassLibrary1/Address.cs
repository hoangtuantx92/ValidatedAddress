using System;
using ValidatedAddressLib;
using System.Xml;

namespace ValidatedAddressLib
{
    public class Address
    {

        //Adress property
        public string _street { get; set; }
        public string _suite { get; set; }
        public string _city { get; set; }
        public string _state { get; set; }
        public string _zip5 { get; set; }
        public string _zip4 { get; set; }
        public IValidatedAddressService _validatedAddressService { get; set; }
        public string _error { get; set; }

        //Address Contruction
        public Address()
        {
            _street = "";
            _suite = "";
            _city = "";
            _state = "";
            _zip5 = "";
            _zip4 = "";
            _error = "";
        }

        //assign value from a xml document to Address property
        public void GetAddressInfo(XmlDocument doc)
        {

            //if no error get address property
            if (doc.GetElementsByTagName("Error")[0] == null)
            {
                if (doc.GetElementsByTagName("Address2")[0] != null)
                    _street = doc.GetElementsByTagName("Address2")[0].InnerText;
                if (doc.GetElementsByTagName("Address1")[0] != null)
                    _suite = doc.GetElementsByTagName("Address1")[0].InnerText;
                if (doc.GetElementsByTagName("City")[0] != null)
                    _city = doc.GetElementsByTagName("City")[0].InnerText;
                if (doc.GetElementsByTagName("State")[0] != null)
                    _state = doc.GetElementsByTagName("State")[0].InnerText;
                if (doc.GetElementsByTagName("Zip5")[0] != null)
                    _zip5 = doc.GetElementsByTagName("Zip5")[0].InnerText;
                if (doc.GetElementsByTagName("Zip4")[0] != null)
                    _zip4 = doc.GetElementsByTagName("Zip4")[0].InnerText;
            }
            else
            {
                _error = "The entried address is not correct. Please go back and input correct address";
            }
        }

        //set Address Validated Service 
        public void SetValidatedAddressService(IValidatedAddressService service)
        {
            _validatedAddressService = service;
        }

        //receive address from UI to Get XML file from Service API, then assign value to address from xml
        public void GetInfoFromService(string street, string apt, string city, string state, string zip5)
        {
            try
            {
                XmlDocument xmlDocument = _validatedAddressService.GetValidatedAddress(street, apt, city, state, zip5);
                GetAddressInfo(xmlDocument);
            }
            catch
            {
                _error = "There is no internet connection. Please get internet connection.";
            }
        }
    }
}
