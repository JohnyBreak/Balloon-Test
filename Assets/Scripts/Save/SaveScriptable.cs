using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[CreateAssetMenu(fileName = "SaveScriptable", menuName = "ScriptableObjects/Save/SaveScriptable")]
public class SaveScriptable : ScriptableObject
{
    [HideInInspector] public SaveData Data;

    private void OnEnable()
    {
        Load();
    }

    public void Load()
    {
        //Debug.LogError("load");
        Data = (SaveData)Load(Application.persistentDataPath + "/saves/Save.save");
    }

    public void Save()
    {
        Save(Data);
    }

    #region Work With File
    private bool Save(object data, string name = "Save")
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        string path = Application.persistentDataPath + "/saves/" + name + ".save";

        FileStream file = File.Create(path);

        formatter.Serialize(file, data);
        file.Close();
        return true;
    }

    private object Load(string path)
    {
        if (!File.Exists(path))
        {
            Save(new SaveData(), "Save");
        }
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);
        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogError($"Failed to load file at {path}");
            file.Close();
            return null;
        }
    }

    // for test

    //public bool Save(object data, string name = "Save")
    //{
    //    //if (!Directory.Exists(Application.persistentDataPath + "/saves")) 
    //    //{
    //    //    Directory.CreateDirectory(Application.persistentDataPath + "/saves");
    //    //}

    //    string path = Application.persistentDataPath + "/"/*saves/"*/ + name + ".save";
    //    string jsonString = JsonUtility.ToJson(data);

    //    File.WriteAllText(path, jsonString);
    //    return true;
    //}

    //public object Load(string path)
    //{
    //    if (!File.Exists(path))
    //    {
    //        //Debug.LogError("Creating new save");
    //        Save(new SaveData(), "Save");
    //    }

    //    //string path = Application.persistentDataPath + "/saves/" + name + ".save";
    //    if (File.Exists(path))
    //    {
    //        // Read the entire file and save its contents.
    //        string fileContents = File.ReadAllText(path);

    //        // Deserialize the JSON data 
    //        //  into a pattern matching the GameData class.
    //        return JsonUtility.FromJson<SaveData>(fileContents);
    //    }

    //    return null;
    //}
    #endregion
}