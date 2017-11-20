using System.Threading.Tasks;
using Xamarin.Forms;

namespace Products.Services
{
    class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "Accept");
        }
    }
}
