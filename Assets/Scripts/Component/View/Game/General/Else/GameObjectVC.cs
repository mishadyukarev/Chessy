using UnityEngine;

namespace Game.Game
{
    public struct GameObjectVC : IBackgroundE, IGeneralZoneE
    {
        readonly GameObject _gO;

        public string Name => _gO.name;
        internal Transform Transform => _gO.transform;

        internal GameObjectVC(in GameObject gO)
        {
            _gO = gO;
        }

        public void SetActive(in bool needActive) => _gO.SetActive(needActive);
        public void SetActiveParent(in bool needActive) => _gO.transform.parent.gameObject.SetActive(needActive);
    }
}