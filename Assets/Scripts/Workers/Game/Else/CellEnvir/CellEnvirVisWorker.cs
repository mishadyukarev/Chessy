using Assets.Scripts.ECS.Game.General.Entities.Containers;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.Else.CellEnvir
{
    internal sealed class CellEnvirVisWorker
    {
        private static CellEnvirEntsContainer _cellEnvironmentEntsContainer;

        internal CellEnvirVisWorker(CellEnvirEntsContainer cellEnvironmentEntsContainer)
        {
            _cellEnvironmentEntsContainer = cellEnvironmentEntsContainer;
        }

        private static SpriteRenderer GetSR(EnvironmentTypes environmentType, int[] xy)
        {
            switch (environmentType)
            {
                case EnvironmentTypes.None:
                    throw new Exception();

                case EnvironmentTypes.Fertilizer:
                    return _cellEnvironmentEntsContainer.CellFertilizerEnt_SprRendCom(xy).SpriteRenderer;

                case EnvironmentTypes.YoungForest:
                    return _cellEnvironmentEntsContainer.CellYoungForestEnt_SprRendCom(xy).SpriteRenderer;

                case EnvironmentTypes.AdultForest:
                    return _cellEnvironmentEntsContainer.CellAdultForestEnt_SprRendCom(xy).SpriteRenderer;

                case EnvironmentTypes.Hill:
                    return _cellEnvironmentEntsContainer.CellHillEnt_SprRendCom(xy).SpriteRenderer;

                case EnvironmentTypes.Mountain:
                    return _cellEnvironmentEntsContainer.CellMountainEnt_SprRendCom(xy).SpriteRenderer;

                default:
                    throw new Exception();
            }
        }

        internal static void ActiveEnvirVis(bool isEnabled, EnvironmentTypes environmentType, int[] xy) => GetSR(environmentType, xy).enabled = isEnabled;

    }
}
