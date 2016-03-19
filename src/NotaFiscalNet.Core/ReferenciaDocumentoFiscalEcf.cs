using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Validacao;
using System;
using System.Linq;

namespace NotaFiscalNet.Core
{
    public sealed class ReferenciaDocumentoFiscalEcf : ISerializavel, IReferenciaDocumentoFiscal
    {
        private string _codigoModeloDocumentoFiscal = string.Empty;

        /// <summary>
        /// [mod] Retorna o C�digo do Modelo do Documento Fiscal Refer�nciado. Preencher com "2B",
        /// quando se tratar de Cupom Fiscal emitido por m�quina registradora (n�o ECF), com "2C",
        /// quando se tratar de Cupom Fiscal PDV, ou "2D", quando se tratar de Cupom Fiscal (emitido
        /// por ECF)
        /// </summary>
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string CodigoModeloDocumentoFiscal
        {
            get { return _codigoModeloDocumentoFiscal; }
            set
            {
                ValidarCodigoModeloDocumentoFiscal(value);
                _codigoModeloDocumentoFiscal = value;
            }
        }

        /// <summary>
        /// [nECF] Retorna ou define o n�mero de ordem seq�encial do ECF que emitiu o Cupom Fiscal
        /// vinculado � NF-e.
        /// </summary>
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public int NumeroEcf { get; set; }

        /// <summary>
        /// [nCOO] Retorna ou define o N�mero do Contador de Ordem de Opera��o - COO vinculado � NF-e.
        /// </summary>
        [ValidateField(3, ChaveErroValidacao.CampoNaoPreenchido)]
        public int NumeroContadorOrdemOperacao { get; set; }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => !string.IsNullOrWhiteSpace(CodigoModeloDocumentoFiscal) ||
                                  NumeroEcf != 0 ||
                                  NumeroContadorOrdemOperacao != 0;

        public void Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("refECF");
            writer.WriteElementString("mod", CodigoModeloDocumentoFiscal);
            writer.WriteElementString("nECF", NumeroEcf.ToString());
            writer.WriteElementString("nCOO", NumeroContadorOrdemOperacao.ToString());
            writer.WriteEndElement();
        }

        private void ValidarCodigoModeloDocumentoFiscal(string valor)
        {
            var valoresValidos = new[] { "2B", "2C", "2D" };
            if (!valoresValidos.Contains(valor))
                throw new ArgumentException("O c�digo do modelo de Documento Fiscal informado � inv�lido.");
        }
    }
}