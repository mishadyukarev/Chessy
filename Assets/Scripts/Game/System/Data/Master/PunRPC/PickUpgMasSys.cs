﻿using Leopotam.Ecs;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    public sealed class PickUpgMasSys : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var whoseMove = WhoseMoveC.WhoseMove;

            if (PickUpgZoneDataUIC.HaveUpgrade(whoseMove))
            {
                switch (ForPickUpgMasC.UpgButType)
                {
                    case PickUpgradeTypes.None: throw new Exception();

                    case PickUpgradeTypes.King:
                        UnitsUpgC.SetUpg(whoseMove, UnitTypes.King, UnitStatTypes.Damage, 0.2f);
                        break;

                    case PickUpgradeTypes.Pawn:
                        UnitsUpgC.SetUpg(whoseMove, UnitTypes.Pawn, UnitStatTypes.Damage, 0.2f);
                        break;

                    case PickUpgradeTypes.Archer:
                        UnitsUpgC.SetUpg(whoseMove, UnitTypes.Rook, UnitStatTypes.Damage, 0.2f);
                        UnitsUpgC.SetUpg(whoseMove, UnitTypes.Bishop, UnitStatTypes.Damage, 0.2f);
                        break;

                    case PickUpgradeTypes.Scout:
                        UnitsUpgC.SetStepUpg(whoseMove, UnitTypes.Scout, 3);
                        break;

                    case PickUpgradeTypes.Water:
                        for (var unit = (UnitTypes)1; unit < (UnitTypes)typeof(UnitTypes).GetEnumNames().Length; unit++)
                        {
                            UnitsUpgC.SetUpg(whoseMove, unit, UnitStatTypes.Water, 0.2f);
                        }
                        break;

                    case PickUpgradeTypes.Farm:
                        BuildsUpgC.AddUpgrade(whoseMove, BuildTypes.Farm);
                        break;

                    case PickUpgradeTypes.Woodcutter:
                        BuildsUpgC.AddUpgrade(whoseMove, BuildTypes.Woodcutter);
                        break;

                    case PickUpgradeTypes.Mine:
                        BuildsUpgC.AddUpgrade(whoseMove, BuildTypes.Mine);
                        break;

                    default: throw new Exception();
                }


                PickUpgZoneDataUIC.SetHaveUpgrade(whoseMove, false);
                PickUpgZoneDataUIC.SetHave_But(whoseMove, ForPickUpgMasC.UpgButType, false);

                RpcSys.SoundToGeneral(sender, ClipGameTypes.PickUpgrade);
            }
        }
    }
}