namespace Game.Game
{
    sealed class ScoutOldNewSys : SystemCellAbstract, IEcsRunSystem
    {
        public ScoutOldNewSys(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = Es.MasterEs.ScoutOldNew<IdxC>().Idx;
            var unit = Es.MasterEs.ScoutOldNew<UnitTC>().Unit;

            if (UnitEs.StatEs.Hp(idx_0).HaveMax)
            {
                if (UnitEs.StatEs.Step(idx_0).HaveMax(Es.CellEs.UnitEs.Main(idx_0)))
                {
                    Es.InventorUnitsEs.Units(UnitTypes.Scout, LevelTypes.First, Es.CellEs.UnitEs.Main(idx_0).OwnerC.Player).Units.Amount -= 1;

                    ref var ownUnitC = ref UnitEs.Main(idx_0).OwnerC;

                    ref var twC = ref UnitEs.ToolWeapon(idx_0).ToolWeapon;
                    ref var levTWC = ref UnitEs.ToolWeapon(idx_0).LevelTW;


                    Es.WhereUnitsEs.WhereUnit(UnitEs.Main(idx_0).UnitC.Unit, UnitEs.Main(idx_0).LevelC.Level, ownUnitC.Player, idx_0).HaveUnit.Have = false;

                    UnitEs.Main(idx_0).UnitC.Unit = UnitTypes.Scout;
                    UnitEs.Main(idx_0).LevelC.Level = LevelTypes.First;
                    if (twC.HaveTW)
                    {
                        Es.InventorToolWeaponEs.ToolWeapons(twC.ToolWeapon, levTWC.Level, ownUnitC.Player).ToolWeapons.Amount += 1;
                        UnitEs.ToolWeapon(idx_0).Reset();
                    }

                    Es.WhereUnitsEs.WhereUnit(UnitTypes.Scout, LevelTypes.First, UnitEs.Main(idx_0).OwnerC.Player, idx_0).HaveUnit.Have = true;


                    Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}