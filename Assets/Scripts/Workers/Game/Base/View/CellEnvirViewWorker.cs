using Assets.Scripts.ECS.Entities.Game.General.Cells.View.Containers.Cell;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.Else.CellEnvir
{
    internal sealed class CellEnvirViewWorker
    {
        private static CellEnvirViewContainerEnts _cellEnvirViewContainerEnts;

        internal CellEnvirViewWorker(CellEnvirViewContainerEnts cellEnvirViewContainerEnts)
        {
            _cellEnvirViewContainerEnts = cellEnvirViewContainerEnts;
        }

        private static SpriteRenderer GetSR(EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return _cellEnvirViewContainerEnts.CellFertilizerEnt_SprRendCom(xy).SpriteRenderer;

                case EnvironmentTypes.YoungForest:
                    return _cellEnvirViewContainerEnts.CellYoungForestEnt_SprRendCom(xy).SpriteRenderer;

                case EnvironmentTypes.AdultForest:
                    return _cellEnvirViewContainerEnts.CellAdultForestEnt_SprRendCom(xy).SpriteRenderer;

                case EnvironmentTypes.Hill:
                    return _cellEnvirViewContainerEnts.CellHillEnt_SprRendCom(xy).SpriteRenderer;

                case EnvironmentTypes.Mountain:
                    return _cellEnvirViewContainerEnts.CellMountainEnt_SprRendCom(xy).SpriteRenderer;

                default:
                    throw new Exception();
            }
        }

        internal static void ActiveEnvirVis(bool isEnabled, EnvironmentTypes environmentType, int[] xy) => GetSR(environmentType, xy).enabled = isEnabled;

    }
}
