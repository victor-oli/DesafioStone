using MongoDB.Bson;
using System.Collections.Generic;
using System;
using DesafioStone.Dominio.ObjectosValor;

namespace DesafioStone.Dominio.Entidades
{
    public class Computador
    {
        public ObjectId Id { get; private set; } 
        public string Descricao { get; private set; }
        public string Andar { get; private set; }
        public List<Ocorrencia> Ocorrencias { get; private set; }

        public Computador(string descricao, string andar)
        {
            Id = new ObjectId();
            this.Descricao = descricao;
            this.Andar = andar;
            this.Ocorrencias = new List<Ocorrencia>();

            this.Ocorrencias.Add(new Ocorrencia("Cadastro de computador", true));
        }

        public bool VerificarDisponibilidade()
        {
            return this.Ocorrencias[this.Ocorrencias.Count - 1].Liberado;
        }

        public void InformarUso()
        {
            if (!VerificarDisponibilidade())
                throw new ComputadorEmUsoException("Não é possível utilizar um computador que já está em uso.");

            this.Ocorrencias.Add(new Ocorrencia("Computador em uso", false));
        }
    }
}