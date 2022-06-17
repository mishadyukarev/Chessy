using Chessy.Game.Model.Entity;
using Chessy.Game.Model.Entity.Cell.Unit;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems
    {
        readonly EntitiesModelGame _eMG;

        readonly UnitSimpleSystems[] _unitSimpleSs = new UnitSimpleSystems[StartValues.CELLS];
        internal readonly UnitAbilitiesSystems UnitAbilitiesSs;

        internal UnitSimpleSystems UnitSimpleS(in byte idxCell) => _unitSimpleSs[idxCell];

        internal void SetMain(in byte cell, in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in ConditionUnitTypes conditionT, in bool isRight) => _unitSimpleSs[cell].SetMainS.Set(unitT, levelT, playerT, conditionT, isRight);
        internal void SetMainToolWeapon(in byte cell, in ToolWeaponTypes twT, in LevelTypes levelT) => _unitSimpleSs[cell].MainTWS.Set(twT, levelT);
        internal void SetExtraToolWeapon(in byte cell, in ToolWeaponTypes twT, in LevelTypes levelT, in float protection) => _unitSimpleSs[cell].ExtraTWS.Set(twT, levelT, protection);
        internal void SetEffects(in byte cell, in float stun, in float protection, in int shoots, in bool haveKingEffect) => _unitSimpleSs[cell].SetEffectsS.Set(stun, protection, shoots, haveKingEffect);
        internal void SetStats(in byte cell, in double hp, in double steps, in double water) => _unitSimpleSs[cell].SetStatsS.Set(hp, steps, water);
        internal void ClearUnit(in byte cell) => _unitSimpleSs[cell].ClearUnitS.Clear();


        internal UnitSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            _eMG = eMG;

            UnitAbilitiesSs = new UnitAbilitiesSystems(sMG, eMG);

            for (byte startCell = 0; startCell < StartValues.CELLS; startCell++)
            {
                _unitSimpleSs[startCell] = new UnitSimpleSystems(eMG.UnitEs(startCell));
            }
        }



        internal void CopyExtraTW(in byte cell_from, in byte cell_to)
        {
            _eMG.ExtraToolWeaponTC(cell_to) = _eMG.ExtraToolWeaponTC(cell_from);
            _eMG.ExtraTWLevelTC(cell_to) = _eMG.ExtraTWLevelTC(cell_from);
            _eMG.ExtraTWProtectionC(cell_to) = _eMG.ExtraTWProtectionC(cell_from);
        }
        internal void CopyMainTW(in byte cell_from, in byte cell_to)
        {
            _eMG.MainToolWeaponTC(cell_to) = _eMG.MainToolWeaponTC(cell_from);
            _eMG.MainTWLevelTC(cell_to) = _eMG.MainTWLevelTC(cell_from);
        }
        internal void CopyEffects(in byte cell_from, in byte cell_to)
        {
            _eMG.StunUnitC(cell_to) = _eMG.StunUnitC(cell_from);
            _eMG.ShieldUnitEffectC(cell_to) = _eMG.ShieldUnitEffectC(cell_from);
            _eMG.FrozenArrawEffectC(cell_to) = _eMG.FrozenArrawEffectC(cell_from);
            _eMG.HaveKingEffect(cell_to) = _eMG.HaveKingEffect(cell_from);
        }
        internal void Set(in byte cell_from, in byte cell_to)
        {
            _eMG.HpUnitC(cell_to) = _eMG.HpUnitC(cell_from);
            _eMG.StepUnitC(cell_to) = _eMG.StepUnitC(cell_from);
            _eMG.WaterUnitC(cell_to) = _eMG.WaterUnitC(cell_from);
        }
    }

    sealed partial class UnitAbilitiesSystems : SystemModel
    {
        internal UnitAbilitiesSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }
    }

    sealed class UnitSimpleSystems
    {
        internal readonly MainToolWeaponUnitWorkS MainTWS;
        internal readonly ClearUnitS ClearUnitS;
        internal readonly MainUnitS SetMainS;
        internal readonly ExtraToolWeaponForUnitS ExtraTWS;
        internal readonly EffectsUnitS SetEffectsS;
        internal readonly StatsUnitWorkerS SetStatsS;

        internal UnitSimpleSystems(in UnitEs unitE)
        {
            MainTWS = new MainToolWeaponUnitWorkS(unitE.MainToolWeaponE);
            ClearUnitS = new ClearUnitS(unitE);
            SetMainS = new MainUnitS(unitE.MainE);
            ExtraTWS = new ExtraToolWeaponForUnitS(unitE.ExtraToolWeaponE);
            SetEffectsS = new EffectsUnitS(unitE.EffectsE);
            SetStatsS = new StatsUnitWorkerS(unitE.StatsE);
        }
    }
}