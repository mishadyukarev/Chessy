﻿
namespace Chessy.Game
{
    sealed class ShieldUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal ShieldUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            //var tw_sel = UnitEs(Es.SelectedIdxC.Idx).ToolWeaponE.ToolWeaponTC;
            //var twLevel_sel = UnitEs(Es.SelectedIdxC.Idx).ToolWeaponE.LevelTC;

            //UIEntExtraTW.Image<ImageUIC>(ToolWeaponTypes.Pick, LevelTypes.Second).SetActive(false);
            //UIEntExtraTW.Image<ImageUIC>(ToolWeaponTypes.Sword, LevelTypes.Second).SetActive(false);
            //UIEntExtraTW.Image<ImageUIC>(ToolWeaponTypes.Shield, LevelTypes.First).SetActive(false);
            //UIEntExtraTW.Image<ImageUIC>(ToolWeaponTypes.Shield, LevelTypes.Second).SetActive(false);

            //if (tw_sel.HaveTW && !tw_sel.Is(ToolWeaponTypes.BowCrossbow))
            //{
            //    UIEntExtraTW.Image<ImageUIC>(tw_sel.ToolWeapon, twLevel_sel.Level).SetActive(true);
            //}
        }
    }
}