using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotaFiscalNet.Core.Utils
{
    public class CalculadorFusoHorario
    {
        private readonly TimeZoneInfo _fusoHorarioGmtMenos3 = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        private readonly TimeZoneInfo _fusoHorarioGmtMenos4 = TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time");
        private readonly TimeZoneInfo _fusoHorarioGmtMenos5 = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        private readonly TimeZoneInfo _fusoHorario;
        private readonly TimeSpan _fusoHorarioOffset;

        public CalculadorFusoHorario(INFe nfe)
        {
            _fusoHorario = ObtemFusoHorarioDocumentoFiscal(nfe);
            _fusoHorarioOffset = ObtemOffsetFusoHorario(_fusoHorario);
        }

        public bool EstaEmPeriodoHorarioVerao(DateTime data)
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

        private TimeSpan ObtemOffsetFusoHorario(TimeZoneInfo fusoHorario)
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
