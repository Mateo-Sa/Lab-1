using System;
using System.Collections.Generic;

// Абстрактный класс фигуры (Prototype)
public abstract class Figure
{
    public int Size { get; protected set; } // // Общий размер фигуры
    public abstract Figure Clone(); // Клон фигуры
    public abstract void Display(); // Отображение фигуры
}
// Конкретные фигуры. 
public class SmallFigure : Figure
{
    public SmallFigure() { Size = 4; }
    public override Figure Clone() { return (Figure)this.MemberwiseClone(); }
    public override void Display() { Console.WriteLine($"Small figure (size {Size})"); }
}
public class LargeFigure : Figure
{
    public LargeFigure() { Size = 9; }
    public override Figure Clone() { return (Figure)this.MemberwiseClone(); }
    public override void Display() { Console.WriteLine($"Large figure (size {Size})"); }
}
public class SuperFigure : Figure
{
    public SuperFigure() { Size = 16; }
    public override Figure Clone() { return (Figure)this.MemberwiseClone(); }
    public override void Display() { Console.WriteLine($"Super figure (size {Size})"); }
}
// Фабрика фигур
public class FigureFactory
{
    private readonly List<Figure> _prototypes = new List<Figure>();
    public FigureFactory()
    {
        _prototypes.Add(new SmallFigure());  // Добавляем экземпляры фигур в список прототипов
        _prototypes.Add(new LargeFigure());
        _prototypes.Add(new SuperFigure());
    }
    public Figure GetRandomFigure()
    {
        Random rnd = new Random();
        int index = rnd.Next(0, _prototypes.Count);
        return _prototypes[index].Clone();     // Возвращаем клон выбранного прототипа
    }
}
public class Proga
{
    public static void Main(string[] args)
    {
        FigureFactory factory = new FigureFactory();

        for (int i = 0; i < 5; i++)
        {
            Figure figure = factory.GetRandomFigure(); // Получаем случайную фигуру (клон прототипа)
            Console.WriteLine("Клон:");
            figure.Display(); // Отображаем фигуру
        }
    }
}