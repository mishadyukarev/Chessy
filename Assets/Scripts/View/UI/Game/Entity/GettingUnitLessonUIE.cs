using Chessy.Common.Component;
using UnityEngine;

namespace Chessy.Game.View.UI.Entity
{
    public readonly struct GettingUnitLessonUIE
    {
        public readonly GameObjectVC ParentGOC;

        public GettingUnitLessonUIE(in Transform gettingGod)
        {
            ParentGOC = new GameObjectVC(gettingGod.gameObject);
        }
    }
}