// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;

int seed = DateTime.Now.Millisecond;
Random rand = new Random(seed);

int length = 500 * 10 *10 * 10;
// var m = GerarMatrizCebola(lenght);
// MostrarMatrizCebola(m);
// Console.WriteLine(solucao3(m, lenght));

MostrarTempo(length, 1);

int solucao1(int[,] mat, int N)
{
    int max = int.MinValue;
    foreach (var item in mat)
        if (max < item)
            max = item;
    return max;
}

int solucao2(int[,] mat, int N)
{
    int max = int.MinValue;


    bool isLeft = mat[0,0] < mat[0, N - 1];
    bool isTop = mat[N - 1, 0] < mat[N - 1, N - 1];

    int positionX = isLeft? 0 : N - 1;
    int positionY = isTop? 0 : N - 1;

    int auxPositionX = 0;
    int auxPositionY = 0;
    bool flag = true;

    while (flag)
    {
        flag = false;
        for (int j = positionY - 1; j < positionY + 2; j++)
        {
            for (int i = positionX - 1; i < positionX + 2; i++)
            {
                if (j == -1 || j == N || i == -1 || i == N) // the elem is outside the matrix
                    continue;

                if (mat[i,j] > max)
                {
                    flag = true;
                    max = mat[i,j];
                    auxPositionX = i;
                    auxPositionY = j;
                }
            }
        }
        positionX = auxPositionX;
        positionY = auxPositionY;
    }
        
    return max;
}

int solucao3(int[,] mat, int N)
{
    int max = mat[0, 0];
    int tam = N % 2 == 0? N / 2 : N / 2 + 1;
    
    if (N == 1)
        return max;
    
    int[] leftTopCorner = {0, 0};
    int[] rightTopCorner = {0, N - 1};
    int[] leftBottomCorner = {N - 1, 0};
    int[] rightBottomCorner = {N - 1, N - 1};


    int[][] positions = {
        leftTopCorner,
        leftBottomCorner,
        rightTopCorner,
        rightBottomCorner
    };
    
    int index = 0;

    for (int i = 0; i < positions.Length; i++)
        if (mat[positions[i][0], positions[i][1]] > max)
        {
            max = mat[positions[i][0], positions[i][1]];
            index = i;
        }

    int startIndexRow = 0;
    int startIndexCol = 0;

    switch (index)
    {
        case 0:
            break;
        case 1:
            startIndexRow = (N / 2);
            break;
        case 2:
            startIndexCol = (N / 2);
            break;
        case 3:
            startIndexRow = (N / 2);
            startIndexCol = (N / 2);
            break;
    }

    int[,] newMatrix = new int[tam, tam];
    
    int col = startIndexCol;

    for (int i = 0; i < tam; i++, startIndexRow++)
    {
        col = startIndexCol;
        for (int j = 0; j < tam; j++, col++)
            newMatrix[i, j] = mat[startIndexRow, col];
    }
    
    return solucao3(newMatrix, tam);
}

int solucao4(int[,] mat, int N)
{
    int max = int.MinValue;
    int positionX = 0;
    int positionY = 0;
    int auxPositionX = 0;
    int auxPositionY = 0;
    bool flag = true;

    while (flag)
    {
        flag = false;
        for (int j = positionY - 1; j < positionY + 2; j++)
        {
            for (int i = positionX - 1; i < positionX + 2; i++)
            {
                if (j == -1 || j == N || i == -1 || i == N) // the elem is outside the matrix
                    continue;

                if (mat[i,j] > max)
                {
                    flag = true;
                    max = mat[i,j];
                    auxPositionX = i;
                    auxPositionY = j;
                }
            }
        }
        positionX = auxPositionX;
        positionY = auxPositionY;
    }
        
    return max;
}


int[,] GerarMatrizCebola(int N)
{
    int[,] mat = new int[N, N];
    int x = rand.Next(N),
        y = rand.Next(N),
        value = rand.Next(500, 1000),
        _x = 0,
        _y = 0;
    mat[x, y] = value;

    int delta = 1;
    int lastMinValue = value;
    int newMinValue = value;
    while (delta < N)
    {
        for (int i = -delta; i <= delta; i++)
        {
            var newValue = lastMinValue - rand.Next(1, 6);
            if (newValue < newMinValue)
                newMinValue = newValue;
            
            _x = x + i;
            _y = y - delta;
            if (_x < 0 || _x >= N || _y < 0 || _y >= N)
                continue;
            
            mat[_x, _y] = newValue;
        }
        
        for (int i = -delta; i <= delta; i++)
        {
            var newValue = lastMinValue - rand.Next(1, 6);
            if (newValue < newMinValue)
                newMinValue = newValue;
            
            _x = x + i;
            _y = y + delta;
            if (_x < 0 || _x >= N || _y < 0 || _y >= N)
                continue;
            
            mat[_x, _y] = newValue;
        }
        
        for (int j = -delta + 1; j < delta; j++)
        {
            var newValue = lastMinValue - rand.Next(1, 6);
            if (newValue < newMinValue)
                newMinValue = newValue;
            
            _x = x - delta;
            _y = y + j;
            if (_x < 0 || _x >= N || _y < 0 || _y >= N)
                continue;
            
            mat[_x, _y] = newValue;
        }
        
        for (int j = -delta + 1; j < delta; j++)
        {
            var newValue = lastMinValue - rand.Next(1, 6);
            if (newValue < newMinValue)
                newMinValue = newValue;
            
            _x = x + delta;
            _y = y + j;
            if (_x < 0 || _x >= N || _y < 0 || _y >= N)
                continue;
            
            mat[_x, _y] = newValue;
        }
        delta++;
        lastMinValue = newMinValue;
    }

    return mat;
}

void MostrarMatrizCebola(int[,] mat)
{
    int N = (int)Math.Sqrt(mat.LongLength);
    StringBuilder sb = new StringBuilder();
    for (int j = 0; j < N; j++)
    {
        if (j == 0)
        {
            for (int i = 0; i < N; i++)
            {
                if (i == 0)
                    sb.Append("┌");
                else sb.Append("┬");
                sb.Append("───────");
            }
            sb.Append("┐\n");
        }
        else
        {
            for (int i = 0; i < N; i++)
            {
                if (i == 0)
                    sb.Append("├");
                else sb.Append("┼");
                sb.Append("───────");
            }
            sb.Append("┤\n");
        }

        for (int k = 0; k < 2; k++)
        {
            for (int i = 0; i < N; i++)
            {
                sb.Append("│");
                sb.Append("       ");
            }
            sb.Append("|\n");
        }
        

        for (int i = 0; i < N; i++)
        {
            sb.Append("│");
            sb.Append(mat[i, j].ToString("  000  "));
        }
        sb.Append("|\n");

        for (int k = 0; k < 2; k++)
        {
            for (int i = 0; i < N; i++)
            {
                sb.Append("│");
                sb.Append("       ");
            }
            sb.Append("|\n");
        }
    }
        
    for (int i = 0; i < N; i++)
    {
        if (i == 0)
            sb.Append("└");
        else sb.Append("┴");
        sb.Append("───────");
    }
    sb.Append("┘\n");
    Console.WriteLine(sb.ToString());
}

void MostrarTempo(int N, int K)
{
    List<int[,]> list = new List<int[,]>();
    Console.Write($"Gerando {K} matrizes para testes: ");
    for (int k = 0; k < K; k++)
    {
        Console.Write($"{k + 1}.. ");
        list.Add(GerarMatrizCebola(N));
    }
    Console.WriteLine("\n");
    
    Stopwatch sw = new Stopwatch();

    Console.WriteLine("Testando solucao1...");
    sw.Start();
    foreach (var mat in list)
    {
        solucao1(mat, N);
    }
    sw.Stop();
    Console.WriteLine($"Solução 1 (n²) para n = {N}: {(double)sw.ElapsedMilliseconds / (double)K} ms / execução\n");
    sw.Reset();

    Console.WriteLine("Testando solucao2...");
    sw.Start();
    foreach (var mat in list)
    {
        solucao2(mat, N);
    }
    sw.Stop();
    Console.WriteLine($"Solução 2 (andré/emerson) para n = {N}: {(double)sw.ElapsedMilliseconds / (double)K} ms / execução\n");
    sw.Reset();

    Console.WriteLine("Testando solucao3...");
    sw.Start();
    foreach (var mat in list)
    {
        solucao3(mat, N);
    }
    sw.Stop();
    Console.WriteLine($"Solução 3 (log(n)) para n = {N}: {(double)sw.ElapsedMilliseconds / (double)K} ms / execução\n");
    sw.Reset();

    Console.WriteLine("Testando solucao4...");
    sw.Start();
    foreach (var mat in list)
    {
        solucao4(mat, N);
    }
    sw.Stop();
    Console.WriteLine($"Solução 4 (falas) para n = {N}: {(double)sw.ElapsedMilliseconds / (double)K} ms / execução\n");
    sw.Reset();
}