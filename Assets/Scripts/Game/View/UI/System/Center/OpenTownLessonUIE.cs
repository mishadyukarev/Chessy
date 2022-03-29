using Chessy.Common.Component;
using UnityEditor;
using UnityEngine;

namespace Chessy.Game.View.UI.Entity
{
    public readonly struct OpenTownLessonUIE
    {
        public readonly GameObjectVC ParentGOC;

        internal OpenTownLessonUIE(in GameObject parentGO)
        {
            ParentGOC = new GameObjectVC(parentGO);
        }
    }
}