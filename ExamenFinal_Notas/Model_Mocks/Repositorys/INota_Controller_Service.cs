using ExamenFinal_Notas.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExamenFinal_Notas.Model_Mocks.Controllers
{
    public interface INota_Controller_Service
    {
        public User Get_UsserLogged(Claim claim);
        public User Usser_lookfor(string user, string password);
        public void Register_User(string username, string contraseña);
        public User Get_Username(string username);
        public List<Etiqueta> Obtener_lista_etiqueta();
        public List<Etiqueta_Nota> Obtener_lista_etiqueta_Nota();
        public List<Nota> Obtener_lista_Notas_UsuarioLoggeado(int id);
        public void Agregar_idUser_a_Nota(Nota nota,int id);
        public void Añadir_Etiq_a_BD(List<Etiqueta_Nota> etiq );
        public Nota Obtener_Nota_Id(int id);
        public void Guardar_Editar_Nota(Nota nota);
        public List<Nota_Compartida> Obtener_NotasCompartidas_Id(int id);
        public List<Nota_Compartida> Obtener_NotasCompartidas_Id_text(int id);
        public void Guardar_Nota_Compartida(Nota_Compartida nota_Compartida);
        public void Eliminar_Nota(int id);
        public List<Etiqueta_Nota> Obtener_Etiqueta_Nota_Detalle();
        
        public List<User> Obtener_Lista_Usuarios();


    }
    public class Nota_Controller_Service: INota_Controller_Service
    {
        private readonly NotasContext _context;
        private ITempDataDictionary _tData;
        public Nota_Controller_Service(NotasContext _context)
        {
            this._context = _context;
        }

        public User Get_Username(string username)
        {
            return _context.Users
                .Where(o => o.Username == username)
                .FirstOrDefault();

        }
        public User Get_UsserLogged(Claim claim)
        {
            var user = _context.Users.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }
        public User Usser_lookfor(string username, string contraseña)
        {
            var usuario = _context.Users.Where(o => o.Username == username && o.Password == contraseña).FirstOrDefault();
            return usuario;
        }
        public void Register_User(string username, string contraseña)
        {
            var usuario = new User();
            usuario.Username = username;
            usuario.Password = contraseña;

            _context.Users.Add(usuario);
            _context.SaveChanges();
        }
        public List<Etiqueta> Obtener_lista_etiqueta()
        {

            return _context.Etiquetas.ToList();
        }
        public List<Etiqueta_Nota> Obtener_lista_etiqueta_Nota()
        {
            return _context.Etiqueta_Notas.ToList();
        }
        public List<Nota> Obtener_lista_Notas_UsuarioLoggeado(int id)
        {
            return _context.Notas.Where(o => o.UserId == id).ToList();
        }
        public void Agregar_idUser_a_Nota(Nota nota, int id)
        {

            nota.UserId = id;
            _context.Notas.Add(nota);
            _context.SaveChanges();
        }
        public void Añadir_Etiq_a_BD(List<Etiqueta_Nota> etiq)
        {
            _context.Etiqueta_Notas.AddRange(etiq);
            _context.SaveChanges();
        }
        public Nota Obtener_Nota_Id(int id)
        {
            return _context.Notas.Where(o => o.Id == id).FirstOrDefault();
        }
        public void Guardar_Editar_Nota(Nota nota)
        {
            _context.Notas.Update(nota);
             _context.SaveChanges();

        }
        public List<Nota_Compartida> Obtener_NotasCompartidas_Id(int id)
        {
            return _context.Nota_Compartidas.Include(o => o.user).Where(o => o.UserIdR == id).ToList();
        }
        public List<Nota_Compartida> Obtener_NotasCompartidas_Id_text(int id)
        {
            return _context.Nota_Compartidas.Include(o => o.nota).Where(o => o.UserIdR == id).ToList();
        }
        public void Guardar_Nota_Compartida(Nota_Compartida nota_Compartida)
        {
            _context.Nota_Compartidas.Add(nota_Compartida);
            _context.SaveChanges();
        }
        public void Eliminar_Nota(int id)
        {
            var nota = _context.Notas.Where(o => o.Id == id).FirstOrDefault();

            var etiqueta = _context.Etiqueta_Notas.Where(o => o.NotaId == id).ToList();

            _context.Notas.Remove(nota);
            _context.Etiqueta_Notas.RemoveRange(etiqueta);
            _context.SaveChanges();
        }
        public List<User> Obtener_Lista_Usuarios()
        {
            return _context.Users.ToList();
        }
        public List<Etiqueta_Nota> Obtener_Etiqueta_Nota_Detalle()
        {
            return _context.Etiqueta_Notas.Include(o => o.Etiqueta).ToList();
        }
    }
}
