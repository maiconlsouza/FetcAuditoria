﻿using BancoDeDados;
using BancoDeDados.DB;
using System;
using System.Collections.Generic;
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
            }
            else
            {
                novo = db.arquivo.Create();
                novo.Descricao = c.Descricao;

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
            return new ArquivoView
            {
                Id = c.Id,
                Descricao = c.Descricao
            };
        }

        public List<ArquivoView> PegaTodas()
        {
            var contas = DBCore.InstanciaDoBanco().arquivo.ToList();

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
                .FirstOrDefault();

            ArquivoView resposta = null;

            if (conta != null)
            {
                resposta = ConverteParaView(conta);
            }

            return resposta;
        }
    }
}
