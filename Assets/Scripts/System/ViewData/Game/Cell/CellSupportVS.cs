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
            var curPlayer = Es.WhoseMovePlayerTC.CurPlayerI;

            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                _isActive = false;
                _color = default;

                if (Es.EffectEs(idx_0).FireE.HaveFireC.Have)
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
                    if (Es.UnitEs(idx_0).VisibleE(curPlayer).IsVisible)
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
                            if (Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
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
                    if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(Es.WhoseMovePlayerTC.CurPlayerI, idx_0).Can)
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
                    foreach (var idx in CellsForArsonArcherEs.Idxs<IdxsC>(Es.SelectedIdxC.Idx).Idxs)
                    {
                        SupportCellVEs.Support<SpriteRendererVC>(idx).Enable();
                        SupportCellVEs.Support<SpriteRendererVC>(idx).Color = ColorsValues.Color(SupportCellVisionTypes.FireSelector);
                    }
                }
            }


            else
            {
                var idxs = CellsForShiftUnitsEs.CellsForShift<IdxsC>(Es.WhoseMovePlayerTC.CurPlayerI, Es.SelectedIdxC.Idx).Idxs;

                foreach (var idx_0 in idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                }

                foreach (var idx_0 in CellsForAttackUnitsEs.CanAttack<IdxsC>(Es.SelectedIdxC.Idx, AttackTypes.Simple, Es.WhoseMovePlayerTC.CurPlayerI).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                }

                foreach (var idx_0 in CellsForAttackUnitsEs.CanAttack<IdxsC>(Es.SelectedIdxC.Idx, AttackTypes.Unique, Es.WhoseMovePlayerTC.CurPlayerI).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
                }
            }
        }
    }
}