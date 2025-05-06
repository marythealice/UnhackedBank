namespace UnhackedBank;
public class Agencia
{
    private HashSet<Conta> _contas = new HashSet<Conta>(100);

    private const uint NumeroMaximoContas = 1;
    private uint NumeroConta;
    public string NumeroAgencia { get; }

    public Agencia(string numeroAgencia)
    {
        NumeroAgencia = numeroAgencia;

    }

    public Conta? CriarConta(Cliente cliente, Banco banco)
    {
        var agenciaTemCapacidade = TemCapacidade();
        if (agenciaTemCapacidade)
        {
            ++NumeroConta;
            string numeroConta = NumeroConta.ToString();
            Conta conta = new Conta(NumeroAgencia, numeroConta, cliente);
            if (conta is null)
            {
                return null;
            }
            _contas.Add(conta);
            return conta;
        }
        var novaAgencia = banco.CriarAgencia() ?? throw new InvalidOperationException("Limite de agencias atingido. ");

        if (novaAgencia is not null)
        {
            var conta = novaAgencia.CriarConta(cliente, banco);
            if (conta is null)
            {
                return null;
            }
            novaAgencia._contas.Add(conta);
            return conta;
        }
        return null;
    }

    public bool TemCapacidade()
    {
        return NumeroMaximoContas > _contas.Count;
    }
    public Conta? ObterConta(string numeroConta, string numeroAgencia)
    {
        var conta = _contas.FirstOrDefault(c => c.ContaPertenceAoCliente(numeroConta, numeroAgencia));
        if (conta is not null)
        {
            return conta;
        }
        return null;
    }
    public Cliente? ObterCliente(string numeroConta, string numeroAgencia)
    {
        var conta = ObterConta(numeroConta, numeroAgencia);
        if (conta is not null)
        {
            return conta.Cliente;
        }
        return null;
    }

    public decimal ObterSaldoPorConta(string numeroConta, string numeroAgencia)
    {
        var conta = _contas.FirstOrDefault(c => c.ContaPertenceAoCliente(numeroConta, numeroAgencia));
        if (conta is not null)
        {
            return conta.ObterSaldo();
        }
        return -1;
    }

    public decimal ObterSaldoPorAgencia()
    {
        return _contas.Sum(c => c.Saldo);
    }


    public decimal EncerrarConta(string numeroConta, string numeroAgencia)
    {
        var conta = ObterConta(numeroConta, numeroAgencia);
        if (conta is not null)
        {
            var saldoRestante = conta.Saldo;
            if (saldoRestante == 0)
            {
                return 0;
            }
            else if (saldoRestante > 0)
            {
                conta.Sacar(saldoRestante);
                return saldoRestante;
            }
            _contas.Remove(conta);

        }
        return -1;
    }


    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        var agencia = obj as Agencia;
        return NumeroAgencia == agencia?.NumeroAgencia;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(NumeroAgencia);
    }

    public override string ToString() => $"{NumeroAgencia}";



}