namespace Game.Game
{
    sealed class SetUnitMS : SystemAbstract, IEcsRunSystem
    {
        internal SetUnitMS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            var whoseMove = E.WhoseMove.Player;

            var idx_0 = E.RpcPoolEs.SetUnitME.IdxC.Idx;
            var unitT = E.RpcPoolEs.SetUnitME.UnitTC.Unit;
            var sender = E.RpcPoolEs.SenderC.Player;


            if (unitT != UnitTypes.None)
            {
                if (E.UnitEs(idx_0).ForPlayer(whoseMove).CanSetUnitHere)
                {
                    E.UnitTC(idx_0).Unit = unitT;
                    E.UnitPlayerTC(idx_0).Player = whoseMove;
                    E.UnitLevelTC(idx_0).Level = LevelTypes.First;
                    E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    E.UnitIsRightArcherC(idx_0).IsRight = false;
                    E.UnitHpC(idx_0).Health = CellUnitStatHp_Values.MAX_HP;
                    E.UnitStepC(idx_0).Steps = E.UnitInfo(E.UnitMainE(idx_0)).MaxSteps;
                    E.UnitWaterC(idx_0).Water = E.UnitInfo(E.UnitMainE(idx_0)).MaxWater;
                    E.UnitExtraTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                    E.UnitExtraLevelTC(idx_0).Level = LevelTypes.None;
                    E.UnitExtraProtectionTC(idx_0).Protection = 0;
                    E.UnitEffectStunC(idx_0).Stun = 0;
                    E.UnitEffectShield(idx_0).Protection = 0;
                    E.UnitEffectFrozenArrawC(idx_0).Shoots = 0;

                    E.UnitInfo(E.UnitMainE(idx_0)).UnitsInGame++;


                    if (unitT == UnitTypes.Pawn)
                    {
                        E.PlayerE(whoseMove).PeopleInCity--;

                        E.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.Axe;
                        E.UnitMainTWLevelTC(idx_0).Level = LevelTypes.First;
                    }

                    else
                    {
                        E.UnitInfo(whoseMove, LevelTypes.First, unitT).HaveInInventor = false;

                        E.UnitMainTWTC(idx_0).ToolWeapon = ToolWeaponTypes.None;
                        E.UnitMainTWLevelTC(idx_0).Level = LevelTypes.None;
                    }

                    E.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
                }


                E.RpcPoolEs.SetUnitME.UnitTC.Unit = UnitTypes.None;
            }
        }
    }
}