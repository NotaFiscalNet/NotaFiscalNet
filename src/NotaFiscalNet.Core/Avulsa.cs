using System;
using System.Xml;
using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações do Fisco do Emitente da Nota Fiscal Eletrônica.
    /// </summary>
    public sealed class Avulsa : ISerializavel, IModificavel
    {
        private string _cnpj;
        private DateTime? _dataEmissao;
        private DateTime? _dataPagamento;
        private string _matriculaAgente;
        private string _nomeAgente;
        private string _numeroDocumento;
        private string _orgaoEmitente;
        private string _reparticaoFiscalEmitente;
        private string _telefone;
        private UfIBGE _unidadeFederativa = UfIBGE.NaoEspecificado;
        private decimal? _valor;

        /// <summary>
        /// [Cnpj] Retorna ou define o Cnpj do Órgão Emitente
        /// </summary>
        public string Cnpj
        {
            get => _cnpj;
	        set => _cnpj = ValidationUtil.ValidateCNPJ(value, "Cnpj", false);
        }

        /// <summary>
        /// [xOrgao] Retorna ou define o nome do Órgão Emitente
        /// </summary>
        public string OrgaoEmitente
        {
            get => _orgaoEmitente;
	        set => _orgaoEmitente = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [matr] Retorna ou define a Matrícula do Agente
        /// </summary>
        public string MatriculaAgente
        {
            get => _matriculaAgente;
	        set => _matriculaAgente = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [xAgente] Retorna ou define o nome do Agente.
        /// </summary>
        public string NomeAgente
        {
            get => _nomeAgente;
	        set => _nomeAgente = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [fone] Retorna ou define o Telefone. Preencher com o DDD + número do telefone.
        /// </summary>
        public string Telefone
        {
            get => _telefone;
	        set
            {
                ValidationUtil.ValidateTelefone(value, "Telefone");
                _telefone = value;
            }
        }

        /// <summary>
        /// [UF] Retorna ou define a Sigla da Unidade Federativa
        /// </summary>
        public UfIBGE UnidadeFederativa
        {
            get => _unidadeFederativa;
	        set => _unidadeFederativa = ValidationUtil.ValidateEnum(value, "UnidadeFederativa");
        }

        /// <summary>
        /// [nDAR] Retorna ou define o número do Documento de Arrecadação da Receita.
        /// </summary>
        public string NumeroDocumento
        {
            get => _numeroDocumento;
	        set => _numeroDocumento = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [dEmi] Retorna ou define a Data de emissão do Documento de Arrecadação da Receita.
        /// </summary>
        /// <remarks>Formato AAAA-MM-DD</remarks>
        public DateTime? DataEmissao
        {
            get => _dataEmissao;
	        set
            {
                if (value.HasValue)
                    ValidationUtil.ValidateTData(value.GetValueOrDefault(), "DataEmissao");

                _dataEmissao = value;
            }
        }

        /// <summary>
        /// [vDAR] Retorna ou define o valor total constante no Documento de Arrecadação da Receita
        /// </summary>
        /// <remarks>Formato com 15 caracteres, sendo 13 no corpo e 2 decimais</remarks>
        public decimal? Valor
        {
            get => _valor;
	        set
            {
                ValidationUtil.ValidateTDec_1302(value, "Valor");
                _valor = value;
            }
        }

        /// <summary>
        /// [repEmi] Retorna ou define a Repartição Fiscal Emitente.
        /// </summary>
        public string ReparticaoFiscalEmitente
        {
            get => _reparticaoFiscalEmitente;
	        set => _reparticaoFiscalEmitente = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [dPag] Retorna ou define a Data de pagamento do Documento de Arrecadação da Receita. Opcional.
        /// </summary>
        /// <remarks>Formato AAAA-MM-DD</remarks>
        public DateTime? DataPagamento
        {
            get => _dataPagamento;
	        set
            {
                if (value.HasValue)
                    ValidationUtil.ValidateTData(value.GetValueOrDefault(), "DataPagamento");
                _dataPagamento = value;
            }
        }

        /// <summary>
        /// Retorna se a classe foi modificada.
        /// </summary>
        public bool Modificado => !string.IsNullOrEmpty(Cnpj) ||
                                  !string.IsNullOrEmpty(OrgaoEmitente) ||
                                  !string.IsNullOrEmpty(MatriculaAgente) ||
                                  !string.IsNullOrEmpty(NomeAgente) ||
                                  !string.IsNullOrEmpty(Telefone) ||
                                  UnidadeFederativa != UfIBGE.NaoEspecificado ||
                                  !string.IsNullOrEmpty(NumeroDocumento) ||
                                  DataEmissao.HasValue ||
                                  Valor.HasValue ||
                                  !string.IsNullOrEmpty(ReparticaoFiscalEmitente) ||
                                  DataPagamento.HasValue;

        public void Serializar(XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("avulsa");

            writer.WriteElementString("Cnpj", SerializationUtil.ToCNPJ(Cnpj));
            writer.WriteElementString("xOrgao", SerializationUtil.ToToken(OrgaoEmitente, 60));
            writer.WriteElementString("matr", SerializationUtil.ToToken(MatriculaAgente, 60));
            writer.WriteElementString("xAgente", SerializationUtil.ToToken(NomeAgente, 60));

            if (!string.IsNullOrEmpty(Telefone))
                writer.WriteElementString("fone", SerializationUtil.ToToken(Telefone, 14));

            writer.WriteElementString("UF", UnidadeFederativa.ToString());

            if (!string.IsNullOrEmpty(NumeroDocumento))
                writer.WriteElementString("nDAR", SerializationUtil.ToToken(NumeroDocumento, 60));

            if (DataEmissao.HasValue)
                writer.WriteElementString("dEmi", SerializationUtil.ToTData(DataEmissao.GetValueOrDefault()));

            if (Valor.HasValue)
                writer.WriteElementString("vDAR", SerializationUtil.ToTDec_1302(Valor.GetValueOrDefault()));

            writer.WriteElementString("repEmi", SerializationUtil.ToToken(ReparticaoFiscalEmitente, 60));

            if (DataPagamento.HasValue)
                writer.WriteElementString("dPag", SerializationUtil.ToTData(DataPagamento.GetValueOrDefault()));

            writer.WriteEndElement();
        }
    }
}