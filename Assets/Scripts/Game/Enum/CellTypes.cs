using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public enum CellTypes
    {
        None,

        Cell,
        First = Cell,

        Unit,
        Build,
        Env,
        Trail,

        Else,

        End
    }
}