using Chessy.Model.Entity;
using Chessy.Model.Values;
using UnityEngine;

namespace Chessy.Model.System
{
    sealed class TruceS : SystemModelAbstract
    {
        internal TruceS(in SystemsModel sM, EntitiesModel eM) : base(sM, eM)
        {
        }

        internal void ExecuteTruce()
        {
            for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            {
                PlayerInfoC(playerT).HaveKingInInventor = true;

                GodInfoC(playerT).HaveGodInInventor = true;
                GodInfoC(playerT).CooldownInSecondsForNextAppearance = 0;

                PawnPeopleInfoC(playerT).PeopleInCity = AfterTruceValues.PEOPLE_AFTER_TRUCE;
                PawnPeopleInfoC(playerT).AmountInGame = 0;
            }


            for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
            {
                FireC(currentCellIdx_0).HaveFire = false;

                _s.TryDestroyAllTrailsOnCell(currentCellIdx_0);


                var unitC_0 = UnitC(currentCellIdx_0);

                if (unitC_0.HaveUnit)
                {
                    if (AboutGameC.GameModeT == GameModeTypes.TrainingOffline)
                    {
                        if (unitC_0.PlayerT == PlayerTypes.First)
                        {
                            TakeUnitFromField(currentCellIdx_0);
                        }
                    }
                    else
                    {
                        TakeUnitFromField(currentCellIdx_0);
                    }
                }


                if (BuildingC(currentCellIdx_0).HaveBuilding)
                {
                    BuildingC(currentCellIdx_0).Dispose();
                }

                else
                {

                }

                if (EnvironmentC(currentCellIdx_0).HaveEnvironment(EnvironmentTypes.YoungForest))
                {
                    EnvironmentC(currentCellIdx_0).Set(EnvironmentTypes.YoungForest, 0);
                    EnvironmentC(currentCellIdx_0).SetRandom(EnvironmentTypes.AdultForest, ValuesChessy.MIN_RESOURCES_FOR_SPAWN, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                }
                else
                {
                    if (!EnvironmentC(currentCellIdx_0).HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        if (Random.Range(0f, 1f) < 0.1f)
                        {
                            EnvironmentC(currentCellIdx_0).Set(EnvironmentTypes.AdultForest, 1);
                        }
                    }
                }
            }
        }

        void TakeUnitFromField(in byte cellIdx_0)
        {
            var unitC_0 = UnitC(cellIdx_0);
            var mainTWC_0 = UnitMainTWC(cellIdx_0);
            var extaTWC_0 = UnitExtraTWC(cellIdx_0);

            if (mainTWC_0.ToolWeaponT.Is(ToolsWeaponsWarriorTypes.BowCrossbow, ToolsWeaponsWarriorTypes.Staff) || mainTWC_0.ToolWeaponT == ToolsWeaponsWarriorTypes.Axe && mainTWC_0.LevelT == LevelTypes.Second)
            {
                ToolWeaponsInInventoryC(unitC_0.PlayerT).Add(mainTWC_0.ToolWeaponT, mainTWC_0.LevelT);
            }

            if (UnitExtraTWC(cellIdx_0).HaveToolWeapon)
            {
                ToolWeaponsInInventoryC(unitC_0.PlayerT).Add(extaTWC_0.ToolWeaponT, extaTWC_0.LevelT);
            }

            if (unitC_0.UnitT == UnitTypes.Tree)
            {
                AboutGameC.HaveTreeUnitInGame = false;
            }

            UnitViewDataC(UnitViewDataC(cellIdx_0).ViewIdxCell).DataIdxCell = 0;
            unitC_0.Dispose();
        }
    }
}