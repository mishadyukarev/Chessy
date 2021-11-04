using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Common
{
    public struct SpritesResComC
    {
        private static Dictionary<SpriteGameTypes, Sprite> _gameSprites;


        public SpritesResComC(bool needUpload)
        {
            if (needUpload)
            {
                _gameSprites = new Dictionary<SpriteGameTypes, Sprite>();

                _gameSprites.Add(SpriteGameTypes.BlackCell, Resources.Load<Sprite>("Black_Sprite"));
                _gameSprites.Add(SpriteGameTypes.WhiteCell, Resources.Load<Sprite>("White_Sprite"));



                _gameSprites.Add(SpriteGameTypes.King, Resources.Load<Sprite>("King_Sprite"));
                _gameSprites.Add(SpriteGameTypes.PawnWood, Resources.Load<Sprite>("PawnWood_Sprite"));
                _gameSprites.Add(SpriteGameTypes.PawnIron, Resources.Load<Sprite>("PawnIron_Sprite"));

                _gameSprites.Add(SpriteGameTypes.RookBow, Resources.Load<Sprite>("RookBow_Sprite"));
                _gameSprites.Add(SpriteGameTypes.RookCrossbow, Resources.Load<Sprite>("RookCrossbow_Sprite"));

                _gameSprites.Add(SpriteGameTypes.BishopBow, Resources.Load<Sprite>("BishopBow_Sprite"));
                _gameSprites.Add(SpriteGameTypes.BishopCrossbow, Resources.Load<Sprite>("BishopCrossbow_Sprite"));

                _gameSprites.Add(SpriteGameTypes.Scout, Resources.Load<Sprite>("Scout_Sprite"));

                _gameSprites.Add(SpriteGameTypes.PickWood, Resources.Load<Sprite>("Pick_Sprite"));
                _gameSprites.Add(SpriteGameTypes.SwordIron, Resources.Load<Sprite>("SwordIron_Sprite"));
                _gameSprites.Add(SpriteGameTypes.ShieldWood, Resources.Load<Sprite>("ShieldWood_Sprite"));
                _gameSprites.Add(SpriteGameTypes.ShieldIron, Resources.Load<Sprite>("ShieldIron_Sprite"));



                _gameSprites.Add(SpriteGameTypes.City, Resources.Load<Sprite>("City_Sprite"));
                _gameSprites.Add(SpriteGameTypes.CityNone, Resources.Load<Sprite>("CityNone_Sprite"));
                _gameSprites.Add(SpriteGameTypes.CityBack, Resources.Load<Sprite>("CityBack_Sprite"));

                _gameSprites.Add(SpriteGameTypes.Farm, Resources.Load<Sprite>("Farm_Sprite"));
                _gameSprites.Add(SpriteGameTypes.FarmBack, Resources.Load<Sprite>("FarmBack_Sprite"));

                _gameSprites.Add(SpriteGameTypes.Woodcutter, Resources.Load<Sprite>("Woodcutter_Sprite"));
                _gameSprites.Add(SpriteGameTypes.WoodcutterBack, Resources.Load<Sprite>("WoodcutterBack_Sprite"));

                _gameSprites.Add(SpriteGameTypes.Mine, Resources.Load<Sprite>("Mine_Sprite"));
                _gameSprites.Add(SpriteGameTypes.MineBack, Resources.Load<Sprite>("Mine_Back"));

                _gameSprites.Add(SpriteGameTypes.Camp, Resources.Load<Sprite>("Camp_Sprite"));
                _gameSprites.Add(SpriteGameTypes.CampBack, Resources.Load<Sprite>("CampBack_Sprite"));



                _gameSprites.Add(SpriteGameTypes.YoungForest, Resources.Load<Sprite>("YoungForest_Sprite"));



                _gameSprites.Add(SpriteGameTypes.Fire, Resources.Load<Sprite>("Fire_Sprite"));
                _gameSprites.Add(SpriteGameTypes.FireNone, Resources.Load<Sprite>("FireNone_Sprite"));



                _gameSprites.Add(SpriteGameTypes.CircularAttack, Resources.Load<Sprite>("CircularAttack_Sprite"));
            }
        }
        public static Sprite Sprite(SpriteGameTypes spriteType)
        {
            if (_gameSprites.ContainsKey(spriteType)) return _gameSprites[spriteType];
            else
            {
                throw new Exception("There isn't sprite");
            }
        }
    }
}