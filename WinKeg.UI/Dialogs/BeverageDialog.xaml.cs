﻿using Microsoft.UI.Xaml;
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
    public sealed partial class BeverageDialog : ContentDialog
    {
        private BeverageDialogModel viewModel;

        public BeverageDialog()
        {
            this.InitializeComponent();

            var vm = new BeverageDialogModel();
            this.DataContext = vm;
            viewModel = vm;
        }

        public Beverage selectedBeverage { get; private set; }

        private void Primary_Click(object sender, RoutedEventArgs e)
        {
            selectedBeverage = viewModel.SelectedBeverage;
            this.Hide();
        }

        private void Secondary_Click(object sender, RoutedEventArgs e)
        {
            selectedBeverage = null;
            this.Hide();
        }
    }
}
