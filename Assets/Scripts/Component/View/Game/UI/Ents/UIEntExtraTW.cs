using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public struct UIEntExtraTW
    {
        static readonly Dictionary<string, Entity> _tw_Images;

        public static ref C Image<C>(in ToolWeaponTypes tw, in LevelTypes level) where C : struct => ref _tw_Images[tw.ToString() + level].Get<C>();


        static UIEntExtraTW()
        {
            _tw_Images = new Dictionary<string, Entity>();
            for (var tw = ToolWeaponTypes.Start; tw <= ToolWeaponTypes.End; tw++)
                for (var level = LevelTypes.None; level <= LevelTypes.End; level++)
                    _tw_Images.Add(tw.ToString() + level, default);
        }
        public UIEntExtraTW(in EcsWorld gameW, in Transform rightZone)
        {
            var additionZone = rightZone.Find("AdditionZone");

            _tw_Images[ToolWeaponTypes.Pick.ToString() + LevelTypes.Second] = gameW.NewEntity()
                .Add(new ImageUIC(additionZone.Find("PickIron_Image").GetComponent<Image>()));

            _tw_Images[ToolWeaponTypes.Sword.ToString() + LevelTypes.Second] = gameW.NewEntity()
                .Add(new ImageUIC(additionZone.Find("SwordIron_Image").GetComponent<Image>()));

            _tw_Images[ToolWeaponTypes.Shield.ToString() + LevelTypes.First] = gameW.NewEntity()
                .Add(new ImageUIC(additionZone.Find("ShieldWood_Image").GetComponent<Image>()));

            _tw_Images[ToolWeaponTypes.Shield.ToString() + LevelTypes.Second] = gameW.NewEntity()
                .Add(new ImageUIC(additionZone.Find("ShieldIron_Image").GetComponent<Image>()));
        }
    }
}