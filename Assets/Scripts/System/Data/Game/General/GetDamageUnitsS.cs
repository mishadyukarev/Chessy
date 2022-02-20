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
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                if (E.UnitTC(idx_0).HaveUnit)
                {
                    var standDamage = CellUnitDamage_Values.StandDamage(E.UnitTC(idx_0).Unit, E.UnitLevelTC(idx_0).Level);
                    float powerDamage = standDamage;

                    if (E.UnitMainTWTC(idx_0).HaveToolWeapon)
                    {
                        powerDamage += standDamage * CellUnitDamage_Values.ToolWeaponMainPercent(E.UnitMainTWTC(idx_0).ToolWeapon, E.UnitMainTWLevelTC(idx_0).Level);
                    }
                    if (E.UnitExtraTWTC(idx_0).HaveToolWeapon) powerDamage += standDamage * CellUnitDamage_Values.ToolWeaponExtraPercent(E.UnitExtraTWTC(idx_0).ToolWeapon);

                    E.UnitDamageAttackC(idx_0).Damage = powerDamage;


                    powerDamage += standDamage * CellUnitDamage_Values.ProtRelaxPercent(E.UnitConditionTC(idx_0).Condition);
                    if (E.BuildTC(idx_0).HaveBuilding) powerDamage += standDamage * CellBuilding_Values.ProtectionPercent(E.BuildTC(idx_0).Build);

                    float protectionPercent = 0;

                    if (E.FertilizeC(idx_0).HaveAny) protectionPercent += CellUnitDamage_Values.ProtectionPercent(EnvironmentTypes.Fertilizer);
                    if (E.YoungForestC(idx_0).HaveAny) protectionPercent += CellUnitDamage_Values.ProtectionPercent(EnvironmentTypes.YoungForest);
                    if (E.AdultForestC(idx_0).HaveAny) protectionPercent += CellUnitDamage_Values.ProtectionPercent(EnvironmentTypes.AdultForest);
                    if (E.HillC(idx_0).HaveAny) protectionPercent += CellUnitDamage_Values.ProtectionPercent(EnvironmentTypes.Hill);
                    if (E.MountainC(idx_0).HaveAny) protectionPercent += CellUnitDamage_Values.ProtectionPercent(EnvironmentTypes.Mountain);

                    powerDamage += standDamage * protectionPercent;

                    E.UnitDamageOnCellC(idx_0).Damage = powerDamage;
                }

            }
        }
    }
}