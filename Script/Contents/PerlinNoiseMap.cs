using UnityEngine;
public class PerlinNoiseMap : MonoBehaviour
{
    enum tileType
    {
        Grass = 0,
    }
    float[,] PreNoise;
    block_info[,] map;
    Vector2Int[] random_dir = new Vector2Int[]
    {
        new Vector2Int(-1,1), new Vector2Int(0, 1), new Vector2Int(1, 1),
        new Vector2Int(-1, 0),                      new Vector2Int(1,0),
        new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, - 1)
    };
    public int map_width = 16;
    public int map_height = 9;
    [Range(0f, 1f)]
    public float positive_weight = 0.1f;
    [Range(0f, 1f)]
    public float negative_weight = 0.1f;
    [Range(0f, 0.25f)]
    public float sub_positive_weight = 0.1f;
    [Range(1, 100)]
    public int scale = 2;
    public int MaxHeight;
    public int MinHeight;
    public void Init()
    {
        GenerateTerrain();
    }
    
    void GenerateTerrain()
    {
        float[,] PerlinNoise = CreatePerlinNoise(map_width, map_height, true);
        map = new block_info[map_height, map_width];
        for (int x =  0; x < map_width; x++)
        {
            int yPos = (int)(PerlinNoise[0,x] * scale);
            if (yPos > MaxHeight)
                yPos = MaxHeight;
            else if (yPos < MinHeight)
                yPos = MinHeight;
            map[yPos + map_height / 2, x] = new block_info(Define.block.grass);
            map[yPos + map_height / 2 - 1, x] = new block_info(Define.block.Dirt);
            map[yPos + map_height / 2 - 2, x] = new block_info(Define.block.Dirt);
            for (int y = 0; y < map_height; y++)
            {
                if (map[y, x] == null)
                    map[y, x] = new block_info(Define.block.Rock);
                else
                    break;
            }
        }
        float[,] mineral = CreatePerlinNoise(map_width, map_height / 2, false);
        for (int y = 0; y < mineral.GetLength(0); y++)
        {
            for (int x = 0; x < mineral.GetLength(1); x++)
            {
                if (mineral[y, x] >= 0.95f)
                {
                    if (map[y, x].block == Define.block.Dirt || map[y, x].block == Define.block.grass)
                        continue;
                    map[y, x] = new block_info(Define.block.Coal);
                }
            }
        }
        for (int y = 0; y < map_height; y++)
        {
            for (int x = 0;x < map_width; x++)
            {
                if (map[y, x] == null)
                    continue;
                GameObject go =Managers.Object.Spawn<BlockController>(new Vector3(x, y, 0), "block.prefab").gameObject;
                if (Managers.Resource.Load<Material>("Terrain.mat") == null)
                    Debug.Log("daf");

                switch (map[y, x].block)
                {
                    case Define.block.grass:
                        go.GetComponent<MeshRenderer>().material = Managers.Resource.Load<Material>("Terrain.mat");
                        break;
                    case Define.block.Dirt:
                        go.GetComponent<MeshRenderer>().material = Managers.Resource.Load<Material>("Dirt.mat");
                        break;
                    case Define.block.Rock:
                        go.GetComponent<MeshRenderer>().material = Managers.Resource.Load<Material>("Rock.mat");
                        break;
                    case Define.block.Coal:
                        go.GetComponent<MeshRenderer>().material = Managers.Resource.Load<Material>("Coal.mat");
                        break;
                }
            }
        }
    }
    float[,] CreatePerlinNoise(int width, int height, bool isLerp)
    {
        int[,] DirCount;
        if (isLerp)
        {
            PreNoise = new float[height, width / 100];
            DirCount = new int[height, width / 100];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width / 100; x++)
                {
                    DirCount[y, x] = Random.Range(0, 8);
                }
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width / 100; x++)
                {
                    switch (DirCount[y, x])
                    {
                        case 0:
                            AddWeight(x, y, 0, positive_weight, width / 100, height);
                            AddWeight(x, y, 1, sub_positive_weight, width / 100, height);
                            AddWeight(x, y, 3, sub_positive_weight, width / 100, height);

                            AddWeight(x, y, 6, negative_weight, width / 100, height);
                            AddWeight(x, y, 7, negative_weight, width / 100, height);
                            AddWeight(x, y, 4, negative_weight, width / 100, height);
                            break;
                        case 1:
                            AddWeight(x, y, 0, sub_positive_weight, width / 100, height);
                            AddWeight(x, y, 1, positive_weight, width / 100, height);
                            AddWeight(x, y, 2, sub_positive_weight, width / 100, height);

                            AddWeight(x, y, 5, negative_weight, width / 100, height);
                            AddWeight(x, y, 6, negative_weight, width / 100, height);
                            AddWeight(x, y, 7, negative_weight, width / 100, height);
                            break;
                        case 2:
                            AddWeight(x, y, 1, sub_positive_weight, width / 100, height);
                            AddWeight(x, y, 2, positive_weight, width / 100, height);
                            AddWeight(x, y, 4, sub_positive_weight, width / 100, height);

                            AddWeight(x, y, 3, negative_weight, width / 100, height);
                            AddWeight(x, y, 5, negative_weight, width / 100, height);
                            AddWeight(x, y, 6, negative_weight, width / 100, height);
                            break;
                        case 3:
                            AddWeight(x, y, 0, sub_positive_weight, width / 100, height);
                            AddWeight(x, y, 3, positive_weight, width / 100, height);
                            AddWeight(x, y, 5, sub_positive_weight, width / 100, height);

                            AddWeight(x, y, 2, negative_weight, width / 100, height);
                            AddWeight(x, y, 4, negative_weight, width / 100, height);
                            AddWeight(x, y, 7, negative_weight, width / 100, height);
                            break;
                        case 4:
                            AddWeight(x, y, 2, sub_positive_weight, width / 100, height);
                            AddWeight(x, y, 4, positive_weight, width / 100, height);
                            AddWeight(x, y, 7, sub_positive_weight, width / 100, height);

                            AddWeight(x, y, 0, negative_weight, width / 100, height);
                            AddWeight(x, y, 3, negative_weight, width / 100, height);
                            AddWeight(x, y, 5, negative_weight, width / 100, height);
                            break;
                        case 5:
                            AddWeight(x, y, 3, sub_positive_weight, width / 100, height);
                            AddWeight(x, y, 5, positive_weight, width / 100, height);
                            AddWeight(x, y, 6, sub_positive_weight, width / 100, height);

                            AddWeight(x, y, 1, negative_weight, width / 100, height);
                            AddWeight(x, y, 2, negative_weight, width / 100, height);
                            AddWeight(x, y, 4, negative_weight, width / 100, height);
                            break;
                        case 6:
                            AddWeight(x, y, 5, sub_positive_weight, width / 100, height);
                            AddWeight(x, y, 6, positive_weight, width / 100, height);
                            AddWeight(x, y, 7, sub_positive_weight, width / 100, height);

                            AddWeight(x, y, 0, negative_weight, width / 100, height);
                            AddWeight(x, y, 1, negative_weight, width / 100, height);
                            AddWeight(x, y, 2, negative_weight, width / 100, height);
                            break;
                        case 7:
                            AddWeight(x, y, 4, sub_positive_weight, width / 100, height);
                            AddWeight(x, y, 7, positive_weight, width / 100, height);
                            AddWeight(x, y, 6, sub_positive_weight, width / 100, height);

                            AddWeight(x, y, 3, negative_weight, width / 100, height);
                            AddWeight(x, y, 0, negative_weight, width / 100, height);
                            AddWeight(x, y, 1, negative_weight, width / 100, height);
                            break;
                    }
                }
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width / 100; x++)
                {
                    if (PreNoise[y, x] > 1)
                        PreNoise[y, x] = 1;
                    else if (PreNoise[y, x] < -1)
                        PreNoise[y, x] = -1;
                    if (PreNoise[y, x] > 0)
                        PreNoise[y, x] = 6 * Mathf.Pow(PreNoise[y, x], 5) - 15 * Mathf.Pow(PreNoise[y, x], 4) + 10 * Mathf.Pow(PreNoise[y, x], 3);
                    else
                        PreNoise[y, x] = 6 * Mathf.Pow(PreNoise[y, x], 5) + 15 * Mathf.Pow(PreNoise[y, x], 4) + 10 * Mathf.Pow(PreNoise[y, x], 3);
                }
            }
            float[,] noise = new float[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int i = 0; i < width / 100 - 1; i++)
                {
                    for (int x = 0; x < 100; x++)
                    {
                        noise[y, i * 100 + x] = Mathf.Lerp(PreNoise[y, i], PreNoise[y, i + 1], (float)x / 100f);
                    }
                }
            }
            return noise;
        }
        else
        {
            PreNoise = new float[height, width];
            DirCount = new int[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    DirCount[y, x] = Random.Range(0, 8);
                }
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    switch (DirCount[y, x])
                    {
                        case 0:
                            AddWeight(x, y, 0, positive_weight, width, height);
                            AddWeight(x, y, 1, sub_positive_weight, width, height);
                            AddWeight(x, y, 3, sub_positive_weight, width, height);

                            AddWeight(x, y, 6, negative_weight, width, height);
                            AddWeight(x, y, 7, negative_weight, width, height);
                            AddWeight(x, y, 4, negative_weight, width, height);
                            break;
                        case 1:
                            AddWeight(x, y, 0, sub_positive_weight, width, height);
                            AddWeight(x, y, 1, positive_weight, width, height);
                            AddWeight(x, y, 2, sub_positive_weight, width, height);

                            AddWeight(x, y, 5, negative_weight, width, height);
                            AddWeight(x, y, 6, negative_weight, width, height);
                            AddWeight(x, y, 7, negative_weight, width, height);
                            break;
                        case 2:
                            AddWeight(x, y, 1, sub_positive_weight, width, height);
                            AddWeight(x, y, 2, positive_weight, width, height);
                            AddWeight(x, y, 4, sub_positive_weight, width, height);

                            AddWeight(x, y, 3, negative_weight, width, height);
                            AddWeight(x, y, 5, negative_weight, width, height);
                            AddWeight(x, y, 6, negative_weight, width, height);
                            break;
                        case 3:
                            AddWeight(x, y, 0, sub_positive_weight, width, height);
                            AddWeight(x, y, 3, positive_weight, width, height);
                            AddWeight(x, y, 5, sub_positive_weight, width, height);

                            AddWeight(x, y, 2, negative_weight, width, height);
                            AddWeight(x, y, 4, negative_weight, width, height);
                            AddWeight(x, y, 7, negative_weight, width, height);
                            break;
                        case 4:
                            AddWeight(x, y, 2, sub_positive_weight, width, height);
                            AddWeight(x, y, 4, positive_weight, width, height);
                            AddWeight(x, y, 7, sub_positive_weight, width, height);

                            AddWeight(x, y, 0, negative_weight, width, height);
                            AddWeight(x, y, 3, negative_weight, width, height);
                            AddWeight(x, y, 5, negative_weight, width, height);
                            break;
                        case 5:
                            AddWeight(x, y, 3, sub_positive_weight, width, height);
                            AddWeight(x, y, 5, positive_weight, width, height);
                            AddWeight(x, y, 6, sub_positive_weight, width, height);

                            AddWeight(x, y, 1, negative_weight, width, height);
                            AddWeight(x, y, 2, negative_weight, width, height);
                            AddWeight(x, y, 4, negative_weight, width, height);
                            break;
                        case 6:
                            AddWeight(x, y, 5, sub_positive_weight, width, height);
                            AddWeight(x, y, 6, positive_weight, width, height);
                            AddWeight(x, y, 7, sub_positive_weight, width, height);

                            AddWeight(x, y, 0, negative_weight, width, height);
                            AddWeight(x, y, 1, negative_weight, width, height);
                            AddWeight(x, y, 2, negative_weight, width, height);
                            break;
                        case 7:
                            AddWeight(x, y, 4, sub_positive_weight, width, height);
                            AddWeight(x, y, 7, positive_weight, width, height);
                            AddWeight(x, y, 6, sub_positive_weight, width, height);

                            AddWeight(x, y, 3, negative_weight, width, height);
                            AddWeight(x, y, 0, negative_weight, width, height);
                            AddWeight(x, y, 1, negative_weight, width, height);
                            break;
                    }
                }
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (PreNoise[y, x] > 1)
                        PreNoise[y, x] = 1;
                    else if (PreNoise[y, x] < -1)
                        PreNoise[y, x] = -1;
                    if (PreNoise[y, x] > 0)
                        PreNoise[y, x] = 6 * Mathf.Pow(PreNoise[y, x], 5) - 15 * Mathf.Pow(PreNoise[y, x], 4) + 10 * Mathf.Pow(PreNoise[y, x], 3);
                    else
                        PreNoise[y, x] = 6 * Mathf.Pow(PreNoise[y, x], 5) + 15 * Mathf.Pow(PreNoise[y, x], 4) + 10 * Mathf.Pow(PreNoise[y, x], 3);
                }
            }
            float[,] noise = PreNoise;

            return noise;
        }
    }
    void AddWeight(int x, int y,int index, float w, int max_width, int max_height)
    {
        if (x + random_dir[index].x >= max_width || x + random_dir[index].x < 0)
            return;
        if (y + random_dir[index].y >= max_height || y + random_dir[index].y < 0)
            return;
        PreNoise[y + random_dir[index].y, x + random_dir[index].x] += w;

    }
}
public class block_info
{
    public Define.block block;

    public block_info(Define.block _block)
    {
        this.block = _block;
    }
}