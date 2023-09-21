using System;
using System.Collections.Generic;
public class Pessoa : IComparable<Pessoa>
{
    public string Nome { get; set; }
    public int Idade { get; set; }

    public Pessoa(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }

    public int CompareTo(Pessoa other)
    {
        // Comparar as pessoas com base na idade.
        return Idade.CompareTo(other.Idade);
    }

    public override string ToString()
    {
        return $"{Nome} ({Idade} anos)";
    }
}