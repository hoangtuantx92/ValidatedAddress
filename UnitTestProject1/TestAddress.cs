using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValidatedAddressLib;
using System.Xml;
using Moq;


namespace UnitTestProject1
{
    [TestClass]
    public class TestAddress
    {
        Address _address;

        [TestInitialize]
        public void TestInit()
        {
            _address = new Address();
        }

        [TestMethod]
        public void CanaryTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GetAddressInfoFromXMLFileNoAptNumber()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("../../address1.xml");

            _address.GetAddressInfo(doc);

            var expectedResult = new Tuple<string, string, string, string, string>
                                                ("6406 IVY LN", "GREENBELT", "MD", "20770", "1441");
            var result = new Tuple<string, string, string, string, string>
                                                (_address._street, _address._city, _address._state, _address._zip5, _address._zip4);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetAddressNoAptNumberValidatedFromMockService()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../address1.xml");
            Mock<IValidatedAddressService> mockValidatedAddressService = new Mock<IValidatedAddressService>();
            mockValidatedAddressService.Setup(x => x.GetValidatedAddress("street", "apt", "city", "state", "zip5")).Returns(doc);

            _address.SetValidatedAddressService(mockValidatedAddressService.Object);
            _address.GetInfoFromService("street", "apt", "city", "state", "zip5");

            var expectedResult = new Tuple<string, string, string, string, string>
                                                ("6406 IVY LN", "GREENBELT", "MD", "20770", "1441");
            var result = new Tuple<string, string, string, string, string>
                                                (_address._street, _address._city, _address._state, _address._zip5, _address._zip4);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetAddressWithAptNumberValidatedFromMockService()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../address2.xml");
            Mock<IValidatedAddressService> mockValidatedAddressService = new Mock<IValidatedAddressService>();
            mockValidatedAddressService.Setup(x => x.GetValidatedAddress("street", "apt", "city", "state", "zip5")).Returns(doc);

            _address.SetValidatedAddressService(mockValidatedAddressService.Object);
            _address.GetInfoFromService("street", "apt", "city", "state", "zip5");

            var expectedResult = new Tuple<string, string, string, string, string, string>
                                                ("8499 Wilcrest Dr", "Apt 2000", "Houston", "TX", "77072", "1441");
            var result = new Tuple<string, string, string, string, string, string>
                                                (_address._street, _address._suite, _address._city, _address._state, _address._zip5, _address._zip4);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetErrorFromValidatedAddressService()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../error.xml");
            Mock<IValidatedAddressService> mockValidatedAddressService = new Mock<IValidatedAddressService>();
            mockValidatedAddressService.Setup(x => x.GetValidatedAddress("street", "apt", "city", "state", "zip5")).Returns(doc);

            _address.SetValidatedAddressService(mockValidatedAddressService.Object);
            _address.GetInfoFromService("street", "apt", "city", "state", "zip5");


            Assert.AreEqual("The entried address is not correct. Please go back and input correct address", _address._error);
        }

        [TestMethod]
        public void GetNetWorkErrorWhileValidateAddress()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("../../address2.xml");
            Mock<IValidatedAddressService> mockValidatedAddressService = new Mock<IValidatedAddressService>();
            mockValidatedAddressService.Setup(x => x.GetValidatedAddress("street", "apt", "city", "state", "zip5")).Throws(new Exception());

            _address.SetValidatedAddressService(mockValidatedAddressService.Object);
            _address.GetInfoFromService("street", "apt", "city", "state", "zip5");


            Assert.AreEqual("There is no internet connection. Please get internet connection.", _address._error);
        }

        [TestMethod]
        public void GetAddressNoAptNumberWithRealService()
        {
            USPSValidatedAddress _uspsService = new USPSValidatedAddress();

            _address.SetValidatedAddressService(_uspsService);
            _address.GetInfoFromService("6406 IVY Lane","", "GREENBELT", "MD", "20770");

            var expectedResult = new Tuple<string, string, string, string, string>
                                                ("6406 IVY LN", "GREENBELT", "MD", "20770", "1441");
            var result = new Tuple<string, string, string, string, string>
                                                (_address._street, _address._city, _address._state, _address._zip5, _address._zip4);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetAddressWithAptNumberWithRealService()
        {
            USPSValidatedAddress _uspsService = new USPSValidatedAddress();

            _address.SetValidatedAddressService(_uspsService);
            _address.GetInfoFromService("8405 WILCREST dr", "APT 1994", "HOUSTON", "TX", "77072");

            var expectedResult = new Tuple<string, string, string, string, string, string>
                                                ("8405 WILCREST DR", "APT 1994", "HOUSTON", "TX", "77072", "4318");
            var result = new Tuple<string, string, string, string, string, string>
                                                (_address._street, _address._suite, _address._city, _address._state, _address._zip5, _address._zip4);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
