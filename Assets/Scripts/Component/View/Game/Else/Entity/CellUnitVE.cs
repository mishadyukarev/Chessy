using ECS;
using UnityEngine;

namespace Game.Game
{
    public struct CellUnitVEs
    {
        readonly Entity _main;
        readonly Entity _extra;

        public ref SpriteRendererVC UnitMainSR => ref _main.Get<SpriteRendererVC>();
        public ref SpriteRendererVC UnitExtraSR => ref _extra.Get<SpriteRendererVC>();

        public CellUnitVEs(GameObject cell, in EcsWorld gameW)
        {
            _main = gameW.NewEntity()
                .Add(new SpriteRendererVC(cell.transform.Find("MainUnit_SR").GetComponent<SpriteRenderer>()));

            _extra = gameW.NewEntity()
                .Add(new SpriteRendererVC(cell.transform.Find("ExtraUnit_SR").GetComponent<SpriteRenderer>()));
        }

        //public void SetAlpha(bool isVisible)
        //{
        //    if (isVisible) _main_SR.color = new Color(_main_SR.color.r, _main_SR.color.g, _main_SR.color.b, 1);
        //    else _main_SR.color = new Color(_main_SR.color.r, _main_SR.color.g, _main_SR.color.b, 0.8f);
        //}
    }
}