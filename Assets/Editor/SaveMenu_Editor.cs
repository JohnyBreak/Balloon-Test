using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class SaveMenu_Editor : MonoBehaviour
{
    private static string _path = Application.persistentDataPath + "/Save.save";
    private static string _directoryPath = Application.persistentDataPath;

    [MenuItem("Save Menu/DeleteSave")]
    public static void DeleteSave() 
    {
        if (System.IO.File.Exists(_path) == false) return;

        System.IO.File.Delete(_path);

    }

    [MenuItem("Save Menu/Open save folder")]
    public static void OpenSaveFolder()
    {
        if (System.IO.Directory.Exists(_directoryPath) == false) return;

        Process.Start(_directoryPath);

    }
}
