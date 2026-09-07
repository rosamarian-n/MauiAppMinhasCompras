using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Resources.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto
			{
				Descricao = txt_descricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text)
			};

			await App.Db.Insert(p);

			await DisplayAlert("Sucesso", "Registro inserido com sucesso!", "OK");
            //Todo metodo que tem await assincrono, ou seja, ele não bloqueia a execução do código
			await Navigation.PopAsync();

        }
        catch (Exception ex)
		{
			await DisplayAlert("Ops!", ex.Message, "OK");
        }
    }
}