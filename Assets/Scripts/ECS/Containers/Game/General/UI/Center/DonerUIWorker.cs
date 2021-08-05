using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using UnityEngine;

namespace Assets.Scripts.Workers.Game.UI.Middle
{
    internal sealed class DonerUIWorker
    {
        internal static void SetColor(Color color) => MainGameSystem.DonerUIEnt_ButtonCom.Button.image.color = color;
    }
}
