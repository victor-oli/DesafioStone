﻿using DesafioStone.Dominio.ObjectosValor;
using System.Collections.Generic;

namespace DesafioStone.Dominio.Entidades
{
    public class Computador
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Andar { get; set; }
        public List<Ocorrencia> Ocorrencias { get; set; }
        public bool Ativo { get; set; }

        public Computador(string descricao, string andar)
        {
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

        public void InformarLiberacao()
        {
            Ocorrencias.Add(Ocorrencia.OcorrenciaFabrica.ComputadorLiberado());
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
    }
}