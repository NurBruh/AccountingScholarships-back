# Как запустить проект через Visual Studio

## Структура проекта

Проект теперь имеет правильную структуру:

```
accounting-scholarships/
├── AccountingScholarships.sln          ← Открывайте ЭТО в Visual Studio
├── AccountingScholarships.API/         ← Стартовый проект
│   ├── Controllers/
│   ├── Middleware/
│   ├── Program.cs
│   └── appsettings.json
├── AccountingScholarships.Application/
├── AccountingScholarships.Domain/
└── AccountingScholarships.Infrastructure/
```

## Запуск в Visual Studio

1. **Откройте Visual Studio**

2. **File → Open → Project/Solution**

3. **Выберите файл:**
   ```
   C:\Users\NurBruh\Diploma\accounting-scholarships\AccountingScholarships.sln
   ```

4. **Установите стартовый проект:**
   - В Solution Explorer кликните правой кнопкой на `AccountingScholarships.API`
   - Выберите "Set as Startup Project"

5. **Запустите приложение:**
   - Нажмите `F5` или кнопку ▶️ (зеленый треугольник)
   - Или: Debug → Start Debugging

6. **Откроется Swagger UI автоматически**

## Работа с проектами в Solution

- **Собрать все:** Build → Build Solution (`Ctrl+Shift+B`)
- **Очистить:** Build → Clean Solution
- **Пересобрать:** Build → Rebuild Solution

## Запуск из командной строки

Если хотите запустить через терминал:

```bash
cd C:\Users\NurBruh\Diploma\accounting-scholarships
dotnet run --project AccountingScholarships.API
```

## Swagger UI

После запуска откроется браузер по адресу:
- `https://localhost:5001/swagger`

Здесь вы можете тестировать все API endpoints.

## Готово!

Все работает через Visual Studio как положено! ✅
