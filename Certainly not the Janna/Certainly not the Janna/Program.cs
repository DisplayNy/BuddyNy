using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace Certainly_not_the_Janna
{
    class Program
    {
        private static AIHeroClient me = ObjectManager.Player;
        public static Menu Menu,
            DrawMenu,
            JannaConf,
            cJannaConf,
            prioConf,
            ComboMenu;
        public const string ChampName = "Janna";

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_OnStart;
            Drawing.OnDraw += Game_OnDraw;
            Game.OnUpdate += Game_OnUpdate;

            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
    {
    if (Player.Instance.ChampionName != ChampName)
    {
        return;
    }
 }

        private static void Game_OnUpdate(EventArgs args)
        {
            Spell.Skillshot Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 300);
            Spell.Targeted W = new Spell.Targeted(SpellSlot.W, 600);
            Spell.Targeted E = new Spell.Targeted(SpellSlot.E, 800);
            Spell.Active R = new Spell.Active(SpellSlot.R, 725);
            //Plus Janna
        }

        private static void Game_OnDraw(EventArgs args)
        {
            
        }

        private static void Game_OnStart(EventArgs args)
        {
            Spell.Skillshot q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 750);
            Spell.Targeted w = new Spell.Targeted(SpellSlot.W, 600);
            Spell.Targeted e = new Spell.Targeted(SpellSlot.E, 800);
            Spell.Skillshot r = new Spell.Skillshot(SpellSlot.R, 725, SkillShotType.Circular, 500);

            Menu = MainMenu.AddMenu("cJanna", "cJanna");
            Menu.AddSeparator();
            Menu.AddLabel("Not Janna");

            DrawMenu = Menu.AddSubMenu("Draw", "DrawJanna");
            DrawMenu.Add("drawDisable", new CheckBox("Disable Draws", true));
            ComboMenu = Menu.AddSubMenu("Combo", "Combo");
            ComboMenu.Add("comboQ", new CheckBox("Use Q in combo", true));
            ComboMenu.Add("comboW", new CheckBox("Use W in combo", true));
            ComboMenu.Add("comboE", new CheckBox("Use E in combo", true));
            ComboMenu.Add("comboR", new CheckBox("Use R in combo", true));

            JannaConf = Menu.AddSubMenu("Shielding", "JannaEConf");
            JannaConf.Add("eSkillShots", new CheckBox("E for skillshots"));
            JannaConf.Add("eDangerLvl", new Slider("If at dangerlevel", 2, 1, 5));
            JannaConf.Add("eTargeted", new CheckBox("E for targeted spells"));
            JannaConf.Add("eDmgProc", new Slider("If % dmg", 15));

            cJannaConf = Menu.AddSubMenu("Ultimate", "JannaRConf");
            cJannaConf.Add("rInterrupt", new CheckBox("Use R to interrupt dangerous spells"));
            cJannaConf.Add("rInsec", new KeyBind("Insec to ally", false, KeyBind.BindTypes.HoldActive));
            cJannaConf.AddLabel("Target has to be selected");
            cJannaConf.Add("flashRange", new Slider("Flash Range", 450, 425, 600));
            cJannaConf.Add("extraDist", new Slider("Extra Insec Dist", 150, 50, 300));
            cJannaConf.Add("maxOwnDistToEnemy", new Slider("Max own dist to enemy", 500, 100, 800));

            prioConf = Menu.AddSubMenu("Janna", "JannaConf");
            foreach (AIHeroClient ally in EntityManager.Heroes.Allies)
            {
                prioConf.Add(ally.ChampionName,
                    new Slider(ally.ChampionName, ally.IsMe ? 1 : TargetSelector.GetPriority(ally), 1, 5));
            }

        }

        }
    }

