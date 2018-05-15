using BancoDeDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BancoDeDados.DB;

namespace RegraDeNegocio
{
    public class UsuarioArquivoNegocio
    {
        public Resposta Salvar(UsuarioArquivoView c)
        {
            var db = DBCore.InstanciaDoBanco();

            UsuarioArquivo novo = null;

            if (!c.Id.Equals("0"))
            {
                var id = c.Id;
                novo = db.usuarioArquivo.Where(w => w.id.Equals(id)).FirstOrDefault();
                novo.lido = c.lido;
                novo.id_arquivo = c.arquivo.Id;
                novo.id_usuario = c.usuario.Id;
                novo.id_grupo = c.grupo.Id;

            }
            else
            {
                novo = db.usuarioArquivo.Create();
                novo.lido = c.lido;
                novo.id_arquivo = c.arquivo.Id;
                novo.id_usuario = c.usuario.Id;
                novo.id_grupo = c.grupo.Id;

                db.usuarioArquivo.Add(novo);
            }

            try
            {
                db.SaveChanges();

                c.Id = novo.id;

                return new Resposta(true, objeto: c);
            }
            catch (Exception ex)
            {
                return new Resposta(false, ex.Message, c);
            }
        }

        public Resposta Excluir(UsuarioArquivoView c)
        {
            try
            {
                using (var db = DBCore.NovaInstanciaDoBanco())
                {
                    var id = c.Id;
                    var objeto = db.usuarioArquivo.Where(w => w.id.Equals(id)).FirstOrDefault();

                    if (objeto == null)
                    {
                        return new Resposta(sucesso: false, mensagem: "UsuarioArquivo não encontrado", objeto: c);
                    }

                    db.usuarioArquivo.Remove(objeto);

                    db.SaveChanges();

                    return new Resposta(sucesso: true, objeto: objeto);
                }
            }
            catch (Exception ex)
            {
                return new Resposta(false, ex.Message, c);
            }
        }

        public UsuarioArquivoView ConverteParaView(UsuarioArquivo c)
        {
            return new UsuarioArquivoView
            {
                Id = c.id,
                lido = c.lido,

                arquivo = new ArquivoNegocio().ConverteParaView(c.Arquivo),
                usuario = new UsuarioNegocio().ConverteParaView(c.Usuario),
                grupo = new GrupoNegocio().ConverteParaView(c.GrupoFK)

            };
        }

        public List<UsuarioArquivoView> PegaTodas()
        {
            var objetos = DBCore.InstanciaDoBanco().usuarioArquivo.ToList();

            var resposta = new List<UsuarioArquivoView>();
            foreach (var c in objetos)
            {
                resposta.Add(ConverteParaView(c));
            }

            return resposta;
        }

        public UsuarioArquivoView PegaPorCodigo(int id)
        {
            var objeto = DBCore.InstanciaDoBanco().usuarioArquivo
                .Where(w => w.id.Equals(id))
                .FirstOrDefault();

            UsuarioArquivoView resposta = null;

            if (objeto != null)
            {
                resposta = ConverteParaView(objeto);
            }

            return resposta;
        }
    }
}
