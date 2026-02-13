using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicios
{
    public class Admin : Autenticavel
    {
    public bool Autenticar(string senha)
    {
        return senha == "admin";
    }
    
    }
}