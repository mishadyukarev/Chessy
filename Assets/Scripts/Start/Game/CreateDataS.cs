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


            var syncAbilities = new EcsSystems(gameWorld)
                .Add(new AbilSyncMasSys());

            dataSC.Add(DataSystTypes.GetAbilities, syncAbilities.Run);


            var runUpd = new EcsSystems(gameWorld)
                .Add(new InputSystem())
                .Add(new RaySystem())
                .Add(new SelectorS());

            dataSC.Add(DataSystTypes.RunUpdate, runUpd.Run);


            var runFixedUpd = new EcsSystems(gameWorld)
                .Add(syncAbilities)
                .Add(new SoundS());

            dataSC.Add(DataSystTypes.RunFixedUpdate, runUpd.Run);



            var getAvailCells = new EcsSystems(gameWorld)
                .Add(new ClearAvailCellsS())
                .Add(new GetAttackKingCellsS())
                .Add(new GetAttackPawnCellsS())
                .Add(new GetAttackArcherCellsSs())
                .Add(new GetSetUnitCellsS())
                .Add(new GetShiftCellsS())
                .Add(new GetArsonCellsS());

            dataSC.Add(DataSystTypes.GetAvailCells, getAvailCells.Run);


            


            new DataSC(list);



            gameSysts
                .Add(runUpd)
                .Add(runFixedUpd)
                .Add(getAvailCells);
        }
    }
}