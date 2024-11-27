using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Transactions;

class Program
{
    struct Carro
    {
        public string placa, horarioEntrada, horarioSaida;
        public int[] intHorarioSaida;
        public int[] intHorarioEntrada;
    }

    string placaProvisoria, horaProvisoria;
    int menu, i = 0, identificador;
    bool validacao = true;
    static void Main(string[] args)
    {
        Program p = new Program();
        p.Start();
    }
    void Start()
    {
        Carro[] carros = new Carro[3];
        for(int i = 0; i < carros.Length; i++){
            carros[i].intHorarioEntrada = new int[2];
            carros[i].intHorarioSaida = new int[2];
        }

        Console.WriteLine("Bem vindo ao estacionamento privativo da Dio!\n\nFique atento: As horas devem ser escritas da forma tradicional, ex: 17:30.\n");

        do
        {

            Console.WriteLine("1 - Estacionar novo veiculo\n2 - Retirar veiculo\n3 - Exibir Relatorio\n4 - Sair");
            menu = Convert.ToInt16(Console.ReadLine());
            switch (menu)
            {
                case 1:
                    // incluir carro
                    if (i < carros.Length)
                    {   
                        do{
                        Console.WriteLine("Quantas horas?\n");
                        carros[i].horarioEntrada = Console.ReadLine();
                        if(carros[i].horarioEntrada.Length != 5)
                        {
                            Console.WriteLine("Horario invalido! Formatação aceita: 00:00\n");
                            validacao = true;
                            Console.WriteLine("Quantas horas?\n");
                            carros[i].horarioEntrada = Console.ReadLine();
                        } else validacao = false;
                        } while (validacao);
                        Console.WriteLine(carros[i].horarioEntrada);//para teste
                        Console.WriteLine("Qual a placa do veiculo que será estacionado?\n");
                        carros[i].placa = Console.ReadLine();
                        if(carros[i].placa.Length != 7){
                        do{
                            Console.WriteLine("Placa inválida!");
                            Console.WriteLine("Qual a placa do veiculo que será estacionado?\n");
                            carros[i].placa = Console.ReadLine();
                        }while(carros[i].placa.Length != 7);
                        }
                        i++;
                    }
                    else {
                        Console.WriteLine("Estacionamento cheio, espere alguem sair.\n");
                    }   
                    validacao = true;
                    break;
                    
                case 2:
                    //retirar carro
                    validacao = true;
                    Console.WriteLine("Quantas horas?\n");
                    horaProvisoria = Console.ReadLine();
                    do
                    {
                        Console.WriteLine("Qual a placa do veiculo que será retirado?\n");
                        placaProvisoria = Console.ReadLine();
                        Console.WriteLine("Buscando placa "+placaProvisoria+"...\n");
                        for(int x = 0; x < carros.Length; x++)
                        {
                            if (carros[x].placa == placaProvisoria)
                            {
                                carros[x].horarioSaida = horaProvisoria;
                                validacao = false;
                                
                                Console.WriteLine("O veiculo de placa " + carros[x].placa + " entrou as " + carros[x].horarioEntrada + /*
                                */" e e está sainda as " + carros[x].horarioSaida + "\nValor a ser pago: "+/*
                                */Pagamento(SplitHora(carros[x].horarioEntrada), SplitMinuto(carros[x].horarioEntrada), SplitHora(carros[x].horarioSaida), SplitMinuto(carros[x].horarioSaida)));

                                break;
                            }
                        } 
                            if(validacao)
                            {
                                Console.WriteLine("Placa não encontrada...");
                                validacao = false;
                            }
                    } while (validacao);
                    validacao = true;
                    break;
                case 3:
                    //exibir relatorio
                    for(int x = 0; x < carros.Length; x++)
                    {
                        if(carros[x].placa != null)
                        Console.WriteLine("Placa "+carros[x].placa+" foi entrou as "+(carros[x].horarioEntrada)+"\n");
                    }
                    break;
                case 4:

                    Console.WriteLine("Programa Encerrado...");
                    validacao = false;
                    break;

                default:

                    Console.WriteLine("Entrada invalida, tente novamente.");
                    validacao = true;
                    break;
            }


        } while (validacao);
    }
    int SplitHora(string hora)
    {
        string[] partes = hora.Split(':');
        int horas = int.Parse(partes[0]);
        
        return horas;
    }
    int SplitMinuto(string hora)
    {
        string[] partes = hora.Split(':');
        int minutos = int.Parse(partes[1]);
        
        return minutos;
    }
    double Pagamento(int horaEntrada, int minutoEntrada, int horaSaida, int minutoSaida)
    {
        int hora = horaSaida - horaEntrada; 
        int minuto = minutoSaida - minutoEntrada;
        //taxa inicial + horas + fracao
        int valorTotal = 5 + 12*hora + 4*(minuto/15);
        return valorTotal;
    }
    void ValidarHora()
    {
        do{
        Console.WriteLine("Quantas horas?");
        Console.ReadLine();
        }while(TimeSpan.TryParse(Console.ReadLine(), out _));
    }


}



