using System;
using BigTed;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using Foundation;
using Mutanda.Services;
using Mutanda.iOS.Dependency;

[assembly: Xamarin.Forms.Dependency(typeof(IosHudService))]
namespace Mutanda.iOS.Dependency
{
    public class IosHudService : IHudService
    {

        UIView _load;

        bool isHudVisible;

        #region IHudManager implementation

        public void ShowHud(string ProgressText = "Loading...")
        {
            isHudVisible = true;
            SetText(ProgressText);
        }

        public void HideHud()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                BTProgressHUD.Dismiss();
                if (_load != null)
                    _load.Hidden = true;
                isHudVisible = false;
            });
        }


        /// <summary>
        /// Method to change Progress Text and show custom loader with Challenge Logo
        /// </summary>
        /// <param name="ProgressText">Progress text.</param>

        public void SetProgress(double Progress, string ProgressText = "")
        {
            int progress = (int)(Progress * 100);
            string text = ProgressText + progress + "%";
            SetText(text);
        }


        public void SetText(string text)
        {
            if (!isHudVisible)
                return;
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                BTProgressHUD.Show(status: text, maskType: ProgressHUD.MaskType.Black);

                try
                {
                    //if (_load == null) {
                    //If you want to use a custom view change this!
                    //	_load = CustomLoadingView (text);
                    //	ProgressHUD.Shared.AddSubview (_load);
                    //}
                    lblTitle.Text = text;

                    UIView[] subView = ProgressHUD.Shared.Subviews;
                    for (int i = 0; i < subView.Length; i++)
                    {
                        subView[i].Hidden = true;
                    }
                    _load.Hidden = false;
                    ProgressHUD.Shared.BringSubviewToFront(_load);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("IosHudService.cs - SetText() " + ex.Message);
                }
            });
        }

        UILabel lblTitle;
        /// <summary>
        /// Customs the loading view.
        /// </summary>
        /// <returns>The loading view.</returns>
        /// <param name="ProgressText">Progress text.</param>
        UIView CustomLoadingView(string ProgressText)
        {
            UIView loadingView = new UIView();
            loadingView.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

            UIImageView imgBg = new UIImageView();
            imgBg.Image = UIImage.FromFile("load_bg.png");
            imgBg.Frame = new CGRect((loadingView.Frame.Width / 2) - 65, (loadingView.Frame.Height / 2) - 70, 130, 140);
            loadingView.Add(imgBg);

            UIImageView someImageView = new UIImageView();
            someImageView.Frame = new CGRect((loadingView.Frame.Width / 2) - 40, (loadingView.Frame.Height / 2) - 50, 75, 75);
            someImageView.AnimationImages = new UIImage[]
            {
                UIImage.FromBundle("spinner.png"),
            };
            someImageView.AnimationRepeatCount = nint.MaxValue; // Repeat forever.
            someImageView.AnimationDuration = 1.0; // Every 1s.
            someImageView.StartAnimating();


            CABasicAnimation rotationAnimation = new CABasicAnimation();
            rotationAnimation.KeyPath = "transform.rotation.z";
            rotationAnimation.To = new NSNumber(Math.PI * 2);
            rotationAnimation.Duration = 1;
            rotationAnimation.Cumulative = true;
            rotationAnimation.RepeatCount = float.PositiveInfinity;
            someImageView.Layer.AddAnimation(rotationAnimation, "rotationAnimation");
            loadingView.Add(someImageView);


            lblTitle = new UILabel();
            lblTitle.Text = ProgressText;
            lblTitle.Frame = new CGRect(imgBg.Frame.X, someImageView.Frame.Y + someImageView.Frame.Height + 15, 130, 20);
            lblTitle.TextAlignment = UITextAlignment.Center;
            lblTitle.TextColor = UIColor.White;
            lblTitle.AdjustsFontSizeToFitWidth = true;

            //TODO // still have to change font style
            //			lblTitle.Font = UIFont.FromName("HelveticaNeue-Bold",18f);
            loadingView.Add(lblTitle);
            return loadingView;
        }

        #endregion
    }

}