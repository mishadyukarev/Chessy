using UnityEngine;

namespace Game.Game
{
    sealed class CellSupportVS : SystemViewAbstract, IEcsRunSystem
    {
        bool _isActive;
        Color _color;

        internal CellSupportVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var cellClick = ref E.CellClickTC;
            var curPlayer = E.CurPlayerI.Player;

            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {
                _isActive = false;
                _color = default;

                if (E.HaveFire(idx_0))
                {
                    if (cellClick.Is(CellClickTypes.UniqueAbility))
                    {
                        if (E.SelectedAbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                        {
                            _isActive = true;
                            _color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                        }
                    }
                }

                if (E.UnitTC(idx_0).HaveUnit)
                {
                    if (E.UnitEs(idx_0).ForPlayer(curPlayer).IsVisible)
                    {
                        if (E.UnitPlayerTC(idx_0).Is(curPlayer))
                        {
                            if (E.UnitTC(idx_0).Is(UnitTypes.Pawn))
                            {
                                if (cellClick.Is(CellClickTypes.GiveTakeTW))
                                {
                                    _isActive = true;
                                    _color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                                }
                            }
                        }

                        else
                        {
                            if (E.AdultForestC(idx_0).HaveAny)
                            {
                                if (cellClick.Is(CellClickTypes.UniqueAbility))
                                {
                                    if (E.SelectedAbilityTC.Is(AbilityTypes.StunElfemale))
                                    {
                                        _isActive = true;
                                        _color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                                    }
                                }
                            }
                        }
                    }
                }


                if (cellClick.Is(CellClickTypes.SetUnit))
                {
                    if (E.UnitEs(idx_0).ForPlayer(E.CurPlayerI.Player).CanSetUnitHere)
                    {
                        _isActive = true;
                        _color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }


                SupportCellVEs.Support<SpriteRendererVC>(idx_0).SetActive(_isActive);
                SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = _color;
            }

            SupportCellVEs.Support<SpriteRendererVC>(E.SelectedIdxC.Idx).Enable();
            SupportCellVEs.Support<SpriteRendererVC>(E.SelectedIdxC.Idx).Color = ColorsValues.Color(SupportCellVisionTypes.Selector);


            if (cellClick.Is(CellClickTypes.UniqueAbility))
            {
                if (cellClick.Is(CellClickTypes.UniqueAbility))
                {
                    if (E.SelectedAbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                    {
                        for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                        {
                            var idx_1 = E.CellEs(E.CenterCloudIdxC.Idx).AroundCellE(dirT).IdxC.Idx;

                            SupportCellVEs.Support<SpriteRendererVC>(idx_1).Enable();
                            SupportCellVEs.Support<SpriteRendererVC>(idx_1).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                        }
                    }
                }

                else if (E.SelectedAbilityTC.Is(AbilityTypes.FireArcher))
                {
                    foreach (var idx in E.UnitEs(E.SelectedIdxC.Idx).ForArson.Idxs)
                    {
                        SupportCellVEs.Support<SpriteRendererVC>(idx).Enable();
                        SupportCellVEs.Support<SpriteRendererVC>(idx).Color = ColorsValues.Color(SupportCellVisionTypes.FireSelector);
                    }
                }
            }


            else
            {
                var idxs = E.UnitEs(E.SelectedIdxC.Idx).ForShift.Idxs;

                foreach (var idx_0 in idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                }

                foreach (var idx_0 in E.UnitEs(E.SelectedIdxC.Idx).ForAttack(AttackTypes.Simple).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                }

                foreach (var idx_0 in E.UnitEs(E.SelectedIdxC.Idx).ForAttack(AttackTypes.Unique).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
                }
            }
        }
    }
}