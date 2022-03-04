using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public sealed class DownDonerUIE
    {
        public ButtonUIC ButtonC;
        public GameObjectVC WaitGoC;

        public DownDonerUIE(in Transform downZone)
        {
            ButtonC = new ButtonUIC(downZone.Find("DonerButton").GetComponent<Button>());
            WaitGoC = new GameObjectVC(downZone.Find("WaitZone").gameObject);
        }
    }
}
