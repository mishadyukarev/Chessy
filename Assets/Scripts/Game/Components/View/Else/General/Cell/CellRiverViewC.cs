using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public struct CellRiverViewC
    {
        private Dictionary<DirectTypes, SpriteRenderer> _sprites;

        public CellRiverViewC(Transform parentCell_Trans)
        {
            var riverZone = parentCell_Trans.Find("RiverZone");

            _sprites = new Dictionary<DirectTypes, SpriteRenderer>();

            _sprites.Add(DirectTypes.Up, riverZone.Find("1").GetComponent<SpriteRenderer>());
            _sprites.Add(DirectTypes.Right, riverZone.Find("2").GetComponent<SpriteRenderer>());
            _sprites.Add(DirectTypes.Down, riverZone.Find("3").GetComponent<SpriteRenderer>());
            _sprites.Add(DirectTypes.Left, riverZone.Find("4").GetComponent<SpriteRenderer>());
        }

        public void SetActiveRive(DirectTypes dirType, bool isActive) => _sprites[dirType].gameObject.SetActive(isActive);
    }
}