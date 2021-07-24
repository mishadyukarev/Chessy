using Assets.Scripts.ECS.Game.General.Entities.Containers;
using System;

namespace Assets.Scripts.Workers.Game.Else.Fire
{
    internal sealed class CellFireWorker
    {
        private static CellFireEntsContainer _cellFireEntsContainer;

        internal CellFireWorker(CellFireEntsContainer cellFireEntsContainer)
        {
            _cellFireEntsContainer = cellFireEntsContainer;
        }

        private static void SetHaveEffect(bool haveEffect, int[] xy) => _cellFireEntsContainer.CellFireEnt_HaverEffectCom(xy).HaveEffect = haveEffect;
        internal static bool HaveEffect(int[] xy) => _cellFireEntsContainer.CellFireEnt_HaverEffectCom(xy).HaveEffect;

        internal static int TimeSteps(int[] xy) => _cellFireEntsContainer.CellFireEnt_TimeStepsCom(xy).TimeSteps;
        internal static void SetTimeSteps(int value, int[] xy) => _cellFireEntsContainer.CellFireEnt_TimeStepsCom(xy).TimeSteps = value;

        internal static bool HaveEffect(EffectTypes effectType, int[] xy)
        {
            switch (effectType)
            {
                case EffectTypes.None:
                    throw new Exception();

                case EffectTypes.Fire:
                    return HaveEffect(xy);

                default:
                    throw new Exception();
            }
        }

        internal static void SetEffect(EffectTypes effectType, int[] xy)
        {
            switch (effectType)
            {
                case EffectTypes.None:
                    throw new Exception();

                case EffectTypes.Fire:
                    SetHaveEffect(true, xy);
                    SetTimeSteps(default, xy);
                    _cellFireEntsContainer.CellFireEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void ResetEffect(EffectTypes effectType, int[] xy)
        {
            switch (effectType)
            {
                case EffectTypes.None:
                    throw new Exception();

                case EffectTypes.Fire:
                    SetHaveEffect(false, xy);
                    SetTimeSteps(default, xy);
                    _cellFireEntsContainer.CellFireEnt_SprRendCom(xy).SpriteRenderer.enabled = false;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void SyncEffect(bool isActive, EffectTypes effectType, int[] xy)
        {
            switch (effectType)
            {
                case EffectTypes.None:
                    throw new Exception();

                case EffectTypes.Fire:
                    SetHaveEffect(isActive, xy);
                    _cellFireEntsContainer.CellFireEnt_SprRendCom(xy).SpriteRenderer.enabled = isActive;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static int TimeStepsEffect(EffectTypes effectType, int[] xy)
        {
            switch (effectType)
            {
                case EffectTypes.None:
                    throw new Exception();

                case EffectTypes.Fire:
                    return _cellFireEntsContainer.CellFireEnt_TimeStepsCom(xy).TimeSteps;

                default:
                    throw new Exception();
            }
        }

        internal static void SetTimeStepsEffect(EffectTypes effectType, int value, int[] xy)
        {
            switch (effectType)
            {
                case EffectTypes.None:
                    throw new Exception();

                case EffectTypes.Fire:
                    _cellFireEntsContainer.CellFireEnt_TimeStepsCom(xy).TimeSteps = value;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void AddTimeStepsEffect(EffectTypes effectType, int[] xy, int adding = 1)
        {
            switch (effectType)
            {
                case EffectTypes.None:
                    throw new Exception();

                case EffectTypes.Fire:
                    _cellFireEntsContainer.CellFireEnt_TimeStepsCom(xy).TimeSteps += adding;
                    break;

                default:
                    throw new Exception();
            }
        }

        internal static void TakeTimeStepsEffect(EffectTypes effectType, int[] xy, int taking = 1)
        {
            switch (effectType)
            {
                case EffectTypes.None:
                    throw new Exception();

                case EffectTypes.Fire:
                    _cellFireEntsContainer.CellFireEnt_TimeStepsCom(xy).TimeSteps -= taking;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
