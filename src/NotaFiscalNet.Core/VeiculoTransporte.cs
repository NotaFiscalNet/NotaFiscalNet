using System.Runtime.InteropServices;
using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;
using NotaFiscalNet.Core.Validacao;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa o Veículo utilizado no Transporte
    /// </summary>
    
    
    
    public sealed class VeiculoTransporte :  ISerializavel, IModificavel
    {
        #region Fields

        private string _placa = string.Empty;
        private SiglaUF _UF = SiglaUF.NaoEspecificado;
        private string _RNTC = string.Empty;

        #endregion Fields

        #region Properties
      
        /// <summary>
        /// [placa] Retorna ou define a Placa do Veículo
        /// </summary>
        [NFeField(ID = "X19", FieldName = "placa", DataType = "token", MinLength = 1, MaxLength = 8, Pattern = "[A-Z0-9]+")]
        [NFeField(ID = "X23", FieldName = "placa", DataType = "token", MinLength = 1, MaxLength = 8, Pattern = "[A-Z0-9]+")]
        [ValidateField(1, ChaveErroValidacao.CampoNaoPreenchido)]
        public string Placa
        {
            get { return _placa; }
            set {
                ValidationUtil.ValidatePlaca(value, "Placa");
                _placa = ValidationUtil.TruncateString(value, 8);
            }
        }

        /// <summary>
        /// [UF] Retorna ou define a Sigla UF do Veículo
        /// </summary>
        [NFeField(ID = "X20", FieldName = "UF", DataType = "TUf")]
        [NFeField(ID = "X24", FieldName = "UF", DataType = "TUf")]
        [ValidateField(2, ChaveErroValidacao.CampoNaoPreenchido, DefaultValue=SiglaUF.NaoEspecificado)]
        public SiglaUF UF
        {
            get { return _UF; }
            set {
                ValidationUtil.ValidateEnum<SiglaUF>(value, "UF");

                _UF = value; 
            }
        }

        /// <summary>
        /// [RNTC] Retorna ou define o Registro Nacional de Transportador de Carga (ANTT). Opcional.
        /// </summary>
        [NFeField(ID = "X21", FieldName = "RNTC", DataType = "token", MinLength = 1, MaxLength = 20, Opcional = true)]
        [NFeField(ID = "X25", FieldName = "RNTC", DataType = "token", MinLength = 1, MaxLength = 20, Opcional = true)]
        [ValidateField(3, true)]
        public string RNTC
        {
            get { return _RNTC; }
            set {
                _RNTC = ValidationUtil.TruncateString(value, 20);
            }
        }

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado
        {
            get
            {
                return
                    !string.IsNullOrEmpty(Placa) ||
                    UF != SiglaUF.NaoEspecificado ||
                    !string.IsNullOrEmpty(RNTC);
            }
        }


        #endregion Properties   

        #region Constructor

        /// <summary>
        /// Inicializa uma nova instância da classe VeiculoTransporte
        /// </summary>
        public VeiculoTransporte()
        {
        }

        #endregion Constructor
  
        #region ISerializavel Members

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, NFe nfe)
        {
            writer.WriteElementString("placa", SerializationUtil.ToToken(Placa.ToUpper(), 8));
            writer.WriteElementString("UF", UF.ToString());
            if (!string.IsNullOrEmpty(RNTC))
                writer.WriteElementString("RNTC", SerializationUtil.ToToken(RNTC, 20));
        }

        #endregion
    }
}
