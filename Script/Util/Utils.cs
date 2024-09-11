using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        if (go.GetComponent<T>() == null)
        {
            T component = go.AddComponent<T>();
            return component;
        }
        else
        {
            return go.GetComponent<T>();
        }
    }
    public static T FindChild<T>(GameObject go) where T : Component
    {
        if (go == null)
            return null;

        int Maxindex = go.transform.childCount;
        for (int i = 0; i < Maxindex; i++)
        {
            if (go.GetComponent<T>() != null)
            {
                T component = go.GetComponent<T>();
                return component;
            }
        }
        return null;
    }
}
