namespace UnhackedBank;

public class Endereco
{
    public string Logradoro { get; private set; }

    public string Numero { get; private set; }

    public string Complemento { get; private set; }

    public string Bairro { get; private set; }

    public string Cep { get; private set; }

    public string Municipio { get; private set; }

    public string Estado { get; private set; }

    public Endereco
    (string logradouro, string numero, string complemento, string bairro, string cep, string municipio, string estado)
    {
        Logradoro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Municipio = municipio;
        Estado = estado;
    }


}