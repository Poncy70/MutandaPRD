using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Linq;

namespace Mutanda
{
    public partial class CustomSocialImage : ContentView
    {

        public event EventHandler Clicked;

        public static BindableProperty ImageProperty =
            BindableProperty.Create<CustomSocialImage, string>(ctrl => ctrl.ImageSocial,
                defaultValue: string.Empty,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    var ctrl = (CustomSocialImage)bindable;
                    ctrl.ImageSocial = newValue;
                });

        public string ImageSocial
        {
            get { return (string)GetValue(ImageProperty); }
            set
            {
                SetValue(ImageProperty, value);
                Image.Source = value;
            }
        }


        public CustomSocialImage()
        {
            InitializeComponent();

            TapGestureRecognizer tgr = new TapGestureRecognizer();

            tgr.Tapped += (s, e) =>
            {
                if (this.Clicked != null) this.Clicked(s, e);
            };

            this.Image.GestureRecognizers.Add(tgr);
        }
    }
}
