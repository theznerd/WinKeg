﻿using Microsoft.UI.Xaml.Controls;
using WinKeg.UI.ViewModels.Menus;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinKeg.UI.Views.Menus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserAdminView : Page
    {
        public UserAdminView()
        {
            this.InitializeComponent();

            var vm = new UserAdminViewModel(new NavService());
            this.DataContext = vm;
        }
    }
}
