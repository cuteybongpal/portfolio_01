using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public int LoadDistance = 1;
    Vector3Int _currentPos;
    Vector3Int CurrentPos {
        get {
            return _currentPos;
        }
        set
        {
            if (CurrentPos == value)
                _currentPos = value;
            else
            {
                if (_currentPos.x > value.x)
                    Managers.Chunk.DisableOutofChunk(new Vector3Int(_currentPos.x + 2, _currentPos.y));
                else
                    Managers.Chunk.DisableOutofChunk(new Vector3Int(_currentPos.x - 2, _currentPos.y));
                _currentPos = value;
                Managers.Chunk.AvailiableInChunk(_currentPos);
                Managers.Chunk.AvailiableInChunk(new Vector3Int(_currentPos.x + 1, _currentPos.y));
                Managers.Chunk.AvailiableInChunk(new Vector3Int(_currentPos.x - 1, _currentPos.y));
            }
        }
    }

    private void Start()
    {
        Managers.Chunk.DisAbleAllObejctsInChunk();
        StartCoroutine(LoadMap());
        
    }
    IEnumerator LoadMap()
    {
        while (true)
        {
            yield return null;
            CurrentPos = Managers.Chunk.GetCurrentCell(gameObject);
        }
    }

}
