using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.Entity.View.Cell
{
    public struct EnvironmentVEs
    {
        readonly Dictionary<EnvironmentTypes, EnvironmentVE> _envs;

        public EnvironmentVE EnvironmentE(in EnvironmentTypes env) => _envs[env];

        public EnvironmentVEs(in GameObject cell, in byte idx)
        {
            _envs = new Dictionary<EnvironmentTypes, EnvironmentVE>();

            var parent = cell.transform.Find("Environments");

            for (var envT = EnvironmentTypes.None + 1; envT < EnvironmentTypes.End; envT++)
            {
                _envs.Add(envT, new EnvironmentVE(parent.Find(envT.ToString() + "_SR").GetComponent<SpriteRenderer>()));
            }
        }
    }
}