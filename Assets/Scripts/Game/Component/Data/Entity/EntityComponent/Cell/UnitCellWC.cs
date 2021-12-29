using System;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public struct UnitCellWC : IUnitCell
    {
        readonly byte _idx;


        public bool CanShift(in byte idx_to)
        {
            var idx_from = _idx;

            ref var stepUnit_from = ref Unit<StepUnitWC>(idx_from);

            if (!Unit<StunC>(idx_from).IsStunned && Unit<UnitC>(idx_from).Have)
            {
                CellSpaceC.TryGetIdxAround(idx_from, out var directs);

                foreach (var item_1 in directs)
                {
                    if (idx_to == item_1.Value && !Environment<EnvC>(idx_to).Have(EnvTypes.Mountain) || Unit<UnitC>(idx_to).Have)
                    {
                        var one = stepUnit_from.HaveStepsForDoing(idx_to);
                        var two = stepUnit_from.HaveMaxSteps;

                        if (one || two)
                        {
                            return true;
                        }
                        
                    }
                }
                return false;
            }
            else return false;
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


            if (!Unit<UnitC>(_idx).Is(UnitTypes.Pawn) || !Unit<ConditionC>(_idx).Is(CondUnitTypes.Relaxed)
                || !Unit<HpUnitWC>(_idx).HaveMax) return false;


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
            var twC = UnitTW<ToolWeaponC>(_idx);

            if (Build<BuildC>(_idx).Have || !Unit<ConditionC>(_idx).Is(CondUnitTypes.Relaxed) || !Unit<HpUnitWC>(_idx).HaveMax) return false;



            var ration = 0f;

            switch (Unit<UnitC>(_idx).Unit)
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


        internal UnitCellWC(in byte idx) => _idx = idx;


        public void SetNew(in UnitTypes unit, in LevelTypes level, in PlayerTypes owner)
        {
            if (unit == UnitTypes.None) throw new Exception();
            if (Unit<UnitC>(_idx).Have) throw new Exception("It's got unit");

            Unit<UnitC>(_idx).Unit = unit;
            Unit<LevelC>(_idx).Level = level;
            Unit<OwnerC>(_idx).Owner = owner;

            WhereUnitsC.Set(unit, level, owner, _idx, true);
        }
        public void Kill()
        {
            if (!Unit<UnitC>(_idx).Have) throw new Exception("It's not got unit");

            if (Unit<UnitC>(_idx).Is(UnitTypes.King))
            {
                PlyerWinnerC.PlayerWinner = Unit<OwnerC>(_idx).Owner;
            }
            else if (Unit<UnitC>(_idx).Is(new[] { UnitTypes.Scout, UnitTypes.Elfemale }))
            {
                ScoutHeroCooldownC.SetStandCooldown(Unit<UnitC>(_idx).Unit, Unit<OwnerC>(_idx).Owner);
                InvUnitsC.Add(Unit<UnitC>(_idx).Unit, Unit<LevelC>(_idx).Level, Unit<OwnerC>(_idx).Owner);
            }

            WhereUnitsC.Set(Unit<UnitC>(_idx).Unit, Unit<LevelC>(_idx).Level, Unit<OwnerC>(_idx).Owner, _idx, false);
            Unit<UnitC>(_idx).Reset();
        }
        public void Shift(in byte idx_to)
        {
            var idx_from = _idx;

            var dir = CellSpaceC.GetDirect(Cell<XyC>(idx_from).Xy, Cell<XyC>(idx_to).Xy);


            Trail<TrailC>(idx_to).TrySetNewTrail(dir.Invert(), Environment<EnvC>(idx_to));
            Trail<TrailC>(idx_from).TrySetNewTrail(dir, Environment<EnvC>(idx_from));

            ref var unit_from = ref Unit<UnitC>(idx_from);
            ref var level_from = ref Unit<LevelC>(idx_from);
            ref var own_from = ref Unit<OwnerC>(idx_from);


            Unit<OwnerC>(idx_to).Set(Unit<OwnerC>(idx_from));
            Unit<LevelC>(idx_to).Set(Unit<LevelC>(idx_from));


            Unit<HpC>(idx_to).Set(Unit<HpC>(idx_from));
            Unit<StepC>(idx_to).Set(Unit<StepC>(idx_from));
            if (Unit<ConditionC>(idx_to).HaveCondition) Unit<ConditionC>(idx_to).Reset();

            UnitTW<UnitTWCellC>(idx_to).Set(idx_from);
            UnitTW<LevelC>(idx_to).Set(UnitTW<LevelC>(idx_from));
            UnitTW<ProtectionC>(idx_to).Set(UnitTW<ProtectionC>(idx_from));

            Unit<EffectsC>(idx_to).Set(Unit<EffectsC>(idx_from));
            Unit<WaterC>(idx_to).Set(Unit<WaterC>(idx_from));
            Unit<MoveInCondC>(idx_to).ResetAll();
            Unit<CooldownUniqC >(idx_to).Replace(Unit<CooldownUniqC>(idx_from));
            Unit<CornerArcherC>(idx_to).Set(Unit<CornerArcherC>(idx_from));
            if (River<RiverC>(idx_to).HaveNearRiver) Unit<WaterUnitC>(idx_from).SetMaxWater();



            if (Build<BuildC>(idx_to).Is(BuildTypes.Camp))
            {
                if (!Build<OwnerC>(idx_to).Is(Unit<OwnerC>(idx_to).Owner))
                {
                    Build<BuildCellC>(idx_to).Remove();
                }
            }


            Unit<UnitC>(idx_to).Unit = Unit<UnitC>(idx_from).Unit;
            WhereUnitsC.Set(Unit<UnitC>(idx_to).Unit, level_from.Level, own_from.Owner, idx_to, true);


            WhereUnitsC.Set(Unit<UnitC>(idx_from).Unit, level_from.Level, own_from.Owner, idx_from, false);
            Unit<UnitC>(idx_from).Reset();

            //UnitCellC<UnitC>(idx_from).Clean(lev_from, own_from.Owner);
        }
        public void AddToInventor()
        {
            var level = Unit<LevelC>(_idx).Level;
            var owner = Unit<OwnerC>(_idx).Owner;

            InvUnitsC.Add(Unit<UnitC>(_idx).Unit, level, owner);

            WhereUnitsC.Set(Unit<UnitC>(_idx).Unit, level, owner, _idx, false);
            Unit<UnitC>(_idx).Reset();
        }
        public void Upgrade()
        {
            ref var levC = ref Unit<LevelC>(_idx);

            if (levC.Is(LevelTypes.First))
            {
                levC.Level = LevelTypes.Second;
            }
            else throw new Exception();
        }
        public void SetScout()
        {
            ref var ownUnitC = ref Unit<OwnerC>(_idx);

            ref var twUnitC = ref UnitTW<UnitTWCellC>(_idx);
            ref var twC = ref UnitTW<ToolWeaponC>(_idx);
            ref var levTWC = ref UnitTW<LevelC>(_idx);


            WhereUnitsC.Set(Unit<UnitC>(_idx).Unit, Unit<LevelC>(_idx).Level, ownUnitC.Owner, _idx, false);

            Unit<UnitC>(_idx).Unit = UnitTypes.Scout;
            Unit<LevelC>(_idx).Level = LevelTypes.First;
            if (twC.HaveTW)
            {
                InvTWC.Add(twC.ToolWeapon, levTWC.Level, ownUnitC.Owner);
                twUnitC.Reset();
            }

            WhereUnitsC.Set(UnitTypes.Scout, LevelTypes.First, Unit<OwnerC>(_idx).Owner, _idx, true);
        }
        public void SetHero(in byte idx_from, in UnitTypes unit, in LevelTypes lev)
        {
            var idx_to = _idx;

            WhereUnitsC.Set(UnitTypes.Archer, Unit<LevelC>(idx_from).Level, Unit<OwnerC>(idx_from).Owner, idx_from, false);
            Unit<UnitC>(idx_from).Reset();

            WhereUnitsC.Set(UnitTypes.Archer, Unit<LevelC>(idx_to).Level, Unit<OwnerC>(idx_to).Owner, idx_to, false);
            Unit<UnitC>(idx_to).Reset();


            Unit<UnitC>(idx_to).Unit = unit;
            Unit<LevelC>(idx_to).Level = lev;

            WhereUnitsC.Set(unit, lev, Unit<OwnerC>(idx_to).Owner, idx_to, true);


            InvUnitsC.Take(Unit<OwnerC>(idx_to).Owner, unit, lev);
        }
        public void Sync(in UnitTypes unit, in LevelTypes lev, in PlayerTypes owner)
        {
            Unit<UnitC>(_idx).Unit = unit;
            Unit<LevelC>(_idx).Level = lev;
            Unit<OwnerC>(_idx).Owner = owner;
        }
    }
}