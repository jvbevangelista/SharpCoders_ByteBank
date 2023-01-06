using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace ByteBank1
{

    public class Program
    {

        static void ShowMenu()
        {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite a senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.WriteLine("Não foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Conta deletada com sucesso");
        }

        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                ApresentaConta(i, cpfs, titulares, saldos);
            }
        }

        static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaApresentar == -1)
            {
                Console.WriteLine("Não foi possível apresentar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            ApresentaConta(indexParaApresentar, cpfs, titulares, saldos);
        }

        static void ApresentarValorAcumulado(List<double> saldos)
        {
            Console.WriteLine($"Total acumulado no banco: R$ {saldos.Sum():F2}", CultureInfo.InvariantCulture);
            // saldos.Sum(); ou .Agregatte(0.0, (x, y) => x + y)
        }

        static void ApresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:F2}");
        }

        static void ManipularConta(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o CPF do titular: ");
            string cpfParaApresentar = Console.ReadLine();
            int index = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);
            
            int option;
            double saque, deposito, transferencia;

            Console.WriteLine("Escolha a operação desejada:");
            Console.WriteLine("1 - Depósito");
            Console.WriteLine("2 - Saque");
            Console.WriteLine("3 - Transferência");
            option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.Write("Digite o valor do depósito: R$ ");
                    deposito = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    saldos[index] += deposito;
                    break;
                case 2:
                    Console.Write("Digite o valor do saque: R$ ");
                    saque = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    if (saque > saldos[index])
                    {
                        Console.WriteLine("Não é possivel realizar esta operação!");
                        Console.WriteLine("Saldo indisponível!");
                        break;
                    }
                    else
                        saldos[index] -= saque;
                    break;
                case 3:
                    Console.Write("Digite o valor da transferência: R$ ");
                    transferencia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                    if (transferencia > saldos[index])
                    {
                        Console.WriteLine("Não é possivel realizar esta operação!");
                        Console.WriteLine("Saldo indisponível!");
                        break;
                    }
                    else
                        Console.Write("Digite o CPF de quem vai receber a transferência: ");
                        string cpfParaApresentar2 = Console.ReadLine();
                        int index2 = cpfs.FindIndex(cpf => cpf == cpfParaApresentar2);
                        saldos[index] -= transferencia;
                        saldos[index2] += transferencia;
                        break; 
            }

            

        }

        public static void Main(string[] args)
        {

            Console.WriteLine("Antes de começar a usar, vamos configurar alguns valores: ");

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            string login, senha;
            int option, index;

            Console.Write("Login: ");
            login = Console.ReadLine();
            Console.Write("Senha: ");
            senha = Console.ReadLine();

            if (login == "admin" && senha == "admin") 
            {
                do
                {
                    ShowMenu();
                    option = int.Parse(Console.ReadLine());

                    Console.WriteLine("-----------------");

                    switch (option)
                    {
                        case 0:
                            Console.WriteLine("Estou encerrando o programa...");
                            break;
                        case 1:
                            RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                            break;
                        case 2:
                            DeletarUsuario(cpfs, titulares, senhas, saldos);
                            break;
                        case 3:
                            ListarTodasAsContas(cpfs, titulares, saldos);
                            break;
                        case 4:
                            ApresentarUsuario(cpfs, titulares, saldos);
                            break;
                        case 5:
                            ApresentarValorAcumulado(saldos);
                            break;
                        case 6:
                            ManipularConta(cpfs, titulares, saldos);
                            break;
                    }

                    Console.WriteLine("-----------------");

                } while (option != 0);
            } else
            {
                do
                {
                    Console.WriteLine("Usuário ou senha incorretos! Tente novamente");
                    Console.Write("Login: ");
                    login = Console.ReadLine();
                    Console.Write("Senha: ");
                    senha = Console.ReadLine();
                } while (login != "admin" && senha != "admin");

                if (login == "admin" && senha == "admin") 
                {
                    ShowMenu();
                    option = int.Parse(Console.ReadLine());

                    Console.WriteLine("-----------------");

                    switch (option)
                    {
                        case 0:
                            Console.WriteLine("Estou encerrando o programa...");
                            break;
                        case 1:
                            RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                            break;
                        case 2:
                            DeletarUsuario(cpfs, titulares, senhas, saldos);
                            break;
                        case 3:
                            ListarTodasAsContas(cpfs, titulares, saldos);
                            break;
                        case 4:
                            ApresentarUsuario(cpfs, titulares, saldos);
                            break;
                        case 5:
                            ApresentarValorAcumulado(saldos);
                            break;
                        case 6:
                          ManipularConta(titulares, senhas, saldos);
                            break;
                    } while (option != 0);
                }



            }

            



        }

    }

}