using System.Linq;
using NotaFiscalNet.Core.Interfaces;
using NotaFiscalNet.Core.Utils;

namespace NotaFiscalNet.Core
{
    /// <summary>
    /// Representa as informações de Volume da Carga.
    /// </summary>
    public sealed class VolumeCarga : ISerializavel, IModificavel
    {
        private long _quantidadeVolumesTransportados;
        private string _especie = string.Empty;
        private string _marca = string.Empty;
        private string _numeracao = string.Empty;
        private decimal _pesoLiquido;
        private decimal _pesoBruto;
        private StringCollection _lacres = new StringCollection();

        /// <summary>
        /// [qVol] Retorna ou define a Quantidade de Volumes transportados (valor máximo
        /// 999999999999999). Opcional.
        /// </summary>
        [NFeField(FieldName = "qVol", DataType = "token", ID = "X27", Pattern = @"[0-9]{1,15}")]
        public long QuantidadeVolumes
        {
            get => _quantidadeVolumesTransportados;
	        set
            {
                ValidationUtil.ValidateLong15(value, "QuantidadeVolumes");
                _quantidadeVolumesTransportados = value;
            }
        }

        /// <summary>
        /// [esp] Retorna ou define a Espécie dos Volumes transportados. Opcional.
        /// </summary>
        [NFeField(FieldName = "esp", DataType = "token", ID = "X28", MinLength = 1, MaxLength = 60)]
        public string Especie
        {
            get => _especie;
	        set => _especie = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [marca] Retorna ou define a Marca dos volumes transportados. Opcional.
        /// </summary>
        [NFeField(FieldName = "marca", DataType = "token", ID = "X29", MinLength = 1, MaxLength = 60)]
        public string Marca
        {
            get => _marca;
	        set => _marca = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [nVol] Retorna ou define a Numeração dos volumes transportados.
        /// </summary>
        [NFeField(FieldName = "nVol", DataType = "token", ID = "X30", MinLength = 1, MaxLength = 60)]
        public string Numeracao
        {
            get => _numeracao;
	        set => _numeracao = ValidationUtil.TruncateString(value, 60);
        }

        /// <summary>
        /// [pesoL] Retorna ou define o Peso Líquido (em kg) do Volume.
        /// </summary>
        [NFeField(FieldName = "pesoL", DataType = "TDec_1203", ID = "X31", Pattern = @"0|0\.[0-9]{3}|[1-9]{1}[0-9]{0,11}(\.[0-9]{3})?")]
        public decimal PesoLiquido
        {
            get => _pesoLiquido;
	        set
            {
                ValidationUtil.ValidateTDec_1203(value, "PesoLiquido");

                _pesoLiquido = value;
            }
        }

        /// <summary>
        /// [pesoB] Retorna ou define o Peso Bruto (em kg) do Volume.
        /// </summary>
        [NFeField(FieldName = "pesoB", DataType = "TDec_1203", ID = "X32", Pattern = @"0|0\.[0-9]{3}|[1-9]{1}[0-9]{0,11}(\.[0-9]{3})?")]
        public decimal PesoBruto
        {
            get => _pesoBruto;
	        set
            {
                ValidationUtil.ValidateTDec_1203(value, "PesoBruto");
                _pesoBruto = value;
            }
        }

        /// <summary>
        /// [lacres] Retorna uma Coleção de Lacres
        /// </summary>
        [NFeField(FieldName = "lacres", ID = "X33", MinLength = 1, MaxLength = 60)]
        public StringCollection Lacres => _lacres;

        /// <summary>
        /// Retorna se a Classe foi modificada
        /// </summary>
        public bool Modificado => QuantidadeVolumes != 0L ||
                                  !string.IsNullOrEmpty(Especie) ||
                                  !string.IsNullOrEmpty(Marca) ||
                                  !string.IsNullOrEmpty(Numeracao) ||
                                  PesoLiquido != 0m ||
                                  PesoBruto != 0m ||
                                  Lacres.Modificado;

        void ISerializavel.Serializar(System.Xml.XmlWriter writer, INFe nfe)
        {
            writer.WriteStartElement("vol"); // Elemento 'vol'
            if (QuantidadeVolumes > 0L)
                writer.WriteElementString("qVol", SerializationUtil.ToToken(QuantidadeVolumes));
            if (!string.IsNullOrEmpty(Especie))
                writer.WriteElementString("esp", SerializationUtil.ToToken(Especie, 60));
            if (!string.IsNullOrEmpty(Marca))
                writer.WriteElementString("marca", SerializationUtil.ToToken(Marca, 60));
            if (!string.IsNullOrEmpty(Numeracao))
                writer.WriteElementString("nVol", SerializationUtil.ToToken(Numeracao, 60));
            if (PesoLiquido > 0m)
                writer.WriteElementString("pesoL", SerializationUtil.ToTDec_1203(PesoLiquido));
            if (PesoBruto > 0m)
                writer.WriteElementString("pesoB", SerializationUtil.ToTDec_1203(PesoBruto));
            if (Lacres.Modificado)
                foreach (var lacre in Lacres.Where(lacre => !string.IsNullOrEmpty(lacre)))
                {
                    writer.WriteStartElement("lacres"); // Elemento 'lacres'
                    writer.WriteElementString("nLacre", SerializationUtil.ToToken(lacre, 60));
                    writer.WriteEndElement(); // Elemento 'lacres'
                }
            writer.WriteEndElement(); // Elemento 'vol'
        }
    }
}