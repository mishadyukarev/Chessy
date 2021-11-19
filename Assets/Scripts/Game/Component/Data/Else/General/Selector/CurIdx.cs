using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public struct CurIdx
    {
        public static byte Idx { get; set; }
        public static bool IsStartDirectToCell => Idx == default;
    }
}