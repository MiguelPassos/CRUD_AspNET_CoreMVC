using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Entity;
using Microsoft.Extensions.Configuration;

namespace Repository
{
    public class UsuarioDAL : IUsuarioDAL
    {
        protected SqlConnection Connection;

        public UsuarioDAL(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetConnectionString("LocalConnection"));
        }

        public IEnumerable<Usuario> GetAll()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                using (var conn = Connection)
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WITH(NOLOCK)", conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Usuario usuario = new Usuario() {
                            Codigo = Convert.ToInt32(dr["Codigo"]),
                            Nome = dr["Nome"].ToString(),
                            DataNascimento = Convert.ToDateTime(dr["DataNascimento"])
                        };

                        listaUsuarios.Add(usuario);
                    }
                    conn.Close();
                }

                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario GetUsuario(int? id)
        {
            Usuario usuario = null;

            try
            {
                using (var conn = Connection)
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WITH(NOLOCK) WHERE Codigo = @ID", conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        usuario = new Usuario()
                        {
                            Codigo = Convert.ToInt32(dr["Codigo"]),
                            Nome = dr["Nome"].ToString(),
                            DataNascimento = Convert.ToDateTime(dr["DataNascimento"])
                        };
                    }

                    conn.Close();
                }

                return usuario;
            }
            catch (Exception ex)
            {
                //Realiza log de erro em Banco ou Arquivo físico e retorna FALSE para tratamento
                //Exemplo: Exibição de mensagem amigável.
                return usuario;
            }
        }

        public IEnumerable<Usuario> GetUsuario(string nomeUsuario)
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                using (var conn = Connection)
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WITH(NOLOCK) WHERE Nome LIKE '@NOME%'", conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@NOME", nomeUsuario);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Usuario usuario = new Usuario()
                        {
                            Codigo = Convert.ToInt32(dr["Codigo"]),
                            Nome = dr["Nome"].ToString(),
                            DataNascimento = Convert.ToDateTime(dr["DataNascimento"])
                        };

                        listaUsuarios.Add(usuario);
                    }

                    conn.Close();
                }

                return listaUsuarios;
            }
            catch (Exception ex)
            {
                //Realiza log de erro em Banco ou Arquivo físico e retorna FALSE para tratamento
                //Exemplo: Exibição de mensagem amigável.
                return null;
            }
        }

        public bool AddUsuario(Usuario usuario)
        {
            try
            {
                using (var conn = Connection)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Usuario VALUES (@NOME, @DTNASCIMENTO)", conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@NOME", usuario.Nome);
                    cmd.Parameters.AddWithValue("@DTNASCIMENTO", usuario.DataNascimento);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                //Realiza log de erro em Banco ou Arquivo físico e retorna FALSE para tratamento
                //Exemplo: Exibição de mensagem amigável.
                return false;
            }
        }
        
        public bool UpdateUsuario(Usuario usuario)
        {
            try
            {
                using (var conn = Connection)
                {
                    SqlCommand cmd = new SqlCommand(@"UPDATE Usuario SET Nome = @NOME, DataNascimento = @DTNASCIMENTO
                                                      WHERE Codigo = @CODIGO", conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@NOME", usuario.Nome);
                    cmd.Parameters.AddWithValue("@DTNASCIMENTO", usuario.DataNascimento);
                    cmd.Parameters.AddWithValue("@CODIGO", usuario.Codigo);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                //Realiza log de erro em Banco ou Arquivo físico e retorna FALSE para tratamento
                //Exemplo: Exibição de mensagem amigável.
                return false;
            }
        }

        public bool DeleteUsuario(int? id)
        {
            try
            {
                using (var conn = Connection)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE Codigo = @ID", conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                //Realiza log de erro em Banco ou Arquivo físico e retorna FALSE para tratamento
                //Exemplo: Exibição de mensagem amigável.
                return false;
            }
        }
    }
}
