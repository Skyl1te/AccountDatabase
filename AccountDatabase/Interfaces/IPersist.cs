namespace AccountDatabase.Interfaces;

public interface IPersist
{
    void SaveToFile();

    void LoadFromFile();
}
