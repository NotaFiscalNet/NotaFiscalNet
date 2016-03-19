//using System;

//namespace NotaFiscalNet.Core.Validacao.Validators
//{
//    internal class ReferenciaDocFiscalValidator : BaseValidator
//    {
//        public override void Validate(ValidationContext context, FieldMember field)
//        {
//            if (field.Source is ReferenciaDocFiscalNFe)
//            {
//                ReferenciaDocFiscalNFe referencia = (ReferenciaDocFiscalNFe)field.Source;
//                object value = field.GetValue();

//                if (field.Property.Name == "ChaveAcessoNFe")
//                    if (string.IsNullOrEmpty(value.ToString()))
//                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));
//            }

//            if (field.Source is ReferenciaDocumentoFiscalCte)
//            {
//                ReferenciaDocumentoFiscalCte referencia = (ReferenciaDocumentoFiscalCte)field.Source;
//                object value = field.GetValue();

//                if (field.Property.Name == "ReferenciaCte")
//                    if (string.IsNullOrEmpty(value.ToString()))
//                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));
//            }

//            if (field.Source is ReferenciaDocumentoFiscalNotaFiscal)
//            {
//                ReferenciaDocumentoFiscalNotaFiscal referencia = (ReferenciaDocumentoFiscalNotaFiscal)field.Source;
//                object value = field.GetValue();

//                if (field.Property.Name == "UF")
//                    if (referencia.CodigoModeloDocFiscal == "01")
//                        if (((UfIBGE)value) == UfIBGE.NaoEspecificado)
//                            context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

//                if (field.Property.Name == "MesAnoEmissao")
//                    if (referencia.CodigoModeloDocFiscal == "01")
//                        if ((DateTime)value <= DateTime.MinValue)
//                            context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

//                if (field.Property.Name == "CNPJ")
//                    if (referencia.CodigoModeloDocFiscal == "01")
//                        if (string.IsNullOrEmpty(value.ToString()))
//                            context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

//                if (field.Property.Name == "CodigoModeloDocumentoFiscal")
//                    if (!(value.ToString() == "55" || value.ToString() == "01"))
//                        context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

//                if (field.Property.Name == "SerieNf")
//                    if (referencia.CodigoModeloDocFiscal == "01")
//                        if ((int)value == 0)
//                            context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));

//                if (field.Property.Name == "NumeroNf")
//                    if (referencia.CodigoModeloDocFiscal == "01")
//                        if ((int)value == 0)
//                            context.Add(ErroValidacao.Create(ChaveErroValidacao.CampoNaoPreenchido, context.Path.ToString(), field.Property.Name));
//            }
//        }
//    }
//}