using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace CRUD_AspNET_CoreMVC.Controllers
{
    public class UsuarioController : Controller
    {
        public IUsuarioDAL UsuarioDAL { get; }

        public UsuarioController(IUsuarioDAL usuarioDAL)
        {
            UsuarioDAL = usuarioDAL;
        }

        public IActionResult Index()
        {
            List<Usuario> listaUsuarios = UsuarioDAL.GetAll().ToList();

            return View(listaUsuarios);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public JsonResult GetDetails(int? codigo)
        {
            if(codigo == null) { return Json("Inválido."); }

            Usuario usuario = UsuarioDAL.GetUsuario(codigo);

            if(usuario == null) { return Json("Não encontrado."); }

            return Json(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateUsuario([FromBody]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (UsuarioDAL.AddUsuario(usuario))
                    return Json("Sucesso");
                else
                    return Json("Falha");
            }
            else
                return Json("Inválido");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateUsuario([FromBody]Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (UsuarioDAL.UpdateUsuario(usuario))
                    return Json("Sucesso");
                else
                    return Json("Falha");
            }
            else
                return Json("Inválido");
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteUsuario(int? codigo)
        {
            if (ModelState.IsValid)
            {
                if (UsuarioDAL.DeleteUsuario(codigo))
                    return Json("Sucesso");
                else
                    return Json("Falha");
            }
            else
                return Json("Inválido");
        }
    }
}