using System.ComponentModel;

namespace NotaFiscalNet.Core.Validacao
{
    /// <summary>
    /// Enumerador para os tipos de erros de validação.
    /// </summary>
    internal enum ChaveErroValidacao
    {
        [Description("EV0001|O valor 'NaoEspecificado' é inválido pois o campo {0} é obrigatório.")]
        EnumNaoEspecificado,

        [Description("EV0002|O valor informado não é valido.")]
        GenericArgumentException,

        [Description("EV0003|A Nota Fiscal Eletrônica não pode ser modificada por estar em modo somente-leitura.")]
        ReadOnlyClass,

        [Description("EV0004|O valor informado é inválido.\r\nInforme um valor maior ou igual a '{0}' e menor ou igual a '{1}'")]
        ValueOutOfRange,

        [Description("EV0005|O valor informado para o Cnpj não é válido.")]
        CNPJInvalido,

        [Description("EV0006|O valor informado para o Cpf não é válido.")]
        CPFInvalido,

        [Description("EV0007|O valor informado para o CNAE Fiscal não é válido.")]
        CNAEInvalido,

        [Description("EV0008|O valor informado para o Telefone não é válido.")]
        TelefoneInvalido,

        [Description("EV0009|O valor informado para o CFOP não é válido.")]
        CFOPInvalido,

        [Description("EV0010|O valor informado para a Placa não é válido.")]
        PlacaInvalida,

        [Description("EV0011|O código GTIN (Global Trade Item Number, antigo código EAN) informado não é válido.")]
        GTINInvalido,

        [Description("EV0012|O Tipo da Alíquota e da Base de Cálculo não pode ser modificada para Situação Tributária informada ({0}).")]
        NotCanChangeTipoCalculo,

        [Description("EV0013|A Base de Cálculo não pode ser modificada para Situação Tributária informada ({0}).")]
        NotCanChangeBaseCalculo,

        [Description("EV0014|O Código EX da Tabela de Incidências do IPI não é válido. O valor deve ser numérico, de 2 a 3 caracteres.")]
        CodigoExTipiInvalido,

        [Description("EV0015|A Aliquota não pode ser modificada para Situação Tributária informada ({0}).")]
        NotCanChangeAliquota,

        [Description("EV0016|O Valor não pode ser modificado para Situação Tributária informada ({0}).")]
        NotCanChangeValor,

        [Description("EV0017|A Modalidade de Base de Cálculo não pode ser modificada para Situação Tributária informada ({0}).")]
        NotCanChangeModalidadeBaseCalculo,

        [Description("EV0018|O Percentual de Redução de Base de Cálculo não pode ser modificado para Situação Tributária informada ({0}).")]
        NotCanChangePercentualReducaoBaseCalculo,

        [Description("EV0019|O Percentual de Margem de Valor Adicionado não pode ser modificado para Situação Tributária informada ({0}).")]
        NotCanChangePercentualMargemValorAdicionado,

        [Description("EV0020|A Base de Cálculo não pode ser modificada para o Tipo de Cálculo informado ({0}).")]
        NotCanChangeBaseCalculoTipoCalculo,

        [Description("EV0021|A Aliquota não pode ser modificada para o Tipo de Cálculo informado ({0}).")]
        NotCanChangeAliquotaTipoCalculo,

        [Description("EV0022|A Quantidade não pode ser modificada para o Tipo de Cálculo informado ({0}).")]
        NotCanChangeQuantidadeTipoCalculo,

        [Description("EV0023|O Valor por Unidade não pode ser modificado para o Tipo de Cálculo informado ({0}).")]
        NotCanChangeValorUnidadeTipoCalculo,

        [Description("EV0025|O campo '{0}' não foi informado.")]
        CampoNaoPreenchido,

        [Description("EV0026|O valor informado para o CEP não é válido.")]
        CEPInvalido,

        [Description("EV0027|A quantidade de itens é inválida.\r\nInforme mais de '{0}' itens nesta coleção.")]
        CollectionMinValue,

        [Description("EV0028|A quantidade de itens é inválida.\r\nInforme menos de '{0}' itens nesta coleção.")]
        CollectionMaxValue,

        [Description("EV0029|O campo Cpf ou o campo Cnpj não foi informado.")]
        CPFouCNPJObrigatorio,

        [Description("EV0030|O código RENAVAM informado não é válido.")]
        CodigoRENAVAMInvalido,

        [Description("EV0031|O código NCM (Nomeclatura Comum do Mercosul) informado não é válido.")]
        NCMInvalido,

        [Description("EV0032|Não é possivel preencher o tipo de imposto ICMS pois já existem informações sobre o imposto ISSQN. Apenas um dos dois pode ser usado.")]
        ConflitoICMSISSQN,

        [Description("EV0033|Não é possivel preencher o tipo de imposto IPI pois já existem informações sobre o imposto ISSQN. Apenas um dos dois pode ser usado.")]
        ConflitoIPIISSQN,

        [Description("EV0034|Não é possivel preencher o tipo de imposto II pois já existem informações sobre o imposto ISSQN. Apenas um dos dois pode ser usado.")]
        ConflitoIIISSQN,

        [Description("EV0035|Não é possivel preencher o tipo de imposto ISSQN pois já existem informações sobre o imposto ICMS. Apenas um dos dois pode ser usado.")]
        ConflitoISSQNICMS,

        [Description("EV0036|Não é possivel preencher o tipo de imposto ISSQN pois já existem informações sobre o imposto IPI. Apenas um dos dois pode ser usado.")]
        ConflitoISSQNIPI,

        [Description("EV0037|Não é possivel preencher o tipo de imposto ISSQN pois já existem informações sobre o imposto II. Apenas um dos dois pode ser usado.")]
        ConflitoISSQNII,

        [Description("EV0038|O valor informado para Inscrição Estadual do Destinatário é inválido.")]
        InscricaoEstadualDestinatario,

        [Description("EV0039|O CST informado não é permitido no contexto.")]
        CstNaoPermitidoNoContexto,

        [Description("EV0040|O código NVE (Nomeclatura de Valor Aduaneiro e Estatístico) informado não é válido.")]
        NVEInvalido,

        [Description("EV9000|Exceção não tratada.")]
        ExcecaoNaoTratada
    }
}