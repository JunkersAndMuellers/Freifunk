using System;
using Xamarin.Forms;

namespace FFStraelen
{
    public class ActivityPage : ContentPage
    {
        public ActivityPage()
        {
            var indicator = new ActivityIndicator()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            indicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy", BindingMode.OneWay);
            indicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy", BindingMode.OneWay);

            var button = new Button()
            {
                HorizontalOptions = LayoutOptions.Fill,
                Text = "BUTTON"
            };

            button.Clicked += (sender, e) => IsBusy = !IsBusy;

            var root = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    button, 
                    indicator
                }
            };

            Content = root;
        }
    }
}

