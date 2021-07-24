using System;

namespace Assets.Scripts.Workers.Cell
{
    internal class CellEffectsWorker : MainGeneralWorker
    {
        private static void SetHaveEffect(bool haveEffect, int[] xy) => EGGM.CellFireEnt_HaverEffectCom(xy).HaveEffect = haveEffect;
        internal static bool HaveEffect(int[] xy) => EGGM.CellFireEnt_HaverEffectCom(xy).HaveEffect;

        internal static int TimeSteps(int[] xy) => EGGM.CellFireEnt_TimeStepsCom(xy).TimeSteps;
        internal static void SetTimeSteps(int value, int[] xy) => EGGM.CellFireEnt_TimeStepsCom(xy).TimeSteps = value;

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
                    EGGM.CellFireEnt_SprRendCom(xy).SpriteRenderer.enabled = true;
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
                    EGGM.CellFireEnt_SprRendCom(xy).SpriteRenderer.enabled = false;
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
                    EGGM.CellFireEnt_SprRendCom(xy).SpriteRenderer.enabled = isActive;
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
                    return EGGM.CellFireEnt_TimeStepsCom(xy).TimeSteps;

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
                    EGGM.CellFireEnt_TimeStepsCom(xy).TimeSteps = value;
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
                    EGGM.CellFireEnt_TimeStepsCom(xy).TimeSteps += adding;
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
                    EGGM.CellFireEnt_TimeStepsCom(xy).TimeSteps -= taking;
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
