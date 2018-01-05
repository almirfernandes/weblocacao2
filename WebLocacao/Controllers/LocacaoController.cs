using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebLocacao.Models;
using PagedList;
using WebLocacao.Infra.Context;
using WebLocacao.Dominio.Entities;





namespace WebLocacao.Controllers
{
    public class LocacaoController : Controller
    {
        private LocacaoContext db = new LocacaoContext();

        // GET: Locacao

            public ActionResult Index(int? pagina, string busca)
        {

            int paginaTamanho = 4;
            int paginaNumero = (pagina ?? 1);

            var locacao = db.Locacao.Include(l => l.Clientes).Include(l => l.Filmes);

            if (!String.IsNullOrEmpty(busca) && !String.IsNullOrWhiteSpace(busca))
            {
                busca = busca.ToUpper();
                TempData["busca"] = busca;

                locacao = locacao.Where(l => l.Clientes.Nome.ToUpper().Contains(busca) ||
                                         l.Filmes.Filme.ToUpper().Contains(busca));
            }
 
            return View(locacao.ToList().ToPagedList(paginaNumero, paginaTamanho));
        }

        




        // GET: Locacao/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = await db.Locacao.FindAsync(id);
            if (locacao == null)
            {
                return HttpNotFound();
            }
            return View(locacao);
        }

        // GET: Locacao/Create
        public ActionResult Create()
        {
            ViewBag.Cliente = new SelectList(db.Cliente, "ID", "Nome");
            ViewBag.Filme = new SelectList(db.Filmes, "ID", "Filme");
            return View();
        }

        // POST: Locacao/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Cliente,Filme,dataLocacao,DataDevolucao")] Locacao locacao)
        {
            

            if (TempData["mensagem"] != null)

            {
                locacao.dataLocacao = DateTime.Today;
                db.Locacao.Add(locacao);
                await db.SaveChangesAsync();
                TempData.Remove("Mensagem");
                var locacaoCliente = db.Locacao.Include(l => l.Clientes).Include(l => l.Filmes).Where(f => f.Filme == locacao.Filme & f.Cliente == locacao.Cliente).OrderByDescending(f => f.dataLocacao);
                if (locacaoCliente.Count() > 0)
                {
                    foreach (var item in locacaoCliente)
                    {
                        if (item.DataDevolucao == null)
                        {
                            TempData["success"] = "Filme " + item.Filmes.Filme + " alugado pelo Cliente " + item.Clientes.Nome;
                            break;
                        }
                    }
                }

                
                return RedirectToAction("Index");
            }


            var locacaofilmes = db.Locacao.Include(l => l.Clientes).Include(l => l.Filmes).Where(f => f.Filme == locacao.Filme).OrderByDescending(f => f.dataLocacao);

            if (locacaofilmes.Count() > 0)
            {

                foreach (var item in locacaofilmes)
                {
                    if (item.DataDevolucao == null)
                    {
                       
                        if (item.Cliente == locacao.Cliente)
                        {
                            
                                ModelState.AddModelError("Filme", "Filme alugado por este Cliente no dia " + item.dataLocacao);
                                break;
                            
                        }
                        else
                        {
                            ModelState.AddModelError("Filme", "Filme alugado pelo Cliente " + item.Clientes.Nome + " no dia " + item.dataLocacao);
                            break;
                        }

                       
                    }
                    else
                    {
                        if (item.Cliente == locacao.Cliente)
                        {
                            ModelState.AddModelError("Filme", "Este Filme já foi alugado por este Cliente no dia " + item.dataLocacao + " e devolvido no dia " + item.DataDevolucao);
                            ViewBag.IncluirAssimMesmo = true;
                            TempData["mensagem"] = "IncluirAssim Mesmo";
                        }
                    }
                }
            }
           
            if (ModelState.IsValid)
            {
                locacao.dataLocacao = DateTime.Today;
                db.Locacao.Add(locacao);
                await db.SaveChangesAsync();
                var locacaoCliente = db.Locacao.Include(l => l.Clientes).Include(l => l.Filmes).Where(f => f.Filme == locacao.Filme & f.Cliente == locacao.Cliente).OrderByDescending(f => f.dataLocacao);
                if (locacaoCliente.Count() > 0)
                {
                    foreach (var item in locacaoCliente)
                    {
                        if (item.DataDevolucao == null)
                        {
                            TempData["success"] = "Filme " + item.Filmes.Filme + " alugado pelo Cliente " + item.Clientes.Nome;
                            break;
                        }
                    }
                }
                return RedirectToAction("Index");
            }
          
            ViewBag.Cliente = new SelectList(db.Cliente, "ID", "Nome", locacao.Cliente);
            ViewBag.Filme = new SelectList(db.Filmes, "ID", "Filme", locacao.Filme);
            return View(locacao);
        }
        
        // GET: Locacao/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = await db.Locacao.FindAsync(id);
            if (locacao == null)
            {
                return HttpNotFound();
            }
            TempData["DataLocacao"] = locacao.dataLocacao;
            ViewBag.Cliente = new SelectList(db.Cliente, "ID", "Nome", locacao.Cliente);
            ViewBag.Filme = new SelectList(db.Filmes, "ID", "Filme", locacao.Filme);
            return View(locacao);
        }

        // POST: Locacao/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Locacao locacao)
        {
            if (ModelState.IsValid)
            {
                locacao.dataLocacao = (DateTime)TempData["DataLocacao"];
                locacao.DataDevolucao = DateTime.Today;
                db.Entry(locacao).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData.Remove("DataLocacao");
                var locacaoCliente = db.Locacao.Include(l => l.Clientes).Include(l => l.Filmes).Where(f => f.Filme == locacao.Filme & f.Cliente == locacao.Cliente).OrderByDescending(f => f.dataLocacao);
                if (locacaoCliente.Count() > 0)
                {
                    foreach (var item in locacaoCliente)
                    {
                        if (item.DataDevolucao == DateTime.Today)
                        {
                            TempData["success"] = "Filme " + item.Filmes.Filme + " devolvido pelo Cliente " + item.Clientes.Nome;
                            break;
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Cliente = new SelectList(db.Cliente, "ID", "Nome", locacao.Cliente);
            ViewBag.Filme = new SelectList(db.Filmes, "ID", "Filme", locacao.Filme);
            return View(locacao);
        }

        // GET: Locacao/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locacao locacao = await db.Locacao.FindAsync(id);
            if (locacao == null)
            {
                return HttpNotFound();
            }
            return View(locacao);
        }

        // POST: Locacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Locacao locacao = await db.Locacao.FindAsync(id);
            db.Locacao.Remove(locacao);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
