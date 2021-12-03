﻿using UnityEngine;

namespace Game.Game
{
    public struct FireVC : IFireCellV
    {
        private SpriteRenderer _cellFire_SR;

        public FireVC(GameObject cell)
        {
            _cellFire_SR = cell.transform.Find("Fire").GetComponent<SpriteRenderer>();
        }

        private void ActiveSR(bool isEnabled) => _cellFire_SR.enabled = isEnabled;

        public void EnableSR() => ActiveSR(true);
        public void DisableSR() => ActiveSR(false);
    }
}