using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

using System;
using System.Collections.Generic;
using System.Linq;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Responsável por armazenar as informações de Indentificação de um Documento Fiscal.
    /// </summary>
    public class IdentificacaoDocumentoFiscal : ISerializavel
    {
        /// <summary>
        /// [cUF] Retorna ou define o código Unidade Federativa (IBGE) do Emitente do Documento Fiscal.
        /// </summary>
        public virtual UfIBGE UnidadeFederativaEmitente { get; set; } = UfIBGE.NaoEspecificado;

        /// <summary>
        /// [cNF] Retorna o código numérico que compõe a Chave de Acesso. Número aleatório gerado
        /// pelo emitente para cada NF-e para evitar acessos indevidos da NF-e.
        /// </summary>
        public int CodigoNumerico { get; set; } = new Random().Next(10000000, 99999999);

        /// <summary>
        /// [natOp] Retorna ou define a descrição da Natureza da Operação.
        /// </summary>
        public string NaturezaOperacao { get; set; }

        /// <summary>
        /// [indPag] Retorna ou define o indicador da Forma de Pagamento. Valor padrão 'AVista'.
        /// </summary>
        public IndicadorFormaPagamento FormaPagamento { get; set; } = IndicadorFormaPagamento.AVista;

        /// <summary>
        /// [mod] Código do modelo do Documento Fiscal. 55 = NF-e; 65 = NFC-e.
        /// Valor padrão: NF-e
        /// </summary>
        public TipoModalidadeDocumentoFiscal ModalidadeDocumentoFiscal { get; set; } = TipoModalidadeDocumentoFiscal.Nfe;

        /// <summary>
        /// [serie] Retorna ou define a Série do Documento Fiscal. 
        /// Série Normal - 0-889
        /// Avulsa Fisco - 890-899
        /// SCAN - 900-999
        /// </summary>
        public int Serie { get; set; }

        /// <summary>
        /// [nNF] Retorna ou define o Número da Nota Fiscal Eletrônica.
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// [dhEmi] Retorna ou define a Data e Hora de emissão do Documento Fiscal
        /// </summary>
        public DateTime DataEmissao { get; set; }

        /// <summary>
        /// [dhSaiEnt] Retorna ou define a Data de Entrada/Saída da Mercadoria/Produto. Opcional.
        /// </summary>
        public DateTime? DataEntradaSaida { get; set; }

        /// <summary>
        /// [tpNF] Retorna ou define o Tipo da Nota Fiscal. Valor padrão 'Saida'.
        /// </summary>
        public TipoNotaFiscal TipoNotaFiscal { get; set; } = TipoNotaFiscal.Saida;

        /// <summary>
        /// [idDest] Retorna ou define o Identificador de Local de destino da operação.
        /// </summary>
        public TipoIdentificadorLocalDestinoOperacao IdentificadorLocalDestinoOperacao { get; set; }

        /// <summary>
        /// [cMunFG] Retorna ou define o Código do Município de Ocorrência do Fator Gerador.
        /// </summary>
        public int CodigoMunicipioFatoGerador { get; set; }

        /// <summary>
        /// [tpImp] Retorna ou define o formato de impressão do DANFE. 
        /// Valor padrão 'Retrato'.
        /// </summary>
        public TipoFormatoImpressaoDanfe TipoImpressao { get; set; } = TipoFormatoImpressaoDanfe.Retrato;

        /// <summary>
        /// [tpEmis] Retorna ou define o Tipo de Emissão da Nota Fiscal Eletrônica. Valor padrão 'Normal'.
        /// </summary>
        public TipoEmissaoNFe TipoEmissao { get; set; } = TipoEmissaoNFe.Normal;

        /// <summary>
        /// [tpAmb] Retorna ou define o valor indicando o ambiente do Documento Fiscal. 
        /// Valor padrão 'Producao'.
        /// </summary>
        public TipoAmbiente Ambiente { get; set; } = TipoAmbiente.Producao;

        /// <summary>
        /// [finNFe] Retorna ou define a Finalidade do Documento Fiscal. Valor padrão 'Normal'.
        /// </summary>
        public TipoFinalidade Finalidade { get; set; } = TipoFinalidade.Normal;

        /// <summary>
        /// [indFinal] Retorna ou define o valor indicando se a operação foi realizada com consumidor final.
        /// </summary>
        public bool OperacaoConsumidorFinal { get; set; }

        /// <summary>
        /// [indPres] Retorna ou define o indicador de presença do comprador no estabelecimento
        /// comercial no momento da operação.
        /// </summary>
        public TipoIndicadorPresencaComprador IndicadorPresencaComprador { get; set; }

        /// <summary>
        /// [procEmi] Retorna ou define o Tipo de processo de emissão da NF-e. Valor padrão 'AplicativoContribuinte'.
        /// </summary>
        public TipoProcessoEmissaoNFe TipoProcessoEmissao { get; set; } = TipoProcessoEmissaoNFe.AplicativoContribuinte;

        /// <summary>
        /// [verProc] Retorna ou define a Versão do Aplicativo de Emissão da NF-e.
        /// </summary>
        public string VersaoAplicativoEmissao { get; set; }

        /// <summary>
        /// [dhCont] Retorna ou define a Data e Hora de entrada em contigência. Apenas se o tipo de
        /// emissão for diferente de normal.
        /// </summary>
        public DateTime? DataHoraEntradaContingencia { get; set; }

        /// <summary>
        /// [xJust] Retorna ou define a Justificativa para entrada em contingência. Apenas se o tipo
        /// de emissão for diferente de normal.
        /// </summary>
        public string JustificativaEntradaContingencia { get; set; }

        /// <summary>
        /// [NFref] Retorna a lista de referências à Documentos Fiscais vinculadas ao Documento
        /// Fiscal atual.
        /// </summary>
        /// <remarks>
        /// Esta informação é utilizada nas hipóteses previstas na legislação (Devolução de
        /// Mercadorias, Substituição de NF cancelada, Complementação de NF, etc).
        /// </remarks>
        public List<IReferenciaDocumentoFiscal> ReferenciasDocumentoFiscais { get; } = new List<IReferenciaDocumentoFiscal>();

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("ide");

            writer.WriteElementString("cUF", UnidadeFederativaEmitente.GetEnumValue());
            writer.WriteElementString("cNF", CodigoNumerico.ToString("00000000"));
            writer.WriteElementString("natOp", NaturezaOperacao.ToToken());
            writer.WriteElementString("indPag", FormaPagamento.GetEnumValue());
            writer.WriteElementString("mod", ModalidadeDocumentoFiscal.GetEnumValue());
            writer.WriteElementString("serie", Serie.ToString());
            writer.WriteElementString("nNF", Numero.ToString());
            writer.WriteElementString("dhEmi", DataEmissao.ToTDateTimeUtc(nfe));

            if (DataEntradaSaida.HasValue)
            {
                writer.WriteElementString("dhSaiEnt", DataEntradaSaida.Value.ToTDateTimeUtc(nfe));
            }
            writer.WriteElementString("tpNF", TipoNotaFiscal.GetEnumValue());
            writer.WriteElementString("idDest", IdentificadorLocalDestinoOperacao.GetEnumValue());
            writer.WriteElementString("cMunFG", CodigoMunicipioFatoGerador.ToString7());
            writer.WriteElementString("tpImp", TipoImpressao.GetEnumValue());
            writer.WriteElementString("tpEmis", TipoEmissao.GetEnumValue());
            writer.WriteElementString("cDV", nfe.DigitoVerificadorChaveAcesso.ToString());
            writer.WriteElementString("tpAmb", Ambiente.GetEnumValue());
            writer.WriteElementString("finNFe", Finalidade.GetEnumValue());
            writer.WriteElementString("indFinal", OperacaoConsumidorFinal ? "1" : "0");
            writer.WriteElementString("indPres", IndicadorPresencaComprador.GetEnumValue());
            writer.WriteElementString("procEmi", TipoProcessoEmissao.GetEnumValue());
            writer.WriteElementString("verProc", VersaoAplicativoEmissao.ToToken(20));

            if (TipoEmissao != TipoEmissaoNFe.Normal)
            {
                writer.WriteElementString("dhCont", DataHoraEntradaContingencia.GetValueOrDefault().ToTDateTimeUtc(nfe));
                writer.WriteElementString("xJust", JustificativaEntradaContingencia);
            }

            if (ReferenciasDocumentoFiscais.Any())
            {
                writer.WriteStartElement("NFref");

                foreach (var referencia in ReferenciasDocumentoFiscais)
                {
                    ((ISerializavel)referencia).Serializar(writer, nfe);
                }

                writer.WriteEndElement();
            }
            
            writer.WriteEndElement();
        }
    }
}