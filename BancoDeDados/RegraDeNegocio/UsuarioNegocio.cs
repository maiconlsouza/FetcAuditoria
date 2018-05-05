using BancoDeDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoDeDados.DB;

namespace RegraDeNegocio
{
    public class UsuarioNegocio
    {
        public Resposta Salvar(UsuarioView c)
        {
            var db = DBCore.InstanciaDoBanco();

            Usuario novo = null;

            if (!c.Id.Equals("0"))
            {
                var id = c.Id;
                novo = db.usuario.Where(w => w.Id.Equals(id)).FirstOrDefault();
                novo.usr = c.usr;
                novo.senha = c.senha;
                novo.nome = c.nome;
                novo.email = c.email;
       
            }
            else
            {
                novo = db.usuario.Create();
                novo.usr = c.usr;
                novo.senha = c.senha;
                novo.nome = c.nome;
                novo.email = c.email;

                db.usuario.Add(novo);
            }

            try
            {
                db.SaveChanges();

                c.Id = novo.Id;

                return new Resposta(true, objeto: c);
            }
            catch (Exception ex)
            {
                return new Resposta(false, ex.Message, c);
            }
        }

        public Resposta Excluir(UsuarioView c)
        {
            try
            {
                using (var db = DBCore.NovaInstanciaDoBanco())
                {
                    var id = c.Id;
                    var objeto = db.usuario.Where(w => w.Id.Equals(id)).FirstOrDefault();

                    if (objeto == null)
                    {
                        return new Resposta(sucesso: false, mensagem: "Usuário não encontrado", objeto: c);
                    }

                    db.usuario.Remove(objeto);

                    db.SaveChanges();

                    return new Resposta(sucesso: true, objeto: objeto);
                }
            }
            catch (Exception ex)
            {
                return new Resposta(false, ex.Message, c);
            }
        }

        public UsuarioView ConverteParaView(Usuario c)
        {
            return new UsuarioView
            {
                Id = c.Id,
                usr = c.usr,
                senha = c.senha,
                nome = c.nome,
                email = c.email,

                tipoUsuario = new TipoUsuarioNegocio().ConverteParaView(c.TipoUsuario),
                grupo = new GrupoNegocio().ConverteParaView(c.grupo)
                
            };
        }

        public List<UsuarioView> PegaTodas()
        {
            var objetos = DBCore.InstanciaDoBanco().usuario.ToList();

            var resposta = new List<UsuarioView>();
            foreach (var c in objetos)
            {
                resposta.Add(ConverteParaView(c));
            }

            return resposta;
        }

        public UsuarioView PegaPorCodigo(int id)
        {
            var objeto = DBCore.InstanciaDoBanco().usuario
                .Where(w => w.Id.Equals(id))
                .FirstOrDefault();

            UsuarioView resposta = null;

            if (objeto != null)
            {
                resposta = ConverteParaView(objeto);
            }

            return resposta;
        }

       
    }
}
