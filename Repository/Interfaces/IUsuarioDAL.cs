using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IUsuarioDAL
    {
        IEnumerable<Usuario> GetAll();

        Usuario GetUsuario(int? id);

        IEnumerable<Usuario> GetUsuario(string nomeUsuario);

        bool AddUsuario(Usuario usuario);

        bool UpdateUsuario(Usuario usuario);

        bool DeleteUsuario(int? id);
    }
}
