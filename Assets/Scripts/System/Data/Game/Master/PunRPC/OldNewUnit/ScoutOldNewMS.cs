namespace Game.Game
{
    sealed class ScoutOldNewMS : SystemAbstract, IEcsRunSystem
    {
        public ScoutOldNewMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var idx_0 = Es.MasterEs.ScoutOldNew<IdxC>().Idx;
            var unit = Es.MasterEs.ScoutOldNew<UnitTC>().Unit;

            if (UnitEs.StatEs.Hp(idx_0).HaveMax)
            {
                if (UnitEs.StatEs.Step(idx_0).HaveMax(UnitEs.Main(idx_0)))
                {
                    Es.InventorUnitsEs.Units(UnitTypes.Scout, LevelTypes.First, UnitEs.Main(idx_0).OwnerC.Player).Units.Amount--;

                    var ownerUnit = UnitEs.Main(idx_0).OwnerC.Player;
                    var twC = UnitEs.ToolWeapon(idx_0).ToolWeaponTC;
                    var levTWC = UnitEs.ToolWeapon(idx_0).LevelTC;

                    if (twC.HaveTW)
                    {
                        Es.InventorToolWeaponEs.ToolWeapons(twC.ToolWeapon, levTWC.Level, ownerUnit).ToolWeapons.Amount += 1;
                        UnitEs.ToolWeapon(idx_0).Reset();
                    }
                    UnitEs.Main(idx_0).Clear(Es.WhereUnitsEs);

                    UnitEs.Main(idx_0).SetNew((UnitTypes.Scout, LevelTypes.First, ownerUnit, ConditionUnitTypes.None, false), Es);

                    Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}