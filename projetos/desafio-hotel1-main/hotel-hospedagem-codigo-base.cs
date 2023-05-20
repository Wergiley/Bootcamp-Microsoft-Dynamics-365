using System;

public class Pessoa
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public int Idade { get; set; }
}

public class Suite
{
    public int Numero { get; set; }
    public int Capacidade { get; set; }
    public decimal ValorDiaria { get; set; }
}

public class Reserva
{
    public int Numero { get; set; }
    public DateTime DataEntrada { get; set; }
    public DateTime DataSaida { get; set; }
    public Pessoa Hóspede { get; set; }
    public Suite Suite { get; set; }

    public int QuantidadeHospedes()
    {
        return Hóspede.Idade >= 18 ? 1 : 0;
    }

    public decimal ValorTotal()
    {
        var qtdDias = (DataSaida - DataEntrada).Days;
        var valorDiaria = Suite.ValorDiaria;
        decimal valorTotal;

        if (qtdDias > 10)
        {
            valorTotal = (decimal) qtdDias * valorDiaria * 0.9m;
        }
        else
        {
            valorTotal = (decimal) qtdDias * valorDiaria;
        }

        return valorTotal;
    }
}

public class Program
{
    public static void Main()
    {
        var pessoa = new Pessoa
        {
            Nome = "João da Silva",
            CPF = "123.456.789-00",
            Idade = 30
        };

        var suite = new Suite
        {
            Numero = 101,
            Capacidade = 2,
            ValorDiaria = 150.0m
        };

        var reserva = new Reserva
        {
            Numero = 1,
            DataEntrada = DateTime.Parse("2023-05-10"),
            DataSaida = DateTime.Parse("2023-05-20"),
            Hóspede = pessoa,
            Suite = suite
        };

        Console.WriteLine("Dados da Reserva:");
        Console.WriteLine($"Hóspede: {reserva.Hóspede.Nome} ({reserva.Hóspede.Idade} anos)");
        Console.WriteLine($"Suíte: {reserva.Suite.Numero}");
        Console.WriteLine($"Período: {reserva.DataEntrada.ToShortDateString()} - {reserva.DataSaida.ToShortDateString()}");
        Console.WriteLine($"Valor Total: {reserva.ValorTotal():C}");
    }
}
