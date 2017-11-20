﻿using Xamarin.Forms;
[assembly: Dependency(typeof(Products.iOS.Implementations.Localize))] //Indicate who is implementing the localize interface method for iOS


namespace Products.iOS.Implementations
{
    using Foundation;
    using Products.Helpers;
    using Products.Interface;
    using System.Globalization;
    using System.Threading;

    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = iOSToDotnetLanguage(pref);
            }
            //This gets called a lot - try/catch can be expensive so consider caching or something
            CultureInfo ci = null;
            try
            {
                ci = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException ei)
            {
                ei.ToString();
                //iOS locale not valid .NET culture  (eg. "en-ES) : English in Spain
                //fallback to first characters, in this case "en"
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    ci = new CultureInfo(fallback);
                }
                catch (CultureNotFoundException e2)
                {
                    e2.ToString();
                    //iOSlanguage not valid .NET culture, falling back to english
                    ci = new CultureInfo("en");
                }
            }
            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        string iOSToDotnetLanguage(string iOSLanguage)
        {
            var netLanguage = iOSLanguage;
            //certain language need to be converted to CultureInfo equivalent
            switch (iOSLanguage)
            {
                case "ms-MY": //Malaysian Malaysia not supported .NET culture
                case "ms-SG"://Malaysian Singapur not supported .NET culture
                    netLanguage = "ms";
                    break;
                case "gsw-CH": //Swiss German not supported .NET culture
                    netLanguage = "de-CH";
                    break;
            }
            return netLanguage;
        }

        string ToDotnetFallbackLanguage(PlatformCulture platCulture)
        {
            var netLanguage = platCulture.LanguageCode; // use the first part of the identifier (two chars, usually)
            switch (platCulture.LanguageCode)
            {
                case "pt":
                    netLanguage = "pt-PT"; //fallback portuguese
                    break;
                case "gsw":
                    netLanguage = "de-CH"; // Equivalent to german 
                    break;
            }
            return netLanguage;
        }
    }
}