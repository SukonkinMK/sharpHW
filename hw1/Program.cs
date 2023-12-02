using hw1;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

public class Program
{
    static void Main(string[] args)
    {
        var movieJson = "{\"Name\":\"Squid Game\",\"Genre\":\"Thriller\",\"Rating\":8.1,\"Budget\":20000000}";
        var movie = JsonSerializer.Deserialize<Movie>(movieJson);
        var xmlSerializer = new XmlSerializer(typeof(Movie));
        var sb = new StringBuilder();
        using (var xmlWriter = XmlWriter.Create(sb))
        {
            xmlSerializer.Serialize(xmlWriter, movie);
        }
        Console.WriteLine(sb.ToString());
        Console.ReadLine();
    }
}