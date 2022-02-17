using UnityEngine;

namespace Game.Game
{
    sealed class CellSupportVS : SystemViewAbstract, IEcsRunSystem
    {
        bool _isActive;
        Color _color;

        internal CellSupportVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var cellClick = ref Es.CellClickTC;
            var curPlayer = Es.CurPlayerI.Player;

            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                _isActive = false;
                _color = default;

                if (Es.HaveFire(idx_0))
                {
                    if (cellClick.Is(CellClickTypes.UniqueAbility))
                    {
                        if (Es.SelAbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                        {
                            _isActive = true;
                            _color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                        }
                    }
                }

                if (Es.UnitTC(idx_0).HaveUnit)
                {
                    if (Es.UnitEs(idx_0).ForPlayer(curPlayer).IsVisibleC)
                    {
                        if (Es.UnitPlayerTC(idx_0).Is(curPlayer))
                        {
                            if (Es.UnitTC(idx_0).Is(UnitTypes.Pawn))
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
                            if (Es.AdultForestC(idx_0).HaveAny)
                            {
                                if (cellClick.Is(CellClickTypes.UniqueAbility))
                                {
                                    if (Es.SelAbilityTC.Is(AbilityTypes.StunElfemale))
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
                    if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(Es.CurPlayerI.Player, idx_0).Can)
                    {
                        _isActive = true;
                        _color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }


                SupportCellVEs.Support<SpriteRendererVC>(idx_0).SetActive(_isActive);
                SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = _color;
            }

            SupportCellVEs.Support<SpriteRendererVC>(Es.SelectedIdxC.Idx).Enable();
            SupportCellVEs.Support<SpriteRendererVC>(Es.SelectedIdxC.Idx).Color = ColorsValues.Color(SupportCellVisionTypes.Selector);


            if (cellClick.Is(CellClickTypes.UniqueAbility))
            {
                if (cellClick.Is(CellClickTypes.UniqueAbility))
                {
                    if (Es.SelAbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                    {
                        CellWorker.TryGetIdxAround(Es.CenterCloudIdxC.Idx, out var dirs);

                        foreach (var item in dirs)
                        {
                            SupportCellVEs.Support<SpriteRendererVC>(item.Value).Enable();
                            SupportCellVEs.Support<SpriteRendererVC>(item.Value).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                        }
                    }
                }

                else if (Es.SelAbilityTC.Is(AbilityTypes.FireArcher))
                {
                    foreach (var idx in Es.UnitEs(Es.SelectedIdxC.Idx).ForArson.Idxs)
                    {
                        SupportCellVEs.Support<SpriteRendererVC>(idx).Enable();
                        SupportCellVEs.Support<SpriteRendererVC>(idx).Color = ColorsValues.Color(SupportCellVisionTypes.FireSelector);
                    }
                }
            }


            else
            {
                var idxs = Es.UnitEs(Es.SelectedIdxC.Idx).ForPlayer(Es.CurPlayerI.Player).ForShift.Idxs;

                foreach (var idx_0 in idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                }

                foreach (var idx_0 in Es.UnitEs(Es.SelectedIdxC.Idx).ForPlayer(Es.CurPlayerI.Player).ForAttack(AttackTypes.Simple).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                }

                foreach (var idx_0 in Es.UnitEs(Es.SelectedIdxC.Idx).ForPlayer(Es.CurPlayerI.Player).ForAttack(AttackTypes.Unique).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
                }
            }
        }
    }
}