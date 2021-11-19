using UnityEngine;

namespace Game.Game
{
    public struct StunVC
    {
        private SpriteRenderer _stun;

        public StunVC(Transform cell)
        {
            _stun = cell.Find("Stun_SR").GetComponent<SpriteRenderer>();
        }

        public void SetEnabled(bool isEnabled) => _stun.enabled = isEnabled;
    }
}