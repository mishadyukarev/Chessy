using Chessy.Common.Component;
using UnityEngine;

namespace Chessy.Common.View.UI.Entity
{
    public readonly struct CanvasUIE
    {
        public readonly CanvasC CanvasC;
        public readonly GameObjectVC MenuCanvasGOC;
        public readonly GameObjectVC GameCanvasGOC;

        public CanvasUIE(in Canvas canvas)
        {
            CanvasC = new CanvasC(canvas);
            MenuCanvasGOC = new GameObjectVC(canvas.transform.Find("Menu+").gameObject);
            GameCanvasGOC = new GameObjectVC(canvas.transform.Find("Game+").gameObject);

            GameCanvasGOC.SetActive(false);
            MenuCanvasGOC.SetActive(true);
        }
    }
}