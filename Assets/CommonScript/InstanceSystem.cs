using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceSystem<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if(_instance == null)
                {
                    var typeName = typeof(T);
                    Debug.LogError($"{typeName}�X�N���v�g��������܂���");
                }
            }
            return _instance;
        }
    }
    virtual protected void Awake2()
    {
        if (FindObjectsOfType<T>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}