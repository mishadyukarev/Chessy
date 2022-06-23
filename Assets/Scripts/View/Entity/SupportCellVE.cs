using UnityEngine;

namespace Chessy.Model
{
    public readonly struct SupportCellVE
    {
        public readonly SpriteRendererVC SupportSRC;
        public readonly SpriteRendererVC NoneSRC;

        public SupportCellVE(in Transform cells)
        {
            var sV = cells.Find("SupportVision");

            SupportSRC = new SpriteRendererVC(sV.Find("SupportVision_SR+").GetComponent<SpriteRenderer>());
            NoneSRC = new SpriteRendererVC(sV.Find("NoneVision_SR").GetComponent<SpriteRenderer>());
        }
    }
}
