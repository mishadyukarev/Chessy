using UnityEditor;
using UnityEngine;

namespace Chessy.Game
{
    public struct IdxCur
    {
        public static byte Idx { get; set; }
        public static bool IsStartDirectToCell => Idx == default;
    }
}