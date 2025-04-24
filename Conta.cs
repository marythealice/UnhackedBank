namespace UnhackedBank;
public class Conta
{
    public uint NumeroConta { get; }
    public decimal Saldo { get; private set; }

    public string NumeroAgencia { get; set; }
    public Cliente Cliente { get; set; }

    // construtor setando valores ao instanciar Conta
    public Conta(string numeroAgencia, uint numeroConta, Cliente cliente)
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
        if (valorSaque < Saldo)
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

    public override string ToString() => $"{NumeroConta}";

}