- *Уникальное владение :* 
  std::unique_ptr гарантирует, что только один указатель владеет в любой момент времени. Это предотвращает ошибки, связанные с двойным освобождением памяти. 
  
- *Передача владельца :* 
  Владение можно передать другому unique_ptr c помощью функции std::move, но нельзя копировать unique_ptr, так как копирование запрещено.

Пример использования: 
```cpp
int main()  
{  
    object* cpp = new object();  
    std::unique_ptr<object> example {cpp};
    //std::unique_ptr<object> example1 {cpp}; ошибка компиляции при                                            добавление данной строки
    std::cout << example->mega;  
    return 0;  
}
```

*Для создание, удаление, переноса указателя используются:*

```cpp
int main()
{
	std::unique_ptr<int> exampleInteger 
	{std::make_unique<int>(125)};
	auto x = exmapleInteger.get(); //получение обычного указателя 
	auto xy = *exampleInteger; //получение значения объекта

	return 0 
}
```