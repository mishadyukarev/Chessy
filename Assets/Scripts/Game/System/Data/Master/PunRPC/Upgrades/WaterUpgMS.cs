using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class WaterUpgMS : IEcsRunSystem
    {
        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var whoseMove = WhoseMoveC.WhoseMove;


            for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
            {
                for (var level = LevelTypes.First; level < LevelTypes.End; level++)
                {
                    UnitUpgC.AddUpg(UpgTypes.PickCenter, UnitStatTypes.Water, unit, level, whoseMove);
                }
            }

            WaterAvailPickUpgC.Set(whoseMove, false);
            PickUpgC.SetHaveUpgrade(whoseMove, false);
            RpcSys.SoundToGeneral(sender, ClipTypes.PickUpgrade);
        }
    }
}