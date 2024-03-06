namespace PhoneBook;

public class PhoneBook
{
    public PhoneBook(List<Contact> contacts)
    {
        Contacts = contacts;
    }

    public PhoneBook()
    {
        Contacts = new List<Contact>();
    }
    public List<Contact> Contacts { get; set; }
    public IConsoleReader ConsoleReader = new ConsoleReader();

    public void AddContact()
    {
        Contact newContact = new Contact();

        ConsoleReader.Write("Введите имя: ");
        newContact.FirstName = ConsoleReader.ReadLine();

        ConsoleReader.Write("Введите фамилию: ");
        newContact.LastName = ConsoleReader.ReadLine();

        ConsoleReader.Write("Введите номер телефона: ");
        newContact.PhoneNumber = ConsoleReader.ReadLine();

        Contacts.Add(newContact);

        ConsoleReader.WriteLine("Контакт добавлен.");
    }

    public void ViewContacts()
    {
        if (Contacts.Count == 0)
        {
            ConsoleReader.WriteLine("Список контактов пуст.");
            return;
        }

        foreach (var contact in Contacts)
        {
            ConsoleReader.WriteLine($"Имя: {contact.FirstName}, Фамилия: {contact.LastName}, Номер телефона: {contact.PhoneNumber}");
        }
    }

    public void UpdateContact()
    {
        ConsoleReader.Write("Введите номер телефона контакта, который хотите обновить: ");
        string phoneNumberToUpdate = ConsoleReader.ReadLine();

        var contactToUpdate = Contacts.Find(contact => contact.PhoneNumber == phoneNumberToUpdate);

        if (contactToUpdate == null)
        {
            ConsoleReader.WriteLine("Контакт с таким номером телефона не найден.");
            return;
        }

        ConsoleReader.Write("Введите новое имя: ");
        contactToUpdate.FirstName = ConsoleReader.ReadLine();

        ConsoleReader.Write("Введите новую фамилию: ");
        contactToUpdate.LastName = ConsoleReader.ReadLine();

        ConsoleReader.WriteLine("Контакт обновлен.");
    }

    public void DeleteContact()
    {
        ConsoleReader.Write("Введите номер телефона контакта, который хотите удалить: ");
        string phoneNumberToDelete = ConsoleReader.ReadLine();

        var contactToDelete = Contacts.Find(contact => contact.PhoneNumber == phoneNumberToDelete);

        if (contactToDelete == null)
        {
            ConsoleReader.WriteLine("Контакт с таким номером телефона не найден.");
            return;
        }

        Contacts.Remove(contactToDelete);
        ConsoleReader.WriteLine("Контакт удален.");
    }

    public void SearchContact()
    {
        ConsoleReader.Write("Введите имя или номер телефона для поиска: ");
        string searchQuery = ConsoleReader.ReadLine();

        var foundContacts = Contacts.FindAll(contact =>
            contact.FirstName.Contains(searchQuery) ||
            contact.LastName.Contains(searchQuery) ||
            contact.PhoneNumber.Contains(searchQuery));

        if (foundContacts.Count == 0)
        {
            ConsoleReader.WriteLine("Контакты не найдены.");
            return;
        }

        foreach (var contact in foundContacts)
        {
            ConsoleReader.WriteLine($"Имя: {contact.FirstName}, Фамилия: {contact.LastName}, Номер телефона: {contact.PhoneNumber}");
        }
    }

    public void SaveBook()
    {
        using (StreamWriter writer = new StreamWriter("contacts.txt"))
        {
            foreach (Contact contact in Contacts)
            {
                writer.WriteLine($"{contact.FirstName},{contact.LastName},{contact.PhoneNumber}");
            }
        }

        ConsoleReader.WriteLine("Книга сохранена.");
    }

    public void LoadBook()
    {
        // Дополнительное задание
        // загрузка контактов из файла contacts.txt
        if (File.Exists("contacts.txt"))
        {
            string[] lines = File.ReadAllLines("contacts.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    Contacts.Add(new Contact
                    {
                        FirstName = parts[0],
                        LastName = parts[1],
                        PhoneNumber = parts[2]
                    });
                }
            }
            ConsoleReader.WriteLine("Книга загружена.");
        }
        else
        {
            ConsoleReader.WriteLine("Файл с контактами не найден.");
        }
    }
}

