namespace UnhackedBank;
public class Conta
{
    public string NumeroConta { get; private set; }
    public decimal Saldo { get; private set; }

    public string NumeroAgencia { get; private set; }
    public Cliente Cliente { get; private set; }

    public Conta(string numeroAgencia, string numeroConta, Cliente cliente)
    {
        NumeroAgencia = numeroAgencia;
        NumeroConta = numeroConta;
        Cliente = cliente;
    }
    public bool Depositar(decimal deposito)
    {
        if (deposito > 0)
        {
            Saldo += deposito;
            return true;
        }
        return false;

    }

    public bool Sacar(decimal valorSaque)
    {
        if (valorSaque <= Saldo)
        {
            Saldo -= valorSaque;
            return true;
        }
        return false;
    }
    public bool Transferir(Conta conta, decimal valorTransferencia)
    {
        if (Sacar(valorTransferencia))
        {
            conta.Depositar(valorTransferencia);
            return true;
        }
        return false;
    }

    public bool ContaPertenceAoCliente(string numeroConta, string numeroAgencia)
    {
        return NumeroConta == numeroConta && NumeroAgencia == numeroAgencia;
    }

    public decimal ObterSaldo()
    {
        return Saldo;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Conta other)
            return false;

        return NumeroConta == other.NumeroConta && NumeroAgencia == other.NumeroAgencia;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(NumeroConta, NumeroAgencia);
    }

    public override string ToString() => $"{NumeroConta}";

}