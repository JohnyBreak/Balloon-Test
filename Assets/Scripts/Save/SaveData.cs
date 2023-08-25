
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public Dictionary<string, int> NameScoreDictionary;

    public SaveData()
    {
        NameScoreDictionary = new Dictionary<string, int>();
    }
}
