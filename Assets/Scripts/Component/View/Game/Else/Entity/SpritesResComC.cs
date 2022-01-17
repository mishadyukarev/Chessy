using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public struct SpritesResC
    {
        static Dictionary<SpriteTypes, Sprite> _gameSprites;


        public SpritesResC(bool needUpload)
        {
            if (needUpload)
            {
                _gameSprites = new Dictionary<SpriteTypes, Sprite>();

                _gameSprites.Add(SpriteTypes.BlackCell, Resources.Load<Sprite>("Black_Sprite"));
                _gameSprites.Add(SpriteTypes.WhiteCell, Resources.Load<Sprite>("White_Sprite"));



                _gameSprites.Add(SpriteTypes.King, Resources.Load<Sprite>("King_Sprite"));
                _gameSprites.Add(SpriteTypes.PawnWood, Resources.Load<Sprite>("PawnWood_Sprite"));
                _gameSprites.Add(SpriteTypes.PawnIron, Resources.Load<Sprite>("PawnIron_Sprite"));

                _gameSprites.Add(SpriteTypes.RookBow, Resources.Load<Sprite>("RookBow_Sprite"));
                _gameSprites.Add(SpriteTypes.RookCrossbow, Resources.Load<Sprite>("RookCrossbow_Sprite"));

                _gameSprites.Add(SpriteTypes.BishopBow, Resources.Load<Sprite>("BishopBow_Sprite"));
                _gameSprites.Add(SpriteTypes.BishopCrossbow, Resources.Load<Sprite>("BishopCrossbow_Sprite"));

                _gameSprites.Add(SpriteTypes.Scout, Resources.Load<Sprite>("Scout_Sprite"));
                _gameSprites.Add(SpriteTypes.Elfemale, Resources.Load<Sprite>("Elfemale_Sprite"));

                _gameSprites.Add(SpriteTypes.PickWood, Resources.Load<Sprite>("Pick_Sprite"));
                _gameSprites.Add(SpriteTypes.SwordIron, Resources.Load<Sprite>("SwordIron_Sprite"));
                _gameSprites.Add(SpriteTypes.ShieldWood, Resources.Load<Sprite>("ShieldWood_Sprite"));
                _gameSprites.Add(SpriteTypes.ShieldIron, Resources.Load<Sprite>("ShieldIron_Sprite"));



                _gameSprites.Add(SpriteTypes.City, Resources.Load<Sprite>("City_Sprite"));
                _gameSprites.Add(SpriteTypes.CityNone, Resources.Load<Sprite>("CityNone_Sprite"));
                _gameSprites.Add(SpriteTypes.CityBack, Resources.Load<Sprite>("CityBack_Sprite"));

                _gameSprites.Add(SpriteTypes.Farm, Resources.Load<Sprite>("Farm_Sprite"));
                _gameSprites.Add(SpriteTypes.FarmBack, Resources.Load<Sprite>("FarmBack_Sprite"));

                _gameSprites.Add(SpriteTypes.Woodcutter, Resources.Load<Sprite>("Woodcutter_Sprite"));
                _gameSprites.Add(SpriteTypes.WoodcutterBack, Resources.Load<Sprite>("WoodcutterBack_Sprite"));

                _gameSprites.Add(SpriteTypes.Mine, Resources.Load<Sprite>("Mine_Sprite"));
                _gameSprites.Add(SpriteTypes.MineBack, Resources.Load<Sprite>("MineBack_Sprite"));

                _gameSprites.Add(SpriteTypes.Camp, Resources.Load<Sprite>("Camp_Sprite"));
                _gameSprites.Add(SpriteTypes.CampBack, Resources.Load<Sprite>("CampBack_Sprite"));



                _gameSprites.Add(SpriteTypes.YoungForest, Resources.Load<Sprite>("YoungForest_Sprite"));



                _gameSprites.Add(SpriteTypes.Fire, Resources.Load<Sprite>("Fire_Sprite"));
                _gameSprites.Add(SpriteTypes.FireNone, Resources.Load<Sprite>("FireNone_Sprite"));



                _gameSprites.Add(SpriteTypes.CircularAttack, Resources.Load<Sprite>("CircularAttack_Sprite"));
            }
        }
        public static Sprite Sprite(SpriteTypes spriteType)
        {
            if (_gameSprites.ContainsKey(spriteType)) return _gameSprites[spriteType];
            else
            {
                throw new Exception("There isn't sprite");
            }
        }
    }
}