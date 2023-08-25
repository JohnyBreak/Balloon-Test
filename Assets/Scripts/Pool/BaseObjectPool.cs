using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObjectPool<T> : MonoBehaviour where T : MonoBehaviour, IPoolable
{
    [SerializeField] protected T _object;
    [SerializeField] protected int _countToPreLoad = 5;

    protected T _objectPrefab;
    protected List<T> _pooledObjects;
    protected static BaseObjectPool<T> _instance;

    public static BaseObjectPool<T> Instance => _instance;

    protected virtual void Awake()
    {
        if (_instance == null) _instance = this;
        _objectPrefab = _object.gameObject.GetComponent<T>();

        _pooledObjects = new List<T>();
        _pooledObjects.Capacity = _countToPreLoad;

    }

    public T GetPooledObject()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            var tempT = _pooledObjects[i];
            if (tempT == null) continue;

            if (!_pooledObjects[i].gameObject.activeInHierarchy)
            {
                _pooledObjects[i].transform.SetParent(null);
                return (T)_pooledObjects[i];
            }

        }
        T o = CreateNewObject();
        o.gameObject.transform.SetParent(null);
        return o;
    }

    protected T CreateNewObject()
    {
        T obj = Instantiate(_objectPrefab, transform.position, Quaternion.identity);

        DisableObject(obj);
        _pooledObjects.Add(obj);
        return obj;
    }

    public void DisableAllObjects() 
    {
        foreach (var @object in _pooledObjects)
        {
            DisableObject(@object);
        }
    }

    public void DisableObject(T obj)
    {
        if (obj == null) return;
        obj.transform.SetParent(transform);
        obj.gameObject.SetActive(false);
    }
}
