using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Windows.Input;
using WinKeg.Data.Models;
using WinKeg.UI.ViewModels.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace WinKeg.UI.Views.Controls
{
    public sealed partial class KegView : UserControl
    {
        public KegView()
        {
            this.InitializeComponent();
        }



        public ICommand PourCommand
        {
            get { return (ICommand)GetValue(PourCommandProperty); }
            set { SetValue(PourCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PourCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PourCommandProperty =
            DependencyProperty.Register("PourCommand", typeof(ICommand), typeof(KegView), new PropertyMetadata(null));


        private bool descriptionVisible = false;
        private async void BevDescription_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            descriptionVisible = !descriptionVisible;
            if(descriptionVisible)
            {
                RevealDescription.Begin();
            }
            else
            {
                HideDescription.Begin();
            }
        }
    }
}
