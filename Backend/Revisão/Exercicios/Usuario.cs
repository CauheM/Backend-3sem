using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicios
{
    public class Usuario : Autenticavel
    {
    public bool Autenticar(string senha)
    {
        return senha == "123";
    } 
    }
}