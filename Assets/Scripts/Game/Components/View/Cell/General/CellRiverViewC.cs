﻿using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public struct CellRiverViewC
    {
        private Transform _parent_Trans;
        private Dictionary<DirectTypes, SpriteRenderer> _sprites;

        public CellRiverViewC(Transform parentCell_Trans)
        {
            _parent_Trans = parentCell_Trans.Find("RiverZone");

            _sprites = new Dictionary<DirectTypes, SpriteRenderer>();

            _sprites.Add(DirectTypes.Up, _parent_Trans.Find("1").GetComponent<SpriteRenderer>());
            _sprites.Add(DirectTypes.Right, _parent_Trans.Find("2").GetComponent<SpriteRenderer>());
            _sprites.Add(DirectTypes.Down, _parent_Trans.Find("3").GetComponent<SpriteRenderer>());
            _sprites.Add(DirectTypes.Left, _parent_Trans.Find("4").GetComponent<SpriteRenderer>());
        }

        public void SetActiveRive(DirectTypes dirType, bool enabled) => _sprites[dirType].enabled = enabled;
        public void Rotate() => _parent_Trans.eulerAngles += new Vector3(0, 0, 180);
    }
}