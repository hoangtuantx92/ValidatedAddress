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
	public partial class DisplayValidatedAddress : ContentPage
	{
		public DisplayValidatedAddress (String address)
		{
            if (address == null)
                throw new ArgumentException();

            InitializeComponent ();

            message.Text = address;
		}

        async void Back_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
	}
}