using System;
using System.Runtime.InteropServices;

namespace NotaFiscalNet.Core.Utils
{
    public class CalculadorFusoHorario
    {
	    private static readonly bool _isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

	    private readonly TimeZoneInfo _fusoHorarioGmtMenos3 =
		    TimeZoneInfo.FindSystemTimeZoneById(_isWindows ? "E. South America Standard Time" : "Brazil/East");

	    private readonly TimeZoneInfo _fusoHorarioGmtMenos4 =
		    TimeZoneInfo.FindSystemTimeZoneById(_isWindows ? "Central Brazilian Standard Time" : "Brazil/West");

	    private readonly TimeZoneInfo _fusoHorarioGmtMenos5 =
		    TimeZoneInfo.FindSystemTimeZoneById(_isWindows ? "Eastern Standard Time" : "Brazil/Acre");
	    
        private readonly TimeZoneInfo _fusoHorario;
        private readonly TimeSpan _fusoHorarioOffset;

        public CalculadorFusoHorario(INFe nfe)
        {
            _fusoHorario = ObtemFusoHorarioDocumentoFiscal(nfe);
            _fusoHorarioOffset = ObtemOffsetFusoHorario(_fusoHorario);
        }

	    private bool EstaEmPeriodoHorarioVerao(DateTime data)
        {
            return _fusoHorario.IsDaylightSavingTime(data);
        }

        public TimeSpan ObtemFusoHorarioOffset(DateTime data)
        {
            var offset = _fusoHorarioOffset;
            if (EstaEmPeriodoHorarioVerao(data))
            {
                offset = offset.Add(TimeSpan.FromHours(-1));
            }
            return offset;
        }

        private static TimeSpan ObtemOffsetFusoHorario(TimeZoneInfo fusoHorario)
        {
            return fusoHorario.BaseUtcOffset;
        }

        private TimeZoneInfo ObtemFusoHorarioDocumentoFiscal(INFe nfe)
        {
            return ObtemFusoHorarioUnidadeFederativa(nfe.Identificacao.UnidadeFederativaEmitente);
        }

        private TimeZoneInfo ObtemFusoHorarioUnidadeFederativa(UfIBGE unidadeFederativa)
        {
            switch (unidadeFederativa)
            {
                case UfIBGE.NaoEspecificado:
                    throw new ApplicationException("A Unidade Federativa do Emitente do Documento Fiscal não foi informada.");
                case UfIBGE.AC:
                    return _fusoHorarioGmtMenos5;
                case UfIBGE.AM:
                case UfIBGE.RO:
                case UfIBGE.RR:
                case UfIBGE.MT:
                case UfIBGE.MS:
                    return _fusoHorarioGmtMenos4;
	            default:
                    return _fusoHorarioGmtMenos3;
            }
        }
    }
}
