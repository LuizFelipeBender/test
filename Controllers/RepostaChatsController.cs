using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Controllers
{
 public class RespostaChatsController : Controller
	{
		private readonly Contexto _context;

	public RespostaChatsController(Contexto context)
	{
	_context = context;
	}

	//GET:RespostaChats

	public async Task<IActionResult> Index()
	{
	    return View(await _context.RepostaChat.ToListAsync());

    	}	
	//GET:RespostaChat/Details/5
	public async Task<IActionResult> Details(int? id){
	if(id == null)
	{
	return NotFound();
	} 
	
	var respostaChat = await _context.RepostaChat
		.FirstOrDefaultAsync(m => m.Id == id);
	if (respostaChat == null)       
	{
	return NotFound();
	}
	return View(respostaChat);
}
        //GET RespostaChat/Create   
        public IActionResult Create ()
        {
            return View();
        }
        //POST:RespostaChats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Resposta,Mensagem")] RepostaChat repostaChat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repostaChat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repostaChat);
        }

    //GET: RespostaChats/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respostaChat = await _context.RepostaChat.FindAsync(id);
            if (respostaChat == null)
            {
                return NotFound();
            }
            return View(respostaChat);
        }

        //POST: RespostaChats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Resposta,Mensagem")] RepostaChat repostaChat)
        {
            if (id != repostaChat.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repostaChat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (RespostaChatExists(repostaChat.Id))
                    {
                        return NotFound();
                    }
                    else{
                    throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(repostaChat);
        }

        //GET: RespostaChats/Delete/5
             public async Task<IActionResult> Delete(int? id)
             {
                if (id == null)
                {
                    return NotFound();
                }
                
                var respostaChat = await _context.RepostaChat
                .FirstOrDefaultAsync(m => m.Id == id);
                if(respostaChat == null)
                {
                    return NotFound();
                }
                return View(respostaChat);
             }

                 //Post RespostaChats/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var respostaChat = await _context.RepostaChat.FindAsync(id);
                _context.RepostaChat.Remove(respostaChat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            private bool RespostaChatExists(int id)
            {
                return _context.RepostaChat.Any(e => e.Id == id);
            }
 
            public async Task<JsonResult>Chat(RequestApi request)
            {
                var respostaChat = await _context.RepostaChat.Where(m=>m.Mensagem.ToUpper().Contains(request.mensagem.ToUpper())).FirstOrDefaultAsync();

                if(respostaChat !=null)
                {
                    var resposta = new ResponseApi{ resposta = respostaChat.Resposta};
                    
                    return Json(resposta);
                }
                else
                {
                    var resposta = new ResponseApi{ resposta = "Não entendi a sua pergunta pode reformular?"};
                    return Json(resposta);
                }
            }
    }

}