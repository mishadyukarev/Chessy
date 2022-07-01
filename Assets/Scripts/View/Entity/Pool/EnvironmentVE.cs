using Chessy.Model;
using Chessy.View.Component;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.View.Entity
{
    public struct EnvironmentVE
    {
        readonly Dictionary<EnvironmentTypes, SpriteRendererVC> _envs;

        public readonly SpriteRendererVC HillUnderC;
        public readonly AnimationVC AnimationC;

        public SpriteRendererVC EnvironmentE(in EnvironmentTypes env) => _envs[env];

        public EnvironmentVE(in GameObject cell)
        {
            _envs = new Dictionary<EnvironmentTypes, SpriteRendererVC>();

            var parent = cell.transform.Find("Environments");

            for (var envT = EnvironmentTypes.None + 1; envT < EnvironmentTypes.End; envT++)
            {
                _envs.Add(envT, new SpriteRendererVC(parent.Find(envT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
            }

            HillUnderC = new SpriteRendererVC(parent.Find(EnvironmentTypes.Hill.ToString() + "Under" + "_SR+").GetComponent<SpriteRenderer>());


            AnimationC = new AnimationVC(_envs[EnvironmentTypes.AdultForest].GO.GetComponent<Animation>());

        }
    }
}