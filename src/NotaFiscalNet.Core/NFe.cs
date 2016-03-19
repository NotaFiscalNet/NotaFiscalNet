using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using NotaFiscalNet.Core.Validacao.Validators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa uma Nota Fiscal Eletrônica.
    /// </summary>

    public sealed class NFe : ISerializavel
    {
        private readonly IdentificacaoNFe _ide = new IdentificacaoNFe();
        private readonly EmitenteNFe _emit = new EmitenteNFe();
        private readonly FiscoEmitenteNFe _avulsa = new FiscoEmitenteNFe();
        private readonly EnderecoEmpresa _retirada = new EnderecoEmpresa();
        private readonly EnderecoEmpresa _entrega = new EnderecoEmpresa();
        private readonly ProdutoCollection _itens = new ProdutoCollection();
        private readonly TotalNFe _totais = new TotalNFe();
        private readonly TransporteNFe _transporte = new TransporteNFe();
        private readonly CobrancaNFe _cobranca = new CobrancaNFe();
        private readonly PagamentoCollection _pagamentos = new PagamentoCollection();
        private readonly InformacoesAdicionaisNFe _informacoesAdicionais = new InformacoesAdicionaisNFe();
        private readonly InformacoesExportacao _informacoesExportacao = new InformacoesExportacao();
        private readonly Compra _compras = new Compra();
        private readonly AquisicaoCana _cana = new AquisicaoCana();
        private readonly AutorizacaoDownloadXmlCollection _autXml = new AutorizacaoDownloadXmlCollection();

        /// <summary>
        /// [versao] Retorna ou define a Versão do Leiaute NF-e. Ex. 2.0.4, 2.0.3, etc.
        /// </summary>
        [NFeField(FieldName = "versao", DataType = "TVerNFe", ID = "A02")]
        [ValidateField(1, true)]
        public string Versao => Constants.VersaoLeiaute;

        /// <summary>
        /// [Id] Retorna a Chave de Acesso da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(FieldName = "Id", DataType = "ID", ID = "A03")]
        [ValidateField(2, true)]
        public string ChaveAcesso
        {
            get
            {
                var ufEmitente = string.Empty;
                var aammEmissaoNFe = string.Empty;
                var cnpjEmitente = string.Empty;
                var mod = string.Empty; // modelo do documento fiscal
                var serie = string.Empty; // série do documento fiscal
                var nNF = string.Empty; // Númedo do documento fiscal
                var cNF = string.Empty; // Código numérico que compõe a chave de acesso
                var tpEmis = string.Empty; // Código da forma de emissão da NFe
                var cDV = string.Empty; // digito verificador da chave de acesso

                if (Emitente == null)
                    throw new InvalidOperationException("As informações do Emitente da Nota Fiscal Eletrônica não foram informadas.");

                if (Emitente.Endereco == null)
                    throw new InvalidOperationException("As informações de Endereço do Emitente da Nota Fiscal Eletrônica não foram informadas.");

                ufEmitente = ((int)Identificacao.UFEnvio).ToString("00"); // 2 digitos
                aammEmissaoNFe = Identificacao.DataEmissao.ToString("yyMM"); // 4 digitos
                cnpjEmitente = Emitente.CNPJ.PadLeft(14, '0'); // 14 digitos
                mod = ((int)Identificacao.CodigoModeloDocFiscal).ToString("00"); // 2 digitos
                serie = Identificacao.Serie.ToString("000"); // 3 digitos no formato (0|[1-9]{1}[0-9]{0,2}")
                nNF = Identificacao.NumeroNF.ToString("000000000"); // 9 digitos
                tpEmis = ((int)Identificacao.TipoEmissao).ToString("0");
                cNF = Identificacao.CodigoNF.ToString("00000000"); // 8 digitos

                // monta a chave de acesso da Nota Fiscal Eletrônica.
                string chave = string.Concat(ufEmitente, aammEmissaoNFe, cnpjEmitente, mod, serie, nNF, tpEmis, cNF);
                chave = string.Concat(chave, CalculoUtil.Modulo11(chave));

                return chave;
            }
        }

        /// <summary>
        /// [cDV] Retorna o Digito Verificador da Chave de Acesso da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(FieldName = "cDV", DataType = "token", ID = "B23", Pattern = "[0-9]{1}")]
        [ValidateField(3, true)]
        public int ChaveAcessoDV
        {
            get
            {
                string chaveAcesso = ChaveAcesso;
                char dv = chaveAcesso[chaveAcesso.Length - 1]; // o último caracterer será o Dígito verificador.
                return int.Parse(dv.ToString());
            }
        }

        /// <summary>
        /// Retorna a referência para o objeto contendo os dados de Identificação da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(FieldName = "ide", ID = "B01")]
        [ValidateField(4)]
        public IdentificacaoNFe Identificacao => _ide;

        /// <summary>
        /// Retorna as informações do Emitente da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(FieldName = "emit", ID = "C01")]
        [ValidateField(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public EmitenteNFe Emitente => _emit;

        /// <summary>
        /// Retorna as informações do Fisco emitente da Nota Fiscal Eletrônica Avulsa. <br/> Informar
        /// apenas no caso de emissão de Nota Fiscal Eletrônica Avulsa, pelo Fisco Emitente.
        /// </summary>
        [NFeField(FieldName = "avulsa", ID = "D01", Opcional = true)]
        [ValidateField(6, true)]
        public FiscoEmitenteNFe Avulsa => _avulsa;

        /// <summary>
        /// Retorna as informações do Destinatário da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(FieldName = "dest", ID = "E01"), ValidateField(7, ChaveErroValidacao.CampoNaoPreenchido)]
        public DestinatarioNFe Destinatario { get; set; }

        /// <summary>
        /// Retorna o Endereço de Retirada (endereço de retirada dos produtos) da Nota Fiscal
        /// Eletrônica. Informar apenas quando for diferente do Endereço do Remetente. Opcional.
        /// </summary>
        [NFeField(FieldName = "retirada", ID = "F01", Opcional = true)]
        [ValidateField(8, true)]
        public EnderecoEmpresa EnderecoRetirada => _retirada;

        /// <summary>
        /// Retorna o Endereço de Entrega (endereço de entrega dos produtos) da Nota Fiscal
        /// Eletrônica. Informar apenas quando for diferente do Endereço do Destinatário. Opcional.
        /// </summary>
        [NFeField(FieldName = "entrega", ID = "G01", Opcional = true)]
        [ValidateField(9, true)]
        public EnderecoEmpresa EnderecoEntrega => _entrega;

        /// <summary>
        /// [autXML] Retorna a lista de Autorizações de Download do XML.
        /// </summary>
        [NFeField(FieldName = "autXML", ID = "G50", Opcional = true)]
        [ValidateField(10, true)]
        public AutorizacaoDownloadXmlCollection AutorizacoesDownloadXml => _autXml;

        /// <summary>
        /// Retorna a lista de Itens (Produtos ou Serviços) da Nota Fiscal Eletrônica.
        /// </summary>
        /// <remarks>A lista pode conter até no máximo 990 itens.</remarks>
        [NFeField(FieldName = "det", ID = "H01", MinLength = 1, MaxLength = 990)]
        [ValidateField(11, ChaveErroValidacao.CampoNaoPreenchido, Validator = typeof(RangeCollectionValidator), MinLength = 1, MaxLength = 990)]
        public ProdutoCollection Itens => _itens;

        /// <summary>
        /// Retorna as Informações de Totalização da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(FieldName = "total", ID = "W01")]
        [ValidateField(12, ChaveErroValidacao.CampoNaoPreenchido)]
        public TotalNFe Totais => _totais;

        /// <summary>
        /// Retorna as Informações de Transporte da Nota Fiscal Eletrônica.
        /// </summary>
        [NFeField(FieldName = "transp", ID = "X01")]
        [ValidateField(13, ChaveErroValidacao.CampoNaoPreenchido)]
        public TransporteNFe Transporte => _transporte;

        /// <summary>
        /// Retorna as Informações de Cobrança da Nota Fiscal Eletrônica. Opcional.
        /// </summary>
        [NFeField(FieldName = "cobr", ID = "Y01", Opcional = true)]
        [ValidateField(14, true)]
        public CobrancaNFe Cobranca => _cobranca;

        [NFeField(FieldName = "pag", ID = "YA01", Opcional = true)]
        [ValidateField(15, true)]
        public PagamentoCollection Pagamentos => _pagamentos;

        /// <summary>
        /// Retorna as Informações Adicionais da Nota Fiscal Eletrônica. Opcional.
        /// </summary>
        [NFeField(FieldName = "infAdic", ID = "Z01", Opcional = true)]
        [ValidateField(16, true)]
        public InformacoesAdicionaisNFe InformacoesAdicionais => _informacoesAdicionais;

        /// <summary>
        /// Retorna as Informações de Exportação da Nota Fiscal Eletrônica. Opcional.
        /// </summary>
        [NFeField(FieldName = "exporta", ID = "ZA01", Opcional = true)]
        [ValidateField(17, true)]
        public InformacoesExportacao Exportacao => _informacoesExportacao;

        /// <summary>
        /// Retorna as Informações de Compras (Notas de Empenho, Pedido e Contrato) da Nota Fiscal
        /// Eletrônica. Opcional.
        /// </summary>
        [NFeField(FieldName = "compra", ID = "ZA01", Opcional = true)]
        [ValidateField(18, true)]
        public Compra Compras => _compras;

        /// <summary>
        /// Retorna ou define o Hash da NFe.
        /// </summary>
        public string DigestValue { get; set; }

        /// <summary>
        /// Retorna o valor referente ao registro de aquisições de cana.
        /// </summary>
        public AquisicaoCana AquisicoesCana => _cana;

        void ISerializavel.Serializar(XmlWriter writer, NFe nfe)
        {
            writer.WriteStartElement("NFe", Constants.NamespacePortalFiscalNFe);

            writer.WriteStartElement("infNFe"); // elemento infNFe
            writer.WriteAttributeString("versao", Versao);
            writer.WriteAttributeString("Id", "NFe" + ChaveAcesso);

            RenderEntity(Identificacao, writer); // ide
            RenderEntity(Emitente, writer); // emit
            RenderAvulsa(writer); // avulsa

            RenderEntity(Destinatario, writer); // dest
            RenderEnderecoRetirada(writer); // retirada
            RenderEnderecoEntrega(writer); // entrega
            RenderAutorizacoesDownloadXml(writer); // autXML
            RenderEntity(Itens, writer); // det
            RenderEntity(Totais, writer); // total
            RenderEntity(Transporte, writer); // transp
            RenderCobranca(writer); // cobr
            RenderPagamentos(writer); // pag
            RenderInformacoesAdicionais(writer); // infAdic
            RenderExportacao(writer); // exporta
            RenderCompras(writer); // compra
            RenderCana(writer); // cana

            writer.WriteEndElement(); // fim do elemento 'infNFe'
            writer.WriteEndElement(); // fim do elemnto 'NFe'
        }

        /// <summary>
        /// Renderização as informações Adicionais
        /// </summary>
        private void RenderInformacoesAdicionais(XmlWriter writer)
        {
            if (InformacoesAdicionais.Modificado)
                RenderEntity(InformacoesAdicionais, writer);
        }

        /// <summary>
        /// Renderização as informações de Cobrança
        /// </summary>
        private void RenderCobranca(XmlWriter writer)
        {
            if (Cobranca.Modificado)
                RenderEntity(Cobranca, writer);
        }

        /// <summary>
        /// Renderiza as formas de pagamento de uma NFC-e.
        /// </summary>
        /// <param name="writer"></param>
        private void RenderPagamentos(XmlWriter writer)
        {
            if (Identificacao.CodigoModeloDocFiscal == TipoModalidadeDocumentoFiscal.NFCe && Pagamentos.Count > 0)
                RenderEntity(Pagamentos, writer);
        }

        /// <summary>
        /// Renderização as informações de Exportação
        /// </summary>
        private void RenderExportacao(XmlWriter writer)
        {
            if (Exportacao.Modificado)
                RenderEntity(Exportacao, writer);
        }

        /// <summary>
        /// Renderização as informações de Compra
        /// </summary>
        private void RenderCompras(XmlWriter writer)
        {
            if (Compras.Modificado)
                RenderEntity(Compras, writer);
        }

        /// <summary>
        /// Renderização as informações de Aquisição de Cana.
        /// </summary>
        private void RenderCana(XmlWriter writer)
        {
            if (AquisicoesCana.Modificado)
                RenderEntity(AquisicoesCana, writer);
        }

        /// <summary>
        /// Renderização as informações do Fisco do Emitente da Nota Fiscal Eletrônica
        /// </summary>
        private void RenderAvulsa(XmlWriter writer)
        {
            if (Avulsa.Modificado)
                RenderEntity(Avulsa, writer);
        }

        /// <summary>
        /// Renderização as informações de endereço de retirada.
        /// </summary>
        /// <param name="writer"></param>
        private void RenderEnderecoRetirada(XmlWriter writer)
        {
            if (!EnderecoRetirada.Modificado)
                return;

            writer.WriteStartElement("retirada");
            RenderEntity(EnderecoRetirada, writer);
            writer.WriteEndElement(); // fim do elemento 'retirada'
        }

        /// <summary>
        /// Renderiza as informações de endereço de entrega.
        /// </summary>
        /// <param name="writer"></param>
        private void RenderEnderecoEntrega(XmlWriter writer)
        {
            if (!EnderecoEntrega.Modificado)
                return;
            writer.WriteStartElement("entrega");
            RenderEntity(EnderecoEntrega, writer);
            writer.WriteEndElement(); // fim do elemento 'entrega'
        }

        private void RenderAutorizacoesDownloadXml(XmlWriter writer)
        {
            if (AutorizacoesDownloadXml.Count == 0)
                return;

            foreach (var autXml in AutorizacoesDownloadXml)
                RenderEntity(autXml, writer);
        }

        private void RenderEntity(ISerializavel entity, XmlWriter writer)
        {
            if (entity == null) return;

            entity.Serializar(writer, this);
        }

        private void Deserialize(XDocument doc, bool ignoreSchemaValidation = false)
        {
            Deserialize(doc.Root);
        }

        private void Deserialize(XElement nfeEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;

            var infNFeEl = nfeEl.Element(ns + "infNFe");

            ParseIdentificacao(infNFeEl.Element(ns + "ide"));
            ParseEmitente(infNFeEl.Element(ns + "emit"));

            var avulsaEl = infNFeEl.Element(ns + "avulsa");
            if (avulsaEl != null)
                ParseAvulsa(avulsaEl);

            var destEl = infNFeEl.Element(ns + "dest");
            if (destEl != null)
                ParseDestinatario(destEl);

            var retiradaEl = infNFeEl.Element(ns + "retirada");
            if (retiradaEl != null)
                ParseRetirada(retiradaEl);

            var entregaEl = infNFeEl.Element(ns + "entrega");
            if (entregaEl != null)
                ParseEntrega(entregaEl);

            var autXmlEls = infNFeEl.Elements(ns + "autXML");
            ParseAutXML(autXmlEls);

            var detEls = infNFeEl.Elements(ns + "det");
            ParseDet(detEls);

            var totalEl = infNFeEl.Element(ns + "total");
            ParseTotal(totalEl);

            var transpEl = infNFeEl.Element(ns + "transp");
            ParseTransp(transpEl);

            var cobrEl = infNFeEl.Element(ns + "cobr");
            if (cobrEl != null)
                ParseCobr(cobrEl);

            ParsePag(infNFeEl.Elements(ns + "pag"));

            var infAdicEl = infNFeEl.Element(ns + "infAdic");
            if (infAdicEl != null)
                ParseInfAdic(infAdicEl);

            var exportaEl = infNFeEl.Element(ns + "exporta");
            if (exportaEl != null)
                ParseExporta(exportaEl);

            var compraEl = infNFeEl.Element(ns + "compra");
            if (compraEl != null)
                ParseCompra(compraEl);

            var canaEl = infNFeEl.Element(ns + "cana");
            if (canaEl != null)
                ParseCana(canaEl);

            var nsXmldsig = Constants.XNamespaceXmldsig;
            var digestValue = nfeEl
                .Element(nsXmldsig + "Signature")
                .Element(nsXmldsig + "SignedInfo")
                .Element(nsXmldsig + "Reference")
                .Element(nsXmldsig + "DigestValue").Value;

            DigestValue = digestValue;
        }

        private void ParseCana(XElement canaEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var cana = AquisicoesCana;

            canaEl.NFElementAsString("safra", value => cana.Safra = value);
            canaEl.NFElementAsString("ref", value => cana.Referencia = DateTime.ParseExact(value, "MM/yyyy", CultureInfo.CurrentCulture));

            foreach (var forDiaEl in canaEl.Elements(ns + "forDia"))
            {
                var forDia = new FornecimentoDiarioCana();
                forDiaEl.NFAttributeAsInt32("dia", value => forDia.Dia = value);
                forDiaEl.NFElementAsDecimal("qtde", value => forDia.Quantidade = value);

                cana.FornecimentosDiarios.Add(forDia);
            }

            canaEl.NFElementAsDecimal("qTotMes", value => cana.QuantidadeTotalMes = value);
            canaEl.NFElementAsDecimal("qTotAnt", value => cana.QuantidadeTotalAnterior = value);
            canaEl.NFElementAsDecimal("qTotGer", value => cana.QuantidadeTotalGeral = value);

            foreach (var deducEl in canaEl.Elements(ns + "deduc"))
            {
                var deduc = new DeducaoCana();
                deducEl.NFAttributeAsString("xDed", value => deduc.Descricao = value);
                deducEl.NFElementAsDecimal("vDed", value => deduc.Valor = value);

                cana.Deducoes.Add(deduc);
            }

            canaEl.NFElementAsDecimal("vFor", value => cana.ValorFornecimentos = value);
            canaEl.NFElementAsDecimal("vTotDed", value => cana.ValorTotalDeducoes = value);
            canaEl.NFElementAsDecimal("vLiqFor", value => cana.ValorLiquidoFornecimentos = value);
        }

        private void ParseCompra(XElement compraEl)
        {
            compraEl.NFElementAsString("xNEmp", value => Compras.NotaEmpenho = value);
            compraEl.NFElementAsString("xPed", value => Compras.Pedido = value);
            compraEl.NFElementAsString("xCont", value => Compras.Contrato = value);
        }

        private void ParseExporta(XElement exportaEl)
        {
            exportaEl.NFElementAsEnum<SiglaUF>("UFSaidaPais", value => Exportacao.UFSaidaPais = value);
            exportaEl.NFElementAsString("xLocExporta", value => Exportacao.LocalEmbarque = value);
            exportaEl.NFElementAsString("xLocDespacho", value => Exportacao.LocalDespacho = value);
        }

        private void ParseInfAdic(XElement infAdicEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;

            infAdicEl.NFElementAsString("infAdFisco", value => InformacoesAdicionais.InformacoesComplementaresFisco = value);
            infAdicEl.NFElementAsString("infCpl", value => InformacoesAdicionais.InformacoesComplementaresContribuinte = value);

            // obsCont
            foreach (var obsContEl in infAdicEl.Elements(ns + "obsCont"))
            {
                var obsCont = new ObservacaoContribuinte();
                obsContEl.NFAttributeAsString("xCampo", value => obsCont.Campo = value);
                obsContEl.NFElementAsString("xTexto", value => obsCont.Texto = value);

                InformacoesAdicionais.ObservacoesContribuinte.Add(obsCont);
            }

            // obsFisco
            foreach (var obsFiscoEl in infAdicEl.Elements(ns + "obsFisco"))
            {
                var obsFisco = new ObservacaoFisco();
                obsFiscoEl.NFAttributeAsString("xCampo", value => obsFisco.Campo = value);
                obsFiscoEl.NFElementAsString("xTexto", value => obsFisco.Texto = value);

                InformacoesAdicionais.ObservacoesFisco.Add(obsFisco);
            }

            // procRef
            foreach (var procRefEl in infAdicEl.Elements(ns + "procRef"))
            {
                var procRef = new Processo();
                procRefEl.NFElementAsString("nProc", value => procRef.Identificador = value);
                procRefEl.NFElementAsEnum<OrigemProcesso>("indProc", value => procRef.OrigemProcesso = value);

                InformacoesAdicionais.Processos.Add(procRef);
            }
        }

        private void ParsePag(IEnumerable<XElement> pagEls)
        {
            foreach (var pagEl in pagEls)
            {
                var pag = new Pagamento();
                pagEl.NFElementAsEnum<TipoPagamento>("tPag", value => pag.TipoPagamento = value);
                pagEl.NFElementAsDecimal("vPag", value => pag.ValorPagamento = value);

                var cardEl = pagEl.Element(Constants.XNamespacePortalFiscalNFe + "card");
                if (cardEl != null)
                {
                    cardEl.NFElementAsString("CNPJ", value => pag.DetalhesOperacaoCartao.CNPJ = value);
                    cardEl.NFElementAsEnum<TipoBandeiraCartao>("tBand", value => pag.DetalhesOperacaoCartao.TipoBandeira = value);
                    cardEl.NFElementAsString("cAut", value => pag.DetalhesOperacaoCartao.CodigoAutorizacao = value);
                }

                Pagamentos.Add(pag);
            }
        }

        private void ParseCobr(XElement cobrEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var cobr = Cobranca;

            var fatEl = cobrEl.Element(ns + "fat");
            if (fatEl != null)
            {
                fatEl.NFElementAsString("nFat", value => cobr.Fatura.Numero = value);
                fatEl.NFElementAsDecimal("vOrig", value => cobr.Fatura.ValorOriginal = value);
                fatEl.NFElementAsDecimal("vDesc", value => cobr.Fatura.ValorDesconto = value);
                fatEl.NFElementAsDecimal("vLiq", value => cobr.Fatura.ValorLiquido = value);
            }

            foreach (var dupEl in cobrEl.Elements(ns + "dup"))
            {
                var dup = new Duplicata();
                dupEl.NFElementAsString("nDup", value => dup.Numero = value);
                dupEl.NFElementAsDateTime("dVenc", value => dup.DataVencimento = value);
                dupEl.NFElementAsDecimal("vDup", value => dup.Valor = value);

                cobr.Duplicatas.Add(dup);
            }
        }

        private void ParseTransp(XElement transpEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var transp = Transporte;

            transpEl.NFElementAsEnum<TipoModalidadeFrete>("modFrete", value => transp.ModalidadeFrete = value);

            // transporta
            var transportaEl = transpEl.Element(ns + "transporta");
            if (transportaEl != null)
                ParseTranspTransporta(transportaEl);

            // retTransp
            var retTranspEl = transpEl.Element(ns + "retTransp");
            if (retTranspEl != null)
                ParseTranspRetTransp(retTranspEl);

            // veicTransp
            var veicTranspEl = transpEl.Element(ns + "veicTransp");
            if (veicTranspEl != null)
                ParseTranspVeicTransp(veicTranspEl);

            // reboque
            ParseTranspReboque(transpEl.Elements(ns + "reboque"));

            transpEl.NFElementAsString("vagao", value => transp.Vagao = value);
            transpEl.NFElementAsString("balsa", value => transp.Balsa = value);

            // vol
            ParseTranspVol(transpEl.Elements(ns + "vol"));
        }

        private void ParseTranspVol(IEnumerable<XElement> volEls)
        {
            foreach (var volEl in volEls)
            {
                var vol = new VolumeCarga();
                volEl.NFElementAsInt64("qVol", value => vol.QuantidadeVolumes = value);
                volEl.NFElementAsString("esp", value => vol.Especie = value);
                volEl.NFElementAsString("marca", value => vol.Marca = value);
                volEl.NFElementAsString("nVol", value => vol.Numeracao = value);
                volEl.NFElementAsDecimal("pesoL", value => vol.PesoLiquido = value);
                volEl.NFElementAsDecimal("pesoB", value => vol.PesoBruto = value);

                foreach (var lacreEl in volEl.Elements(Constants.XNamespacePortalFiscalNFe + "lacres"))
                    vol.Lacres.Add(lacreEl.Value);

                Transporte.VolumesCarga.Add(vol);
            }
        }

        private void ParseTranspReboque(IEnumerable<XElement> reboqueEls)
        {
            foreach (var reboqueEl in reboqueEls)
            {
                var veicTransp = new VeiculoTransporte();
                reboqueEl.NFElementAsString("placa", value => veicTransp.Placa = value);
                reboqueEl.NFElementAsEnum<SiglaUF>("UF", value => veicTransp.UF = value);
                reboqueEl.NFElementAsString("RNTC", value => veicTransp.RNTC = value);

                Transporte.Reboques.Add(veicTransp);
            }
        }

        private void ParseTranspVeicTransp(XElement veicTranspEl)
        {
            var veicTransp = Transporte.VeiculoTransporte;
            veicTranspEl.NFElementAsString("placa", value => veicTransp.Placa = value);
            veicTranspEl.NFElementAsEnum<SiglaUF>("UF", value => veicTransp.UF = value);
            veicTranspEl.NFElementAsString("RNTC", value => veicTransp.RNTC = value);
        }

        private void ParseTranspRetTransp(XElement retTranspEl)
        {
            var retTransp = Transporte.RetencaoICMS;
            retTranspEl.NFElementAsDecimal("vServ", value => retTransp.ValorServico = value);
            retTranspEl.NFElementAsDecimal("vBCRet", value => retTransp.BaseCalculoRetencao = value);
            retTranspEl.NFElementAsDecimal("pICMSRet", value => retTransp.AliquotaRetencao = value);
            retTranspEl.NFElementAsDecimal("vICMSRet", value => retTransp.ValorICMSRetido = value);
            retTranspEl.NFElementAsInt32("CFOP", value => retTransp.CFOP = value);
            retTranspEl.NFElementAsInt32("cMunFG", value => retTransp.CodigoMunicipioFatorGerador = value);
        }

        private void ParseTranspTransporta(XElement transportaEl)
        {
            var transporta = Transporte.Transportador;
            transportaEl.NFElementAsString("CNPJ", value => transporta.CNPJ = value);
            transportaEl.NFElementAsString("CPF", value => transporta.CPF = value);
            transportaEl.NFElementAsString("xNome", value => transporta.Nome = value);
            transportaEl.NFElementAsString("IE", value => transporta.InscricaoEstadual = value);
            transportaEl.NFElementAsString("xEnder", value => transporta.EnderecoCompleto = value);
            transportaEl.NFElementAsString("xMun", value => transporta.Municipio = value);
            transportaEl.NFElementAsEnum<SiglaUF>("UF", value => transporta.UF = value);
        }

        private void ParseTotal(XElement totalEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;

            ParseTotalIcms(totalEl.Element(ns + "ICMSTot"));

            var issqnTotEl = totalEl.Element(ns + "ISSQNtot");
            if (issqnTotEl != null)
                ParseTotalIssqn(issqnTotEl);

            var retTribEl = totalEl.Element(ns + "retTrib");
            if (retTribEl != null)
                ParseTotalRetTrib(retTribEl);
        }

        private void ParseTotalRetTrib(XElement retTribEl)
        {
            var retTrib = Totais.RetencaoTributosFederais;

            retTribEl.NFElementAsDecimal("vRetPIS", value => retTrib.ValorRetidoPIS = value);
            retTribEl.NFElementAsDecimal("vRetCOFINS", value => retTrib.ValorRetidoCOFINS = value);
            retTribEl.NFElementAsDecimal("vRetCSLL", value => retTrib.ValorRetidoCSLL = value);
            retTribEl.NFElementAsDecimal("vBCIRRF", value => retTrib.BaseCalculo = value);
            retTribEl.NFElementAsDecimal("vIRRF", value => retTrib.ValorRetidoIRRF = value);
            retTribEl.NFElementAsDecimal("vBCRetPrev", value => retTrib.BaseCalculoRetencaoPrevidenciaSocial = value);
            retTribEl.NFElementAsDecimal("vRetPrev", value => retTrib.ValorRetencaoPrevidenciaSocial = value);
        }

        private void ParseTotalIssqn(XElement issqnTotEl)
        {
            var issqnTot = Totais.ISSQN;

            issqnTotEl.NFElementAsDecimal("vServ", value => issqnTot.ValorTotalServicos = value);
            issqnTotEl.NFElementAsDecimal("vBC", value => issqnTot.BaseCalculo = value);
            issqnTotEl.NFElementAsDecimal("vISS", value => issqnTot.ValorTotalISS = value);
            issqnTotEl.NFElementAsDecimal("vPIS", value => issqnTot.ValorPIS = value);
            issqnTotEl.NFElementAsDecimal("vCOFINS", value => issqnTot.ValorCOFINS = value);

            issqnTotEl.NFElementAsDateTime("dCompet", value => issqnTot.DataCompetencia = value);
            issqnTotEl.NFElementAsDecimal("vDeducao", value => issqnTot.ValorDeducao = value);
            issqnTotEl.NFElementAsDecimal("vOutro", value => issqnTot.ValorOutrasRetencoes = value);
            issqnTotEl.NFElementAsDecimal("vDescIncond", value => issqnTot.ValorDescontoIncondicionado = value);
            issqnTotEl.NFElementAsDecimal("vDescCond", value => issqnTot.ValorDescontoCondicionado = value);
            issqnTotEl.NFElementAsDecimal("vISSRet", value => issqnTot.ValorIssRetido = value);
            issqnTotEl.NFElementAsInt32("cRegTrib", value => issqnTot.CodigoRegimeTributacao = value);
        }

        private void ParseTotalIcms(XElement icmsTotEl)
        {
            var icmsTot = Totais.ICMS;

            icmsTotEl.NFElementAsDecimal("vBC", value => icmsTot.BaseCalculo = value);
            icmsTotEl.NFElementAsDecimal("vICMS", value => icmsTot.ValorTotalICMS = value);
            icmsTotEl.NFElementAsDecimal("vICMSDeson", value => icmsTot.ValorTotalICMSDesonerado = value);
            icmsTotEl.NFElementAsDecimal("vBCST", value => icmsTot.BaseCalculoST = value);
            icmsTotEl.NFElementAsDecimal("vST", value => icmsTot.ValorTotalICMSST = value);
            icmsTotEl.NFElementAsDecimal("vProd", value => icmsTot.ValorTotalProdutos = value);
            icmsTotEl.NFElementAsDecimal("vFrete", value => icmsTot.ValorTotalFrete = value);
            icmsTotEl.NFElementAsDecimal("vSeg", value => icmsTot.ValorTotalSeguro = value);
            icmsTotEl.NFElementAsDecimal("vDesc", value => icmsTot.ValorTotalDesconto = value);
            icmsTotEl.NFElementAsDecimal("vII", value => icmsTot.ValorTotalII = value);
            icmsTotEl.NFElementAsDecimal("vIPI", value => icmsTot.ValorTotalIPI = value);
            icmsTotEl.NFElementAsDecimal("vPIS", value => icmsTot.ValorTotalPIS = value);
            icmsTotEl.NFElementAsDecimal("vCOFINS", value => icmsTot.ValorTotalCOFINS = value);
            icmsTotEl.NFElementAsDecimal("vOutro", value => icmsTot.ValorOutrasDespesas = value);
            icmsTotEl.NFElementAsDecimal("vNF", value => icmsTot.ValorTotalNFe = value);
            icmsTotEl.NFElementAsDecimal("vTotTrib", value => icmsTot.ValorTotalTributos = value);
        }

        private void ParseAutXML(IEnumerable<XElement> autXmlEls)
        {
            foreach (var autXml in autXmlEls)
            {
                // pega o primeiro elemento filho da tag 'autXML' (CNPJ ou CPF).
                var cpfCnpj = autXml.Elements().First().Value;
                var autorizacao = new AutorizacaoDownloadXml(cpfCnpj);
                AutorizacoesDownloadXml.Add(autorizacao);
            }
        }

        private void ParseDet(IEnumerable<XElement> detEls)
        {
            foreach (var detEl in detEls)
            {
                var produto = new Produto();
                produto.NumeroItem = int.Parse(detEl.Attribute("nItem").Value);

                // prod
                ParseDetProduto(detEl, produto);

                // imposto
                var impostoEl = detEl.Element(Constants.XNamespacePortalFiscalNFe + "imposto");
                ParseDetImposto(impostoEl, produto);

                Itens.Add(produto);
            }
        }

        private void ParseDetImposto(XElement impostoEl, Produto produto)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            impostoEl.NFElementAsDecimal("vTotTrib", value => produto.Imposto.ValorTotalTributos = value);

            var issqnEl = impostoEl.Element(ns + "ISSQN");
            if (issqnEl != null)
            {
                ParseDetImpostoIssqn(issqnEl, produto);
            }
            else
            {
                // ICMS
                var icmsEl = impostoEl.Element(ns + "ICMS");
                ParseDetImpostoIcms(icmsEl, produto);

                // IPI
                var ipiEl = impostoEl.Element(ns + "IPI");
                if (ipiEl != null)
                    ParseDetImpostoIpi(ipiEl, produto);

                // II - Imposto de Importação
                var iiEl = impostoEl.Element(ns + "II");
                if (iiEl != null)
                    ParseDetImpostoII(iiEl, produto);

                // PIS
                var pisEl = impostoEl.Element(ns + "PIS");
                if (pisEl != null)
                    ParseDetImpostoPis(pisEl, produto);

                // PISST (PIS Substituição Tributária)
                var pisNtEl = impostoEl.Element(ns + "PISST");
                if (pisNtEl != null)
                    ParseDetImpostoPisST(pisNtEl, produto);

                // COFINS
                var cofinsEl = impostoEl.Element(ns + "COFINS");
                if (cofinsEl != null)
                    ParseDetImpostoCofins(cofinsEl, produto);

                // COFINSNT (COFINS Substituição Tributária)
                var cofinsStEl = impostoEl.Element(ns + "COFINSST");
                if (cofinsStEl != null)
                    ParseDetImpostoCofinsST(cofinsStEl, produto);

                impostoEl.NFElementAsString("infAdProd", value => produto.InformacoesAdicionais = value);
            }
        }

        private void ParseDetImpostoCofinsST(XElement cofinsStEl, Produto produto)
        {
            var cofinsST = produto.Imposto.COFINSST;

            if (cofinsStEl.Element(Constants.XNamespacePortalFiscalNFe + "vBC") != null)
            {
                cofinsST.TipoCalculo = TipoCalculoCOFINS.PercentualValor;
                cofinsStEl.NFElementAsDecimal("vBC", value => cofinsST.BaseCalculo = value);
                cofinsStEl.NFElementAsDecimal("pCOFINS", value => cofinsST.Aliquota = value);
            }
            else
            {
                cofinsST.TipoCalculo = TipoCalculoCOFINS.ValorQuantidade;
                cofinsStEl.NFElementAsDecimal("qBCProd", value => cofinsST.BaseCalculo = value);
                cofinsStEl.NFElementAsDecimal("vAliqProd", value => cofinsST.BaseCalculo = value);
            }
            cofinsStEl.NFElementAsDecimal("vCOFINS", value => cofinsST.BaseCalculo = value);
        }

        private void ParseDetImpostoCofins(XElement cofinsEl, Produto produto)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var cofins = produto.Imposto.COFINS;

            // COFINSAliq
            var cofinsAliqEl = cofinsEl.Element(ns + "COFINSAliq");
            if (cofinsAliqEl != null)
            {
                cofins.TipoCalculo = TipoCalculoCOFINS.PercentualValor;

                cofinsAliqEl.NFElementAsEnum<SituacaoTributariaCOFINS>("CST", value => cofins.SituacaoTributaria = value);
                cofinsAliqEl.NFElementAsDecimal("vBC", value => cofins.BaseCalculo = value);
                cofinsAliqEl.NFElementAsDecimal("pCOFINS", value => cofins.Aliquota = value);
                cofinsAliqEl.NFElementAsDecimal("vPIS", value => cofins.Valor = value);
            }

            // COFINSQtde
            var cofinsQtdeEl = cofinsEl.Element(ns + "COFINSQtde");
            if (cofinsQtdeEl != null)
            {
                cofins.TipoCalculo = TipoCalculoCOFINS.ValorQuantidade;

                cofinsQtdeEl.NFElementAsEnum<SituacaoTributariaCOFINS>("CST", value => cofins.SituacaoTributaria = value);
                cofinsQtdeEl.NFElementAsDecimal("qBCProd", value => cofins.BaseCalculo = value);
                cofinsQtdeEl.NFElementAsDecimal("vAliqProd", value => cofins.Aliquota = value);
                cofinsQtdeEl.NFElementAsDecimal("vCOFINS", value => cofins.Valor = value);
            }

            // COFINSNT
            var cofinsNtEl = cofinsEl.Element(ns + "COFINSNT");
            if (cofinsNtEl != null)
                cofinsNtEl.NFElementAsEnum<SituacaoTributariaCOFINS>("CST", value => cofins.SituacaoTributaria = value);

            // COFINSOutr
            var cofinsOutrEl = cofinsEl.Element(ns + "COFINSOutr");
            if (cofinsOutrEl != null)
            {
                cofinsOutrEl.NFElementAsEnum<SituacaoTributariaCOFINS>("CST", value => cofins.SituacaoTributaria = value);

                if (cofinsOutrEl.Element(ns + "vBC") != null)
                {
                    cofins.TipoCalculo = TipoCalculoCOFINS.PercentualValor;
                    cofinsOutrEl.NFElementAsDecimal("vBC", value => cofins.BaseCalculo = value);
                    cofinsOutrEl.NFElementAsDecimal("pCOFINS", value => cofins.Aliquota = value);
                }
                else
                {
                    cofins.TipoCalculo = TipoCalculoCOFINS.ValorQuantidade;
                    cofinsOutrEl.NFElementAsDecimal("qBCProd", value => cofins.BaseCalculo = value);
                    cofinsOutrEl.NFElementAsDecimal("vAliqProd", value => cofins.Aliquota = value);
                }

                cofinsOutrEl.NFElementAsDecimal("vCOFINS", value => cofins.Valor = value);
            }
        }

        private void ParseDetImpostoPisST(XElement pisStEl, Produto produto)
        {
            var ipiST = produto.Imposto.PISST;

            if (pisStEl.Element(Constants.XNamespacePortalFiscalNFe + "vBC") != null)
            {
                ipiST.TipoCalculo = TipoCalculoPIS.PercentualValor;
                pisStEl.NFElementAsDecimal("vBC", value => ipiST.BaseCalculo = value);
                pisStEl.NFElementAsDecimal("pPIS", value => ipiST.Aliquota = value);
            }
            else
            {
                ipiST.TipoCalculo = TipoCalculoPIS.ValorQuantidade;
                pisStEl.NFElementAsDecimal("qBCProd", value => ipiST.BaseCalculo = value);
                pisStEl.NFElementAsDecimal("vAliqProd", value => ipiST.BaseCalculo = value);
            }
            pisStEl.NFElementAsDecimal("vPIS", value => ipiST.BaseCalculo = value);
        }

        private void ParseDetImpostoPis(XElement pisEl, Produto produto)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var pis = produto.Imposto.PIS;

            // PISAliq
            var pisAliqEl = pisEl.Element(ns + "PISAliq");
            if (pisAliqEl != null)
            {
                pis.TipoCalculo = TipoCalculoPIS.PercentualValor;

                pisAliqEl.NFElementAsEnum<SituacaoTributariaPIS>("CST", value => pis.SituacaoTributaria = value);
                pisAliqEl.NFElementAsDecimal("vBC", value => pis.BaseCalculo = value);
                pisAliqEl.NFElementAsDecimal("pPIS", value => pis.Aliquota = value);
                pisAliqEl.NFElementAsDecimal("vPIS", value => pis.Valor = value);
            }

            // PISQtde
            var pisQtdeEl = pisEl.Element(ns + "PISQtde");
            if (pisQtdeEl != null)
            {
                pis.TipoCalculo = TipoCalculoPIS.ValorQuantidade;

                pisQtdeEl.NFElementAsEnum<SituacaoTributariaPIS>("CST", value => pis.SituacaoTributaria = value);
                pisQtdeEl.NFElementAsDecimal("qBCProd", value => pis.BaseCalculo = value);
                pisQtdeEl.NFElementAsDecimal("vAliqProd", value => pis.Aliquota = value);
                pisQtdeEl.NFElementAsDecimal("vPIS", value => pis.Valor = value);
            }

            // PISNT
            var pisNtEl = pisEl.Element(ns + "PISNT");
            if (pisNtEl != null)
                pisNtEl.NFElementAsEnum<SituacaoTributariaPIS>("CST", value => pis.SituacaoTributaria = value);

            // PISOutr
            var pisOutrEl = pisEl.Element(ns + "PISOutr");
            if (pisOutrEl != null)
            {
                pisOutrEl.NFElementAsEnum<SituacaoTributariaPIS>("CST", value => pis.SituacaoTributaria = value);

                if (pisOutrEl.Element(ns + "vBC") != null)
                {
                    pis.TipoCalculo = TipoCalculoPIS.PercentualValor;
                    pisOutrEl.NFElementAsDecimal("vBC", value => pis.BaseCalculo = value);
                    pisOutrEl.NFElementAsDecimal("pPIS", value => pis.Aliquota = value);
                }
                else
                {
                    pis.TipoCalculo = TipoCalculoPIS.ValorQuantidade;
                    pisOutrEl.NFElementAsDecimal("qBCProd", value => pis.BaseCalculo = value);
                    pisOutrEl.NFElementAsDecimal("vAliqProd", value => pis.Aliquota = value);
                }

                pisOutrEl.NFElementAsDecimal("vPIS", value => pis.Valor = value);
            }
        }

        private void ParseDetImpostoII(XElement iiEl, Produto produto)
        {
            var ii = produto.Imposto.II;

            iiEl.NFElementAsDecimal("vBC", value => ii.BaseCalculo = value);
            iiEl.NFElementAsDecimal("vDespAdu", value => ii.ValorDespesasAduaneiras = value);
            iiEl.NFElementAsDecimal("vII", value => ii.ValorII = value);
            iiEl.NFElementAsDecimal("vIOF", value => ii.ValorIOF = value);
        }

        private void ParseDetImpostoIpi(XElement ipiEl, Produto produto)
        {
            var ipi = produto.Imposto.IPI;

            ipiEl.NFElementAsString("clEnq", value => ipi.ClasseIPICigarroBebida = value);
            ipiEl.NFElementAsString("CNPJProd", value => ipi.CNPJProdutor = value);
            ipiEl.NFElementAsString("cSelo", value => ipi.CodigoSeloControle = value);
            ipiEl.NFElementAsInt32("qSelo", value => ipi.QuantidadeSeloControle = value);
            ipiEl.NFElementAsString("cEnq", value => ipi.CodigoEnquadramentoLegal = value);

            var ns = Constants.XNamespacePortalFiscalNFe;

            var ipiTribEl = ipiEl.Element(ns + "IPITrib");
            if (ipiTribEl != null)
            {
                ipiTribEl.NFElementAsEnum<SituacaoTributariaIPI>("CST", value => ipi.SituacaoTributaria = value);

                if (ipiTribEl.Element(ns + "vBC") != null)
                {
                    ipi.TipoCalculo = TipoCalculoIPI.Percentual;
                    ipiTribEl.NFElementAsDecimal("vBC", value => ipi.BaseCalculo = value);
                    ipiTribEl.NFElementAsDecimal("pIPI", value => ipi.Aliquota = value);
                }
                else
                {
                    ipi.TipoCalculo = TipoCalculoIPI.Valor;
                    ipiTribEl.NFElementAsDecimal("qUnid", value => ipi.Quantidade = value);
                    ipiTribEl.NFElementAsDecimal("vUnid", value => ipi.ValorUnidade = value);
                }

                ipiTribEl.NFElementAsDecimal("vIPI", value => ipi.Valor = value);
            }
            else
            {
                var ipiNtEl = ipiEl.Element(ns + "IPINT");
                if (ipiNtEl != null)
                    ipiNtEl.NFElementAsEnum<SituacaoTributariaIPI>("CST", value => ipi.SituacaoTributaria = value);
            }
        }

        private void ParseDetImpostoIcms(XElement icmsEl, Produto produto)
        {
            var el = icmsEl.Elements().First();

            switch (el.Name.LocalName)
            {
                case "ICMS00":
                    ParseDetImpostoIcms00(el, produto);
                    break;

                case "ICMS10":
                    ParseDetImpostoIcms10(el, produto);
                    break;

                case "ICMS20":
                    ParseDetImpostoIcms20(el, produto);
                    break;

                case "ICMS30":
                    ParseDetImpostoIcms30(el, produto);
                    break;

                case "ICMS40":
                    ParseDetImpostoIcms40(el, produto);
                    break;

                case "ICMS51":
                    ParseDetImpostoIcms51(el, produto);
                    break;

                case "ICMS60":
                    ParseDetImpostoIcms60(el, produto);
                    break;

                case "ICMS70":
                    ParseDetImpostoIcms70(el, produto);
                    break;

                case "ICMS90":
                    ParseDetImpostoIcms90(el, produto);
                    break;

                case "ICMSPart":
                    ParseDetImpostoIcmsPartilha(el, produto);
                    break;

                case "ICMSST":
                    ParseDetImpostoIcmsST(el, produto);
                    break;

                case "ICMSSN101":
                    ParseDetImpostoIcmsSN101(el, produto);
                    break;

                case "ICMSSN102":
                    ParseDetImpostoIcmsSN102(el, produto);
                    break;

                case "ICMSSN201":
                    ParseDetImpostoIcmsSN201(el, produto);
                    break;

                case "ICMSSN202":
                    ParseDetImpostoIcmsSN202(el, produto);
                    break;

                case "ICMSSN500":
                    ParseDetImpostoIcmsSN500(el, produto);
                    break;

                case "ICMSSN900":
                    ParseDetImpostoIcmsSN900(el, produto);
                    break;
            }
        }

        private void ParseDetImpostoIcmsSN900(XElement icmsSN900El, Produto produto)
        {
            var icms = new IcmsSN900();
            icmsSN900El.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);

            icmsSN900El.NFElementAsEnum<ModalidadeBaseCalculoIcms>("modBC", value => icms.ModalidadeBaseCalculo = value);
            icmsSN900El.NFElementAsDecimal("vBC", value => icms.ValorBaseCalculo = value);
            icmsSN900El.NFElementAsDecimal("pRedBC", value => icms.PercentualReducaoBaseCalculo = value);
            icmsSN900El.NFElementAsDecimal("pICMS", value => icms.Aliquota = value);
            icmsSN900El.NFElementAsDecimal("vICMS", value => icms.Valor = value);

            icmsSN900El.NFElementAsEnum<ModalidadeBaseCalculoIcmsST>("modBCST", value => icms.ModalidadeBaseCalculoST = value);
            icmsSN900El.NFElementAsDecimal("pMVAST", value => icms.PercentualMargemValorAdicionadoST = value);
            icmsSN900El.NFElementAsDecimal("pRedBCST", value => icms.PercentualReducaoBaseCalculoST = value);
            icmsSN900El.NFElementAsDecimal("vBCST", value => icms.ValorBaseCalculoST = value);
            icmsSN900El.NFElementAsDecimal("pICMSST", value => icms.AliquotaST = value);
            icmsSN900El.NFElementAsDecimal("vICMSST", value => icms.ValorST = value);

            icmsSN900El.NFElementAsDecimal("pCredSN", value => icms.AliquotaCredito = value);
            icmsSN900El.NFElementAsDecimal("vCredICMSSN", value => icms.ValorCredito = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcmsSN500(XElement icmsSN500El, Produto produto)
        {
            var origem = icmsSN500El.NFElementAsEnum<OrigemMercadoria>("orig");

            decimal? vBCSTRet = null;
            decimal? vICMSSTRet = null;

            icmsSN500El.NFElementAsDecimal("vBCSTRet", value => vBCSTRet = value);
            icmsSN500El.NFElementAsDecimal("vICMSSTRet", value => vICMSSTRet = value);

            IcmsSN500 icms;
            if (vBCSTRet.HasValue && vICMSSTRet.HasValue)
                icms = new IcmsSN500(origem, vBCSTRet.Value, vICMSSTRet.Value);
            else
                icms = new IcmsSN500(origem);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcmsSN202(XElement icmsSN202El, Produto produto)
        {
            var origem = icmsSN202El.NFElementAsEnum<OrigemMercadoria>("orig");
            var csosn = icmsSN202El.NFElementAsEnum<CSOSN>("CSOSN");

            var icms = new IcmsSN202(origem, csosn);

            icmsSN202El.NFElementAsEnum<ModalidadeBaseCalculoIcmsST>("modBCST", value => icms.ModalidadeBaseCalculoST = value);

            icmsSN202El.NFElementAsDecimal("pMVAST", value => icms.PercentualMargemValorAdicionadoST = value);
            icmsSN202El.NFElementAsDecimal("pRedBCST", value => icms.PercentualReducaoBaseCalculoST = value);
            icmsSN202El.NFElementAsDecimal("vBCST", value => icms.ValorBaseCalculoST = value);
            icmsSN202El.NFElementAsDecimal("pICMSST", value => icms.AliquotaST = value);
            icmsSN202El.NFElementAsDecimal("vICMSST", value => icms.ValorST = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcmsSN201(XElement icmsSN201El, Produto produto)
        {
            var icms = new IcmsSN201();
            icmsSN201El.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);
            icmsSN201El.NFElementAsEnum<ModalidadeBaseCalculoIcmsST>("modBCST", value => icms.ModalidadeBaseCalculoST = value);

            icmsSN201El.NFElementAsDecimal("pMVAST", value => icms.PercentualMargemValorAdicionadoST = value);
            icmsSN201El.NFElementAsDecimal("pRedBCST", value => icms.PercentualReducaoBaseCalculoST = value);
            icmsSN201El.NFElementAsDecimal("vBCST", value => icms.ValorBaseCalculoST = value);
            icmsSN201El.NFElementAsDecimal("pICMSST", value => icms.AliquotaST = value);
            icmsSN201El.NFElementAsDecimal("vICMSST", value => icms.ValorST = value);

            icmsSN201El.NFElementAsDecimal("pCredSN", value => icms.AliquotaCredito = value);
            icmsSN201El.NFElementAsDecimal("vCredICMSSN", value => icms.ValorCredito = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcmsSN102(XElement icmsSN102El, Produto produto)
        {
            var origem = icmsSN102El.NFElementAsEnum<OrigemMercadoria>("orig");
            var csosn = icmsSN102El.NFElementAsEnum<CSOSN>("CSOSN");

            var icms = new IcmsSN102(origem, csosn);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcmsSN101(XElement icmsSN101El, Produto produto)
        {
            var icms = new IcmsSN101();
            icmsSN101El.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);

            icmsSN101El.NFElementAsDecimal("pCredSN", value => icms.AliquotaCredito = value);
            icmsSN101El.NFElementAsDecimal("vCredICMSSN", value => icms.ValorCredito = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcmsST(XElement icmsSTEl, Produto produto)
        {
            var icms = new IcmsST();
            icmsSTEl.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);
            icmsSTEl.NFElementAsDecimal("vBCSTRet", value => icms.ValorBaseCalculoSTRetido = value);
            icmsSTEl.NFElementAsDecimal("vICMSSTRet", value => icms.ValorSTRetido = value);
            icmsSTEl.NFElementAsDecimal("vBCSTDest", value => icms.ValorBaseCalculoSTUFDestino = value);
            icmsSTEl.NFElementAsDecimal("vICMSSTDest", value => icms.ValorSTUFDestino = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcmsPartilha(XElement icmsPartEl, Produto produto)
        {
            var origem = icmsPartEl.NFElementAsEnum<OrigemMercadoria>("orig");
            var cst = icmsPartEl.NFElementAsEnum<SituacaoTributariaICMS>("CST");

            var icms = new IcmsPartilha(origem, cst);

            icmsPartEl.NFElementAsEnum<ModalidadeBaseCalculoIcms>("modBC", value => icms.ModalidadeBaseCalculo = value);
            icmsPartEl.NFElementAsDecimal("vBC", value => icms.ValorBaseCalculo = value);
            icmsPartEl.NFElementAsDecimal("pRedBC", value => icms.PercentualReducaoBaseCalculo = value);
            icmsPartEl.NFElementAsDecimal("pICMS", value => icms.Aliquota = value);
            icmsPartEl.NFElementAsDecimal("vICMS", value => icms.Valor = value);

            icmsPartEl.NFElementAsEnum<ModalidadeBaseCalculoIcmsST>("modBCST", value => icms.ModalidadeBaseCalculoST = value);
            icmsPartEl.NFElementAsDecimal("pMVAST", value => icms.PercentualMargemValorAdicionadoST = value);
            icmsPartEl.NFElementAsDecimal("pRedBCST", value => icms.PercentualReducaoBaseCalculoST = value);
            icmsPartEl.NFElementAsDecimal("vBCST", value => icms.ValorBaseCalculoST = value);
            icmsPartEl.NFElementAsDecimal("pICMSST", value => icms.AliquotaST = value);
            icmsPartEl.NFElementAsDecimal("vICMSST", value => icms.ValorST = value);

            icmsPartEl.NFElementAsDecimal("pBCOp", value => icms.PercentualBaseCalculoOperacaoPropria = value);
            icmsPartEl.NFElementAsEnum<SiglaUF>("UFST", value => icms.UFST = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcms90(XElement icms90El, Produto produto)
        {
            var icms = new Icms90();
            icms90El.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);

            icms90El.NFElementAsEnum<ModalidadeBaseCalculoIcms>("modBC", value => icms.ModalidadeBaseCalculo = value);
            icms90El.NFElementAsDecimal("vBC", value => icms.ValorBaseCalculo = value);
            icms90El.NFElementAsDecimal("pRedBC", value => icms.PercentualReducaoBaseCalculo = value);
            icms90El.NFElementAsDecimal("pICMS", value => icms.Aliquota = value);
            icms90El.NFElementAsDecimal("vICMS", value => icms.Valor = value);

            icms90El.NFElementAsEnum<ModalidadeBaseCalculoIcmsST>("modBCST", value => icms.ModalidadeBaseCalculoST = value);
            icms90El.NFElementAsDecimal("pMVAST", value => icms.PercentualMargemValorAdicionadoST = value);
            icms90El.NFElementAsDecimal("pRedBCST", value => icms.PercentualReducaoBaseCalculoST = value);
            icms90El.NFElementAsDecimal("vBCST", value => icms.ValorBaseCalculoST = value);
            icms90El.NFElementAsDecimal("pICMSST", value => icms.AliquotaST = value);
            icms90El.NFElementAsDecimal("vICMSST", value => icms.ValorST = value);

            icms90El.NFElementAsDecimal("vICMSDeson", value => icms.ValorIcmsDesoneracao = value);
            icms90El.NFElementAsEnum<MotivoDesoneracaoCondicionalICMS>("motDesICMS", value => icms.MotivoDesoneracaoIcms = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcms70(XElement icms70El, Produto produto)
        {
            var icms = new Icms70();
            icms70El.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);
            icms70El.NFElementAsEnum<ModalidadeBaseCalculoIcms>("modBC", value => icms.ModalidadeBaseCalculo = value);
            icms70El.NFElementAsDecimal("pRedBC", value => icms.PercentualReducaoBaseCalculo = value);
            icms70El.NFElementAsDecimal("vBC", value => icms.ValorBaseCalculo = value);
            icms70El.NFElementAsDecimal("pICMS", value => icms.Aliquota = value);
            icms70El.NFElementAsDecimal("vICMS", value => icms.Valor = value);
            icms70El.NFElementAsEnum<ModalidadeBaseCalculoIcmsST>("modBCST", value => icms.ModalidadeBaseCalculoST = value);
            icms70El.NFElementAsDecimal("pMVAST", value => icms.PercentualMargemValorAdicionadoST = value);
            icms70El.NFElementAsDecimal("pRedBCST", value => icms.PercentualReducaoBaseCalculoST = value);
            icms70El.NFElementAsDecimal("vBCST", value => icms.ValorBaseCalculoST = value);
            icms70El.NFElementAsDecimal("pICMSST", value => icms.AliquotaST = value);
            icms70El.NFElementAsDecimal("vICMSST", value => icms.ValorST = value);
            icms70El.NFElementAsDecimal("vICMSDeson", value => icms.ValorIcmsDesoneracao = value);
            icms70El.NFElementAsEnum<MotivoDesoneracaoCondicionalICMS>("motDesICMS", value => icms.MotivoDesoneracaoIcms = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcms60(XElement icms60El, Produto produto)
        {
            var orig = icms60El.NFElementAsEnum<OrigemMercadoria>("orig");

            decimal? vBCSTRet = null;
            decimal? vICMSSTRet = null;

            icms60El.NFElementAsDecimal("vBCSTRet", value => vBCSTRet = value);
            icms60El.NFElementAsDecimal("vICMSSTRet", value => vICMSSTRet = value);

            Icms60 icms;
            if (vBCSTRet != null && vICMSSTRet != null)
                icms = new Icms60(orig, vBCSTRet.Value, vICMSSTRet.Value);
            else
                icms = new Icms60(orig);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcms51(XElement icms51El, Produto produto)
        {
            var icms = new Icms51();
            icms51El.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);
            icms51El.NFElementAsEnum<ModalidadeBaseCalculoIcms>("modBC", value => icms.ModalidadeBaseCalculo = value);
            icms51El.NFElementAsDecimal("pRedBC", value => icms.PercentualReducaoBaseCalculo = value);
            icms51El.NFElementAsDecimal("vBC", value => icms.ValorBaseCalculo = value);
            icms51El.NFElementAsDecimal("pICMS", value => icms.Aliquota = value);

            icms51El.NFElementAsDecimal("vICMSOp", value => icms.ValorIcmsOperacao = value);
            icms51El.NFElementAsDecimal("pDif", value => icms.AliquotaDiferimento = value);
            icms51El.NFElementAsDecimal("vICMSDif", value => icms.ValorIcmsDiferido = value);

            icms51El.NFElementAsDecimal("vICMS", value => icms.Valor = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcms40(XElement icms40El, Produto produto)
        {
            var orig = icms40El.NFElementAsEnum<OrigemMercadoria>("orig");
            var cst = icms40El.NFElementAsEnum<SituacaoTributariaICMS>("CST");

            decimal? vICMSDeson = null;
            MotivoDesoneracaoCondicionalICMS? motDesICMS = null;

            icms40El.NFElementAsDecimal("vICMSDeson", value => vICMSDeson = value);
            icms40El.NFElementAsEnum<MotivoDesoneracaoCondicionalICMS>("motDesICMS", value => motDesICMS = value);

            Icms40 icms;
            if (vICMSDeson != null && motDesICMS != null)
                icms = new Icms40(orig, cst, vICMSDeson.Value, motDesICMS.Value);
            else
                icms = new Icms40(orig, cst);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcms30(XElement icms30El, Produto produto)
        {
            var icms = new Icms30();
            icms30El.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);
            icms30El.NFElementAsEnum<ModalidadeBaseCalculoIcmsST>("modBCST", value => icms.ModalidadeBaseCalculoST = value);
            icms30El.NFElementAsDecimal("pMVAST", value => icms.PercentualMargemValorAdicionadoST = value);
            icms30El.NFElementAsDecimal("pRedBCST", value => icms.PercentualReducaoBaseCalculoST = value);
            icms30El.NFElementAsDecimal("vBCST", value => icms.ValorBaseCalculoST = value);
            icms30El.NFElementAsDecimal("pICMSST", value => icms.AliquotaST = value);
            icms30El.NFElementAsDecimal("vICMSST", value => icms.ValorST = value);
            icms30El.NFElementAsDecimal("vICMSDeson", value => icms.ValorIcmsDesoneracao = value);
            icms30El.NFElementAsEnum<MotivoDesoneracaoCondicionalICMS>("motDesICMS", value => icms.MotivoDesoneracaoIcms = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcms20(XElement icms20El, Produto produto)
        {
            var icms = new Icms20();
            icms20El.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);
            icms20El.NFElementAsEnum<ModalidadeBaseCalculoIcms>("modBC", value => icms.ModalidadeBaseCalculo = value);
            icms20El.NFElementAsDecimal("pRedBC", value => icms.PercentualReducaoBaseCalculo = value);
            icms20El.NFElementAsDecimal("vBC", value => icms.ValorBaseCalculo = value);
            icms20El.NFElementAsDecimal("pICMS", value => icms.Aliquota = value);
            icms20El.NFElementAsDecimal("vICMS", value => icms.Valor = value);
            icms20El.NFElementAsDecimal("vICMSDeson", value => icms.ValorIcmsDesoneracao = value);
            icms20El.NFElementAsEnum<MotivoDesoneracaoCondicionalICMS>("motDesICMS", value => icms.MotivoDesoneracaoIcms = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcms00(XElement icms00El, Produto produto)
        {
            var icms = new Icms00();
            icms00El.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);
            icms00El.NFElementAsEnum<ModalidadeBaseCalculoIcms>("modBC", value => icms.ModalidadeBaseCalculo = value);
            icms00El.NFElementAsDecimal("vBC", value => icms.ValorBaseCalculo = value);
            icms00El.NFElementAsDecimal("pICMS", value => icms.Aliquota = value);
            icms00El.NFElementAsDecimal("vICMS", value => icms.Valor = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIcms10(XElement icms10El, Produto produto)
        {
            var icms = new Icms10();
            icms10El.NFElementAsEnum<OrigemMercadoria>("orig", value => icms.Origem = value);
            icms10El.NFElementAsEnum<ModalidadeBaseCalculoIcms>("modBC", value => icms.ModalidadeBaseCalculo = value);
            icms10El.NFElementAsDecimal("vBC", value => icms.ValorBaseCalculo = value);
            icms10El.NFElementAsDecimal("pICMS", value => icms.Aliquota = value);
            icms10El.NFElementAsDecimal("vICMS", value => icms.Valor = value);
            icms10El.NFElementAsEnum<ModalidadeBaseCalculoIcmsST>("modBCST", value => icms.ModalidadeBaseCalculoST = value);
            icms10El.NFElementAsDecimal("pMVAST", value => icms.PercentualMargemValorAdicionadoST = value);
            icms10El.NFElementAsDecimal("pRedBCST", value => icms.PercentualReducaoBaseCalculoST = value);
            icms10El.NFElementAsDecimal("vBCST", value => icms.ValorBaseCalculoST = value);
            icms10El.NFElementAsDecimal("pICMSST", value => icms.AliquotaST = value);
            icms10El.NFElementAsDecimal("vICMSST", value => icms.ValorST = value);

            produto.Imposto.Icms = icms;
        }

        private void ParseDetImpostoIssqn(XElement issqnEl, Produto produto)
        {
            issqnEl.NFElementAsDecimal("vBC", value => produto.Imposto.ISSQN.BaseCalculo = value);
            issqnEl.NFElementAsDecimal("vAliq", value => produto.Imposto.ISSQN.Aliquota = value);
            issqnEl.NFElementAsDecimal("vISSQN", value => produto.Imposto.ISSQN.ValorISSQN = value);
            issqnEl.NFElementAsInt32("cMunFG", value => produto.Imposto.ISSQN.CodigoMunicipioFatoGeradorIBGE = value);
            issqnEl.NFElementAsString("cListServ", value => produto.Imposto.ISSQN.CodigoServico = value);

            issqnEl.NFElementAsDecimal("vDeducao", value => produto.Imposto.ISSQN.ValorDeducao = value);
            issqnEl.NFElementAsDecimal("vOutro", value => produto.Imposto.ISSQN.ValorOutrasRetencoes = value);
            issqnEl.NFElementAsDecimal("vDescIncond", value => produto.Imposto.ISSQN.ValorDescontoIncondicionado = value);
            issqnEl.NFElementAsDecimal("vDescCond", value => produto.Imposto.ISSQN.ValorDescontoCondicionado = value);
            issqnEl.NFElementAsDecimal("vISSRet", value => produto.Imposto.ISSQN.ValorIssRetido = value);
            issqnEl.NFElementAsEnum<IndicadorExigibilidadeIss>("indISS",
                value => produto.Imposto.ISSQN.IndicadorExigibilidade = value);
            issqnEl.NFElementAsString("cServico", value => produto.Imposto.ISSQN.CodigoServicoPrestadoMunicipio = value);
            issqnEl.NFElementAsInt32("cMun", value => produto.Imposto.ISSQN.CodigoMunicipioIncidenciaImposto = value);
            issqnEl.NFElementAsInt32("cPais", value => produto.Imposto.ISSQN.CodigoPaisServicoPrestado = value);
            issqnEl.NFElementAsString("nProcesso", value => produto.Imposto.ISSQN.NumeroProcessoSuspensao = value);
            issqnEl.NFElementAsString("indIncentivo", value => produto.Imposto.ISSQN.PossuiIncentivoFiscal = (value == "1"));
        }

        private void ParseDetProduto(XElement detEl, Produto produto)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;

            var prodEl = detEl.Element(ns + "prod");
            prodEl.NFElementAsString("cProd", value => produto.Codigo = value);
            prodEl.NFElementAsString("cEAN", value => produto.CodigoGTIN = value);
            prodEl.NFElementAsString("xProd", value => produto.Descricao = value);
            prodEl.NFElementAsString("NCM", value => produto.CodigoNCM = value);
            prodEl.NFElementAsString("NVE", value => produto.CodigoNVE = value);
            prodEl.NFElementAsString("EXTIPI", value => produto.CodigoExTIPI = value);
            prodEl.NFElementAsInt32("CFOP", value => produto.CFOP = value);
            prodEl.NFElementAsString("uCom", value => produto.Unidade = value);
            prodEl.NFElementAsDecimal("qCom", value => produto.Quantidade = value);
            prodEl.NFElementAsDecimal("vUnCom", value => produto.ValorUnitario = value);
            prodEl.NFElementAsDecimal("vProd", value => produto.ValorTotalBruto = value);
            prodEl.NFElementAsString("cEANTrib", value => produto.CodigoGTINTributario = value);
            prodEl.NFElementAsString("uTrib", value => produto.UnidadeTributavel = value);
            prodEl.NFElementAsDecimal("qTrib", value => produto.QuantidadeTributavel = value);
            prodEl.NFElementAsDecimal("vUnTrib", value => produto.ValorUnitarioTributavel = value);
            prodEl.NFElementAsDecimal("vFrete", value => produto.ValorTotalFrete = value);
            prodEl.NFElementAsDecimal("vSeg", value => produto.ValorTotalSeguro = value);
            prodEl.NFElementAsDecimal("vDesc", value => produto.ValorDesconto = value);
            prodEl.NFElementAsDecimal("vOutro", value => produto.ValorOutrasDespesasAcessorias = value);
            prodEl.NFElementAsInt32("indTot", value => produto.ItemCompoeValorTotalNFe = (value == 1));

            // Declarações de Importação
            ParseDeclaracaoImportacao(prodEl.Elements(ns + "DI"), produto);

            ParseDetalhamentoExportacao(prodEl.Elements(ns + "detExport"), produto);

            prodEl.NFElementAsString("xPed", value => produto.PedidoCompra = value);
            prodEl.NFElementAsInt32("nItemPed", value => produto.ItemPedidoCompra = value);
            prodEl.NFElementAsString("nFCI", value => produto.NumeroFCI = Guid.Parse(value));

            // Veiculo
            var veicProdEl = prodEl.Element(ns + "veicProd");
            if (veicProdEl != null)
                ParseVeicProd(veicProdEl, produto);

            // Medicamentos
            ParseMedicamentos(prodEl.Elements(ns + "med"), produto);

            // Armas
            ParseArmas(prodEl.Elements(ns + "arma"), produto);

            // Combustível
            var combEl = prodEl.Element(ns + "comb");
            if (combEl != null)
                ParseCombustivel(combEl, produto);

            prodEl.NFElementAsString("nRECOPI", value => produto.NumeroRECOPI = value);
        }

        private void ParseCombustivel(XElement combEl, Produto produto)
        {
            var comb = produto.DetalhamentoCombustivel;
            combEl.NFElementAsInt32("cProdANP", value => comb.CodigoProdutoANP = value);
            combEl.NFElementAsString("CODIF", value => comb.CodigoCODIF = value);
            combEl.NFElementAsDecimal("qTemp", value => comb.QuantidadeCombustivelFaturadaTempAmbiente = value);
            combEl.NFElementAsEnum<SiglaUF>("UFCons", value => comb.UFConsumo = value);

            var cideEl = combEl.Element(Constants.XNamespacePortalFiscalNFe + "CIDE");
            if (cideEl != null)
            {
                cideEl.NFElementAsDecimal("qBCProd", value => comb.CIDE.BaseCalculo = value);
                cideEl.NFElementAsDecimal("vAliqProd", value => comb.CIDE.Aliquota = value);
                cideEl.NFElementAsDecimal("vCIDE", value => comb.CIDE.Valor = value);
            }
        }

        private void ParseArmas(IEnumerable<XElement> armaEls, Produto produto)
        {
            foreach (var armaEl in armaEls)
            {
                var arma = new Armamento();

                armaEl.NFElementAsEnum<TipoArmamento>("tpArma", value => arma.TipoArma = value);
                armaEl.NFElementAsString("nSerie", value => arma.NumeroSerie = value);
                armaEl.NFElementAsString("nCano", value => arma.NumeroCano = value);
                armaEl.NFElementAsString("descr", value => arma.Descricao = value);

                produto.DetalhamentoArmamento.Add(arma);
            }
        }

        private void ParseMedicamentos(IEnumerable<XElement> medEls, Produto produto)
        {
            foreach (var medEl in medEls)
            {
                var med = new Medicamento();

                medEl.NFElementAsString("nLote", value => med.NumeroLote = value);
                medEl.NFElementAsDecimal("qLote", value => med.QuantidadeProdutoLote = value);
                medEl.NFElementAsDateTime("dFab", value => med.DataFabricacao = value);
                medEl.NFElementAsDateTime("dVal", value => med.DataValidade = value);
                medEl.NFElementAsDecimal("vPMC", value => med.PrecoMaximoConsumidor = value);

                produto.DetalhamentoMedicamento.Add(med);
            }
        }

        private void ParseVeicProd(XElement veicProdEl, Produto produto)
        {
            var veicProd = produto.DetalhamentoVeiculo;

            veicProdEl.NFElementAsEnum<TipoOperacaoVendaVeiculo>("tpOp", value => veicProd.TipoOperacaoVenda = value);
            veicProdEl.NFElementAsString("chassi", value => veicProd.Chassi = value);
            veicProdEl.NFElementAsString("cCor", value => veicProd.CodigoCor = value);
            veicProdEl.NFElementAsString("xCor", value => veicProd.DescricaoCor = value);
            veicProdEl.NFElementAsString("pot", value => veicProd.PotenciaMotor = value);
            veicProdEl.NFElementAsString("cilin", value => veicProd.Cilindradas = value);
            veicProdEl.NFElementAsString("pesoL", value => veicProd.PesoLiquido = value);
            veicProdEl.NFElementAsString("pesoB", value => veicProd.PesoBruto = value);
            veicProdEl.NFElementAsString("nSerie", value => veicProd.NumeroSerie = value);
            veicProdEl.NFElementAsString("tpComb", value => veicProd.Combustivel = value);
            veicProdEl.NFElementAsString("nMotor", value => veicProd.NumeroMotor = value);
            veicProdEl.NFElementAsDecimal("CMT", value => veicProd.CapacidadeMaximaTracao = value);
            veicProdEl.NFElementAsInt32("dist", value => veicProd.DistanciaEixos = value);
            veicProdEl.NFElementAsInt32("anoMod", value => veicProd.AnoModelo = value);
            veicProdEl.NFElementAsInt32("anoFab", value => veicProd.AnoFabricacao = value);
            veicProdEl.NFElementAsString("tpPint", value => veicProd.TipoPintura = value);
            veicProdEl.NFElementAsEnum<TipoVeiculoRENAVAM>("tpVeic", value => veicProd.TipoVeiculo = value);
            veicProdEl.NFElementAsEnum<EspecieVeiculoRENAVAM>("espVeic", value => veicProd.EspecieVeiculo = value);
            veicProdEl.NFElementAsString("VIN", value => veicProd.ChassiRemarcado = (value == "R"));
            veicProdEl.NFElementAsEnum<CondicaoVeiculo>("condVeic", value => veicProd.CondicaoVeiculo = value);
            veicProdEl.NFElementAsInt32("cMod", value => veicProd.CodigoMarcaModelo = value);
            veicProdEl.NFElementAsEnum<TipoCorDenatran>("cCorDENATRAN", value => veicProd.CorDenatran = value);
            veicProdEl.NFElementAsInt32("lota", value => veicProd.LotacaoMaximaPassageirosSentados = value);
            veicProdEl.NFElementAsEnum<TipoRestricaoVeiculo>("tpRest", value => veicProd.TipoRestricao = value);
        }

        private void ParseDeclaracaoImportacao(IEnumerable<XElement> diEls, Produto produto)
        {
            if (diEls == null)
                return;

            foreach (var diEl in diEls)
            {
                var di = new DeclaracaoImportacao();
                di.Produto = produto;

                diEl.NFElementAsString("nDI", value => di.Numero = value);
                diEl.NFElementAsDateTime("dDI", value => di.DataRegistro = value);
                diEl.NFElementAsString("xLocDesemb", value => di.LocalDesembaracoAduaneiro = value);
                diEl.NFElementAsEnum<SiglaUF>("UFDesemb", value => di.UFDesembaracoAduaneiro = value);
                diEl.NFElementAsDateTime("dDesemb", value => di.DataDesembaracoAduaneiro = value);

                diEl.NFElementAsEnum<TipoViaTransporteInternacional>("tpViaTransp", value => di.TipoViaTransporte = value);
                diEl.NFElementAsDecimal("vAFRMM", value => di.ValorAFRMM = value);
                diEl.NFElementAsEnum<TipoIntermedioImportacao>("tpIntermedio", value => di.TipoIntermedio = value);
                diEl.NFElementAsString("CNPJ", value => di.CNPJ = value);
                diEl.NFElementAsEnum<SiglaUF>("UFTerceiro", value => di.UFTerceiro = value);

                diEl.NFElementAsString("cExportador", value => di.CodigoExportador = value);
                diEl.NFElementAsString("nDI", value => di.Numero = value);

                foreach (var adiEl in diEl.Elements(Constants.XNamespacePortalFiscalNFe + "adi"))
                {
                    var adi = new DeclaracaoImportacaoAdicao();
                    adiEl.NFElementAsInt32("nAdicao", value => adi.Numero = value);
                    adiEl.NFElementAsInt32("nSeqAdic", value => adi.NumeroSequencial = value);
                    adiEl.NFElementAsString("cFabricante", value => adi.CodigoFabricante = value);
                    adiEl.NFElementAsDecimal("vDescDI", value => adi.ValorDescontoItemDI = value);

                    di.Adicoes.Add(adi);
                }

                produto.DeclaracoesImportacao.Add(di);
            }
        }

        private void ParseDetalhamentoExportacao(IEnumerable<XElement> detExportEls, Produto produto)
        {
            if (detExportEls == null)
                return;

            foreach (var detExportEl in detExportEls)
            {
                var de = new DetalheExportacao();
                detExportEl.NFElementAsString("nDraw", value => de.NumeroDrawback = value);

                var exportIndEl = detExportEl.Element(Constants.XNamespacePortalFiscalNFe + "exportInd");
                if (exportIndEl != null)
                {
                    exportIndEl.NFElementAsString("nRE", value => de.Detalhamento.NumeroRegistro = value);
                    exportIndEl.NFElementAsString("chNFe", value => de.Detalhamento.ChaveAcessoNFe = value);
                    exportIndEl.NFElementAsDecimal("qExport", value => de.Detalhamento.QuantidadeItemExportado = value);
                }

                produto.DetalhamentoExportacao.Add(de);
            }
        }

        private void ParseEntrega(XElement entregaEl)
        {
            throw new NotImplementedException("Parse do elemento 'entrega' não implementado.");
        }

        private void ParseIdentificacao(XElement ideEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;

            var ide = Identificacao;

            ide.UFEnvio = ideEl.NFElementAsEnum<UfIBGE>("cUF");
            ide.CodigoNF = ideEl.NFElementAsInt32("cNF");
            ide.NaturezaOperacao = ideEl.NFElementAsString("natOp");
            ide.FormaPagamento = ideEl.NFElementAsEnum<IndicadorFormaPagamento>("indPag");
            ide.CodigoModeloDocFiscal = ideEl.NFElementAsEnum<TipoModalidadeDocumentoFiscal>("mod");
            ide.Serie = ideEl.NFElementAsInt32("serie");
            ide.NumeroNF = ideEl.NFElementAsInt32("nNF");
            ide.DataEmissao = ideEl.NFElementAsDateTime("dhEmi");

            var dhSaiEntEl = ideEl.Element(ns + "dhSaiEnt");
            if (dhSaiEntEl != null)
                ide.DataEntradaSaida = DateTime.Parse(dhSaiEntEl.Value);

            ide.TipoNotaFiscal = ideEl.NFElementAsEnum<TipoNotaFiscal>("tpNF");
            ide.IdentificadorLocalDestinoOperacao = ideEl.NFElementAsEnum<TipoIdentificadorLocalDestinoOperacao>("idDest");
            ide.CodigoMunicipioFatoGerador = ideEl.NFElementAsInt32("cMunFG");
            ide.TipoImpressao = ideEl.NFElementAsEnum<TipoFormatoImpressaoDANFE>("tpImp");
            ide.TipoEmissao = ideEl.NFElementAsEnum<TipoEmissaoNFe>("tpEmis");
            ide.Ambiente = ideEl.NFElementAsEnum<TipoAmbiente>("tpAmb");
            ide.Finalidade = ideEl.NFElementAsEnum<TipoFinalidade>("finNFe");
            ide.OperacaoConsumidorFinal = ideEl.NFElementAsInt32("indPres") == 1;
            ide.TipoProcessoEmissao = ideEl.NFElementAsEnum<TipoProcessoEmissaoNFe>("procEmi");
            ide.VersaoAplicativoEmissao = ideEl.NFElementAsString("verProc");

            var dhContEl = ideEl.Element(ns + "dhCont");
            if (dhContEl != null)
                ide.DataHoraEntradaContingencia = DateTime.Parse(dhContEl.Value);

            var xJustEl = ideEl.Element(ns + "xJust");
            if (xJustEl != null)
                ide.JustificativaEntradaContingencia = xJustEl.Value;

            ParseReferenciaDocFiscal(ideEl.Elements(ns + "NFref"));
        }

        private void ParseReferenciaDocFiscal(IEnumerable<XElement> nfRefEls)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;

            foreach (var nfRefEl in nfRefEls)
            {
                var refEl = nfRefEl.Element(ns + "refNFe");
                if (refEl != null)
                {
                    Identificacao.ReferenciasDocumentoFiscais.Add(new ReferenciaDocumentoFiscalNfe()
                    {
                        ChaveAcessoNFe = refEl.Value
                    });
                    continue;
                }

                refEl = nfRefEl.Element(ns + "refNF");
                if (refEl != null)
                {
                    var nfRef = new ReferenciaDocumentoFiscalNotaFiscal();
                    refEl.NFElementAsEnum<UfIBGE>("cUF", value => nfRef.UnidadeFederativa = value);
                    refEl.NFElementAsString("AAMM", value => nfRef.MesAnoEmissao = DateTime.ParseExact(value, "yyMM", CultureInfo.InvariantCulture));
                    refEl.NFElementAsString("CNPJ", value => nfRef.CNPJ = value);
                    refEl.NFElementAsString("mod", value => nfRef.CodigoModeloDocumentoFiscal = value);
                    refEl.NFElementAsInt32("serie", value => nfRef.SerieNf = value);
                    refEl.NFElementAsInt32("nNF", value => nfRef.NumeroNf = value);

                    Identificacao.ReferenciasDocumentoFiscais.Add(nfRef);
                    continue;
                }

                refEl = nfRefEl.Element(ns + "refNFP");
                if (refEl != null)
                {
                    var nfRef = new ReferenciaDocumentoFiscalNotaFiscalProdutor();
                    refEl.NFElementAsEnum<UfIBGE>("cUF", value => nfRef.UnidadeFederativa = value);
                    refEl.NFElementAsString("AAMM", value => nfRef.MesAnoEmissao = DateTime.ParseExact(value, "yyMM", CultureInfo.InvariantCulture));
                    refEl.NFElementAsString("CNPJ", value => nfRef.CNPJ = value);
                    refEl.NFElementAsString("CPF", value => nfRef.CPF = value);
                    refEl.NFElementAsString("IE", value => nfRef.InscricaoEstadual = value);
                    refEl.NFElementAsString("mod", value => nfRef.CodigoModeloDocumentoFiscal = value);
                    refEl.NFElementAsInt32("serie", value => nfRef.SerieNf = value);
                    refEl.NFElementAsInt32("nNF", value => nfRef.NumeroNf = value);

                    Identificacao.ReferenciasDocumentoFiscais.Add(nfRef);
                    continue;
                }

                refEl = nfRefEl.Element(ns + "refCTe");
                if (refEl != null)
                {
                    Identificacao.ReferenciasDocumentoFiscais.Add(new ReferenciaDocumentoFiscalCte()
                    {
                        ReferenciaCte = refEl.Value
                    });

                    continue;
                }

                refEl = nfRefEl.Element(ns + "refECF");
                if (refEl != null)
                {
                    var nfRef = new ReferenciaDocumentoFiscalEcf();
                    refEl.NFElementAsString("mod", value => nfRef.CodigoModeloDocumentoFiscal = value);
                    refEl.NFElementAsInt32("nECF", value => nfRef.NumeroEcf = value);
                    refEl.NFElementAsInt32("nCOO", value => nfRef.NumeroContadorOrdemOperacao = value);
                    Identificacao.ReferenciasDocumentoFiscais.Add(nfRef);

                    continue;
                }
            }
        }

        private void ParseEmitente(XElement emitEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var emit = Emitente;

            if (!emitEl.NFElementAsString("CNPJ", value => emit.CNPJ = value))
                emitEl.NFElementAsString("CPF", value => emit.CPF = value);

            emitEl.NFElementAsString("xNome", value => emit.Nome = value);
            emitEl.NFElementAsString("xFant", value => emit.NomeFantasia = value);

            // Endereço do Emitente
            ParseEnderecoEmitente(emitEl.Element(ns + "enderEmit"));

            emitEl.NFElementAsString("IE", value => emit.InscricaoEstadual = value);
            emitEl.NFElementAsString("IEST", value => emit.InscricaoMunicipal = value);
            emitEl.NFElementAsString("IM", value => emit.InscricaoEstadualSubstitutoTributario = value);
            emitEl.NFElementAsEnum<CodigoRegimeTributario>("CRT", value => emit.CodigoRegimeTributario = value);
        }

        private void ParseEnderecoEmitente(XElement enderEmitEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;
            var endereco = Emitente.Endereco;

            endereco.Logradouro = enderEmitEl.NFElementAsString("xLgr");
            endereco.Numero = enderEmitEl.NFElementAsString("nro");

            var xCplEl = enderEmitEl.Element(ns + "xCpl");
            if (xCplEl != null)
                endereco.Complemento = xCplEl.Value;

            endereco.Bairro = enderEmitEl.NFElementAsString("xBairro");
            endereco.CodigoMunicipioIBGE = enderEmitEl.NFElementAsInt32("cMun");
            endereco.NomeMunicipio = enderEmitEl.NFElementAsString("xMun");
            endereco.UF = enderEmitEl.NFElementAsEnum<SiglaUF>("UF");
            endereco.CEP = enderEmitEl.NFElementAsString("CEP");

            var cPaisEl = enderEmitEl.Element(ns + "cPais");
            if (cPaisEl != null)
                endereco.CodigoPaisBACEN = int.Parse(cPaisEl.Value);

            var xPaisEl = enderEmitEl.Element(ns + "xPais");
            if (xPaisEl != null)
                endereco.NomePais = xPaisEl.Value;

            var foneEl = enderEmitEl.Element(ns + "fone");
            if (foneEl != null)
                endereco.Telefone = foneEl.Value;
        }

        private void ParseAvulsa(XElement avulsaEl)
        {
            throw new NotImplementedException("O Elemento 'avulsa' não foi implementado.");
        }

        private void ParseDestinatario(XElement destEl)
        {
            var ns = Constants.XNamespacePortalFiscalNFe;

            string cnpjCpfIdEstrangeiro;

            var cnpjEl = destEl.Element(ns + "CNPJ");
            if (cnpjEl != null)
            {
                cnpjCpfIdEstrangeiro = cnpjEl.Value;
            }
            else
            {
                var cpfEl = destEl.Element(ns + "CPF");
                cnpjCpfIdEstrangeiro = cpfEl != null ? cpfEl.Value : destEl.Element(ns + "idEstrangeiro").Value;
            }

            var dest = Destinatario = new DestinatarioNFe(cnpjCpfIdEstrangeiro);

            destEl.NFElementAsString("xNome", value => dest.Nome = value);

            var enderDestEl = destEl.Element(ns + "enderDest");
            if (enderDestEl != null)
                ParseEnderecoDestinatario(enderDestEl);

            destEl.NFElementAsEnum<IndicadorIEDestinatario>("indIEDest", value => dest.IndicadorIEDestinatario = value);
            destEl.NFElementAsString("IE", value => dest.InscricaoEstadual = value);
            destEl.NFElementAsString("ISUF", value => dest.InscricaoSUFRAMA = value);
            destEl.NFElementAsString("email", value => dest.Email = value);
        }

        private void ParseEnderecoDestinatario(XElement enderDestEl)
        {
            ParseEndereco(enderDestEl, Destinatario.Endereco);
        }

        private void ParseEndereco(XElement enderecoEl, Endereco endereco)
        {
            enderecoEl.NFElementAsString("xLgr", value => endereco.Logradouro = value);
            enderecoEl.NFElementAsString("nro", value => endereco.Numero = value);
            enderecoEl.NFElementAsString("xCpl", value => endereco.Complemento = value);
            enderecoEl.NFElementAsString("xBairro", value => endereco.Bairro = value);
            enderecoEl.NFElementAsInt32("cMun", value => endereco.CodigoMunicipioIBGE = value);
            enderecoEl.NFElementAsString("xMun", value => endereco.NomeMunicipio = value);
            enderecoEl.NFElementAsEnum<SiglaUF>("UF", value => endereco.UF = value);
            enderecoEl.NFElementAsString("CEP", value => endereco.Logradouro = value);
            enderecoEl.NFElementAsString("cPais", value => endereco.Logradouro = value);
            enderecoEl.NFElementAsString("xPais", value => endereco.Logradouro = value);
            enderecoEl.NFElementAsString("fone", value => endereco.Logradouro = value);
        }

        private void ParseRetirada(XElement retiradaEl)
        {
            throw new NotImplementedException("Parse do elemento 'retirada' não implementado.");
        }

        /// <summary>
        /// Realiza a validação da Nota Fiscal Eletrônica preenchida.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        public ResultadoValidacao Validar()
        {
            /// TODO: Descomentar aqui
            //LicenseValidator licenseValidator = new LicenseValidator();
            //licenseValidator.ValidateNFe(this);

            StringBuilder sb = new StringBuilder();
            NFeValidatorEngine engine = new NFeValidatorEngine(this);
            bool valido = engine.ValidateNFe();

            ResultadoValidacao resultadoValidacao;

            if (valido)
                resultadoValidacao = new ResultadoValidacao();
            else
                resultadoValidacao = new ResultadoValidacao(engine.Context.Errors);

            return resultadoValidacao;
        }

        public static NFe Parse(string xml)
        {
            if (String.IsNullOrEmpty(xml))
                throw new ArgumentNullException(nameof(xml));

            var doc = XDocument.Parse(xml);
            return Parse(doc);
        }

        public static NFe Parse(XDocument doc)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));

            var nfe = new NFe();
            nfe.Deserialize(doc);
            return nfe;
        }

        public static NFe Parse(XDocument doc, bool ignoreSchemaValidation)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));

            var nfe = new NFe();
            nfe.Deserialize(doc, ignoreSchemaValidation);
            return nfe;
        }

        public static NFe Parse(XElement nfeEl)
        {
            if (nfeEl == null) throw new ArgumentNullException(nameof(nfeEl));

            var nfe = new NFe();
            nfe.Deserialize(nfeEl);
            return nfe;
        }

        /// <summary>
        /// Gera o xml (não assinado) de acordo com o preenchimento da Nota Fiscal Eletrônica.
        /// </summary>
        /// <returns>String xml contendo a Nota Fiscal Eletrônica não assinada.</returns>
        /// <exception cref="System.Exception">Causado caso a NF-e não seja válida.</exception>
        public string GerarXmlNaoAssinado()
        {
            var settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false), OmitXmlDeclaration = true };

            string xml;

            using (var sw = new StringWriter())
            using (var writer = XmlWriter.Create(sw, settings))
            {
                ((ISerializavel)this).Serializar(writer, this);
                writer.Flush();
                xml = sw.ToString();
            }

            return xml;
        }

        /// <summary>
        /// Gera o xml (não assinado) de acordo com o preenchimento da Nota Fiscal Eletrônica, sem
        /// validação dos dados.
        /// </summary>
        /// <returns>String xml contendo a Nota Fiscal Eletrônica não assinada.</returns>
        /// <exception cref="System.Exception">Causado caso a NF-e não seja válida.</exception>
        public string GerarXmlNaoAssinadoSemValidacao()
        {
            var utf8 = new UTF8Encoding(false);

            var settings = new XmlWriterSettings { Encoding = utf8, OmitXmlDeclaration = true };

            string xml;

            using (var sw = new StringWriter())
            using (var writer = XmlWriter.Create(sw, settings))
            {
                ((ISerializavel)this).Serializar(writer, this);
                writer.Flush();
                xml = sw.ToString();
            }

            //Assinador.Instance.ValidarXmlNFeNaoAssinada(xml);

            return xml;
        }

        /// <summary>
        /// Salva o xml gerado referente a Nota Fiscal Eletrônica sem assinatura digital.
        /// </summary>
        /// <param name="caminho">Caminho onde o arquivo contendo a NF-e será gravado.</param>
        /// <returns>Retorna o nome do arquivo xml contendo a Nota Fiscal Eletrônica assinada.</returns>
        /// <remarks>
        /// O nome do arquivo será gerado automaticamente com base na chave de acesso. O seguinte
        /// formato será utilizado: [CHAVE_ACESSO]-nfe.xml.
        /// </remarks>
        public string SalvarXmlNaoAssinado(string caminho)
        {
            var filename = $"{ChaveAcesso}-nfe.xml";
            var path = Path.Combine(caminho, filename);

            var utf8 = new UTF8Encoding(false);

            var settings = new XmlWriterSettings { Encoding = utf8 };

            using (var writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartDocument();
                writer.WriteRaw(GerarXmlNaoAssinado());
                writer.Flush();
            }

            return path;
        }
    }
}