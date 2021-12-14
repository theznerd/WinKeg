using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinKeg.Data.Models;
using WinKeg.UI.ViewModels.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinKeg.UI.Dialogs
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PourDialog : ContentDialog
    {
        private PourDialogModel _vm;
        private Keg _keg;
        public PourDialog(Keg k)
        {
            this.InitializeComponent();

            var vm = new PourDialogModel(k);
            _vm = vm;
            _keg = k;
            this.DataContext = vm;
        }

        private void Close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
