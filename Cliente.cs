namespace UnhackedBank;
public class Cliente
{
    public string Nome { get; }
    public string Documento { get; }

    public Endereco Endereco { get; private set; }

    public Cliente(string nome, string documento, Endereco endereco)
    {
        Nome = nome;
        Documento = documento;
        Endereco = endereco;
    }

    public void MudarEndereco(Endereco novoendereco)
    {
        Endereco = novoendereco;
    }

    public override string ToString() => $"{Nome}";

}