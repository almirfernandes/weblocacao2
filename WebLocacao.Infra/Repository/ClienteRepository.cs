using System.Collections.Generic;
using WebLocacao.Infra.Context;
using WebLocacao.Infra.Interfaces;
using WebLocacao.Dominio.Entities;
using System.Linq;

namespace WebLocacao.Infra.Repository
{
    public class ClienteRepository: ICliente
    {
        private readonly LocacaoContext db;

        

        public ClienteRepository()
        {
            db = new LocacaoContext();
        }

        public void Detalhar()
        {
            throw new System.NotImplementedException();
        }

        public void Editar()
        {
            throw new System.NotImplementedException();
        }

        public void Excluir()
        {
            throw new System.NotImplementedException();
        }

        public void Incluir()
        {
            throw new System.NotImplementedException();
        }

   

        public IEnumerable<Cliente> ObterTodos()
        {
            return db.Cliente.ToList();
        }
    }
}
