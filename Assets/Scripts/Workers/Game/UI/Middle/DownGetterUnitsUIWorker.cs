﻿using Assets.Scripts.Workers.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Workers.Game.UI.Middle
{
    internal sealed class DownGetterUnitsUIWorker : MainGeneralUIWorker
    {
        private static Button KingButton => EGGUIM.TakerKingEnt_ButtonCom.Button;

        internal static void SetActiveKingButton(bool isActive) => KingButton.gameObject.SetActive(isActive);
        internal static void SetColorKing(Color color) => KingButton.image.color = color;
    }
}
