﻿using System;
using Xamarin.Forms;

namespace Products.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public string Image { get; set; }

        public double Stock { get; set; }

        public DateTime LastPurchase { get; set; }

        public string Remarks { get; set; }

        public string ImageFullPath
        {
            get
            {
                return string.Format(Application.Current.Resources["BACKURL"].ToString() + "{0}", 
                    Image.Substring(1));
            }
        }
    }
}
