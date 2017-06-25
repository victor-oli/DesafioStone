using DesafioStone.Dominio.ObjectosValor;
using MongoDB.Bson;
using System.Collections.Generic;
using System;

namespace DesafioStone.Dominio.Entidades
{
    public class Computador
    {
        public ObjectId Id { get; private set; }
        public string Descricao { get; private set; }
        public string Andar { get; private set; }
        public List<Ocorrencia> Ocorrencias { get; private set; }
        public bool Ativo { get; private set; }

        public Computador(string descricao, string andar)
        {
            Id = new ObjectId();
            this.Descricao = descricao;
            this.Andar = andar;
            this.Ocorrencias = new List<Ocorrencia>();
            this.Ativo = true;

            this.Ocorrencias.Add(new Ocorrencia("Cadastro de computador", true));
        }

        public bool VerificarDisponibilidade()
        {
            return PegarUltimaOcorrencia().Liberado;
        }

        public void InformarUso()
        {
            if (!VerificarDisponibilidade())
                throw new ComputadorEmUsoException("Não é possível utilizar um computador que já está em uso.");

            this.Ocorrencias.Add(new Ocorrencia("Computador em uso", false));
        }

        public void Desativar()
        {
            if (!VerificarDisponibilidade())
                throw new ComputadorEmUsoException(string.Format("O computador {0} não pode ser desativador pois está em uso.", this.Descricao));

            this.Ativo = false;

            this.Ocorrencias.Add(new Ocorrencia("Computador desativado", false));
        }

        public Ocorrencia PegarUltimaOcorrencia()
        {
            return this.Ocorrencias[this.Ocorrencias.Count - 1];
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}