using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ChunkManager
{
    public Dictionary<Vector3Int, List<BlockController>> chunk = new Dictionary<Vector3Int, List<BlockController>>();
    Grid _grid;
    public void Init()
    {
        _grid = Managers.Instance.gameObject.GetComponent<Grid>();
    }

    public BlockController[] FindGameObjectsInChunks(Vector3Int Pos)
    {
        if (chunk.ContainsKey(Pos))
            return chunk[Pos].ToArray();
        else
        {
            Debug.Log("청크가 존재하지 않습니다.");
            return null;
        }
    }

    public void AddInChunk(BlockController go)
    {
        Vector3Int Pos= _grid.WorldToCell(go.transform.position);
        if (!chunk.ContainsKey(Pos))
            chunk[Pos] = new List<BlockController>();
        chunk[Pos].Add(go);
    }
    public void DisableOutofChunk(Vector3Int Pos)
    {
        if (!chunk.ContainsKey(Pos))
        {
            Debug.Log("청크가 존재하지 않음!");
            return;
        }
        for (int i = 0; i < chunk[Pos].Count; i++)
        {
            if (!chunk[Pos][i].gameObject.activeSelf)
                break;
            chunk[Pos][i].gameObject.SetActive(false);
        }
    }
    public void DisAbleAllObejctsInChunk()
    {
        foreach (Vector3Int i in  chunk.Keys)
        {
            for (int j = 0; j < chunk[i].Count; j++)
            {
                chunk[i][j].gameObject.SetActive(false);
            }
        }
        
    }
    public void AvailiableInChunk(Vector3Int Pos)
    {
        if (!chunk.ContainsKey(Pos))
        {
            Debug.Log("청크가 존재하지 않음!");
            return;
        }
        for (int i = 0; i < chunk[Pos].Count; i++)
        {
            if (chunk[Pos][i].isBreaking)
                continue;
            chunk[Pos][i].gameObject.SetActive(true);
        }
    }
    public Vector3Int GetCurrentCell(GameObject go)
    {
        return _grid.WorldToCell(go.transform.position);
    }
}
