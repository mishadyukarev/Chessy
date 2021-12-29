using System;
using UnityEngine;

namespace Game.Game
{
    public struct BuildVC : IBuildCellV
    {
        SpriteRenderer _build_SR;


        public BuildVC(GameObject build)
        {
            _build_SR = build.GetComponent<SpriteRenderer>();
        }

        public void Set(BuildTypes build, bool isVisForMe, bool isVisForNext)
        {
            if (build != BuildTypes.None)
            {
                if (isVisForMe)
                {
                    switch (build)
                    {
                        case BuildTypes.None:
                            throw new Exception();

                        case BuildTypes.City:
                            _build_SR.sprite = SpritesResC.Sprite(SpriteTypes.City);
                            break;

                        case BuildTypes.Farm:
                            _build_SR.sprite = SpritesResC.Sprite(SpriteTypes.Farm);
                            break;

                        case BuildTypes.Woodcutter:
                            _build_SR.sprite = SpritesResC.Sprite(SpriteTypes.Woodcutter);
                            break;

                        case BuildTypes.Mine:
                            _build_SR.sprite = SpritesResC.Sprite(SpriteTypes.Mine);
                            break;

                        case BuildTypes.Camp:
                            _build_SR.sprite = SpritesResC.Sprite(SpriteTypes.Camp);
                            break;

                        default:
                            throw new Exception();
                    }
                    _build_SR.enabled = true;


                    var color = _build_SR.color;
                    color.a = isVisForNext ? 1 : 0.7f;

                    _build_SR.color = color;
                }
                else
                {
                    _build_SR.enabled = false;
                }
            }
            else
            {
                _build_SR.enabled = false;
            }
        }
    }
}
