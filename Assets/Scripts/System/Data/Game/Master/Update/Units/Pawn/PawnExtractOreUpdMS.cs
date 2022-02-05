using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    sealed class PawnExtractOreUpdMS : SystemAbstract, IEcsRunSystem
    {
        internal PawnExtractOreUpdMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitMainE(idx_0).Is(UnitTypes.Pawn) && Es.UnitConditionE(idx_0).ConditionTC.Is(ConditionUnitTypes.Relaxed))
                {
                    if (Es.UnitTWE(idx_0).ToolWeaponTC.Is(ToolWeaponTypes.Pick))
                    {
                        if (Es.EnvHillE(idx_0).HaveEnvironment && !Es.EnvAdultForestE(idx_0).HaveEnvironment)
                        {
                            Es.EnvHillE(idx_0).ExtractPawn();
                        }
                    }
                }
            }
        }
    }
}