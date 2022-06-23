using Chessy.Common.Component;
using UnityEngine;

namespace Chessy.Model.View.UI.Entity
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