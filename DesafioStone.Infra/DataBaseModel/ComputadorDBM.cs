using DesafioStone.Dominio.Entidades;
using DesafioStone.Dominio.ObjectosValor;
using MongoDB.Bson;
using System.Collections.Generic;

namespace DesafioStone.Infra.DataBaseModel
{
    public class ComputadorDBM
    {
        public ObjectId Id { get; private set; }
        public string Descricao { get; private set; }
        public string Andar { get; private set; }
        public List<Ocorrencia> Ocorrencias { get; private set; }
        public bool Ativo { get; private set; }

        public ComputadorDBM(Computador computador)
        {
            Id = new ObjectId();
            Descricao = computador.Descricao;
            Andar = computador.Andar;
            Ativo = computador.Ativo;
            Ocorrencias = new List<Ocorrencia>();
            Ocorrencias = computador.Ocorrencias;
        }

        public Computador ConverterParaComputador()
        {
            Computador computador = new Computador(Descricao, Andar);
            computador.Ativo = Ativo;
            computador.Id = Id.ToString();
            computador.Ocorrencias = Ocorrencias;

            computador.Ocorrencias.ForEach(x => x.DataOcorrencia = x.DataOcorrencia);

            return computador;
        }
    }
}