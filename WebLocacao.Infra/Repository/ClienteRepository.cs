using System.Collections.Generic;
using WebLocacao.Infra.Context;
using WebLocacao.Infra.Interfaces;
using WebLocacao.Dominio.Entities;
using System.Linq;
using System.Data.Entity;

namespace WebLocacao.Infra.Repository
{
    public class ClienteRepository: ICliente
    {
        private readonly LocacaoContext db;

        

        public ClienteRepository()
        {
            db = new LocacaoContext();
        }

        public Cliente Detalhar(int id)
        {
            return db.Cliente.Find(id);
        }

        public void Editar(Cliente cliente)
        {
            db.Entry(cliente).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Excluir(Cliente cliente)
        {
            db.Cliente.Remove(cliente);
            db.SaveChanges();
        }

        public void Incluir(Cliente cliente)
        {
            db.Cliente.Add(cliente);
            db.SaveChanges();
        }

   

        public IEnumerable<Cliente> ObterTodos()
        {
            return db.Cliente.ToList();
        }
    }
}
