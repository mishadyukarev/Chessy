﻿using Game.Common;
using UnityEngine;
using static Game.Game.DownToolWeaponUIEs;

namespace Game.Game
{
    struct DownToolWeaponUIS : IEcsRunSystem
    {
        public void Run()
        {
            var color = Button<ImageUIC>(ToolWeaponTypes.Pick).Color;
            color.a = 0;
            Button<ImageUIC>(ToolWeaponTypes.Pick).Color = color;

            color = Button<ImageUIC>(ToolWeaponTypes.Sword).Color;
            color.a = 0;
            Button<ImageUIC>(ToolWeaponTypes.Sword).Color = color;

            color = Button<ImageUIC>(ToolWeaponTypes.Shield).Color;
            color.a = 0;
            Button<ImageUIC>(ToolWeaponTypes.Shield).Color = color;


            var tw_sel = SelectedToolWeaponE.SelectedTW<ToolWeaponC>().ToolWeapon;
            var levTw_sel = SelectedToolWeaponE.SelectedTW<LevelTC>().Level;

            color = Button<ImageUIC>(tw_sel).Color;
            color.a = 1;
            Button<ImageUIC>(tw_sel).Color = color;


            Image<ImageUIC>(tw_sel, levTw_sel).SetActive(true);

            if (levTw_sel == LevelTypes.First)
            {
                Image<ImageUIC>(tw_sel, LevelTypes.Second).SetActive(false);
            }
            else
            {
                Image<ImageUIC>(tw_sel, LevelTypes.First).SetActive(false);
            }   

            Button<TextUIC>(ToolWeaponTypes.Pick).Text = InventorToolWeaponE.ToolWeapons<AmountC>(ToolWeaponTypes.Pick, LevelTypes.Second, Entities.WhoseMoveE.CurPlayerI).Amount.ToString();
            Button<TextUIC>(ToolWeaponTypes.Sword).Text = InventorToolWeaponE.ToolWeapons<AmountC>(ToolWeaponTypes.Sword, LevelTypes.Second, Entities.WhoseMoveE.CurPlayerI).Amount.ToString();
            Button<TextUIC>(ToolWeaponTypes.Shield).Text = InventorToolWeaponE.ToolWeapons<AmountC>(ToolWeaponTypes.Shield, SelectedToolWeaponE.SelectedTW<LevelTC>().Level, Entities.WhoseMoveE.CurPlayerI).Amount.ToString();
        }
    }
}
