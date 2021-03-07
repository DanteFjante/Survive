using System;
using UnityEngine;
using Worldgen;

[Serializable]
public class Map
{
    public MapPoint[,] map;
    public int width => map.GetLength(0);
    public int height => map.GetLength(1);

    public Map(int width, int height)
    {
        map = new MapPoint[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = new MapPoint();
            }
        }
    }
    
    public void AddBiomeMap(Biome[,] biomemap)
    {
        for (int i = 0; i < biomemap.GetLength(0); i++)
        {
            for (int j = 0; j < biomemap.GetLength(1); j++)
            {
                map[i, j].biome = biomemap[i, j];
            }
        }
    }

    public void SetColor(int x, int y, Color color)
    {
        map[x, y].color = color;
    }
    
    public void SetHeight(int x, int y, float height)
    {
        map[x, y].position.y = height;
    }
    
    public void SetPosition(int x, int y, Vector3 position)
    {
        map[x, y].position = position;
    }
    
    public void SetBiome(int x, int y, ref Biome biome)
    {
        map[x, y].biome = biome;
    }
    
    public float GetHeight(int x, int y)
    {
        return map[x, y].position.y;
    }
    
    public Vector3 GetPosition(int x, int y)
    {
        return map[x, y].position;
    }
    
    public Color GetColor(int x, int y)
    {
        return map[x, y].color;
    }
    
    public Biome GetBiome(int x, int y)
    {
        return map[x, y].biome;
    }


    public struct MapPoint
    {
        public Vector3 position;
        public Color color;
        public Biome biome;
    }
}

