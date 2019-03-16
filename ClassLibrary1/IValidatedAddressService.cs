using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ValidatedAddressLib
{
    public interface IValidatedAddressService
    {
        XmlDocument GetValidatedAddress(string street, string apt, string city, string state, string zip5);

        string GetAddressURL(string street, string apt, string city, string state, string zip5);
        
    }
}
