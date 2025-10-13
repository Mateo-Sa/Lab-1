using System;

public sealed class Elevator
{
    private static Elevator _instance;
    public int CurrentFloor { get; private set; }

    private Elevator() { CurrentFloor = 1; } // Лифт стартует на первом этаже

    public static Elevator Instance
    {
        get
        {
            if (_instance == null)
            {

                    if (_instance == null)
                    {
                        _instance = new Elevator();
                    }
                
            }
            return _instance;
        }
    }
    public void Floor(int Number)
    {
        Console.WriteLine($"Лифт начал движение с этажа {CurrentFloor}...");
        CurrentFloor = Number;
        Console.WriteLine($"Лифт прибыл на этаж {CurrentFloor}.");
    }
}

public class Building
{
    public List<Floor> Floors { get; } = new List<Floor>();
    private static Building instance;

    public Building(int number)
    {
        for (int i = 1; i <= number; i++)
        {
            Floors.Add(new Floor(i));
        }
        Console.WriteLine("Создано здание с {0} этажами", number);
    }
    public static Building GetInstance(int Count)
    {
        if (instance == null)
            instance = new Building(Count);
        return instance;
    }
    public void Display()
    {
        Console.WriteLine("Этажей: {0}", Floors.Count);
    }
}

//Представление этажа
public class Floor
{
    public int FloorNumber { get; }

    public Floor(int Number)
    {
        FloorNumber = Number;
    }
}

public class Proga
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Введите кол-во этажей");
        int a = Convert.ToInt32(Console.ReadLine());
        Building building = Building.GetInstance(a); // Здание с  этажами
        Elevator elevator = Elevator.Instance;
        Console.WriteLine("Введите на какой этаж вам надо");
        int b = Convert.ToInt32(Console.ReadLine());
        elevator.Floor(b);                           // Вызов лифта на этаж
        Console.WriteLine("Введите на какой этаж вам надо");
        int с = Convert.ToInt32(Console.ReadLine());
        elevator.Floor(с);                           // Вызов лифта на  этаж
        Console.WriteLine($"Лифт сейчас на этаже: {elevator.CurrentFloor}");
        Console.WriteLine("Введите кол-во этажей");
        int d = Convert.ToInt32(Console.ReadLine());
        Building anotherBuilding = Building.GetInstance(d);
        anotherBuilding.Display(); // Параметры игнорируются, выводится первое здание
    }
}