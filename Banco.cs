namespace UnhackedBank;
public class Banco
{
    private List<Conta> _contas = new List<Conta>(100);
    private const int NumContaMaxima = 10;
    public const string NumeroAgencia = "0001";

    public uint NumeroConta = 0;



    public Conta CriarConta(Cliente cliente)
    {
        foreach (Conta contaIgual in _contas)
        {
            if (contaIgual.Cliente.Documento == cliente.Documento)
            {
                return null;
            }
        }
        Conta conta = new Conta(NumeroAgencia, ++NumeroConta, cliente);
        return conta;
    }
    public bool AdicionarConta(Conta conta)
    {
        if (_contas.Count < NumContaMaxima)
        {
            _contas.Add(conta);
            return true;
        }
        return false;
    }
    public Conta ObterConta(string documento)
    {
        foreach (Conta conta in _contas)
        {
            if (conta.Cliente.Documento == documento)
            {
                return conta;
            }
        }
        return null;
    }
    public Cliente ObterCliente(string documento)
    {
        var conta = ObterConta(documento);
        if (conta is not null)
        {
            return conta.Cliente;
        }
        return null;
    }

    public decimal EncerrarConta(string documento)
    {
        var conta = ObterConta(documento);
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

        }
        return -1;
    }



}