﻿using Assets.Scripts.Workers.UI;

namespace Assets.Scripts.Workers.Game.UI
{
    internal sealed class UIMiddleWorker : MainGeneralUIWorker
    {
        internal static bool IsReady(bool key) => EGGUIM.ReadyEnt_ActivatedDictCom.IsActivatedButtonDict[key];
        internal static void SetIsReady(bool key, bool value) => EGGUIM.ReadyEnt_ActivatedDictCom.IsActivatedButtonDict[key] = value;
    }
}
