using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace UnhackedBank;

public class Banco
{
    private HashSet<Agencia> _agencias = new HashSet<Agencia>(100);
    private uint NumeroDeAgencias = 0;

    private const uint NumeroMaximoAgencias = 10;

    public Agencia? CriarAgencia()
    {
        ++NumeroDeAgencias;
        string numeroAgencia = NumeroDeAgencias.ToString();
        Agencia agenciaUB = new Agencia(numeroAgencia);
        if (_agencias.Count < NumeroMaximoAgencias)
        {
            _agencias.Add(agenciaUB);
            return agenciaUB;
        }
        return null;
    }

    public Agencia? CriarNovaAgencia()
    {
        var agenciaComCapacidade = _agencias.FirstOrDefault(a => a.TemCapacidade());
        if (agenciaComCapacidade is null)
        {
            var novaAgencia = CriarAgencia();
            return novaAgencia;
        }
        return agenciaComCapacidade;
    }

    public decimal ObterSaldoAgencia(string numeroAgencia)
    {
        var agencia = _agencias.FirstOrDefault(a => a.NumeroAgencia == numeroAgencia);
        if (agencia is not null)
            return agencia.ObterSaldoPorAgencia();

        return -1;
    }

    public Agencia? ObterAgencia(string numeroAgencia)
    {
        return _agencias.FirstOrDefault(a => a.NumeroAgencia == numeroAgencia);
    }

    public decimal ObterSaldoTotalBanco()
    {
        return _agencias.Sum(a => a.ObterSaldoPorAgencia());
    }

    public Conta? MigrarConta(string numeroConta, string numeroAgencia, string numeroNovaAgencia, Banco banco)
    {
        var agencia = _agencias.FirstOrDefault(a => a.NumeroAgencia == numeroAgencia);
        if (agencia is null)
            throw new InvalidOperationException("Agencia de origem não encontrada.");

        var agenciaNova = _agencias.FirstOrDefault(a => a.NumeroAgencia == numeroNovaAgencia);
        if (agenciaNova is null)
            throw new InvalidOperationException("Agencia de destino não encontrada. ");

        var agenciaNovaTemCapacidade = agenciaNova.TemCapacidade();
        if (!agenciaNovaTemCapacidade)
        {
            throw new InvalidOperationException("Não há mais espaço para clientes novos na agencia escolhida. ");
        }

        var conta = agencia.ObterConta(numeroConta, numeroAgencia);
        var cliente = agencia.ObterCliente(numeroConta, numeroAgencia);

        if (conta is null)
            throw new InvalidOperationException("A conta não foi encontrada. ");

        if (cliente is null)
            throw new InvalidOperationException("O cliente não foi encontrado. ");


        decimal saldoRestante = conta.Saldo;
        agencia.EncerrarConta(numeroConta, numeroAgencia);


        var novaConta = agenciaNova.CriarConta(cliente, banco);
        novaConta?.Depositar(saldoRestante);
        return novaConta;

    }
}



