using ECS;
using Game.Common;
using TMPro;

namespace Game.Game
{
    public readonly struct DownUIEs
    {
        public readonly DownPawnUIE PawnEs;

        public DownUIEs(in EcsWorld gameW)
        {
            var downZone = CanvasC.FindUnderCurZone("DownZone").transform;


            PawnEs = new DownPawnUIE(downZone, gameW);

            new DownToolWeaponUIEs(gameW, downZone);
            new UIEntDownDoner(gameW, downZone);
            
            new DownScoutUIEs(gameW, downZone);
            new DownHeroUIE(gameW, downZone);
        }
    }
}