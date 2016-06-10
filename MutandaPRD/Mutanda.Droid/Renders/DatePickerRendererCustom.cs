//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using System;
//using System.ComponentModel;
//using Android.App;
//using Android.Content.Res;
//using Android.Widget;
//using AView = Android.Views.View;
//using Xamarin.Forms.Platform.Android;

////[assembly: ExportRenderer(typeof(DataPickerCustom), typeof(DatePickerRendererCustom))]
//namespace Mutanda.Droid.Renders
//{
//    public class DatePickerRenderer : ViewRenderer<DatePicker, EditText>
//    {
//        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
//        {
//            base.OnElementChanged(e);

//            if (e.OldElement == null)
//            {
//                var textField = new EditText(Context) { Focusable = false, Clickable = true, Tag = this };

//                textField.SetOnClickListener(TextFieldClickHandler.Instance);
//                SetNativeControl(textField);
//                _textColorSwitcher = new TextColorSwitcher(textField.TextColors);
//            }

//            SetDate(Element.Date);

//            UpdateMinimumDate();
//            UpdateMaximumDate();
//            UpdateTextColor();
//        }
//    }
//}