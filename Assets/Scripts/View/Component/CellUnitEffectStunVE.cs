﻿//using ECS;
//using UnityEngine;

//using Chessy.Model.Entity; namespace Chessy.Model
//{
//    public sealed class CellUnitEffectStunVE : EntityAbstract
//    {
//        public ref SpriteRendererVC Stun => ref Ent.Get<SpriteRendererVC>();

//        internal CellUnitEffectStunVE(in Transform unitEffectT, in EcsWorld gameW) : base(gameW)
//        {
//            Ent.Add(new SpriteRendererVC(unitEffectT.Find("Stun_SR").GetComponent<SpriteRenderer>()));
//        }
//    }
//}