using UnityEngine;

namespace Scripts.Game
{
    internal struct GenerZoneViewCom
    {
        private GameObject _gameGeneralZone_GO;

        internal GenerZoneViewCom(GameObject gameGeneralZone_GO) => _gameGeneralZone_GO = gameGeneralZone_GO;

        internal void Attach(Transform transForAttach) => transForAttach.transform.SetParent(_gameGeneralZone_GO.transform);
    }
}
