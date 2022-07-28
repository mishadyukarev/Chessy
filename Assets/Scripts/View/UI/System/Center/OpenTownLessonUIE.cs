using Chessy.View.Component;
using UnityEngine;
namespace Chessy.View.UI.Entity
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