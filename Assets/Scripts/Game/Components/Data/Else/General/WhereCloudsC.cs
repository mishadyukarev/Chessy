using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    internal struct WhereCloudsC
    {
        private static List<byte> _clouds;

        internal WhereCloudsC(bool needNew) : this()
        {
            if (needNew) _clouds = new List<byte>();
        }

        //internal static List<byte> Clouds => _clouds.Copy();

        internal static void Add(byte idxCell) => _clouds.Add(idxCell);
        internal static void Remove(byte idxCell) => _clouds.Remove(idxCell);

        internal static byte Cloud => _clouds[0];
    }
}