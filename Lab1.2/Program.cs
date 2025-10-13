using System;

// Абстрактные продукты
interface IMovie
{
    string GetMovieInfo();
}
interface ISubtitle
{
    string GetSubtitleText();
}
// Абстрактная фабрика
interface IMovieFactory
{
    IMovie CreateMovie();
    ISubtitle CreateSubtitle();
}
// Конкретные продукты (фильм и субтитры на определенном языке)
class EnglishMovie : IMovie
{
    public string GetMovieInfo() { return "English movie."; }
}
class EnglishSubtitle : ISubtitle
{
    public string GetSubtitleText() { return "English subtitles."; }
}
class RussianMovie : IMovie
{
    public string GetMovieInfo() { return "Russian movie."; }
}
class RussianSubtitle : ISubtitle
{
    public string GetSubtitleText() { return "Russian subtitles."; }
}
// Конкретные фабрики (создают фильмы и субтитры на определенном языке)
class EnglishMovieFactory : IMovieFactory
{
    public IMovie CreateMovie() { return new EnglishMovie(); }
    public ISubtitle CreateSubtitle() { return new EnglishSubtitle(); }
}
class RussianMovieFactory : IMovieFactory
{
    public IMovie CreateMovie() { return new RussianMovie(); }
    public ISubtitle CreateSubtitle() { return new RussianSubtitle(); }
}
// Клиентский код
class MovieSystem
{
    private IMovieFactory _factory;
    private IMovie _movie;
    private ISubtitle _subtitle;
    public MovieSystem(IMovieFactory factory)
    {
        _factory = factory;
        _movie = _factory.CreateMovie();
        _subtitle = _factory.CreateSubtitle();
    }
    public void DisplayMovieDetails()
    {
        Console.WriteLine(_movie.GetMovieInfo());
        Console.WriteLine(_subtitle.GetSubtitleText());
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Выберите язык: 1 - Русский, 2 - Английский");

        string vybor = Console.ReadLine();
        IMovieFactory factory;

        if (vybor == "1")
            factory = new RussianMovieFactory();
        else if (vybor == "2")
            factory = new EnglishMovieFactory();
        else
        {
            Console.WriteLine("Неверный ввод!");
            return;
        }
        MovieSystem movie = new MovieSystem(factory);
        Console.WriteLine("\nВаш фильм готов:");
        movie.DisplayMovieDetails();
    }
}