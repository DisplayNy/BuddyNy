using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace Certainly_not_the_Blitz
{
    class Program
    {
        public const string ChampName = "Blitzcrank";

        public static Menu Menu,
            DrawMenu,
            ComboMenu;

        public static Spell.Skillshot Q;
        public static Spell.Active W, E, R;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComple;
            Loading.OnLoadingComplete += Game_OnStart;
            Drawing.OnDraw += Game_OnDraw;
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnStart(EventArgs args)
        {
            Chat.Print("Certainly not the Blitz");

            Menu = MainMenu.AddMenu("Blitz", "Blitz");
            Menu.AddSeparator();
            Menu.AddLabel("DisplayNy");

            DrawMenu = Menu.AddSubMenu("Draw", "Draw");
            DrawMenu.Add("drawDisable", new CheckBox("Disable all Draws", true));
            ComboMenu = Menu.AddSubMenu("Combo", "ComboBlitz");
            ComboMenu.Add("comboQ", new CheckBox("Use Q in combo", true));
            ComboMenu.Add("comboW", new CheckBox("Use W in combo", true));
            ComboMenu.Add("comboE", new CheckBox("Use E in combo", true));
            ComboMenu.Add("comboR", new CheckBox("Use R in combo", true));
        }

        private static void Game_OnDraw(EventArgs args)
        {
            if (Player.Instance.IsDead) return;

            if (DrawMenu["Draw_Q"].Cast<CheckBox>().CurrentValue && Q.IsReady())
                Circle.Draw(Color.Aquamarine, Q.Range, Player.Instance.Position);
            if (DrawMenu["Draw_R"].Cast<CheckBox>().CurrentValue && R.IsReady())
                Circle.Draw(Color.Beige, Q.Range, Player.Instance.Position);
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            var QTarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var WTarget = TargetSelector.GetTarget(1500, DamageType.Physical);
            var RTarget = TargetSelector.GetTarget(R.Range, DamageType.Magical);
            var combo = Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo;
            var flee = Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Flee;
        }
        private static void OnLoadingComple(EventArgs args)
        {
            if (Player.Instance.ChampionName != ChampName) return;

            Q = new Spell.Skillshot(SpellSlot.Q, (int)950f, SkillShotType.Linear, (int)0.25f, (int)1800f, (int)70f);
            W = new Spell.Active(SpellSlot.W, (int)700f);
            E = new Spell.Active(SpellSlot.E, (int)150f);
            R = new Spell.Active(SpellSlot.R, (int)540f);

        }
    }
}
