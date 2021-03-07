using GenerationTools;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Worldgen
{
    public struct NoiseGenJob : IJobParallelFor
    {
        public int width;
        public NoiseData noise;
        public NativeArray<float3> inputVectors;
        public NativeArray<float3> returnVectors;


        public void Execute(int index)
        {
            int x = index % width;
            int y = index / width;

            float3 vec = inputVectors[index];
            noise.GetWarpDomainValue(ref vec);
            returnVectors[index] = vec;
        }
    }
}