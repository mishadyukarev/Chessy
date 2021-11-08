using UnityEngine;

namespace Chessy.Game
{
    public struct CellFireViewComponent
    {
        private SpriteRenderer _cellFire_SR;

        public CellFireViewComponent(GameObject cell)
        {
            _cellFire_SR = cell.transform.Find("Fire").GetComponent<SpriteRenderer>();
        }

        private void ActiveSR(bool isEnabled) => _cellFire_SR.enabled = isEnabled;

        public void EnableSR() => ActiveSR(true);
        public void DisableSR() => ActiveSR(false);
    }
}
