using Exercicio1;

Console.Clear();


Pessoa CAuhe = new Pessoa("Cauhê", 16);
CAuhe.EscolherIdade(21);
CAuhe.ExibirDados();
CAuhe.Apresenta();
CAuhe.Apresentar("Matheus");

Console.WriteLine();

Funcionario PF = new Funcionario("Cauhê", 16, 3500);
PF.ExibirDados();
Console.WriteLine($"Salário: R$ {PF.Salario}");

Console.WriteLine();

Veiculo um1 = new Carro();
Veiculo dois2 = new Bicicleta();
um1.Mover();
dois2.Mover();

Console.WriteLine();

Usuario U = new Usuario();
Console.WriteLine($"Usuário autenticado: {U.Autenticar("123")}");

Admin A = new Admin();
Console.WriteLine($"Administrador autenticado: {A.Autenticar("admin")}");

Console.WriteLine();

Console.WriteLine($"Soma: {Calculadora.Somar(5, 3)}");
Console.WriteLine($"Multiplicação: {Calculadora.Multiplicar(5, 3)}");