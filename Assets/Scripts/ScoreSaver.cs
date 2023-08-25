using UnityEngine;

public class ScoreSaver : MonoBehaviour
{
    [SerializeField] private SaveScriptable _save;

    //private void Awake()
    //{
    //    foreach (var item in _save.Data.NameScoreDictionary)
    //    {
    //        Debug.Log($"{item.Key} {item.Value}");
    //    }
    //}


    public void OnScoreSave(string name, int score) 
    {
        AddData(name, score);
        _save.Save();
    }

    private void AddData(string name, int score)
    {
        if (!_save.Data.NameScoreDictionary.ContainsKey(name))
        {
            _save.Data.NameScoreDictionary.Add(name, score);
            return;
        }

        if (score < _save.Data.NameScoreDictionary[name]) return;

        _save.Data.NameScoreDictionary[name] = score;
    }
}
