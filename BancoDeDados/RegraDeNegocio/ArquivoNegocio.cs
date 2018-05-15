using BancoDeDados;
using BancoDeDados.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RegraDeNegocio
{
    public class ArquivoNegocio
    {
        public Resposta Salvar(ArquivoView c)
        {
            var db = DBCore.InstanciaDoBanco();

            Arquivo novo = null;

            if (!c.Id.Equals("0"))
            {
                var id = c.Id;
                novo = db.arquivo.Where(w => w.Id.Equals(id)).FirstOrDefault();
                novo.Descricao = c.Descricao;
                novo.ArquivoLocal = c.ArquivoLocal;
            }
            else
            {
                novo = db.arquivo.Create();
                novo.Descricao = c.Descricao;
                novo.ArquivoLocal = c.ArquivoLocal;

                db.arquivo.Add(novo);
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

        public Resposta Excluir(ArquivoView c)
        {
            try
            {
                using (var db = DBCore.NovaInstanciaDoBanco())
                {
                    var id = c.Id;
                    var conta = db.arquivo.Where(w => w.Id.Equals(id)).FirstOrDefault();

                    if (conta == null)
                    {
                        return new Resposta(sucesso: false, mensagem: "Categoria não encontrada.", objeto: c);
                    }

                    db.arquivo.Remove(conta);

                    db.SaveChanges();

                    return new Resposta(sucesso: true, objeto: conta);
                }
            }
            catch (Exception ex)
            {
                return new Resposta(false, ex.Message, c);
            }
        }

        public ArquivoView ConverteParaView(Arquivo c)
        {
            var resposta = new ArquivoView
            {
                Id = c.Id,
                Descricao = c.Descricao,
                ArquivoLocal = c.ArquivoLocal
            };

            if (c.Usuarios != null && c.Usuarios.Count > 0)
            {
                resposta.Grupo = new GrupoView
                {
                    Nome = c.Usuarios.FirstOrDefault().GrupoFK.Nome
                };
            }

            return resposta;
        }

        public List<ArquivoView> PegaTodas()
        {
            var contas = DBCore.InstanciaDoBanco().arquivo
                .Include(i => i.Usuarios)
                .Include("Usuarios.GrupoFK")
                .ToList();

            var resposta = new List<ArquivoView>();
            foreach (var c in contas)
            {
                resposta.Add(ConverteParaView(c));
            }

            return resposta;
        }

        public ArquivoView PegaPorCodigo(int id)
        {
            var conta = DBCore.InstanciaDoBanco().arquivo
                .Where(w => w.Id.Equals(id))
                .Include(i => i.Usuarios)
                .Include("Usuarios.GrupoFK")
                .FirstOrDefault();

            ArquivoView resposta = null;

            if (conta != null)
            {
                resposta = ConverteParaView(conta);
            }

            return resposta;
        }

        public Resposta Salvar(string descricao, int grupo)
        {
            using (var db = DBCore.NovaInstanciaDoBanco())
            {
                var arquivo = new Arquivo
                {
                    Descricao = descricao,
                    Usuarios = new List<UsuarioArquivo>()
                };

                var usuarios = db.grupo.Where(w => w.Id.Equals(grupo))
                    .FirstOrDefault()
                    .Usuarios.ToList();

                db.arquivo.Add(arquivo);
                db.SaveChanges();
                
                foreach (var usuario in usuarios)
                {
                    var permissao = new UsuarioArquivo
                    {
                        id_arquivo = arquivo.Id,
                        id_grupo = grupo,
                        id_usuario = usuario.Id,
                        lido = 0
                    };
                    
                    db.usuarioArquivo.Add(permissao);
                }

                db.SaveChanges();

                return new Resposta(true, objeto: ConverteParaView(arquivo));
            }
        }

        public void AtualizaArquivo(int id, string arquivo)
        {
            var obj = PegaPorCodigo(id);
            if (obj != null)
            {
                obj.ArquivoLocal = arquivo;
                Salvar(obj);
            }
        }

        public Tuple<int, int> MeuDasboard(int usuario)
        {
            var db = DBCore.InstanciaDoBanco();

            var resumo = db.usuarioArquivo.Where(w =>
                w.id_usuario.Equals(usuario)
            )
            .GroupBy(g => g.id_usuario)
            .Select(s => new
            {
                Lidos = s.Sum(f => f.lido.Equals(1) ? 1 : 0),
                NaoLidos = s.Sum(f => !f.lido.Equals(1) ? 1 : 0)
            }).FirstOrDefault();

            return new Tuple<int, int>(resumo.Lidos, resumo.NaoLidos);
        }

        public List<ArquivoView> PegaMeusArquivosStatus(int usuario, int status)
        {
            var contas = DBCore.InstanciaDoBanco().usuarioArquivo
                .Where(w => 
                    w.id_usuario.Equals(usuario)
                    && (w.lido.Equals(status) || status.Equals(9))
                )
                .Select(s => s.Arquivo)
                .Include(i => i.Usuarios)
                .Include("Usuarios.GrupoFK")
                .OrderBy(o => o.Id)
                .ToList();                

            var resposta = new List<ArquivoView>();
            foreach (var c in contas)
            {
                resposta.Add(ConverteParaView(c));
            }

            return resposta;
        }

        public void MarcaArquivoLido(int id, int usuario)
        {
            using (var db = DBCore.NovaInstanciaDoBanco())
            {
                var objeto = db.usuarioArquivo.Where(w =>
                    w.id_arquivo.Equals(id) && w.id_usuario.Equals(usuario)
                ).FirstOrDefault();

                objeto.lido = 1;

                db.SaveChanges();
            }
        }

        public List<RelatorioAuditoriaView> RelatorioAuditoria()
        {
            var db = DBCore.InstanciaDoBanco();

            var resumo = db.usuarioArquivo.Include(s => s.Usuario)
            .GroupBy(g => g.id_usuario)
            .Select(s => new
            {
                s.Key,
                Lidos = s.Sum(f => f.lido.Equals(1) ? 1 : 0),
                NaoLidos = s.Sum(f => !f.lido.Equals(1) ? 1 : 0)
            }).ToList();

            var resposta = new List<RelatorioAuditoriaView>();

            foreach(var c in resumo)
            {
                var v = new RelatorioAuditoriaView
                {
                    Usuario = new UsuarioView
                    {
                        nome = new UsuarioNegocio().PegaPorCodigo(c.Key).nome
                    },
                    Lidos = c.Lidos,
                    NaoLidos = c.NaoLidos
                };

                resposta.Add(v);
            }

            return resposta;
        }
    }
}
