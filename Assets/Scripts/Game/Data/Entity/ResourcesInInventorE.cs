﻿//using ECS;

//namespace Chessy.Game
//{
//    public sealed class ResourcesInInventorE : EntityAbstract
//    {
//        public readonly ResourceTypes ResT;
//        public readonly PlayerTypes PlayerT;

//        public ref ResourcesC ResourceC => ref Ent.Get<ResourcesC>();

//        internal ResourcesInInventorE(in ResourceTypes res, in PlayerTypes player, in EcsWorld gameW) : base(gameW)
//        {
//            ResT = res;
//            PlayerT = player;

//            Ent.Add(new ResourcesC(Start_Values.Resources(res)));
//        }

//        public void Buy(in BuildingTypes build) => ResourceC.Resources -= ResourcesEconomy_Values.ForBuild(build, ResT);
//    }
//}