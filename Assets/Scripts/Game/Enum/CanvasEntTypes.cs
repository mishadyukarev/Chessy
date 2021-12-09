using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public enum CanvasEntTypes
    {
        None,

        //Up
        Leave,
        First = Leave,
        Alpha,
        DirectWind,

        //Center
        EndGame,
        Motion,

        End,
    }
}