/*
 O Banco UB é pequeno e tem a capacidade de gerenciar apenas 10 Clientes.
 No sistema que vamos construir o Gerente deverá ser capaz de:

  - Abrir Conta
  - Depositar valores
  - Sacar valores
  - Transferir valores entre contas da UB
  - Alterar o Endereço do Cliente
    
*/
using UnhackedBank;

Banco BancoUB = new Banco();
bool sair = false;

do
{
  Console.WriteLine("1 - Criar conta. ");
  Console.WriteLine("2 - Depositar. ");
  Console.WriteLine("3 - Sacar. ");
  Console.WriteLine("4 - Fazer transferencia. ");
  Console.WriteLine("5 - Alterar endereco do cliente. ");
  Console.WriteLine("6 - Encontrar cliente. ");
  Console.WriteLine("7 - Encontrar conta. ");
  Console.WriteLine("8 - Encerrar conta. ");
  Console.WriteLine("9 - Sair. ");
  var operacao = Console.ReadLine();

  if (operacao == "1")
  {
    Console.WriteLine("Digite seu nome. ");
    string nome = Console.ReadLine();
    Console.WriteLine("Digite o numero de seu documento ou o de sua empresa. ");
    string documento = Console.ReadLine();
    var endereco = ObterDadosEndereco();

    Cliente cliente = new Cliente(nome, documento, endereco);

    var conta = BancoUB.CriarConta(cliente);

    if (conta is not null)
    {
      var foiAdicionado = BancoUB.AdicionarConta(conta);
      if (foiAdicionado)
      {
        Console.WriteLine("A conta foi adicionada com sucesso");
      }
      else
      {
        Console.WriteLine("Não pudemos criar a conta - numero maximo de clientes foi atingido. ");
      }
    }
    else
    {
      Console.WriteLine("Conta ja existe. Redirecionando ao Menu. ");
      continue;
    }
  }
  else if (operacao == "2")
  {
    Console.WriteLine("Digite seu documento. ");
    string documento = Console.ReadLine();
    var conta = BancoUB.ObterConta(documento);
    if (conta is null)
    {
      Console.WriteLine("O documento passado nao pertence a nenhuma conta. ");
      Console.WriteLine("Redirecionando ao Menu. ");
      continue;
    }
    Console.WriteLine("Digite o valor a ser depositado");
    decimal deposito = decimal.Parse(Console.ReadLine());
    var foiDepositado = conta.Depositar(deposito);
    if (foiDepositado)
    {
      Console.WriteLine($"Deposito feito com sucesso. Seu saldo atual e de {conta.Saldo} ");
    }
    else
    {
      Console.WriteLine("Valor de deposito invalido. ");
      Console.WriteLine("Redirecionando ao Menu. ");
      continue;
    }
  }

  else if (operacao == "3")
  {
    Console.WriteLine("Digite seu documento. ");
    string documento = Console.ReadLine();
    var conta = BancoUB.ObterConta(documento);
    if (conta is not null)
    {
      Console.WriteLine("Digite o valor a ser sacado. ");
      decimal valorSaque = decimal.Parse(Console.ReadLine());
      var FoiSacado = conta.Sacar(valorSaque);
      if (FoiSacado)
      {
        Console.WriteLine($"Saque realizado com sucesso. Seu saldo atua é {conta.Saldo}");
      }
      else
      {
        Console.WriteLine("Saldo insuficiente para este valor de saque. Voltando ao Menu. ");
        continue;
      }
    }
  }

  else if (operacao == "4")
  {
    Console.WriteLine("Digite o seu documento. ");
    string documentoContaTitular = Console.ReadLine();
    var contaTitular = BancoUB.ObterConta(documentoContaTitular);
    if (contaTitular is null)
    {
      Console.WriteLine("O documento passado nao pertence a nenhuma conta. ");
      Console.WriteLine("Redirecionando ao Menu. ");
      continue;
    }

    Console.WriteLine("Digite o documento da conta para qual deseja fazer transferencia. ");
    string documentoContaRecebedora = Console.ReadLine();
    var contaRecebedora = BancoUB.ObterConta(documentoContaRecebedora);
    if (contaRecebedora is null)
    {
      Console.WriteLine("O documento passado nao pertence a nenhuma conta. ");
      Console.WriteLine("Redirecionando ao Menu. ");
      continue;
    }
    Console.WriteLine("Digite o valor a ser transferido. ");
    decimal valortrasnferencia = decimal.Parse(Console.ReadLine());
    var foiTransferido = contaTitular.Transferir(contaRecebedora, valortrasnferencia);
    if (foiTransferido)
    {
      Console.WriteLine($"Transferencia feita com sucesso. ");
    }
    else
    {
      Console.WriteLine("Valor a ser transferido maior que o saldo. ");
      Console.WriteLine("Redirecionando ao Menu. ");
      continue;
    }
  }

  else if (operacao == "5")
  {
    Console.WriteLine("Digite o documento do cliente do qual deseja mudar o endereco. ");
    string documento = Console.ReadLine();
    var cliente = BancoUB.ObterCliente(documento);
    if (cliente is not null)
    {
      Console.WriteLine("Alterando o endereco. ");
      var novoEndereco = ObterDadosEndereco();
      cliente.MudarEndereco(novoEndereco);
      Console.WriteLine($"Este é o novo endereco {cliente.Endereco}");
    }
    else
    {
      Console.WriteLine("Este cliente nao existe. Redirecionando ao Menu. ");
      continue;
    }
  }

  else if (operacao == "6")
  {
    Console.WriteLine("Digite seu documento. ");
    string documento = Console.ReadLine();
    var cliente = BancoUB.ObterCliente(documento);
    if (cliente is not null)
    {
      Console.WriteLine($"O cliente encontrado e {cliente}");
    }
    else
    {
      Console.WriteLine("O cliente nao existe. ");
      Console.WriteLine("Voltando ao menu. ");
      continue;
    }

  }

  else if (operacao == "7")
  {
    Console.WriteLine("Digite seu documento. ");
    string documento = Console.ReadLine();
    var conta = BancoUB.ObterConta(documento);
    if (conta is not null)
    {
      Console.WriteLine($"A conta encontrada e {conta}");
    }
    else
    {
      Console.WriteLine("O cliente nao existe. ");
      Console.WriteLine("Voltando ao menu. ");
      continue;
    }

  }

  else if (operacao == "8")
  {
    Console.WriteLine("Ao remover uma conta, todo o saldo restante será sacado automaticamente. ");
    Console.WriteLine("Se deseja continuar mesmo assim, digite 1. ");
    string continuar = Console.ReadLine();
    if (continuar != "1")
      continue;

    Console.WriteLine("Digite o documento da conta que deseja encerrar. ");
    string documento = Console.ReadLine();
    var contaFoiEncerrada = BancoUB.EncerrarConta(documento);
    if (contaFoiEncerrada == -1)
    {
      Console.WriteLine("A conta nao existe. ");
      Console.WriteLine("Voltando ao menu. ");
      continue;
    }
    else if (contaFoiEncerrada == 0)
    {
      Console.WriteLine("Conta não possuía saldo e foi encerrada com sucesso. ");
    }
    else if (contaFoiEncerrada > 0)
    {
      Console.WriteLine($"Foi-se feito saque de {contaFoiEncerrada} reais e conta foi encerrada com sucesso. ");
    }
  }

  else if (operacao == "9")
  {
    sair = true;
  }
} while (sair == false);



Endereco ObterDadosEndereco()
{
  Console.WriteLine("Digite o Logradouro. ");
  string logradouro = Console.ReadLine();
  Console.WriteLine("Digiteo o numero do local. ");
  string numero = Console.ReadLine();
  Console.WriteLine("Digite o complemento. ");
  string complemento = Console.ReadLine();
  Console.WriteLine("Digite o Bairro. ");
  string bairro = Console.ReadLine();
  Console.WriteLine("Digite o CEP ");
  string cep = Console.ReadLine();
  Console.WriteLine("Digite o Munucipio. ");
  string municipio = Console.ReadLine();
  Console.WriteLine("Digite o Estado. ");
  string estado = Console.ReadLine();
  Endereco endereco = new Endereco(logradouro, numero, complemento, bairro, cep, municipio, estado);
  return endereco;
}

