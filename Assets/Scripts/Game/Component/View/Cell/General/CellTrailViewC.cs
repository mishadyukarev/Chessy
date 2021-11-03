using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public struct CellTrailViewC
    {
        private Transform _parent_Trans;
        private Dictionary<DirectTypes, SpriteRenderer> _trails_SRs;

        public CellTrailViewC(Transform cell_Trans)
        {
            _trails_SRs = new Dictionary<DirectTypes, SpriteRenderer>();

            _parent_Trans = cell_Trans.Find("TrailZone");

            for (var dir = (DirectTypes)1; dir < (DirectTypes)typeof(DirectTypes).GetEnumNames().Length; dir++)
            {
                _trails_SRs.Add(dir, _parent_Trans.Find(dir.ToString()).GetComponent<SpriteRenderer>());
            }
        }

        public void SetActive(DirectTypes dir, bool enabled)
        {
            if (!_trails_SRs.ContainsKey(dir)) throw new Exception();

            _trails_SRs[dir].enabled = enabled;
        }
        public void Rotate() => _parent_Trans.eulerAngles += new Vector3(0, 0, 180);
    }
}