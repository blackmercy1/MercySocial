Перегрузка методов — это механизм, при котором методы с одинаковым именем имеют разные сигнатуры, и компилятор выбирает правильный метод на этапе компиляции в зависимости от типов параметров. Это вид статического полиморфизма, так как компилятор решает, какой метод вызвать, еще до запуска программы.

```csharp
public class Evaluator
{
	private int Evaluate(int first, int second) => first + second;
	
	private string Evaluate(string first, string second)
	=> first + second;
}
```