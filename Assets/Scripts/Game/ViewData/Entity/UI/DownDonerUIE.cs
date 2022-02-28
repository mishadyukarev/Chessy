using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct DownDonerUIE
    {
        public ButtonUIC ButtonC;
        public GameObjectVC Wait;

        public DownDonerUIE(in Transform downZone)
        {
            ButtonC = new ButtonUIC(downZone.Find("DonerButton").GetComponent<Button>());
            Wait = new GameObjectVC(downZone.Find("WaitZone").gameObject);
        }
    }
}
