using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private LeaderBoardItem _prefab;
    [SerializeField] private SaveScriptable _save;
    [SerializeField] private Transform _parent;

    public void FillBoard() 
    {
        foreach (var item in _save.Data.NameScoreDictionary)
        {
            var boardItem = Instantiate(_prefab, _parent);
            boardItem.SetText($"{item.Key} - {item.Value}");
        }
    }

}
