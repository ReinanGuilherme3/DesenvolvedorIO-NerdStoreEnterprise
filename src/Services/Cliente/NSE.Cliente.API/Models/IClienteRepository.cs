using NSE.Core.Data;

namespace NSE.Cliente.API.Models;

public interface IClienteRepository : IRepository<Cliente>
{
    Task Adicionar(Cliente cliente);
    Task<IEnumerable<Cliente>> ObterTodos();
    Task<Cliente?> ObterPorCpf(string cpf);
}
