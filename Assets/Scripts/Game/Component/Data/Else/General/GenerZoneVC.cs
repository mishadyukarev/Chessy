using UnityEngine;

namespace Chessy.Game
{
    public struct GenerZoneVC
    {
        private static GameObject _gameGeneralZone_GO;

        public GenerZoneVC(GameObject gameGeneralZone_GO) => _gameGeneralZone_GO = gameGeneralZone_GO;

        public static void Attach(Transform transForAttach) => transForAttach.transform.SetParent(_gameGeneralZone_GO.transform);
    }
}
