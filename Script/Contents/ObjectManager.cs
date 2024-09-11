using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class ObjectManager
{
    public PlayerController player;
    public HashSet<MonsterController> monsters { get; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>();

    public T Spawn<T>(Vector3 pos ,string key = "", int Id = 0) where T : BaseController
    {
        Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            GameObject go = Managers.Resource.Instantiate(key, pos);
            go.name = "Player";
            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            player = pc;
            return pc as T;
        }
        else if ( type == typeof(MonsterController))
        {
            string Key = "";
            if (Id == 1)
                Key = "Oak.prefab";
            else if (Id == 2)
                Key = "Goblin.prefab";
            else if (Id == 3)
                Key = "Slime.prefab";
            else if (Id == 4)
                Key = "Wybern.prefab";
            else if (Id == 5)
                Key = "Golem.prefab";
            else
                Debug.Log("아이디가 맞질 않습니다.");
            
            GameObject go = Managers.Resource.Instantiate(Key, pos);

            MonsterController mc = go.GetOrAddComponent<MonsterController>();
            return mc as T;
        }
        else if (type == typeof (BlockController))
        {
            GameObject go = Managers.Resource.Instantiate(key, pos);
            BlockController bc = go.GetOrAddComponent<BlockController>();
            Managers.Chunk.AddInChunk(bc);
            return bc as T;
        }
        return null;
    }
    public void DeSpawn<T>(T obj) where T : BaseController
    {
        Type type  = typeof (T);
        if (type == typeof(PlayerController))
        {
            Debug.Log("플레이어 죽음ㅋ");
            player = null;
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(MonsterController))
        {
            if (monsters.Contains(obj as MonsterController))
            {
                monsters.Remove(obj as MonsterController);
                Managers.Resource.Destroy(obj.gameObject);
            }
        }
        else if (type == typeof(ProjectileController))
        {
            Projectiles.Remove(obj as ProjectileController);
            Managers.Resource.Destroy (obj.gameObject);
        }
    }
}
