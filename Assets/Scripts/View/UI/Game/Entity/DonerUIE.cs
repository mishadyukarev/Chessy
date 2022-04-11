using Chessy.Common.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game.Entity.View.UI.Down
{
    public struct DonerUIE
    {
        public readonly ButtonUIC ButtonC;
        public readonly GameObjectVC WaitGoC;

        public DonerUIE(in Transform downZone)
        {
            ButtonC = new ButtonUIC(downZone.Find("DonerButton").GetComponent<Button>());
            WaitGoC = new Chessy.Common.Component.GameObjectVC(downZone.Find("WaitZone").gameObject);
        }
    }
}
