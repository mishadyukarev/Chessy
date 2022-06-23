using Chessy.Common.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Model.Entity.View.UI.Down
{
    public struct DonerUIE
    {
        public readonly ButtonUIC ButtonC;
        public readonly GameObjectVC WaitGoC;
        internal readonly ImageUIC ImageC;

        public DonerUIE(in Transform downZone)
        {
            var donerReady = downZone.Find("DonerReady+");
            ButtonC = new ButtonUIC(donerReady.Find("Button+").GetComponent<Button>());
            ImageC = new ImageUIC(donerReady.Find("Image+").GetComponent<Image>());
            WaitGoC = new GameObjectVC(downZone.Find("WaitZone").gameObject);
        }
    }
}
