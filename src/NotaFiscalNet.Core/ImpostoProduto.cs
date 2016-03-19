using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;
using NotaFiscalNet.Core.Interfaces;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa os Tributos incidentes no Produto ou Serviço
    /// </summary>
    public sealed class ImpostoProduto : ISerializavel, IModificavel
    {
        private Icms _icms;
        private readonly ImpostoICMS _ICMS;
        private readonly ImpostoIPI _ipi;
        private readonly ImpostoII _ii;
        private readonly ImpostoPIS _pis;
        private readonly ImpostoPISST _pisst;
        private readonly ImpostoCOFINS _cofins;
        private readonly ImpostoCOFINSST _cofinsst;
        private readonly ImpostoISSQN _issqn;
        private decimal? _valorTotalTributos;

        private readonly Produto _produto;

        internal ImpostoProduto(Produto produto)
        {
            _produto = produto;
            _ICMS = new ImpostoICMS(this);
            _ipi = new ImpostoIPI(this);
            _ii = new ImpostoII(this);
            _pis = new ImpostoPIS(this);
            _pisst = new ImpostoPISST(this);
            _cofins = new ImpostoCOFINS(this);
            _cofinsst = new ImpostoCOFINSST(this);
            _issqn = new ImpostoISSQN(this);
        }

        /// <summary>
        /// [vTotTrib] Retorna ou define o valor estimado total de impostos federais, estaduais e municipais.
        /// </summary>
        [NFeField(FieldName = "vTotTrib", DataType = "TDec_1302")]
        public decimal? ValorTotalTributos
        {
            get { return _valorTotalTributos; }
            set
            {
                _valorTotalTributos = value.HasValue
                    ? (decimal?)ValidationUtil.ValidateTDec_1302(value.Value, "ValorTotalTributos")
                    : null;
            }
        }

        /// <summary>
        /// Retorna ou define o imposto ICMS sendo empregado no produto.
        /// </summary>
        public Icms Icms
        {
            get { return _icms; }
            set
            {
                ValidarConflitoISSQN();
                _icms = value;
            }
        }

        /// <summary>
        /// Retorna o ICMS
        /// </summary>
        [Obsolete("Usar o campos Icms.")]
        [NFeField(ID = "N01", FieldName = "ICMS")]
        [ValidateField(1, true)]
        public ImpostoICMS ICMS
        {
            get { return _ICMS; }
        }

        /// <summary>
        /// Retorna o valor representando as informações de declaração do IPI para o produto. Opcional.
        /// </summary>
        [NFeField(ID = "O01", FieldName = "IPI", Opcional = true)]
        [ValidateField(2, true)]
        public ImpostoIPI IPI
        {
            get { return _ipi; }
        }

        /// <summary>
        /// Retorna o II (Imposto de Importação). Opcional.
        /// </summary>
        [NFeField(ID = "P01", FieldName = "II", Opcional = true)]
        [ValidateField(3, true)]
        public ImpostoII II
        {
            get { return _ii; }
        }

        /// <summary>
        /// Retorna o ISSQN
        /// </summary>
        [NFeField(ID = "U01", FieldName = "ISSQN")]
        [ValidateField(4, true)]
        public ImpostoISSQN ISSQN
        {
            get { return _issqn; }
        }

        /// <summary>
        /// Retorna as informações do PIS (Programa de Integração Social).
        /// </summary>
        [NFeField(ID = "Q01", FieldName = "PIS")]
        [ValidateField(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public ImpostoPIS PIS
        {
            get { return _pis; }
        }

        /// <summary>
        /// Retorna o PIS ST. Opcional.
        /// </summary>
        [NFeField(ID = "R01", FieldName = "PISST", Opcional = true)]
        [ValidateField(6, true)]
        public ImpostoPISST PISST
        {
            get { return _pisst; }
        }

        /// <summary>
        /// Retorna o COFINS
        /// </summary>
        [NFeField(ID = "S01", FieldName = "COFINS")]
        [ValidateField(7, ChaveErroValidacao.CampoNaoPreenchido)]
        public ImpostoCOFINS COFINS
        {
            get { return _cofins; }
        }

        /// <summary>
        /// Retorna o COFINS ST. Opcional.
        /// </summary>
        [NFeField(ID = "T01", FieldName = "COFINSST")]
        [ValidateField(8, true)]
        public ImpostoCOFINSST COFINSST
        {
            get { return _cofinsst; }
        }

        /// <summary>
        /// Retorna a referência para o objeto Produto no qual o Imposto se refere.
        /// </summary>
        internal Produto Produto { get { return _produto; } }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado
        {
            get
            {
                return
                    ICMS.Modificado || Icms != null ||
                    IPI.Modificado ||
                    II.Modificado ||
                    PIS.Modificado ||
                    PISST.Modificado ||
                    COFINS.Modificado ||
                    COFINSST.Modificado ||
                    ISSQN.Modificado;
            }
        }

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("imposto"); // Elemento 'imposto'

            if (ValorTotalTributos.HasValue)
                writer.WriteElementString("vTotTrib", ValorTotalTributos.Value.ToTDec_1302());

            if (Icms != null || ICMS.Modificado)
            {
                /// Nova maneira de registrar o imposto ICMS.
                if (Icms != null)
                {
                    writer.WriteStartElement("ICMS");
                    ((ISerializavel)Icms).Serializar(writer, nfe);
                    writer.WriteEndElement();
                }
                else
                {
                    // TODO: Obsoleto
                    if (ICMS.Modificado)
                        ((ISerializavel)ICMS).Serializar(writer, nfe);
                }

                if (IPI.Modificado)
                    ((ISerializavel)IPI).Serializar(writer, nfe);
                if (II.Modificado)
                    ((ISerializavel)II).Serializar(writer, nfe);
            }
            else
            {
                if (IPI.Modificado)
                    ((ISerializavel)IPI).Serializar(writer, nfe);

                if (ISSQN.Modificado)
                    ((ISerializavel)ISSQN).Serializar(writer, nfe);
            }

            if (PIS.Modificado)
                ((ISerializavel)PIS).Serializar(writer, nfe);
            if (PISST.Modificado)
                ((ISerializavel)PISST).Serializar(writer, nfe);
            if (COFINS.Modificado)
                ((ISerializavel)COFINS).Serializar(writer, nfe);
            if (COFINSST.Modificado)
                ((ISerializavel)COFINSST).Serializar(writer, nfe);

            writer.WriteEndElement(); // Elemento 'imposto'
        }

        private void ValidarConflitoISSQN()
        {
            if (ISSQN.Modificado)
                throw new ErroValidacaoNFeException(ChaveErroValidacao.ConflitoICMSISSQN);
        }
    }
}