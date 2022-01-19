using ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct ResourcesSpriteVEs
    {
        static Dictionary<SpriteTypes, Entity> _gameSprites;
        static Dictionary<UnitTypes, Entity> _units;

        public static ref SpriteVC SpriteVC(in SpriteTypes sprite)
        {
            if (!_gameSprites.ContainsKey(sprite)) throw new Exception();
            return ref _gameSprites[sprite].Get<SpriteVC>();
        }
        public static ref SpriteVC UnitSpriteVC(in UnitTypes unit)
        {
            if (!_units.ContainsKey(unit)) throw new Exception();
            return ref _units[unit].Get<SpriteVC>();
        }

        public ResourcesSpriteVEs(in EcsWorld gameW)
        {
            _gameSprites = new Dictionary<SpriteTypes, Entity>();
            _units = new Dictionary<UnitTypes, Entity>();


            _gameSprites.Add(SpriteTypes.BlackCell, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("Black_Sprite"))));


            _gameSprites.Add(SpriteTypes.WhiteCell, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("White_Sprite"))));



            _gameSprites.Add(SpriteTypes.King, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("King_Sprite"))));
            _gameSprites.Add(SpriteTypes.PawnWood, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("PawnWood_Sprite"))));
            _gameSprites.Add(SpriteTypes.PawnIron, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("PawnIron_Sprite"))));

            _gameSprites.Add(SpriteTypes.RookBow, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("RookBow_Sprite"))));
            _gameSprites.Add(SpriteTypes.RookCrossbow, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("RookCrossbow_Sprite"))));

            _gameSprites.Add(SpriteTypes.BishopBow, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("BishopBow_Sprite"))));
            _gameSprites.Add(SpriteTypes.BishopCrossbow, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("BishopCrossbow_Sprite"))));

            _gameSprites.Add(SpriteTypes.Scout, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("Scout_Sprite"))));
            _gameSprites.Add(SpriteTypes.Elfemale, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("Elfemale_Sprite"))));

            _gameSprites.Add(SpriteTypes.PickWood, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("Pick_Sprite"))));
            _gameSprites.Add(SpriteTypes.SwordIron, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("SwordIron_Sprite"))));
            _gameSprites.Add(SpriteTypes.ShieldWood, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("ShieldWood_Sprite"))));
            _gameSprites.Add(SpriteTypes.ShieldIron, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("ShieldIron_Sprite"))));

            //for (var unit = UnitTypes.Camel; unit <= UnitTypes.End; unit++)
            //{
            //    _units.Add(unit, gameW.NewEntity()
            //        .Add(new SpriteVC(Resources.Load<Sprite>(unit +"_Sprite"))));
            //}





            _gameSprites.Add(SpriteTypes.City, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("City_Sprite"))));
            _gameSprites.Add(SpriteTypes.CityNone, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("CityNone_Sprite"))));
            _gameSprites.Add(SpriteTypes.CityBack, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("CityBack_Sprite"))));

            _gameSprites.Add(SpriteTypes.Farm, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("Farm_Sprite"))));
            _gameSprites.Add(SpriteTypes.FarmBack, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("FarmBack_Sprite"))));

            _gameSprites.Add(SpriteTypes.Woodcutter, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("Woodcutter_Sprite"))));
            _gameSprites.Add(SpriteTypes.WoodcutterBack, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("WoodcutterBack_Sprite"))));

            _gameSprites.Add(SpriteTypes.Mine, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("Mine_Sprite"))));
            _gameSprites.Add(SpriteTypes.MineBack, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("MineBack_Sprite"))));

            _gameSprites.Add(SpriteTypes.Camp, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("Camp_Sprite"))));
            _gameSprites.Add(SpriteTypes.CampBack, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("CampBack_Sprite"))));



            _gameSprites.Add(SpriteTypes.YoungForest, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("YoungForest_Sprite"))));



            _gameSprites.Add(SpriteTypes.Fire, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("Fire_Sprite"))));
            _gameSprites.Add(SpriteTypes.FireNone, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("FireNone_Sprite"))));



            _gameSprites.Add(SpriteTypes.CircularAttack, gameW.NewEntity()
                .Add(new SpriteVC(Resources.Load<Sprite>("CircularAttack_Sprite"))));

        }
    }
}