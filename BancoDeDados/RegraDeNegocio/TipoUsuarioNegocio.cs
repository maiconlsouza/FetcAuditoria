using BancoDeDados;
using BancoDeDados.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegraDeNegocio
{
    public class TipoUsuarioNegocio
    {
        public Resposta Salvar(TipoUsuarioView c)
        {
            var db = DBCore.InstanciaDoBanco();

            TipoUsuario novo = null;

            if (!c.Id.Equals("0"))
            {
                var id = c.Id;
                novo = db.tipoUsuarios.Where(w => w.Id.Equals(id)).FirstOrDefault();
                novo.descricao = c.Descricao;
                novo.sobe_arquivo = c.sobe_arquivo;
                novo.cria_usuario = c.cria_usuario;

            }
            else
            {
                novo = db.tipoUsuarios.Create();
                novo.descricao = c.Descricao;
                novo.sobe_arquivo = c.sobe_arquivo;
                novo.cria_usuario = c.cria_usuario;

                db.tipoUsuarios.Add(novo);
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

        public Resposta Excluir(TipoUsuarioView c)
        {
            try
            {
                using (var db = DBCore.NovaInstanciaDoBanco())
                {
                    var id = c.Id;
                    var conta = db.tipoUsuarios.Where(w => w.Id.Equals(id)).FirstOrDefault();

                    if (conta == null)
                    {
                        return new Resposta(sucesso: false, mensagem: "Impossivel achar o tipo do usuário", objeto: c);
                    }

                    db.tipoUsuarios.Remove(conta);

                    db.SaveChanges();

                    return new Resposta(sucesso: true, objeto: conta);
                }
            }
            catch (Exception ex)
            {
                return new Resposta(false, ex.Message, c);
            }
        }

        public TipoUsuarioView ConverteParaView(TipoUsuario c)
        {
            return new TipoUsuarioView
            {
                Id = c.Id,
                Descricao = c.descricao,
                sobe_arquivo = c.sobe_arquivo,
                cria_usuario = c.cria_usuario
            };
        }

        public List<TipoUsuarioView> PegaTodas()
        {
            var contas = DBCore.InstanciaDoBanco().tipoUsuarios.ToList();

            var resposta = new List<TipoUsuarioView>();
            foreach (var c in contas)
            {
                resposta.Add(ConverteParaView(c));
            }

            return resposta;
        }

        public TipoUsuarioView PegaPorCodigo(int id)
        {
            var arq = DBCore.InstanciaDoBanco().tipoUsuarios
                .Where(w => w.Id.Equals(id))
                .FirstOrDefault();

            TipoUsuarioView resposta = null;

            if (arq != null)
            {
                resposta = ConverteParaView(arq);
            }

            return resposta;
        }
    }
}
