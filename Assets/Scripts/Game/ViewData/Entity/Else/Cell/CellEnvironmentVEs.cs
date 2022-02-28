using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct CellEnvironmentVEs
    {
        readonly Dictionary<EnvironmentTypes, CellEnvironmentVE> _envs;

        public CellEnvironmentVE EnvironmentE(in EnvironmentTypes env) => _envs[env];

        public CellEnvironmentVEs(in GameObject cell, in byte idx)
        {
            _envs = new Dictionary<EnvironmentTypes, CellEnvironmentVE>();

            var parent = cell.transform.Find("Environments");

            for (var envT = EnvironmentTypes.None + 1; envT < EnvironmentTypes.End; envT++)
            {
                _envs.Add(envT, new CellEnvironmentVE(parent.Find(envT.ToString() + "_SR").GetComponent<SpriteRenderer>(), envT, idx));
            }
        }
    }
}