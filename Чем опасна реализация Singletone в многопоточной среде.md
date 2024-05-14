```csharp
public class Singletone
{
	private static Singletone _instance;
	
	private Singletone(){ }
	
	public static Singletone Instance
	{
		get
		{
			if(_instance == null)
				_instance = new Singletone();
			return new _instance;
		}
	}
}
```
Возможно создание сразу двух Singletone объектов

