using Chessy.Model;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.Entity
{
    public struct EnvironmentVE
    {
        public readonly SpriteRendererVC[] EnvironmentSRs;

        public readonly SpriteRendererVC HillUnderC;
        public readonly AnimationVC AnimationC;

        public SpriteRendererVC EnvironmentE(in EnvironmentTypes env) => EnvironmentSRs[(byte)env];

        public EnvironmentVE(in GameObject cell)
        {
            EnvironmentSRs = new SpriteRendererVC[(byte)EnvironmentTypes.End];

            var parent = cell.transform.Find("Environments");

            for (var envT = EnvironmentTypes.None + 1; envT < EnvironmentTypes.End; envT++)
            {
                EnvironmentSRs[(byte)envT] = new SpriteRendererVC(parent.Find(envT.ToString() + "_SR+").GetComponent<SpriteRenderer>());
            }

            HillUnderC = new SpriteRendererVC(parent.Find(EnvironmentTypes.Hill.ToString() + "Under" + "_SR+").GetComponent<SpriteRenderer>());


            AnimationC = new AnimationVC(EnvironmentSRs[(byte)EnvironmentTypes.AdultForest].GO.GetComponent<Animation>());

        }
    }
}