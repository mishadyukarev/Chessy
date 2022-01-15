using static Game.Game.CellUnitE;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct ShieldUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var tw_sel = ref CellUnitTWE.UnitTW<ToolWeaponC>(SelIdx<IdxC>().Idx);
            ref var twLevel_sel = ref CellUnitTWE.UnitTW<LevelTC>(SelIdx<IdxC>().Idx);

            UIEntExtraTW.Image<ImageUIC>(TWTypes.Pick, LevelTypes.Second).SetActive(false);
            UIEntExtraTW.Image<ImageUIC>(TWTypes.Sword, LevelTypes.Second).SetActive(false);
            UIEntExtraTW.Image<ImageUIC>(TWTypes.Shield, LevelTypes.First).SetActive(false);
            UIEntExtraTW.Image<ImageUIC>(TWTypes.Shield, LevelTypes.Second).SetActive(false);

            if (tw_sel.HaveTW)
            {
                UIEntExtraTW.Image<ImageUIC>(tw_sel.ToolWeapon, twLevel_sel.Level).SetActive(true);
            }
        }
    }
}