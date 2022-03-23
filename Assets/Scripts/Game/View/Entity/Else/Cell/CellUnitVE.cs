using UnityEngine;

namespace Chessy.Game
{
    public class CellUnitVE
    {
        public SpriteRendererVC SpriteRenderC;

        internal CellUnitVE(in SpriteRenderer sr)
        {
            SpriteRenderC = new SpriteRendererVC(sr);
        }

        public void Enable(in bool isVisForNext)
        {
            SpriteRenderC.Enable();
            if (isVisForNext) SpriteRenderC.Color = new Color(SpriteRenderC.Color.r, SpriteRenderC.Color.g, SpriteRenderC.Color.b, 1);
            else SpriteRenderC.Color = new Color(SpriteRenderC.Color.r, SpriteRenderC.Color.g, SpriteRenderC.Color.b, 0.6f);
        }
    }
}