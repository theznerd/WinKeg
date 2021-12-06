using Microsoft.UI.Xaml.Controls;
using WinKeg.UI.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WinKeg.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPageView : Page
    {
        public MenuPageView()
        {
            this.InitializeComponent();

            var vm = new MenuPageViewModel(new NavService());
            DataContext = vm;
        }
    }
}
