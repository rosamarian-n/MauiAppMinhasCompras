using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Properties.Helders
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;
        //SQLiteAsyncConnection serve para manter a lista de operação aberta de leitura e escrita, mantendo assincrona dentro do hd
        public SQLiteDatabaseHelper(string patch) 
        { 
            _conn = new SQLiteAsyncConnection(patch);
            _conn.CreateTableAsync<Produto>().Wait();
        }
        //Construtor sempre é chamado quando o objeto é instanciado, ou seja, quando a classe é chamada (patch=caminho)
        //Explica-se o caminho do banco de dados, e cria a conexão com o banco de dados
        //CreateTableAsync<Produto>() cria a tabela Produto no banco de dados, caso não exista, e Wait() espera a conclusão da operação antes de prosseguir

        //declaração de todos os metodos que precisar

        public Task<int> Insert(Produto p) 
        {
            return _conn.InsertAsync(p);
            //Retorno numero de registros inseridos no banco de dados, caso seja 1, significa que a inserção foi bem sucedida
        }

        public Task<List<Produto>> Update(Produto p) 
        {
            string sql = "UPDATE Produto SET Descricao= ?, Quantidade= ?, Preco= ? WHERE Id= ?";
            return _conn.QueryAsync<p>(
                sql,p.Descricao, p.Quantidade, p.Preco, p.Id
                );                
        }

        public void Delete(int id) { }

        public void GetAll() { }
        //lista de todos os produtos

        public void Search(string q) { }
    }
}
