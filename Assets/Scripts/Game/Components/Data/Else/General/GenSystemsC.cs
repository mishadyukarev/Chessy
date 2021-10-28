﻿using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Game.Components.Data.Else.General
{
    public class GenSystemsC : ScriptableObject
    {
        [MenuItem("Tools/MyTool/Do It in C#")]
        static void DoIt()
        {
            EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        }
    }
}