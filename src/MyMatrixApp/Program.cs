/*
Задание 1
Создайте класс MyMatrix, представляющий матрицу m на n.
Создайте конструктор, принимающий число строк и столбцов, заполняющий матрицу
случайными числами в диапазоне, который пользователь вводит при запуске программы.

Создайте метод Fill, перезаполняющий матрицу случайными значениями.
Создайте метод ChangeSize, изменяющий число строк и/или столбцов с копированием
значений существующей матрицы. Если новая матрица больше существующий, то метод
должен дозаполнить новую матрицу случайными числами.

Создайте метод ShowPartialy, принимающий в качестве параметров начальные и конечные
значения строк и столбцов, значения матрицы внутри которых нужно вывести на консоль.

Создайте метод Show, выводящий все значения матрицы на консоль.
Создайте индексатор для матрицы вида this[int index1, int index2] с аксессором и мутатором.
*/

using System;

public class MyMatrix
{
    private int _rows;
    private int _cols;
    private int _minValue;
    private int _maxValue;
    private int[,] _matrix;
    private Random _random;

    // Конструктор
    public MyMatrix(int rows, int cols, int minValue, int maxValue)
    {
        if (rows <= 0 || cols <= 0)
            throw new ArgumentException("Количество строк и столбцов должно быть положительным.");

        if (minValue > maxValue)
            throw new ArgumentException("Минимальное значение должно быть меньше или равно максимальному значению.");

        _rows = rows;
        _cols = cols;
        _minValue = minValue;
        _maxValue = maxValue;
        _matrix = new int[rows, cols];
        _random = new Random();

        Fill(minValue, maxValue);
    }

    // Индексатор для доступа к элементам матрицы
    public int this[int index1, int index2]
    {
        get => _matrix[index1, index2];
        set => _matrix[index1, index2] = value;
    }

    // Метод заполнения матрицы случайными значениями
    public void Fill(int minValue, int maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                _matrix[i, j] = _random.Next(minValue, maxValue + 1);
            }
        }
    }

    // Метод изменения размера матрицы
    public void ChangeSize(int newRows, int newCols)
    {
        if (newRows <= 0 || newCols <= 0)
            throw new ArgumentException("Новое количество строк и столбцов должно быть положительным.");

        int[,] newMatrix = new int[newRows, newCols];

        // Копирование значений существующей матрицы
        for (int i = 0; i < Math.Min(_rows, newRows); i++)
        {
            for (int j = 0; j < Math.Min(_cols, newCols); j++)
            {
                newMatrix[i, j] = _matrix[i, j];
            }
        }

        _matrix = newMatrix;

        // Заполнение новой матрицы случайными значениями, если она больше старой
        if (newRows > _rows || newCols > _cols)
        {
            _rows = newRows;
            _cols = newCols;
            Fill(_minValue, _maxValue);
        }

        _rows = newRows;
        _cols = newCols;
    }

    // Метод для отображения части матрицы
    public void ShowPartial(int startRow, int endRow, int startCol, int endCol)
    {
        for (int i = startRow; i <= endRow && i < _rows; i++)
        {
            for (int j = startCol; j <= endCol && j < _cols; j++)
            {
                Console.Write(_matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    // Метод для отображения всей матрицы
    public void Show()
    {
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _cols; j++)
            {
                Console.Write(_matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите количество строк матрицы:");
        int rows = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество столбцов матрицы:");
        int cols = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите минимальное значение случайного числа:");
        int minValue = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите максимальное значение случайного числа:");
        int maxValue = int.Parse(Console.ReadLine());

        MyMatrix matrix = new MyMatrix(rows, cols, minValue, maxValue);
        Console.WriteLine("Сгенерированная матрица:");
        matrix.Show();

        // Тестирование метода Fill
        matrix.Fill(10, 50);
        Console.WriteLine("Матрица после повторного заполнения:");
        matrix.Show();

        // Тестирование метода ChangeSize
        Console.WriteLine("Изменение размера матрицы:");
        matrix.ChangeSize(4, 6);
        matrix.Show();

        // Тестирование метода ShowPartial
        Console.WriteLine("Частичное отображение матрицы (2-3 строки и 1-4 столбцы):");
        matrix.ShowPartial(2, 3, 1, 4);
    }
}
