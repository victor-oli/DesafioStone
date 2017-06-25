using DesafioStone.Dominio.ObjectosValor;
using MongoDB.Bson;
using System.Collections.Generic;
using System;

namespace DesafioStone.Dominio.Entidades
{
    public class Computador
    {
        public ObjectId Id { get; private set; }
        public string Descricao { get; set; }
        public string Andar { get; set; }
        public List<Ocorrencia> Ocorrencias { get; private set; }
        public bool Ativo { get; set; }

        public Computador(string descricao, string andar)
        {
            Id = new ObjectId();
            this.Descricao = descricao.Trim().ToUpper();
            this.Andar = andar.Trim().ToUpper();
            this.Ocorrencias = new List<Ocorrencia>();
            this.Ativo = true;

            this.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.PrimeiraOcorrencia());
        }

        public bool VerificarDisponibilidade()
        {
            return PegarUltimaOcorrencia().Liberado;
        }

        public void InformarUso()
        {
            if (!VerificarDisponibilidade())
                throw new ComputadorEmUsoException("Não é possível utilizar um computador que já está em uso.");

            this.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorEmUso());
        }

        public void Desativar()
        {
            if (!VerificarDisponibilidade())
                throw new ComputadorEmUsoException(string.Format("O computador {0} não pode ser desativador pois está em uso.", this.Descricao));

            this.Ativo = false;

            this.Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorDesativado());
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