using UnityEngine;

public class LeaderBoardItem : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;

    public void SetText(string text) 
    {
        _text.text = text;
    }
}
