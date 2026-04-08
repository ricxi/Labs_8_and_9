using System.Collections.Generic;

public interface IDataService
{
    void Save(GameData data, bool overwrite = true);
    GameData Load(string name);
    void Delete(string name);
    IEnumerable<string> ListSaves();
}
