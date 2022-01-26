
namespace Game.Game
{
    struct ShieldUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var tw_sel = ref CellUnitEs.ToolWeapon(Entities.SelectedIdxE.IdxC.Idx).ToolWeaponC;
            ref var twLevel_sel = ref CellUnitEs.ToolWeapon(Entities.SelectedIdxE.IdxC.Idx).LevelC;

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