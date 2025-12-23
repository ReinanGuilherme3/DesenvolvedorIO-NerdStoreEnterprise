using Microsoft.EntityFrameworkCore;
using NSE.Cliente.API.Data;
using NSE.Core.Data;

namespace NSE.Cliente.API.Models;

public class ClienteRepository : IClienteRepository
{
    private readonly ClientesContext _context;

    public ClienteRepository(ClientesContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Adicionar(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
    }

    public async Task<Cliente?> ObterPorCpf(string cpf)
    {
        return await _context.Clientes.FirstOrDefaultAsync(c => c.Cpf.Numero == cpf);
    }

    public async Task<IEnumerable<Cliente>> ObterTodos()
    {
        return await _context.Clientes.AsNoTracking().ToListAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
