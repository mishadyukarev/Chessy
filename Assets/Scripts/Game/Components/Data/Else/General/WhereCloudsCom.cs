using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    internal struct WhereCloudsCom
    {
        private List<byte> _clouds;

        internal WhereCloudsCom(bool needNew) : this()
        {
            if (needNew) _clouds = new List<byte>();
        }

        internal void Add(byte idxCell) => _clouds.Add(idxCell);
        internal void Remove(byte idxCell) => _clouds.Remove(idxCell);

        internal byte Cloud => _clouds[0];
    }
}