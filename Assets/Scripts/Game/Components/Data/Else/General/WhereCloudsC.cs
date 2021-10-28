using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    public struct WhereCloudsC
    {
        private static List<byte> _clouds;

        public WhereCloudsC(bool needNew) : this()
        {
            if (needNew) _clouds = new List<byte>();
        }

        //public static List<byte> Clouds => _clouds.Copy();

        public static void Add(byte idxCell) => _clouds.Add(idxCell);
        public static void Remove(byte idxCell) => _clouds.Remove(idxCell);

        public static byte Cloud => _clouds[0];
    }
}