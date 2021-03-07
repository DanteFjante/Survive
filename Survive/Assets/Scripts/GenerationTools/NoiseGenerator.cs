using System;
using UnityEngine;

namespace Worldgen
{
    [Serializable]
    public class NoiseGenerator
    {
        public NoiseData data;
        public float amplify;
        public float heightOffset;
        public bool warpPosition;
    }
}