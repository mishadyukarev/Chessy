using ECS;
using UnityEngine;

namespace Game.Game
{
    public struct CellUnitVEs
    {
        static Entity[] _main;
        static Entity[] _extra;

        public static ref C UnitMain<C>(in byte idx) where C : struct, IUnitCellV => ref _main[idx].Get<C>();
        public static ref C UnitExtra<C>(in byte idx) where C : struct, IUnitCellV => ref _main[idx].Get<C>();

        public CellUnitVEs(in EcsWorld gameW, GameObject[] cells)
        {
            _main = new Entity[CellValues.ALL_CELLS_AMOUNT];
            _extra = new Entity[CellValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < _main.Length; idx++)
            {
                _main[idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(cells[idx].transform.Find("MainUnit_SR").GetComponent<SpriteRenderer>()));

                _extra[idx] = gameW.NewEntity()
                    .Add(new SpriteRendererVC(cells[idx].transform.Find("ExtraUnit_SR").GetComponent<SpriteRenderer>()));
            }
        }

        //public void SetAlpha(bool isVisible)
        //{
        //    if (isVisible) _main_SR.color = new Color(_main_SR.color.r, _main_SR.color.g, _main_SR.color.b, 1);
        //    else _main_SR.color = new Color(_main_SR.color.r, _main_SR.color.g, _main_SR.color.b, 0.8f);
        //}
    }
    public interface IUnitCellV { }
}