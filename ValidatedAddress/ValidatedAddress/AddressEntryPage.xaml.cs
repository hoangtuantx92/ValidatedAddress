using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ValidatedAddressLib;

namespace ValidatedAddress
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddressEntryPage : ContentPage
	{
    
		public AddressEntryPage ()
		{

			InitializeComponent ();

        }

        async void Check_Address_Clicked(object sender, System.EventArgs e)
        {
            
            Address validatedAddress = GetValidatedAddress();

            string message = FormatAddress(validatedAddress);

            await Navigation.PushAsync(new DisplayValidatedAddress(message));
        }

        Address GetValidatedAddress()
        {
            Address address = new Address();
            USPSValidatedAddress validatedAddressService = new USPSValidatedAddress();

            address.SetValidatedAddressService(validatedAddressService);

            address.GetInfoFromService(street.Text, apartmentNumber.Text, city.Text, state.Text, zip5.Text);

            return address;
        }

        String FormatAddress(Address validatedAddress)
        {
            if(validatedAddress._error == "")
            {
                if (validatedAddress._suite == "")
                    return validatedAddress._street + ", " + validatedAddress._city + ", " 
                        + validatedAddress._state + " " + validatedAddress._zip5 + "-" + validatedAddress._zip4;

                return validatedAddress._street + " " + validatedAddress._suite + ", " + validatedAddress._city + ", "
                        + validatedAddress._state + " " + validatedAddress._zip5 + "-" + validatedAddress._zip4;

            }

            return validatedAddress._error;
        }
    }
}