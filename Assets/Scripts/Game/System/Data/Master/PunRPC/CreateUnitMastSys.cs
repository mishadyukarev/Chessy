﻿using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;

namespace Chessy.Game
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
                if (InvResC.CanCreateUnit(playerSend, unitTypeForCreating, out var needRes))
                {
                    InvResC.BuyCreateUnit(playerSend, unitTypeForCreating);
                    InvUnitsC.AddUnit(playerSend, unitTypeForCreating, LevelUnitTypes.First);

                    RpcSys.SoundToGeneral(sender, ClipTypes.SoundGoldPack);
                }
                else
                {
                    RpcSys.SoundToGeneral(sender, ClipTypes.Mistake);
                    RpcSys.MistakeEconomyToGeneral(sender, needRes);
                }
            }
            else
            {
                RpcSys.SoundToGeneral(sender, ClipTypes.Mistake);
                RpcSys.SimpleMistakeToGeneral(MistakeTypes.NeedCity, sender);
            }
        }
    }
}