using static Game.Game.CellUnitEntities;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct ShieldUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var tw_sel = ref CellUnitTWE.UnitTW<ToolWeaponC>(EntitiesPool.SelectedIdxE.IdxC.Idx);
            ref var twLevel_sel = ref CellUnitTWE.UnitTW<LevelTC>(EntitiesPool.SelectedIdxE.IdxC.Idx);

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