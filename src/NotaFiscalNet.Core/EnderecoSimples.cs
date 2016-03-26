using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;
using System.Xml;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Endereço Simples do Emitente, Destinatário, Retirada e Entrega da Nota Fiscal Eletrônica.
    /// </summary>
    public class EnderecoSimples : IModificavel
    {
        public virtual bool Modificado => !string.IsNullOrEmpty(Logradouro) ||
                                          !string.IsNullOrEmpty(Numero) ||
                                          !string.IsNullOrEmpty(Complemento) ||
                                          !string.IsNullOrEmpty(Bairro) ||
                                          CodigoMunicipioIBGE != 0 ||
                                          !string.IsNullOrEmpty(NomeMunicipio) ||
                                          UF != SiglaUF.NaoEspecificado;

        internal void SerializeEnderecoSimples(XmlWriter writer, INFe nfe)
        {
            writer.WriteElementString("xLgr", Logradouro.ToToken(60));
            writer.WriteElementString("nro", Numero.ToToken(60));
            if (!string.IsNullOrEmpty(Complemento))
                writer.WriteElementString("xCpl", Complemento.ToToken(60));
            writer.WriteElementString("xBairro", Bairro.ToToken(60));
            writer.WriteElementString("cMun", CodigoMunicipioIBGE.ToString());
            writer.WriteElementString("xMun", NomeMunicipio.ToToken(60));
            writer.WriteElementString("UF", UF.ToString());
        }

        private string _logradouro = string.Empty;
        private string _numero = string.Empty;
        private string _complemento = string.Empty;
        private string _bairro = string.Empty;
        private int _codigoMunicipioIBGE;
        private string _nomeMunicipio = string.Empty;
        private SiglaUF _UF = SiglaUF.NaoEspecificado;

        /// <summary>
        /// [xLgr] Retorna ou define o Logradouro
        /// </summary>
        [NFeField(ID = "C06", FieldName = "xLgr", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60)]
        [NFeField(ID = "E06", FieldName = "xLgr", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 2, MaxLength = 60)]
        [NFeField(ID = "F03", FieldName = "xLgr", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60)]
        [NFeField(ID = "G03", FieldName = "xLgr", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60)]
        [CampoValidavel(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Logradouro
        {
            get { return _logradouro; }
            set { _logradouro = ValidationUtil.ValidateTString(value, 2, 60); }
        }

        /// <summary>
        /// [nro] Retorna ou define o Número
        /// </summary>
        [NFeField(ID = "C07", FieldName = "nro", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60)]
        [NFeField(ID = "E07", FieldName = "nro", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60)]
        [NFeField(ID = "F04", FieldName = "nro", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60)]
        [NFeField(ID = "G04", FieldName = "nro", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60)]
        [CampoValidavel(2, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Numero
        {
            get { return _numero; }
            set { _numero = ValidationUtil.ValidateTString(value, 1, 60); }
        }

        /// <summary>
        /// [xCpl] Retorna ou define o Complemento. Opcional.
        /// </summary>
        [NFeField(ID = "C08", FieldName = "xCpl", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60, Opcional = true)]
        [NFeField(ID = "E08", FieldName = "xCpl", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60, Opcional = true)]
        [NFeField(ID = "F05", FieldName = "xCpl", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60, Opcional = true)]
        [NFeField(ID = "G05", FieldName = "xCpl", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 1, MaxLength = 60, Opcional = true)]
        [CampoValidavel(3, Opcional = true)]
        public string Complemento
        {
            get { return _complemento; }
            set { _complemento = value == null ? null : ValidationUtil.ValidateTString(value, 1, 60); }
        }

        /// <summary>
        /// [xBairro] Retorna ou define o Bairro
        /// </summary>
        [NFeField(ID = "C09", FieldName = "xBairro", DataType = "TString",
            Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", MinLength = 1, MaxLength = 60)]
        [NFeField(ID = "E09", FieldName = "xBairro", DataType = "TString",
            Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", MinLength = 1, MaxLength = 60)]
        [NFeField(ID = "F06", FieldName = "xBairro", DataType = "TString",
            Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", MinLength = 1, MaxLength = 60)]
        [NFeField(ID = "G06", FieldName = "xBairro", DataType = "TString",
            Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}", MinLength = 1, MaxLength = 60)]
        [CampoValidavel(4, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = ValidationUtil.ValidateTString(value, 2, 60); }
        }

        /// <summary>
        /// [cMun] Retorna ou define o Código do Município de acordo com a Tabela do IBGE. Informar
        /// '9999999' para operações com o Exterior.
        /// </summary>
        [NFeField(ID = "C10", FieldName = "cMun", DataType = "TCodMunIBGE", Pattern = "[0-9]{7}")]
        [NFeField(ID = "E10", FieldName = "cMun", DataType = "TCodMunIBGE", Pattern = "[0-9]{7}")]
        [NFeField(ID = "F07", FieldName = "cMun", DataType = "TCodMunIBGE", Pattern = "[0-9]{7}")]
        [NFeField(ID = "G07", FieldName = "cMun", DataType = "TCodMunIBGE", Pattern = "[0-9]{7}")]
        [CampoValidavel(5, ChaveErroValidacao.CampoNaoPreenchido)]
        public int CodigoMunicipioIBGE
        {
            get { return _codigoMunicipioIBGE; }
            set { _codigoMunicipioIBGE = ValidationUtil.ValidateTCodMunIBGE(value, "CodigoMunicipioIBGE"); }
        }

        /// <summary>
        /// [xMun] Retorna ou define o Nome do Município. Informar 'EXTERIOR' para operações com o Exterior.
        /// </summary>
        [NFeField(ID = "C11", FieldName = "xMun", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 2, MaxLength = 60)]
        [NFeField(ID = "E11", FieldName = "xMun", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 2, MaxLength = 60)]
        [NFeField(ID = "F08", FieldName = "xMun", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 2, MaxLength = 60)]
        [NFeField(ID = "G08", FieldName = "xMun", DataType = "TString", Pattern = "[!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1}",
            MinLength = 2, MaxLength = 60)]
        [CampoValidavel(6, ChaveErroValidacao.CampoNaoPreenchido)]
        public string NomeMunicipio
        {
            get { return _nomeMunicipio; }
            set { _nomeMunicipio = ValidationUtil.ValidateTString(value, 2, 60); }
        }

        /// <summary>
        /// [UF] Retorna ou define a Sigla da UF. Informar 'EX' para operações com o Exterior.
        /// </summary>
        [NFeField(ID = "C12", FieldName = "UF", DataType = "TUf")]
        [NFeField(ID = "E12", FieldName = "UF", DataType = "TUf")]
        [NFeField(ID = "F09", FieldName = "UF", DataType = "TUf")]
        [NFeField(ID = "G09", FieldName = "UF", DataType = "TUf")]
        [CampoValidavel(7, ChaveErroValidacao.CampoNaoPreenchido, ValorNaoPreenchido = UfIBGE.NaoEspecificado)]
        public SiglaUF UF
        {
            get { return _UF; }
            set { _UF = ValidationUtil.ValidateEnum(value, "UF"); }
        }
    }
}