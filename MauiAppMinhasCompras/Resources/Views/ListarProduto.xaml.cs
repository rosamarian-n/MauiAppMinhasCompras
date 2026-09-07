using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Resources.Views;


public partial class ListarProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
	public ListarProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
    }

	protected async override void OnAppearing()
	{
		List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());

		}
		catch (Exception ex) 
		{
			DisplayAlert("Ops", ex.Message, "OK"); 
		}

    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		string q = e.NewTextValue;

		lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		string msg= $"O valor total dos produtos é {soma:C}";

		DisplayAlert("Total dos Produtos", msg, "OK");

    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // 1?? PEGA O PRODUTO SELECIONADO
            // sender = MenuItem clicado
            // BindingContext = objeto Produto associado ao item
            var produto = (Produto)((MenuItem)sender).BindingContext;
            
            // Mostra: "Remover 'teste'?" com botões "Sim" e "Não"
            // Se clicar em "Sim" (true), executa a remoção
            if (await DisplayAlert("Confirmar", $"Remover '{produto.Descricao}'?", "Sim", "Não"))

            {
                // remove o banco de dados
                await App.Db.Delete(produto.Id);
                
                // ObservableCollection atualiza o ListView automaticamente
                lista.Remove(produto);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
        

    }
}