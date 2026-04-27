internal class Service
{
    private int _id;
    private string _name;
    private double _price;
    private ServiceType _type;
    private bool _available;

    public Service()
    {
        _id = 0;
        _name = "";
        _price = 0;
        _type = ServiceType.Cleaning;
        _available = false;
    }

    public Service(int id, string name, double price, ServiceType type, bool available)
    {
        Id = id;
        Name = name;
        Price = price;
        Type = type;
        Available = available;
    }

    public int Id
    {
        get
        {
            return _id;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("ID должно быть больше 0");
            }
            _id = value;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                _name = "Нет названия";
            }
            else
            {
                _name = value;
            }
        }
    }

    public double Price
    {
        get
        {
            return _price;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("Цена должна быть больше нуля");
            }
            _price = value;
        }
    }

    public ServiceType Type
    {
        get
        {
            return _type;
        }
        set
        {
            if (!Enum.IsDefined(typeof(ServiceType), value))
            {
                throw new ArgumentException("Недопустимый тип услуги");
            }
            _type = value;
        }
    }

    public bool Available
    {
        get
        {
            return _available;
        }
        set
        {
            _available = value;
        }
    }

    public override string ToString()
    {
        return
            $"ID: {_id}\n" +
            $"Название: {_name}\n" +
            $"Цена: {_price} руб.\n" +
            $"Тип: {_type}\n" +
            $"Доступна: {(_available ? "Да" : "Нет")}";
    }
}