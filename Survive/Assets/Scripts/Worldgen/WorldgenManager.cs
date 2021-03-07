using System;
using UnityEngine;
using Worldgen;

public class WorldgenManager : MonoBehaviour
{

    public MeshFilter meshFilter;
    

    public Vector2Int size;
    public float PointDistance;
    public Biome defaultBiome;

    private void Awake()
    {
        defaultBiome.validate += () =>
            {
                if(meshFilter != null)
                {
                Map map = createMap();
                meshFilter.mesh = MeshMaker.CreateFloor(map);
            };
        };
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(meshFilter != null && meshFilter.mesh.vertexCount < size.x * size.y)
        {
            Map map = createMap();
            meshFilter.mesh = MeshMaker.CreateFloor(map);
        }
    }

    private Map createMap()
    {
        Map map = new Map(size.x+1, size.y+1);

        for (int i = 0; i < size.x+1; i++)
        {
            for (int j = 0; j < size.y+1; j++)
            {
                map.SetBiome(i,j, ref defaultBiome);
                Vector3 pos = map.GetBiome(i, j).GetPosAt(i * PointDistance, j * PointDistance);

                map.SetPosition(i, j, pos);

                map.SetColor(i, j, 
                    Color.Lerp(
                        new Color(.88f, .6f,.33f),
                        Color.green, 
                        map.GetHeight(i, j) / 5));
            }
        }

        return map;
    }
}
