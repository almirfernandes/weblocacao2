using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLocacao.Dominio.Entities;

namespace WebLocacao.Infra.Interfaces
{
    public interface ICliente
    {
        IEnumerable<Cliente> ObterTodos();

        void Incluir(Cliente cliente);

        Cliente Detalhar(int id);

        void Excluir(Cliente cliente);

        void Editar(Cliente cliente);
        
    }
}
