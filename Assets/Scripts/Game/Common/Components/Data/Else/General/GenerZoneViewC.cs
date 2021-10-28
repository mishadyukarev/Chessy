using UnityEngine;

namespace Scripts.Game
{
    public struct GenerZoneViewC
    {
        private static GameObject _gameGeneralZone_GO;

        public GenerZoneViewC(GameObject gameGeneralZone_GO) => _gameGeneralZone_GO = gameGeneralZone_GO;

        public static void Attach(Transform transForAttach) => transForAttach.transform.SetParent(_gameGeneralZone_GO.transform);
    }
}
