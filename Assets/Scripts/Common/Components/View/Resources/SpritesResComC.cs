using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Common
{
    public struct SpritesResComC
    {
        private static Dictionary<SpriteGameTypes, Sprite> _gameSprites;


        internal SpritesResComC(bool needUpload)
        {
            if (needUpload)
            {
                _gameSprites = new Dictionary<SpriteGameTypes, Sprite>();

                var sectionName = "Sprites/";


                _gameSprites.Add(SpriteGameTypes.BlackCell, Resources.Load<Sprite>(sectionName + "Black_Sprite"));
                _gameSprites.Add(SpriteGameTypes.WhiteCell, Resources.Load<Sprite>(sectionName + "White_Sprite"));



                _gameSprites.Add(SpriteGameTypes.King, Resources.Load<Sprite>(sectionName + "King_Sprite"));
                _gameSprites.Add(SpriteGameTypes.PawnWood, Resources.Load<Sprite>(sectionName + "PawnWood_Sprite"));
                _gameSprites.Add(SpriteGameTypes.PawnIron, Resources.Load<Sprite>(sectionName + "PawnIron_Sprite"));

                _gameSprites.Add(SpriteGameTypes.RookBow, Resources.Load<Sprite>(sectionName + "RookBow_Sprite"));
                _gameSprites.Add(SpriteGameTypes.RookCrossbow, Resources.Load<Sprite>(sectionName + "RookCrossbow_Sprite"));

                _gameSprites.Add(SpriteGameTypes.BishopBow, Resources.Load<Sprite>(sectionName + "BishopBow_Sprite"));
                _gameSprites.Add(SpriteGameTypes.BishopCrossbow, Resources.Load<Sprite>(sectionName + "BishopCrossbow_Sprite"));

                _gameSprites.Add(SpriteGameTypes.Scout, Resources.Load<Sprite>(sectionName + "Scout_Sprite"));

                _gameSprites.Add(SpriteGameTypes.PickWood, Resources.Load<Sprite>(sectionName + "Pick_Sprite"));
                _gameSprites.Add(SpriteGameTypes.SwordIron, Resources.Load<Sprite>(sectionName + "SwordIron_Sprite"));
                _gameSprites.Add(SpriteGameTypes.ShieldWood, Resources.Load<Sprite>(sectionName + "ShieldWood_Sprite"));
                _gameSprites.Add(SpriteGameTypes.ShieldIron, Resources.Load<Sprite>(sectionName + "ShieldIron_Sprite"));



                _gameSprites.Add(SpriteGameTypes.City, Resources.Load<Sprite>(sectionName + "City_Sprite"));
                _gameSprites.Add(SpriteGameTypes.CityNone, Resources.Load<Sprite>(sectionName + "CityBack_Sprite"));
                _gameSprites.Add(SpriteGameTypes.CityBack, Resources.Load<Sprite>(sectionName + "CityBack_Sprite"));

                _gameSprites.Add(SpriteGameTypes.Farm, Resources.Load<Sprite>(sectionName + "Farm_Sprite"));
                _gameSprites.Add(SpriteGameTypes.FarmBack, Resources.Load<Sprite>(sectionName + "FarmBack_Sprite"));

                _gameSprites.Add(SpriteGameTypes.Woodcutter, Resources.Load<Sprite>(sectionName + "Woodcutter_Sprite"));
                _gameSprites.Add(SpriteGameTypes.WoodcutterBack, Resources.Load<Sprite>(sectionName + "WoodcutterBack_Sprite"));

                _gameSprites.Add(SpriteGameTypes.Mine, Resources.Load<Sprite>(sectionName + "Mine_Sprite"));
                _gameSprites.Add(SpriteGameTypes.MineBack, Resources.Load<Sprite>(sectionName + "Mine_Back"));

                _gameSprites.Add(SpriteGameTypes.Camp, Resources.Load<Sprite>(sectionName + "Camp_Sprite"));
                _gameSprites.Add(SpriteGameTypes.CampBack, Resources.Load<Sprite>(sectionName + "CampBack_Sprite"));



                _gameSprites.Add(SpriteGameTypes.YoungForest, Resources.Load<Sprite>(sectionName + "YoungForest_Sprite"));



                _gameSprites.Add(SpriteGameTypes.Fire, Resources.Load<Sprite>(sectionName + "Fire_Sprite"));
                _gameSprites.Add(SpriteGameTypes.FireNone, Resources.Load<Sprite>(sectionName + "FireNone_Sprite"));



                _gameSprites.Add(SpriteGameTypes.CircularAttack, Resources.Load<Sprite>(sectionName + "CircularAttack_Sprite"));
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