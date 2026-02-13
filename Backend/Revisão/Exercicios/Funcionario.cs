using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicios
{
    public class Funcionario : Pessoa
    {
        
     public double Salario;

    public Funcionario(string nome, int idade, double salario)
        : base(nome, idade)
    {
        Salario = salario;
    }

    }
}