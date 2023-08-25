using UnityEngine;

public abstract class BaseUIScreen : MonoBehaviour
{
    [SerializeField] protected GameObject _holder;

    public virtual void ShowScreen()
    {
        _holder.SetActive(true);
    }

    public void HideScreen() 
    {
        _holder.SetActive(false);
    }
}
