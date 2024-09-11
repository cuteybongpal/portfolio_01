using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Managers : MonoBehaviour
{
    static Managers instance = null;
    public static Managers Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Managers>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("Managers");
                    instance = managerObject.AddComponent<Managers>();
                }
            }
            return instance;
        }
    }
    PerlinNoiseMap pnm;
    ResourcesManager _resource = new ResourcesManager();
    InputManager _input = new InputManager();
    ObjectManager _object = new ObjectManager();
    DataManager _dataManager = new DataManager();
    ChunkManager _chunk = new ChunkManager();
    Grid _grid;
    public static ResourcesManager Resource { get { return Instance._resource; } }
    public static InputManager Input { get { return Instance._input; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static DataManager DataManager { get { return Instance._dataManager; } }
    public static ChunkManager Chunk { get { return Instance._chunk; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Define.Init();
            Chunk.Init();
            pnm = GetComponent<PerlinNoiseMap>();
            _grid = GetComponent<Grid>();
            Resource.LoadAllAsync<Material>("PreLoad(Mat)", () =>
            {
                Resource.LoadAllAsync<TextAsset>("PreLoad(Data)", () =>
                {
                    Resource.LoadAllAsync<GameObject>("PreLoad(Prefab)", () =>
                    {
                        DataManager.Init();
                        pnm.Init();
                        Object.Spawn<PlayerController>(new Vector3(50, 60, 0),"Player.prefab");
                    });
                });
            });
        }
    }
    private void Start()
    {
        StartCoroutine(_update());
    }
    IEnumerator _update()
    {
        while (true)
        {
            yield return null;
            _input.Update();
        }
    }
}
