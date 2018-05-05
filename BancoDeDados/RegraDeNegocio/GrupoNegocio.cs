using BancoDeDados;
using BancoDeDados.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegraDeNegocio
{
    public class GrupoNegocio
    {
        public Resposta Salvar(GrupoView c)
        {
            var db = DBCore.InstanciaDoBanco();

            Grupo novo = null;

            if (!c.Id.Equals("0"))
            {
                var id = c.Id;
                novo = db.grupo.Where(w => w.Id.Equals(id)).FirstOrDefault();
                novo.Nome = c.Nome;
                novo.Ativo = c.Ativo;
            }
            else
            {
                novo = db.grupo.Create();
                novo.Nome = c.Nome;
                novo.Ativo = c.Ativo;

                db.grupo.Add(novo);
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

        public Resposta Excluir(GrupoView c)
        {
            try
            {
                using (var db = DBCore.NovaInstanciaDoBanco())
                {
                    var id = c.Id;
                    var conta = db.grupo.Where(w => w.Id.Equals(id)).FirstOrDefault();

                    if (conta == null)
                    {
                        return new Resposta(sucesso: false, mensagem: "Impossivel achar o grupo", objeto: c);
                    }

                    db.grupo.Remove(conta);

                    db.SaveChanges();

                    return new Resposta(sucesso: true, objeto: conta);
                }
            }
            catch (Exception ex)
            {
                return new Resposta(false, ex.Message, c);
            }
        }

        public GrupoView ConverteParaView(Grupo c)
        {
            return new GrupoView
            {
                Id = c.Id,
                Nome = c.Nome,
                Ativo = c.Ativo

            };
        }

        public List<GrupoView> PegaTodas()
        {
            var contas = DBCore.InstanciaDoBanco().grupo.ToList();

            var resposta = new List<GrupoView>();
            foreach (var c in contas)
            {
                resposta.Add(ConverteParaView(c));
            }

            return resposta;
        }

        public GrupoView PegaPorCodigo(int id)
        {
            var arq = DBCore.InstanciaDoBanco().grupo
                .Where(w => w.Id.Equals(id))
                .FirstOrDefault();

            GrupoView resposta = null;

            if (arq != null)
            {
                resposta = ConverteParaView(arq);
            }

            return resposta;
        }
    }
}
