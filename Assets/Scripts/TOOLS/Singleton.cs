using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//单例基类
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance => instance;

    //相当于构造器？
    protected virtual void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
            instance = (T) this;
        
        //DontDestroyOnLoad(this);
    }

    //是否已经初始化
    public static bool IsInitialized
    {
        get { return instance; }
    }

    //销毁时把静态变量清空
    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
