using AccountDatabase.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace AccountDatabase;

public class AccountsStorage : IPersist, IEquatable<AccountsStorage>, IXmlFormattable
{
    private List<Account> _accounts;
    private readonly string _storageName;

    public AccountsStorage(string storageName)
    {
        _storageName = storageName;
        _accounts = new List<Account>();
    }

    public void Register(string username, string password)
    {
        bool accountExists = _accounts.Any(account => account.Username == username);

        if (accountExists)
            throw new Exception("Username already exists!");

        Account newAccount = new Account
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = ToSha256(password),
            RegistrationTime = DateTime.Now
        };

        _accounts.Add(newAccount);


    }

    private string ToSha256(string str)
    {
        SHA256 sha = SHA256.Create();
        byte[] bytes = sha.ComputeHash(Encoding.Default.GetBytes(str));

        string result = string.Empty;

        foreach(byte b in bytes)
        {
            result += b.ToString("x2");
        }
        return result;
    }

    public void SaveToFile()
    {
        JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        { 
            WriteIndented = true
        };
        string jsonString = JsonSerializer.Serialize(_accounts, jsonSerializerOptions);

        File.WriteAllText(_storageName, jsonString);
    }

    public void LoadFromFile()
    {
        if (!File.Exists(_storageName))
            throw new Exception("JSON file with account not found!");

        string jsonString = File.ReadAllText(_storageName);
        _accounts.Clear();
        _accounts = JsonSerializer.Deserialize<List<Account>>(jsonString);

    }

    public void PrintAccount()
    {
        foreach (var account in _accounts)
        {
            Console.WriteLine($"{account.Username} ({account.Id})");
        }
    }

    public bool Equals(AccountsStorage? other)
    {
        if (other == null) return false;

       
        if (this._accounts.Count != other._accounts.Count)
        {
            return false;
        }

                foreach (var account in _accounts)
        {
            bool accountExists = other._accounts.Any(acc => acc.Id == account.Id
                && acc.Username == account.Username);

            if (!accountExists)
            {
                return false;
            }
        }

        return true;
    }

    public Account? GetAccountByCredentials(string username, string password)
    {
        string hashedPassword = ToSha256(password);
        foreach (var account in _accounts)
        {
            if (account.Username == username && account.PasswordHash == hashedPassword)
            {
                return account;
            }
        }
        return null;

        /*return _accounts.Find(account =>
        account.Username == username && account.PasswordHash == hashedPassword);*/
    }

    public string GetAsXml()
    {
        StringBuilder xmlFormatBuilder = new StringBuilder();
        xmlFormatBuilder.AppendLine("<Accounts>");

        foreach (var account in _accounts)
        {
            xmlFormatBuilder.AppendLine($"  <Account>");
            xmlFormatBuilder.AppendLine($"    <Id>{account.Id}</Id>");
            xmlFormatBuilder.AppendLine($"    <Username>{account.Username}</Username>");
            xmlFormatBuilder.AppendLine($"    <PasswordHash>{account.PasswordHash}</PasswordHash>");
            xmlFormatBuilder.AppendLine($"    <RegistrationTime>{account.RegistrationTime}</RegistrationTime>");
            xmlFormatBuilder.AppendLine($"  </Account>");
        }

        xmlFormatBuilder.AppendLine("</Accounts>");

        return xmlFormatBuilder.ToString();
    }
}
