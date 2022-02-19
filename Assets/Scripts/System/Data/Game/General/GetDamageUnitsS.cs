using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    sealed class GetDamageUnitsS : SystemAbstract, IEcsRunSystem
    {
        internal GetDamageUnitsS(in Entities ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitTC(idx_0).HaveUnit)
                {
                    var standDamage = CellUnitDamage_Values.StandDamage(Es.UnitTC(idx_0).Unit, Es.UnitLevelTC(idx_0).Level);
                    float powerDamage = standDamage;

                    if (Es.UnitMainTWTC(idx_0).HaveToolWeapon) powerDamage += standDamage * CellUnitDamage_Values.ToolWeaponMainPercent(Es.UnitMainTWTC(idx_0).ToolWeapon, Es.UnitLevelTC(idx_0).Level);
                    if (Es.UnitExtraTWTC(idx_0).HaveToolWeapon) powerDamage += standDamage * CellUnitDamage_Values.ToolWeaponExtraPercent(Es.UnitExtraTWTC(idx_0).ToolWeapon);

                    Es.UnitEs(idx_0).DamageAttackC.Damage = powerDamage;


                    powerDamage += standDamage * CellUnitDamage_Values.ProtRelaxPercent(Es.UnitConditionTC(idx_0).Condition);
                    if (Es.BuildTC(idx_0).HaveBuilding) powerDamage += standDamage * CellBuilding_Values.ProtectionPercent(Es.BuildTC(idx_0).Build);

                    float protectionPercent = 0;

                    if (Es.FertilizeC(idx_0).HaveAny) protectionPercent += CellUnitDamage_Values.ProtectionPercent(EnvironmentTypes.Fertilizer);
                    if (Es.YoungForestC(idx_0).HaveAny) protectionPercent += CellUnitDamage_Values.ProtectionPercent(EnvironmentTypes.YoungForest);
                    if (Es.AdultForestC(idx_0).HaveAny) protectionPercent += CellUnitDamage_Values.ProtectionPercent(EnvironmentTypes.AdultForest);
                    if (Es.HillC(idx_0).HaveAny) protectionPercent += CellUnitDamage_Values.ProtectionPercent(EnvironmentTypes.Hill);
                    if (Es.MountainC(idx_0).HaveAny) protectionPercent += CellUnitDamage_Values.ProtectionPercent(EnvironmentTypes.Mountain);

                    powerDamage += standDamage * protectionPercent;

                    Es.UnitEs(idx_0).DamageOnCell.Damage = powerDamage;
                }

            }
        }
    }
}