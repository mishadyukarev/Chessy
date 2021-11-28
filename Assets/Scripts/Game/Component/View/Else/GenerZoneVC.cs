using UnityEngine;

namespace Game.Game
{
    public struct GenerZoneVC
    {
        private static GameObject _genZone;

        public GenerZoneVC(GameObject genZone) => _genZone = genZone;

        public static void Attach(Transform transForAttach) => transForAttach.transform.SetParent(_genZone.transform);
    }
}
