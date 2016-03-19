using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa um determinado tio de Armamento utilizado nos Produtos Específicos
    /// </summary>
    public sealed class Armamento : INFeSerializable
    {
        void INFeSerializable.Serialize(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("arma");

            writer.WriteElementString("tpArma", SerializationUtil.GetEnumValue(TipoArma));
            writer.WriteElementString("nSerie", SerializationUtil.ToToken(NumeroSerie, 9));
            writer.WriteElementString("nCano", SerializationUtil.ToToken(NumeroCano, 9));
            writer.WriteElementString("descr", SerializationUtil.ToToken(Descricao, 256));

            writer.WriteEndElement(); // fim do elemento 'arma'
        }

        private TipoArmamento _tipoArma = TipoArmamento.NaoEspecificado;
        private string _numeroSerie;
        private string _numeroCano;
        private string _descricao = string.Empty;

        /// <summary>
        /// Retorna ou define o Tipo da Arma.
        /// </summary>
        [NFeField(ID = "L02", FieldName = "tpArma", DataType = "token")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido, DefaultValue = TipoArmamento.NaoEspecificado)]
        public TipoArmamento TipoArma
        {
            get { return _tipoArma; }
            set
            {
                ValidationUtil.ValidateEnum(value, "TipoArma");
                _tipoArma = value;
            }
        }

        /// <summary>
        /// Retorna ou define o Número de Série da Arma de Fogo
        /// </summary>
        [NFeField(ID = "L03", FieldName = "nSerie")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public string NumeroSerie
        {
            get { return _numeroSerie; }
            set { _numeroSerie = ValidationUtil.TruncateString(value, 15); }
        }

        /// <summary>
        /// Retorna ou define o Número do Cano da Arma de Fogo
        /// </summary>
        [NFeField(ID = "L04", FieldName = "nCano")]
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public string NumeroCano
        {
            get { return _numeroCano; }
            set { _numeroCano = ValidationUtil.TruncateString(value, 15); }
        }

        /// <summary>
        /// Retorna ou define a Descrição Completa da Arma de Fogo, compreendendo: calibre, marca,
        /// capacidade, tipo de funcionamento, comprimento e demais elementos que permitam a sua
        /// perfeita identificação.
        /// </summary>
        [NFeField(ID = "L05", FieldName = "descr", DataType = "token", MinLength = 1, MaxLength = 256)]
        [ValidateField(4, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = ValidationUtil.TruncateString(value, 256); }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return
                    TipoArma != TipoArmamento.NaoEspecificado ||
                    !string.IsNullOrEmpty(NumeroCano) ||
                    !string.IsNullOrEmpty(NumeroSerie) ||
                    !string.IsNullOrEmpty(Descricao);
            }
        }
    }
}