//using System;

//namespace Chessy.Game.Systems.Model
//{
//    sealed class UnitGetMaxStepsAndWaterS : SystemAbstract, IEcsRunSystem
//    {
//        internal UnitGetMaxStepsAndWaterS(in EntitiesModel ents) : base(ents)
//        {
//        }

//        public void Run()
//        {
//            for (var playerT = PlayerTypes.None; playerT < PlayerTypes.End; playerT++)
//            {
//                for (var levelT = LevelTypes.None + 1; levelT < LevelTypes.End; levelT++)
//                {
//                    for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
//                    {
//                        var waterMax = 1f;

//                        if (E.PlayerE(playerT).AvailableHeroTC.Is(UnitTypes.Snowy))
//                        {
//                            waterMax *= 1.5f;
//                        }
                        
//                        E.UnitInfoE(playerT, levelT).WaterUnitMax = waterMax;
//                    }
//                }
//            }


//            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
//            {
//                if (E.UnitTC(idx_0).HaveUnit)
//                {
//                    var unitT = E.UnitTC(idx_0).Unit;
//                    var levelT = E.UnitLevelTC(idx_0).Level;
//                    var playerT = E.UnitPlayerTC(idx_0).Player;

//                    var stepsMax = 0f;
//                    switch (unitT)
//                    {
//                        case UnitTypes.King: stepsMax = 1; break;

//                        case UnitTypes.Pawn:                      
//                            stepsMax = E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff) ? 3 : 1;                        
//                            break;

//                        case UnitTypes.Elfemale: stepsMax = 1.5f; break;
//                        case UnitTypes.Snowy: stepsMax = 1.5f; break;
//                        case UnitTypes.Undead: stepsMax = 2; break;
//                        case UnitTypes.Hell: stepsMax = 1; break;

//                        case UnitTypes.Skeleton: stepsMax = 2; break;

//                        case UnitTypes.Camel: stepsMax = 2; break;

//                        default: throw new Exception();
//                    }

//                    switch (E.PlayerE(playerT).AvailableHeroTC.Unit)
//                    {
//                        case UnitTypes.Undead:
//                            if (unitT == UnitTypes.Pawn) stepsMax *= 1.5f;
//                            break;
//                    }

//                    E.UnitStatsE(idx_0).MaxStepsC.Steps = stepsMax;
//                }
//            }

//        }
//    }
//}