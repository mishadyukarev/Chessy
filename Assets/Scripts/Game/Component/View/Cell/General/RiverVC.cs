using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct RiverVC
    {
        private Transform _parent_Trans;
        private Dictionary<DirectTypes, SpriteRenderer> _sprites;

        public RiverVC(Transform parentCell_Trans)
        {
            _parent_Trans = parentCell_Trans.Find("RiverZone");

            _sprites = new Dictionary<DirectTypes, SpriteRenderer>();

            _sprites.Add(DirectTypes.Up, _parent_Trans.Find("1").GetComponent<SpriteRenderer>());
            _sprites.Add(DirectTypes.Right, _parent_Trans.Find("2").GetComponent<SpriteRenderer>());
            _sprites.Add(DirectTypes.Down, _parent_Trans.Find("3").GetComponent<SpriteRenderer>());
            _sprites.Add(DirectTypes.Left, _parent_Trans.Find("4").GetComponent<SpriteRenderer>());
        }

        public void SetActiveRive(DirectTypes dirType, bool enabled) => _sprites[dirType].enabled = enabled;
        public void Rotate(PlayerTypes player)
        {
            switch (player)
            {
                case PlayerTypes.None: throw new Exception();
                case PlayerTypes.First:
                    _parent_Trans.localEulerAngles = new Vector3(0, 0, 0);
                    break;
                case PlayerTypes.Second:
                    _parent_Trans.localEulerAngles = new Vector3(0, 0, 180);
                    break;
                default: throw new Exception();
            }

        }
    }
}