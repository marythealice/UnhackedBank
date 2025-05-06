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

Banco banco = new Banco();
var agenciaInicial = banco.CriarAgencia();
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
  Console.WriteLine("8 - Migrar conta de agência. ");
  Console.WriteLine("9 - Encerrar conta. ");
  Console.WriteLine("10 - Listagem de saldo por cliente. ");
  Console.WriteLine("11 - Listagem de saldo por agência. ");
  Console.WriteLine("12 - Listagem de saldo por banco ");
  Console.WriteLine("13 - Sair. ");
  var operacao = Console.ReadLine();

  if (operacao == "1")
  {
    Console.WriteLine("Digite seu nome. ");
    string nome = Console.ReadLine();
    Console.WriteLine("Digite o numero de seu documento ou o de sua empresa. ");
    string documento = Console.ReadLine();
    var endereco = ObterDadosEndereco();

    Cliente cliente = new Cliente(nome, documento, endereco);

    try
    {
      var conta = agenciaInicial?.CriarConta(cliente, banco);

      if (conta is not null)
      {
        Console.WriteLine("A conta foi adicionada com sucesso");
      }
      else
      {
        Console.WriteLine("Não foi possível criar a conta. Dados do cliente foram invalidos. Redirecionando ao Menu. ");
        continue;
      }
    }
    catch (InvalidOperationException error)
    {
      Console.WriteLine($"A conta não pode ser criada. Erro: {error.Message}");
    }



  }
  else if (operacao == "2")
  {
    Console.WriteLine("Digite o número da conta. ");
    string numeroConta = Console.ReadLine();
    Console.WriteLine("Digite o número da agência. ");
    string numeroAgencia = Console.ReadLine();
    var agencia = banco.ObterAgencia(numeroAgencia);
    var conta = agencia.ObterConta(numeroConta, numeroAgencia);
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
    Console.WriteLine("Digite o número da conta. ");
    string numeroConta = Console.ReadLine();
    Console.WriteLine("Digite o número da agência. ");
    string numeroAgencia = Console.ReadLine();
    var agencia = banco.ObterAgencia(numeroAgencia);
    var conta = agencia.ObterConta(numeroConta, numeroAgencia);
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
    Console.WriteLine("Digite o número da conta. ");
    string numeroConta = Console.ReadLine();
    Console.WriteLine("Digite o número da agência. ");
    string numeroAgencia = Console.ReadLine();
    var agencia = banco.ObterAgencia(numeroAgencia);
    var conta = agencia.ObterConta(numeroConta, numeroAgencia);
    if (conta is null)
    {
      Console.WriteLine("A conta referente aos dados passados não existe. ");
      Console.WriteLine("Redirecionando ao Menu. ");
      continue;
    }

    Console.WriteLine("Digite o número da conta a transferir. ");
    string numeroContaRecebedora = Console.ReadLine();
    Console.WriteLine("Digite o número da agência. ");
    string numeroAgenciaRecebedora = Console.ReadLine();
    var agenciaRecebedora = banco.ObterAgencia(numeroAgenciaRecebedora);
    var contaRecebedora = agenciaRecebedora.ObterConta(numeroConta, numeroAgencia);
    if (contaRecebedora is null)
    {
      Console.WriteLine("O documento passado nao pertence a nenhuma conta. ");
      Console.WriteLine("Redirecionando ao Menu. ");
      continue;
    }
    Console.WriteLine("Digite o valor a ser transferido. ");
    decimal valortrasnferencia = decimal.Parse(Console.ReadLine());
    var foiTransferido = conta.Transferir(contaRecebedora, valortrasnferencia);
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
    Console.WriteLine("Digite o número da conta do cliente. ");
    string numeroConta = Console.ReadLine();
    Console.WriteLine("Digite o número da agência do cliente. ");
    string numeroAgencia = Console.ReadLine();
    var agencia = banco.ObterAgencia(numeroAgencia);
    var cliente = agencia.ObterCliente(numeroConta, numeroAgencia);
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
    Console.WriteLine("Digite o número da conta. ");
    string numeroConta = Console.ReadLine();
    Console.WriteLine("Digite o número da agência. ");
    string numeroAgencia = Console.ReadLine();
    var agencia = banco.ObterAgencia(numeroAgencia);
    var cliente = agencia.ObterCliente(numeroConta, numeroAgencia);
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
    Console.WriteLine("Digite o número da conta. ");
    string numeroConta = Console.ReadLine();
    Console.WriteLine("Digite o número da agência. ");
    string numeroAgencia = Console.ReadLine();
    var agencia = banco.ObterAgencia(numeroAgencia);
    var conta = agencia.ObterConta(numeroConta, numeroAgencia);
    if (conta is not null)
    {
      Console.WriteLine($"A conta encontrada foi: Número:{conta}, Agência:{agencia}. ");
    }
    else
    {
      Console.WriteLine("A conta nao existe. ");
      Console.WriteLine("Voltando ao menu. ");
      continue;
    }

  }

  else if (operacao == "8")
  {
    Console.WriteLine("Digite o número da conta a migrar. ");
    string numeroConta = Console.ReadLine();
    Console.WriteLine("Digite o número da agência de origem. ");
    string numeroAgencia = Console.ReadLine();
    Console.WriteLine("Digite o número da agência para qual deseja migrar. ");
    string numeroAgenciaNova = Console.ReadLine();
    try
    {
      banco.MigrarConta(numeroConta, numeroAgencia, numeroAgenciaNova, banco);
    }
    catch (InvalidOperationException erro)
    {
      Console.WriteLine($"Erro no processamento: {erro.Message}");
    }
  }

  else if (operacao == "9")
  {
    Console.WriteLine("Ao remover uma conta, todo o saldo restante será sacado automaticamente. ");
    Console.WriteLine("Se deseja continuar mesmo assim, digite 1. ");
    string continuar = Console.ReadLine();
    if (continuar != "1")
      continue;

    Console.WriteLine("Digite o número da conta. ");
    string numeroConta = Console.ReadLine();
    Console.WriteLine("Digite o número da agência. ");
    string numeroAgencia = Console.ReadLine();
    var agencia = banco.ObterAgencia(numeroAgencia);
    var contaFoiEncerrada = agencia.EncerrarConta(numeroConta, numeroAgencia);
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

  else if (operacao == "10")
  {
    Console.WriteLine("Digite o número da conta. ");
    string numeroConta = Console.ReadLine();
    Console.WriteLine("Digite o número da agência. ");
    string numeroAgencia = Console.ReadLine();
    var agencia = banco.ObterAgencia(numeroAgencia);
    var conta = agencia.ObterConta(numeroConta, numeroAgencia);
    if (conta is not null)
    {
      var saldo = conta.ObterSaldo;
      {
        Console.WriteLine($"O saldo do cliente é {saldo}. ");
      }
    }
    Console.WriteLine("A conta não existe. Redirecionando ao Menu. ");
    continue;
  }
  else if (operacao == "11")
  {
    Console.WriteLine("Digite o número da agencia que deseja saber o saldo total. ");
    string numeroAgencia = Console.ReadLine();
    var saldoAgencia = banco.ObterSaldoAgencia(numeroAgencia);
    if (saldoAgencia > 0)
    {
      Console.WriteLine($"O saldo da agencia {numeroAgencia} é {saldoAgencia}");
    }
    else if (saldoAgencia == -1)
    {
      Console.WriteLine("A agencia não existe. Redirecionando ao Menu. ");
      continue;
    }
  }

  else if (operacao == "12")
  {
    var saldoTotal = banco.ObterSaldoTotalBanco();
    Console.WriteLine($"O saldo total do banco é {saldoTotal}");
  }

  else if (operacao == "13")
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

