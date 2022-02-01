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

            if (UnitStatEs(idx_0).Hp.HaveMax)
            {
                if (UnitStatEs(idx_0).StepE.HaveMax(UnitEs(idx_0).MainE))
                {
                    Es.InventorUnitsEs.Units(UnitTypes.Scout, LevelTypes.First, UnitEs(idx_0).MainE.OwnerC.Player).TakeUnit();

                    var ownerUnit = UnitEs(idx_0).MainE.OwnerC.Player;
                    var twC = UnitEs(idx_0).ToolWeaponE.ToolWeaponTC;
                    var levTWC = UnitEs(idx_0).ToolWeaponE.LevelTC;

                    if (twC.HaveTW)
                    {
                        Es.InventorToolWeaponEs.ToolWeapons(twC.ToolWeapon, levTWC.Level, ownerUnit).ToolWeapons.Amount += 1;
                        UnitEs(idx_0).ToolWeaponE.Reset();
                    }
                    UnitEs(idx_0).MainE.Clear(Es.WhereUnitsEs);

                    UnitEs(idx_0).MainE.SetNew((UnitTypes.Scout, LevelTypes.First, ownerUnit, ConditionUnitTypes.None, false), Es);

                    Es.Rpc.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }

                else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
            }
            else Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreHp, sender);
        }
    }
}