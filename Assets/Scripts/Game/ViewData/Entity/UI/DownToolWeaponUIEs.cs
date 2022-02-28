using UnityEngine;

namespace Chessy.Game
{
    public struct DownToolWeaponUIEs
    {
        //static Dictionary<string, Entity> _toolWeapon;

        //public ButtonUIC ButtonUIC;
        //public ImageUIC ImageUIC;
        //public TextUIC TextUIC;

        //public static ref C Button<C>(in ToolWeaponTypes tw) where C : struct => ref _ents[tw].Get<C>();
        //public static ref ImageUIC Image(in ToolWeaponTypes tw, in LevelTypes level) => ref _toolWeapon[tw.ToString() + level].Get<ImageUIC>();

        public DownToolWeaponUIEs(in Transform downZone)
        {
            //var gTZone = downZone.Find("GiveTake");

            //for (var tw = ToolWeaponTypes.None + 1; tw < ToolWeaponTypes.End; tw++)
            //{
            //    var zone = gTZone.Find(tw.ToString());

            //    ButtonUIC = new ButtonUIC(zone.Find("Button").GetComponent<Button>());
            //    ImageUIC = new ImageUIC(zone.Find("Back_Image").GetComponent<Image>());
            //    TextUIC = new TextUIC(zone.Find("Amount_TextMP").GetComponent<TextMeshProUGUI>());

            //    for (var level = LevelTypes.None + 1; level < LevelTypes.End; level++)
            //    {
            //        _toolWeapon.Add(tw.ToString() + level, gameW.NewEntity()
            //            .Add(new ImageUIC(zone.Find(tw.ToString()).Find(level + "_Image").GetComponent<Image>())));
            //    }
            //}
        }
    }
}
