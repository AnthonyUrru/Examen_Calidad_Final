using ExamenFinal_Notas.Model_Mocks.Controllers;
using ExamenFinal_Notas.Model_Mocks.Services;
using ExamenFinal_Notas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenFinal_Notas.Controllers
{
    public class NotasController : Controller
    {
        private readonly ICookieService _cook;
        private readonly INota_Controller_Service _repos;
        public NotasController(INota_Controller_Service _repos,ICookieService _cook)
        {
            //this._context = context;
            this._repos = _repos;
            this._cook = _cook;
        }
        [HttpGet]
        public ActionResult Index()
        {

            //ViewBag.Etiquetas = _context.Etiquetas.ToList();
            ViewBag.Etiquetas = _repos.Obtener_lista_etiqueta();
            // ViewBag.Etiquetitas = _context.Etiqueta_Notas.ToList();
            ViewBag.Etiquetitas = _repos.Obtener_lista_etiqueta_Nota();
            //ViewBag.Nota_Compartidas = _context.Nota_Compartidas.Include(o => o.user).Where(o=>o.UserIdR==LoggerUser().Id).ToList();
            

            return View();
        }
        [HttpGet]
        public IActionResult _Index(string search)
        {
            // var notas = _context.Notas.Where(o => o.UserId == LoggerUser().Id).ToList();
            var notas = _repos.Obtener_lista_Notas_UsuarioLoggeado(LoggerUser().Id);
            //ViewBag.Etiquetas = _context.Etiquetas.ToList();
            ViewBag.Etiquetas = _repos.Obtener_lista_etiqueta();
            //ViewBag.Etiquetitas = _context.Etiqueta_Notas.ToList();
            ViewBag.Etiquetitas = _repos.Obtener_lista_etiqueta_Nota();
            if (!String.IsNullOrEmpty(search))
            {
                notas = notas.Where(o => o.Tittle.Contains(search) || o.Nota_Content.Contains(search)).ToList();
                return View(notas);
            }
            return View(notas);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Etiquetas = _context.Etiquetas.ToList();
            ViewBag.Etiquetas = _repos.Obtener_lista_etiqueta();
            return View(new Nota());
        }
        [HttpPost]
        public IActionResult Create(Nota nota, List<int> etiqueta)
        {
            nota.DateRegister = DateTime.Now;
            List<Etiqueta_Nota> etiq = new List<Etiqueta_Nota>();
            Nota_Compartida us = new Nota_Compartida();
            if (etiqueta.Count() == 0)
                ModelState.AddModelError("etiqueta", "Campo invalido");

            if (ModelState.IsValid)
            {
                //nota.UserId = LoggerUser().Id;
                //_context.Notas.Add(nota);
                //_context.SaveChanges();
                //----------------------------------------------
                _repos.Agregar_idUser_a_Nota(nota,LoggerUser().Id);
                // us.NotaId = nota.Id;
                //us.UserId = LoggerUser().Id;
                //_context.User_Notas.Add(us);
                // _context.SaveChanges();
                foreach (var item in etiqueta)
                {
                    var eti_nota = new Etiqueta_Nota();
                    eti_nota.EtiquetaId = item;
                    eti_nota.NotaId = nota.Id;
                    etiq.Add(eti_nota);
                }
                _repos.Añadir_Etiq_a_BD(etiq);
                //_context.Etiqueta_Notas.AddRange(etiq);
                //_context.SaveChanges();

                //ViewBag.Etiquetas = _context.Etiquetas.ToList();
                ViewBag.Etiquetas = _repos.Obtener_lista_etiqueta();
                return View("Index");
            }
            else
            {
                Response.StatusCode = 400;
                //ViewBag.Etiquetas = _context.Etiquetas.ToList();
                ViewBag.Etiquetas = _repos.Obtener_lista_etiqueta();
                return View(nota);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //ViewBag.Etiquetas = _context.Etiquetas.ToList();
            ViewBag.Etiquetas = _repos.Obtener_lista_etiqueta();
            //var nota = _context.Notas.Where(o => o.Id == id).FirstOrDefault();
            var nota = _repos.Obtener_Nota_Id(id);
            return View(nota);

        }
        [HttpPost]
        public IActionResult Edit(Nota nota /*List<int> etiqueta*/)
        {
            nota.DateRegister = DateTime.Now;
            //List<EtiquetaNota> etic = new List<EtiquetaNota>();

            //if (etiqueta.Count() == 0)
            //    ModelState.AddModelError("etiqueta", "Seleccione por lo menos uno");

            if (ModelState.IsValid)
            {

                _repos.Guardar_Editar_Nota(nota);
                //_context.Notas.Update(nota);
               // _context.SaveChanges();




                //foreach (var item in etiqueta)
                //{
                //    var etique = new EtiquetaNota();
                //    etique.IdEtiqueta = item;
                //    etique.IdNota = nota.Id;
                //    etic.Add(etique);
                //}
                //context.EtiquetaNotas.AddRange(etic);
                //context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                Response.StatusCode = 400;
                //ViewBag.Etiquetas = _context.Etiquetas.ToList();
                ViewBag.Etiquetas = _repos.Obtener_lista_etiqueta();
                return View(nota);
            }

        }
        [HttpGet]
        public IActionResult Compartir(string search)
        {
            //var notas =_context.Nota_Compartidas.Include(o => o.user).Where(o => o.UserIdR == LoggerUser().Id).ToList();

           // ViewBag.Etiquetas = _context.Etiquetas.ToList();
            ViewBag.Etiquetas = _repos.Obtener_lista_etiqueta();

            //ViewBag.Etiquetitas = _context.Etiqueta_Notas.ToList();
            ViewBag.Etiquetitas = _repos.Obtener_lista_etiqueta_Nota();

            //ViewBag.Nota_Compartidas = _context.Nota_Compartidas.Include(o => o.user).Where(o => o.UserIdR == LoggerUser().Id).ToList();
            ViewBag.Nota_Compartidas = _repos.Obtener_NotasCompartidas_Id(LoggerUser().Id);

            //ViewBag.Nota_Compartidas_text = _context.Nota_Compartidas.Include(o => o.nota).Where(o => o.UserIdR == LoggerUser().Id).ToList();
            ViewBag.Nota_Compartidas_text = _repos.Obtener_NotasCompartidas_Id_text(LoggerUser().Id);
            return View();
        }
        [HttpPost]
        public IActionResult Compartir2(int usuario, int notaId){
            var nota_compartida = new Nota_Compartida();
            nota_compartida.NotaId = notaId;
            nota_compartida.UserIdC = LoggerUser().Id;
            nota_compartida.UserIdR = usuario;
            _repos.Guardar_Nota_Compartida(nota_compartida);
            //_context.Nota_Compartidas.Add(nota_compartida);
            //_context.SaveChanges();
            return RedirectToAction("Index");
            }
        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            // var nota = _context.Notas.Where(o => o.Id == id).FirstOrDefault();

            //var etiqueta = _context.Etiqueta_Notas.Where(o => o.NotaId == id).ToList();

            //_context.Notas.Remove(nota);
            //_context.Etiqueta_Notas.RemoveRange(etiqueta);
            //_context.SaveChanges();
            _repos.Eliminar_Nota(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detalle(int id)
        {

            //var etiqueta = _context.Etiquetas.ToList();
            var etoqueda= _repos.Obtener_lista_etiqueta();
            //ViewBag.Etiquetas = _context.Etiqueta_Notas.Include(o => o.Etiqueta).ToList();
            ViewBag.Etiquetas = _repos.Obtener_Etiqueta_Nota_Detalle();
           //ViewBag.Users = _context.Users.ToList();
           ViewBag.Users = _repos.Obtener_Lista_Usuarios();
            // var nota = _context.Notas.Where(o => o.Id == id).FirstOrDefault();
            var nota = _repos.Obtener_Nota_Id(id);
            return View(nota);
        }
       
        public IActionResult Barra_Lateral()
        {
            return View();
        }
        private User LoggerUser()
        {
            _cook.SetHttpContext(HttpContext);
            var claim = _cook.Obtener_ClaimPrincipal();
            var user = _repos.Get_UsserLogged(claim);
            return user;

        }
        //public User LoggerUser()
        //{
        //    var claim = HttpContext.User.Claims.FirstOrDefault();
        //    var user = _context.Users.Where(o => o.Username == claim.Value).FirstOrDefault();
        //    return user;
        //}


    }
}
