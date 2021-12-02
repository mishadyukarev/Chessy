using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct UnitC : IUnitCell
    {
        UnitTypes _unit;
        readonly byte _idx;

        public UnitTypes Unit => _unit;
        public bool Have => Unit != UnitTypes.None;
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
        public int CostFood
        {
            get
            {
                if (Have)
                {
                    if (!Is(UnitTypes.King)) return 10;
                    return 0;
                }

                return 0;
            }
        }
        public bool CanExtract(out int extract, out EnvTypes env, out ResTypes res)
        {
            extract = 0;
            env = EnvTypes.None;
            res = ResTypes.None;


            if (Environment<EnvC>(_idx).Have(EnvTypes.AdultForest))
            {
                env = EnvTypes.AdultForest;
                res = ResTypes.Wood;
            }
            else return false;


            if (!Is(UnitTypes.Pawn) || !Unit<ConditionC>(_idx).Is(CondUnitTypes.Relaxed)
                || !Unit<HpC>(_idx).HaveMax) return false;


            var ration = 0f;

            switch (Unit<LevelC>(_idx).Level)
            {
                case LevelTypes.First: ration = 0.1f; break;
                case LevelTypes.Second: ration = 0.2f; break;
                default: throw new Exception();
            }


            var envResC = Environment<EnvResC>(_idx);

            extract = (int)(envResC.Max(env) * ration);

            if (extract > envResC.Amount(env)) extract = envResC.Amount(env);

            return true;
        }
        public bool CanResume(out int resume, out EnvTypes env)
        {
            resume = 0;
            env = EnvTypes.None;


            var envC = Environment<EnvC>(_idx);
            var envResC = Environment<EnvResC>(_idx);
            var twC = ToolWeapon<ToolWeaponC>(_idx);

            if (Build<BuildC>(_idx).Have || !Unit<ConditionC>(_idx).Is(CondUnitTypes.Relaxed) || !Unit<HpC>(_idx).HaveMax) return false;



            var ration = 0f;

            switch (_unit)
            {
                case UnitTypes.Pawn:
                    if (!envC.Have(EnvTypes.Hill) && !twC.Is(TWTypes.Pick)) return false;

                    env = EnvTypes.Hill;

                    switch (Unit<LevelC>(_idx).Level)
                    {
                        case LevelTypes.First: ration = 0.3f; break;
                        case LevelTypes.Second: ration = 0.6f; break;
                        default: throw new Exception();
                    }
                    break;

                case UnitTypes.Elfemale:
                    ration = 0.3f;
                    env = EnvTypes.AdultForest;
                    break;

                default: return false;
            }



            resume = (int)(envResC.Max(env) * ration);

            if (resume > envResC.Amount(env)) resume = envResC.Amount(env);

            return true;
        }
    


        public UnitC(UnitTypes unit, byte idx)
        {
            _unit = unit;
            _idx = idx;
        }



        public void SetNew(UnitTypes unit, LevelTypes level, PlayerTypes owner)
        {
            if (unit == UnitTypes.None) throw new Exception();
            if (Have) throw new Exception("It's got unit");
 
            _unit = unit;
            WhereUnitsC.Set(unit, level, owner, _idx, true);
        }

        public void Kill(LevelTypes level, PlayerTypes owner)
        {
            if (!Have) throw new Exception("It's not got unit");

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
            if (!Have) throw new Exception("It's not got unit");

            WhereUnitsC.Set(_unit, level, owner, _idx, false);
            _unit = UnitTypes.None;
        }

        public void Shift(in byte idx_from, in DirectTypes dir_from)
        {
            var idx_to = _idx;

            Trail<TrailC>(idx_to).TrySetNewTrail(dir_from.Invert(), Environment<EnvC>(idx_to));
            Trail<TrailC>(idx_from).TrySetNewTrail(dir_from, Environment<EnvC>(idx_from));

            ref var unit_from = ref Unit<UnitC>(idx_from);
            ref var level_from = ref Unit<LevelC>(idx_from);
            ref var own_from = ref Unit<OwnerC>(idx_from);
            

            Unit<OwnerC>(idx_to).Set(Unit<OwnerC>(idx_from));
            Unit<LevelC>(idx_to).Set(Unit<LevelC>(idx_from));


            Unit<HpC>(idx_to).Set(Unit<HpC>(idx_from));
            Unit<StepC>(idx_to).Set(Unit<StepC>(idx_from));
            if (Unit<ConditionC>(idx_to).HaveCondition) Unit<ConditionC>(idx_to).Reset();

            ToolWeapon<ToolWeaponC>(idx_to).Set(ToolWeapon<ToolWeaponC>(idx_from));
            ToolWeapon<LevelC>(idx_to).Set(ToolWeapon<LevelC>(idx_from));
            ToolWeapon<ShieldC>(idx_to).Set(ToolWeapon<ShieldC>(idx_from));

            Unit<UnitEffectsC>(idx_to).Set(Unit<UnitEffectsC>(idx_from));
            Unit<WaterC>(idx_to).Set(Unit<WaterC>(idx_from));
            Unit<MoveInCondC>(idx_to).ResetAll();
            Unit<CooldownUniqC>(idx_to).Replace(Unit<CooldownUniqC>(idx_from));
            Unit<CornerArcherC>(idx_to).Set(Unit<CornerArcherC>(idx_from));
            if (River<RiverC>(idx_to).HaveNearRiver) Unit<WaterC>(idx_to).SetMaxWater(UnitUpgC.UpgPercent(UnitStatTypes.Water, unit_from.Unit, level_from.Level, own_from.Owner));



            if (Build<BuildC>(idx_to).Is(BuildTypes.Camp))
            {
                if (!Build<OwnerC>(idx_to).Is(Unit<OwnerC>(idx_to).Owner))
                {
                    Build<BuildC>(idx_to).Remove();
                }
            }


            _unit = Unit<UnitC>(idx_from).Unit;
            WhereUnitsC.Set(_unit, level_from.Level, own_from.Owner, _idx, true);


            WhereUnitsC.Set(_unit, level_from.Level, own_from.Owner, idx_from, false);
            Unit<UnitC>(idx_from)._unit = UnitTypes.None;

            //UnitCellC<UnitC>(idx_from).Clean(lev_from, own_from.Owner);
        }

        public void AddToInventor()
        {
            var level = Unit<LevelC>(_idx).Level;
            var owner = Unit<OwnerC>(_idx).Owner;

            InvUnitsC.Add(_unit, level, owner);

            WhereUnitsC.Set(_unit, level, owner, _idx, false);
            _unit = UnitTypes.None;
        }

        public void CreateScout()
        {
            var level = Unit<LevelC>(_idx).Level;
            var owner = Unit<OwnerC>(_idx).Owner;

            InvUnitsC.Take(owner, UnitTypes.Scout, LevelTypes.First);

            WhereUnitsC.Set(_unit, level, owner, _idx, false);
            _unit = UnitTypes.None;
        }

        public void SetFromToUnit(UnitTypes unit, in byte idx_from)
        {
            var idx_to = _idx;

            ref var unit_from = ref Unit<UnitC>(idx_from);
            ref var level_from = ref Unit<LevelC>(idx_from);
            ref var own_from = ref Unit<OwnerC>(idx_from);

            WhereUnitsC.Set(UnitTypes.Archer, level_from.Level, own_from.Owner, idx_from, false);
            unit_from._unit = UnitTypes.None;


            ref var unit_to = ref Unit<UnitC>(idx_to);
            ref var level_to = ref Unit<LevelC>(idx_to);
            ref var own_to = ref Unit<OwnerC>(idx_to);

            WhereUnitsC.Set(UnitTypes.Archer, level_to.Level, own_to.Owner, idx_to, false);
            _unit = UnitTypes.None;


            _unit = unit;
            level_to.Set(LevelTypes.First);

            WhereUnitsC.Set(unit, level_to.Level, own_to.Owner, idx_to, true);


            InvUnitsC.Take(own_to.Owner, unit_to.Unit, level_to.Level);
        }

        public int ExtractFood()
        {
            switch (Unit<LevelC>(_idx).Level)
            {
                case LevelTypes.None: throw new Exception();

                case LevelTypes.First:
                    switch (Unit)
                    {
                        case UnitTypes.None: throw new Exception();
                        case UnitTypes.King: throw new Exception();
                        case UnitTypes.Pawn: return 10;
                        case UnitTypes.Archer: throw new Exception();
                        case UnitTypes.Scout: throw new Exception();
                        case UnitTypes.Elfemale: throw new Exception();
                        case UnitTypes.End: throw new Exception();
                        default: throw new Exception();
                    }
                case LevelTypes.Second:
                    switch (Unit)
                    {
                        case UnitTypes.None: throw new Exception();
                        case UnitTypes.King: throw new Exception();
                        case UnitTypes.Pawn: return 20;
                        case UnitTypes.Archer: throw new Exception();
                        case UnitTypes.Scout: throw new Exception();
                        case UnitTypes.Elfemale: throw new Exception();
                        case UnitTypes.End: throw new Exception();
                        default: throw new Exception();
                    }
                case LevelTypes.End: throw new Exception();
                default: throw new Exception();
            }
        }

        public void Sync(UnitTypes unit)
        {
            _unit = unit;
        }
    }
}
