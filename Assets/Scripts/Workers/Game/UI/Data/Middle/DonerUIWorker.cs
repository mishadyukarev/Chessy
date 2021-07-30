using Assets.Scripts.Workers.UI;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Middle
{
    internal sealed class DonerUIWorker : MainGeneralUIWorker
    {
        internal static void SetColor(Color color) => EGGUIM.DonerUIEnt_ButtonCom.Button.image.color = color;
    }
}
