using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using DAO.Dao;
using Model.Models;
using MySql.Data.MySqlClient;

namespace WebAPI.Controllers
{
    public class UsuariosController : ApiController
    {
        private UsuarioDAO _daoUsuario = new UsuarioDAO();

        // GET: api/Usuarios
        public IList<Usuario> GetUsuarios()
        {
            return _daoUsuario.Listar();
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(string id)
        {
            Usuario usuario = _daoUsuario.BuscarPorLogin(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(string id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Login)
            {
                return BadRequest();
            }

            try
            {
                usuario.Login = id;
                _daoUsuario.Editar(usuario);
            }
            catch (MySqlException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Usuarios
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _daoUsuario.Incluir(usuario);
            }
            catch (MySqlException)
            {
                if (UsuarioExists(usuario.Login))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = usuario.Login }, usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(string id)
        {
            var usuario = _daoUsuario.BuscarPorLogin(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _daoUsuario.Excluir(id);

            return Ok(usuario);
        }

        private bool UsuarioExists(string id)
        {
            return _daoUsuario.BuscarPorLogin(id) != null;
        }
    }
}