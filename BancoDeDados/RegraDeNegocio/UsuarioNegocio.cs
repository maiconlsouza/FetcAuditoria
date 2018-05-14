using BancoDeDados;
using BancoDeDados.DB;
using Interacao.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
                novo.usuario = c.usr;
                novo.senha = c.senha;
                novo.nome = c.nome;
                novo.email = c.email;
       
            }
            else
            {
                novo = db.usuario.Create();
                novo.usuario = c.usr;
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
                usr = c.usuario,
                senha = c.senha,
                nome = c.nome,
                email = c.email,

                tipoUsuario = new TipoUsuarioNegocio().ConverteParaView(c.TipoUsuario),
                grupo = new GrupoNegocio().ConverteParaView(c.GrupoFK)                
            };
        }

        public List<UsuarioView> PegaTodas()
        {
            var objetos = DBCore.InstanciaDoBanco().usuario
                .Include(g => g.GrupoFK)
                .Include(g => g.TipoUsuario)
                .ToList();

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

        public Resposta Login(string usuario, string senha)
        {
            var objeto = DBCore.InstanciaDoBanco().usuario
                .Where(w => w.usuario.Equals(usuario))
                .FirstOrDefault();

            if (objeto == null)
            {
                return new Resposta(false, "Usuário não encontrado");
            }

            if (!objeto.senha.Equals(senha.GeraSHA1()))
            {
                return new Resposta(false, "Senha incorreta");
            }

            return new Resposta(sucesso: true, objeto: objeto);
        }
    }
}
