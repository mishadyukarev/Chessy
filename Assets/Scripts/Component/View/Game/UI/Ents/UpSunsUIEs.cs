using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UpSunsUIEs
    {
        static Entity _rightSun;
        static Entity _leftSun;

        public static ref ImageUIC ImageC(in bool isRightSun)
        {
            if (isRightSun) return ref _rightSun.Get<ImageUIC>();
            else return ref _leftSun.Get<ImageUIC>();
        }

        public UpSunsUIEs(in EcsWorld gameW, in Transform upZone)
        {
            _rightSun = gameW.NewEntity()
                .Add(new ImageUIC( upZone.Find("SunRight").Find("Image").GetComponent<Image>()));

            _leftSun = gameW.NewEntity()
                .Add(new ImageUIC(upZone.Find("SunLeft").Find("Image").GetComponent<Image>()));
        }
    }
}