C# поддерживает статический полиморфизм через обобщения с ограничениями. Когда вы используете обобщенные методы или классы, компилятор применяет типы, указанные при создании объекта или вызове метода. Это позволяет использовать различные реализации для разных типов во время компиляции, не требуя виртуальных вызовов.

```csharp
public interface IPrintable
{
    void Print();
}

public class Document : IPrintable
{
    public void Print()
    {
        Console.WriteLine("Printing document...");
    }
}

public class Photo : IPrintable
{
    public void Print()
    {
        Console.WriteLine("Printing photo...");
    }
}

public class Printer
{
    public void Print<T>(T item) where T : IPrintable
    {
        item.Print();
    }
}

public class Program
{
    public static void Main()
    {
        Printer printer = new Printer();
        Document doc = new Document();
        Photo photo = new Photo();

        printer.Print(doc);   // Вызовет Document.Print
        printer.Print(photo); // Вызовет Photo.Print
    }
}
```

