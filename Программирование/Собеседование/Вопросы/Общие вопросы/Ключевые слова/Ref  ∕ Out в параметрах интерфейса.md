### Ковариантные  интерфейсы

Обобщенные интерфейсы могут быть ковариантными, если к универсальному параметру применяется ключевое слово out. Такой параметр должен представлять тип объекта, который возвращается из метода. Например:

```csharp
public class Program  
{  
    public static void Main(string[] args)  
    {  
        IMessenger<Message> message = new SimpleMessenger();  
    }  
}  
  
  
interface IMessenger<out T>  
{  
    T SendMessage(string message);  
}  
  
class SimpleMessenger: IMessenger<EmailMessage> {  
    public EmailMessage SendMessage(string message)  
    {  
        return new EmailMessage(message);  
    }  
}  
  
class Message  
{  
    public string Text { get; set; }  
    public Message(string text) => Text = text;  
}  
class EmailMessage : Message  
{  
    public EmailMessage(string text): base(text) { }  
}
```

При создании ковариантного интерфейса надо учитывать, что универсальный параметр может использоваться только в качестве типа значения, возвращаемого методами интерфейса. Но не может использоваться в качестве типа аргументов метода или ограничения методов интерфейса.


### Контравариантные интерфейсы

Для создания контравариантного интерфейса надо использовать ключевое слово in. Например, возьмем те же классы Message и EmailMessage и определим следующие типы:

```csharp
public class Program  
{  
    public static void Main(string[] args)  
    {  
        IMessenger<EmailMessage> message = new SimpleMessenger();  
    }  
}  
  
  
interface IMessenger<in T>  
{  
    void SendMessage(T message);  
}  
  
class SimpleMessenger: IMessenger<Message> {  
    public void SendMessage(Message message)  
    {  
        throw new NotImplementedException();  
    }  
}  
  
class Message  
{  
    public string Text { get; set; }  
    public Message(string text) => Text = text;  
}  
class EmailMessage : Message  
{  
    public EmailMessage(string text): base(text) { }  
}

```

Здесь опять же интерфейс IMessenger представляет интерфейс мессенджера и определяет метод `SendMessage()` для отправки условного сообщения. Ключевое слово in в определении интерфейса указывает, что этот интерфейс - контравариантный.

Класс SimpleMessenger представляет условную программу отправки сообщений и реализует этот интерфейс. Причем в качестве типа используемого этот класс использует тип Message. То есть SimpleMessenger фактически представляет тип `IMessenger<Message>`.

При создании контрвариантного интерфейса надо учитывать, что универсальный параметр контрвариантного типа может применяться только к аргументам метода, но не может применяться к возвращаемому результату метода.