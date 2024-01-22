using System;

class Program
{
    public static void Main(string[] args)
    {
        int numJogadores;
        int quantCartelas;
        int posVencedor = 0; //posicao do vencedor no ranking
        string nome;
        char sexo;
        int idade;
        Console.WriteLine("------------Bem vindo ao Bingo!-------------\n");

        //repetindo até que o usuário digite uma quantidade de jogadores válida
        do
        {
            //pedindo a quantidade de jogadores e os dados dele
            Console.Write("Digite a quantidade de jogadores que terá na partida (2 a 5):  ");
            numJogadores = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Sejam bem-vindos jogadores! Agora insira os dados de cada jogador.");
            Console.WriteLine();

            if (numJogadores < 2 || numJogadores > 5)
            {
                Console.WriteLine("Quantidade de jogadores inválida! O mínimo de jogadores é 2 e o máximo é 5.");
            }
        } while (numJogadores < 2 || numJogadores > 5);

        //vetor para armazenar os dados dos jogadores
        Jogador[] vetorDadosJogadores = new Jogador[numJogadores];
        //vetor para armazenar a copia dos dados dos jogadores para imprimir no ranking
        Jogador[] vetorDadosJogadoresCopia = new Jogador[numJogadores];

        //comando de repetição que pede as cartelas até o número de jogadores
        for (int jogador = 0; jogador < numJogadores; jogador++)
        {
            Console.WriteLine();
            Console.Write($"Jogador(a) {jogador + 1}, qual seu nome? ");
            nome = Console.ReadLine();

            Console.Write($"{nome}, qual seu sexo? (M ou F) ");
            sexo = char.Parse(Console.ReadLine());


            Console.Write($"{nome}, qual sua idade? ");
            idade = int.Parse(Console.ReadLine());


            do
            {
                Console.Write($"{nome}, quantas cartelas você quer? ");
                Console.WriteLine();
                quantCartelas = int.Parse(Console.ReadLine());
                if (quantCartelas < 1 || quantCartelas > 4)
                {
                    Console.WriteLine("Quantidade de cartelas inválida! O mínimo de cartelas é 1 e o máximo é 4.");
                }

            } while (quantCartelas < 1 || quantCartelas > 4);//4 por ser no maximo 4 cartelas

            //criando um obj dados para armazenar todos os dados
            Jogador dadosJogador = new Jogador(nome, sexo, idade, quantCartelas, numJogadores);

            vetorDadosJogadores[jogador] = dadosJogador; //vetor com todos os dados
            vetorDadosJogadoresCopia[jogador] = dadosJogador; //vetor cópia dos dados para imprimir no ranking
            if (quantCartelas >= 1 && quantCartelas <= 4)
            {

                //gerando a quantidade de cartelas pedida
                for (int cart = 0; cart < quantCartelas; cart++)
                {
                    Cartela novaCartela = new Cartela();
                    novaCartela.GeraCartelaBingo();
                    if (cart <= dadosJogador.QuantCartelas)
                    {
                        dadosJogador.AdicionarCartela(novaCartela, cart); //adiciona cartela no obj dadosJogador
                    }
                }
                //inicializando a copia das cartelas
                dadosJogador.InicializarCartelasCopiadas();
            }
        }

        //pergunta pro usuario se quer ver as cartelas
        Console.WriteLine();
        Console.Write("Você deseja ver todas as cartelas geradas antes de começar o jogo? (S ou N)\n");
        string quero = Console.ReadLine();

        if (quero.ToUpper() == "S")
        {
            for (int i = 0; i < numJogadores; i++)
            {
                Console.WriteLine($"Cartela(s) do {i + 1}º jogador:");
                for (int j = 0; j < vetorDadosJogadores[i].QuantCartelas; j++)
                {
                    if (vetorDadosJogadores[i].CartelaJogadores[j] != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{j + 1}º Cartela:");
                        Console.WriteLine();
                        vetorDadosJogadores[i].CartelaJogadores[j].ImprimirCartela();
                        Console.WriteLine();
                    }
                }
            }
        }

        if (quero.ToUpper() == "N")
        {
            Console.WriteLine("Ok!");
            Console.WriteLine();
        }

        //Começo do jogo

        Sorteador sorteio = new Sorteador(); //criando um obj do tipo sorteador para chamar os metodos da classe
        int[] numerosSorteados = new int[75]; //vetor de sorteados
        int numeroSorteado;
        int rodada = 0; //variavel para controlar a posicao do num sorteado
        Jogador[] ranking = new Jogador[numJogadores]; //criando um vetor de ranking para armazenar os dados dos jogadores em cada posicao e dps imprimir

        int jogadoresAtivos = numJogadores; //variavel que controla o loop do jogo
        int posPerdedor = ranking.Length - 1;

        Console.WriteLine("Vamos começar o jogo!");
        Console.WriteLine();

        // enquanto tiver mais de um jogador ativo
        while (jogadoresAtivos > 1)
        {


            // sorteia o número pelo metodo
            numeroSorteado = sorteio.SortearNumero();
            numerosSorteados[rodada] = numeroSorteado; //armazena o numero sorteado no vetor
            rodada++;

            // exibe o número sorteado para todos os jogadores
            Console.WriteLine();
            Console.WriteLine($"-------------O número sorteado é {numeroSorteado}----------------");
            Console.WriteLine();

            //roda de acordo com a quant de jogadores(para pegar a posicao do jogador)
            for (int jogador = 0; jogador < numJogadores && jogadoresAtivos > 1; jogador++)
            {
                //roda enquanto tem jogador e cartela necessaria
                if (vetorDadosJogadores[jogador] != null && vetorDadosJogadores[jogador].QuantCartelas > 0)
                {

                    //roda de acordo com a quant de cartelas(para pegar a posicao da cartela)
                    for (int cartela = 0; cartela < vetorDadosJogadores[jogador].QuantCartelas; cartela++)
                    {

                        if (vetorDadosJogadores[jogador].CartelaJogadores[cartela] != null) //ignora se a cartela for nula e pula
                        {

                            string resposta;
                            string bingo;
                            string opcaoLouC;
                            Console.WriteLine($"Jogador(a) {vetorDadosJogadores[jogador].Nome}, é a sua vez!");
                            Console.WriteLine();
                            Console.WriteLine($"Jogador(a) {vetorDadosJogadores[jogador].Nome}, verifique na sua {cartela + 1}º cartela, se tem o número {numeroSorteado}: ");
                            Console.WriteLine();
                            vetorDadosJogadores[jogador].CartelaJogadores[cartela].ImprimirCartela(); //imprime a cartela pelo metodo pro jogador conferir o numero
                            Console.Write($"O número {numeroSorteado} está na cartela acima? (S ou N): ");
                            resposta = Console.ReadLine();
                            Console.WriteLine();

                            if (numerosSorteados[4] != 0) //se ja tiver sorteado 5 numeros pergunte do bingo
                            {
                                Console.WriteLine("Deseja gritar Bingo? (S ou N)");
                                Console.WriteLine("Obs: Se tiver completado alguma quina.");
                                bingo = Console.ReadLine();

                                if (bingo.ToUpper() == "S")
                                {

                                    Console.WriteLine();
                                    Console.Write("Digite o número da cartela para verificar o bingo: "); //pede a cartela
                                    int numCartelaVerificacao = int.Parse(Console.ReadLine());
                                    Console.Write("Digite 'L' para verificar uma linha ou 'C' para verificar uma coluna: "); //pergunta se é l ou c
                                    opcaoLouC = Console.ReadLine();

                                    int linhaOuColunaVerificacao = -1;
                                    bool eLinha = false;

                                    if (opcaoLouC.ToUpper() == "L") //se for linha
                                    {
                                        Console.Write("Digite o número da linha (1 a 5): ");
                                        linhaOuColunaVerificacao = int.Parse(Console.ReadLine()) - 1;
                                        eLinha = true;
                                    }
                                    else if (opcaoLouC.ToUpper() == "C") //se for coluna
                                    {
                                        Console.Write("Digite o número da coluna (1 a 5): ");
                                        linhaOuColunaVerificacao = int.Parse(Console.ReadLine()) - 1;
                                        eLinha = false;
                                    }

                                    bool bingoEncontrado = vetorDadosJogadores[jogador].VerificaBingoCartela(numCartelaVerificacao, numerosSorteados, linhaOuColunaVerificacao, eLinha); //chama o metodo que verifica o bingo na cartela expecifica
                                    if (bingoEncontrado)//se retornar true
                                    {
                                        Console.WriteLine($"Bingo! Parabéns Jogador(a) {vetorDadosJogadores[jogador].Nome}, você venceu essa rodada.");

                                        ranking[posVencedor] = vetorDadosJogadoresCopia[jogador]; //1 posicao
                                        vetorDadosJogadores[jogador] = null; // remove jogador e seus dados
                                        posVencedor++;
                                        jogadoresAtivos--; //removendo jogador da partida
                                        jogador--; //volta o loop apra nao pular jogador
                                        break; // saindo do loop do jogador atual
                                    }
                                    else
                                    {

                                        if (vetorDadosJogadores[jogador].QuantCartelas > 1) //se o jogador tiver + de uma cartela e gritar bingo errado
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Você gritou bingo errado! Sua cartela será excluída e preste mais atenção na próxima.");
                                          Console.WriteLine();
                                            vetorDadosJogadores[jogador].CartelaJogadores[cartela] = null; //excluindo a cartela do jogador naquela posicao


                                            // verifica se o jogador ainda tem outra cartela ativa
                                            bool outraCartelaAtiva = false;
                                            for (int i = 0; i < vetorDadosJogadores[jogador].QuantCartelas; i++) //percorre o vetor de cartelas do jogador
                                            {
                                                if (vetorDadosJogadores[jogador].CartelaJogadores[i] != null)
                                                {
                                                    outraCartelaAtiva = true;
                                                    break; //se percorrer no loop e tiver outra cartela ativa ok
                                                }
                                            }

                                          if(!outraCartelaAtiva){
                                            Console.WriteLine(vetorDadosJogadores[jogador].Nome + ", você não tem mais cartelas, será desclassificado(a)!\n");
                                            ranking[posPerdedor] = vetorDadosJogadoresCopia[jogador]; //joga o jogador pra ultima posicao
                                            posPerdedor--;
                                            vetorDadosJogadores[jogador] = null; // removendo o  jogador da partida
                                            jogadoresAtivos--;
                                            jogador--;
                                            break;
                                          }
                                        }

                                        else
                                        {


                                            Console.WriteLine("Você gritou bingo errado! Como você tem apenas uma cartela, você será desclassificado automaticamente.");


                                            ranking[posPerdedor] = vetorDadosJogadoresCopia[jogador]; //joga o jogador pra ultima posicao
                                            posPerdedor--;
                                            vetorDadosJogadores[jogador] = null; // removendo o  jogador da partida
                                            jogadoresAtivos--;
                                            jogador--;
                                            break;
                                        }
                                    }
                                }
                                else if (bingo.ToUpper() == "N")//se nao quiser gritar bingo segue o jogo
                                {
                                    Console.WriteLine("Ok! Segue o jogo...");
                                    Console.WriteLine();
                                }
                            }

                            //resposta se tem ou não o numero na cartela
                            if (resposta.ToUpper() == "S") //se falar que tem na cartela, substitui
                            {
                                Console.WriteLine("Bacana! Seu número será marcado.");
                                Console.WriteLine();
                                if (vetorDadosJogadores[jogador].CartelaJogadores[cartela] != null)
                                { //se a cartela nao for nula, substitui o numero
                                    vetorDadosJogadores[jogador].CartelaJogadores[cartela].SubstituirNumero(numeroSorteado);
                                }
                            }
                            else if (resposta.ToUpper() == "N")//se nao tiver, segue o jogo
                            {
                                Console.WriteLine();
                                Console.WriteLine("Que pena! Segue o jogo...");
                                Console.WriteLine();
                                //nada acontece
                            }
                            else
                            {
                                Console.WriteLine("Resposta inválida, por favor, digite S para sim ou N para não.");
                                cartela--; // Volta para a mesma cartela para dar a resposta novamente
                            }
                        }

                        //ranking restantes



                    }
                }
            }
        }

        for (int jogador = 0; jogador < numJogadores; jogador++)
        {
            if (vetorDadosJogadores[jogador] != null)
            {
                ranking[posVencedor] = vetorDadosJogadoresCopia[jogador];
                posVencedor++;
            }
        }



        Console.WriteLine();
        Console.WriteLine("O jogo acabou! Obrigado por jogar Ed.\n");

        //ranking
        Console.WriteLine("Ranking dos jogadores: \n");
        for (int i = 0; i < numJogadores; i++)
        {

            if (ranking[i] != null)
            {
                Console.WriteLine($"{i + 1}º Lugar:\n");
                Console.WriteLine($"Nome: {ranking[i].Nome}");
                Console.WriteLine($"Idade: {ranking[i].Idade} anos");
                Console.WriteLine($"Sexo: {ranking[i].Sexo}\n");
            }
        }
    }
}



class Jogador
{
    //dados jogador
    public string Nome;
    public char Sexo;
    public int Idade;
    public int QuantCartelas;
    public int NumJogadores;

    public Cartela[] CartelaJogadores; //cartelas que serao alteradas
    public Cartela[] CartelasCopias;//copia das primeiras cartelas geradas

    public Jogador(string nome, char sexo, int idade, int quantCartelas, int numJogadores) //construtor para acessar os dados
    {
        Nome = nome;
        Sexo = sexo;
        Idade = idade;
        QuantCartelas = quantCartelas;
        NumJogadores = numJogadores;
        CartelaJogadores = new Cartela[quantCartelas];
        CartelasCopias = new Cartela[quantCartelas];
    }

    //metodo que assossia o jogador as suas respectivas cartelas
    public void AdicionarCartela(Cartela cartela, int posicao)
    {
        CartelaJogadores[posicao] = cartela;
    }


    //metodo que verifica o bingo
    public bool VerificaBingoCartela(int numeroCartela, int[] numerosSorteados, int linhaOuColuna, bool ehLinha)
    {
        int numDaCartela = numeroCartela - 1; //diminui -1 para acessar a posicao real da cartela no vetor
        if (numDaCartela >= 0 && numDaCartela < CartelaJogadores.Length && CartelaJogadores[numDaCartela] != null) //se o numero for maior que 0, estiver entre os parametros de cartela e a cartela nao for nula entre
        {
            if (CartelasCopias[numDaCartela] != null)
            {
                bool bingoEncontrado = true; //inicialmente bingo é true

                if (ehLinha) //se o usuario escolher para verificar a linha
                {
                    int[] numerosDaLinha = new int[5]; //armazena os numeros da linha no vetor
                    for (int i = 0; i < 5; i++)
                    {
                        numerosDaLinha[i] = CartelasCopias[numDaCartela].ObterMatrizCartela()[linhaOuColuna, i];
                    }//armazena no vetor linha os numeros da linha expecificada

                    foreach (int numero in numerosDaLinha)
                    {
                        //percorre a linha comparando numero sorteado com o numero da linha
                        if (numero != 0 && !NumerosSorteadosContem(numerosSorteados, numero))
                        {
                            bingoEncontrado = false; //se nao ter o numero nao é bingo
                            break;
                        }
                    }
                }
                else
                {
                    int[] numerosDaColuna = new int[5];
                    for (int i = 0; i < 5; i++)
                    {
                        numerosDaColuna[i] = CartelasCopias[numDaCartela].ObterMatrizCartela()[i, linhaOuColuna];
                    }

                    foreach (int numero in numerosDaColuna)
                    {
                        if (numero != 0 && !NumerosSorteadosContem(numerosSorteados, numero))
                        {
                            bingoEncontrado = false;
                            break;
                        }
                    }
                }

                return bingoEncontrado;
            }
            else
            {
                Console.WriteLine("A copia da cartela ainda não foi inicializada.");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Numero de cartela inválido ou não existente.");
            return false;
        }
    }



    //metodo que pega todos os numeros sorteados e compara com cada posicao da linha ou coluna
    public bool NumerosSorteadosContem(int[] numerosSorteados, int numero)
    {
        for (int i = 0; i < numerosSorteados.Length; i++) //loop que percorre os numeros sorteados
        {
            //se i=6, sera o 6 numeros sorteado
            int linha = i / 5;  // calcula a linha da matriz 5x5, em apenas um for 
            int coluna = i % 5; // Calcular a coluna da matriz 5x5, em apenas um for
                                // Verificar se não é a posição (2, 2) onde está o valor -1
            if (numerosSorteados[i] != 0 && !(linha == 2 && coluna == 2)) //ignora o -1 da matriz para comparar os numeros
            {
                if (numerosSorteados[i] == numero)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //metodo que adiciona as cartelas copiadas no vetor de dados do jogador
    public void InicializarCartelasCopiadas()
    {
        CartelasCopias = new Cartela[QuantCartelas];
        for (int i = 0; i < QuantCartelas; i++)
        {
            if (CartelaJogadores[i] != null)
            {
                CartelasCopias[i] = CartelaJogadores[i].GerarCopiaCartela();
            }
        }
    }
}


class Cartela
{
    public int[,] cartela = new int[5, 5]; //cartela que é gerada
    private Random aleatorio = new Random(); //randaom que gera numeros aleatorios para a cartela


    //metodo apenas para ter acesso a matriz de cartela na classe jogador
    public int[,] ObterMatrizCartela()
    {
        return cartela;
    }


    //metodo que gera todas as cartelas do bingo com numeros diferentes e nos criterios apresentados
    public void GeraCartelaBingo()
    {
        // cria uma matriz cartela
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                // altera posição central para vazio(-1)
                if (i == 2 && j == 2)
                {
                    cartela[i, j] = -1;
                }
                else
                {
                    // limita até qual número poderá ser gerado por coluna
                    int minPorColuna = 1 + j * 15; // parametro minimo
                    int maxPorColuna = (j + 1) * 15; // parametro maximo

                    int numero; //variavel que armazena os numeros gerados
                    bool numRepetido; //variavel que controla a geracao de numeros repetidos


                    do
                    {
                        //gerando um número aleatório dentro do intervalo da coluna
                        numero = aleatorio.Next(minPorColuna, maxPorColuna + 1);

                        //verificando por meio do método se o número gerado está repetido na cartela
                        numRepetido = VerificarRepeticao(cartela, numero);
                    } while (numRepetido); //se o número for repetido, gera outro número

                    cartela[i, j] = numero; //colocando o numero na cartela
                }
            }
        }
    }

    public bool VerificarRepeticao(int[,] cartela, int numero)
    {
        // verifica se o número ja esta presente em alguma posição da cartela
        for (int i = 0; i < cartela.GetLength(0); i++)
        {
            for (int j = 0; j < cartela.GetLength(1); j++)
            {
                if (cartela[i, j] == numero)
                {
                    return true; // retorna verdadeiro se o número estiver repetido e gera outro
                }
            }
        }
        return false; // se não estiver repetido, retorna falso e nao gera outro
    }


    //metodo que imprime a cartela
    public void ImprimirCartela()
    {
        Console.Write("    ");
        for (int i = 1; i <= 5; i++)
        {
            Console.Write($"C{i}  "); //coloca o numero das colunas
        }
        Console.WriteLine();
        for (int i = 0; i < 5; i++)
        {
            Console.Write($"L{i + 1}  "); //coloca o numero das linhas
            for (int j = 0; j < 5; j++)
            {
                Console.Write($"{cartela[i, j],-4}");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }


    //metodo para marcar o numero sorteado, substituindo ele por 0
    public void SubstituirNumero(int numero)
    {

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {

                if (cartela[i, j] == numero)
                { //percorre a cartela e se ela tiver o numero sorteado substitui
                    cartela[i, j] = 0;
                    return;
                }
            }
        }


    }


    //metodo para criar uma copia das cartelas para verficiar o bingo
    public Cartela GerarCopiaCartela()
    {
        Cartela copia = new Cartela();
        //copia os numeros da cartela original
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                copia.cartela[i, j] = cartela[i, j];
            }
        }
        return copia;
    }
}

class Sorteador
{
    private int[] numerosSorteados = new int[75]; // vetor para armazenar os números sorteados
    private int totalNumerosSorteados = 0; // contador q controla a quant de cartela sorteados
    private Random sorteiaPedra = new Random(); //criacao do random
    private int numSorteado; //public para utilizar ele no metodo verificar repeticao

    public int SortearNumero()
    {
        do
        {
            numSorteado = sorteiaPedra.Next(1, 76); // sorteando um número entre 1 e 75
        } while (VerificarRepeticaoNum(numSorteado)); //enquanto numero nao for repetido

        numerosSorteados[totalNumerosSorteados] = numSorteado; // armazenando o número no vetor
        totalNumerosSorteados++; // incrementando o contador de números sorteados

        return numSorteado;
    }

    private bool VerificarRepeticaoNum(int numeroSort)
    {
        // verifica se o número já foi sorteado anteriormente
        for (int i = 0; i < totalNumerosSorteados; i++)
        {
            if (numerosSorteados[i] == numeroSort)
            {
                return true; //numero repetido
            }
        }
        return false; //numero nao repetido
    }
}
