
namespace Game.Game
{
    sealed class ShieldUIS : SystemViewAbstract, IEcsRunSystem
    {
        public ShieldUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var tw_sel = ref Es.CellEs.UnitEs.ToolWeapon(Es.SelectedIdxE.IdxC.Idx).ToolWeapon;
            ref var twLevel_sel = ref Es.CellEs.UnitEs.ToolWeapon(Es.SelectedIdxE.IdxC.Idx).LevelTW;

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