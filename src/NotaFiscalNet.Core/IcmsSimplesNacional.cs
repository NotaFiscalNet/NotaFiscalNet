namespace NotaFiscalNet.Core
{
    public abstract class IcmsSimplesNacional : Icms
    {
        /// <summary>
        /// Retorna ou define o Códido do ICMS pelo Simples Nacional.
        /// </summary>
        public virtual CSOSN CSOSN { get; protected set; }

        /// <summary>
        /// Retorna ou define o Códido da Situação Tributária do ICMS.
        /// </summary>
        public override SituacaoTributariaICMS CST
        {
            get => (SituacaoTributariaICMS)(int)CSOSN;
	        protected set { }
        }
    }
}