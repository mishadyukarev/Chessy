using UnityEngine;

namespace Scripts.Game
{
    internal struct CellFireViewComponent
    {
        private SpriteRenderer _cellFire_SR;

        internal CellFireViewComponent(GameObject cell)
        {
            _cellFire_SR = cell.transform.Find("Fire").GetComponent<SpriteRenderer>();
        }

        private void ActiveSR(bool isEnabled) => _cellFire_SR.enabled = isEnabled;

        internal void EnableSR() => ActiveSR(true);
        internal void DisableSR() => ActiveSR(false);
    }
}
