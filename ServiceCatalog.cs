internal class ServiceCatalog
{
    private const string FileName = "DB.bin";

    public static List<Service> ReadData()
    {
        List<Service> services = new List<Service>();
        if (!File.Exists(FileName))
            return services;
        try
        {
            using (BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    Service service = new Service();
                    service.Id = reader.ReadInt32();
                    service.Name = reader.ReadString();
                    service.Price = reader.ReadDouble();
                    service.Type = (ServiceType)reader.ReadInt32();
                    service.Available = reader.ReadBoolean();
                    services.Add(service);
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
        }
        return services;
    }

    public static void SaveData(List<Service> services)
    {
        try
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Create)))
            {
                writer.Write(services.Count);
                foreach (Service s in services)
                {
                    writer.Write(s.Id);
                    writer.Write(s.Name);
                    writer.Write(s.Price);
                    writer.Write((int)s.Type);
                    writer.Write(s.Available);
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка сохранения: {ex.Message}");
        }
    }

    public static List<Service> GetAvailableServices(List<Service> services)
    {
        return services.Where(s => s.Available).OrderBy(s => s.Price).ToList();
    }

    public static List<Service> GetServicesByType(List<Service> services, ServiceType type)
    {
        return services.Where(s => s.Type == type).ToList();
    }

    public static double GetAveragePrice(List<Service> services)
    {
        return services.Any() ? services.Average(s => s.Price) : 0;
    }

    public static Service GetMostExpensive(List<Service> services)
    {
        return services.OrderByDescending(s => s.Price).FirstOrDefault();
    }

    public static bool IsIdTaken(List<Service> services, int id)
    {
        return services.Any(s => s.Id == id);
    }
}