using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Contém as informações de Totalização da Nota Fiscal Eletrônica.
    /// </summary>
    
    
    
    public sealed class TotalNFe :  INFeSerializable
    {
        #region Fields

        private TotalICMS _ICMS = new TotalICMS();
        private TotalISSQN _ISSQN = new TotalISSQN();
        private RetencaoTributosFederais _retencaoTributosFederais = new RetencaoTributosFederais();

        private bool _isReadOnly = false;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Retorna o Total de ICMS
        /// </summary>
        [NFeField(ID = "W01", FieldName = "ICMSTot")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public TotalICMS ICMS
        {
            get { return _ICMS ; }
        }

        /// <summary>
        /// Retorna o Total de ISSQN. Opcional.
        /// </summary>
        [NFeField(ID = "W17", FieldName = "ISSQNtot", Opcional = true)]
        [ValidateField(2, true)]
        public TotalISSQN ISSQN
        {
            get { return _ISSQN; }
        }

        /// <summary>
        /// Retorna as Retenções de Tributos Federais. Opcional.
        /// </summary>
        /// <remarks>
        /// Exemplos de atos normativos que definem obrigatoriedade da retenção de contribuições:
        /// a) IRPJ/CSLL/PIS/COFINS - Fonte - Recebimentos de Órgãos Públicos Federais Lei nº 9.430, de 27 de dezembro de 1996, 
        /// art. 64 Lei nº 10.833/2003, art. 34 como normas infralegais, temos como exemplo: Instrução Normativa SRF nº 480/2004
        /// e Instrução Normativa nº 539, de 25/04/2005.
        /// b) Retenção do Imposto de Renda pelas Fontes Pagadoras REMUNERAÇÃO DE SERVIÇOS PROFISSIONAIS PRESTADOS POR PESSOA 
        /// JURÍDICA LEI Nº 7.450/85, ART. 52 
        /// c) IRPJ, CSLL, COFINS e PIS - Serviços Prestados por Pessoas Jurídicas - Retenção na Fonte Lei nº 10.833 de 29.12.2003, 
        /// arts. 30, 31, 32, 35 e 36
        /// </remarks>
        [NFeField(ID = "W23", FieldName = "retTrib", Opcional = true)]
        [ValidateField(3, true)]
        public RetencaoTributosFederais RetencaoTributosFederais
        {
            get { return _retencaoTributosFederais; }
        }

        /// <summary>
        /// Retorna o valor indicando se a Nota Fiscal Eletrônica está em modo somente-leitura.
        /// </summary>
        /// <remarks>
        /// A Nota Fiscal Eletrônica estará em modo somente-leitura quando for instanciada a partir de um arquivo assinado digitalmente.
        /// </remarks>
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
        }

        #endregion Properties

        #region INFeSerializable Members

        void INFeSerializable.Serialize(System.Xml.XmlWriter writer, NFe nfe)
        {
            #region total

            writer.WriteStartElement("total");
            
            ((INFeSerializable)ICMS).Serialize(writer, nfe);
            if (ISSQN.IsDirty)
                ((INFeSerializable)ISSQN).Serialize(writer, nfe);
            if (RetencaoTributosFederais.IsDirty)
                ((INFeSerializable)RetencaoTributosFederais).Serialize(writer, nfe);

            writer.WriteEndElement(); // fim do elemento 'total' 

            #endregion total
        }

        #endregion
    }
}
