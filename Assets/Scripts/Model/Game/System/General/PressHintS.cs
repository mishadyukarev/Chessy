using Chessy.Common.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class PressHintS : SystemModel
    {
        readonly PageBookTypes _neededPageBookT;
        bool _isPressed;
        float _timer;

        const float TIMER = 0.5f;

        public PressHintS(in PageBookTypes pageBookT, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _neededPageBookT = pageBookT;
        }

        public void Press(in bool isPressed) => _isPressed = isPressed;

        public void Update()
        {
            if (_isPressed && !eMG.Common.IsOpenedBook)
            {
                _timer += Time.deltaTime;

                if (_timer >= TIMER)
                {
                    eMG.Common.SoundActionC(ClipCommonTypes.OpenBook).Invoke();


                    eMG.Common.IsOpenedBook = true;
                    eMG.Common.PageBookT = _neededPageBookT;

                    eMG.NeedUpdateView = true;
                }
            }
            else _timer = 0;
        }
    }
}