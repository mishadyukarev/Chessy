using Chessy.Common.Entity;
using Chessy.Common.Enum;
using Chessy.Common.Interface;
using System;
using System.Collections.Generic;

namespace Chessy.Common.Model.System
{
    public sealed class SystemsModelCommon : IEcsRunSystem, IToggleScene
    {
        readonly EntitiesModelCommon _eMCommon;
        readonly List<IEcsRunSystem> _updates;

        public SystemsModelCommon(in EntitiesModelCommon eMCommon)
        {
            _eMCommon = eMCommon;

            _updates = new List<IEcsRunSystem>()
            {
                new AdLaunchS(eMCommon),
            };
        }

        public void Run()
        {
            _updates.ForEach((IEcsRunSystem iRun) => iRun.Run());
        }


        public void ToggleScene(in SceneTypes newSceneT)
        {
            if (_eMCommon.SceneTC.Is(newSceneT)) throw new Exception("Need other scene");

            _eMCommon.SceneTC.Scene = newSceneT;

            switch (newSceneT)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        break;
                    }

                case SceneTypes.Game:
                    {
                        _eMCommon.BookE.IsOpenedBook = true;
                        _eMCommon.BookE.PageBookTC.PageBookT = PageBookTypes.Main;
                        break;
                    }
                default: throw new Exception();
            }
        }

    }
}