# CrossCuttingTask
Сквозная задача ***[Готова]***

Что умеет на данный момент?
 - Чтение из файлов json, xml, txt;
 - Чтение архивированных файлов zip, rar;
 - Парсинг математического выражения любой сложности;
 - Предоставляет RESTful Service с SwaggerUI;
 - UI на базе Blazor Framework;
 - Запись истории файлов в базу даннных(SQL);
 - Тестирование методов RESTful Service(WebApi Testing);
 - Запись в файлы json, xml, txt;
 - Архивирование в zip;
 - Шифрование данных MD5;
 - Добавлены Unit Tests;

Использованные паттерны:
 - Decorator;
 - Builder;
 
***Примечание:***
   Ввиду закрытого исходного кода WinRar не удалось реализовать архивацию в RAR
 
 ***Blazor UI:***
 ![Add File](https://github.com/fpmovec/Images/raw/main/image.png)
 
 ![Add File](https://github.com/fpmovec/Images/raw/main/image1.png)
 
 ***Swagger UI***
 
 ![Add File](https://github.com/fpmovec/Images/raw/main/swagger.png)


***Структура репозитория:***

- ClientApp - папка с UI;
- Croos_Cutting_Task - папка с основным кодом(парсинг, веб-сервис, паттерны);
- WebApiTests - папка с тестами для Веб-Сервиса;
- UnitTests - папка с юнит-тестами паттернов, парсинга;
 
