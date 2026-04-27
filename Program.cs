using Checkup;

List<Service> services = ServiceCatalog.ReadData();

while (true)
{
    Console.WriteLine("\n\nКАТАЛОГ УСЛУГ\n");
    Console.WriteLine("1. Просмотр базы данных");
    Console.WriteLine("2. Добавить услугу");
    Console.WriteLine("3. Удалить услугу");
    Console.WriteLine("4. Доступные услуги (по цене)");
    Console.WriteLine("5. Услуги по типу");
    Console.WriteLine("6. Средняя цена");
    Console.WriteLine("7. Самая дорогая услуга");
    Console.WriteLine("0. Выход");

    int choice = HelpConsole.ReadInt("Выберите пункт: ");

    switch (choice)
    {
        case 1:
            if (!services.Any())
            {
                Console.WriteLine("База данных пуста.");
                break;
            }
            foreach (Service s in services)
                Console.WriteLine(s + "\n");
            break;

        case 2:
            try
            {
                int id = HelpConsole.ReadPositiveInt("Введите ID: ");
                if (ServiceCatalog.IsIdTaken(services, id))
                {
                    Console.WriteLine("Ошибка: такой ID уже существует.");
                    break;
                }
                Console.Write("Введите название: ");
                string name = Console.ReadLine();
                double price = HelpConsole.ReadPositiveDouble("Введите цену: ");
                var names = Enum.GetNames(typeof(ServiceType));
                for (int i = 0; i < names.Length; i++)
                    Console.WriteLine($"{i} - {names[i]}");
                ServiceType type = (ServiceType)HelpConsole.ReadInt("Введите тип (число): ");
                if (!Enum.IsDefined(typeof(ServiceType), type))
                {
                    Console.WriteLine("Ошибка: недопустимый тип.");
                    break;
                }
                bool available = HelpConsole.ReadBool("Доступна?");
                services.Add(new Service(id, name, price, type, available));
                ServiceCatalog.SaveData(services);
                Console.WriteLine("Услуга добавлена.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
            break;

        case 3:
            try
            {
                int removeId = HelpConsole.ReadPositiveInt("Введите ID для удаления: ");
                int removed = services.RemoveAll(s => s.Id == removeId);
                if (removed == 0)
                    Console.WriteLine("Услуга с таким ID не найдена.");
                else
                {
                    ServiceCatalog.SaveData(services);
                    Console.WriteLine("Услуга удалена.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
            break;

        case 4:
            var available2 = ServiceCatalog.GetAvailableServices(services);
            if (!available2.Any())
                Console.WriteLine("Нет доступных услуг.");
            else
                foreach (Service s in available2)
                    Console.WriteLine(s + "\n");
            break;

        case 5:
            var typeNames = Enum.GetNames(typeof(ServiceType));
            for (int i = 0; i < typeNames.Length; i++)
                Console.WriteLine($"{i} - {typeNames[i]}");
            ServiceType filterType = (ServiceType)HelpConsole.ReadInt("Введите тип (число): ");
            if (!Enum.IsDefined(typeof(ServiceType), filterType))
            {
                Console.WriteLine("Ошибка: недопустимый тип.");
                break;
            }
            var byType = ServiceCatalog.GetServicesByType(services, filterType);
            if (!byType.Any())
                Console.WriteLine("Услуги данного типа не найдены.");
            else
                foreach (Service s in byType)
                    Console.WriteLine(s + "\n");
            break;

        case 6:
            if (!services.Any())
            {
                Console.WriteLine("База данных пуста.");
                break;
            }
            Console.WriteLine($"Средняя цена: {ServiceCatalog.GetAveragePrice(services):F2} руб.");
            break;

        case 7:
            Service mostExpensive = ServiceCatalog.GetMostExpensive(services);
            if (mostExpensive == null)
                Console.WriteLine("База данных пуста.");
            else
                Console.WriteLine(mostExpensive);
            break;

        case 0:
            ServiceCatalog.SaveData(services);
            return;

        default:
            Console.WriteLine("Неверный пункт меню.");
            break;
    }
}