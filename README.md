# Price Quote Alert

Pequeno projeto designado a vigiar o preço de um ativo.

## Linguagens / Frameworks / API

C# (.Net core 3.0)

[Moq](https://github.com/Moq/moq4/wiki/Quickstart)

[MSTest](https://docs.microsoft.com/pt-br/dotnet/core/testing/unit-testing-with-mstest)

[SendGrid](https://sendgrid.com/)

[AlphaVantage](https://www.alphavantage.co/)

## Fazendo build

`detnet build`

## Testes

`dotnet test`

## Execução

`PriceQuotationApp.exe PETR4 22.40 23.40`

### Observações

App.Config -> será necessário criar um com os parametros dos respectivos sites/API citadas a cima:

    <add key="apikey" value="*" /> (SendGrid API Key)

    <add key="apikeyAV" value="*"/> (AlphaVantage API Key)

    <add key="from" value="*"/> (MailMessage [from])

    <add key="to" value="*"/> (MailMessage [to])

    <add key="smtp" value="*"/> (SMTP Host)
