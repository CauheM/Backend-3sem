using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicios
{
    public interface Autenticavel
    {
        bool Autenticar(string senha);
    }
}