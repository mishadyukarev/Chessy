namespace Game.Game
{
    internal class UnitShiftMS : SystemAbstract, IEcsRunSystem
    {
        internal UnitShiftMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            var idx_from = E.RpcPoolEs.ShiftUnitME.FromIdxC.Idx;

            if (idx_from == 0) return;


            var idx_to = E.RpcPoolEs.ShiftUnitME.ToIdxC.Idx;
            var sender = E.RpcPoolEs.SenderC.Player;

            if (E.UnitEs(idx_from).ForShift.Contains(idx_to) && E.UnitPlayerTC(idx_from).Is(E.WhoseMove.Player))
            {
                E.UnitStepC(idx_from).Steps -= E.UnitEs(idx_from).NeedSteps(idx_to).Steps;

                E.UnitMainE(idx_from).ShiftTo.Idx = idx_to;

                E.RpcPoolEs.SoundToGeneral(sender, ClipTypes.ClickToTable);
            }

            E.RpcPoolEs.ShiftUnitME.FromIdxC.Idx = 0;
        }
    }
}