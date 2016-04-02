## Use a *NotaFiscal.net* no Visual Studio
---------------

Como pré-requisito, você precisa do [Visual Studio 2015](https://www.visualstudio.com/downloads/download-visual-studio-vs) para iniciar um projeto de *Documentos Fiscais*.

*Passo 1.* Instale o pacote Nuget `NotaFiscalNet.Core` no *Package Manager Console*:
```
Install-Package NotaFiscalNet.Core
```

*Passo 2.* Instancie um objeto do tipo *NFe* e comece a preencher as propriedades para geração do *xml*. Use os [tutoriais](http://) disponíveis para auxílio sobre o que preencher, ou a [documentação](http://) se tiver dúvidas técnicas sobre cada campo.

```
using NotaFiscalNet.Core;

var nfe = new NFe();
nfe.Identificacao.Ambiente = TipoAmbiente.Homologacao;
nfe.Identificacao.DataEmissao = DateTime.Now;

// ...

nfe.Emitente.Nome = "Razao Social Emitente";
nfe.Emitente.NomeFantasia = "Nome Fantasia Emitente";

// ...

var produto = new Produto();
produto.Codigo = "1";
produto.Descricao = "Produto 1";
nfe.Itens.Add(produto);
```

*Passo 3.* Execute a validação dos dados informados, para garantir que o *xml* gerado passe por todas as regras necessárias para o envio. Se houver erros, é neste instante que você poderá avaliar o [resultado](http://) que dirá exatamente o que precisa ser informado.

```
ResultadoValidacao resultado = nfe.Validar();
```

*Passo 4.* Gere o *xml*!

```
string xml = nfe.Serializar();
```
