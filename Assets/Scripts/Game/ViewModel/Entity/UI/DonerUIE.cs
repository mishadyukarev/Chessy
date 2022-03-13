using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game.Entity.View.UI.Down
{
    public sealed class DonerUIE
    {
        public readonly ButtonUIC ButtonC;
        public readonly GameObjectVC WaitGoC;

        public DonerUIE(in Transform downZone)
        {
            ButtonC = new ButtonUIC(downZone.Find("DonerButton").GetComponent<Button>());
            WaitGoC = new GameObjectVC(downZone.Find("WaitZone").gameObject);
        }
    }
}
