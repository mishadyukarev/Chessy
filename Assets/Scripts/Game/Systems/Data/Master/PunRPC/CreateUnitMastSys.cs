﻿using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;

namespace Scripts.Game
{
    public sealed class CreateUnitMastSys : IEcsRunSystem
    {
        private EcsFilter<ForCreatingUnitMasCom> _creatorUnitFilter = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);

            var unitTypeForCreating = _creatorUnitFilter.Get1(0).UnitTypeForCreating;


            var playerSend = WhoseMoveC.WhoseMove;


            if (WhereBuildsC.IsSettedCity(playerSend))
            {
                if (InventResC.CanCreateUnit(playerSend, unitTypeForCreating, out var needRes))
                {
                    InventResC.BuyCreateUnit(playerSend, unitTypeForCreating);
                    InventorUnitsC.AddUnit(playerSend, unitTypeForCreating, LevelUnitTypes.Wood);

                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.SoundGoldPack);
                }
                else
                {
                    RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                    RpcSys.MistakeEconomyToGeneral(sender, needRes);
                }
            }
            else
            {
                RpcSys.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedCity, sender);
            }
        }
    }
}