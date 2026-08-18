using MauiAppMinhasCompras.Resources.Views;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();

            MainPage = new NavigationPage(new ListarProduto());
        }

    }
}
