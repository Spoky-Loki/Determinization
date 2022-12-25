# Determinization
	Недетерминированный конечный автомат задан в виде таблицы переходов. 
  Напишите программу, которая считает эту таблицу из файла 
  и с помощью алгоритма детерминизации построит эквивалентный детерминированный автомат.

## Формат файла с автоматом  
0 1 //Алфавит  
- A 0:A 0:B 1:A //Строка состояния  
- B 0:B 1:B 1:C  
+ C 0:C  
	"+" значит что состояние конечно "-" нет  
	переход представлен как <символ>:<переход>  
  Стартовым является первое состояние  

## Методы  
	functions.readFromFile("путь к файлу"); //Читает автомат из файла (результат работы функции список состояний)
	functions.determinization("список состояний"); //Удаляет недостижимые состояния (результат работы функции список состояний дка)
	functions.createFileWithTable("путь к файлу", "список состояний"); //Создает файл с новым автоматом
