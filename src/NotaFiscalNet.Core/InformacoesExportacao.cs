﻿using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representam as informações de Exportação da Nota Fiscal Eletrônica.
    /// </summary>

    public sealed class InformacoesExportacao : ISerializavel, IModificavel
    {
        private SiglaUF _ufSaidaPais = SiglaUF.NaoEspecificado;
        private string _localEmbarque = string.Empty;
        private string _localDespacho = string.Empty;

        /// <summary>
        /// [UFEmbarq] Retorna ou define a Sigla da UF de Embarque o de transposição de fronteira.
        /// </summary>
        [NFeField(ID = "ZA02", FieldName = "UFSaidaPais", DataType = "TUfEmi")]
        [CampoValidavel(1, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = SiglaUF.NaoEspecificado)]
        public SiglaUF UFSaidaPais
        {
            get { return _ufSaidaPais; }
            set
            {
                if (value == SiglaUF.EX)
                    throw new InvalidOperationException("EX não é um valor válido para o campo.");

                _ufSaidaPais = ValidationUtil.ValidateEnum(value, "UFEmbarque");
            }
        }

        /// <summary>
        /// [xLocExporta] Retorna ou define a Descrição do Local de Embarque ou de transposição de fronteira.
        /// </summary>
        [NFeField(ID = "ZA03", FieldName = "xLocExporta", DataType = "TString", MinLength = 1, MaxLength = 60, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}")]
        [CampoValidavel(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public string LocalEmbarque
        {
            get { return _localEmbarque; }
            set
            {
                _localEmbarque = ValidationUtil.TruncateString(value, 60);
            }
        }

        /// <summary>
        /// [xLocDespacho] Retorna ou define a Descrição do Local de Despacho.
        /// </summary>
        [NFeField(ID = "ZA04", FieldName = "xLocDespacho", DataType = "TString", MinLength = 1, MaxLength = 60, Pattern = @"[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", Opcional = true)]
        [CampoValidavel(2)]
        public string LocalDespacho
        {
            get { return _localDespacho; }
            set
            {
                _localDespacho = ValidationUtil.TruncateString(value, 60);
            }
        }

        /// <summary>
        /// Retorna se a classe foi modificada.
        /// </summary>
        public bool Modificado => UFSaidaPais != SiglaUF.NaoEspecificado ||
                                  !string.IsNullOrEmpty(LocalEmbarque) ||
                                  !string.IsNullOrEmpty(LocalDespacho);

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("exporta"); // Elemento 'exporta'

            writer.WriteElementString("UFSaidaPais", UFSaidaPais.ToString());
            writer.WriteElementString("xLocExporta", SerializationUtil.ToToken(LocalEmbarque, 60));

            if (!String.IsNullOrEmpty(LocalDespacho))
                writer.WriteElementString("xLocDespacho", SerializationUtil.ToToken(LocalDespacho, 60));

            writer.WriteEndElement(); // Elemento 'exporta'
        }
    }
}