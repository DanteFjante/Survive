using System.Collections.Generic;
using UnityEngine;

public static class MeshMaker
{

    public static Mesh CreateFloor(Map map)
    {
        List<int> indices = new List<int>();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<Color> colors = new List<Color>();
        
        for (int i = 0; i < map.width; i++)
        {
            for (int j = 0; j < map.height; j++)
            {

                vertices.Add(map.GetPosition(i,j));

                if(i < map.width -1 && j < map.height - 1)
                {
                    int width = map.width;
                    indices.Add(i + j * width + 1);
                    indices.Add(i + j * width + width);
                    indices.Add(i + j * width);
                    
                    indices.Add(i + j * width + 1);
                    indices.Add(i + j * width + width + 1);
                    indices.Add(i + j * width + width);
                }

                uvs.Add(new Vector2(i, j));
                
                colors.Add(map.GetColor(i, j));
            }
        }
        
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = indices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.colors = colors.ToArray();

        return mesh;
    }

}
