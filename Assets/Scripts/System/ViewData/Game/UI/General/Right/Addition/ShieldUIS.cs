
namespace Game.Game
{
    sealed class ShieldUIS : SystemViewAbstract, IEcsRunSystem
    {
        public ShieldUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var tw_sel = UnitEs(Es.SelectedIdxE.IdxC.Idx).ToolWeaponE.ToolWeaponTC;
            var twLevel_sel = UnitEs(Es.SelectedIdxE.IdxC.Idx).ToolWeaponE.LevelTC;

            UIEntExtraTW.Image<ImageUIC>(ToolWeaponTypes.Pick, LevelTypes.Second).SetActive(false);
            UIEntExtraTW.Image<ImageUIC>(ToolWeaponTypes.Sword, LevelTypes.Second).SetActive(false);
            UIEntExtraTW.Image<ImageUIC>(ToolWeaponTypes.Shield, LevelTypes.First).SetActive(false);
            UIEntExtraTW.Image<ImageUIC>(ToolWeaponTypes.Shield, LevelTypes.Second).SetActive(false);

            if (tw_sel.HaveTW)
            {
                UIEntExtraTW.Image<ImageUIC>(tw_sel.ToolWeapon, twLevel_sel.Level).SetActive(true);
            }
        }
    }
}