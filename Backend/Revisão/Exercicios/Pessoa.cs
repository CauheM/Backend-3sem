using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Exercicios
{
    public class Pessoa
    {
        public string Nome;
        public int Idade;

    public Pessoa(string n, int i)
        {
            Nome = n;
            Idade = i;
        }

         public void EscolherIdade(int id = 0)
        {
            if (id > 0)
            {
                Idade = id;
            }
            else
            {
                Console.WriteLine("Idade invalida");
            }

        }
    public void ExibirDados()
        {
     
         Console.WriteLine($"Meu nome {Nome}");
         Console.WriteLine($"Minha Idade é {Idade}");

        }

    public void Apresenta()
    {
        Console.WriteLine($"Olá, meu nome é {Nome}");
    }

    public void Apresentar(string sobrenome)
    {
        Console.WriteLine($"Olá, meu nome é {Nome} {sobrenome}");
    }



    }


}