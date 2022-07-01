using Chessy.Model.Component;
using Chessy.View.Component;
using UnityEngine;
namespace Chessy.View.UI.Entity
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