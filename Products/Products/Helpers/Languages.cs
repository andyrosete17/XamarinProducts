
namespace Products.Helpers
{
    using Products.Interface;
    using Products.Resources;
    using Xamarin.Forms;
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string NoEmail
        {
            get { return Resource.NoEmail; }
        }

        public static string Title
        {
            get { return Resource.Title; }
        }

        public static string Email_Label
        {
            get { return Resource.Email_Label; }
        }

        public static string Email_placeHolder
        {
            get { return Resource.Email_placeHolder; }
        }

        public static string Password_Label
        {
            get { return Resource.Password_Label; }
        }

        public static string Password_placeHolder
        {
            get { return Resource.Password_placeHolder; }
        }

        public static string Remember_me
        {
            get { return Resource.Remember_me; }
        }

        public static string Forgot_password
        {
            get { return Resource.Remember_me; }
        }

        public static string Register_New_User
        {
            get { return Resource.Remember_me; }
        }

        public static string Login_Facebook
        {
            get { return Resource.Login_Facebook; }
        }

        public static string NoPassword
        {
            get { return Resource.NoPassword; }
        }

        public static string InternetError
        {
            get { return Resource.InternetError; }
        }

        public static string InternetSettingsError
        {
            get { return Resource.InternetSettingsError; }
        }

    }
}
