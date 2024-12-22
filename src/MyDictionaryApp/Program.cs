/*
Задание 3
Создайте коллекцию MyDictionary<TKey,TValue>.

Реализуйте в простейшем приближении возможность использования ее
экземпляра аналогично экземпляру класса Dictionary<TKey,TValue>.

Минимально требуемый интерфейс взаимодействия с экземпляром должен
включать метод добавления элемента, индексатор для получения значения
элемента по указанному индексу и свойство только для чтения для
получения общего количества элементов.

Реализуйте возможность перебора элементов коллекции в цикле foreach.
При выполнении нельзя использовать коллекции, только массивы.
*/

using System;
using System.Collections;

public class MyDictionary<TKey, TValue> : IEnumerable
{
    private TKey[] _keys;
    private TValue[] _values;
    private int _count;
    private const int _initialCapacity = 4; // Начальная емкость

    // Конструктор по умолчанию
    public MyDictionary()
    {
        _keys = new TKey[_initialCapacity];
        _values = new TValue[_initialCapacity];
        _count = 0;
    }

    // Свойство только для чтения для получения общего количества элементов
    public int Count => _count;

    // Индексатор для получения значения по ключу
    public TValue this[TKey key]
    {
        get
        {
            int index = Array.IndexOf(_keys, key, 0, _count);
            if (index == -1)
                throw new KeyNotFoundException("Ключ не найден.");
            return _values[index];
        }
        set
        {
            int index = Array.IndexOf(_keys, key, 0, _count);
            if (index == -1)
            {
                Add(key, value); // Если ключ не существует, добавляем новую пару
            }
            else
            {
                _values[index] = value; // Обновляем существующее значение
            }
        }
    }

    // Метод для добавления элемента
    public void Add(TKey key, TValue value)
    {
        if (_count == _keys.Length)
        {
            Resize();
        }

        if (Array.IndexOf(_keys, key, 0, _count) != -1)
        {
            throw new ArgumentException("Ключ уже существует.");
        }

        _keys[_count] = key;
        _values[_count] = value;
        _count++;
    }

    // Метод для изменения размера массивов
    private void Resize()
    {
        int newCapacity = _keys.Length * 2; // Увеличиваем размер в 2 раза
        TKey[] newKeys = new TKey[newCapacity];
        TValue[] newValues = new TValue[newCapacity];

        Array.Copy(_keys, newKeys, _keys.Length);
        Array.Copy(_values, newValues, _values.Length);

        _keys = newKeys;
        _values = newValues;
    }

    // Реализация интерфейса IEnumerable для перебора в цикле foreach
    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return new KeyValuePair<TKey, TValue>(_keys[i], _values[i]);
        }
    }
}

// Структура для хранения пар ключ-значение
public struct KeyValuePair<TKey, TValue>
{
    public TKey Key { get; }
    public TValue Value { get; }

    public KeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}

// Пример использования
class Program
{
    static void Main(string[] args)
    {
        MyDictionary<string, int> myDictionary = new MyDictionary<string, int>();

        // Добавление ключей и значений
        myDictionary.Add("one", 1);
        myDictionary.Add("two", 2);
        myDictionary.Add("three", 3);

        // Получение значения по ключу
        Console.WriteLine("Значение для ключа 'two': " + myDictionary["two"]);

        // Перебор элементов в цикле foreach с указанием типа
        Console.WriteLine("Элементы в словаре:");
        foreach (KeyValuePair<string, int> pair in myDictionary)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        // Вывод общего количества элементов
        Console.WriteLine("Общее количество элементов: " + myDictionary.Count);
    }
}
