using Leopotam.Ecs;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public sealed class CreateDataS
    {
        public CreateDataS(EcsSystems gameSysts)
        {
            var gameWorld = gameSysts.World;


            var list = new List<object>();
            var dataSC = new Dictionary<DataSystTypes, Action>();
            list.Add(dataSC);


            var runUpd = new EcsSystems(gameWorld)
                .Add(new InputSystem())
                .Add(new RaySystem())
                .Add(new SelectorS());

            dataSC.Add(DataSystTypes.RunUpdate, runUpd.Run);



            var runFixedUpd = new EcsSystems(gameWorld)
                .Add(new SoundS());

            dataSC.Add(DataSystTypes.RunFixedUpdate, runFixedUpd.Run);


            var afterDoing = new EcsSystems(gameWorld)
                .Add(new VisibElseS())

                .Add(new AbilSyncMS())

                .Add(new ClearAvailCellsS())
                .Add(new GetAttackKingCellsS())
                .Add(new GetAttackPawnCellsS())
                .Add(new GetAttackArcherCellsSs())
                .Add(new GetSetUnitCellsS())
                .Add(new GetShiftCellsS())
                .Add(new GetArsonCellsS());

            dataSC.Add(DataSystTypes.RunAfterDoing, afterDoing.Run);


            


            new DataSC(list);



            gameSysts
                .Add(runUpd)
                .Add(runFixedUpd)
                .Add(afterDoing);
        }
    }
}