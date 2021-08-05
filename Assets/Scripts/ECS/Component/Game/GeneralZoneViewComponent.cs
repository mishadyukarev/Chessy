using UnityEngine;

namespace Assets.Scripts.ECS.Component.Game
{
    internal struct GeneralZoneViewComponent
    {
        private GameObject _gameGeneralZone_GO;

        internal GeneralZoneViewComponent(GameObject gameGeneralZone_GO) => _gameGeneralZone_GO = gameGeneralZone_GO;

        internal void Attach(Transform transForAttach) => transForAttach.transform.SetParent(_gameGeneralZone_GO.transform);
    }
}
