using UnityEngine;

namespace Chessy.Game
{
    public struct CellStunViewC
    {
        private SpriteRenderer _stun;

        public CellStunViewC(Transform cell)
        {
            _stun = cell.Find("Stun_SR").GetComponent<SpriteRenderer>();
        }

        public void SetEnabled(bool isEnabled) => _stun.enabled = isEnabled;
    }
}