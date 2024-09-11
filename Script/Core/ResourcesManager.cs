using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using static UnityEditor.FilePathAttribute;

public class ResourcesManager
{
    Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();
    
    public GameObject Instantiate(string key  ,Vector3 pos, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>(key);
        if (prefab == null)
            return null;
        GameObject go = GameObject.Instantiate(prefab, parent);
        go.name = prefab.name;
        go.transform.position = pos;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null) return;

        Destroy(go);
    }

    public  T Load<T>(string key) where T : UnityEngine.Object
    {
        if (_resources.TryGetValue(key, out UnityEngine.Object _resource))
            return _resource as T;
        return null;
    }
    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        if ( _resources.TryGetValue(key,out UnityEngine.Object _resource))
        {
            callback?.Invoke(_resources as T);
            return;
        }
        var op = Addressables.LoadAssetAsync<T>(key);
        op.Completed += (obj) =>
        {
            _resources.Add(key, obj.Result);
            callback?.Invoke(obj.Result as T);
        };
    }
    public void LoadAllAsync<T>(string label, Action callback = null) where T : UnityEngine.Object
    {
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (AsyncOperationHandle<IList<IResourceLocation>> obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                bool[] isCompleted = new bool[obj.Result.Count];
                for (int i = 0; i < obj.Result.Count; i++)
                {
                    int index = i;
                    var loadOp = Addressables.LoadAssetAsync<T>(obj.Result[i].PrimaryKey);
                    loadOp.Completed += (AsyncOperationHandle<T> loadObj) =>
                    {
                        if (loadObj.Status == AsyncOperationStatus.Succeeded)
                        {
                            
                            _resources.Add(obj.Result[index].PrimaryKey, loadObj.Result);
                            isCompleted[index] = true;
                            bool AllCompleted = true;
                            for (int j = 0; j < obj.Result.Count; j++)
                            {
                                AllCompleted &= isCompleted[j];
                            }
                            if (AllCompleted)
                            {
                                callback?.Invoke();
                            }
                        }
                        else
                        {
                            Debug.LogError($"Failed to load resource: {obj.Result[i].PrimaryKey}");
                        }
                    };
                }
            }
            else
            {
                Debug.LogError($"Failed to load resource locations for label: {label}");
            }
        };
    }
}
