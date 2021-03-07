using System;
using GenerationTools;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Worldgen
{
    public class GeneratorJobTest : MonoBehaviour
    {

        public NoiseData TestNoise;

        public int width, height;


        public MeshFilter filter;
        
        
        private NoiseGenJob jobs;

        private void Start()
        {
            jobs = new NoiseGenJob();
            jobs.noise = TestNoise;
            jobs.width = width;

            jobs.inputVectors = new NativeArray<float3>(width * height, Allocator.TempJob);
            
            jobs.returnVectors = new NativeArray<float3>(width * height, Allocator.TempJob);

            JobHandle handle = jobs.Schedule(width * height, 64);
            handle.Complete();

            Map map = new Map(width, height);
            for (int i = 0; i < map.width; i++)
            {
                for (int j = 0; j < map.height; j++)
                {
                    map.SetPosition(i, j, jobs.returnVectors[i + j * width]);
                    map.SetColor(i, j, Color.Lerp(Color.yellow, Color.green, map.GetHeight(i, j)));
                }
                
            }

            filter.mesh = MeshMaker.CreateFloor(map);
            jobs.inputVectors.Dispose();
            jobs.returnVectors.Dispose();

        }
    }
}