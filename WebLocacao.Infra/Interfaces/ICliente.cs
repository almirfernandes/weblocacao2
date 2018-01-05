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

        void Incluir();

        void Detalhar();

        void Excluir();

        void Editar();
        
    }
}
