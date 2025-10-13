using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace ArticleConverterApp
{
    public class Program
    {
        public static void Main(string[] args)   
        {
            ArticleConverter.ConvertTxtToJson("C:\\Users\\PC\\OneDrive\\Документы\\СмолГУ!\\3 курс\\5 семестр\\Инженерия\\Lab1.1\\input.txt", "C:\\Users\\PC\\OneDrive\\Документы\\СмолГУ!\\3 курс\\5 семестр\\Инженерия\\Lab1.1\\out.json");
        }
    }
    public class ArticleConverter
    {
        public static void ConvertTxtToJson(string txtFile, string jsonFile)
        {
            try
            {
                string[] lines = File.ReadAllLines(txtFile, Encoding.UTF8);   

                var builder = new Builder(); 

                // Заполняем данными билдер
                builder.SetTitle(lines[0])          
                    .SetAuthors(lines[1])          
                    .SetArticleText(string.Join(Environment.NewLine, lines[2..^1])) 
                    .SetExpectedHash(lines[^1]);   

                ArticleData article = builder.Build(); // Создаем объект ArticleData с помощью билдера

                string jsonString = JsonConvert.SerializeObject(article, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFile, jsonString, Encoding.UTF8); 

                Console.WriteLine($"Файл успешно конвертирован в: {jsonFile}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла ошибка: {e.Message}");
            }
        }
    }
    public class ArticleData
    {
        public string Title { get; set; }
        public string Authors { get; set; }
        public string ArticleText { get; set; }
        public string Hash { get; set; }
    }
    // Builder
    public class Builder
    {
        private readonly ArticleData _article = new ArticleData(); 
        private string _expectedHash;   // Временное хранилище ожидаемого хеша
        public Builder SetTitle(string title) { _article.Title = title; return this; }
        public Builder SetAuthors(string authors) { _article.Authors = authors; return this; }
        public Builder SetArticleText(string text) { _article.ArticleText = text; return this; }
        public Builder SetExpectedHash(string hash) { _expectedHash = hash; return this; }
        public ArticleData Build()
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(_article.ArticleText);
            byte[] hashBytes = sha256.ComputeHash(bytes);
            string calculatedHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            if (calculatedHash != _expectedHash)  // Сравниваем вычисленный и ожидаемый хеш
            {
                throw new Exception("Ошибка валидации хеша!");
            }
             _article.Hash = calculatedHash;
             return _article;
        }
    }
}