using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : BaseController
{
    public enum Attribute
    {
        UnBreakable,
        Breakable,
        None,
    }
    public Attribute[] attributes;
    public Texture2D[] crackingTexture;
    int damagedlv = 0;
    Material material;
    float BreakTime;
    public float breakingTime;
    public bool isBreaking;
    public int DamagedLevel
    {
        get { return damagedlv; }
        set
        {
            damagedlv = value;

            if (damagedlv != 0)
            {
                if (damagedlv >= 4)
                {
                    gameObject.SetActive(false);
                    material.mainTexture = null;
                    isBreaking = true;
                    return;
                }
                //material.SetTexture = crackingTexture[damagedlv];
            }
            else
                material.mainTexture = null;
        }
    }
    protected override void Init()
    {
        int[] triangles = new int[6]
        {
            0,2,3,3,1,0
        };
        MeshFilter filter = GetComponent<MeshFilter>();
        filter.mesh.SetTriangles(triangles, 0);
        
        material = GetComponent<MeshRenderer>().material;
    }
    public void BlockBreaking()
    {
        BreakTime += Time.deltaTime;
        if (breakingTime / 4 < BreakTime)
        {
            DamagedLevel++;
            BreakTime = 0;
        }
    }
    public void UnBreaking()
    {
        BreakTime = 0;
        DamagedLevel = 0;
    }
}
