using AccountDatabase;

AccountsStorage storage = new AccountsStorage("users.json");

//storage.Register("banderik", "sdfsdf");
//storage.Register("petrenko", "8924ghf");
//storage.Register("sivchuk", "l2ka98");
//storage.Register("imperator", "oafui23r2q");
//storage.Register("emitza", "p1soae0r");
//storage.Register("patriot", "30owrjmka");

//storage.SaveToFile();

storage.LoadFromFile();
storage.PrintAccount();
Console.WriteLine( storage.GetAccountByCredentials("banderik", "sdfsdf").Username);

Console.WriteLine("--------------");

string xmlAccounts = storage.GetAsXml();
Console.WriteLine("Accounts in XML format:");
Console.WriteLine(xmlAccounts);
