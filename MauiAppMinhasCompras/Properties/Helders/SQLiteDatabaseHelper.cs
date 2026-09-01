using MauiAppMinhasCompras.Models;
using SQLite;

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
            return _conn.QueryAsync<Produto>(
                sql,p.Descricao, p.Quantidade, p.Preco, p.Id
                );                
        }

        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
            //Table<Produto> retorna a tabela Produto do banco de dados
            //(i => i.Id == id) é uma expressão lambda que representa a condição de exclusão, ou seja, excluir o registro cujo Id seja igual ao id passado como parâmetro
            //Chama a tabela, cria a expressão delete e inclue todos os items da tabela com lambda
        }

        public Task<List<Produto>> GetAll() 
        {
           return _conn.Table<Produto>().ToListAsync();
        }
        //lista de todos os produtos

        public Task<List<Produto>> Search(string q) 
        { 
            string sql = "SELECT * Produto WHERE descricao LIKE '%" + q + "%'"; 

            return _conn.QueryAsync<Produto>(sql);
            //Pesquisa no banco de dados todos os produtos que contenham a string q na descrição, usando o operador LIKE do SQL
        }
    }
}
