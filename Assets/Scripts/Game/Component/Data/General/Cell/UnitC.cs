using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct UnitC : IUnitCell
    {
        UnitTypes _unit;
        readonly byte _idx;

        public UnitTypes Unit => _unit;
        public bool HaveUnit => Unit != UnitTypes.None;
        public bool IsMelee
        {
            get
            {
                switch (Unit)
                {
                    case UnitTypes.None: throw new Exception();
                    case UnitTypes.King: return true;
                    case UnitTypes.Pawn: return true;
                    case UnitTypes.Archer: return false;
                    case UnitTypes.Scout: return true;
                    case UnitTypes.Elfemale: return false;
                    default: throw new Exception();
                }
            }
        }
        public bool Is(params UnitTypes[] units)
        {
            foreach (var unit in units)
                if (unit == _unit) return true;
            return false;
        }
        public bool IsHero
        {
            get
            {
                switch (_unit)
                {
                    case UnitTypes.None: throw new Exception();
                    case UnitTypes.King: return false;
                    case UnitTypes.Pawn: return false;
                    case UnitTypes.Archer: return false;
                    case UnitTypes.Scout: return false;
                    case UnitTypes.Elfemale: return true;
                    default: throw new Exception();
                }
            }
        }



        public UnitC(UnitTypes unit, byte idx)
        {
            _unit = unit;
            _idx = idx;
        }



        public void SetNew(UnitTypes unit, LevelTypes level, PlayerTypes owner)
        {
            if (unit == UnitTypes.None) throw new Exception();
            if (HaveUnit) throw new Exception("It's got unit");
 
            _unit = unit;
            WhereUnitsC.Set(unit, level, owner, _idx, true);
        }

        public void Kill(LevelTypes level, PlayerTypes owner)
        {
            if (!HaveUnit) throw new Exception("It's not got unit");

            if (Is(UnitTypes.King))
            {
                PlyerWinnerC.PlayerWinner = owner;
            }
            else if (Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
            {
                ScoutHeroCooldownC.SetStandCooldown(_unit, owner);
                InvUnitsC.Add(_unit, level, owner);
            }

            WhereUnitsC.Set(_unit, level, owner, _idx, false);
            _unit = UnitTypes.None;
        }

        public void Clean(in LevelTypes level, in PlayerTypes owner)
        {
            if (!HaveUnit) throw new Exception("It's not got unit");

            WhereUnitsC.Set(_unit, level, owner, _idx, false);
            _unit = UnitTypes.None;
        }

        public void Shift(in byte idx_from, in DirectTypes dir_from)
        {
            var idx_to = _idx;

            TrailCellC<TrailC>(idx_to).TrySetNewTrail(dir_from.Invert(), EnvCellC<EnvC>(idx_to));
            TrailCellC<TrailC>(idx_from).TrySetNewTrail(dir_from, EnvCellC<EnvC>(idx_from));

            ref var unit_from = ref UnitCellC<UnitC>(idx_from);
            ref var level_from = ref UnitCellC<LevelC>(idx_from);
            ref var own_from = ref UnitCellC<OwnerC>(idx_from);
            

            UnitCellC<OwnerC>(idx_to).Set(UnitCellC<OwnerC>(idx_from));
            UnitCellC<LevelC>(idx_to).Set(UnitCellC<LevelC>(idx_from));


            UnitCellC<HpC>(idx_to).Set(UnitCellC<HpC>(idx_from));
            UnitCellC<StepC>(idx_to).Set(UnitCellC<StepC>(idx_from));
            if (UnitCellC<ConditionC>(idx_to).HaveCondition) UnitCellC<ConditionC>(idx_to).Reset();

            TWCellC<ToolWeaponC>(idx_to).Set(TWCellC<ToolWeaponC>(idx_from));
            TWCellC<LevelC>(idx_to).Set(TWCellC<LevelC>(idx_from));
            TWCellC<ShieldC>(idx_to).Set(TWCellC<ShieldC>(idx_from));

            UnitCellC<UnitEffectsC>(idx_to).Set(UnitCellC<UnitEffectsC>(idx_from));
            UnitCellC<WaterC>(idx_to).Set(UnitCellC<WaterC>(idx_from));
            UnitCellC<MoveInCondC>(idx_to).ResetAll();
            UnitCellC<CooldownUniqC>(idx_to).Replace(UnitCellC<CooldownUniqC>(idx_from));
            UnitCellC<CornerArcherC>(idx_to).Set(UnitCellC<CornerArcherC>(idx_from));
            if (RiverCellC<RiverC>(idx_to).HaveNearRiver) UnitCellC<WaterC>(idx_to).SetMaxWater(UnitUpgC.UpgPercent(UnitStatTypes.Water, unit_from.Unit, level_from.Level, own_from.Owner));



            if (BuildCellC<BuildC>(idx_to).Is(BuildTypes.Camp))
            {
                if (!BuildCellC<OwnerC>(idx_to).Is(UnitCellC<OwnerC>(idx_to).Owner))
                {
                    BuildCellC<BuildC>(idx_to).Remove(BuildCellC<OwnerC>(idx_to).Owner);
                }
            }


            _unit = UnitCellC<UnitC>(idx_from).Unit;
            WhereUnitsC.Set(_unit, level_from.Level, own_from.Owner, _idx, true);


            WhereUnitsC.Set(_unit, level_from.Level, own_from.Owner, idx_from, false);
            UnitCellC<UnitC>(idx_from)._unit = UnitTypes.None;

            //UnitCellC<UnitC>(idx_from).Clean(lev_from, own_from.Owner);
        }

        public void AddToInventor()
        {
            var level = UnitCellC<LevelC>(_idx).Level;
            var owner = UnitCellC<OwnerC>(_idx).Owner;

            InvUnitsC.Add(_unit, level, owner);

            WhereUnitsC.Set(_unit, level, owner, _idx, false);
            _unit = UnitTypes.None;
        }

        public void CreateScout()
        {
            var level = UnitCellC<LevelC>(_idx).Level;
            var owner = UnitCellC<OwnerC>(_idx).Owner;

            InvUnitsC.Take(owner, UnitTypes.Scout, LevelTypes.First);

            WhereUnitsC.Set(_unit, level, owner, _idx, false);
            _unit = UnitTypes.None;
        }

        public void SetFromToUnit(UnitTypes unit, in byte idx_from)
        {
            var idx_to = _idx;

            ref var unit_from = ref UnitCellC<UnitC>(idx_from);
            ref var level_from = ref UnitCellC<LevelC>(idx_from);
            ref var own_from = ref UnitCellC<OwnerC>(idx_from);

            WhereUnitsC.Set(UnitTypes.Archer, level_from.Level, own_from.Owner, idx_from, false);
            unit_from._unit = UnitTypes.None;


            ref var unit_to = ref UnitCellC<UnitC>(idx_to);
            ref var level_to = ref UnitCellC<LevelC>(idx_to);
            ref var own_to = ref UnitCellC<OwnerC>(idx_to);

            WhereUnitsC.Set(UnitTypes.Archer, level_to.Level, own_to.Owner, idx_to, false);
            _unit = UnitTypes.None;


            _unit = unit;
            level_to.Set(LevelTypes.First);

            WhereUnitsC.Set(unit, level_to.Level, own_to.Owner, idx_to, true);


            InvUnitsC.Take(own_to.Owner, unit_to.Unit, level_to.Level);
        }

        public void Sync(UnitTypes unit)
        {
            _unit = unit;
        }
    }
}
