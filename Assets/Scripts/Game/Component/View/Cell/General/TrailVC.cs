using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct TrailVC
    {
        private Transform _parent_Trans;
        private Dictionary<DirectTypes, SpriteRenderer> _trails_SRs;

        public TrailVC(Transform cell_Trans)
        {
            _trails_SRs = new Dictionary<DirectTypes, SpriteRenderer>();

            _parent_Trans = cell_Trans.Find("TrailZone");

            for (var dir = DirectTypes.First; dir < DirectTypes.End; dir++)
            {
                _trails_SRs.Add(dir, _parent_Trans.Find(dir.ToString()).GetComponent<SpriteRenderer>());
            }
        }

        public void SetActive(DirectTypes dir, bool enabled)
        {
            if (!_trails_SRs.ContainsKey(dir)) throw new Exception();

            _trails_SRs[dir].enabled = enabled;
        }
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