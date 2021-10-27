using UnityEngine;

namespace Scripts.Game
{
    internal struct GenerZoneViewC
    {
        private static GameObject _gameGeneralZone_GO;

        internal GenerZoneViewC(GameObject gameGeneralZone_GO) => _gameGeneralZone_GO = gameGeneralZone_GO;

        internal static void Attach(Transform transForAttach) => transForAttach.transform.SetParent(_gameGeneralZone_GO.transform);
    }
}
