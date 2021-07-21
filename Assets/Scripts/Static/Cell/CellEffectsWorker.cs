using System;

namespace Assets.Scripts.Static.Cell
{
    internal class CellEffectsWorker : MainWorker
    {
        internal static bool HaveEffect(EffectTypes effectType, int[] xy)
        {
            switch (effectType)
            {
                case EffectTypes.None:
                    throw new Exception();

                case EffectTypes.Fire:
                    return EGGM.CellFireEnt_HaverEffectCom(xy).HaveEffect;

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
                    EGGM.CellFireEnt_HaverEffectCom(xy).HaveEffect = true;
                    EGGM.CellFireEnt_TimeStepsCom(xy).ResetTimeSteps();
                    EGGM.CellFireEnt_SprRendCom(xy).Enabled = true;
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
                    EGGM.CellFireEnt_HaverEffectCom(xy).HaveEffect = false;
                    EGGM.CellFireEnt_TimeStepsCom(xy).ResetTimeSteps();
                    EGGM.CellFireEnt_SprRendCom(xy).Enabled = false;
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
                    EGGM.CellFireEnt_HaverEffectCom(xy).HaveEffect = isActive;
                    EGGM.CellFireEnt_SprRendCom(xy).Enabled = isActive;
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
                    EGGM.CellFireEnt_TimeStepsCom(xy).SetTimeSteps(value);
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
                    EGGM.CellFireEnt_TimeStepsCom(xy).AddTimeSteps(adding);
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
                    EGGM.CellFireEnt_TimeStepsCom(xy).TakeTimeSteps(taking);
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}
