﻿using UnityEngine;

namespace Chessy.Game
{
    public readonly struct SupportCellVE
    {
        public readonly SpriteRendererVC Support;
        public readonly SpriteRendererVC NoneSRC;

        public SupportCellVE(in Transform cells)
        {
            var sV = cells.Find("SupportVision");

            Support = new SpriteRendererVC(sV.Find("SupportVision_SR+").GetComponent<SpriteRenderer>());
            NoneSRC = new SpriteRendererVC(sV.Find("NoneVision_SR").GetComponent<SpriteRenderer>());
        }
    }
}
