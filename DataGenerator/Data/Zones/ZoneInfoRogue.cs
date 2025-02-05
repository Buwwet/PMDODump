﻿using RogueEssence.Content;
using RogueElements;
using RogueEssence.LevelGen;
using RogueEssence.Dungeon;
using RogueEssence.Ground;
using RogueEssence.Script;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using RogueEssence;
using RogueEssence.Data;
using PMDC.Dungeon;
using PMDC.LevelGen;
using PMDC;
using PMDC.Data;

namespace DataGenerator.Data
{
    public partial class ZoneInfo
    {

        #region GUILDMASTER TRAIL
        static void FillGuildmaster(ZoneData zone)
        {
            zone.Name = new LocalText("Guildmaster Trail");
            zone.Level = 5;
            zone.LevelCap = true;
            zone.BagRestrict = 4;
            zone.Rescues = 2;
            zone.Rogue = RogueStatus.AllTransfer;

            int max_floors = 30;

            LayeredSegment floorSegment = new LayeredSegment();
            floorSegment.IsRelevant = true;
            floorSegment.ZoneSteps.Add(new SaveVarsZoneStep(PR_EXITS_RESCUE));
            floorSegment.ZoneSteps.Add(new FloorNameIDZoneStep(PR_FLOOR_DATA, new LocalText("Guildmaster Trail\n{0}F")));

            //money
            MoneySpawnZoneStep moneySpawnZoneStep = new MoneySpawnZoneStep(PR_RESPAWN_MONEY, new RandRange(63, 72), new RandRange(21, 24));
            moneySpawnZoneStep.ModStates.Add(new FlagType(typeof(CoinModGenState)));
            floorSegment.ZoneSteps.Add(moneySpawnZoneStep);

            //items
            ItemSpawnZoneStep itemSpawnZoneStep = new ItemSpawnZoneStep();
            itemSpawnZoneStep.Priority = PR_RESPAWN_ITEM;

            //necesities
            CategorySpawn<InvItem> necessities = new CategorySpawn<InvItem>();
            necessities.SpawnRates.SetRange(14, new IntRange(0, 30));
            itemSpawnZoneStep.Spawns.Add("necessities", necessities);

            necessities.Spawns.Add(new InvItem("berry_leppa"), new IntRange(0, 30), 30);//Leppa
            necessities.Spawns.Add(new InvItem("berry_oran"), new IntRange(0, 10), 70);//Oran
            necessities.Spawns.Add(new InvItem("berry_oran"), new IntRange(10, 30), 40);//Oran
            necessities.Spawns.Add(new InvItem("berry_sitrus"), new IntRange(10, 30), 30);//Sitrus
            necessities.Spawns.Add(new InvItem("food_apple"), new IntRange(0, 15), 40);//Apple
            necessities.Spawns.Add(new InvItem("food_grimy"), new IntRange(5, 30), 30);//Grimy Food
            necessities.Spawns.Add(new InvItem("berry_lum"), new IntRange(2, 30), 50);//Lum berry

            necessities.Spawns.Add(new InvItem("seed_reviver"), new IntRange(0, 30), 30);//reviver seed
            necessities.Spawns.Add(new InvItem("seed_reviver", true), new IntRange(0, 30), 15);//reviver seed
            necessities.Spawns.Add(new InvItem("machine_recall_box"), new IntRange(4, 30), 30);//Link Box


            //snacks
            CategorySpawn<InvItem> snacks = new CategorySpawn<InvItem>();
            snacks.SpawnRates.SetRange(10, new IntRange(0, 30));
            itemSpawnZoneStep.Spawns.Add("snacks", snacks);

            foreach(string key in IteratePinchBerries())
                snacks.Spawns.Add(new InvItem(key), new IntRange(0, 30), 3);
            snacks.Spawns.Add(new InvItem("berry_enigma"), new IntRange(0, 30), 4);//enigma berry

            snacks.Spawns.Add(new InvItem("berry_jaboca"), new IntRange(0, 30), 5);//Jaboca
            snacks.Spawns.Add(new InvItem("berry_rowap"), new IntRange(0, 30), 5);//Rowap

            foreach (string key in IterateTypeBerries())
                snacks.Spawns.Add(new InvItem(key), new IntRange(6, 30), 1);

            snacks.Spawns.Add(new InvItem("seed_blast"), new IntRange(0, 30), 20);//blast seed
            snacks.Spawns.Add(new InvItem("seed_warp"), new IntRange(0, 30), 10);//warp seed
            snacks.Spawns.Add(new InvItem("seed_decoy"), new IntRange(0, 30), 10);//decoy seed
            snacks.Spawns.Add(new InvItem("seed_sleep"), new IntRange(0, 30), 10);//sleep seed
            snacks.Spawns.Add(new InvItem("seed_blinker"), new IntRange(0, 30), 10);//blinker seed
            snacks.Spawns.Add(new InvItem("seed_last_chance"), new IntRange(0, 30), 5);//last-chance seed
            snacks.Spawns.Add(new InvItem("seed_doom"), new IntRange(0, 30), 5);//doom seed
            snacks.Spawns.Add(new InvItem("seed_ban"), new IntRange(0, 30), 10);//ban seed
            snacks.Spawns.Add(new InvItem("seed_pure"), new IntRange(0, 30), 4);//pure seed
            snacks.Spawns.Add(new InvItem("seed_pure", true), new IntRange(0, 30), 4);//pure seed
            snacks.Spawns.Add(new InvItem("seed_ice"), new IntRange(0, 30), 10);//ice seed
            snacks.Spawns.Add(new InvItem("seed_vile"), new IntRange(0, 30), 10);//vile seed

            snacks.Spawns.Add(new InvItem("herb_power"), new IntRange(0, 30), 10);//power herb
            snacks.Spawns.Add(new InvItem("herb_mental"), new IntRange(0, 30), 5);//mental herb
            snacks.Spawns.Add(new InvItem("herb_white"), new IntRange(0, 30), 50);//white herb


            //boosters
            CategorySpawn<InvItem> boosters = new CategorySpawn<InvItem>();
            boosters.SpawnRates.SetRange(7, new IntRange(0, 30));
            itemSpawnZoneStep.Spawns.Add("boosters", boosters);

            foreach (string key in IterateGummis())
                boosters.Spawns.Add(new InvItem(key), new IntRange(0, 30), 1);

            IntRange range = new IntRange(3, 30);

            boosters.Spawns.Add(new InvItem("boost_protein"), range, 2);//protein
            boosters.Spawns.Add(new InvItem("boost_iron"), range, 2);//iron
            boosters.Spawns.Add(new InvItem("boost_calcium"), range, 2);//calcium
            boosters.Spawns.Add(new InvItem("boost_zinc"), range, 2);//zinc
            boosters.Spawns.Add(new InvItem("boost_carbos"), range, 2);//carbos
            boosters.Spawns.Add(new InvItem("boost_hp_up"), range, 2);//hp up

            //throwable
            CategorySpawn<InvItem> ammo = new CategorySpawn<InvItem>();
            ammo.SpawnRates.SetRange(12, new IntRange(0, 30));
            itemSpawnZoneStep.Spawns.Add("ammo", ammo);

            range = new IntRange(0, 30);
            {
                ammo.Spawns.Add(new InvItem("ammo_stick", false, 4), range, 10);//stick
                ammo.Spawns.Add(new InvItem("ammo_cacnea_spike", false, 3), range, 10);//cacnea spike
                ammo.Spawns.Add(new InvItem("wand_path", false, 2), range, 10);//path wand
                ammo.Spawns.Add(new InvItem("wand_fear", false, 4), range, 10);//fear wand
                ammo.Spawns.Add(new InvItem("wand_switcher", false, 4), range, 10);//switcher wand
                ammo.Spawns.Add(new InvItem("wand_whirlwind", false, 4), range, 10);//whirlwind wand
                ammo.Spawns.Add(new InvItem("wand_lure", false, 4), range, 10);//lure wand
                ammo.Spawns.Add(new InvItem("wand_slow", false, 4), range, 10);//slow wand
                ammo.Spawns.Add(new InvItem("wand_pounce", false, 4), range, 10);//pounce wand
                ammo.Spawns.Add(new InvItem("wand_warp", false, 2), range, 10);//warp wand
                ammo.Spawns.Add(new InvItem("wand_topsy_turvy", false, 4), range, 10);//topsy-turvy wand
                ammo.Spawns.Add(new InvItem("wand_lob", false, 4), range, 10);//lob wand
                ammo.Spawns.Add(new InvItem("wand_purge", false, 4), range, 10);//purge wand

                ammo.Spawns.Add(new InvItem("ammo_iron_thorn", false, 3), range, 10);//iron thorn
                ammo.Spawns.Add(new InvItem("ammo_silver_spike", false, 3), range, 10);//silver spike
                ammo.Spawns.Add(new InvItem("ammo_gravelerock", false, 3), range, 10);//Gravelerock

                ammo.Spawns.Add(new InvItem("ammo_corsola_twig", false, 3), range, 10);//corsola spike
                ammo.Spawns.Add(new InvItem("ammo_rare_fossil", false, 3), range, 10);//Rare fossil

                ammo.Spawns.Add(new InvItem("ammo_geo_pebble", false, 3), range, 10);//Geo Pebble
            }


            //special items
            CategorySpawn<InvItem> special = new CategorySpawn<InvItem>();
            special.SpawnRates.SetRange(7, new IntRange(0, 30));
            itemSpawnZoneStep.Spawns.Add("special", special);

            {
                range = new IntRange(0, 15);
                int rate = 2;

                special.Spawns.Add(new InvItem("apricorn_blue"), range, rate);//blue apricorns
                special.Spawns.Add(new InvItem("apricorn_green"), range, rate);//green apricorns
                special.Spawns.Add(new InvItem("apricorn_brown"), range, rate);//brown apricorns
                special.Spawns.Add(new InvItem("apricorn_purple"), range, rate);//purple apricorns
                special.Spawns.Add(new InvItem("apricorn_red"), range, rate);//red apricorns
                special.Spawns.Add(new InvItem("apricorn_white"), range, rate);//white apricorns
                special.Spawns.Add(new InvItem("apricorn_yellow"), range, rate);//yellow apricorns
                special.Spawns.Add(new InvItem("apricorn_black"), range, rate);//black apricorns

                range = new IntRange(15, 25);
                rate = 1;

                special.Spawns.Add(new InvItem("apricorn_plain", true), range, rate);//Plain Apricorn
                special.Spawns.Add(new InvItem("apricorn_blue", true), range, rate);//blue apricorns
                special.Spawns.Add(new InvItem("apricorn_green", true), range, rate);//green apricorns
                special.Spawns.Add(new InvItem("apricorn_brown", true), range, rate);//brown apricorns
                special.Spawns.Add(new InvItem("apricorn_purple", true), range, rate);//purple apricorns
                special.Spawns.Add(new InvItem("apricorn_red", true), range, rate);//red apricorns
                special.Spawns.Add(new InvItem("apricorn_white", true), range, rate);//white apricorns
                special.Spawns.Add(new InvItem("apricorn_yellow", true), range, rate);//yellow apricorns
                special.Spawns.Add(new InvItem("apricorn_black", true), range, rate);//black apricorns

            }

            special.Spawns.Add(new InvItem("key", false, 1), new IntRange(0, 25), 40);//Key
            special.Spawns.Add(new InvItem("machine_assembly_box"), new IntRange(9, 30), 35);//Assembly Box
                                                                          //special.Spawns.Add(new InvItem("machine_storage_box"), 10);//Storage Box
            special.Spawns.Add(new InvItem("machine_ability_capsule"), new IntRange(13, 30), 15); // Ability Capsule



            //orbs
            CategorySpawn<InvItem> orbs = new CategorySpawn<InvItem>();
            orbs.SpawnRates.SetRange(10, new IntRange(0, 30));
            itemSpawnZoneStep.Spawns.Add("orbs", orbs);

            {
                range = new IntRange(3, 30);
                orbs.Spawns.Add(new InvItem("orb_one_room"), range, 7);//One-Room Orb
                orbs.Spawns.Add(new InvItem("orb_fill_in"), range, 7);//Fill-In Orb
                orbs.Spawns.Add(new InvItem("orb_one_room", true), range, 3);//One-Room Orb
                orbs.Spawns.Add(new InvItem("orb_fill_in", true), range, 3);//Fill-In Orb
            }

            {
                range = new IntRange(0, 25);
                orbs.Spawns.Add(new InvItem("orb_petrify"), range, 10);//Petrify
                orbs.Spawns.Add(new InvItem("orb_halving"), range, 10);//Halving
                orbs.Spawns.Add(new InvItem("orb_slumber"), range, 8);//Slumber Orb
                orbs.Spawns.Add(new InvItem("orb_slow"), range, 8);//Slow
                orbs.Spawns.Add(new InvItem("orb_totter"), range, 8);//Totter
                orbs.Spawns.Add(new InvItem("orb_spurn"), range, 5);//Spurn
                orbs.Spawns.Add(new InvItem("orb_stayaway"), range, 3);//Stayaway
                orbs.Spawns.Add(new InvItem("orb_pierce"), range, 8);//Pierce
                orbs.Spawns.Add(new InvItem("orb_slumber", true), range, 3);//Slumber Orb
                orbs.Spawns.Add(new InvItem("orb_slow", true), range, 3);//Slow
                orbs.Spawns.Add(new InvItem("orb_totter", true), range, 3);//Totter
                orbs.Spawns.Add(new InvItem("orb_spurn", true), range, 2);//Spurn
                orbs.Spawns.Add(new InvItem("orb_stayaway", true), range, 3);//Stayaway
                orbs.Spawns.Add(new InvItem("orb_pierce", true), range, 3);//Pierce
            }

            orbs.Spawns.Add(new InvItem("orb_cleanse"), new IntRange(2, 25), 7);//Cleanse

            {
                range = new IntRange(6, 25);
                orbs.Spawns.Add(new InvItem("orb_all_aim"), range, 10);//All-Aim Orb
                orbs.Spawns.Add(new InvItem("orb_trap_see"), range, 10);//Trap-See
                orbs.Spawns.Add(new InvItem("orb_trapbust"), range, 10);//Trapbust
                orbs.Spawns.Add(new InvItem("orb_foe_hold"), range, 10);//Foe-Hold
                orbs.Spawns.Add(new InvItem("orb_mobile"), range, 10);//Mobile
                orbs.Spawns.Add(new InvItem("orb_rollcall"), range, 10);//Roll Call
                orbs.Spawns.Add(new InvItem("orb_mug"), range, 10);//Mug
                orbs.Spawns.Add(new InvItem("orb_mirror"), range, 10);//Mirror
            }

            {
                range = new IntRange(10, 25);
                orbs.Spawns.Add(new InvItem("orb_weather"), range, 10);//Weather Orb
                orbs.Spawns.Add(new InvItem("orb_foe_seal"), range, 10);//Foe-Seal
                orbs.Spawns.Add(new InvItem("orb_freeze"), range, 10);//Freeze
                orbs.Spawns.Add(new InvItem("orb_devolve"), range, 10);//Devolve
                orbs.Spawns.Add(new InvItem("orb_nullify"), range, 10);//Nullify
            }

            {
                range = new IntRange(0, 20);
                orbs.Spawns.Add(new InvItem("orb_rebound"), range, 10);//Rebound
                orbs.Spawns.Add(new InvItem("orb_all_protect"), range, 5);//All Protect
                orbs.Spawns.Add(new InvItem("orb_luminous"), range, 9);//Luminous
                orbs.Spawns.Add(new InvItem("orb_trawl"), range, 9);//Trawl
                orbs.Spawns.Add(new InvItem("orb_scanner"), range, 9);//Scanner
                orbs.Spawns.Add(new InvItem("orb_all_protect", true), range, 5);//All Protect
                orbs.Spawns.Add(new InvItem("orb_luminous", true), range, 2);//Luminous
                orbs.Spawns.Add(new InvItem("orb_trawl", true), range, 2);//Trawl
            }

            //held items
            CategorySpawn<InvItem> heldItems = new CategorySpawn<InvItem>();
            heldItems.SpawnRates.SetRange(2, new IntRange(0, 30));
            itemSpawnZoneStep.Spawns.Add("held", heldItems);

            foreach (string key in IterateTypeBoosters())
                heldItems.Spawns.Add(new InvItem(key), new IntRange(0, 30), 1);
            foreach (string key in IterateTypePlates())
                heldItems.Spawns.Add(new InvItem(key), new IntRange(0, 30), 1);

            heldItems.Spawns.Add(new InvItem("held_mobile_scarf"), new IntRange(0, 20), 2);//Mobile Scarf
            heldItems.Spawns.Add(new InvItem("held_pass_scarf"), new IntRange(0, 20), 2);//Pass Scarf


            heldItems.Spawns.Add(new InvItem("held_cover_band"), new IntRange(0, 25), 2);//Cover Band
            heldItems.Spawns.Add(new InvItem("held_reunion_cape"), new IntRange(0, 25), 1);//Reunion Cape
            heldItems.Spawns.Add(new InvItem("held_reunion_cape", true), new IntRange(0, 25), 1);//Reunion Cape

            heldItems.Spawns.Add(new InvItem("held_trap_scarf"), new IntRange(0, 15), 2);//Trap Scarf
            heldItems.Spawns.Add(new InvItem("held_trap_scarf", true), new IntRange(0, 15), 1);//Trap Scarf

            heldItems.Spawns.Add(new InvItem("held_grip_claw"), new IntRange(0, 30), 2);//Grip Claw

            range = new IntRange(0, 20);
            heldItems.Spawns.Add(new InvItem("held_twist_band"), range, 2);//Twist Band
            heldItems.Spawns.Add(new InvItem("held_metronome"), range, 1);//Metronome
            heldItems.Spawns.Add(new InvItem("held_twist_band", true), range, 1);//Twist Band
            heldItems.Spawns.Add(new InvItem("held_metronome", true), range, 1);//Metronome
            heldItems.Spawns.Add(new InvItem("held_shell_bell"), range, 1);//Shell Bell
            heldItems.Spawns.Add(new InvItem("held_scope_lens"), range, 1);//Scope Lens
            heldItems.Spawns.Add(new InvItem("held_power_band"), range, 2);//Power Band
            heldItems.Spawns.Add(new InvItem("held_special_band"), range, 2);//Special Band
            heldItems.Spawns.Add(new InvItem("held_defense_scarf"), range, 2);//Defense Scarf
            heldItems.Spawns.Add(new InvItem("held_zinc_band"), range, 2);//Zinc Band
            heldItems.Spawns.Add(new InvItem("held_wide_lens", true), range, 2);//Wide Lens
            heldItems.Spawns.Add(new InvItem("held_pierce_band", true), range, 2);//Pierce Band
            heldItems.Spawns.Add(new InvItem("held_shell_bell", true), range, 1);//Shell Bell
            heldItems.Spawns.Add(new InvItem("held_scope_lens", true), range, 1);//Scope Lens

            heldItems.Spawns.Add(new InvItem("held_shed_shell"), new IntRange(0, 20), 2);//Shed Shell
            heldItems.Spawns.Add(new InvItem("held_shed_shell", true), new IntRange(0, 20), 1);//Shed Shell

            heldItems.Spawns.Add(new InvItem("held_x_ray_specs"), new IntRange(0, 30), 2);//X-Ray Specs
            heldItems.Spawns.Add(new InvItem("held_x_ray_specs", true), new IntRange(0, 30), 1);//X-Ray Specs

            heldItems.Spawns.Add(new InvItem("held_goggle_specs"), new IntRange(0, 30), 2);//Goggle Specs
            heldItems.Spawns.Add(new InvItem("held_goggle_specs", true), new IntRange(0, 30), 1);//Goggle Specs

            heldItems.Spawns.Add(new InvItem("held_big_root"), new IntRange(0, 15), 2);//Big Root
            heldItems.Spawns.Add(new InvItem("held_big_root", true), new IntRange(0, 15), 1);//Big Root

            int stickRate = 2;
            range = new IntRange(0, 15);

            heldItems.Spawns.Add(new InvItem("held_weather_rock"), range, stickRate);//Weather Rock
            heldItems.Spawns.Add(new InvItem("held_expert_belt"), range, stickRate);//Expert Belt
            heldItems.Spawns.Add(new InvItem("held_choice_scarf"), range, stickRate);//Choice Scarf
            heldItems.Spawns.Add(new InvItem("held_choice_specs"), range, stickRate);//Choice Specs
            heldItems.Spawns.Add(new InvItem("held_choice_band"), range, stickRate);//Choice Band
            heldItems.Spawns.Add(new InvItem("held_assault_vest"), range, stickRate);//Assault Vest
            heldItems.Spawns.Add(new InvItem("held_life_orb"), range, stickRate);//Life Orb
            heldItems.Spawns.Add(new InvItem("held_heal_ribbon"), range, stickRate);//Heal Ribbon

            stickRate = 1;
            range = new IntRange(15, 30);

            heldItems.Spawns.Add(new InvItem("held_weather_rock"), range, stickRate);//Weather Rock
            heldItems.Spawns.Add(new InvItem("held_expert_belt"), range, stickRate);//Expert Belt
            heldItems.Spawns.Add(new InvItem("held_choice_scarf"), range, stickRate);//Choice Scarf
            heldItems.Spawns.Add(new InvItem("held_choice_specs"), range, stickRate);//Choice Specs
            heldItems.Spawns.Add(new InvItem("held_choice_band"), range, stickRate);//Choice Band
            heldItems.Spawns.Add(new InvItem("held_assault_vest"), range, stickRate);//Assault Vest
            heldItems.Spawns.Add(new InvItem("held_life_orb"), range, stickRate);//Life Orb
            heldItems.Spawns.Add(new InvItem("held_heal_ribbon"), range, stickRate);//Heal Ribbon


            heldItems.Spawns.Add(new InvItem("held_warp_scarf"), new IntRange(0, 30), 1);//Warp Scarf
            heldItems.Spawns.Add(new InvItem("held_warp_scarf", true), new IntRange(0, 30), 1);//Warp Scarf


            range = new IntRange(8, 30);
            heldItems.Spawns.Add(new InvItem("held_toxic_orb"), range, 1);//Toxic Orb
            heldItems.Spawns.Add(new InvItem("held_flame_orb"), range, 1);//Flame Orb
            heldItems.Spawns.Add(new InvItem("held_sticky_barb"), range, 10);//Sticky Barb
            heldItems.Spawns.Add(new InvItem("held_ring_target"), range, 1);//Ring Target

            //machines
            CategorySpawn<InvItem> machines = new CategorySpawn<InvItem>();
            machines.SpawnRates.SetRange(7, new IntRange(0, 30));
            itemSpawnZoneStep.Spawns.Add("tms", machines);

            range = new IntRange(0, 30);
            machines.Spawns.Add(new InvItem("tm_echoed_voice"), range, 2);//TM Echoed Voice
            machines.Spawns.Add(new InvItem("tm_double_team"), range, 2);//TM Double Team
            machines.Spawns.Add(new InvItem("tm_toxic"), range, 2);//TM Toxic
            machines.Spawns.Add(new InvItem("tm_will_o_wisp"), range, 2);//TM Will-o-Wisp
            machines.Spawns.Add(new InvItem("tm_dragon_tail"), range, 2);//TM Dragon Tail
            machines.Spawns.Add(new InvItem("tm_protect"), range, 2);//TM Protect
            machines.Spawns.Add(new InvItem("tm_defog"), range, 2);//TM Defog
            machines.Spawns.Add(new InvItem("tm_roar"), range, 2);//TM Roar
            machines.Spawns.Add(new InvItem("tm_swagger"), range, 2);//TM Swagger
            machines.Spawns.Add(new InvItem("tm_dive"), range, 2);//TM Dive
            machines.Spawns.Add(new InvItem("tm_fly"), range, 2);//TM Fly
            machines.Spawns.Add(new InvItem("tm_facade"), range, 2);//TM Facade
            machines.Spawns.Add(new InvItem("tm_rock_climb"), range, 2);//TM Rock Climb
            machines.Spawns.Add(new InvItem("tm_waterfall"), range, 2);//TM Waterfall
            machines.Spawns.Add(new InvItem("tm_smack_down"), range, 2);//TM Smack Down
            machines.Spawns.Add(new InvItem("tm_flame_charge"), range, 2);//TM Flame Charge
            machines.Spawns.Add(new InvItem("tm_low_sweep"), range, 2);//TM Low Sweep
            machines.Spawns.Add(new InvItem("tm_charge_beam"), range, 2);//TM Charge Beam
            machines.Spawns.Add(new InvItem("tm_payback"), range, 2);//TM Payback
            machines.Spawns.Add(new InvItem("tm_wild_charge"), range, 2);//TM Wild Charge
            machines.Spawns.Add(new InvItem("tm_hone_claws"), range, 2);//TM Hone Claws
            machines.Spawns.Add(new InvItem("tm_telekinesis"), range, 2);//TM Telekinesis
            machines.Spawns.Add(new InvItem("tm_giga_impact"), range, 2);//TM Giga Impact
            machines.Spawns.Add(new InvItem("tm_rock_polish"), range, 2);//TM Rock Polish
            machines.Spawns.Add(new InvItem("tm_dig"), range, 2);//TM Dig
            machines.Spawns.Add(new InvItem("tm_gyro_ball"), range, 2);//TM Gyro Ball
            machines.Spawns.Add(new InvItem("tm_substitute"), range, 2);//TM Substitute
            machines.Spawns.Add(new InvItem("tm_trick_room"), range, 2);//TM Trick Room
            machines.Spawns.Add(new InvItem("tm_safeguard"), range, 2);//TM Safeguard
            machines.Spawns.Add(new InvItem("tm_venoshock"), range, 2);//TM Venoshock
            machines.Spawns.Add(new InvItem("tm_work_up"), range, 2);//TM Work Up
            machines.Spawns.Add(new InvItem("tm_scald"), range, 2);//TM Scald
            machines.Spawns.Add(new InvItem("tm_explosion"), range, 2);//TM Explosion
            machines.Spawns.Add(new InvItem("tm_u_turn"), range, 2);//TM U-turn
            machines.Spawns.Add(new InvItem("tm_thunder_wave"), range, 2);//TM Thunder Wave
            machines.Spawns.Add(new InvItem("tm_return"), range, 2);//TM Return
            machines.Spawns.Add(new InvItem("tm_pluck"), range, 2);//TM Pluck
            machines.Spawns.Add(new InvItem("tm_frustration"), range, 2);//TM Frustration
            machines.Spawns.Add(new InvItem("tm_thief"), range, 2);//TM Thief
            machines.Spawns.Add(new InvItem("tm_acrobatics"), range, 2);//TM Acrobatics
            machines.Spawns.Add(new InvItem("tm_false_swipe"), range, 2);//TM False Swipe
            machines.Spawns.Add(new InvItem("tm_fling"), range, 2);//TM Fling
            machines.Spawns.Add(new InvItem("tm_captivate"), range, 2);//TM Captivate
            machines.Spawns.Add(new InvItem("tm_roost"), range, 2);//TM Roost
            machines.Spawns.Add(new InvItem("tm_infestation"), range, 2);//TM Infestation
            machines.Spawns.Add(new InvItem("tm_drain_punch"), range, 2);//TM Drain Punch
            machines.Spawns.Add(new InvItem("tm_water_pulse"), range, 2);//TM Water Pulse
            machines.Spawns.Add(new InvItem("tm_giga_drain"), range, 2);//TM Giga Drain
            machines.Spawns.Add(new InvItem("tm_shock_wave"), range, 2);//TM Shock Wave
            machines.Spawns.Add(new InvItem("tm_volt_switch"), range, 2);//TM Volt Switch
            machines.Spawns.Add(new InvItem("tm_steel_wing"), range, 2);//TM Steel Wing
            machines.Spawns.Add(new InvItem("tm_psyshock"), range, 2);//TM Psyshock
            machines.Spawns.Add(new InvItem("tm_bulldoze"), range, 2);//TM Bulldoze
            machines.Spawns.Add(new InvItem("tm_poison_jab"), range, 2);//TM Poison Jab
            machines.Spawns.Add(new InvItem("tm_dream_eater"), range, 2);//TM Dream Eater
            machines.Spawns.Add(new InvItem("tm_thunder"), range, 2);//TM Thunder
            machines.Spawns.Add(new InvItem("tm_x_scissor"), range, 2);//TM X-Scissor
            machines.Spawns.Add(new InvItem("tm_retaliate"), range, 2);//TM Retaliate
            machines.Spawns.Add(new InvItem("tm_reflect"), range, 2);//TM Reflect
            machines.Spawns.Add(new InvItem("tm_quash"), range, 2);//TM Quash
            machines.Spawns.Add(new InvItem("tm_round"), range, 2);//TM Round
            machines.Spawns.Add(new InvItem("tm_aerial_ace"), range, 2);//TM Aerial Ace
            machines.Spawns.Add(new InvItem("tm_incinerate"), range, 2);//TM Incinerate
            machines.Spawns.Add(new InvItem("tm_struggle_bug"), range, 2);//TM Struggle Bug
            machines.Spawns.Add(new InvItem("tm_dragon_claw"), range, 2);//TM Dragon Claw
            machines.Spawns.Add(new InvItem("tm_rain_dance"), range, 2);//TM Rain Dance
            machines.Spawns.Add(new InvItem("tm_sunny_day"), range, 2);//TM Sunny Day
            machines.Spawns.Add(new InvItem("tm_sandstorm"), range, 2);//TM Sandstorm
            machines.Spawns.Add(new InvItem("tm_hail"), range, 2);//TM Hail
            machines.Spawns.Add(new InvItem("tm_rock_tomb"), range, 2);//TM Rock Tomb
            machines.Spawns.Add(new InvItem("tm_attract"), range, 2);//TM Attract
            machines.Spawns.Add(new InvItem("tm_hidden_power"), range, 2);//TM Hidden Power
            machines.Spawns.Add(new InvItem("tm_taunt"), range, 2);//TM Taunt
            machines.Spawns.Add(new InvItem("tm_ice_beam"), range, 2);//TM Ice Beam
            machines.Spawns.Add(new InvItem("tm_light_screen"), range, 2);//TM Light Screen
            machines.Spawns.Add(new InvItem("tm_stone_edge"), range, 2);//TM Stone Edge
            machines.Spawns.Add(new InvItem("tm_shadow_claw"), range, 2);//TM Shadow Claw
            machines.Spawns.Add(new InvItem("tm_grass_knot"), range, 2);//TM Grass Knot
            machines.Spawns.Add(new InvItem("tm_brick_break"), range, 2);//TM Brick Break
            machines.Spawns.Add(new InvItem("tm_calm_mind"), range, 2);//TM Calm Mind
            machines.Spawns.Add(new InvItem("tm_torment"), range, 2);//TM Torment
            machines.Spawns.Add(new InvItem("tm_strength"), range, 2);//TM Strength
            machines.Spawns.Add(new InvItem("tm_cut"), range, 2);//TM Cut
            machines.Spawns.Add(new InvItem("tm_rock_smash"), range, 2);//TM Rock Smash
            machines.Spawns.Add(new InvItem("tm_bulk_up"), range, 2);//TM Bulk Up
            machines.Spawns.Add(new InvItem("tm_rest"), range, 2);//TM Rest
            machines.Spawns.Add(new InvItem("tm_psych_up"), range, 2);//TM Psych Up

            range = new IntRange(10, 30);

            machines.Spawns.Add(new InvItem("tm_psychic"), range, 1);//TM Psychic
            machines.Spawns.Add(new InvItem("tm_flamethrower"), range, 1);//TM Flamethrower
            machines.Spawns.Add(new InvItem("tm_hyper_beam"), range, 1);//TM Hyper Beam
            machines.Spawns.Add(new InvItem("tm_blizzard"), range, 1);//TM Blizzard
            machines.Spawns.Add(new InvItem("tm_rock_slide"), range, 1);//TM Rock Slide
            machines.Spawns.Add(new InvItem("tm_sludge_wave"), range, 1);//TM Sludge Wave
            machines.Spawns.Add(new InvItem("tm_swords_dance"), range, 1);//TM Swords Dance
            machines.Spawns.Add(new InvItem("tm_energy_ball"), range, 1);//TM Energy Ball
            machines.Spawns.Add(new InvItem("tm_fire_blast"), range, 1);//TM Fire Blast
            machines.Spawns.Add(new InvItem("tm_thunderbolt"), range, 1);//TM Thunderbolt
            machines.Spawns.Add(new InvItem("tm_shadow_ball"), range, 1);//TM Shadow Ball
            machines.Spawns.Add(new InvItem("tm_dark_pulse"), range, 1);//TM Dark Pulse
            machines.Spawns.Add(new InvItem("tm_earthquake"), range, 1);//TM Earthquake
            machines.Spawns.Add(new InvItem("tm_frost_breath"), range, 1);//TM Frost Breath
            machines.Spawns.Add(new InvItem("tm_dazzling_gleam"), range, 1);//TM Dazzling Gleam
            machines.Spawns.Add(new InvItem("tm_snarl"), range, 1);//TM Snarl
            machines.Spawns.Add(new InvItem("tm_focus_blast"), range, 1);//TM Focus Blast
            machines.Spawns.Add(new InvItem("tm_overheat"), range, 1);//TM Overheat
            machines.Spawns.Add(new InvItem("tm_sludge_bomb"), range, 1);//TM Sludge Bomb
            machines.Spawns.Add(new InvItem("tm_recycle"), range, 1);//TM Solar Beam
            machines.Spawns.Add(new InvItem("tm_flash_cannon"), range, 1);//TM Flash Cannon
            machines.Spawns.Add(new InvItem("tm_surf"), range, 1);//TM Surf

            machines.Spawns.Add(new InvItem("tm_psychic", true), range, 1);//TM Psychic
            machines.Spawns.Add(new InvItem("tm_flamethrower", true), range, 1);//TM Flamethrower
            machines.Spawns.Add(new InvItem("tm_hyper_beam", true), range, 1);//TM Hyper Beam
            machines.Spawns.Add(new InvItem("tm_blizzard", true), range, 1);//TM Blizzard
            machines.Spawns.Add(new InvItem("tm_rock_slide", true), range, 1);//TM Rock Slide
            machines.Spawns.Add(new InvItem("tm_sludge_wave", true), range, 1);//TM Sludge Wave
            machines.Spawns.Add(new InvItem("tm_swords_dance", true), range, 1);//TM Swords Dance
            machines.Spawns.Add(new InvItem("tm_energy_ball", true), range, 1);//TM Energy Ball
            machines.Spawns.Add(new InvItem("tm_fire_blast", true), range, 1);//TM Fire Blast
            machines.Spawns.Add(new InvItem("tm_thunderbolt", true), range, 1);//TM Thunderbolt
            machines.Spawns.Add(new InvItem("tm_shadow_ball", true), range, 1);//TM Shadow Ball
            machines.Spawns.Add(new InvItem("tm_dark_pulse", true), range, 1);//TM Dark Pulse
            machines.Spawns.Add(new InvItem("tm_earthquake", true), range, 1);//TM Earthquake
            machines.Spawns.Add(new InvItem("tm_frost_breath", true), range, 1);//TM Frost Breath
            machines.Spawns.Add(new InvItem("tm_dazzling_gleam", true), range, 1);//TM Dazzling Gleam
            machines.Spawns.Add(new InvItem("tm_snarl", true), range, 1);//TM Snarl
            machines.Spawns.Add(new InvItem("tm_focus_blast", true), range, 1);//TM Focus Blast
            machines.Spawns.Add(new InvItem("tm_overheat", true), range, 1);//TM Overheat
            machines.Spawns.Add(new InvItem("tm_sludge_bomb", true), range, 1);//TM Sludge Bomb
            machines.Spawns.Add(new InvItem("tm_recycle", true), range, 1);//TM Solar Beam
            machines.Spawns.Add(new InvItem("tm_flash_cannon", true), range, 1);//TM Flash Cannon
            machines.Spawns.Add(new InvItem("tm_surf", true), range, 1);//TM Surf

            //evo items
            CategorySpawn<InvItem> evoItems = new CategorySpawn<InvItem>();
            evoItems.SpawnRates.SetRange(3, new IntRange(0, 30));
            itemSpawnZoneStep.Spawns.Add("evo", evoItems);

            range = new IntRange(0, 25);
            evoItems.Spawns.Add(new InvItem("evo_fire_stone"), range, 10);//Fire Stone
            evoItems.Spawns.Add(new InvItem("evo_thunder_stone"), range, 10);//Thunder Stone
            evoItems.Spawns.Add(new InvItem("evo_water_stone"), range, 10);//Water Stone
            evoItems.Spawns.Add(new InvItem("evo_moon_stone"), range, 10);//Moon Stone
            evoItems.Spawns.Add(new InvItem("evo_dusk_stone"), range, 10);//Dusk Stone
            evoItems.Spawns.Add(new InvItem("evo_dawn_stone"), range, 10);//Dawn Stone
            evoItems.Spawns.Add(new InvItem("evo_shiny_stone"), range, 10);//Shiny Stone
            evoItems.Spawns.Add(new InvItem("evo_leaf_stone"), range, 10);//Leaf Stone
            evoItems.Spawns.Add(new InvItem("evo_ice_stone"), range, 10);//Ice Stone
            evoItems.Spawns.Add(new InvItem("evo_sun_ribbon"), range, 10);//Sun Ribbon
            evoItems.Spawns.Add(new InvItem("evo_lunar_ribbon"), range, 10);//Moon Ribbon
            evoItems.Spawns.Add(new InvItem("held_metal_coat"), range, 10);//Metal Coat
            floorSegment.ZoneSteps.Add(itemSpawnZoneStep);


            //mobs
            TeamSpawnZoneStep poolSpawn = new TeamSpawnZoneStep();
            poolSpawn.Priority = PR_RESPAWN_MOB;

            //19 Rattata : 33 Tackle
            poolSpawn.Spawns.Add(GetTeamMob("rattata", "", "tackle", "", "", "", new RandRange(3)), new IntRange(0, 2), 10);

            //173 Cleffa : 98 Magic Guard : 383 Pound
            poolSpawn.Spawns.Add(GetTeamMob("cleffa", "magic_guard", "pound", "", "", "", new RandRange(3)), new IntRange(0, 3), 10);

            //427 Buneary : 50 Run Away : 1 Pound : 150 Splash
            poolSpawn.Spawns.Add(GetTeamMob("buneary", "run_away", "pound", "splash", "", "", new RandRange(3)), new IntRange(0, 3), 10);

            //016 Pidgey : 16 Gust : 28 Sand Attack
            poolSpawn.Spawns.Add(GetTeamMob("pidgey", "", "gust", "sand_attack", "", "", new RandRange(3)), new IntRange(1, 3), 10);

            //1//175 Togepi : 204 Charm
            poolSpawn.Spawns.Add(GetTeamMob("togepi", "serene_grace", "charm", "", "", "", new RandRange(5)), new IntRange(1, 3), 10);

            //287 Slakoth : Truant : 010 Scratch
            poolSpawn.Spawns.Add(GetTeamMob("slakoth", "", "scratch", "", "", "", new RandRange(6)), new IntRange(1, 4), 10);

            //265 Wurmple : 081 String Shot : 033 Tackle
            poolSpawn.Spawns.Add(GetTeamMob("wurmple", "", "string_shot", "tackle", "", "", new RandRange(5)), new IntRange(1, 3), 10);


            //1//403 Shinx : 043 Leer : 033 Tackle
            poolSpawn.Spawns.Add(GetTeamMob("shinx", "", "leer", "tackle", "", "", new RandRange(4)), new IntRange(1, 3), 10);

            //420 Cherubi : 073 Leech Seed : 033 Tackle
            poolSpawn.Spawns.Add(GetTeamMob("cherubi", "", "leech_seed", "tackle", "", "", new RandRange(6)), new IntRange(2, 4), 10);

            //290 Nincada : 117 Bide : 010 Scratch : 106 Harden
            poolSpawn.Spawns.Add(GetTeamMob("nincada", "", "bide", "scratch", "harden", "", new RandRange(9)), new IntRange(2, 4), 10);

            //5//406 Budew : 071 Absorb : 078 Stun Spore : 346 Water Sport
            poolSpawn.Spawns.Add(GetTeamMob("budew", "poison_point", "absorb", "stun_spore", "water_sport", "", new RandRange(6)), new IntRange(2, 4), 10);

            //7//433 Chingling : 310 Astonish : 035 Wrap
            poolSpawn.Spawns.Add(GetTeamMob("chingling", "", "astonish", "wrap", "", "", new RandRange(8)), new IntRange(2, 4), 10);

            //133 Eevee : 270 Helping Hand : 098 Quick Attack
            poolSpawn.Spawns.Add(GetTeamMob("eevee", "", "helping_hand", "quick_attack", "", "", new RandRange(8)), new IntRange(3, 5), 10);

            //228 Houndour : 052 Ember : 123 Smog : 046 Roar
            poolSpawn.Spawns.Add(GetTeamMob("houndour", "", "ember", "smog", "roar", "", new RandRange(7)), new IntRange(3, 5), 10);

            //172 Pichu : 084 Thundershock : 204 Charm
            poolSpawn.Spawns.Add(GetTeamMob("pichu", "", "thunder_shock", "charm", "", "", new RandRange(5)), new IntRange(4, 5), 10);


            {
                //155 Cyndaquil : 43 Leer : 52 Ember
                poolSpawn.Spawns.Add(GetTeamMob("cyndaquil", "", "leer", "ember", "", "", new RandRange(10), "wander_normal", true), new IntRange(3, 5), 10);

                //152 Chikorita : 77 Poison Powder : 075 Razor Leaf
                poolSpawn.Spawns.Add(GetTeamMob("chikorita", "", "poison_powder", "vine_whip", "", "", new RandRange(10), "wander_normal", true), new IntRange(3, 5), 10);

                //158 Totodile : 44 Bite : 55 Water Gun
                poolSpawn.Spawns.Add(GetTeamMob("totodile", "", "bite", "water_gun", "", "", new RandRange(10), "wander_normal", true), new IntRange(3, 5), 10);
            }


            {
                //266 Silcoon : 106 Harden : 450 Bug Bite
                poolSpawn.Spawns.Add(GetTeamMob("silcoon", "", "harden", "bug_bite", "", "", new RandRange(8), TeamMemberSpawn.MemberRole.Leader, "wait_attack"), new IntRange(4, 6), 10);
                //268 Cascoon : 106 Harden : 450 Bug Bite
                poolSpawn.Spawns.Add(GetTeamMob("cascoon", "", "harden", "bug_bite", "", "", new RandRange(8), TeamMemberSpawn.MemberRole.Leader, "wait_attack"), new IntRange(4, 6), 10);
            }

            //if (ii >= 9 && ii < 10)
            //{
            //    //174 * Igglybuff : 383 Copycat
            //    //appears in a special situation
            //    GetGenericMob(mobZoneStep, 174, -1, 383, -1, -1, -1, new RangeSpawn(15));
            //}


            //447 Riolu : 068 Counter : 098 Quick Attack
            poolSpawn.Spawns.Add(GetTeamMob("riolu", "", "counter", "quick_attack", "", "", new RandRange(11), TeamMemberSpawn.MemberRole.Leader), new IntRange(4, 6), 10);

            //090 Shellder : 062 Aurora Beam : 055 Water Gun
            poolSpawn.Spawns.Add(GetTeamMob("shellder", "", "aurora_beam", "water_gun", "", "", new RandRange(10)), new IntRange(4, 5), 10);

            //102 Exeggcute : 140 Barrage : 115 Reflect
            poolSpawn.Spawns.Add(GetTeamMob("exeggcute", "", "barrage", "reflect", "", "", new RandRange(10), TeamMemberSpawn.MemberRole.Support), new IntRange(5, 7), 100);

            //15//190 Aipom : 310 Astonish : 321 Tickle : 010 Scratch
            poolSpawn.Spawns.Add(GetTeamMob("aipom", "", "astonish", "tickle", "scratch", "", new RandRange(10), TeamMemberSpawn.MemberRole.Leader), new IntRange(5, 7), 10);


            //046 Paras : 78 Stun Spore : 141 Leech Life
            poolSpawn.Spawns.Add(GetTeamMob("paras", "", "poison_powder", "leech_life", "", "", new RandRange(9)), new IntRange(5, 7), 10);

            //456 Finneon : 114 Storm Drain : 55 Water Gun : 16 Gust
            poolSpawn.Spawns.Add(GetTeamMob("finneon", "storm_drain", "water_gun", "gust", "", "", new RandRange(13), TeamMemberSpawn.MemberRole.Loner), new IntRange(5, 7), 10);

            //first individual, then in groups
            //4//261 Poochyena : 336 Howl : 44 Bite
            poolSpawn.Spawns.Add(GetTeamMob("poochyena", "", "howl", "bite", "", "", new RandRange(10), TeamMemberSpawn.MemberRole.Support), new IntRange(5, 7), 100);


            //353 Shuppet : 174 Curse : 101 Night Shade
            poolSpawn.Spawns.Add(GetTeamMob("shuppet", "", "curse", "night_shade", "", "", new RandRange(11)), new IntRange(6, 8), 10);


            //220 Swinub : 426 Mud Bomb
            poolSpawn.Spawns.Add(GetTeamMob("swinub", "", "mud_bomb", "", "", "", new RandRange(13), TeamMemberSpawn.MemberRole.Leader), new IntRange(6, 8), 10);

            //108 Lickitung : 35 Wrap : 122 Lick
            poolSpawn.Spawns.Add(GetTeamMob("lickitung", "", "wrap", "lick", "", "", new RandRange(14), TeamMemberSpawn.MemberRole.Leader), new IntRange(6, 8), 10);

            //352 Kecleon : 168 Thief
            poolSpawn.Spawns.Add(GetTeamMob("kecleon", "", "thief", "", "", "", new RandRange(11), TeamMemberSpawn.MemberRole.Loner, "thief"), new IntRange(6, 8), 10);

            //417 Pachirisu : 609 Nuzzle : 098 Quick Attack
            poolSpawn.Spawns.Add(GetTeamMob("pachirisu", "", "nuzzle", "quick_attack", "", "", new RandRange(13)), new IntRange(6, 8), 10);

            //1//056 Mankey : 128 Defiant : 083 Anger Point : 043 Leer : 067 Low Kick
            poolSpawn.Spawns.Add(GetTeamMob("mankey", "", "leer", "low_kick", "", "", new RandRange(14)), new IntRange(7, 9), 10);

            //052 Meowth : 6 Pay Day : 10 Scratch : Taunt
            poolSpawn.Spawns.Add(GetTeamMob("meowth", "", "pay_day", "scratch", "", "", new RandRange(14)), new IntRange(7, 9), 10);

            //316 Gulpin : 124 Sludge : 281 Yawn
            poolSpawn.Spawns.Add(GetTeamMob("gulpin", "", "sludge", "yawn", "", "", new RandRange(14)), new IntRange(7, 9), 10);

            //035 Clefairy : 56 Cute Charm : 516 Bestow : 1 Pound
            poolSpawn.Spawns.Add(GetTeamMob("clefairy", "cute_charm", "bestow", "pound", "", "", new RandRange(15), "item_finder"), new IntRange(7, 9), 10);

            //455 Carnivine : 275 Ingrain : 230 Sweet Scent : 44 Bite
            poolSpawn.Spawns.Add(GetTeamMob("carnivine", "", "ingrain", "sweet_scent", "bite", "", new RandRange(16)), new IntRange(7, 9), 10);

            //1//123 Scyther : 410 Vacuum Wave : 206 False Swipe
            poolSpawn.Spawns.Add(GetTeamMob("scyther", "", "vacuum_wave", "false_swipe", "", "", new RandRange(11), TeamMemberSpawn.MemberRole.Loner), new IntRange(7, 9), 10);

            //39//286 Imprison?
            //037 Vulpix : 52 Ember
            poolSpawn.Spawns.Add(GetTeamMob("vulpix", "", "ember", "", "", "", new RandRange(18)), new IntRange(7, 9), 10);

            //8//127 Pinsir : 11 Vice Grip : 069 Seismic Toss
            poolSpawn.Spawns.Add(GetTeamMob("pinsir", "", "vice_grip", "seismic_toss", "", "", new RandRange(15), TeamMemberSpawn.MemberRole.Loner), new IntRange(8, 10), 10);

            //21//096 Drowzee : 095 Hypnosis : 050 Disable : 096 Meditate
            poolSpawn.Spawns.Add(GetTeamMob("drowzee", "", "hypnosis", "disable", "meditate", "", new RandRange(16)), new IntRange(8, 10), 10);

            //438 Bonsly : 102 Mimic? : 67 Low Kick : 88 Rock Throw
            poolSpawn.Spawns.Add(GetTeamMob("bonsly", "", "mimic", "low_kick", "rock_throw", "", new RandRange(15), "weird_tree"), new IntRange(8, 10), 10);

            //441 Chatot : 497 Echoed Voice : 297 Feather Dance
            poolSpawn.Spawns.Add(GetTeamMob("chatot", "", "echoed_voice", "feather_dance", "", "", new RandRange(18), TeamMemberSpawn.MemberRole.Loner), new IntRange(8, 10), 10);

            //29//439 Mime Jr. : 383 Copycat : 164 Substitute
            poolSpawn.Spawns.Add(GetTeamMob("mime_jr", "", "copycat", "substitute", "", "", new RandRange(18)), new IntRange(8, 10), 10);

            //236 Tyrogue : 33 Tackle : 252 Fake Out
            poolSpawn.Spawns.Add(GetTeamMob("tyrogue", "", "tackle", "fake_out", "", "", new RandRange(16)), new IntRange(8, 10), 10);

            //288 Vigoroth : 281 Yawn : 303 Slack Off : 163 Slash
            poolSpawn.Spawns.Add(GetTeamMob("vigoroth", "", "yawn", "slack_off", "slash", "", new RandRange(18), TeamMemberSpawn.MemberRole.Loner), new IntRange(8, 10), 10);

            //328 Trapinch : 71 Arena Trap : 328 Sand Tomb : 91 Dig
            poolSpawn.Spawns.Add(GetTeamMob("trapinch", "arena_trap", "sand_tomb", "dig", "", "", new RandRange(18), TeamMemberSpawn.MemberRole.Leader), new IntRange(9, 11), 10);

            //17//446 Munchlax : 033 Tackle : 133 Amnesia : 498 Chip Away
            poolSpawn.Spawns.Add(GetTeamMob("munchlax", "", "tackle", "amnesia", "chip_away", "", new RandRange(20), "wander_normal", true), new IntRange(9, 11), 10);

            //246 Larvitar : 103 Screech : 157 Rock Slide
            poolSpawn.Spawns.Add(GetTeamMob("larvitar", "", "screech", "rock_slide", "", "", new RandRange(18)), new IntRange(9, 11), 10);

            //100 Voltorb : 043 Soundproof : 451 Charge Beam : 120 Self-Destruct
            poolSpawn.Spawns.Add(GetTeamMob("voltorb", "soundproof", "charge_beam", "self_destruct", "", "", new RandRange(20)), new IntRange(9, 11), 10);

            //15//436 Bronzor : 149 Psywave : 286 Imprison
            poolSpawn.Spawns.Add(GetTeamMob("bronzor", "", "psywave", "imprison", "", "", new RandRange(20)), new IntRange(9, 11), 10);

            //296 Makuhita : 282 Knock Off : 292 Arm Thrust
            poolSpawn.Spawns.Add(GetTeamMob("makuhita", "", "knock_off", "arm_thrust", "", "", new RandRange(18)), new IntRange(10, 12), 10);

            //37//304 Aron : 232 Metal Claw : 334 Iron Defense

            //23//104 Cubone : 099 Rage : 125 Bone Club
            poolSpawn.Spawns.Add(GetTeamMob("cubone", "", "rage", "bone_club", "", "", new RandRange(23)), new IntRange(10, 12), 10);

            //198 Murkrow : 228 Pursuit : 17 Wing Attack : 372 Assurance
            poolSpawn.Spawns.Add(GetTeamMob("murkrow", "", "pursuit", "wing_attack", "assurance", "", new RandRange(18)), new IntRange(10, 12), 10);

            //343 Baltoy : 322 Cosmic Power : 377 Heal Block : 189 Mud Slap
            poolSpawn.Spawns.Add(GetTeamMob("baltoy", "", "cosmic_power", "heal_block", "mud_slap", "", new RandRange(19), TeamMemberSpawn.MemberRole.Support), new IntRange(11, 13), 10);

            //129 Magikarp : 150 Splash : 33 Tackle
            poolSpawn.Spawns.Add(GetTeamMob("magikarp", "", "splash", "tackle", "", "", new RandRange(14)), new IntRange(11, 13), 10);

            //17//371 Bagon : 116 Focus Energy : 099 Rage : 029 Headbutt
            poolSpawn.Spawns.Add(GetTeamMob("bagon", "", "focus_energy", "rage", "headbutt", "", new RandRange(20)), new IntRange(11, 13), 10);

            //24//231 Phanpy : 205 Rollout : 021 Slam
            poolSpawn.Spawns.Add(GetTeamMob("phanpy", "", "rollout", "slam", "", "", new RandRange(22)), new IntRange(11, 13), 10);

            //1//359 Absol : 013 Razor Wind
            poolSpawn.Spawns.Add(GetTeamMob("absol", "", "razor_wind", "", "", "", new RandRange(20), TeamMemberSpawn.MemberRole.Loner), new IntRange(11, 13), 10);

            //035 Clefairy : 56 Cute Charm : 266 Follow Me : 3 Double Slap : 107 Minimize
            poolSpawn.Spawns.Add(GetTeamMob("clefairy", "cute_charm", "follow_me", "pound", "minimize", "", new RandRange(22), TeamMemberSpawn.MemberRole.Support), new IntRange(11, 13), 10);

            //206 Dunsparce : 180 Spite : 103 Screech : 246 Ancient Power
            poolSpawn.Spawns.Add(GetTeamMob("dunsparce", "", "spite", "screech", "ancient_power", "", new RandRange(22)), new IntRange(11, 13), 10);

            //194 Wooper : 341 Mud Shot : 21 Slam : 133 Amnesia
            poolSpawn.Spawns.Add(GetTeamMob("wooper", "", "mud_shot", "slam", "amnesia", "", new RandRange(24)), new IntRange(11, 13), 10);

            //185 Sudowoodo : 335 Block : 157 Rock Slide : 102 Mimic
            poolSpawn.Spawns.Add(GetTeamMob("sudowoodo", "", "block", "rock_slide", "mimic", "", new RandRange(24), "weird_tree"), new IntRange(11, 13), 10);

            //29//167 Spinarak : 169 Spider Web : 101 Night Shade
            poolSpawn.Spawns.Add(GetTeamMob("spinarak", "", "night_shade", "spider_web", "", "", new RandRange(25), TeamMemberSpawn.MemberRole.Support), new IntRange(12, 14), 10);

            //33//404 Luxio : 209 Spark : 268 Charge
            poolSpawn.Spawns.Add(GetTeamMob("luxio", "", "spark", "charge", "", "", new RandRange(22)), new IntRange(12, 14), 10);

            //177 Natu : 100 Teleport : 248 Future Sight
            poolSpawn.Spawns.Add(GetTeamMob("natu", "", "teleport", "future_sight", "", "", new RandRange(25), TeamMemberSpawn.MemberRole.Loner), new IntRange(12, 14), 10);

            //322 Numel : 481 Flame Burst
            poolSpawn.Spawns.Add(GetTeamMob("numel", "", "flame_burst", "", "", "", new RandRange(22)), new IntRange(12, 14), 10);

            //095 Onix : 317 Rock Tomb : 157 Rock Slide
            poolSpawn.Spawns.Add(GetTeamMob("onix", "", "rock_tomb", "rock_slide", "", "", new RandRange(24), TeamMemberSpawn.MemberRole.Loner), new IntRange(12, 14), 10);

            //8//238 Smoochum : 186 Sweet Kiss : 181 Powder Snow
            poolSpawn.Spawns.Add(GetTeamMob("smoochum", "", "sweet_kiss", "powder_snow", "", "", new RandRange(23)), new IntRange(12, 14), 10);

            //352 Kecleon : 168 Thief : 425 Shadow Sneak
            poolSpawn.Spawns.Add(GetTeamMob("kecleon", "", "thief", "shadow_sneak", "", "", new RandRange(25), TeamMemberSpawn.MemberRole.Support, "thief"), new IntRange(13, 15), 10);

            //1//132 Ditto : 144 Transform
            poolSpawn.Spawns.Add(GetTeamMob("ditto", "", "transform", "", "", "", new RandRange(20), TeamMemberSpawn.MemberRole.Loner), new IntRange(13, 15), 10);

            //200 Misdreavus : 180 Spite : 310 Astonish : 220 Pain Split
            poolSpawn.Spawns.Add(GetTeamMob("misdreavus", "", "spite", "astonish", "pain_split", "", new RandRange(25)), new IntRange(13, 15), 10);

            //107 Hitmonchan : 264 Focus Punch : 193 Foresight
            poolSpawn.Spawns.Add(GetTeamMob("hitmonchan", "", "focus_punch", "foresight", "", "", new RandRange(23), TeamMemberSpawn.MemberRole.Support), new IntRange(12, 14), 10);

            //037-1 Vulpix : 196 Icy Wind
            poolSpawn.Spawns.Add(GetTeamMob(new MonsterID("vulpix", 1, "", Gender.Unknown), "", "icy_wind", "", "", "", new RandRange(25)), new IntRange(12, 14), 10);

            //037 Vulpix : 52 Ember : 506 Hex
            poolSpawn.Spawns.Add(GetTeamMob("vulpix", "", "ember", "hex", "", "", new RandRange(28)), new IntRange(14, 16), 10);

            //299 Nosepass : 408 Power Gem : 435 Discharge
            poolSpawn.Spawns.Add(GetTeamMob("nosepass", "", "power_gem", "discharge", "", "", new RandRange(21), TeamMemberSpawn.MemberRole.Loner), new IntRange(14, 16), 10);

            //218 Slugma : 510 Incinerate : 106 Harden
            poolSpawn.Spawns.Add(GetTeamMob("slugma", "", "incinerate", "harden", "", "", new RandRange(23), TeamMemberSpawn.MemberRole.Loner), new IntRange(14, 16), 10);

            //136 Flareon : 424 Fire Fang
            poolSpawn.Spawns.Add(GetTeamMob("flareon", "", "fire_fang", "", "", "", new RandRange(23)), new IntRange(14, 16), 10);

            //463 Lickilicky : 205 Rollout : 438 Power Whip : 378 Wring Out : 35 Wrap
            poolSpawn.Spawns.Add(GetTeamMob("lickilicky", "", "rollout", "power_whip", "wring_out", "wrap", new RandRange(24)), new IntRange(15, 17), 10);

            //476 Probopass : 443 Magnet Bomb : 393 Magnet Rise : 414 Earth Power
            poolSpawn.Spawns.Add(GetTeamMob("probopass", "", "magnet_bomb", "magnet_rise", "earth_power", "", new RandRange(26), TeamMemberSpawn.MemberRole.Loner), new IntRange(15, 17), 10);

            //353 Shuppet : 271 Trick : 180 Spite
            poolSpawn.Spawns.Add(GetTeamMob("shuppet", "", "trick", "spite", "", "", new RandRange(25), "thief"), new IntRange(15, 17), 10);

            //136 Flareon : 83 Fire Spin : 387 Last Resort
            poolSpawn.Spawns.Add(GetTeamMob("flareon", "", "fire_spin", "last_resort", "", "", new RandRange(26)), new IntRange(15, 17), 10);

            //23//294 Loudred : 103 Screech : 023 Stomp

            //34//020 Raticate : 162 Super Fang : 389 Sucker Punch : 158 Hyper Fang
            //34//019 Rattata : 283 Endeavor : 098 Quick Attack

            //34//075 Graveler : 446 Stealth Rock : 350 Rock Blast

            //26//223 Remoraid : 055 Hustle : 116 Focus Energy : 199 Lock-On : 352 Water Pulse

            //31//459 Snover : 420 Ice Shard : 320 Grass Whistle : 275 Ingrain

            //44//143 Snorlax : 187 Belly Drum : 156 Rest : 034 Body Slam

            //29//176 Togetic : 273 Wish : 227 Encore : 584 Fairy Wind

            //344 Claydol : 60 Psybeam : 246 Ancient Power : 100 Teleport
            poolSpawn.Spawns.Add(GetTeamMob("claydol", "", "psybeam", "ancient_power", "teleport", "", new RandRange(28), "retreater"), new IntRange(15, 17), 10);

            //23//226 Mantine : 469 Wide Guard : 017 Wing Attack

            //29//125 Electabuzz : 009 Thunder Punch : 129 Swift : 113 Light Screen
            //29//499 Magmar : 007 Fire Punch : 499 Clear Smog : 109 Confuse Ray
            //18//124 Jynx : 008 Ice Punch : 577 Draining Kiss : 531 Heart Stamp

            //42//326 Grumpig : 277 Magic Coat : 473 Psyshock : 316 Odor Sleuth

            //106 Hitmonlee : 469 Wide Guard : 136 High Jump Kick
            poolSpawn.Spawns.Add(GetTeamMob("hitmonlee", "", "wide_guard", "high_jump_kick", "", "", new RandRange(27), TeamMemberSpawn.MemberRole.Loner), new IntRange(16, 18), 10);

            //114 Haze?
            //4//109 Koffing : 499 Clear Smog : 120 Self-Destruct
            poolSpawn.Spawns.Add(GetTeamMob("koffing", "levitate", "clear_smog", "self_destruct", "", "", new RandRange(27)), new IntRange(16, 18), 10);

            //219 Magcargo : 436 Lava Plume : 414 Earth Power
            poolSpawn.Spawns.Add(GetTeamMob("magcargo", "", "lava_plume", "earth_power", "", "", new RandRange(25), TeamMemberSpawn.MemberRole.Loner), new IntRange(16, 18), 10);

            //46//599 Muk : 151 Acid Armor : 188 Sludge Bomb

            //69//189 Jumpluff : 476 Rage Powder : 262 Memento : 073 Leech Seed

            //47//185 Sudowoodo : 335 Block : 359 Hammer Arm : 452 Wood Hammer : Rock Slide

            //50//162 Furret : 382 Me First : 270 Helping Hand : 226 Baton Pass

            //50//229 Houndoom : 373 Embargo : 053 Flamethrower : 492 Foul Play : 251 Beat Up

            //49//082 Magneton : 604 Electric Terrain : 199 Lock-On : 192 Zap Cannon

            //359 Absol : 14 Swords Dance : 400 Night Slash : 427 Psycho Cut  (3 tiles in front, 1 tile away)
            poolSpawn.Spawns.Add(GetTeamMob("absol", "", "swords_dance", "night_slash", "psycho_cut", "", new RandRange(28)), new IntRange(17, 19), 10);

            //303 Mawile : 242 Crunch : 442 Iron Head : 313 Fake Tears?
            poolSpawn.Spawns.Add(GetTeamMob("mawile", "", "crunch", "iron_head", "fake_tears", "", new RandRange(27)), new IntRange(17, 19), 10);

            //006 Charizard : 126 Fire Blast : 403 Air Slash
            {
                TeamMemberSpawn mob = GetTeamMob("charizard", "", "fire_blast", "wing_attack", "", "", new RandRange(45), TeamMemberSpawn.MemberRole.Loner, "wander_normal", true);
                MobSpawnItem itemSpawn = new MobSpawnItem(true);
                foreach(string item_name in IterateGummis())
                    itemSpawn.Items.Add(new InvItem(item_name), 100);
                mob.Spawn.SpawnFeatures.Add(itemSpawn);
                poolSpawn.Spawns.Add(mob, new IntRange(17, 19), 10);
            }

            //36//153 Bayleef : 113 Light Screen : 115 Reflect : 235 Synthesis : 075 Razor Leaf : 363 Natural Gift

            //40//286 Breloom : 090 Poison Heal : 073 Leech Seed : 147 Spore

            //32//332 Cacturne : 596 Spiky Shield : Needle Arm

            //358 Wake-Up Slap, 265 Smelling Salts?
            //297 Hariyama : 395 Force Palm : 362 Brine
            poolSpawn.Spawns.Add(GetTeamMob("hariyama", "", "force_palm", "brine", "", "", new RandRange(30)), new IntRange(17, 19), 10);

            //25//315 Roselia : 230 Sweet Scent : 320 Grass Whistle : 202 Giga Drain

            //491 Acid Spray? 380 Gastro Acid?
            //317 Swalot : 60 Sticky Hold : 188 Sludge Bomb : 151 Acid Armor
            poolSpawn.Spawns.Add(GetTeamMob("swalot", "sticky_hold", "sludge_bomb", "acid_armor", "", "", new RandRange(32), TeamMemberSpawn.MemberRole.Loner), new IntRange(18, 20), 10);

            //50//428 Lopunny : 415 Switcheroo : 494 Entrainment : 98 Quick Attack

            //27//358 Chimecho : 215 Heal Bell : 281 Yawn
            poolSpawn.Spawns.Add(GetTeamMob("chimecho", "", "heal_bell", "yawn", "confusion", "", new RandRange(30), TeamMemberSpawn.MemberRole.Support), new IntRange(19, 21), 10);

            //20//171 Lanturn : 035 Illuminate : 145 Bubble Beam : 486 Electro Ball : 254 Stockpile

            //47//171 Lanturn : 035 Illuminate : 435 Discharge : 598 Eerie Impulse : 392 Aqua Ring

            //46//334 Altaria : 054 Mist : 143 Sky Attack : 195 Perish Song : 219 Safeguard : 287 Refresh

            //37//417 Pachirisu : 162 Super Fang : 609 Nuzzle : 186 Sweet Kiss

            //28//028 Sandslash : 008 Sand Veil : 306 Crush Claw : 163 Slash

            //288 Grudge?
            //292 Shedinja : 180 Spite : 210 Fury Cutter : 566 Phantom Force
            poolSpawn.Spawns.Add(GetTeamMob("shedinja", "", "spite", "fury_cutter", "phantom_force", "", new RandRange(35)), new IntRange(19, 21), 10);

            //53//465 Tangrowth : 144 Regenerator : 438 Power Whip

            //43//442 Spiritomb : 109 Confuse Ray : 262 Memento : 095 Hypnosis

            //46//214 Heracross : 068 Swarm : 153 Moxie : 280 Brick Break : 224 Megahorn : 203 Endure : 179 Reversal

            //35//329 Vibrava : 225 Dragon Breath : 048 Supersonic

            //44//076 Golem : 153 Explosion : 205 Rollout

            //38//324 Torkoal : 257 Heat Wave : 334 Iron Defense : 175 Flail

            //39 Jigglypuff : 47 Sing : 156 Rest : 574 Disarming Voice
            poolSpawn.Spawns.Add(GetTeamMob("jigglypuff", "", "sing", "rest", "", "", new RandRange(35)), new IntRange(19, 21), 10);

            //373 Embargo? 289 Snatch?
            //354 Banette : 130 Cursed Body : 288 Grudge : 425 Shadow Sneak
            poolSpawn.Spawns.Add(GetTeamMob("banette", "cursed_body", "grudge", "shadow_sneak", "", "", new RandRange(32)), new IntRange(19, 21), 10);

            //33//321 Wailord : 323 Water Spout : 156 Rest : 250 Whirlpool

            //31//308 Medicham : 364 Feint : 244 Psych Up : 136 High Jump Kick

            //1//097 Hypno : 171 Nightmare : 093 Confusion : 415 Switcheroo
            poolSpawn.Spawns.Add(GetTeamMob("hypno", "", "nightmare", "confusion", "switcheroo", "", new RandRange(33), TeamMemberSpawn.MemberRole.Support, "thief"), new IntRange(19, 24), 10);

            //43//411 Bastiodon : 368 Metal Burst : 269 Taunt

            //27//264 Linoone : 343 Covet : 415 Switcheroo : 516 Bestow // to allies?

            //37//327 Spinda : 298 Teeter Dance : 253 Uproar
            //21//441 Chatot : 448 Chatter : 590 Confide : 119 Mirror Move

            //21//212 Scizor : 418 Bullet Punch : 232 Metal Claw

            //48//157 Typhlosion : 436 Lava Plume : 053 Flamethrower

            //43//442 Spiritomb : 095 Hypnosis : 138 Dream Eater
            poolSpawn.Spawns.Add(GetTeamMob("spiritomb", "", "hypnosis", "dream_eater", "", "", new RandRange(32)), new IntRange(19, 22), 10);

            //37//311 Plusle : 097 Agility : 270 Helping Hand
            //40//312 Minun : 486 Electro Ball : 376 Trump Card
            //crowd with one

            //51//435 Skuntank : 053 Flamethrower : 262 Memento

            //51//319 Sharpedo : 130 Skull Bash : 242 Crunch

            //32//199 Slowking : 144 Regenerator : 303 Slack Off : 505 Heal Pulse : 428 Zen Headbutt

            //1//121 Starmie : 035 Illuminate : 105 Recover : 129 Swift

            //017 Pidgeotto : 119 Mirror Move : 17 Wing Attack
            poolSpawn.Spawns.Add(GetTeamMob("pidgeotto", "", "mirror_move", "wing_attack", "", "", new RandRange(34)), new IntRange(20, 22), 10);

            //361 Snorunt : 196 Icy Wind : 181 Powder Snow
            poolSpawn.Spawns.Add(GetTeamMob("snorunt", "", "icy_wind", "powder_snow", "", "", new RandRange(33)), new IntRange(20, 22), 10);

            //215 Sneasel : 124 Pickpocket : 097 Agility : 196 Icy Wind : 185 Feint Attack
            poolSpawn.Spawns.Add(GetTeamMob("sneasel", "pickpocket", "agility", "icy_wind", "feint_attack", "", new RandRange(35), TeamMemberSpawn.MemberRole.Support), new IntRange(20, 22), 10);

            //15//369 Relicanth : 069 Rock Head : 457 Head Smash : 317 Rock Tomb

            //15//466 Electivire : 486 Electro Ball : 009 Thunder Punch : 569 Ion Deluge

            //45//405 Luxray : 528 Wild Charge : 268 Charge

            //15//335 Zangoose : 279 Revenge : 468 Hone Claws : 098 Quick Attack

            //23//110 Weezing : 114 Haze : 120 Self-Destruct

            //128 Tauros : 037 Thrash : 371 Payback : 036 Take Down : 099 Rage

            //103 Exeggutor : 140 Barrage : 121 Egg Bomb : 402 Seed Bomb


            //472 Gliscor : 512 Acrobatics

            //40//262 Mightyena : 046 Roar : 036 Take Down : 555 Snarl

            //429 Mismagius : 595 Mystical Fire : 381 Lucky Chant
            poolSpawn.Spawns.Add(GetTeamMob("mismagius", "", "mystical_fire", "lucky_chant", "", "", new RandRange(33), TeamMemberSpawn.MemberRole.Support), new IntRange(19, 21), 10);

            //352 Kecleon : 168 Thief : 425 Shadow Sneak : 485 Synchronoise
            poolSpawn.Spawns.Add(GetTeamMob("kecleon", "", "thief", "shadow_sneak", "synchronoise", "", new RandRange(33), TeamMemberSpawn.MemberRole.Support, "thief"), new IntRange(19, 21), 10);


            //29//122 Mr. Mime : 164 Substitute : 112 Barrier : 501 Quick Guard

            //35//042 Golbat : 109 Confuse Ray : 103 Screech : 305 Poison Fang : 512 Acrobatics

            //25//142 Aerodactyl : 367 Pressure : 246 Ancient Power : 017 Wing attack

            //053 Persian : 415 Switcheroo : 185 Feint Attack : 129 Swift
            {
                TeamMemberSpawn mob = GetTeamMob("persian", "", "switcheroo", "feint_attack", "swift", "", new RandRange(33), "thief");
                mob.Spawn.SpawnFeatures.Add(new MobSpawnItem(true, "held_toxic_orb", "held_flame_orb", "held_iron_ball", "held_ring_target", "held_choice_scarf"));
                poolSpawn.Spawns.Add(mob, new IntRange(20, 22), 10);
            }

            //46//162 Furret : 133 Amnesia : 266 Follow Me : 226 Baton Pass

            //58//168 Ariados : 564 Sticky Web : 599 Venom Drench : 398 Poison Jab

            //340 Whiscash : 107 Anticipation : 248 Future Sight : 209 Spark : 089 Earthquake : 401 Aqua Tail

            //17//024 Arbok : 137 Glare : 035 Wrap : 103 Screech

            //42//362 Glalie : 573 Freeze-Dry : 196 Icy Wind

            //135 Jolteon : 86 Thunder Wave : 97 Agility : 42 Pin Missile
            poolSpawn.Spawns.Add(GetTeamMob("jolteon", "", "thunder_wave", "agility", "pin_missile", "", new RandRange(35)), new IntRange(21, 23), 10);

            //448 Lucario : 370 Close Combat

            //47//308 Medicham : 105 Recover : 203 Endure : 179 Reversal

            //45//222 Corsola : 203 Endure : 243 Mirror Coat : 105 Recover

            //269 Dustox : 92 Toxic : 236 Moonlight : 405 Bug Buzz
            poolSpawn.Spawns.Add(GetTeamMob("dustox", "", "toxic", "moonlight", "bug_buzz", "", new RandRange(34)), new IntRange(21, 23), 10);
            //267 Beautifly : 213 Attract : 483 Quiver Dance : 202 Giga Drain : 318 Silver Wind
            poolSpawn.Spawns.Add(GetTeamMob("beautifly", "", "attract", "quiver_dance", "giga_drain", "silver_wind", new RandRange(34)), new IntRange(21, 23), 10);

            //48//186 Politoed : 195 Perish Song : 304 Hyper Voice

            //34//Sunflora : Solar Power : Sunny Day : Solar Beam

            //170 Mind Reader, 154 Fury Swipes, 405 Bug Buzz
            //291 Ninjask : 14 Swords Dance : 163 Slash
            poolSpawn.Spawns.Add(GetTeamMob("ninjask", "", "swords_dance", "slash", "", "", new RandRange(38), "retreater"), new IntRange(21, 23), 10);

            //197 Umbreon : 212 Mean Look : 185 Feint Attack
            poolSpawn.Spawns.Add(GetTeamMob("umbreon", "", "mean_look", "feint_attack", "", "", new RandRange(36), TeamMemberSpawn.MemberRole.Support), new IntRange(22, 24), 10);

            //32//262 Mightyena : 046 Roar : 168 Thief : 373 Embargo //run away when low on HP
            poolSpawn.Spawns.Add(GetTeamMob("mightyena", "", "roar", "thief", "embargo", "", new RandRange(37), TeamMemberSpawn.MemberRole.Support, "retreater"), new IntRange(22, 24), 10);

            //221 Piloswine : 423 Ice Fang : 31 Fury Attack : 36 Take Down
            poolSpawn.Spawns.Add(GetTeamMob("piloswine", "", "ice_fang", "fury_attack", "take_down", "", new RandRange(38)), new IntRange(22, 24), 10);

            //101 Electrode : 205 Rollout : 153 Explosion : 113 Light Screen
            poolSpawn.Spawns.Add(GetTeamMob("electrode", "", "rollout", "explosion", "", "", new RandRange(38), TeamMemberSpawn.MemberRole.Support), new IntRange(22, 24), 10);

            //1//225 Delibird : 217 Present

            poolSpawn.Spawns.Add(GetTeamMob("delibird", "", "present", "", "", "", new RandRange(39), TeamMemberSpawn.MemberRole.Support), new IntRange(22, 25), 10);

            //29//368 Gorebyss : 504 Shell Smash : 226 Baton Pass

            //59//105 Marowak : 155 Bonemarang : 198 Bone Rush : 514 Retaliate
            poolSpawn.Spawns.Add(GetTeamMob("marowak", "", "bonemerang", "bone_rush", "retaliate", "", new RandRange(37), TeamMemberSpawn.MemberRole.Support), new IntRange(22, 24), 10);

            //53//462 Magnezone : 393 Magnet Rise : 435 Discharge

            //50//315 Roselia : 312 Aromatherapy : 235 Synthesis : 080 Petal Dance

            //36//460 Abomasnow : 420 Ice Shard : 320 Grass Whistle : 452 Wood Hammer

            //52//466 Drifblim : 138 Flare Boost : 254 Stockpile : 466 Ominous Wind : 226 Baton Pass

            //91 Cloyster : 92 Skill Link : 182 Protect : 131 Spike Cannon : 191 Spikes
            poolSpawn.Spawns.Add(GetTeamMob("cloyster", "skill_link", "protect", "spike_cannon", "spikes", "", new RandRange(36)), new IntRange(23, 25), 10);

            //478 Froslass : 130 Cursed Body : 194 Destiny Bond : 58 Ice Beam
            poolSpawn.Spawns.Add(GetTeamMob("froslass", "cursed_body", "destiny_bond", "ice_beam", "", "", new RandRange(36)), new IntRange(23, 25), 10);

            //110 Weezing : 194 Destiny Bond : 153 Explosion : 188 Sludge Bomb
            {
                TeamMemberSpawn mob = GetTeamMob("weezing", "neutralizing_gas", "destiny_bond", "explosion", "", "", new RandRange(50), TeamMemberSpawn.MemberRole.Loner, "wander_normal", true);
                MobSpawnItem itemSpawn = new MobSpawnItem(true);
                foreach (string item_name in IterateGummis())
                    itemSpawn.Items.Add(new InvItem(item_name), 100);
                mob.Spawn.SpawnFeatures.Add(itemSpawn);
                poolSpawn.Spawns.Add(mob, new IntRange(22, 25), 10);
            }

            //67//139 Omastar : 504 Shell Smash : 131 Spike Cannon : 362 Brine

            //047 Parasect : 27 Effect Spore : 476 Rage Powder : 147 Spore
            poolSpawn.Spawns.Add(GetTeamMob("parasect", "effect_spore", "rage_powder", "spore", "", "", new RandRange(38), TeamMemberSpawn.MemberRole.Support), new IntRange(23, 26), 10);

            //457 Lumineon : 33 Swift Swim : 318 Silver Wind : 369 U-Turn : 445 Captivate
            poolSpawn.Spawns.Add(GetTeamMob("lumineon", "swift_swim", "silver_wind", "u_turn", "captivate", "", new RandRange(38)), new IntRange(24, 26), 10);

            //196 Espeon : 094 Psychic : 234 Morning Sun
            poolSpawn.Spawns.Add(GetTeamMob("espeon", "", "psychic", "morning_sun", "", "", new RandRange(35)), new IntRange(24, 26), 10);

            //36//Quagsire : 133 Amnesia : 089 Earthquake

            //48//Bibarel : 133 Amnesia : 158 Hyper Fang : 276 Superpower

            //32//131 Lapras : 047 Sing : 057 Surf : 195 Perish Song : 058 Ice Beam

            //1//132 Ditto : 144 Transform

            //41//085 Dodrio : 161 Tri-Attack : 065 Drill Peck

            //1//164 Noctowl : 143 Sky Attack : 095 Hypnosis

            //130 Skull Bash?
            //009 Blastoise : 056 Hydro Pump
            poolSpawn.Spawns.Add(GetTeamMob("blastoise", "", "hydro_pump", "", "", "", new RandRange(40), TeamMemberSpawn.MemberRole.Loner), new IntRange(24, 27), 10);

            //31//475 Gallade : 469 Wide Guard : 427 Psycho Cut : 100 Teleport : 14 Swords Dance
            poolSpawn.Spawns.Add(GetTeamMob("gallade", "", "wide_guard", "psycho_cut", "teleport", "swords_dance", new RandRange(40), TeamMemberSpawn.MemberRole.Leader, "retreater"), new IntRange(24, 27), 10);
            //282 Gardevoir : 94 Psychic : Wish : Moonblast
            poolSpawn.Spawns.Add(GetTeamMob("gardevoir", "", "psychic", "wish", "moonblast", "", new RandRange(40), TeamMemberSpawn.MemberRole.Support), new IntRange(24, 27), 10);

            //036 Clefable : 98 Magic Guard : 309 Meteor Mash : 236 Moonlight : 227 Encore

            //9//437 Bronzong : 241 Sunny Day : 240 Rain Dance : 286 Imprison : 360 Gyro Ball
            poolSpawn.Spawns.Add(GetTeamMob("bronzong", "", "sunny_day", "rain_dance", "imprison", "gyro_ball", new RandRange(42), "retreater"), new IntRange(24, 28), 10);

            //13//358 Chimecho : 361 Healing Wish : 281 Yawn : 35 Wrap
            poolSpawn.Spawns.Add(GetTeamMob("chimecho", "", "healing_wish", "yawn", "wrap", "", new RandRange(40), TeamMemberSpawn.MemberRole.Support), new IntRange(24, 27), 10);

            //386 Punishment?
            //289 Slaking : 359 Hammer Arm : 227 Encore : 303 Slack Off
            poolSpawn.Spawns.Add(GetTeamMob("slaking", "", "hammer_arm", "encore", "slack_off", "", new RandRange(45), TeamMemberSpawn.MemberRole.Loner), new IntRange(24, 26), 10);

            //1//468 Togekiss : 032 Serene Grace : 055 Hustle : 403 Air Slash : 396 Aura Sphere : 245 Extreme Speed

            //461 Weavile : 251 Beat Up : 289 Snatch : 400 Night Slash : 008 Ice Punch
            poolSpawn.Spawns.Add(GetTeamMob("weavile", "", "beat_up", "snatch", "night_slash", "ice_punch", new RandRange(38), TeamMemberSpawn.MemberRole.Loner), new IntRange(24, 27), 10);

            //63//428 Lopunny : 361 Healing Wish : 203 Endure


            //29//437 Bronzong : 248 Future Sight : 377 Heal Block : 286 Imprison

            //026 Raichu : 031 Lightning Rod : 087 Thunder
            poolSpawn.Spawns.Add(GetTeamMob("raichu", "lightning_rod", "thunder", "", "", "", new RandRange(41)), new IntRange(26, 30), 10);

            //134 Vaporeon : 392 Aqua Ring : 055 Water Gun : 240 Rain Dance
            poolSpawn.Spawns.Add(GetTeamMob("vaporeon", "", "aqua_ring", "water_gun", "rain_dance", "", new RandRange(39), TeamMemberSpawn.MemberRole.Support), new IntRange(26, 30), 10);

            //43//315 Roserade : 311 Weather Ball : 312 Aromatherapy : 345 Magical Leaf
            poolSpawn.Spawns.Add(GetTeamMob("roserade", "", "weather_ball", "aromatherapy", "magical_leaf", "", new RandRange(43)), new IntRange(26, 30), 10);

            //33//464 Rhyperior : 116 Solid Rock : 439 Rock Wrecker : 529 Drill Run


            //018 Pidgeot : 051 Keen Eye : 403 Air Slash : 542 Hurricane
            poolSpawn.Spawns.Add(GetTeamMob("pidgeot", "keen_eye", "sky_attack", "air_slash", "hurricane", "", new RandRange(42)), new IntRange(27, 30), 10);

            //Lucario: Endure : Final Gambit : Reversal

            //60//230 Kingdra : 116 Focus Energy : 434 Draco Meteor : 056 Hydro Pump

            //have something with Haze support a Draco Meteor user, or Baton Pass?  Or Power Swap

            //038 Ninetales : 219 Safeguard : 53 Flamethrower : 286 Imprison
            poolSpawn.Spawns.Add(GetTeamMob("ninetales", "", "safeguard", "flamethrower", "imprison", "", new RandRange(46), TeamMemberSpawn.MemberRole.Support), new IntRange(27, 30), 10);

            //003 Venusaur : 76 Solarbeam : 79 Sleep Powder
            poolSpawn.Spawns.Add(GetTeamMob("venusaur", "", "solar_beam", "sleep_powder", "", "", new RandRange(48)), new IntRange(28, 30), 10);

            //421 Cherrim : 80 Petal Dance, 270 Helping Hand : 241 Sunny Day
            poolSpawn.Spawns.Add(GetTeamMob("cherrim", "", "petal_dance", "helping_hand", "sunny_day", "", new RandRange(47), TeamMemberSpawn.MemberRole.Support), new IntRange(28, 30), 10);

            //330 Flygon : 89 Earthquake : 525 Dragon Tail
            poolSpawn.Spawns.Add(GetTeamMob("flygon", "", "earthquake", "dragon_tail", "", "", new RandRange(50), TeamMemberSpawn.MemberRole.Loner), new IntRange(28, 30), 10);


            poolSpawn.TeamSizes.Add(1, new IntRange(0, 30), 12);
            poolSpawn.TeamSizes.Add(2, new IntRange(5, 10), 3);
            poolSpawn.TeamSizes.Add(2, new IntRange(10, 15), 4);
            poolSpawn.TeamSizes.Add(2, new IntRange(15, 30), 6);

            poolSpawn.TeamSizes.Add(3, new IntRange(15, 30), 3);
            poolSpawn.TeamSizes.Add(3, new IntRange(24, 30), 4);

            floorSegment.ZoneSteps.Add(poolSpawn);


            TileSpawnZoneStep tileSpawn = new TileSpawnZoneStep();
            tileSpawn.Priority = PR_RESPAWN_TRAP;
            tileSpawn.Spawns.Add(new EffectTile("trap_mud", false), new IntRange(0, max_floors), 10);//mud trap
            tileSpawn.Spawns.Add(new EffectTile("trap_warp", true), new IntRange(0, max_floors), 10);//warp trap
            tileSpawn.Spawns.Add(new EffectTile("trap_gust", false), new IntRange(0, max_floors), 10);//gust trap
            tileSpawn.Spawns.Add(new EffectTile("trap_chestnut", false), new IntRange(0, max_floors), 10);//chestnut trap
            tileSpawn.Spawns.Add(new EffectTile("trap_poison", false), new IntRange(0, max_floors), 10);//poison trap
            tileSpawn.Spawns.Add(new EffectTile("trap_slumber", false), new IntRange(0, max_floors), 10);//sleep trap
            tileSpawn.Spawns.Add(new EffectTile("trap_sticky", false), new IntRange(0, max_floors), 10);//sticky trap
            tileSpawn.Spawns.Add(new EffectTile("trap_seal", false), new IntRange(0, max_floors), 10);//seal trap
            tileSpawn.Spawns.Add(new EffectTile("trap_self_destruct", false), new IntRange(0, max_floors), 10);//selfdestruct trap
            tileSpawn.Spawns.Add(new EffectTile("trap_trip", true), new IntRange(0, 15), 10);//trip trap
            tileSpawn.Spawns.Add(new EffectTile("trap_trip", false), new IntRange(15, max_floors), 10);//trip trap
            tileSpawn.Spawns.Add(new EffectTile("trap_hunger", true), new IntRange(0, max_floors), 10);//hunger trap
            tileSpawn.Spawns.Add(new EffectTile("trap_apple", true), new IntRange(0, 15), 3);//apple trap
            tileSpawn.Spawns.Add(new EffectTile("trap_apple", false), new IntRange(15, max_floors), 3);//apple trap
            tileSpawn.Spawns.Add(new EffectTile("trap_pp_leech", true), new IntRange(0, max_floors), 10);//pp-leech trap
            tileSpawn.Spawns.Add(new EffectTile("trap_summon", false), new IntRange(0, max_floors), 10);//summon trap
            tileSpawn.Spawns.Add(new EffectTile("trap_explosion", false), new IntRange(0, max_floors), 10);//explosion trap
            tileSpawn.Spawns.Add(new EffectTile("trap_slow", false), new IntRange(0, max_floors), 10);//slow trap
            tileSpawn.Spawns.Add(new EffectTile("trap_spin", false), new IntRange(0, max_floors), 10);//spin trap
            tileSpawn.Spawns.Add(new EffectTile("trap_grimy", false), new IntRange(0, max_floors), 10);//grimy trap
            tileSpawn.Spawns.Add(new EffectTile("trap_trigger", true), new IntRange(0, max_floors), 20);//trigger trap
                                                                                            //pokemon trap
            tileSpawn.Spawns.Add(new EffectTile("trap_grudge", true), new IntRange(15, max_floors), 10);//grudge trap
                                                                                             //training switch
            floorSegment.ZoneSteps.Add(tileSpawn);


            SpawnList<IGenPriority> appleZoneSpawns = new SpawnList<IGenPriority>();
            appleZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("food_apple"))))), 10);
            SpreadStepZoneStep appleZoneStep = new SpreadStepZoneStep(new SpreadPlanSpaced(new RandRange(3, 5), new IntRange(0, 30)), appleZoneSpawns);//apple
            floorSegment.ZoneSteps.Add(appleZoneStep);
            SpawnList<IGenPriority> bigAppleZoneSpawns = new SpawnList<IGenPriority>();
            bigAppleZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("food_apple_big"))))), 10);
            SpreadStepZoneStep bigAppleZoneStep = new SpreadStepZoneStep(new SpreadPlanSpaced(new RandRange(13, 15), new IntRange(0, 30)), bigAppleZoneSpawns);//big apple
            floorSegment.ZoneSteps.Add(bigAppleZoneStep);
            SpawnList<IGenPriority> leppaZoneSpawns = new SpawnList<IGenPriority>();
            leppaZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("berry_leppa"))))), 10);
            SpreadStepZoneStep leppaZoneStep = new SpreadStepZoneStep(new SpreadPlanSpaced(new RandRange(4, 7), new IntRange(0, 30)), leppaZoneSpawns);//leppa
            floorSegment.ZoneSteps.Add(leppaZoneStep);
            SpawnList<IGenPriority> joySeedZoneSpawns = new SpawnList<IGenPriority>();
            joySeedZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("seed_joy"))))), 10);
            SpreadStepZoneStep joySeedZoneStep = new SpreadStepZoneStep(new SpreadPlanSpaced(new RandRange(12, 30), new IntRange(0, 30)), joySeedZoneSpawns);//joy seed
            floorSegment.ZoneSteps.Add(joySeedZoneStep);
            SpawnList<IGenPriority> keyZoneSpawns = new SpawnList<IGenPriority>();
            keyZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("key", 1))))), 10);
            SpreadStepZoneStep keyZoneStep = new SpreadStepZoneStep(new SpreadPlanQuota(new RandRange(1), new IntRange(0, 5)), keyZoneSpawns);//key
            floorSegment.ZoneSteps.Add(keyZoneStep);
            SpawnList<IGenPriority> assemblyZoneSpawns = new SpawnList<IGenPriority>();
            assemblyZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("machine_assembly_box"))))), 10);
            SpreadStepZoneStep assemblyZoneStep = new SpreadStepZoneStep(new SpreadPlanSpaced(new RandRange(3, 7), new IntRange(6, 30)), assemblyZoneSpawns);//assembly box
            floorSegment.ZoneSteps.Add(assemblyZoneStep);
            SpawnList<IGenPriority> apricornZoneSpawns = new SpawnList<IGenPriority>();
            apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_blue"))))), 10);//blue apricorns
            apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_green"))))), 10);//green apricorns
            apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_brown"))))), 10);//brown apricorns
            apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_purple"))))), 10);//purple apricorns
            apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_red"))))), 10);//red apricorns
            apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_white"))))), 10);//white apricorns
            apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_yellow"))))), 10);//yellow apricorns
            apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_black"))))), 10);//black apricorns
            SpreadStepZoneStep apricornZoneStep = new SpreadStepZoneStep(new SpreadPlanSpaced(new RandRange(4, 7), new IntRange(1, 21)), apricornZoneSpawns);//apricorn (variety)
            floorSegment.ZoneSteps.Add(apricornZoneStep);

            SpawnList<IGenPriority> cleanseZoneSpawns = new SpawnList<IGenPriority>();
            cleanseZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("orb_cleanse"))))), 10);
            SpreadStepZoneStep cleanseZoneStep = new SpreadStepZoneStep(new SpreadPlanQuota(new RandDecay(1, 10, 50), new IntRange(3, 30)), cleanseZoneSpawns);//cleanse orb
            floorSegment.ZoneSteps.Add(cleanseZoneStep);

            SpawnList<IGenPriority> evoZoneSpawns = new SpawnList<IGenPriority>();
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_fire_stone"))))), 10);//Fire Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_thunder_stone"))))), 10);//Thunder Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_water_stone"))))), 10);//Water Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_moon_stone"))))), 10);//Moon Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_dusk_stone"))))), 10);//Dusk Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_dawn_stone"))))), 10);//Dawn Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_shiny_stone"))))), 10);//Shiny Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_leaf_stone"))))), 10);//Leaf Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_ice_stone"))))), 10);//Ice Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_sun_ribbon"))))), 10);//Sun Ribbon
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_lunar_ribbon"))))), 10);//Moon Ribbon
            SpreadStepZoneStep evoItemZoneStep = new SpreadStepZoneStep(new SpreadPlanQuota(new RandRange(2, 4), new IntRange(0, 15)), evoZoneSpawns);//evo items
            floorSegment.ZoneSteps.Add(evoItemZoneStep);


            SpreadRoomZoneStep evoZoneStep = new SpreadRoomZoneStep(PR_GRID_GEN_EXTRA, PR_ROOMS_GEN_EXTRA, new SpreadPlanSpaced(new RandRange(2, 5), new IntRange(3, 30)));
            List<BaseRoomFilter> evoFilters = new List<BaseRoomFilter>();
            evoFilters.Add(new RoomFilterComponent(true, new ImmutableRoom()));
            evoFilters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
            evoZoneStep.Spawns.Add(new RoomGenOption(new RoomGenEvo<MapGenContext>(), new RoomGenEvo<ListMapGenContext>(), evoFilters), 10);
            floorSegment.ZoneSteps.Add(evoZoneStep);


            string[] dexMap = { "bulbasaur", "ivysaur", "venusaur", "charmander", "charmeleon", "charizard", "squirtle", "wartortle", "blastoise", "chikorita", "bayleef", "meganium", "cyndaquil", "quilava", "typhlosion", "totodile", "croconaw", "feraligatr",
                                      "pichu", "pikachu", "raichu", "sentret", "furret", "buneary", "lopunny",
                                      "pidgey", "pidgeotto", "pidgeot", "cleffa", "clefairy", "clefable", "wurmple", "silcoon", "beautifly", "cascoon", "dustox", "togepi", "togetic",
                                      "togekiss", "slakoth", "vigoroth", "slaking", "shinx", "luxio", "luxray", "cherubi", "cherrim", "nincada", "ninjask", "shedinja",
                                      "budew", "roselia", "roserade", "chingling", "chimecho", "eevee", "vaporeon", "jolteon", "flareon", "espeon", "umbreon", "leafeon",
                                      "glaceon", "sylveon", "houndour", "houndoom", "riolu", "lucario", "shellder", "cloyster", "exeggcute", "exeggutor", "aipom", "ambipom",
                                      "paras", "parasect", "finneon", "lumineon", "ralts", "kirlia", "gardevoir", "gallade", "shuppet", "banette", "poochyena",
                                      "mightyena", "swinub", "piloswine", "mamoswine", "lickitung", "lickilicky", "kecleon", "pachirisu", "mankey", "primeape", "meowth", "persian",
                                      "gulpin", "swalot", "carnivine", "scyther", "scizor", "vulpix", "ninetales", "pinsir", "drowzee", "hypno", "bonsly", "sudowoodo",
                                      "chatot", "mime_jr", "mr_mime", "tyrogue", "hitmonlee", "hitmonchan", "hitmontop", "trapinch", "vibrava", "flygon",
                                      "munchlax", "snorlax", "larvitar", "pupitar", "tyranitar", "voltorb", "electrode", "bronzor", "bronzong", "makuhita", "hariyama",
                                      "cubone", "marowak", "murkrow", "honchkrow", "baltoy", "claydol", "magikarp", "gyarados", "bagon", "shelgon", "salamence",
                                      "phanpy", "donphan", "absol", "spinarak", "ariados", "natu", "xatu", "dunsparce", "numel", "camerupt",
                                      "wooper", "quagsire", "onix", "steelix", "smoochum", "jynx", "ditto", "misdreavus", "mismagius", "delibird", "wynaut", "wobbuffet",
                                      "nosepass", "probopass", "slugma", "magcargo", "koffing", "weezing", "mawile", "igglybuff", "jigglypuff", "wigglytuff" };

            {
                //monster houses
                SpreadHouseZoneStep monsterChanceZoneStep = new SpreadHouseZoneStep(PR_HOUSES, new SpreadPlanChance(10, new IntRange(4, 15)));
                monsterChanceZoneStep.HouseStepSpawns.Add(new MonsterHouseStep<ListMapGenContext>(GetAntiFilterList(new ImmutableRoom(), new NoEventRoom())), 10);
                foreach (string key in IterateGummis())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 4);//gummis
                foreach (string key in IterateApricorns())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 4);//apricorns
                monsterChanceZoneStep.Items.Add(new MapItem("evo_fire_stone"), new IntRange(0, 30), 4);//Fire Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_thunder_stone"), new IntRange(0, 30), 4);//Thunder Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_water_stone"), new IntRange(0, 30), 4);//Water Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_moon_stone"), new IntRange(0, 30), 4);//Moon Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_dusk_stone"), new IntRange(0, 30), 4);//Dusk Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_dawn_stone"), new IntRange(0, 30), 4);//Dawn Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_shiny_stone"), new IntRange(0, 30), 4);//Shiny Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_leaf_stone"), new IntRange(0, 30), 4);//Leaf Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_ice_stone"), new IntRange(0, 30), 4);//Ice Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_sun_ribbon"), new IntRange(0, 30), 4);//Sun Ribbon
                monsterChanceZoneStep.Items.Add(new MapItem("evo_lunar_ribbon"), new IntRange(0, 30), 4);//Moon Ribbon
                monsterChanceZoneStep.Items.Add(new MapItem("food_banana"), new IntRange(0, 30), 25);//banana
                foreach (string key in IterateTMs())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 2);//TMs
                monsterChanceZoneStep.Items.Add(new MapItem("loot_nugget"), new IntRange(0, 30), 10);//nugget
                monsterChanceZoneStep.Items.Add(new MapItem("loot_pearl", 1), new IntRange(0, 30), 10);//pearl
                monsterChanceZoneStep.Items.Add(new MapItem("loot_heart_scale", 2), new IntRange(0, 30), 10);//heart scale
                monsterChanceZoneStep.Items.Add(new MapItem("key", 1), new IntRange(0, 30), 10);//key
                monsterChanceZoneStep.Items.Add(new MapItem("machine_recall_box"), new IntRange(0, 30), 10);//link box
                monsterChanceZoneStep.Items.Add(new MapItem("machine_assembly_box"), new IntRange(10, 30), 10);//assembly box
                monsterChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, 30), 10);//ability capsule
                monsterChanceZoneStep.Items.Add(new MapItem("held_friend_bow"), new IntRange(0, 30), 10);//friend bow

                //monsterChanceZoneStep.ItemThemes.Add(new ItemThemeNone(0, new RandRange(5, 11)), new ParamRange(0, 30), 20);
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemThemeNone(50, new RandRange(2, 4))), new IntRange(0, 30), 20);//no theme
                                                                                                                                                                                                                         //monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMoney(500, new ParamRange(5, 11)), new ParamRange(0, 30));
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeType(ItemData.UseType.Learn, true, true, new RandRange(3, 5)), new IntRange(0, 30), 10);//TMs
                monsterChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(GummiState)), true, true, new RandRange(3, 7)), new IntRange(0, 30), 30);//gummis
                monsterChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(RecruitState)), true, true, new RandRange(2, 6)), new IntRange(0, 30), 10);//apricorns
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemStateType(new FlagType(typeof(EvoState)), true, true, new RandRange(2, 4))), new IntRange(0, 10), 40);//evo items
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemStateType(new FlagType(typeof(EvoState)), true, true, new RandRange(2, 4))), new IntRange(10, 20), 20);//evo items
                for (int ii = 0; ii < dexMap.Length; ii++)
                    monsterChanceZoneStep.Mobs.Add(GetHouseMob(dexMap[ii], "wander_smart"), new IntRange(0, 30), 10);//all monsters in the game
                monsterChanceZoneStep.MobThemes.Add(new MobThemeNone(50, new RandRange(7, 13)), new IntRange(19, 30), 10);
                monsterChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.FirstEvo | EvoFlag.NoEvo, new RandRange(7, 13)), new IntRange(0, 10), 10);
                monsterChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.FirstEvo | EvoFlag.NoEvo | EvoFlag.MidEvo, new RandRange(7, 13)), new IntRange(10, 20), 10);
                monsterChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.All, new RandRange(7, 13)), new IntRange(12, 22), 10);
                monsterChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.FinalEvo | EvoFlag.NoEvo | EvoFlag.MidEvo, new RandRange(7, 13)), new IntRange(20, 30), 10);
                floorSegment.ZoneSteps.Add(monsterChanceZoneStep);
            }

            {
                //monster halls
                SpreadHouseZoneStep monsterChanceZoneStep = new SpreadHouseZoneStep(PR_HOUSES, new SpreadPlanChance(10, new IntRange(15, 30)));
                monsterChanceZoneStep.HouseStepSpawns.Add(new MonsterHallStep<ListMapGenContext>(new Loc(11, 9), GetAntiFilterList(new ImmutableRoom(), new NoEventRoom())), 10);
                foreach (string key in IterateGummis())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 4);//gummis
                foreach (string key in IterateApricorns())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 4);//apricorns
                monsterChanceZoneStep.Items.Add(new MapItem("evo_fire_stone"), new IntRange(0, 30), 4);//Fire Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_thunder_stone"), new IntRange(0, 30), 4);//Thunder Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_water_stone"), new IntRange(0, 30), 4);//Water Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_moon_stone"), new IntRange(0, 30), 4);//Moon Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_dusk_stone"), new IntRange(0, 30), 4);//Dusk Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_dawn_stone"), new IntRange(0, 30), 4);//Dawn Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_shiny_stone"), new IntRange(0, 30), 4);//Shiny Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_leaf_stone"), new IntRange(0, 30), 4);//Leaf Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_ice_stone"), new IntRange(0, 30), 4);//Ice Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_sun_ribbon"), new IntRange(0, 30), 4);//Sun Ribbon
                monsterChanceZoneStep.Items.Add(new MapItem("evo_lunar_ribbon"), new IntRange(0, 30), 4);//Moon Ribbon
                monsterChanceZoneStep.Items.Add(new MapItem("food_banana"), new IntRange(0, 30), 25);//banana
                foreach (string key in IterateTMs())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 2);//TMs
                monsterChanceZoneStep.Items.Add(new MapItem("loot_nugget"), new IntRange(0, 30), 10);//nugget
                monsterChanceZoneStep.Items.Add(new MapItem("loot_pearl", 1), new IntRange(0, 30), 10);//pearl
                monsterChanceZoneStep.Items.Add(new MapItem("loot_heart_scale", 2), new IntRange(0, 30), 10);//heart scale
                monsterChanceZoneStep.Items.Add(new MapItem("key", 1), new IntRange(0, 30), 10);//key
                monsterChanceZoneStep.Items.Add(new MapItem("machine_recall_box"), new IntRange(0, 30), 10);//link box
                monsterChanceZoneStep.Items.Add(new MapItem("machine_assembly_box"), new IntRange(10, 30), 10);//assembly box
                monsterChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, 30), 10);//ability capsule
                monsterChanceZoneStep.Items.Add(new MapItem("held_friend_bow"), new IntRange(0, 30), 10);//friend bow

                //monsterChanceZoneStep.ItemThemes.Add(new ItemThemeNone(0, new RandRange(5, 11)), new ParamRange(0, 30), 20);
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemThemeNone(50, new RandRange(2, 4))), new IntRange(0, 30), 20);//no theme
                                                                                                                                                                                                                         //monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMoney(500, new ParamRange(5, 11)), new ParamRange(0, 30));
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeType(ItemData.UseType.Learn, true, true, new RandRange(3, 5)), new IntRange(0, 30), 10);//TMs
                monsterChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(GummiState)), true, true, new RandRange(3, 7)), new IntRange(0, 30), 30);//gummis
                monsterChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(RecruitState)), true, true, new RandRange(2, 6)), new IntRange(0, 30), 10);//apricorns

                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemStateType(new FlagType(typeof(EvoState)), true, true, new RandRange(2, 4))), new IntRange(0, 10), 40);//evo items
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemStateType(new FlagType(typeof(EvoState)), true, true, new RandRange(2, 4))), new IntRange(10, 20), 20);//evo items
                for (int ii = 0; ii < dexMap.Length; ii++)
                    monsterChanceZoneStep.Mobs.Add(GetHouseMob(dexMap[ii], "wander_smart"), new IntRange(0, 30), 10);//all monsters in the game
                monsterChanceZoneStep.MobThemes.Add(new MobThemeNone(50, new RandRange(7, 13)), new IntRange(19, 30), 10);
                monsterChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.FirstEvo | EvoFlag.NoEvo, new RandRange(7, 13)), new IntRange(0, 10), 10);
                monsterChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.FirstEvo | EvoFlag.NoEvo | EvoFlag.MidEvo, new RandRange(7, 13)), new IntRange(10, 20), 10);
                monsterChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.All, new RandRange(7, 13)), new IntRange(12, 22), 10);
                monsterChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.FinalEvo | EvoFlag.NoEvo | EvoFlag.MidEvo, new RandRange(7, 13)), new IntRange(20, 30), 10);
                floorSegment.ZoneSteps.Add(monsterChanceZoneStep);
            }

            {
                SpreadHouseZoneStep chestChanceZoneStep = new SpreadHouseZoneStep(PR_HOUSES, new SpreadPlanQuota(new RandRange(2, 5), new IntRange(6, 30)));
                chestChanceZoneStep.ModStates.Add(new FlagType(typeof(ChestModGenState)));
                chestChanceZoneStep.HouseStepSpawns.Add(new ChestStep<ListMapGenContext>(false, GetAntiFilterList(new ImmutableRoom(), new NoEventRoom())), 10);
                foreach (string key in IterateGummis())
                    chestChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 4);//gummis
                chestChanceZoneStep.Items.Add(new MapItem("apricorn_big"), new IntRange(0, 30), 20);//big apricorn
                chestChanceZoneStep.Items.Add(new MapItem("medicine_elixir"), new IntRange(0, 30), 80);//elixir
                chestChanceZoneStep.Items.Add(new MapItem("medicine_max_elixir"), new IntRange(0, 30), 20);//max elixir
                chestChanceZoneStep.Items.Add(new MapItem("medicine_potion"), new IntRange(0, 30), 20);//potion
                chestChanceZoneStep.Items.Add(new MapItem("medicine_max_potion"), new IntRange(0, 30), 20);//max potion
                chestChanceZoneStep.Items.Add(new MapItem("medicine_full_heal"), new IntRange(0, 30), 20);//full heal
                foreach (string key in IterateXItems())
                    chestChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 15);//X-Items
                foreach (string key in IterateTMs())
                    chestChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 2);//TMs
                chestChanceZoneStep.Items.Add(new MapItem("loot_nugget"), new IntRange(0, 30), 20);//nugget
                chestChanceZoneStep.Items.Add(new MapItem("loot_heart_scale", 3), new IntRange(0, 30), 10);//heart scale
                chestChanceZoneStep.Items.Add(new MapItem("medicine_amber_tear", 1), new IntRange(0, 30), 20);//amber tear
                chestChanceZoneStep.Items.Add(new MapItem("ammo_rare_fossil", 3), new IntRange(0, 30), 20);//rare fossil
                chestChanceZoneStep.Items.Add(new MapItem("seed_reviver"), new IntRange(0, 30), 20);//reviver seed
                chestChanceZoneStep.Items.Add(new MapItem("seed_joy"), new IntRange(0, 30), 20);//joy seed
                chestChanceZoneStep.Items.Add(new MapItem("machine_recall_box"), new IntRange(0, 30), 10);//link box
                chestChanceZoneStep.Items.Add(new MapItem("machine_assembly_box"), new IntRange(10, 30), 10);//assembly box
                chestChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, 30), 10);//ability capsule
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeNone(0, new RandRange(2, 5)), new IntRange(0, 30), 30);
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeRange(true, true, new RandRange(2, 4), "seed_reviver"), new IntRange(0, 30), 10);//reviver seed
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeRange(true, true, new RandRange(1, 4), "seed_joy"), new IntRange(0, 30), 10);//joy seed
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeRange(true, true, new RandRange(1, 3), ItemArray(IterateManmades())), new IntRange(0, 30), 100);//manmade items
                chestChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(EquipState)), true, true, new RandRange(1, 3)), new IntRange(0, 30), 20);//equip
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeRange(true, true, new RandRange(1, 3), ItemArray(IterateTypePlates())), new IntRange(0, 30), 10);//plates
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeType(ItemData.UseType.Learn, true, true, new RandRange(1, 3)), new IntRange(0, 30), 10);//TMs
                chestChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(GummiState)), true, true, new RandRange(2, 5)), new IntRange(0, 30), 20);
                chestChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(RecruitState)), true, true, new RandRange(1)), new IntRange(0, 30), 10);

                floorSegment.ZoneSteps.Add(chestChanceZoneStep);
            }


            {
                SpreadHouseZoneStep chestChanceZoneStep = new SpreadHouseZoneStep(PR_HOUSES, new SpreadPlanQuota(new RandDecay(1, 5, 50), new IntRange(6, 30)));
                chestChanceZoneStep.ModStates.Add(new FlagType(typeof(ChestModGenState)));
                chestChanceZoneStep.HouseStepSpawns.Add(new ChestStep<ListMapGenContext>(true, GetAntiFilterList(new ImmutableRoom(), new NoEventRoom())), 10);
                foreach (string key in IterateGummis())
                    chestChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 4);//gummis
                foreach (string key in IterateApricorns())
                    chestChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 4);//apricorns
                chestChanceZoneStep.Items.Add(new MapItem("apricorn_big"), new IntRange(0, 30), 10);//big apricorn
                chestChanceZoneStep.Items.Add(new MapItem("medicine_elixir"), new IntRange(0, 30), 80);//elixir
                chestChanceZoneStep.Items.Add(new MapItem("medicine_max_elixir"), new IntRange(0, 30), 20);//max elixir
                chestChanceZoneStep.Items.Add(new MapItem("medicine_potion"), new IntRange(0, 30), 20);//potion
                chestChanceZoneStep.Items.Add(new MapItem("medicine_max_potion"), new IntRange(0, 30), 20);//max potion
                chestChanceZoneStep.Items.Add(new MapItem("medicine_full_heal"), new IntRange(0, 30), 20);//full heal
                foreach (string key in IterateXItems())
                    chestChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 15);//X-Items
                foreach (string key in IterateTMs())
                    chestChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 2);//TMs
                chestChanceZoneStep.Items.Add(new MapItem("loot_nugget"), new IntRange(0, 30), 20);//nugget
                chestChanceZoneStep.Items.Add(new MapItem("loot_pearl", 3), new IntRange(0, 30), 5);//pearl
                chestChanceZoneStep.Items.Add(new MapItem("loot_heart_scale", 3), new IntRange(0, 30), 10);//heart scale
                chestChanceZoneStep.Items.Add(new MapItem("medicine_amber_tear", 1), new IntRange(0, 30), 200);//amber tear
                chestChanceZoneStep.Items.Add(new MapItem("ammo_rare_fossil", 3), new IntRange(0, 30), 20);//rare fossil
                chestChanceZoneStep.Items.Add(new MapItem("seed_reviver"), new IntRange(0, 30), 20);//reviver seed
                chestChanceZoneStep.Items.Add(new MapItem("seed_joy"), new IntRange(0, 30), 20);//joy seed
                chestChanceZoneStep.Items.Add(new MapItem("seed_golden"), new IntRange(0, 30), 20);//golden seed
                chestChanceZoneStep.Items.Add(new MapItem("machine_recall_box"), new IntRange(0, 30), 10);//link box
                chestChanceZoneStep.Items.Add(new MapItem("machine_assembly_box"), new IntRange(10, 30), 10);//assembly box
                chestChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, 30), 10);//ability capsule
                chestChanceZoneStep.Items.Add(new MapItem("evo_harmony_scarf"), new IntRange(0, 30), 10);//harmony scarf
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemThemeNone(50, new RandRange(4, 7))), new IntRange(0, 30), 30);
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemThemeRange(true, true, new RandRange(3, 5), "seed_reviver")), new IntRange(0, 30), 10);//reviver seed
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemThemeRange(true, true, new RandRange(2, 5), "seed_joy")), new IntRange(0, 30), 10);//joy seed
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemThemeRange(true, true, new RandRange(1), "seed_golden")), new IntRange(0, 30), 10);//golden seed
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemThemeRange(true, true, new RandRange(1), "evo_harmony_scarf")), new IntRange(20, 30), 20);//Harmony Scarf
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemThemeRange(true, true, new RandRange(3, 6), ItemArray(IterateManmades()))), new IntRange(0, 30), 20);//manmade items
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemStateType(new FlagType(typeof(EquipState)), true, true, new RandRange(3, 6))), new IntRange(0, 30), 20);//equip
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemThemeRange(true, true, new RandRange(3, 6), ItemArray(IterateTypePlates()))), new IntRange(0, 30), 10);//plates
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemThemeType(ItemData.UseType.Learn, true, true, new RandRange(3, 6))), new IntRange(0, 30), 20);//TMs
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemStateType(new FlagType(typeof(GummiState)), true, true, new RandRange(4, 9))), new IntRange(0, 30), 10);//gummis
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(2, 5), "loot_pearl"), new ItemStateType(new FlagType(typeof(RecruitState)), true, true, new RandRange(3, 7))), new IntRange(0, 30), 10);//apricorns

                for (int ii = 0; ii < dexMap.Length; ii++)
                    chestChanceZoneStep.Mobs.Add(GetHouseMob(dexMap[ii], "wander_smart"), new IntRange(0, 30), 10);//all monsters in the game
                chestChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.FirstEvo | EvoFlag.NoEvo, new RandRange(7, 13)), new IntRange(0, 10), 10);
                chestChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.FirstEvo | EvoFlag.NoEvo | EvoFlag.MidEvo, new RandRange(7, 13)), new IntRange(10, 20), 10);
                chestChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.All, new RandRange(7, 13)), new IntRange(12, 22), 10);
                chestChanceZoneStep.MobThemes.Add(new MobThemeTypingSeeded(EvoFlag.FinalEvo | EvoFlag.NoEvo | EvoFlag.MidEvo, new RandRange(7, 13)), new IntRange(20, 30), 10);

                floorSegment.ZoneSteps.Add(chestChanceZoneStep);
            }


            //we need two types of vaults:
            //locked by key
            //locked by switch
            //can they overlap? only the types can overlap but we may need to make do with just that - need to add a new connection type
            //each has its own table of items and foes

            //key vaults
            //removed...

            //switch vaults
            {
                SpreadVaultZoneStep vaultChanceZoneStep = new SpreadVaultZoneStep(PR_SPAWN_ITEMS_EXTRA, PR_SPAWN_MOBS_EXTRA, new SpreadPlanQuota(new RandRange(1, 4), new IntRange(0, 30)));

                //making room for the vault
                {
                    ResizeFloorStep<ListMapGenContext> vaultStep = new ResizeFloorStep<ListMapGenContext>(new Loc(8, 8), Dir8.DownRight, Dir8.UpLeft);
                    vaultChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_ROOMS_PRE_VAULT, vaultStep));
                }
                {
                    ClampFloorStep<ListMapGenContext> vaultStep = new ClampFloorStep<ListMapGenContext>(new Loc(0), new Loc(78, 54));
                    vaultChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_ROOMS_PRE_VAULT_CLAMP, vaultStep));
                }

                // room addition step
                {
                    SpawnList<RoomGen<ListMapGenContext>> detourRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    detourRooms.Add(new RoomGenCross<ListMapGenContext>(new RandRange(4), new RandRange(4), new RandRange(3), new RandRange(3)), 10);
                    SpawnList<PermissiveRoomGen<ListMapGenContext>> detourHalls = new SpawnList<PermissiveRoomGen<ListMapGenContext>>();
                    detourHalls.Add(new RoomGenAngledHall<ListMapGenContext>(0, new RandRange(2, 4), new RandRange(2, 4)), 10);
                    AddConnectedRoomsStep<ListMapGenContext> detours = new AddConnectedRoomsStep<ListMapGenContext>(detourRooms, detourHalls);
                    detours.Amount = new RandRange(8, 10);
                    detours.HallPercent = 100;
                    detours.Filters.Add(new RoomFilterComponent(true, new NoConnectRoom()));
                    detours.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.SwitchVault));
                    detours.RoomComponents.Set(new NoConnectRoom());
                    detours.RoomComponents.Set(new NoEventRoom());
                    detours.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.SwitchVault));
                    detours.HallComponents.Set(new NoConnectRoom());
                    detours.RoomComponents.Set(new NoEventRoom());

                    vaultChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_ROOMS_GEN_EXTRA, detours));
                }

                //sealing the vault
                {
                    SwitchSealStep<ListMapGenContext> vaultStep = new SwitchSealStep<ListMapGenContext>("sealed_block", "tile_switch", true);
                    vaultStep.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.SwitchVault));
                    vaultStep.SwitchFilters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                    vaultStep.SwitchFilters.Add(new RoomFilterComponent(true, new BossRoom()));
                    vaultChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_TILES_GEN_EXTRA, vaultStep));
                }

                //items for the vault
                {
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_elixir"), new IntRange(0, 30), 800);//elixir
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_max_elixir"), new IntRange(0, 30), 100);//max elixir
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_potion"), new IntRange(0, 30), 200);//potion
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_max_potion"), new IntRange(0, 30), 100);//max potion
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_full_heal"), new IntRange(0, 30), 200);//full heal
                    foreach (string key in IterateXItems())
                        vaultChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 50);//X-Items
                    foreach (string key in IterateGummis())
                        vaultChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 200);//gummis
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_amber_tear", 1), new IntRange(0, 30), 2000);//amber tear
                    vaultChanceZoneStep.Items.Add(new MapItem("seed_reviver"), new IntRange(0, 30), 200);//reviver seed
                    vaultChanceZoneStep.Items.Add(new MapItem("seed_joy"), new IntRange(0, 30), 200);//joy seed
                    vaultChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, 30), 200);//ability capsule
                    vaultChanceZoneStep.Items.Add(new MapItem("evo_harmony_scarf"), new IntRange(0, 30), 100);//harmony scarf
                    vaultChanceZoneStep.Items.Add(new MapItem("orb_itemizer"), new IntRange(15, 30), 50);//itemizer orb
                    vaultChanceZoneStep.Items.Add(new MapItem("wand_transfer", 3), new IntRange(15, 30), 50);//transfer wand
                    vaultChanceZoneStep.Items.Add(new MapItem("key", 1), new IntRange(0, 30), 1000);//key
                }

                // item spawnings for the vault
                for (int ii = 0; ii < 30; ii++)
                {
                    //add a PickerSpawner <- PresetMultiRand <- coins
                    List<MapItem> treasures = new List<MapItem>();
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    PickerSpawner<ListMapGenContext, MapItem> treasurePicker = new PickerSpawner<ListMapGenContext, MapItem>(new PresetMultiRand<MapItem>(treasures));


                    SpawnList<IStepSpawner<ListMapGenContext, MapItem>> boxSpawn = new SpawnList<IStepSpawner<ListMapGenContext, MapItem>>();

                    //444      ***    Light Box - 1* items
                    {
                        SpawnList<MapItem> silks = new SpawnList<MapItem>();
                        foreach (string key in IterateSilks())
                            silks.Add(new MapItem(key), 10);

                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_light", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(silks, new RandRange(1)))), 10);
                    }

                    //445      ***    Heavy Box - 2* items
                    {
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_heavy", new SpeciesItemContextSpawner<ListMapGenContext>(new IntRange(2), new RandRange(1))), 10);
                    }

                    ////446      ***    Nifty Box - all high tier TMs, ability capsule, heart scale 9, max potion, full heal, max elixir
                    //{
                    //    SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();

                    //    for (int nn = 588; nn < 700; nn++)
                    //        boxTreasure.Add(new MapItem(nn), 1);//TMs
                    //    boxTreasure.Add(new MapItem("machine_ability_capsule"), 100);//ability capsule
                    //    boxTreasure.Add(new MapItem("loot_heart_scale"), 100);//heart scale
                    //    boxTreasure.Add(new MapItem("medicine_potion"), 60);//potion
                    //    boxTreasure.Add(new MapItem("medicine_max_potion"), 30);//max potion
                    //    boxTreasure.Add(new MapItem("medicine_full_heal"), 100);//full heal
                    //    boxTreasure.Add(new MapItem("medicine_elixir"), 60);//elixir
                    //    boxTreasure.Add(new MapItem("medicine_max_elixir"), 30);//max elixir
                    //    boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_nifty", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 10);
                    //}

                    //447      ***    Dainty Box - Stat ups, wonder gummi, nectar, golden apple, golden banana
                    {
                        SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();

                        foreach (string key in IterateGummis())
                            boxTreasure.Add(new MapItem(key), 1);

                        boxTreasure.Add(new MapItem("boost_protein"), 2);//protein
                        boxTreasure.Add(new MapItem("boost_iron"), 2);//iron
                        boxTreasure.Add(new MapItem("boost_calcium"), 2);//calcium
                        boxTreasure.Add(new MapItem("boost_zinc"), 2);//zinc
                        boxTreasure.Add(new MapItem("boost_carbos"), 2);//carbos
                        boxTreasure.Add(new MapItem("boost_hp_up"), 2);//hp up
                        boxTreasure.Add(new MapItem("boost_nectar"), 2);//nectar

                        boxTreasure.Add(new MapItem("food_apple_perfect"), 10);//perfect apple
                        boxTreasure.Add(new MapItem("food_banana_big"), 10);//big banana
                        boxTreasure.Add(new MapItem("gummi_wonder"), 4);//wonder gummi
                        boxTreasure.Add(new MapItem("seed_joy"), 10);//joy seed
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_dainty", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 3);
                    }

                    //448    Glittery Box - golden apple, amber tear, golden banana, nugget, golden thorn 9
                    {
                        SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();
                        boxTreasure.Add(new MapItem("ammo_golden_thorn"), 10);//golden thorn
                        boxTreasure.Add(new MapItem("medicine_amber_tear"), 10);//Amber Tear
                        boxTreasure.Add(new MapItem("loot_nugget"), 10);//nugget
                        boxTreasure.Add(new MapItem("seed_golden"), 10);//golden seed
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_glittery", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 2);
                    }

                    //449      ***    Deluxe Box - Legendary exclusive items, harmony scarf, golden items, stat ups, wonder gummi, perfect apricorn, max potion/full heal/max elixir
                    //{
                    //    SpeciesItemListSpawner<ListMapGenContext> legendSpawner = new SpeciesItemListSpawner<ListMapGenContext>(new IntRange(1, 3), new RandRange(1));
                    //    legendSpawner.Species.Add(144);
                    //    legendSpawner.Species.Add(145);
                    //    legendSpawner.Species.Add(146);
                    //    legendSpawner.Species.Add(150);
                    //    boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_deluxe", legendSpawner), 5);
                    //}
                    //{
                    //    SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();

                    //    for (int nn = 0; nn < 18; nn++)//Gummi
                    //        boxTreasure.Add(new MapItem(76 + nn), 1);

                    //    boxTreasure.Add(new MapItem("food_apple_golden"), 10);//golden apple
                    //    boxTreasure.Add(new MapItem("food_banana_golden"), 10);//gold banana
                    //    boxTreasure.Add(new MapItem("medicine_amber_tear"), 10);//Amber Tear
                    //    boxTreasure.Add(new MapItem("loot_nugget"), 10);//nugget
                    //    boxTreasure.Add(new MapItem("seed_golden"), 10);//golden seed
                    //    boxTreasure.Add(new MapItem("medicine_max_potion"), 30);//max potion
                    //    boxTreasure.Add(new MapItem("medicine_full_heal"), 100);//full heal
                    //    boxTreasure.Add(new MapItem("medicine_max_elixir"), 30);//max elixir
                    //    boxTreasure.Add(new MapItem("gummi_wonder"), 4);//wonder gummi
                    //    boxTreasure.Add(new MapItem("evo_harmony_scarf"), 1);//harmony scarf
                    //    boxTreasure.Add(new MapItem("apricorn_perfect"), 1);//perfect apricorn
                    //    boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_deluxe", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 10);
                    //}

                    MultiStepSpawner<ListMapGenContext, MapItem> boxPicker = new MultiStepSpawner<ListMapGenContext, MapItem>(new LoopedRand<IStepSpawner<ListMapGenContext, MapItem>>(boxSpawn, new RandRange(1)));

                    //MultiStepSpawner <- PresetMultiRand
                    MultiStepSpawner<ListMapGenContext, MapItem> mainSpawner = new MultiStepSpawner<ListMapGenContext, MapItem>();
                    mainSpawner.Picker = new PresetMultiRand<IStepSpawner<ListMapGenContext, MapItem>>(treasurePicker, boxPicker);
                    vaultChanceZoneStep.ItemSpawners.SetRange(mainSpawner, new IntRange(0, 30));
                }
                vaultChanceZoneStep.ItemAmount.SetRange(new RandRange(5, 7), new IntRange(0, 30));

                // item placements for the vault
                {
                    RandomRoomSpawnStep<ListMapGenContext, MapItem> detourItems = new RandomRoomSpawnStep<ListMapGenContext, MapItem>();
                    detourItems.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.SwitchVault));
                    vaultChanceZoneStep.ItemPlacements.SetRange(detourItems, new IntRange(0, 30));
                }

                // mobs
                // Vault FOES
                {
                    //37//470 Leafeon : 320 Grass Whistle : 076 Solar Beam* : 235 Synthesis : 241 Sunny Day
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("leafeon", "", "grass_whistle", "solar_beam", "synthesis", "sunny_day", 4), new IntRange(0, 10), 10);

                    //43//471 Glaceon : 59 Blizzard : 270 Helping Hand : 258 Hail
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("glaceon", "", "blizzard", "helping_hand", "hail", "", 4), new IntRange(20, 30), 10);

                    //64//479 Rotom : 86 Thunder Wave : 271 Trick : 435 Discharge : 164 Substitute
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("rotom", "", "thunder_wave", "trick", "discharge", "substitute", 4), new IntRange(0, 30), 10);

                    //234 !! Stantler : 43 Leer : 95 Hypnosis : 36 Take Down : 109 Confuse Ray
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("stantler", "", "leer", "hypnosis", "take_down", "confuse_ray", 4), new IntRange(0, 10), 10);

                    //130 !! Gyarados : 153 Moxie : 242 Crunch : 82 Dragon Rage : 240 Rain Dance : 401 Aqua Tail
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("gyarados", "moxie", "crunch", "dragon_rage", "rain_dance", "aqua_tail", 4), new IntRange(8, 15), 10);

                    //53//452 Drapion : 367 Acupressure : 398 Poison Jab : 424 Fire Fang : 565 Fell Stinger
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("drapion", "", "acupressure", "poison_jab", "fire_fang", "fell_stinger", 4), new IntRange(5, 30), 10);

                    //24//262 Mightyena : 372 Assurance : 336 Howl
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("mightyena", "", "assurance", "howl", "", "", 4), new IntRange(0, 30), 10);

                    //24//20 Raticate : 228 Pursuit : 162 Super Fang : 372 Assurance
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("raticate", "", "pursuit", "super_fang", "assurance", "", 4), new IntRange(0, 30), 10);

                    //29//137 Porygon : 160 Conversion : 060 Psybeam : 324 Signal Beam : 033 Tackle
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("porygon", "", "conversion", "psybeam", "signal_beam", "tackle", 4), new IntRange(5, 25), 10);

                    //50//233 Porygon2 : 176 Conversion 2 : 105 Recover : 161 Tri Attack : 111 Defense Curl
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("porygon2", "", "conversion_2", "recover", "tri_attack", "defense_curl", 4), new IntRange(10, 30), 5);

                    //40//474 Porygon-Z : 097 Agility : 433 Trick Room : 435 Discharge
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("porygon_z", "", "agility", "trick_room", "discharge", "", 4), new IntRange(10, 30), 5);

                    //67//474 Porygon-Z : 373 Embargo : 199 Lock-On : 063 Hyper Beam
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("porygon_z", "", "embargo", "lock_on", "hyper_beam", "", 4), new IntRange(10, 30), 5);

                    //115 Kangaskhan : 113 Scrappy : 146 Dizzy Punch : 004 Comet Punch : 99 Rage : 203 Endure
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("kangaskhan", "scrappy", "dizzy_punch", "comet_punch", "rage", "endure", 4), new IntRange(0, 30), 5);

                    //40//224 Octillery : 021 Suction Cups : 097 Sniper : 058 Ice Beam : 060 Psybeam : 190 Octazooka
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("octillery", "sniper", "ice_beam", "psybeam", "octazooka", "", 4), new IntRange(0, 15), 5);

                    //40//208 Steelix : 174 Curse : 360 Gyro Ball : 231 Iron Tail : Dig
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("steelix", "", "curse", "gyro_ball", "iron_tail", "leer", 4), new IntRange(10, 30), 5);

                }
                vaultChanceZoneStep.MobAmount.SetRange(new RandRange(7, 11), new IntRange(0, 30));

                // mob placements
                {
                    PlaceRandomMobsStep<ListMapGenContext> secretMobPlacement = new PlaceRandomMobsStep<ListMapGenContext>();
                    secretMobPlacement.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.SwitchVault));
                    secretMobPlacement.ClumpFactor = 20;
                    vaultChanceZoneStep.MobPlacements.SetRange(secretMobPlacement, new IntRange(0, 30));
                }

                floorSegment.ZoneSteps.Add(vaultChanceZoneStep);
            }

            {
                SpreadBossZoneStep bossChanceZoneStep = new SpreadBossZoneStep(PR_ROOMS_GEN_EXTRA, PR_SPAWN_ITEMS_EXTRA, new SpreadPlanQuota(new RandDecay(0, 4, 50), new IntRange(2, 30)));

                {
                    ResizeFloorStep<ListMapGenContext> resizeStep = new ResizeFloorStep<ListMapGenContext>(new Loc(8, 8), Dir8.DownRight, Dir8.UpLeft);
                    bossChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_ROOMS_PRE_VAULT, resizeStep));
                }
                {
                    ClampFloorStep<ListMapGenContext> vaultStep = new ClampFloorStep<ListMapGenContext>(new Loc(0), new Loc(78, 54));
                    bossChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_ROOMS_PRE_VAULT_CLAMP, vaultStep));
                }

                //BOSS TEAMS
                // no specific items to be used in lv5 dungeons

                string[] customWaterCross = new string[] {    "~~~...~~~",
                                                                      "~~~...~~~",
                                                                      "~~X...X~~",
                                                                      ".........",
                                                                      ".........",
                                                                      ".........",
                                                                      "~~X...X~~",
                                                                      "~~~...~~~",
                                                                      "~~~...~~~"};
                // vespiquen and its underlings
                //416 Vespiquen : 454 Attack Order : 455 Heal Order : 456 Defend Order : 408 Power Gem
                //415 Combee    : 230 Sweet Scent : 366 Tailwind : 450 Bug Bite : 283 Endeavor
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("vespiquen", "", "attack_order", "defend_order", "heal_order", "power_gem", "", new Loc(4, 1)));
                    mobSpawns.Add(GetBossMob("combee", "", "sweet_scent", "tailwind", "bug_bite", "endeavor", "", new Loc(4, 2)));
                    mobSpawns.Add(GetBossMob("combee", "", "sweet_scent", "tailwind", "bug_bite", "endeavor", "", new Loc(1, 4)));
                    mobSpawns.Add(GetBossMob("combee", "", "sweet_scent", "tailwind", "bug_bite", "endeavor", "", new Loc(7, 4)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customWaterCross, new Loc(4, 4), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 14), 10);
                }


                string[] customLavaLake = new string[] {      "X^^.^.^^X",
                                                                      "^^^^.^^^^",
                                                                      "^^^.^.^^^",
                                                                      ".^.....^.",
                                                                      "^.^...^.^",
                                                                      ".^.....^.",
                                                                      "^^^.^.^^^",
                                                                      "^^^^.^^^^",
                                                                      "X^^.^.^^X"};
                // lava plume synergy
                //323 Camerupt : 284 Eruption : 414 Earth Power : 436 Lava Plume : 281 Yawn
                //229 Houndoom : 53 Flamethrower : 46 Roar : 492 Foul Play : 517 Inferno
                //136 Flareon : 83 Fire Spin : 394 Flare Blitz : 98 Quick Attack : 436 Lava Plume
                //059 Arcanine : 257 Heat Wave : 555 Snarl : 245 Extreme Speed : 126 Fire Blast
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("camerupt", "solid_rock", "eruption", "earth_power", "lava_plume", "yawn", "", new Loc(4, 2)));
                    mobSpawns.Add(GetBossMob("houndoom", "flash_fire", "flamethrower", "roar", "foul_play", "inferno", "", new Loc(2, 4)));
                    mobSpawns.Add(GetBossMob("flareon", "flash_fire", "fire_spin", "flare_blitz", "quick_attack", "lava_plume", "", new Loc(4, 6)));
                    mobSpawns.Add(GetBossMob("arcanine", "flash_fire", "heat_wave", "snarl", "extreme_speed", "fire_blast", "", new Loc(6, 4)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customLavaLake, new Loc(4, 4), mobSpawns, true), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(14, 19), 10);
                }



                string[] customJigsaw = new string[] {        "....XXX....",
                                                                      "XX..XXX..XX",
                                                                      "XX.......XX",
                                                                      "...........",
                                                                      "...........",
                                                                      "XX.......XX",
                                                                      "XX..XXX..XX",
                                                                      "....XXX...."};
                //    sand team
                //248 Tyranitar : 444 Stone Edge : 242 Crunch : 103 Screech : 269 Taunt
                //208 Steelix : 446 Stealth Rock : 231 Iron Tail : 91 Dig : 20 Bind
                //185 Sudowoodo : 359 Hammer Arm : 68 Counter : 335 Block : 452 Wood Hammer
                //232 Donphan : 484 Heavy Slam : 523 Bulldoze : 282 Knock Off : 205 Rollout
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("tyranitar", "", "stone_edge", "crunch", "screech", "taunt", "", new Loc(3, 1)));
                    mobSpawns.Add(GetBossMob("steelix", "", "stealth_rock", "iron_tail", "dig", "bind", "", new Loc(7, 1)));
                    mobSpawns.Add(GetBossMob("sudowoodo", "", "hammer_arm", "counter", "block", "wood_hammer", "", new Loc(2, 3)));
                    mobSpawns.Add(GetBossMob("donphan", "", "heavy_slam", "bulldoze", "knock_off", "rollout", "", new Loc(8, 3)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customJigsaw, new Loc(5, 3), mobSpawns, true), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(12, 30), 10);
                }


                string[] customSkyChex = new string[] {       "..___..",
                                                                      "..___..",
                                                                      "__...__",
                                                                      "__...__",
                                                                      "__...__",
                                                                      "..___..",
                                                                      "..___.."};
                //    dragon team
                //149 Dragonite : 63 Hyper Beam : 200 Outrage : 525 Dragon Tail : 355 Roost
                //130 Gyarados : 89 Earthquake : 127 Waterfall : 349 Dragon Dance : 423 Ice Fang
                //142 Aerodactyl : 446 Stealth Rock : 97 Agility : 157 Rock Slide : 469 Wide Guard
                //006 Charizard : 519 Fire Pledge : 82 Dragon Rage : 314 Air Cutter : 257 Heat Wave
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("dragonite", "", "hyper_beam", "dragon_tail", "outrage", "roost", "", new Loc(0, 0)));
                    mobSpawns.Add(GetBossMob("gyarados", "", "earthquake", "waterfall", "dragon_dance", "ice_fang", "", new Loc(6, 0)));
                    mobSpawns.Add(GetBossMob("aerodactyl", "", "stealth_rock", "agility", "rock_slide", "wide_guard", "", new Loc(0, 6)));
                    mobSpawns.Add(GetBossMob("charizard", "", "fire_pledge", "dragon_rage", "air_cutter", "heat_wave", "", new Loc(6, 6)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customSkyChex, new Loc(3, 3), mobSpawns, true), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(23, 30), 10);
                }

                string[] customPillarHalls = new string[] {   ".........",
                                                                      "..X...X..",
                                                                      ".........",
                                                                      ".........",
                                                                      "..X...X..",
                                                                      ".........",
                                                                      ".........",
                                                                      "..X...X..",
                                                                      "........."};
                // dragon? team 2
                //373 Salamence : 434 Draco Meteor : 203 Endure : 82 Dragon Rage : 53 Flamethrower
                //160 Feraligatr : 276 Superpower : 401 Aqua Tail : 423 Ice Fang : 242 Crunch
                //475 Gallade : 370 Close Combat : 348 Leaf Blade : 427 Psycho Cut : 364 Feint
                //430 Honchkrow : 114 Haze : 355 Roost : 511 Quash : 399 Dark Pulse
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("salamence", "", "draco_meteor", "endure", "dragon_rage", "flamethrower", "", new Loc(4, 3)));
                    mobSpawns.Add(GetBossMob("feraligatr", "", "superpower", "aqua_tail", "ice_fang", "crunch", "", new Loc(2, 3)));
                    mobSpawns.Add(GetBossMob("gallade", "", "close_combat", "leaf_blade", "psycho_cut", "feint", "", new Loc(6, 3)));
                    mobSpawns.Add(GetBossMob("honchkrow", "", "haze", "roost", "quash", "dark_pulse", "", new Loc(4, 1)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customPillarHalls, new Loc(4, 5), mobSpawns, true), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(19, 30), 10);
                }


                string[] customSkyDiamond = new string[] {    "XXX_._XXX",
                                                                      "XX__.__XX",
                                                                      "X___.___X",
                                                                      "____.____",
                                                                      ".........",
                                                                      "____.____",
                                                                      "X___.___X",
                                                                      "XX__.__XX",
                                                                      "XXX_._XXX"};
                //337 lunatone : 478 Magic Room : 94 Psychic : 322 Cosmic Power : 585 Moonblast
                //338 solrock : 377 Heal Block : 234 Morning Sun : 322 Cosmic Power : 76 Solar Beam
                //344 claydol : 286 Imprison : 471 Power Split : 153 Explosion : 326 Extrasensory
                //437 bronzong : 219 Safeguard : 319 Metal Sound : 248 Future Sight : 430 Flash Cannon
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("lunatone", "", "magic_room", "psychic", "cosmic_power", "moonblast", "", new Loc(2, 2)));
                    mobSpawns.Add(GetBossMob("solrock", "", "heal_block", "morning_sun", "cosmic_power", "solar_beam", "", new Loc(6, 2)));
                    mobSpawns.Add(GetBossMob("claydol", "", "imprison", "power_split", "explosion", "extrasensory", "", new Loc(2, 6)));
                    mobSpawns.Add(GetBossMob("bronzong", "", "safeguard", "metal_sound", "future_sight", "flash_cannon", "", new Loc(6, 6)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customSkyDiamond, new Loc(4, 4), mobSpawns, true), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(23, 30), 10);
                }



                string[] customPillarCross = new string[] {   ".........",
                                                                      ".XX...XX.",
                                                                      ".XX...XX.",
                                                                      ".........",
                                                                      ".........",
                                                                      ".........",
                                                                      ".XX...XX.",
                                                                      ".XX...XX.",
                                                                      "........."};
                //   All ditto, no impostor
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("ditto", "", "", "", "", "", "", new Loc(4, 1)));
                    mobSpawns.Add(GetBossMob("ditto", "", "", "", "", "", "", new Loc(4, 7)));
                    mobSpawns.Add(GetBossMob("ditto", "", "", "", "", "", "", new Loc(1, 4)));
                    mobSpawns.Add(GetBossMob("ditto", "", "", "", "", "", "", new Loc(7, 4)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customPillarCross, new Loc(4, 4), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }


                string[] customSideWalls = new string[] {     ".........",
                                                                      ".........",
                                                                      "..X...X..",
                                                                      "..X...X..",
                                                                      ".........",
                                                                      "..X...X..",
                                                                      "..X...X..",
                                                                      "........."};
                //   pink wall
                //36 Clefable : 98 Magic Guard : 266 Follow Me : 107 Minimize : 138 Dream Eater : 118 Metronome
                //35 Clefairy : 132 Friend Guard : 322 Cosmic Power : 381 Lucky Chant : 500 Stored Power : 236 Moonlight
                //39 Jigglypuff : 132 Friend Guard : 47 Sing : 50 Disable : 445 Captivate : 496 Round
                //440 Happiny : 132 Friend Guard : 204 Charm : 287 Refresh : 186 Sweet Kiss : 164 Substitute
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("clefable", "magic_guard", "follow_me", "minimize", "dream_eater", "metronome", "", new Loc(4, 3)));
                    mobSpawns.Add(GetBossMob("clefairy", "friend_guard", "moonlight", "cosmic_power", "lucky_chant", "stored_power", "", new Loc(4, 2)));
                    mobSpawns.Add(GetBossMob("jigglypuff", "friend_guard", "sing", "disable", "captivate", "round", "", new Loc(3, 3)));
                    mobSpawns.Add(GetBossMob("happiny", "friend_guard", "charm", "refresh", "sweet_kiss", "substitute", "", new Loc(5, 3)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customSideWalls, new Loc(4, 4), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 14), 10);
                }


                string[] customSemiCovered = new string[] {   ".........",
                                                                      ".........",
                                                                      ".XX...XX.",
                                                                      ".........",
                                                                      ".........",
                                                                      "........."};
                //  Eeveelution 1
                // Vaporeon : Muddy Water : Aqua Ring : Aurora Beam : Helping Hand
                // Jolteon : Thunderbolt : Agility : Signal Beam : Stored Power
                // Flareon : Flare Blitz : Will-O-Wisp : Smog : Helping Hand
                // Sylveon : 585 Moonblast : 219 Safeguard : 247 Shadow Ball : Stored Power
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("vaporeon", "", "muddy_water", "aqua_ring", "aurora_beam", "helping_hand", "", new Loc(4, 1)));
                    mobSpawns.Add(GetBossMob("jolteon", "", "thunderbolt", "agility", "signal_beam", "stored_power", "", new Loc(3, 1)));
                    mobSpawns.Add(GetBossMob("flareon", "", "flare_blitz", "will_o_wisp", "smog", "helping_hand", "", new Loc(5, 1)));
                    mobSpawns.Add(GetBossMob("sylveon", "", "moonblast", "safeguard", "shadow_ball", "stored_power", "", new Loc(4, 0)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customSemiCovered, new Loc(4, 3), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }
                //  Eeveelution 2
                // Espeon : Psyshock : Morning Sun : Dazzling Gleam : Stored Power
                // Leafeon : Leaf Blade : Grass Whistle : X-Scissor : Helping Hand
                // Glaceon : Frost Breath : Barrier : Water Pulse : Stored Power
                // Umbreon : Snarl : Moonlight : Shadow Ball : Helping Hand
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("espeon", "", "psyshock", "morning_sun", "dazzling_gleam", "stored_power", "", new Loc(4, 1)));
                    mobSpawns.Add(GetBossMob("leafeon", "", "leaf_blade", "grass_whistle", "x_scissor", "helping_hand", "", new Loc(3, 1)));
                    mobSpawns.Add(GetBossMob("glaceon", "", "frost_breath", "barrier", "water_pulse", "stored_power", "", new Loc(5, 1)));
                    mobSpawns.Add(GetBossMob("umbreon", "", "snarl", "moonlight", "shadow_ball", "helping_hand", "", new Loc(4, 0)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customSemiCovered, new Loc(4, 3), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }


                string[] customTwoPillars = new string[] {    "...........",
                                                                      "..XX...XX..",
                                                                      "..XX...XX..",
                                                                      "...........",
                                                                      "..........."};
                //   Discharge + volt absorb
                //135 Jolteon : 10 Volt Absorb : 435 Discharge : 97 Agility : 113 Light Screen : 324 Signal Beam
                //417 Pachirisu : 10 Volt Absorb : 266 Follow Me : 162 Super Fang : 435 Discharge : 569 Ion Deluge
                //26 Raichu : 31 Lightning Rod : 447 Grass Knot : 85 Thunderbolt : 411 Focus Blast : 231 Iron Tail
                //101 Electrode : 106 Aftermath : 435 Discharge : 598 Eerie Impulse : 49 Sonic Boom : 268 Charge
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    mobSpawns.Add(GetBossMob("jolteon", "volt_absorb", "discharge", "agility", "light_screen", "signal_beam", "", new Loc(4, 1)));
                    mobSpawns.Add(GetBossMob("pachirisu", "volt_absorb", "follow_me", "super_fang", "discharge", "ion_deluge", "", new Loc(6, 1)));
                    mobSpawns.Add(GetBossMob("raichu", "lightning_rod", "grass_knot", "thunderbolt", "focus_blast", "iron_tail", "", new Loc(2, 3)));
                    mobSpawns.Add(GetBossMob("electrode", "aftermath", "discharge", "eerie_impulse", "sonic_boom", "charge", "", new Loc(8, 3)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customTwoPillars, new Loc(5, 3), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);

                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }

                //   Fossil team with ancient power; omastar has shell smash

                //entry hazards/gradual grind
                //forretress

                // redirection synergy; gyarados+lightningrod?

                //   PP stall team

                //   something with draco meteor - altaria? - also has haze support
                //   maybe an overheat team with haze support


                //sealing the boss room and treasure room
                {
                    BossSealStep<ListMapGenContext> vaultStep = new BossSealStep<ListMapGenContext>("sealed_block", "tile_boss");
                    vaultStep.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.BossLocked));
                    vaultStep.BossFilters.Add(new RoomFilterComponent(false, new BossRoom()));
                    bossChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_TILES_GEN_EXTRA, vaultStep));
                }


                //items for the vault
                {
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_elixir"), new IntRange(0, 30), 800);//elixir
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_max_elixir"), new IntRange(0, 30), 100);//max elixir
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_potion"), new IntRange(0, 30), 200);//potion
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_max_potion"), new IntRange(0, 30), 100);//max potion
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_full_heal"), new IntRange(0, 30), 200);//full heal
                    foreach (string key in IterateXItems())
                        bossChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 50);//X-Items
                    foreach (string key in IterateGummis())
                        bossChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 200);//gummis
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_amber_tear", 1), new IntRange(0, 30), 200);//amber tear
                    bossChanceZoneStep.Items.Add(new MapItem("seed_reviver"), new IntRange(0, 30), 200);//reviver seed
                    bossChanceZoneStep.Items.Add(new MapItem("seed_joy"), new IntRange(0, 30), 200);//joy seed
                    bossChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, 30), 200);//ability capsule
                    bossChanceZoneStep.Items.Add(new MapItem("evo_harmony_scarf"), new IntRange(0, 30), 100);//harmony scarf
                    bossChanceZoneStep.Items.Add(new MapItem("key", 1), new IntRange(0, 30), 1000);//key

                    bossChanceZoneStep.Items.Add(new MapItem("tm_toxic"), new IntRange(0, 30), 5);//TM Toxic
                    bossChanceZoneStep.Items.Add(new MapItem("tm_rock_climb"), new IntRange(0, 30), 5);//TM Rock Climb
                    bossChanceZoneStep.Items.Add(new MapItem("tm_waterfall"), new IntRange(0, 30), 5);//TM Waterfall
                    bossChanceZoneStep.Items.Add(new MapItem("tm_charge_beam"), new IntRange(0, 30), 5);//TM Charge Beam
                    bossChanceZoneStep.Items.Add(new MapItem("tm_hone_claws"), new IntRange(0, 30), 5);//TM Hone Claws
                    bossChanceZoneStep.Items.Add(new MapItem("tm_giga_impact"), new IntRange(0, 30), 5);//TM Giga Impact
                    bossChanceZoneStep.Items.Add(new MapItem("tm_dig"), new IntRange(0, 30), 5);//TM Dig
                    bossChanceZoneStep.Items.Add(new MapItem("tm_safeguard"), new IntRange(0, 30), 5);//TM Safeguard
                    bossChanceZoneStep.Items.Add(new MapItem("tm_work_up"), new IntRange(0, 30), 5);//TM Work Up
                    bossChanceZoneStep.Items.Add(new MapItem("tm_scald"), new IntRange(0, 30), 5);//TM Scald
                    bossChanceZoneStep.Items.Add(new MapItem("tm_u_turn"), new IntRange(0, 30), 5);//TM U-turn
                    bossChanceZoneStep.Items.Add(new MapItem("tm_thunder_wave"), new IntRange(0, 30), 5);//TM Thunder Wave
                    bossChanceZoneStep.Items.Add(new MapItem("tm_roost"), new IntRange(0, 30), 5);//TM Roost
                    bossChanceZoneStep.Items.Add(new MapItem("tm_psyshock"), new IntRange(0, 30), 5);//TM Psyshock
                    bossChanceZoneStep.Items.Add(new MapItem("tm_thunder"), new IntRange(0, 30), 5);//TM Thunder
                    bossChanceZoneStep.Items.Add(new MapItem("tm_x_scissor"), new IntRange(0, 30), 5);//TM X-Scissor
                    bossChanceZoneStep.Items.Add(new MapItem("tm_taunt"), new IntRange(0, 30), 5);//TM Taunt
                    bossChanceZoneStep.Items.Add(new MapItem("tm_ice_beam"), new IntRange(0, 30), 5);//TM Ice Beam
                    bossChanceZoneStep.Items.Add(new MapItem("tm_light_screen"), new IntRange(0, 30), 5);//TM Light Screen
                    bossChanceZoneStep.Items.Add(new MapItem("tm_calm_mind"), new IntRange(0, 30), 5);//TM Calm Mind
                    bossChanceZoneStep.Items.Add(new MapItem("tm_torment"), new IntRange(0, 30), 5);//TM Torment
                    bossChanceZoneStep.Items.Add(new MapItem("tm_strength"), new IntRange(0, 30), 5);//TM Strength
                    bossChanceZoneStep.Items.Add(new MapItem("tm_cut"), new IntRange(0, 30), 5);//TM Cut
                    bossChanceZoneStep.Items.Add(new MapItem("tm_rock_smash"), new IntRange(0, 30), 5);//TM Rock Smash
                    bossChanceZoneStep.Items.Add(new MapItem("tm_bulk_up"), new IntRange(0, 30), 5);//TM Bulk Up
                    bossChanceZoneStep.Items.Add(new MapItem("tm_rest"), new IntRange(0, 30), 5);//TM Rest

                    bossChanceZoneStep.Items.Add(new MapItem("tm_psychic"), new IntRange(0, 30), 5);//TM Psychic
                    bossChanceZoneStep.Items.Add(new MapItem("tm_flamethrower"), new IntRange(0, 30), 5);//TM Flamethrower
                    bossChanceZoneStep.Items.Add(new MapItem("tm_hyper_beam"), new IntRange(0, 30), 5);//TM Hyper Beam
                    bossChanceZoneStep.Items.Add(new MapItem("tm_blizzard"), new IntRange(0, 30), 5);//TM Blizzard
                    bossChanceZoneStep.Items.Add(new MapItem("tm_rock_slide"), new IntRange(0, 30), 5);//TM Rock Slide
                    bossChanceZoneStep.Items.Add(new MapItem("tm_sludge_wave"), new IntRange(0, 30), 5);//TM Sludge Wave
                    bossChanceZoneStep.Items.Add(new MapItem("tm_swords_dance"), new IntRange(0, 30), 5);//TM Swords Dance
                    bossChanceZoneStep.Items.Add(new MapItem("tm_energy_ball"), new IntRange(0, 30), 5);//TM Energy Ball
                    bossChanceZoneStep.Items.Add(new MapItem("tm_fire_blast"), new IntRange(0, 30), 5);//TM Fire Blast
                    bossChanceZoneStep.Items.Add(new MapItem("tm_thunderbolt"), new IntRange(0, 30), 5);//TM Thunderbolt
                    bossChanceZoneStep.Items.Add(new MapItem("tm_shadow_ball"), new IntRange(0, 30), 5);//TM Shadow Ball
                    bossChanceZoneStep.Items.Add(new MapItem("tm_dark_pulse"), new IntRange(0, 30), 5);//TM Dark Pulse
                    bossChanceZoneStep.Items.Add(new MapItem("tm_earthquake"), new IntRange(0, 30), 5);//TM Earthquake
                    bossChanceZoneStep.Items.Add(new MapItem("tm_frost_breath"), new IntRange(0, 30), 5);//TM Frost Breath
                    bossChanceZoneStep.Items.Add(new MapItem("tm_dazzling_gleam"), new IntRange(0, 30), 5);//TM Dazzling Gleam
                    bossChanceZoneStep.Items.Add(new MapItem("tm_snarl"), new IntRange(0, 30), 5);//TM Snarl
                    bossChanceZoneStep.Items.Add(new MapItem("tm_focus_blast"), new IntRange(0, 30), 5);//TM Focus Blast
                    bossChanceZoneStep.Items.Add(new MapItem("tm_overheat"), new IntRange(0, 30), 5);//TM Overheat
                    bossChanceZoneStep.Items.Add(new MapItem("tm_sludge_bomb"), new IntRange(0, 30), 5);//TM Sludge Bomb
                    bossChanceZoneStep.Items.Add(new MapItem("tm_recycle"), new IntRange(0, 30), 5);//TM Solar Beam
                    bossChanceZoneStep.Items.Add(new MapItem("tm_flash_cannon"), new IntRange(0, 30), 5);//TM Flash Cannon
                    bossChanceZoneStep.Items.Add(new MapItem("tm_surf"), new IntRange(0, 30), 5);//TM Surf

                }

                // item spawnings for the vault
                for (int ii = 0; ii < 30; ii++)
                {
                    //add a PickerSpawner <- PresetMultiRand <- coins
                    List<MapItem> treasures = new List<MapItem>();
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(new MapItem("loot_nugget"));
                    PickerSpawner<ListMapGenContext, MapItem> treasurePicker = new PickerSpawner<ListMapGenContext, MapItem>(new PresetMultiRand<MapItem>(treasures));

                    SpawnList<IStepSpawner<ListMapGenContext, MapItem>> boxSpawn = new SpawnList<IStepSpawner<ListMapGenContext, MapItem>>();

                    //444      ***    Light Box - 1* items
                    {
                        SpawnList<MapItem> silks = new SpawnList<MapItem>();
                        foreach (string key in IterateSilks())
                            silks.Add(new MapItem(key), 10);

                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_light", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(silks, new RandRange(1)))), 10);
                    }

                    //445      ***    Heavy Box - 2* items
                    {
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_heavy", new SpeciesItemContextSpawner<ListMapGenContext>(new IntRange(2), new RandRange(1))), 10);
                    }

                    ////446      ***    Nifty Box - all high tier TMs, ability capsule, heart scale 9, max potion, full heal, max elixir
                    //{
                    //    SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();

                    //    for (int nn = 588; nn < 700; nn++)
                    //        boxTreasure.Add(new MapItem(nn), 1);//TMs
                    //    boxTreasure.Add(new MapItem("machine_ability_capsule"), 100);//ability capsule
                    //    boxTreasure.Add(new MapItem("loot_heart_scale"), 100);//heart scale
                    //    boxTreasure.Add(new MapItem("medicine_potion"), 60);//potion
                    //    boxTreasure.Add(new MapItem("medicine_max_potion"), 30);//max potion
                    //    boxTreasure.Add(new MapItem("medicine_full_heal"), 100);//full heal
                    //    boxTreasure.Add(new MapItem("medicine_elixir"), 60);//elixir
                    //    boxTreasure.Add(new MapItem("medicine_max_elixir"), 30);//max elixir
                    //    boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_nifty", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 10);
                    //}

                    //447      ***    Dainty Box - Stat ups, wonder gummi, nectar, golden apple, golden banana
                    {
                        SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();

                        foreach (string key in IterateGummis())
                            boxTreasure.Add(new MapItem(key), 1);

                        boxTreasure.Add(new MapItem("boost_protein"), 2);//protein
                        boxTreasure.Add(new MapItem("boost_iron"), 2);//iron
                        boxTreasure.Add(new MapItem("boost_calcium"), 2);//calcium
                        boxTreasure.Add(new MapItem("boost_zinc"), 2);//zinc
                        boxTreasure.Add(new MapItem("boost_carbos"), 2);//carbos
                        boxTreasure.Add(new MapItem("boost_hp_up"), 2);//hp up
                        boxTreasure.Add(new MapItem("boost_nectar"), 2);//nectar

                        boxTreasure.Add(new MapItem("food_apple_perfect"), 10);//perfect apple
                        boxTreasure.Add(new MapItem("food_banana_big"), 10);//big banana
                        boxTreasure.Add(new MapItem("gummi_wonder"), 4);//wonder gummi
                        boxTreasure.Add(new MapItem("seed_joy"), 10);//joy seed
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_dainty", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 3);
                    }

                    //448    Glittery Box - golden apple, amber tear, golden banana, nugget, golden thorn 9
                    {
                        SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();
                        boxTreasure.Add(new MapItem("ammo_golden_thorn"), 10);//golden thorn
                        boxTreasure.Add(new MapItem("medicine_amber_tear"), 10);//Amber Tear
                        boxTreasure.Add(new MapItem("loot_nugget"), 10);//nugget
                        boxTreasure.Add(new MapItem("seed_golden"), 10);//golden seed
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_glittery", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 2);
                    }

                    //449      ***    Deluxe Box - Legendary exclusive items, harmony scarf, golden items, stat ups, wonder gummi, perfect apricorn, max potion/full heal/max elixir
                    //{
                    //    SpeciesItemListSpawner<ListMapGenContext> legendSpawner = new SpeciesItemListSpawner<ListMapGenContext>(new IntRange(1, 3), new RandRange(1));
                    //    legendSpawner.Species.Add(144);
                    //    legendSpawner.Species.Add(145);
                    //    legendSpawner.Species.Add(146);
                    //    legendSpawner.Species.Add(150);
                    //    boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_deluxe", legendSpawner), 5);
                    //}
                    //{
                    //    SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();

                    //    for (int nn = 0; nn < 18; nn++)//Gummi
                    //        boxTreasure.Add(new MapItem(76 + nn), 1);

                    //    boxTreasure.Add(new MapItem("food_apple_golden"), 10);//golden apple
                    //    boxTreasure.Add(new MapItem("food_banana_golden"), 10);//gold banana
                    //    boxTreasure.Add(new MapItem("medicine_amber_tear"), 10);//Amber Tear
                    //    boxTreasure.Add(new MapItem("loot_nugget"), 10);//nugget
                    //    boxTreasure.Add(new MapItem("seed_golden"), 10);//golden seed
                    //    boxTreasure.Add(new MapItem("medicine_max_potion"), 30);//max potion
                    //    boxTreasure.Add(new MapItem("medicine_full_heal"), 100);//full heal
                    //    boxTreasure.Add(new MapItem("medicine_max_elixir"), 30);//max elixir
                    //    boxTreasure.Add(new MapItem("gummi_wonder"), 4);//wonder gummi
                    //    boxTreasure.Add(new MapItem("evo_harmony_scarf"), 1);//harmony scarf
                    //    boxTreasure.Add(new MapItem("apricorn_perfect"), 1);//perfect apricorn
                    //    boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_deluxe", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 10);
                    //}

                    MultiStepSpawner<ListMapGenContext, MapItem> boxPicker = new MultiStepSpawner<ListMapGenContext, MapItem>(new LoopedRand<IStepSpawner<ListMapGenContext, MapItem>>(boxSpawn, new RandRange(1)));

                    //MultiStepSpawner <- PresetMultiRand
                    MultiStepSpawner<ListMapGenContext, MapItem> mainSpawner = new MultiStepSpawner<ListMapGenContext, MapItem>();
                    mainSpawner.Picker = new PresetMultiRand<IStepSpawner<ListMapGenContext, MapItem>>(treasurePicker, boxPicker);
                    bossChanceZoneStep.ItemSpawners.SetRange(mainSpawner, new IntRange(0, 30));
                }
                bossChanceZoneStep.ItemAmount.SetRange(new RandRange(2, 4), new IntRange(0, 30));

                // item placements for the vault
                {
                    RandomRoomSpawnStep<ListMapGenContext, MapItem> detourItems = new RandomRoomSpawnStep<ListMapGenContext, MapItem>();
                    detourItems.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.BossLocked));
                    bossChanceZoneStep.ItemPlacements.SetRange(detourItems, new IntRange(0, 30));
                }

                floorSegment.ZoneSteps.Add(bossChanceZoneStep);
            }

            SpawnRangeList<IGenPriority> shopZoneSpawns = new SpawnRangeList<IGenPriority>();
            {
                ShopStep<MapGenContext> shop = new ShopStep<MapGenContext>(GetAntiFilterList(new ImmutableRoom(), new NoEventRoom()));
                shop.Personality = 0;
                shop.SecurityStatus = "shop_security";
                shop.Items.Add(new MapItem("berry_oran", 0, 100), 20);//oran
                shop.Items.Add(new MapItem("berry_leppa", 0, 150), 20);//leppa
                shop.Items.Add(new MapItem("berry_lum", 0, 100), 20);//lum
                shop.Items.Add(new MapItem("berry_sitrus", 0, 100), 20);//sitrus
                shop.Items.Add(new MapItem("seed_reviver", 0, 800), 15);//reviver
                shop.Items.Add(new MapItem("seed_ban", 0, 500), 15);//ban
                foreach (string key in IterateApricorns())
                    shop.Items.Add(new MapItem(key, 0, 600), 2);//apricorns
                shop.Items.Add(new MapItem("apricorn_plain", 0, 800), 5);//plain apricorn
                foreach (string key in IteratePinchBerries())
                    shop.Items.Add(new MapItem(key, 0, 600), 3);//pinch berries
                foreach (string key in IterateTypeBerries())
                    shop.Items.Add(new MapItem(key, 0, 100), 1);//type berries
                shop.Items.Add(new MapItem("seed_blast", 0, 350), 20);//blast seed
                shop.Items.Add(new MapItem("seed_joy", 0, 2000), 5);//joy seed

                shop.Items.Add(new MapItem("evo_fire_stone", 0, 2500), 10);//Fire Stone
                shop.Items.Add(new MapItem("evo_thunder_stone", 0, 2500), 10);//Thunder Stone
                shop.Items.Add(new MapItem("evo_water_stone", 0, 2500), 10);//Water Stone
                shop.Items.Add(new MapItem("evo_moon_stone", 0, 3500), 10);//Moon Stone
                shop.Items.Add(new MapItem("evo_dusk_stone", 0, 3500), 10);//Dusk Stone
                shop.Items.Add(new MapItem("evo_dawn_stone", 0, 2500), 10);//Dawn Stone
                shop.Items.Add(new MapItem("evo_shiny_stone", 0, 3500), 10);//Shiny Stone
                shop.Items.Add(new MapItem("evo_leaf_stone", 0, 2500), 10);//Leaf Stone
                shop.Items.Add(new MapItem("evo_ice_stone", 0, 2500), 10);//Ice Stone

                shop.ItemThemes.Add(new ItemThemeNone(100, new RandRange(3, 9)), 10);
                shop.ItemThemes.Add(new ItemStateType(new FlagType(typeof(EvoState)), false, true, new RandRange(3, 5)), 10);//evo items

                // 213 Shuckle : 126 Contrary : 380 Gastro Acid : 230 Sweet Scent : 450 Bug Bite : 92 Toxic
                shop.StartMob = GetShopMob("shuckle", "contrary", "gastro_acid", "sweet_scent", "bug_bite", "toxic", new string[] { "xcl_family_shuckle_00", "xcl_family_shuckle_01", "xcl_family_shuckle_02", "xcl_family_shuckle_03" }, 0);
                {
                    // 213 Shuckle : 126 Contrary : 380 Gastro Acid : 230 Sweet Scent : 450 Bug Bite : 92 Toxic
                    shop.Mobs.Add(GetShopMob("shuckle", "contrary", "gastro_acid", "sweet_scent", "bug_bite", "toxic", new string[] { "xcl_family_shuckle_00", "xcl_family_shuckle_01", "xcl_family_shuckle_02", "xcl_family_shuckle_03" }, -1), 5);
                    // 213 Shuckle : 126 Contrary : 564 Sticky Web : 611 Infestation : 189 Mud-Slap : 522 Struggle Bug
                    shop.Mobs.Add(GetShopMob("shuckle", "contrary", "sticky_web", "infestation", "mud_slap", "struggle_bug", new string[] { "xcl_family_shuckle_00", "xcl_family_shuckle_01", "xcl_family_shuckle_02", "xcl_family_shuckle_03" }, -1), 5);
                    // 213 Shuckle : 126 Contrary : 201 Sandstorm : 564 Sticky Web : 446 Stealth Rock : 88 Rock Throw
                    shop.Mobs.Add(GetShopMob("shuckle", "sturdy", "sandstorm", "sticky_web", "stealth_rock", "rock_throw", new string[] { "xcl_family_shuckle_00", "xcl_family_shuckle_01", "xcl_family_shuckle_02", "xcl_family_shuckle_03" }, -1, "shuckle"), 10);
                    // 213 Shuckle : 5 Sturdy : 379 Power Trick : 504 Shell Smash : 205 Rollout : 360 Gyro Ball
                    shop.Mobs.Add(GetShopMob("shuckle", "sturdy", "power_trick", "shell_smash", "rollout", "gyro_ball", new string[] { "xcl_family_shuckle_00", "xcl_family_shuckle_01", "xcl_family_shuckle_02", "xcl_family_shuckle_03" }, -1, "shuckle"), 10);
                    // 213 Shuckle : 5 Sturdy : 379 Power Trick : 450 Bug Bite : 444 Stone Edge : 523 Bulldoze
                    shop.Mobs.Add(GetShopMob("shuckle", "sturdy", "power_trick", "bug_bite", "stone_edge", "bulldoze", new string[] { "xcl_family_shuckle_00", "xcl_family_shuckle_01", "xcl_family_shuckle_02", "xcl_family_shuckle_03" }, -1, "shuckle"), 5);
                }

                shopZoneSpawns.Add(new GenPriority<GenStep<MapGenContext>>(PR_SHOPS, shop), new IntRange(2, 25), 10);
            }
            {
                ShopStep<MapGenContext> shop = new ShopStep<MapGenContext>(GetAntiFilterList(new ImmutableRoom(), new NoEventRoom()));
                shop.Personality = 0;
                shop.SecurityStatus = "shop_security";
                shop.Items.Add(new MapItem("berry_oran", 0, 100), 20);//oran
                shop.Items.Add(new MapItem("berry_leppa", 0, 150), 20);//leppa
                shop.Items.Add(new MapItem("berry_lum", 0, 100), 20);//lum
                shop.Items.Add(new MapItem("berry_sitrus", 0, 100), 20);//sitrus
                shop.Items.Add(new MapItem("seed_reviver", 0, 800), 15);//reviver
                shop.Items.Add(new MapItem("seed_ban", 0, 500), 15);//ban
                foreach (string key in IterateApricorns())
                    shop.Items.Add(new MapItem(key, 0, 600), 2);//apricorns
                shop.Items.Add(new MapItem("apricorn_plain", 0, 800), 5);//plain apricorn
                foreach (string key in IteratePinchBerries())
                    shop.Items.Add(new MapItem(key, 0, 600), 3);//pinch berries
                foreach (string key in IterateTypeBerries())
                    shop.Items.Add(new MapItem(key, 0, 100), 1);//type berries
                shop.Items.Add(new MapItem("seed_blast", 0, 350), 20);//blast seed
                shop.Items.Add(new MapItem("seed_joy", 0, 2000), 5);//joy seed


                shop.Items.Add(new MapItem("tm_psychic", 0, 5000), 2);//TM Psychic
                shop.Items.Add(new MapItem("tm_flamethrower", 0, 5000), 2);//TM Flamethrower
                shop.Items.Add(new MapItem("tm_hyper_beam", 0, 5000), 2);//TM Hyper Beam
                shop.Items.Add(new MapItem("tm_blizzard", 0, 5000), 2);//TM Blizzard
                shop.Items.Add(new MapItem("tm_rock_slide", 0, 5000), 2);//TM Rock Slide
                shop.Items.Add(new MapItem("tm_sludge_wave", 0, 5000), 2);//TM Sludge Wave
                shop.Items.Add(new MapItem("tm_swords_dance", 0, 4000), 2);//TM Swords Dance
                shop.Items.Add(new MapItem("tm_energy_ball", 0, 5000), 2);//TM Energy Ball
                shop.Items.Add(new MapItem("tm_fire_blast", 0, 5000), 2);//TM Fire Blast
                shop.Items.Add(new MapItem("tm_thunderbolt", 0, 5000), 2);//TM Thunderbolt
                shop.Items.Add(new MapItem("tm_shadow_ball", 0, 5000), 2);//TM Shadow Ball
                shop.Items.Add(new MapItem("tm_dark_pulse", 0, 5000), 2);//TM Dark Pulse
                shop.Items.Add(new MapItem("tm_earthquake", 0, 4000), 2);//TM Earthquake
                shop.Items.Add(new MapItem("tm_frost_breath", 0, 4000), 2);//TM Frost Breath
                shop.Items.Add(new MapItem("tm_dazzling_gleam", 0, 5000), 2);//TM Dazzling Gleam
                shop.Items.Add(new MapItem("tm_snarl", 0, 4000), 2);//TM Snarl
                shop.Items.Add(new MapItem("tm_focus_blast", 0, 5000), 2);//TM Focus Blast
                shop.Items.Add(new MapItem("tm_overheat", 0, 5000), 2);//TM Overheat
                shop.Items.Add(new MapItem("tm_sludge_bomb", 0, 5000), 2);//TM Sludge Bomb
                shop.Items.Add(new MapItem("tm_recycle", 0, 5000), 2);//TM Solar Beam
                shop.Items.Add(new MapItem("tm_flash_cannon", 0, 5000), 2);//TM Flash Cannon
                shop.Items.Add(new MapItem("tm_surf", 0, 5000), 2);//TM Surf

                foreach (string key in IterateTypeBoosters())
                    shop.Items.Add(new MapItem(key, 0, 2000), 5);//type items

                shop.ItemThemes.Add(new ItemThemeNone(100, new RandRange(3, 9)), 10);
                shop.ItemThemes.Add(new ItemThemeRange(false, true, new RandRange(3, 5), ItemArray(IterateTypeBoosters())), 10);//type items
                shop.ItemThemes.Add(new ItemThemeType(ItemData.UseType.Learn, false, true, new RandRange(3, 6)), 10);//TMs

                // 352 Kecleon : 16 color change : 485 synchronoise : 20 bind : 103 screech : 86 thunder wave
                shop.StartMob = GetShopMob("kecleon", "color_change", "synchronoise", "bind", "screech", "thunder_wave", new string[] { "xcl_family_kecleon_00", "xcl_family_kecleon_01", "xcl_family_kecleon_04" }, 0);
                {
                    // 352 Kecleon : 16 color change : 485 synchronoise : 20 bind : 103 screech : 86 thunder wave
                    shop.Mobs.Add(GetShopMob("kecleon", "color_change", "synchronoise", "bind", "screech", "thunder_wave", new string[] { "xcl_family_kecleon_00", "xcl_family_kecleon_01", "xcl_family_kecleon_04" }, -1), 10);
                    // 352 Kecleon : 16 color change : 485 synchronoise : 20 bind : 50 disable : 374 fling
                    shop.Mobs.Add(GetShopMob("kecleon", "color_change", "synchronoise", "bind", "disable", "fling", new string[] { "xcl_family_kecleon_00", "xcl_family_kecleon_01", "xcl_family_kecleon_04" }, -1), 10);
                    // 352 Kecleon : 168 protean : 425 shadow sneak : 246 ancient power : 510 incinerate : 168 thief
                    shop.Mobs.Add(GetShopMob("kecleon", "protean", "shadow_sneak", "ancient_power", "incinerate", "thief", new string[] { "xcl_family_kecleon_00", "xcl_family_kecleon_01", "xcl_family_kecleon_04" }, -1, "shuckle"), 10);
                    // 352 Kecleon : 168 protean : 332 aerial ace : 421 shadow claw : 60 psybeam : 364 feint
                    shop.Mobs.Add(GetShopMob("kecleon", "protean", "aerial_ace", "shadow_claw", "psybeam", "feint", new string[] { "xcl_family_kecleon_00", "xcl_family_kecleon_01", "xcl_family_kecleon_04" }, -1, "shuckle"), 10);
                }

                shopZoneSpawns.Add(new GenPriority<GenStep<MapGenContext>>(PR_SHOPS, shop), new IntRange(25, 30), 10);
            }
            {
                ShopStep<MapGenContext> shop = new ShopStep<MapGenContext>(GetAntiFilterList(new ImmutableRoom(), new NoEventRoom()));
                shop.Personality = 1;
                shop.SecurityStatus = "shop_security";
                shop.Items.Add(new MapItem("orb_weather", 0, 300), 20);//weather orb
                shop.Items.Add(new MapItem("orb_mobile", 0, 600), 20);//mobile orb
                shop.Items.Add(new MapItem("orb_luminous", 0, 600), 20);//luminous orb
                shop.Items.Add(new MapItem("orb_fill_in", 0, 400), 20);//fill-in orb
                shop.Items.Add(new MapItem("orb_all_aim", 0, 300), 20);//all-aim orb
                shop.Items.Add(new MapItem("orb_cleanse", 0, 300), 20);//cleanse orb
                shop.Items.Add(new MapItem("orb_one_shot", 0, 600), 20);//one-shot orb
                shop.Items.Add(new MapItem("orb_endure", 0, 500), 20);//endure orb
                shop.Items.Add(new MapItem("orb_pierce", 0, 400), 20);//pierce orb
                shop.Items.Add(new MapItem("orb_stayaway", 0, 600), 20);//stayaway orb
                shop.Items.Add(new MapItem("orb_all_protect", 0, 600), 20);//all protect orb
                shop.Items.Add(new MapItem("orb_trap_see", 0, 300), 20);//trap-see orb
                shop.Items.Add(new MapItem("orb_trapbust", 0, 300), 20);//trapbust orb
                shop.Items.Add(new MapItem("orb_slumber", 0, 500), 20);//slumber orb
                shop.Items.Add(new MapItem("orb_totter", 0, 400), 20);//totter orb
                shop.Items.Add(new MapItem("orb_petrify", 0, 400), 20);//petrify orb
                shop.Items.Add(new MapItem("orb_freeze", 0, 400), 20);//freeze orb
                shop.Items.Add(new MapItem("orb_spurn", 0, 500), 20);//spurn orb
                shop.Items.Add(new MapItem("orb_foe_hold", 0, 500), 20);//foe-hold orb
                shop.Items.Add(new MapItem("orb_nullify", 0, 400), 20);//nullify orb
                shop.Items.Add(new MapItem("orb_all_dodge", 0, 300), 20);//all-dodge orb
                shop.Items.Add(new MapItem("orb_one_room", 0, 500), 20);//one-room orb
                shop.Items.Add(new MapItem("orb_slow", 0, 400), 20);//slow orb
                shop.Items.Add(new MapItem("orb_rebound", 0, 400), 20);//rebound orb
                shop.Items.Add(new MapItem("orb_mirror", 0, 400), 20);//mirror orb
                shop.Items.Add(new MapItem("orb_foe_seal", 0, 400), 20);//foe-seal orb
                shop.Items.Add(new MapItem("orb_halving", 0, 600), 20);//halving orb
                shop.Items.Add(new MapItem("orb_rollcall", 0, 300), 20);//rollcall orb
                shop.Items.Add(new MapItem("orb_mug", 0, 300), 20);//mug orb

                shop.Items.Add(new MapItem("wand_path", 3, 180), 40);//path wand
                shop.Items.Add(new MapItem("wand_pounce", 3, 150), 40);//pounce wand
                shop.Items.Add(new MapItem("wand_whirlwind", 3, 150), 40);//whirlwind wand
                shop.Items.Add(new MapItem("wand_switcher", 3, 150), 40);//switcher wand
                shop.Items.Add(new MapItem("wand_lure", 3, 120), 40);//lure wand
                shop.Items.Add(new MapItem("wand_fear", 3, 150), 40);//slow wand
                shop.Items.Add(new MapItem("wand_topsy_turvy", 3, 150), 40);//topsy-turvy wand
                shop.Items.Add(new MapItem("wand_warp", 3, 150), 40);//warp wand
                shop.Items.Add(new MapItem("wand_purge", 3, 120), 40);//purge wand
                shop.Items.Add(new MapItem("wand_lob", 3, 120), 40);//lob wand

                shop.Items.Add(new MapItem("evo_fire_stone", 0, 2500), 50);//Fire Stone
                shop.Items.Add(new MapItem("evo_thunder_stone", 0, 2500), 50);//Thunder Stone
                shop.Items.Add(new MapItem("evo_water_stone", 0, 2500), 50);//Water Stone
                shop.Items.Add(new MapItem("evo_moon_stone", 0, 3500), 50);//Moon Stone
                shop.Items.Add(new MapItem("evo_dusk_stone", 0, 3500), 50);//Dusk Stone
                shop.Items.Add(new MapItem("evo_dawn_stone", 0, 2500), 50);//Dawn Stone
                shop.Items.Add(new MapItem("evo_shiny_stone", 0, 3500), 50);//Shiny Stone
                shop.Items.Add(new MapItem("evo_leaf_stone", 0, 2500), 50);//Leaf Stone
                shop.Items.Add(new MapItem("evo_ice_stone", 0, 2500), 50);//Ice Stone
                shop.Items.Add(new MapItem("evo_sun_ribbon", 0, 3500), 50);//Sun Ribbon
                shop.Items.Add(new MapItem("evo_lunar_ribbon", 0, 3500), 50);//Moon Ribbon

                shop.Items.Add(new MapItem("held_metal_coat", 0, 3500), 40);//Metal Coat
                foreach (string key in IterateTypeBoosters())
                    shop.Items.Add(new MapItem(key, 0, 2000), 10);//type items


                shop.ItemThemes.Add(new ItemThemeNone(100, new RandRange(3, 9)), 10);
                shop.ItemThemes.Add(new ItemStateType(new FlagType(typeof(EvoState)), false, true, new RandRange(3, 5)), 10);//evo items
                shop.ItemThemes.Add(new ItemThemeRange(false, true, new RandRange(3, 5), ItemArray(IterateTypePlates())), 10);//type items

                // Cleffa : 98 Magic Guard : 118 Metronome : 47 Sing : 204 Charm : 313 Fake Tears
                {
                    MobSpawn post_mob = new MobSpawn();
                    post_mob.BaseForm = new MonsterID("cleffa", 0, "normal", Gender.Unknown);
                    post_mob.Tactic = "shopkeeper";
                    post_mob.Level = new RandRange(5);
                    post_mob.Intrinsic = "magic_guard";
                    post_mob.SpecifiedSkills.Add("metronome");
                    post_mob.SpecifiedSkills.Add("sing");
                    post_mob.SpecifiedSkills.Add("charm");
                    post_mob.SpecifiedSkills.Add("fake_tears");
                    post_mob.SpawnFeatures.Add(new MobSpawnDiscriminator(1));
                    post_mob.SpawnFeatures.Add(new MobSpawnInteractable(new BattleScriptEvent("ShopkeeperInteract")));
                    post_mob.SpawnFeatures.Add(new MobSpawnLuaTable("{ Role = \"Shopkeeper\" }"));
                    shop.StartMob = post_mob;
                }
                {
                    // 35 Clefairy : 132 Friend Guard : 282 Knock Off : 107 Minimize : 236 Moonlight : 277 Magic Coat
                    shop.Mobs.Add(GetShopMob("clefairy", "friend_guard", "knock_off", "minimize", "moonlight", "magic_coat", new string[] { "xcl_family_clefairy_00", "xcl_family_clefairy_03" }, -1), 10);
                    // 36 Clefable : 109 Unaware : 118 Metronome : 500 Stored Power : 343 Covet : 271 Trick
                    shop.Mobs.Add(GetShopMob("clefable", "unaware", "metronome", "stored_power", "covet", "trick", new string[] { "xcl_family_clefairy_00", "xcl_family_clefairy_03" }, -1), 5);
                    // 36 Clefable : 98 Magic Guard : 118 Metronome : 213 Attract : 282 Knock Off : 266 Follow Me
                    shop.Mobs.Add(GetShopMob("clefable", "magic_guard", "metronome", "attract", "knock_off", "follow_me", new string[] { "xcl_family_clefairy_00", "xcl_family_clefairy_03" }, -1), 5);
                }

                shopZoneSpawns.Add(new GenPriority<GenStep<MapGenContext>>(PR_SHOPS, shop), new IntRange(15, 30), 10);
            }
            SpreadStepRangeZoneStep shopZoneStep = new SpreadStepRangeZoneStep(new SpreadPlanSpaced(new RandRange(4, 14), new IntRange(2, 28)), shopZoneSpawns);
            shopZoneStep.ModStates.Add(new FlagType(typeof(ShopModGenState)));
            floorSegment.ZoneSteps.Add(shopZoneStep);


            for (int ii = 0; ii < 30; ii++)
            {
                GridFloorGen layout = new GridFloorGen();

                //Floor settings
                MapDataStep<MapGenContext> floorData = new MapDataStep<MapGenContext>();
                floorData.TimeLimit = 1500;
                if (ii < 5)
                    floorData.Music = "B01. Demonstration.ogg";
                else if (ii < 9)
                    floorData.Music = "B18. Faulted Cliffs.ogg";
                else if (ii < 14)
                    floorData.Music = "B07. Flyaway Cliffs.ogg";
                else if (ii < 19)
                    floorData.Music = "B11. Igneous Tunnel.ogg";
                else if (ii < 24)
                    floorData.Music = "Fortune Ravine.ogg";
                else
                    floorData.Music = "B14. Champion Road.ogg";

                floorData.CharSight = Map.SightRange.Dark;

                if (ii < 11)
                    floorData.TileSight = Map.SightRange.Clear;
                else if (ii < 26)
                    floorData.TileSight = Map.SightRange.Dark;

                layout.GenSteps.Add(PR_FLOOR_DATA, floorData);

                //Tilesets
                if (ii < 5)
                    AddTextureData(layout, "purity_forest_2_wall", "purity_forest_2_floor", "purity_forest_2_secondary", "normal");
                else if (ii < 9)
                    AddTextureData(layout, "western_cave_2_wall", "western_cave_2_floor", "western_cave_2_secondary", "water");
                else if (ii < 11)
                    AddTextureData(layout, "mt_travail_wall", "mt_travail_floor", "mt_travail_secondary", "ground");
                else if (ii < 14)
                    AddTextureData(layout, "quicksand_unused_wall", "quicksand_unused_floor", "quicksand_unused_secondary", "fairy");
                else if (ii < 17)
                    AddTextureData(layout, "mt_blaze_wall", "mt_blaze_floor", "mt_blaze_secondary", "fire");
                else if (ii < 19)
                    AddTextureData(layout, "magma_cavern_2_wall", "magma_cavern_2_floor", "magma_cavern_2_secondary", "fire");
                else if (ii < 21)
                    AddTextureData(layout, "high_cave_area_wall", "high_cave_area_floor", "high_cave_area_secondary", "steel");
                else if (ii < 24)
                {
                    if (ii < 23)
                        AddTextureData(layout, "northwind_field_wall", "high_cave_area_floor", "high_cave_area_secondary", "ice");
                    else
                        AddTextureData(layout, "northwind_field_wall", "high_cave_area_floor", "high_cave_area_secondary", "ice");
                }
                else
                    AddTextureData(layout, "buried_relic_2_sky_wall", "buried_relic_2_sky_floor", "buried_relic_2_sky_secondary", "steel", true);

                //wonder tiles
                RandRange wonderTileRange;
                if (ii < 10)
                    wonderTileRange = new RandRange(2, 4);
                else if (ii < 18)
                    wonderTileRange = new RandRange(1, 3);
                else
                    wonderTileRange = new RandRange(4);

                if (ii < 18)
                    AddSingleTrapStep(layout, wonderTileRange, "tile_wonder", true);
                else
                    AddSingleTrapStep(layout, wonderTileRange, "tile_wonder", false);

                //traps
                RandRange trapRange = new RandRange(0);
                if (ii < 10)
                    trapRange = new RandRange(6, 9);
                else if (ii < 15)
                    trapRange = new RandRange(8, 12);
                else if (ii < 20)
                    trapRange = new RandRange(10, 14);
                else if (ii < 25)
                    trapRange = new RandRange(12, 16);
                else
                    trapRange = new RandRange(10, 14);
                AddTrapsSteps(layout, trapRange);


                //money - 25,725P to 54,600P
                if (ii < 5)
                    AddMoneyData(layout, new RandRange(3, 7));
                else if (ii < 10)
                    AddMoneyData(layout, new RandRange(3, 8));
                else if (ii < 20)
                    AddMoneyData(layout, new RandRange(4, 9));
                else
                    AddMoneyData(layout, new RandRange(6, 10));

                //items
                if (ii < 5)
                    AddItemData(layout, new RandRange(3, 5), 25);
                else if (ii < 10)
                    AddItemData(layout, new RandRange(3, 6), 25);
                else
                    AddItemData(layout, new RandRange(4, 7), 25);

                List<MapItem> specificSpawns = new List<MapItem>();
                if (ii == 0)
                    specificSpawns.Add(new MapItem("apricorn_plain"));//Plain Apricorn
                if (ii == 29)
                    specificSpawns.Add(new MapItem("seed_reviver"));//Reviver Seed

                RandomRoomSpawnStep<MapGenContext, MapItem> specificItemZoneStep = new RandomRoomSpawnStep<MapGenContext, MapItem>(new PickerSpawner<MapGenContext, MapItem>(new PresetMultiRand<MapItem>(specificSpawns)));
                specificItemZoneStep.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                layout.GenSteps.Add(PR_SPAWN_ITEMS, specificItemZoneStep);



                //Add:
                // hidden wall items (silver spikes, rare fossils, wands, orbs, plates) - never add anything score-affecting when it comes to roguelockable dungeons
                // No valuable items, no chests, no distinct TMs.  maybe held items...

                SpawnList<MapItem> wallSpawns = new SpawnList<MapItem>();
                wallSpawns.Add(new MapItem("loot_heart_scale", 1), 50);//heart scale
                wallSpawns.Add(new MapItem("ammo_silver_spike", 3), 20);//silver spike
                wallSpawns.Add(new MapItem("ammo_rare_fossil", 3), 20);//rare fossil
                wallSpawns.Add(new MapItem("ammo_corsola_twig", 2), 20);//corsola spike
                wallSpawns.Add(new MapItem("berry_jaboca"), 10);//jaboca berry
                wallSpawns.Add(new MapItem("berry_rowap"), 10);//rowap berry
                wallSpawns.Add(new MapItem("wand_path", 1), 10);//path wand
                wallSpawns.Add(new MapItem("wand_fear", 3), 10);//fear wand
                wallSpawns.Add(new MapItem("wand_switcher", 2), 10);//switcher wand
                wallSpawns.Add(new MapItem("wand_whirlwind", 2), 10);//whirlwind wand

                wallSpawns.Add(new MapItem("orb_slumber"), 10);//Slumber Orb
                wallSpawns.Add(new MapItem("orb_slow"), 10);//Slow
                wallSpawns.Add(new MapItem("orb_totter"), 10);//Totter
                wallSpawns.Add(new MapItem("orb_spurn"), 10);//Spurn
                wallSpawns.Add(new MapItem("orb_stayaway"), 10);//Stayaway
                wallSpawns.Add(new MapItem("orb_pierce"), 10);//Pierce
                foreach (string key in IterateTypePlates())
                    wallSpawns.Add(new MapItem(key), 1);

                TerrainSpawnStep<MapGenContext, MapItem> wallItemZoneStep = new TerrainSpawnStep<MapGenContext, MapItem>(new Tile("wall"));
                wallItemZoneStep.Spawn = new PickerSpawner<MapGenContext, MapItem>(new LoopedRand<MapItem>(wallSpawns, new RandRange(6, 10)));
                layout.GenSteps.Add(PR_SPAWN_ITEMS, wallItemZoneStep);

                if (ii >= 5 && ii < 15)
                {
                    //pearls in the water
                    SpawnList<MapItem> waterSpawns = new SpawnList<MapItem>();
                    waterSpawns.Add(new MapItem("loot_pearl", 1), 50);//pearl
                    TerrainSpawnStep<MapGenContext, MapItem> waterItemZoneStep = new TerrainSpawnStep<MapGenContext, MapItem>(new Tile("water"));
                    waterItemZoneStep.Spawn = new PickerSpawner<MapGenContext, MapItem>(new LoopedRand<MapItem>(waterSpawns, new RandRange(0, 4)));
                    layout.GenSteps.Add(PR_SPAWN_ITEMS, waterItemZoneStep);
                }

                if (ii >= 16 && ii < 19)
                {

                    {
                        AddDisconnectedRoomsStep<MapGenContext> addDisconnect = new AddDisconnectedRoomsStep<MapGenContext>();
                        addDisconnect.Components.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Disconnected));
                        addDisconnect.Amount = new RandRange(1, 4);

                        //Give it some room types to place
                        SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                        //cave
                        genericRooms.Add(new RoomGenCave<MapGenContext>(new RandRange(3, 7), new RandRange(3, 7)), 10);


                        addDisconnect.GenericRooms = genericRooms;

                        layout.GenSteps.Add(PR_ROOMS_GEN_EXTRA, addDisconnect);
                    }

                    {
                        //secret items
                        SpawnList<InvItem> secretItemSpawns = new SpawnList<InvItem>();
                        secretItemSpawns.Add(new InvItem("held_weather_rock"), 3);//Weather Rock
                        secretItemSpawns.Add(new InvItem("held_expert_belt"), 3);//Expert Belt
                        secretItemSpawns.Add(new InvItem("held_choice_scarf"), 3);//Choice Scarf
                        secretItemSpawns.Add(new InvItem("held_choice_specs"), 3);//Choice Specs
                        secretItemSpawns.Add(new InvItem("held_choice_band"), 3);//Choice Band
                        secretItemSpawns.Add(new InvItem("held_assault_vest"), 3);//Assault Vest
                        secretItemSpawns.Add(new InvItem("held_life_orb"), 3);//Life Orb
                        secretItemSpawns.Add(new InvItem("held_heal_ribbon"), 3);//Heal Ribbon
                        secretItemSpawns.Add(new InvItem("key", false, 1), 35);//Key
                        foreach (string key in IterateGummis())
                            secretItemSpawns.Add(new InvItem(key), 2);

                        RandomRoomSpawnStep<MapGenContext, InvItem> secretPlacement = new RandomRoomSpawnStep<MapGenContext, InvItem>(new PickerSpawner<MapGenContext, InvItem>(new LoopedRand<InvItem>(secretItemSpawns, new RandRange(1, 3))));
                        secretPlacement.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Disconnected));
                        layout.GenSteps.Add(PR_SPAWN_ITEMS, secretPlacement);
                    }
                    {
                        //secret money
                        List<MapItem> secretItemSpawns = new List<MapItem>();
                        secretItemSpawns.Add(MapItem.CreateMoney(200));
                        secretItemSpawns.Add(MapItem.CreateMoney(200));
                        secretItemSpawns.Add(MapItem.CreateMoney(200));
                        RandomRoomSpawnStep<MapGenContext, MapItem> secretPlacement = new RandomRoomSpawnStep<MapGenContext, MapItem>(new PickerSpawner<MapGenContext, MapItem>(new PresetMultiRand<MapItem>(secretItemSpawns)));
                        secretPlacement.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Disconnected));
                        layout.GenSteps.Add(PR_SPAWN_ITEMS, secretPlacement);
                    }

                    {
                        SpawnList<IStepSpawner<MapGenContext, MapItem>> boxSpawn = new SpawnList<IStepSpawner<MapGenContext, MapItem>>();

                        //444      ***    Light Box - 1* items
                        {
                            SpawnList<MapItem> silks = new SpawnList<MapItem>();
                            foreach (string key in IterateSilks())
                                silks.Add(new MapItem(key), 10);

                            boxSpawn.Add(new BoxSpawner<MapGenContext>("box_light", new PickerSpawner<MapGenContext, MapItem>(new LoopedRand<MapItem>(silks, new RandRange(1)))), 10);
                        }

                        //445      ***    Heavy Box - 2* items
                        {
                            boxSpawn.Add(new BoxSpawner<MapGenContext>("box_heavy", new SpeciesItemContextSpawner<MapGenContext>(new IntRange(2), new RandRange(1))), 10);
                        }

                        MultiStepSpawner<MapGenContext, MapItem> boxPicker = new MultiStepSpawner<MapGenContext, MapItem>(new LoopedRand<IStepSpawner<MapGenContext, MapItem>>(boxSpawn, new RandRange(0, 2)));

                        RandomRoomSpawnStep<MapGenContext, MapItem> secretPlacement = new RandomRoomSpawnStep<MapGenContext, MapItem>(boxPicker);
                        secretPlacement.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Disconnected));
                        layout.GenSteps.Add(PR_SPAWN_ITEMS, secretPlacement);
                    }

                    //secret enemies
                    SpecificTeamSpawner specificTeam = new SpecificTeamSpawner();
                    // 218 Slugma : 510 Incinerate : 106 Harden
                    specificTeam.Spawns.Add(GetGenericMob("slugma", "", "incinerate", "harden", "", "", new RandRange(23), "patrol"));

                    //secret enemies
                    PlaceRandomMobsStep<MapGenContext> secretMobPlacement = new PlaceRandomMobsStep<MapGenContext>(new LoopedTeamSpawner<MapGenContext>(specificTeam, new RandRange(3, 6)));
                    secretMobPlacement.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Disconnected));
                    {
                        secretMobPlacement.ClumpFactor = 20;
                    }
                    layout.GenSteps.Add(PR_SPAWN_MOBS, secretMobPlacement);

                }

                if (ii >= 6 && ii < 8)
                {
                    //280 Ralts : 100 Teleport
                    //always holds a key
                    MobSpawn mob = GetGenericMob("ralts", "", "teleport", "growl", "", "", new RandRange(10));
                    MobSpawnItem keySpawn = new MobSpawnItem(true);
                    keySpawn.Items.Add(new InvItem("key", false, 1), 10);
                    mob.SpawnFeatures.Add(keySpawn);

                    SpecificTeamSpawner specificTeam = new SpecificTeamSpawner();
                    specificTeam.Spawns.Add(mob);

                    LoopedTeamSpawner<MapGenContext> spawner = new LoopedTeamSpawner<MapGenContext>(specificTeam);
                    {
                        spawner.AmountSpawner = new RandDecay(0, 4, 80);
                    }
                    PlaceRandomMobsStep<MapGenContext> secretMobPlacement = new PlaceRandomMobsStep<MapGenContext>(spawner);
                    layout.GenSteps.Add(PR_SPAWN_MOBS, secretMobPlacement);
                }

                if (ii >= 11 && ii < 14)
                {
                    //147 Dratini : 35 Wrap : 43 Leer
                    SpecificTeamSpawner specificTeam = new SpecificTeamSpawner();
                    specificTeam.Spawns.Add(GetGenericMob("dratini", "", "wrap", "leer", "", "", new RandRange(15)));

                    LoopedTeamSpawner<MapGenContext> spawner = new LoopedTeamSpawner<MapGenContext>(specificTeam);
                    {
                        spawner.AmountSpawner = new RandRange(1, 3);
                    }
                    PlaceDisconnectedMobsStep<MapGenContext> secretMobPlacement = new PlaceDisconnectedMobsStep<MapGenContext>(spawner);
                    secretMobPlacement.AcceptedTiles.Add(new Tile("water"));
                    layout.GenSteps.Add(PR_SPAWN_MOBS, secretMobPlacement);
                }

                if (ii >= 21 && ii < 24)
                {
                    //142 Aerodactyl : 17 Wing Attack : 246 Ancient Power : 48 Supersonic : 97 Agility
                    SpecificTeamSpawner specificTeam = new SpecificTeamSpawner();
                    MobSpawn mob = GetGenericMob("aerodactyl", "", "wing_attack", "ancient_power", "supersonic", "agility", new RandRange(48), "wander_smart");
                    mob.SpawnFeatures.Add(new MobSpawnItem(true, "loot_nugget"));
                    specificTeam.Spawns.Add(mob);

                    LoopedTeamSpawner<MapGenContext> spawner = new LoopedTeamSpawner<MapGenContext>(specificTeam);
                    {
                        spawner.AmountSpawner = new RandRange(1);
                    }
                    PlaceDisconnectedMobsStep<MapGenContext> secretMobPlacement = new PlaceDisconnectedMobsStep<MapGenContext>(spawner);
                    secretMobPlacement.AcceptedTiles.Add(new Tile("pit"));
                    layout.GenSteps.Add(PR_SPAWN_MOBS, secretMobPlacement);
                }



                //enemies
                if (ii < 8)
                    AddRespawnData(layout, 10, 80);
                else if (ii < 16)
                    AddRespawnData(layout, 15, 90);
                else if (ii < 20)
                    AddRespawnData(layout, 20, 100);
                else if (ii < 27)
                    AddRespawnData(layout, 30, 100);
                else
                    AddRespawnData(layout, 25, 120);

                //enemies
                if (ii < 8)
                    AddEnemySpawnData(layout, 30, new RandRange(5, 7));
                else if (ii < 16)
                    AddEnemySpawnData(layout, 30, new RandRange(9, 14));
                else if (ii < 20)
                    AddEnemySpawnData(layout, 30, new RandRange(13, 18));
                else if (ii < 27)
                    AddEnemySpawnData(layout, 20, new RandRange(18, 25));
                else
                    AddEnemySpawnData(layout, 20, new RandRange(15, 24));


                //construct paths
                if (ii < 5)
                {
                    AddInitGridStep(layout, 4, 4, 10, 10);

                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(90);
                    path.BranchRatio = new RandRange(0, 25);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //cross
                    genericRooms.Add(new RoomGenCross<MapGenContext>(new RandRange(4, 11), new RandRange(4, 11), new RandRange(2, 6), new RandRange(2, 6)), 10);
                    //round
                    genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(5, 9), new RandRange(5, 9)), 10);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(75, 50));

                }
                else if (ii < 9)
                {
                    AddInitGridStep(layout, 5, 4, 9, 9, 2);

                    GridPathCircle<MapGenContext> path = new GridPathCircle<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.CircleRoomRatio = new RandRange(70);
                    path.Paths = new RandRange(2, 5);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //cross
                    genericRooms.Add(new RoomGenCross<MapGenContext>(new RandRange(3, 9), new RandRange(3, 9), new RandRange(3, 7), new RandRange(3, 7)), 10);
                    //round
                    genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(5, 8), new RandRange(5, 8)), 10);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(0), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                }
                else if (ii < 13)
                {
                    AddInitGridStep(layout, 5, 3, 10, 10);

                    GridPathTwoSides<MapGenContext> path = new GridPathTwoSides<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.GapAxis = Axis4.Horiz;

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //cross
                    genericRooms.Add(new RoomGenCross<MapGenContext>(new RandRange(4, 11), new RandRange(4, 11), new RandRange(2, 6), new RandRange(2, 6)), 10);
                    //round
                    genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(5, 9), new RandRange(5, 9)), 10);
                    //blocked
                    genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(6, 10), new RandRange(6, 10), new RandRange(2, 4), new RandRange(2, 4)), 10);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(0), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(100, 50));

                }
                else if (ii < 19)
                {
                    AddInitGridStep(layout, 6, 4, 10, 10);

                    if (ii < 16)
                    {
                        GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                        path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        path.RoomRatio = new RandRange(70);
                        path.BranchRatio = new RandRange(0, 25);

                        SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                        //cross
                        genericRooms.Add(new RoomGenCross<MapGenContext>(new RandRange(4, 11), new RandRange(4, 11), new RandRange(2, 6), new RandRange(2, 6)), 10);
                        //round
                        genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(5, 9), new RandRange(5, 9)), 10);
                        //blocked
                        genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(6, 10), new RandRange(6, 10), new RandRange(2, 4), new RandRange(2, 4)), 10);
                        path.GenericRooms = genericRooms;

                        SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                        genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                        path.GenericHalls = genericHalls;

                        layout.GenSteps.Add(PR_GRID_GEN, path);

                        layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(75, 50));
                        layout.GenSteps.Add(PR_GRID_GEN, new SetGridDefaultsStep<MapGenContext>(new RandRange(20), GetImmutableFilterList()));

                    }
                    else
                    {
                        GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                        path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        path.RoomRatio = new RandRange(60);
                        path.BranchRatio = new RandRange(0, 25);

                        SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                        //cross
                        genericRooms.Add(new RoomGenCross<MapGenContext>(new RandRange(4, 11), new RandRange(4, 11), new RandRange(2, 6), new RandRange(2, 6)), 10);
                        //round
                        genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(5, 9), new RandRange(5, 9)), 10);
                        //blocked
                        genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(6, 10), new RandRange(6, 10), new RandRange(2, 4), new RandRange(2, 4)), 10);
                        path.GenericRooms = genericRooms;

                        SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                        genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                        path.GenericHalls = genericHalls;

                        layout.GenSteps.Add(PR_GRID_GEN, path);

                        layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(75, 50));
                        layout.GenSteps.Add(PR_GRID_GEN, new SetGridDefaultsStep<MapGenContext>(new RandRange(10), GetImmutableFilterList()));

                    }
                }
                else if (ii < 22)
                {
                    AddInitGridStep(layout, 6, 4, 10, 10);

                    if (ii < 21)
                    {
                        GridPathGrid<MapGenContext> path = new GridPathGrid<MapGenContext>();
                        path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        path.RoomRatio = 80;
                        path.HallRatio = 30;

                        SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                        //square
                        genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(4, 9), new RandRange(4, 9), new RandRange(0, 81)), 10);
                        path.GenericRooms = genericRooms;

                        SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                        genericHalls.Add(new RoomGenAngledHall<MapGenContext>(20), 10);
                        path.GenericHalls = genericHalls;

                        layout.GenSteps.Add(PR_GRID_GEN, path);
                    }
                    else
                    {
                        GridPathGrid<MapGenContext> path = new GridPathGrid<MapGenContext>();
                        path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        path.RoomRatio = 100;
                        path.HallRatio = 0;

                        SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                        //square
                        genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(4, 9), new RandRange(4, 9), new RandRange(0, 81)), 10);
                        path.GenericRooms = genericRooms;

                        SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                        genericHalls.Add(new RoomGenAngledHall<MapGenContext>(20), 10);
                        path.GenericHalls = genericHalls;

                        layout.GenSteps.Add(PR_GRID_GEN, path);
                    }
                }
                else if (ii < 24)
                {
                    AddInitGridStep(layout, 5, 4, 9, 9);

                    GridPathBeetle<MapGenContext> path = new GridPathBeetle<MapGenContext>();
                    path.LargeRoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.GiantHallGen.Add(new RoomGenBump<MapGenContext>(new RandRange(44, 61), new RandRange(4, 9), new RandRange(0, 101)), 10);
                    path.LegPercent = 80;
                    path.ConnectPercent = 80;
                    path.Vertical = true;

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8), new RandRange(0, 101)), 10);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(100), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                }
                else if (ii < 27)
                {
                    AddInitGridStep(layout, 7, 5, 7, 7);

                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(100);
                    path.BranchRatio = new RandRange(0, 25);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    if (ii < 26)
                    {
                        //cross
                        genericRooms.Add(new RoomGenCross<MapGenContext>(new RandRange(5, 15), new RandRange(5, 15), new RandRange(3, 6), new RandRange(3, 6)), 10);
                        //square
                        genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(8, 15), new RandRange(8, 15), new RandRange(0, 91)), 10);
                    }
                    else
                    {
                        //square
                        genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(4, 7), new RandRange(4, 7), new RandRange(0, 81)), 10);
                    }
                    //blocked
                    genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(5, 9), new RandRange(5, 9), new RandRange(8, 9), new RandRange(1, 3)), 4);
                    genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(5, 9), new RandRange(5, 9), new RandRange(1, 3), new RandRange(8, 9)), 4);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(100), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(65, 100));
                    layout.GenSteps.Add(PR_GRID_GEN, new SetGridDefaultsStep<MapGenContext>(new RandRange(10), GetImmutableFilterList()));

                }
                else
                {
                    AddInitGridStep(layout, 8, 6, 8, 8);

                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(80);
                    path.BranchRatio = new RandRange(0, 25);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //square
                    genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8), new RandRange(0, 30)), 10);
                    //blocked
                    genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(5, 9), new RandRange(5, 9), new RandRange(8, 9), new RandRange(1, 2)), 4);
                    genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(5, 9), new RandRange(5, 9), new RandRange(1, 2), new RandRange(8, 9)), 4);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(75, 50));
                }

                AddDrawGridSteps(layout);

                AddStairStep(layout, false);

                //water
                if (ii < 4)
                    AddWaterSteps(layout, "water", new RandRange(35));//water
                else if (ii < 8)
                    AddWaterSteps(layout, "water", new RandRange(25));//water
                else if (ii < 10)
                    AddWaterSteps(layout, "water", new RandRange(22));//water
                else if (ii < 12)
                    AddWaterSteps(layout, "water", new RandRange(30), false);//water
                else if (ii < 14)
                    AddWaterSteps(layout, "water", new RandRange(15), false);//water
                else if (ii < 15)
                    AddWaterSteps(layout, "lava", new RandRange(22));//lava
                else if (ii < 16)
                    AddWaterSteps(layout, "lava", new RandRange(30));//lava
                else if (ii < 18)
                    AddWaterSteps(layout, "lava", new RandRange(50));//lava
                else if (ii < 19)
                    AddWaterSteps(layout, "lava", new RandRange(20));//lava
                else if (ii < 20)
                { }
                else if (ii < 23)
                    AddWaterSteps(layout, "pit", new RandRange(30), false);//abyss
                else if (ii < 24)
                { }
                else if (ii < 26)
                    AddWaterSteps(layout, "pit", new RandRange(35));//abyss
                else if (ii < 27)
                    AddWaterSteps(layout, "pit", new RandRange(55));//abyss
                else if (ii < 28)
                    AddWaterSteps(layout, "pit", new RandRange(75));//abyss
                else if (ii < 29)
                    AddWaterSteps(layout, "pit", new RandRange(45));//abyss
                else
                    AddWaterSteps(layout, "pit", new RandRange(25));//abyss

                layout.GenSteps.Add(PR_DBG_CHECK, new DetectIsolatedStairsStep<MapGenContext, MapGenEntrance, MapGenExit>());

                floorSegment.Floors.Add(layout);
            }

            zone.Segments.Add(floorSegment);

            LayeredSegment staticSegment = new LayeredSegment();
            {
                LoadGen layout = new LoadGen();
                MappedRoomStep<MapLoadContext> startGen = new MappedRoomStep<MapLoadContext>();
                startGen.MapID = "guildmaster_summit";
                layout.GenSteps.Add(PR_TILES_INIT, startGen);
                MapEffectStep<MapLoadContext> noRescue = new MapEffectStep<MapLoadContext>();
                noRescue.Effect.OnMapRefresh.Add(0, new MapNoRescueEvent());
                layout.GenSteps.Add(PR_FLOOR_DATA, noRescue);
                staticSegment.Floors.Add(layout);
            }

            zone.Segments.Add(staticSegment);
        }
        #endregion

        #region SECRET GARDEN
        static void FillSecretGarden(ZoneData zone)
        {
            zone.Name = new LocalText("Secret Garden");
            zone.Level = 5;
            zone.LevelCap = true;
            zone.BagRestrict = 8;
            zone.TeamSize = 2;
            zone.Rescues = 2;
            zone.Rogue = RogueStatus.AllTransfer;
            int max_floors = 40;

            LayeredSegment floorSegment = new LayeredSegment();
            floorSegment.IsRelevant = true;
            floorSegment.ZoneSteps.Add(new SaveVarsZoneStep(PR_EXITS_RESCUE));
            floorSegment.ZoneSteps.Add(new FloorNameIDZoneStep(PR_FLOOR_DATA, new LocalText("Secret Garden\nB{0}F")));

            //money
            MoneySpawnZoneStep moneySpawnZoneStep = new MoneySpawnZoneStep(PR_RESPAWN_MONEY, new RandRange(63, 72), new RandRange(21, 24));
            moneySpawnZoneStep.ModStates.Add(new FlagType(typeof(CoinModGenState)));
            floorSegment.ZoneSteps.Add(moneySpawnZoneStep);

            //items
            ItemSpawnZoneStep itemSpawnZoneStep = new ItemSpawnZoneStep();
            itemSpawnZoneStep.Priority = PR_RESPAWN_ITEM;


            //necessities
            CategorySpawn<InvItem> necessities = new CategorySpawn<InvItem>();
            necessities.SpawnRates.SetRange(14, new IntRange(0, max_floors));
            itemSpawnZoneStep.Spawns.Add("necessities", necessities);


            necessities.Spawns.Add(new InvItem("berry_sitrus"), new IntRange(0, max_floors), 45);//Sitrus Berry
            necessities.Spawns.Add(new InvItem("berry_leppa"), new IntRange(0, 31), 70);//Leppa Berry
            necessities.Spawns.Add(new InvItem("food_apple"), new IntRange(0, 31), 15);//Apple
            necessities.Spawns.Add(new InvItem("berry_lum"), new IntRange(0, max_floors), 50);//Lum Berry
            necessities.Spawns.Add(new InvItem("seed_reviver"), new IntRange(0, 27), 10);//Reviver Seed
                                                                               //special
            CategorySpawn<InvItem> special = new CategorySpawn<InvItem>();
            special.SpawnRates.SetRange(4, new IntRange(0, 27));
            itemSpawnZoneStep.Spawns.Add("special", special);


            special.Spawns.Add(new InvItem("machine_recall_box"), new IntRange(0, 27), 25);//Link Box
            special.Spawns.Add(new InvItem("machine_assembly_box"), new IntRange(6, 27), 5);//Assembly Box
            special.Spawns.Add(new InvItem("key", false, 1), new IntRange(0, 24), 10);//Key
                                                                                     //snacks
            CategorySpawn<InvItem> snacks = new CategorySpawn<InvItem>();
            snacks.SpawnRates.SetRange(10, new IntRange(0, max_floors));
            itemSpawnZoneStep.Spawns.Add("snacks", snacks);


            snacks.Spawns.Add(new InvItem("berry_apicot"), new IntRange(0, 27), 3);//Apicot Berry
            snacks.Spawns.Add(new InvItem("berry_liechi"), new IntRange(0, 27), 3);//Liechi Berry
            snacks.Spawns.Add(new InvItem("berry_ganlon"), new IntRange(0, 27), 3);//Ganlon Berry
            snacks.Spawns.Add(new InvItem("berry_salac"), new IntRange(0, 27), 3);//Salac Berry
            snacks.Spawns.Add(new InvItem("berry_petaya"), new IntRange(0, 27), 3);//Petaya Berry
            snacks.Spawns.Add(new InvItem("berry_starf"), new IntRange(0, 27), 3);//Starf Berry
            snacks.Spawns.Add(new InvItem("berry_micle"), new IntRange(0, 27), 3);//Micle Berry
            snacks.Spawns.Add(new InvItem("berry_enigma"), new IntRange(0, 27), 4);//Enigma Berry
            snacks.Spawns.Add(new InvItem("berry_jaboca"), new IntRange(0, max_floors), 1);//Jaboca Berry
            snacks.Spawns.Add(new InvItem("berry_rowap"), new IntRange(0, max_floors), 1);//Rowap Berry
            snacks.Spawns.Add(new InvItem("berry_tanga"), new IntRange(0, 27), 1);//Tanga Berry
            snacks.Spawns.Add(new InvItem("berry_colbur"), new IntRange(0, 27), 1);//Colbur Berry
            snacks.Spawns.Add(new InvItem("berry_haban"), new IntRange(0, 27), 1);//Haban Berry
            snacks.Spawns.Add(new InvItem("berry_wacan"), new IntRange(0, 27), 1);//Wacan Berry
            snacks.Spawns.Add(new InvItem("berry_chople"), new IntRange(0, 27), 1);//Chople Berry
            snacks.Spawns.Add(new InvItem("berry_occa"), new IntRange(0, 27), 1);//Occa Berry
            snacks.Spawns.Add(new InvItem("berry_coba"), new IntRange(0, 27), 1);//Coba Berry
            snacks.Spawns.Add(new InvItem("berry_kasib"), new IntRange(0, 27), 1);//Kasib Berry
            snacks.Spawns.Add(new InvItem("berry_rindo"), new IntRange(0, 27), 1);//Rindo Berry
            snacks.Spawns.Add(new InvItem("berry_shuca"), new IntRange(0, 27), 1);//Shuca Berry
            snacks.Spawns.Add(new InvItem("berry_yache"), new IntRange(0, 27), 1);//Yache Berry
            snacks.Spawns.Add(new InvItem("berry_chilan"), new IntRange(0, 27), 1);//Chilan Berry
            snacks.Spawns.Add(new InvItem("berry_kebia"), new IntRange(0, 27), 1);//Kebia Berry
            snacks.Spawns.Add(new InvItem("berry_payapa"), new IntRange(0, 27), 1);//Payapa Berry
            snacks.Spawns.Add(new InvItem("berry_charti"), new IntRange(0, 27), 1);//Charti Berry
            snacks.Spawns.Add(new InvItem("berry_babiri"), new IntRange(0, 27), 1);//Babiri Berry
            snacks.Spawns.Add(new InvItem("berry_passho"), new IntRange(0, 27), 1);//Passho Berry
            snacks.Spawns.Add(new InvItem("berry_roseli"), new IntRange(0, 27), 1);//Roseli Berry
            snacks.Spawns.Add(new InvItem("seed_blast"), new IntRange(0, max_floors), 20);//Blast Seed
            snacks.Spawns.Add(new InvItem("seed_warp"), new IntRange(0, max_floors), 10);//Warp Seed
            snacks.Spawns.Add(new InvItem("seed_decoy"), new IntRange(0, max_floors), 10);//Decoy Seed
            snacks.Spawns.Add(new InvItem("seed_sleep"), new IntRange(0, max_floors), 10);//Sleep Seed
            snacks.Spawns.Add(new InvItem("seed_blinker"), new IntRange(0, max_floors), 10);//Blinker Seed
            snacks.Spawns.Add(new InvItem("seed_last_chance"), new IntRange(0, max_floors), 5);//Last-Chance Seed
            snacks.Spawns.Add(new InvItem("seed_doom"), new IntRange(0, max_floors), 5);//Doom Seed
            snacks.Spawns.Add(new InvItem("seed_ban"), new IntRange(0, max_floors), 10);//Ban Seed
            snacks.Spawns.Add(new InvItem("seed_pure", true), new IntRange(0, max_floors), 3);//Pure Seed
            snacks.Spawns.Add(new InvItem("seed_pure"), new IntRange(0, max_floors), 3);//Pure Seed
            snacks.Spawns.Add(new InvItem("seed_ice"), new IntRange(0, max_floors), 10);//Ice Seed
            snacks.Spawns.Add(new InvItem("seed_vile"), new IntRange(0, max_floors), 10);//Vile Seed
            snacks.Spawns.Add(new InvItem("herb_power"), new IntRange(0, max_floors), 10);//Power Herb
            snacks.Spawns.Add(new InvItem("herb_mental"), new IntRange(0, max_floors), 10);//Mental Herb
            snacks.Spawns.Add(new InvItem("herb_white"), new IntRange(0, max_floors), 50);//White Herb
                                                                                  //boosters
            CategorySpawn<InvItem> boosters = new CategorySpawn<InvItem>();
            boosters.SpawnRates.SetRange(7, new IntRange(0, 27));
            itemSpawnZoneStep.Spawns.Add("boosters", boosters);


            boosters.Spawns.Add(new InvItem("boost_protein", true), new IntRange(0, max_floors), 4);//Protein
            boosters.Spawns.Add(new InvItem("boost_protein"), new IntRange(0, max_floors), 6);//Protein
            boosters.Spawns.Add(new InvItem("boost_iron", true), new IntRange(0, max_floors), 4);//Iron
            boosters.Spawns.Add(new InvItem("boost_iron"), new IntRange(0, max_floors), 6);//Iron
            boosters.Spawns.Add(new InvItem("boost_calcium", true), new IntRange(0, max_floors), 4);//Calcium
            boosters.Spawns.Add(new InvItem("boost_calcium"), new IntRange(0, max_floors), 6);//Calcium
            boosters.Spawns.Add(new InvItem("boost_zinc", true), new IntRange(0, max_floors), 4);//Zinc
            boosters.Spawns.Add(new InvItem("boost_zinc"), new IntRange(0, max_floors), 6);//Zinc
            boosters.Spawns.Add(new InvItem("boost_carbos", true), new IntRange(0, max_floors), 4);//Carbos
            boosters.Spawns.Add(new InvItem("boost_carbos"), new IntRange(0, max_floors), 6);//Carbos
            boosters.Spawns.Add(new InvItem("boost_hp_up", true), new IntRange(0, max_floors), 4);//HP Up
            boosters.Spawns.Add(new InvItem("boost_hp_up"), new IntRange(0, max_floors), 6);//HP Up
                                                                                   //throwable
            CategorySpawn<InvItem> throwable = new CategorySpawn<InvItem>();
            throwable.SpawnRates.SetRange(12, new IntRange(0, max_floors));
            itemSpawnZoneStep.Spawns.Add("throwable", throwable);


            throwable.Spawns.Add(new InvItem("ammo_stick", false, 4), new IntRange(0, max_floors), 10);//Stick
            throwable.Spawns.Add(new InvItem("ammo_cacnea_spike", false, 3), new IntRange(0, max_floors), 10);//Cacnea Spike
            throwable.Spawns.Add(new InvItem("wand_path", false, 2), new IntRange(0, max_floors), 10);//Path Wand
            throwable.Spawns.Add(new InvItem("wand_fear", false, 4), new IntRange(0, max_floors), 10);//Fear Wand
            throwable.Spawns.Add(new InvItem("wand_switcher", false, 4), new IntRange(0, max_floors), 10);//Switcher Wand
            throwable.Spawns.Add(new InvItem("wand_whirlwind", false, 4), new IntRange(0, max_floors), 10);//Whirlwind Wand
            throwable.Spawns.Add(new InvItem("wand_lure", false, 4), new IntRange(0, max_floors), 10);//Lure Wand
            throwable.Spawns.Add(new InvItem("wand_slow", false, 4), new IntRange(0, max_floors), 10);//Slow Wand
            throwable.Spawns.Add(new InvItem("wand_pounce", false, 4), new IntRange(0, max_floors), 10);//Pounce Wand
            throwable.Spawns.Add(new InvItem("wand_warp", false, 2), new IntRange(0, max_floors), 10);//Warp Wand
            throwable.Spawns.Add(new InvItem("wand_topsy_turvy", false, 3), new IntRange(0, max_floors), 10);//Topsy-Turvy Wand
            throwable.Spawns.Add(new InvItem("wand_lob", false, 4), new IntRange(0, max_floors), 10);//Lob Wand
            throwable.Spawns.Add(new InvItem("wand_purge", false, 4), new IntRange(0, max_floors), 10);//Purge Wand
            throwable.Spawns.Add(new InvItem("ammo_iron_thorn", false, 3), new IntRange(0, max_floors), 10);//Iron Thorn
            throwable.Spawns.Add(new InvItem("ammo_silver_spike", false, 3), new IntRange(0, max_floors), 10);//Silver Spike
            throwable.Spawns.Add(new InvItem("ammo_gravelerock", false, 3), new IntRange(0, max_floors), 10);//Gravelerock
            throwable.Spawns.Add(new InvItem("ammo_corsola_twig", false, 3), new IntRange(0, max_floors), 10);//Corsola Twig
            throwable.Spawns.Add(new InvItem("ammo_rare_fossil", false, 3), new IntRange(0, max_floors), 10);//Rare Fossil
            throwable.Spawns.Add(new InvItem("ammo_geo_pebble", false, 3), new IntRange(0, max_floors), 10);//Geo Pebble
                                                                                               //orbs
            CategorySpawn<InvItem> orbs = new CategorySpawn<InvItem>();
            orbs.SpawnRates.SetRange(10, new IntRange(0, max_floors));
            itemSpawnZoneStep.Spawns.Add("orbs", orbs);


            orbs.Spawns.Add(new InvItem("orb_one_room", true), new IntRange(0, max_floors), 3);//One-Room Orb
            orbs.Spawns.Add(new InvItem("orb_one_room"), new IntRange(0, max_floors), 7);//One-Room Orb
            orbs.Spawns.Add(new InvItem("orb_fill_in", true), new IntRange(0, max_floors), 3);//Fill-In Orb
            orbs.Spawns.Add(new InvItem("orb_fill_in"), new IntRange(0, max_floors), 7);//Fill-In Orb
            orbs.Spawns.Add(new InvItem("orb_petrify", true), new IntRange(0, max_floors), 3);//Petrify Orb
            orbs.Spawns.Add(new InvItem("orb_petrify"), new IntRange(0, max_floors), 7);//Petrify Orb
            orbs.Spawns.Add(new InvItem("orb_halving", true), new IntRange(0, max_floors), 3);//Halving Orb
            orbs.Spawns.Add(new InvItem("orb_halving"), new IntRange(0, max_floors), 7);//Halving Orb
            orbs.Spawns.Add(new InvItem("orb_slumber", true), new IntRange(0, max_floors), 2);//Slumber Orb
            orbs.Spawns.Add(new InvItem("orb_slumber"), new IntRange(0, max_floors), 6);//Slumber Orb
            orbs.Spawns.Add(new InvItem("orb_slow", true), new IntRange(0, max_floors), 2);//Slow Orb
            orbs.Spawns.Add(new InvItem("orb_slow"), new IntRange(0, max_floors), 6);//Slow Orb
            orbs.Spawns.Add(new InvItem("orb_totter", true), new IntRange(0, max_floors), 2);//Totter Orb
            orbs.Spawns.Add(new InvItem("orb_totter"), new IntRange(0, max_floors), 6);//Totter Orb
            orbs.Spawns.Add(new InvItem("orb_spurn", true), new IntRange(0, max_floors), 2);//Spurn Orb
            orbs.Spawns.Add(new InvItem("orb_spurn"), new IntRange(0, max_floors), 6);//Spurn Orb
            orbs.Spawns.Add(new InvItem("orb_stayaway", true), new IntRange(0, max_floors), 2);//Stayaway Orb
            orbs.Spawns.Add(new InvItem("orb_stayaway"), new IntRange(0, max_floors), 2);//Stayaway Orb
            orbs.Spawns.Add(new InvItem("orb_pierce", true), new IntRange(0, max_floors), 3);//Pierce Orb
            orbs.Spawns.Add(new InvItem("orb_pierce"), new IntRange(0, max_floors), 7);//Pierce Orb
            orbs.Spawns.Add(new InvItem("orb_cleanse"), new IntRange(0, max_floors), 7);//Cleanse Orb
            orbs.Spawns.Add(new InvItem("orb_all_aim"), new IntRange(0, max_floors), 10);//All-Aim Orb
            orbs.Spawns.Add(new InvItem("orb_trap_see"), new IntRange(0, max_floors), 10);//Trap-See Orb
            orbs.Spawns.Add(new InvItem("orb_trapbust"), new IntRange(0, max_floors), 10);//Trapbust Orb
            orbs.Spawns.Add(new InvItem("orb_foe_hold"), new IntRange(0, max_floors), 10);//Foe-Hold Orb
            orbs.Spawns.Add(new InvItem("orb_mobile"), new IntRange(0, max_floors), 10);//Mobile Orb
            orbs.Spawns.Add(new InvItem("orb_rollcall"), new IntRange(0, max_floors), 10);//Rollcall Orb
            orbs.Spawns.Add(new InvItem("orb_mug"), new IntRange(0, max_floors), 10);//Mug Orb
            orbs.Spawns.Add(new InvItem("orb_mirror"), new IntRange(0, max_floors), 10);//Mirror Orb
            orbs.Spawns.Add(new InvItem("orb_weather"), new IntRange(0, max_floors), 10);//Weather Orb
            orbs.Spawns.Add(new InvItem("orb_foe_seal"), new IntRange(0, max_floors), 10);//Foe-Seal Orb
            orbs.Spawns.Add(new InvItem("orb_freeze"), new IntRange(0, max_floors), 10);//Freeze Orb
            orbs.Spawns.Add(new InvItem("orb_devolve"), new IntRange(0, max_floors), 10);//Devolve Orb
            orbs.Spawns.Add(new InvItem("orb_nullify"), new IntRange(0, max_floors), 10);//Nullify Orb
            orbs.Spawns.Add(new InvItem("orb_rebound"), new IntRange(0, max_floors), 10);//Rebound Orb
            orbs.Spawns.Add(new InvItem("orb_all_protect", true), new IntRange(0, max_floors), 3);//All Protect Orb
            orbs.Spawns.Add(new InvItem("orb_all_protect"), new IntRange(0, max_floors), 7);//All Protect Orb
            orbs.Spawns.Add(new InvItem("orb_luminous", true), new IntRange(0, max_floors), 3);//Luminous Orb
            orbs.Spawns.Add(new InvItem("orb_luminous"), new IntRange(0, max_floors), 7);//Luminous Orb
            orbs.Spawns.Add(new InvItem("orb_trawl", true), new IntRange(0, max_floors), 3);//Trawl Orb
            orbs.Spawns.Add(new InvItem("orb_trawl"), new IntRange(0, max_floors), 7);//Trawl Orb
            orbs.Spawns.Add(new InvItem("orb_scanner"), new IntRange(0, max_floors), 10);//Scanner Orb
                                                                                //held
            CategorySpawn<InvItem> held = new CategorySpawn<InvItem>();
            held.SpawnRates.SetRange(2, new IntRange(0, 27));
            itemSpawnZoneStep.Spawns.Add("held", held);


            held.Spawns.Add(new InvItem("held_silver_powder"), new IntRange(0, max_floors), 5);//Silver Powder
            held.Spawns.Add(new InvItem("held_black_glasses"), new IntRange(0, max_floors), 5);//Black Glasses
            held.Spawns.Add(new InvItem("held_dragon_scale"), new IntRange(0, max_floors), 5);//Dragon Scale
            held.Spawns.Add(new InvItem("held_magnet"), new IntRange(0, max_floors), 5);//Magnet
            held.Spawns.Add(new InvItem("held_pink_bow"), new IntRange(0, max_floors), 5);//Pink Bow
            held.Spawns.Add(new InvItem("held_black_belt"), new IntRange(0, max_floors), 5);//Black Belt
            held.Spawns.Add(new InvItem("held_charcoal"), new IntRange(0, max_floors), 5);//Charcoal
            held.Spawns.Add(new InvItem("held_sharp_beak"), new IntRange(0, max_floors), 5);//Sharp Beak
            held.Spawns.Add(new InvItem("held_spell_tag"), new IntRange(0, max_floors), 5);//Spell Tag
            held.Spawns.Add(new InvItem("held_miracle_seed"), new IntRange(0, max_floors), 5);//Miracle Seed
            held.Spawns.Add(new InvItem("held_soft_sand"), new IntRange(0, max_floors), 5);//Soft Sand
            held.Spawns.Add(new InvItem("held_never_melt_ice"), new IntRange(0, max_floors), 5);//Never-Melt Ice
            held.Spawns.Add(new InvItem("held_silk_scarf"), new IntRange(0, max_floors), 5);//Silk Scarf
            held.Spawns.Add(new InvItem("held_poison_barb"), new IntRange(0, max_floors), 5);//Poison Barb
            held.Spawns.Add(new InvItem("held_twisted_spoon"), new IntRange(0, max_floors), 5);//Twisted Spoon
            held.Spawns.Add(new InvItem("held_hard_stone"), new IntRange(0, max_floors), 5);//Hard Stone
            held.Spawns.Add(new InvItem("held_metal_coat"), new IntRange(0, max_floors), 5);//Metal Coat
            held.Spawns.Add(new InvItem("held_mystic_water"), new IntRange(0, max_floors), 5);//Mystic Water
            held.Spawns.Add(new InvItem("held_insect_plate"), new IntRange(0, max_floors), 5);//Insect Plate
            held.Spawns.Add(new InvItem("held_dread_plate"), new IntRange(0, max_floors), 5);//Dread Plate
            held.Spawns.Add(new InvItem("held_draco_plate"), new IntRange(0, max_floors), 5);//Draco Plate
            held.Spawns.Add(new InvItem("held_zap_plate"), new IntRange(0, max_floors), 5);//Zap Plate
            held.Spawns.Add(new InvItem("held_pixie_plate"), new IntRange(0, max_floors), 5);//Pixie Plate
            held.Spawns.Add(new InvItem("held_fist_plate"), new IntRange(0, max_floors), 5);//Fist Plate
            held.Spawns.Add(new InvItem("held_flame_plate"), new IntRange(0, max_floors), 5);//Flame Plate
            held.Spawns.Add(new InvItem("held_sky_plate"), new IntRange(0, max_floors), 5);//Sky Plate
            held.Spawns.Add(new InvItem("held_spooky_plate"), new IntRange(0, max_floors), 5);//Spooky Plate
            held.Spawns.Add(new InvItem("held_meadow_plate"), new IntRange(0, max_floors), 5);//Meadow Plate
            held.Spawns.Add(new InvItem("held_earth_plate"), new IntRange(0, max_floors), 5);//Earth Plate
            held.Spawns.Add(new InvItem("held_icicle_plate"), new IntRange(0, max_floors), 5);//Icicle Plate
            held.Spawns.Add(new InvItem("held_toxic_plate"), new IntRange(0, max_floors), 5);//Toxic Plate
            held.Spawns.Add(new InvItem("held_mind_plate"), new IntRange(0, max_floors), 5);//Mind Plate
            held.Spawns.Add(new InvItem("held_stone_plate"), new IntRange(0, max_floors), 5);//Stone Plate
            held.Spawns.Add(new InvItem("held_iron_plate"), new IntRange(0, max_floors), 5);//Iron Plate
            held.Spawns.Add(new InvItem("held_splash_plate"), new IntRange(0, max_floors), 5);//Splash Plate
            held.Spawns.Add(new InvItem("held_mobile_scarf"), new IntRange(0, max_floors), 10);//Mobile Scarf
            held.Spawns.Add(new InvItem("held_pass_scarf"), new IntRange(0, max_floors), 10);//Pass Scarf
            held.Spawns.Add(new InvItem("held_cover_band"), new IntRange(0, max_floors), 10);//Cover Band
            held.Spawns.Add(new InvItem("held_reunion_cape", true), new IntRange(0, max_floors), 5);//Reunion Cape
            held.Spawns.Add(new InvItem("held_reunion_cape"), new IntRange(0, max_floors), 5);//Reunion Cape
            held.Spawns.Add(new InvItem("held_trap_scarf", true), new IntRange(0, max_floors), 5);//Trap Scarf
            held.Spawns.Add(new InvItem("held_trap_scarf"), new IntRange(0, max_floors), 5);//Trap Scarf
            held.Spawns.Add(new InvItem("held_grip_claw"), new IntRange(0, max_floors), 10);//Grip Claw
            held.Spawns.Add(new InvItem("held_twist_band", true), new IntRange(0, max_floors), 5);//Twist Band
            held.Spawns.Add(new InvItem("held_twist_band"), new IntRange(0, max_floors), 5);//Twist Band
            held.Spawns.Add(new InvItem("held_metronome", true), new IntRange(0, max_floors), 5);//Metronome
            held.Spawns.Add(new InvItem("held_metronome"), new IntRange(0, max_floors), 5);//Metronome
            held.Spawns.Add(new InvItem("held_shell_bell", true), new IntRange(0, max_floors), 5);//Shell Bell
            held.Spawns.Add(new InvItem("held_shell_bell"), new IntRange(0, max_floors), 5);//Shell Bell
            held.Spawns.Add(new InvItem("held_scope_lens", true), new IntRange(0, max_floors), 5);//Scope Lens
            held.Spawns.Add(new InvItem("held_scope_lens"), new IntRange(0, max_floors), 5);//Scope Lens
            held.Spawns.Add(new InvItem("held_power_band"), new IntRange(0, max_floors), 10);//Power Band
            held.Spawns.Add(new InvItem("held_special_band"), new IntRange(0, max_floors), 10);//Special Band
            held.Spawns.Add(new InvItem("held_defense_scarf"), new IntRange(0, max_floors), 10);//Defense Scarf
            held.Spawns.Add(new InvItem("held_zinc_band"), new IntRange(0, max_floors), 10);//Zinc Band
            held.Spawns.Add(new InvItem("held_wide_lens", true), new IntRange(0, max_floors), 10);//Wide Lens
            held.Spawns.Add(new InvItem("held_pierce_band", true), new IntRange(0, max_floors), 5);//Pierce Band
            held.Spawns.Add(new InvItem("held_pierce_band"), new IntRange(0, max_floors), 5);//Pierce Band
            held.Spawns.Add(new InvItem("held_shed_shell", true), new IntRange(0, max_floors), 5);//Shed Shell
            held.Spawns.Add(new InvItem("held_shed_shell"), new IntRange(0, max_floors), 5);//Shed Shell
            held.Spawns.Add(new InvItem("held_x_ray_specs", true), new IntRange(0, max_floors), 5);//X-Ray Specs
            held.Spawns.Add(new InvItem("held_x_ray_specs"), new IntRange(0, max_floors), 5);//X-Ray Specs
            held.Spawns.Add(new InvItem("held_big_root", true), new IntRange(0, max_floors), 5);//Big Root
            held.Spawns.Add(new InvItem("held_big_root"), new IntRange(0, max_floors), 5);//Big Root
            held.Spawns.Add(new InvItem("held_weather_rock", true), new IntRange(0, max_floors), 5);//Weather Rock
            held.Spawns.Add(new InvItem("held_weather_rock"), new IntRange(0, max_floors), 5);//Weather Rock
            held.Spawns.Add(new InvItem("held_expert_belt", true), new IntRange(0, max_floors), 5);//Expert Belt
            held.Spawns.Add(new InvItem("held_expert_belt"), new IntRange(0, max_floors), 5);//Expert Belt
            held.Spawns.Add(new InvItem("held_choice_scarf"), new IntRange(0, max_floors), 10);//Choice Scarf
            held.Spawns.Add(new InvItem("held_choice_specs"), new IntRange(0, max_floors), 10);//Choice Specs
            held.Spawns.Add(new InvItem("held_choice_band"), new IntRange(0, max_floors), 10);//Choice Band
            held.Spawns.Add(new InvItem("held_assault_vest"), new IntRange(0, max_floors), 10);//Assault Vest
            held.Spawns.Add(new InvItem("held_life_orb"), new IntRange(0, max_floors), 10);//Life Orb
            held.Spawns.Add(new InvItem("held_heal_ribbon", true), new IntRange(0, max_floors), 5);//Heal Ribbon
            held.Spawns.Add(new InvItem("held_heal_ribbon"), new IntRange(0, max_floors), 5);//Heal Ribbon
            held.Spawns.Add(new InvItem("held_warp_scarf", true), new IntRange(0, max_floors), 5);//Warp Scarf
            held.Spawns.Add(new InvItem("held_warp_scarf"), new IntRange(0, max_floors), 5);//Warp Scarf
            held.Spawns.Add(new InvItem("held_toxic_orb"), new IntRange(0, max_floors), 10);//Toxic Orb
            held.Spawns.Add(new InvItem("held_flame_orb"), new IntRange(0, max_floors), 10);//Flame Orb
            held.Spawns.Add(new InvItem("held_sticky_barb"), new IntRange(0, max_floors), 10);//Sticky Barb
            held.Spawns.Add(new InvItem("held_ring_target"), new IntRange(0, max_floors), 10);//Ring Target
                                                                                //evo
            CategorySpawn<InvItem> evo = new CategorySpawn<InvItem>();
            evo.SpawnRates.SetRange(1, new IntRange(0, 27));
            itemSpawnZoneStep.Spawns.Add("evo", evo);


            evo.Spawns.Add(new InvItem("evo_leaf_stone"), new IntRange(0, max_floors), 10);//Leaf Stone
            evo.Spawns.Add(new InvItem("evo_sun_stone"), new IntRange(0, max_floors), 10);//Sun Stone
            evo.Spawns.Add(new InvItem("evo_fire_stone"), new IntRange(0, max_floors), 10);//Fire Stone
            evo.Spawns.Add(new InvItem("evo_kings_rock"), new IntRange(0, max_floors), 10);//King's Rock
            evo.Spawns.Add(new InvItem("evo_link_cable"), new IntRange(0, max_floors), 10);//Link Cable
            evo.Spawns.Add(new InvItem("evo_moon_stone"), new IntRange(0, max_floors), 10);//Moon Stone
            evo.Spawns.Add(new InvItem("evo_water_stone"), new IntRange(0, max_floors), 10);//Water Stone
                                                                               //tms
            CategorySpawn<InvItem> tms = new CategorySpawn<InvItem>();
            tms.SpawnRates.SetRange(5, new IntRange(0, 27));
            itemSpawnZoneStep.Spawns.Add("tms", tms);


            tms.Spawns.Add(new InvItem("tm_secret_power"), new IntRange(0, max_floors), 10);//TM Secret Power
            tms.Spawns.Add(new InvItem("tm_echoed_voice"), new IntRange(0, max_floors), 10);//TM Echoed Voice
            tms.Spawns.Add(new InvItem("tm_double_team"), new IntRange(0, max_floors), 10);//TM Double Team
            tms.Spawns.Add(new InvItem("tm_toxic"), new IntRange(0, max_floors), 10);//TM Toxic
            tms.Spawns.Add(new InvItem("tm_protect"), new IntRange(0, max_floors), 10);//TM Protect
            tms.Spawns.Add(new InvItem("tm_defog"), new IntRange(0, max_floors), 10);//TM Defog
            tms.Spawns.Add(new InvItem("tm_roar"), new IntRange(0, max_floors), 10);//TM Roar
            tms.Spawns.Add(new InvItem("tm_hyper_beam"), new IntRange(0, max_floors), 5);//TM Hyper Beam
            tms.Spawns.Add(new InvItem("tm_swagger"), new IntRange(0, max_floors), 10);//TM Swagger
            tms.Spawns.Add(new InvItem("tm_fly"), new IntRange(0, max_floors), 10);//TM Fly
            tms.Spawns.Add(new InvItem("tm_facade"), new IntRange(0, max_floors), 10);//TM Façade
            tms.Spawns.Add(new InvItem("tm_rock_climb"), new IntRange(0, max_floors), 10);//TM Rock Climb
            tms.Spawns.Add(new InvItem("tm_payback"), new IntRange(0, max_floors), 10);//TM Payback
            tms.Spawns.Add(new InvItem("tm_giga_impact"), new IntRange(0, max_floors), 5);//TM Giga Impact
            tms.Spawns.Add(new InvItem("tm_dig"), new IntRange(0, max_floors), 10);//TM Dig
            tms.Spawns.Add(new InvItem("tm_substitute"), new IntRange(0, max_floors), 10);//TM Substitute
            tms.Spawns.Add(new InvItem("tm_safeguard"), new IntRange(0, max_floors), 10);//TM Safeguard
            tms.Spawns.Add(new InvItem("tm_swords_dance"), new IntRange(0, max_floors), 5);//TM Swords Dance
            tms.Spawns.Add(new InvItem("tm_work_up"), new IntRange(0, max_floors), 10);//TM Work Up
            tms.Spawns.Add(new InvItem("tm_return"), new IntRange(0, max_floors), 10);//TM Return
            tms.Spawns.Add(new InvItem("tm_frustration"), new IntRange(0, max_floors), 10);//TM Frustration
            tms.Spawns.Add(new InvItem("tm_flash"), new IntRange(0, max_floors), 10);//TM Flash
            tms.Spawns.Add(new InvItem("tm_thief"), new IntRange(0, max_floors), 10);//TM Thief
            tms.Spawns.Add(new InvItem("tm_fling"), new IntRange(0, max_floors), 10);//TM Fling
            tms.Spawns.Add(new InvItem("tm_captivate"), new IntRange(0, max_floors), 10);//TM Captivate
            tms.Spawns.Add(new InvItem("tm_earthquake"), new IntRange(0, max_floors), 10);//TM Earthquake
            tms.Spawns.Add(new InvItem("tm_reflect"), new IntRange(0, max_floors), 10);//TM Reflect
            tms.Spawns.Add(new InvItem("tm_round"), new IntRange(0, max_floors), 10);//TM Round
            tms.Spawns.Add(new InvItem("tm_rain_dance"), new IntRange(0, max_floors), 10);//TM Rain Dance
            tms.Spawns.Add(new InvItem("tm_sunny_day"), new IntRange(0, max_floors), 10);//TM Sunny Day
            tms.Spawns.Add(new InvItem("tm_sandstorm"), new IntRange(0, max_floors), 10);//TM Sandstorm
            tms.Spawns.Add(new InvItem("tm_hail"), new IntRange(0, max_floors), 10);//TM Hail
            tms.Spawns.Add(new InvItem("tm_attract"), new IntRange(0, max_floors), 10);//TM Attract
            tms.Spawns.Add(new InvItem("tm_hidden_power"), new IntRange(0, max_floors), 10);//TM Hidden Power
            tms.Spawns.Add(new InvItem("tm_taunt"), new IntRange(0, max_floors), 10);//TM Taunt
            tms.Spawns.Add(new InvItem("tm_light_screen"), new IntRange(0, max_floors), 10);//TM Light Screen
            tms.Spawns.Add(new InvItem("tm_grass_knot"), new IntRange(0, max_floors), 10);//TM Grass Knot
            tms.Spawns.Add(new InvItem("tm_strength"), new IntRange(0, max_floors), 10);//TM Strength
            tms.Spawns.Add(new InvItem("tm_cut"), new IntRange(0, max_floors), 10);//TM Cut
            tms.Spawns.Add(new InvItem("tm_rock_smash"), new IntRange(0, max_floors), 10);//TM Rock Smash
            tms.Spawns.Add(new InvItem("tm_surf"), new IntRange(0, max_floors), 10);//TM Surf
            tms.Spawns.Add(new InvItem("tm_rest"), new IntRange(0, max_floors), 10);//TM Rest
            tms.Spawns.Add(new InvItem("tm_psych_up"), new IntRange(0, max_floors), 10);//TM Psych Up


            floorSegment.ZoneSteps.Add(itemSpawnZoneStep);


            //mobs
            TeamSpawnZoneStep poolSpawn = new TeamSpawnZoneStep();
            poolSpawn.Priority = PR_RESPAWN_MOB;


            //010 Caterpie : 033 Tackle : 081 String Shot
            poolSpawn.Spawns.Add(GetTeamMob("caterpie", "", "tackle", "string_shot", "", "", new RandRange(3)), new IntRange(0, 3), 10);
            //396 Starly : 033 Tackle
            poolSpawn.Spawns.Add(GetTeamMob("starly", "", "tackle", "", "", "", new RandRange(3)), new IntRange(0, 3), 10);
            //191 Sunkern : 071 Absorb
            poolSpawn.Spawns.Add(GetTeamMob("sunkern", "", "absorb", "", "", "", new RandRange(5)), new IntRange(0, 3), 10);
            //273 Seedot : 117 Bide
            poolSpawn.Spawns.Add(GetTeamMob("seedot", "", "bide", "", "", "", new RandRange(5)), new IntRange(0, 3), 10);
            //161 Sentret : 010 Scratch : 111 Defense Curl
            poolSpawn.Spawns.Add(GetTeamMob("sentret", "", "scratch", "defense_curl", "", "", new RandRange(5)), new IntRange(1, 5), 10);
            //013 Weedle : 040 Poison Sting : 081 String Shot
            poolSpawn.Spawns.Add(GetTeamMob("weedle", "", "poison_sting", "string_shot", "", "", new RandRange(5)), new IntRange(1, 5), 10);
            //050 Diglett : 071 Arena Trap : 310 Astonish : 028 Sand Attack
            poolSpawn.Spawns.Add(GetTeamMob("diglett", "arena_trap", "astonish", "sand_attack", "", "", new RandRange(6)), new IntRange(1, 5), 10);
            //333 Swablu : 030 Natural Cure : 047 Sing : 064 Peck
            poolSpawn.Spawns.Add(GetTeamMob("swablu", "natural_cure", "sing", "peck", "", "", new RandRange(6)), new IntRange(1, 5), 10);
            //309 Electrike : 336 Howl : 033 Tackle
            poolSpawn.Spawns.Add(GetTeamMob("electrike", "", "howl", "tackle", "", "", new RandRange(6)), new IntRange(1, 5), 10);
            //412 Burmy : 033 Tackle
            poolSpawn.Spawns.Add(GetTeamMob("burmy", "", "tackle", "", "", "", new RandRange(10), "turret"), new IntRange(3, 7), 5);
            //412 Burmy : 033 Tackle
            poolSpawn.Spawns.Add(GetTeamMob(new MonsterID("burmy", 1, "", Gender.Unknown), "", "tackle", "", "", "", new RandRange(10), "turret"), new IntRange(3, 7), 5);
            //412 Burmy : 033 Tackle
            poolSpawn.Spawns.Add(GetTeamMob(new MonsterID("burmy", 2, "", Gender.Unknown), "", "tackle", "", "", "", new RandRange(10), "turret"), new IntRange(3, 7), 5);
            //066 Machop : 067 Low Kick : 116 Focus Energy
            poolSpawn.Spawns.Add(GetTeamMob("machop", "", "low_kick", "focus_energy", "", "", new RandRange(8)), new IntRange(3, 7), 10);
            //043 Oddish : 230 Sweet Scent : 051 Acid
            poolSpawn.Spawns.Add(GetTeamMob("oddish", "", "sweet_scent", "acid", "", "", new RandRange(9)), new IntRange(3, 7), 10);
            //300 Skitty : 056 Cute Charm : 252 Fake Out
            poolSpawn.Spawns.Add(GetTeamMob("skitty", "cute_charm", "fake_out", "", "", "", new RandRange(10)), new IntRange(3, 7), 10);
            //209 Snubbull : 044 Bite : 204 Charm
            poolSpawn.Spawns.Add(GetTeamMob("snubbull", "", "bite", "charm", "", "", new RandRange(9)), new IntRange(3, 7), 10);
            //179 Mareep : 178 Cotton Spore : 084 Thunder Shock
            poolSpawn.Spawns.Add(GetTeamMob("mareep", "", "cotton_spore", "thunder_shock", "", "", new RandRange(10)), new IntRange(3, 7), 10);
            //187 Hoppip : 235 Synthesis : 033 Tackle
            poolSpawn.Spawns.Add(GetTeamMob("hoppip", "", "synthesis", "tackle", "", "", new RandRange(10)), new IntRange(3, 7), 10);
            //023 Ekans : 040 Poison Sting : 043 Leer
            poolSpawn.Spawns.Add(GetTeamMob("ekans", "", "poison_sting", "leer", "", "", new RandRange(10)), new IntRange(5, 9), 10);
            //393 Piplup : 128 Defiant : 064 Peck
            poolSpawn.Spawns.Add(GetTeamMob("piplup", "defiant", "peck", "", "", "", new RandRange(12), TeamMemberSpawn.MemberRole.Loner), new IntRange(5, 9), 10);
            //390 Chimchar : 269 Taunt : 052 Ember
            poolSpawn.Spawns.Add(GetTeamMob("chimchar", "", "taunt", "ember", "", "", new RandRange(12), TeamMemberSpawn.MemberRole.Loner), new IntRange(5, 9), 10);
            //387 Turtwig : 110 Withdraw : 071 Absorb
            poolSpawn.Spawns.Add(GetTeamMob("turtwig", "", "withdraw", "absorb", "", "", new RandRange(12), TeamMemberSpawn.MemberRole.Loner), new IntRange(5, 9), 10);
            //133 Eevee : 608 Baby-Doll Eyes : 129 Swift
            poolSpawn.Spawns.Add(GetTeamMob("eevee", "", "baby_doll_eyes", "swift", "", "", new RandRange(11)), new IntRange(5, 9), 10);
            //325 Spoink : 149 Psywave
            poolSpawn.Spawns.Add(GetTeamMob("spoink", "", "psywave", "", "", "", new RandRange(14)), new IntRange(5, 9), 10);
            //438 Bonsly : 313 Fake Tears : 067 Low Kick
            poolSpawn.Spawns.Add(GetTeamMob("bonsly", "", "fake_tears", "low_kick", "", "", new RandRange(14), TeamMemberSpawn.MemberRole.Loner, "weird_tree"), new IntRange(5, 9), 10);
            //077 Ponyta : 172 Flame Wheel : 039 Tail Whip
            poolSpawn.Spawns.Add(GetTeamMob("ponyta", "", "flame_wheel", "tail_whip", "", "", new RandRange(13)), new IntRange(5, 9), 10);
            //060 Poliwag : 346 Water Sport : 055 Water Gun
            poolSpawn.Spawns.Add(GetTeamMob("poliwag", "", "water_sport", "water_gun", "", "", new RandRange(12)), new IntRange(5, 9), 10);
            //193 Yanma : 003 Speed Boost : 049 Sonic Boom
            poolSpawn.Spawns.Add(GetTeamMob("yanma", "speed_boost", "sonic_boom", "", "", "", new RandRange(14)), new IntRange(7, 11), 10);
            //440 Happiny : 383 Copycat : 204 Charm
            poolSpawn.Spawns.Add(GetTeamMob("happiny", "", "copycat", "charm", "", "", new RandRange(16), TeamMemberSpawn.MemberRole.Support), new IntRange(7, 11), 5);
            //215 Sneasel : 196 Icy Wind : 269 Taunt
            poolSpawn.Spawns.Add(GetTeamMob("sneasel", "", "icy_wind", "taunt", "", "", new RandRange(16)), new IntRange(7, 11), 10);
            //274 Nuzleaf : 259 Torment : 075 Razor Leaf
            poolSpawn.Spawns.Add(GetTeamMob("nuzleaf", "", "torment", "razor_leaf", "", "", new RandRange(16)), new IntRange(7, 11), 10);
            //360 Wynaut : 227 Encore : 068 Counter : 243 Mirror Coat
            poolSpawn.Spawns.Add(GetTeamMob("wynaut", "", "encore", "counter", "mirror_coat", "", new RandRange(17)), new IntRange(7, 11), 10);
            //163 Hoothoot : 115 Reflect : 064 Peck
            poolSpawn.Spawns.Add(GetTeamMob("hoothoot", "", "reflect", "peck", "", "", new RandRange(16)), new IntRange(7, 11), 10);
            //074 Geodude : 088 Rock Throw : 111 Defense Curl
            poolSpawn.Spawns.Add(GetTeamMob("geodude", "", "rock_throw", "defense_curl", "", "", new RandRange(16)), new IntRange(7, 11), 10);
            //079 Slowpoke : 050 Disable : 055 Water Gun : 093 Confusion
            poolSpawn.Spawns.Add(GetTeamMob("slowpoke", "", "disable", "water_gun", "confusion", "", new RandRange(17)), new IntRange(7, 11), 10);
            //092 Gastly : 101 Night Shade : 095 Hypnosis
            poolSpawn.Spawns.Add(GetTeamMob("gastly", "", "night_shade", "hypnosis", "", "", new RandRange(19)), new IntRange(9, 13), 10);
            //058 Growlithe : 052 Ember : 046 Roar : 316 Odor Sleuth
            poolSpawn.Spawns.Add(GetTeamMob("growlithe", "", "ember", "roar", "odor_sleuth", "", new RandRange(20), TeamMemberSpawn.MemberRole.Support), new IntRange(9, 13), 10);
            //012 Butterfree : 014 Compound Eyes : 018 Whirlwind : 093 Confusion
            poolSpawn.Spawns.Add(GetTeamMob("butterfree", "compound_eyes", "whirlwind", "confusion", "", "", new RandRange(22)), new IntRange(11, 15), 10);
            //056 Mankey : 069 Seismic Toss : 103 Screech
            poolSpawn.Spawns.Add(GetTeamMob("mankey", "", "seismic_toss", "screech", "", "", new RandRange(23)), new IntRange(11, 15), 10);
            //397 Staravia : 104 Double Team : 018 Whirlwind : 017 Wing Attack
            poolSpawn.Spawns.Add(GetTeamMob("staravia", "", "double_team", "whirlwind", "wing_attack", "", new RandRange(23)), new IntRange(11, 15), 10);
            //216 Teddiursa : 230 Sweet Scent : 343 Covet : 154 Fury Swipes
            poolSpawn.Spawns.Add(GetTeamMob("teddiursa", "", "sweet_scent", "covet", "fury_swipes", "", new RandRange(22)), new IntRange(11, 15), 10);
            //083 Farfetch'd : 128 Defiant : 332 Aerial Ace : 282 Knock Off
            poolSpawn.Spawns.Add(GetTeamMob("farfetchd", "defiant", "aerial_ace", "knock_off", "", "", new RandRange(23)), new IntRange(11, 15), 10);
            //180 Flaaffy : 178 Cotton Spore : 268 Charge : 486 Electro Ball
            poolSpawn.Spawns.Add(GetTeamMob("flaaffy", "", "cotton_spore", "charge", "electro_ball", "", new RandRange(25)), new IntRange(13, 17), 10);
            //227 Skarmory : 191 Spikes : 232 Metal Claw
            poolSpawn.Spawns.Add(GetTeamMob("skarmory", "", "spikes", "metal_claw", "", "", new RandRange(28), TeamMemberSpawn.MemberRole.Leader), new IntRange(13, 17), 10);
            //210 Granbull : 155 Rattled : 424 Fire Fang : 422 Thunder Fang : 423 Ice Fang : 184 Scary Face
            poolSpawn.Spawns.Add(GetTeamMob("granbull", "rattled", "fire_fang", "thunder_fang", "ice_fang", "scary_face", new RandRange(26), TeamMemberSpawn.MemberRole.Leader), new IntRange(13, 17), 10);
            //075 Graveler : 222 Magnitude : 205 Rollout
            poolSpawn.Spawns.Add(GetTeamMob("graveler", "", "magnitude", "rollout", "", "", new RandRange(25), TeamMemberSpawn.MemberRole.Loner), new IntRange(13, 17), 10);
            //188 Skiploom : 073 Leech Seed : 072 Mega Drain : 235 Synthesis
            poolSpawn.Spawns.Add(GetTeamMob("skiploom", "", "leech_seed", "mega_drain", "synthesis", "", new RandRange(26)), new IntRange(13, 17), 10);
            //310 Manectric : 604 Electric Terrain : 209 Spark
            poolSpawn.Spawns.Add(GetTeamMob("manectric", "", "electric_terrain", "spark", "", "", new RandRange(26)), new IntRange(13, 17), 10);
            //015 Beedrill : 390 Toxic Spikes : 228 Pursuit : 041 Twineedle
            poolSpawn.Spawns.Add(GetTeamMob("beedrill", "", "toxic_spikes", "pursuit", "twineedle", "", new RandRange(25)), new IntRange(13, 17), 10);
            //413 Wormadam : 107 Anticipation : 075 Razor Leaf : 074 Growth
            poolSpawn.Spawns.Add(GetTeamMob("wormadam", "anticipation", "razor_leaf", "growth", "", "", new RandRange(29), TeamMemberSpawn.MemberRole.Leader, "turret"), new IntRange(15, 19), 5);
            //413 Wormadam : 107 Anticipation : 350 Rock Blast : 106 Harden
            poolSpawn.Spawns.Add(GetTeamMob(new MonsterID("wormadam", 1, "", Gender.Unknown), "anticipation", "rock_blast", "harden", "", "", new RandRange(29), TeamMemberSpawn.MemberRole.Leader, "turret"), new IntRange(15, 19), 5);
            //413 Wormadam : 107 Anticipation : 429 Mirror Shot : 319 Metal Sound
            poolSpawn.Spawns.Add(GetTeamMob(new MonsterID("wormadam", 2, "", Gender.Unknown), "anticipation", "mirror_shot", "metal_sound", "", "", new RandRange(29), TeamMemberSpawn.MemberRole.Leader, "turret"), new IntRange(15, 19), 5);
            //391 Monferno : 172 Flame Wheel : 259 Torment
            poolSpawn.Spawns.Add(GetTeamMob("monferno", "", "flame_wheel", "torment", "", "", new RandRange(29)), new IntRange(15, 19), 10);
            //213 Shuckle : 227 Encore : 522 Struggle Bug
            poolSpawn.Spawns.Add(GetTeamMob("shuckle", "", "encore", "struggle_bug", "", "", new RandRange(29)), new IntRange(15, 19), 10);
            //067 Machoke : 116 Focus Energy : 490 Low Sweep
            poolSpawn.Spawns.Add(GetTeamMob("machoke", "", "focus_energy", "low_sweep", "", "", new RandRange(29)), new IntRange(15, 19), 10);
            //093 Haunter : 212 Mean Look : 101 Night Shade
            poolSpawn.Spawns.Add(GetTeamMob("haunter", "", "mean_look", "night_shade", "", "", new RandRange(29)), new IntRange(15, 19), 10);
            //051 Dugtrio : 071 Arena Trap : 389 Sucker Punch : 523 Bulldoze
            poolSpawn.Spawns.Add(GetTeamMob("dugtrio", "arena_trap", "sucker_punch", "bulldoze", "", "", new RandRange(29)), new IntRange(15, 19), 10);
            //207 Gligar : 282 Knock Off : 512 Acrobatics
            poolSpawn.Spawns.Add(GetTeamMob("gligar", "", "knock_off", "acrobatics", "", "", new RandRange(29)), new IntRange(15, 19), 10);
            //064 Kadabra : 477 Telekinesis : 060 Psybeam
            poolSpawn.Spawns.Add(GetTeamMob("kadabra", "", "telekinesis", "psybeam", "", "", new RandRange(33)), new IntRange(17, 21), 10);
            //113 Chansey : 516 Bestow : 505 Heal Pulse
            poolSpawn.Spawns.Add(GetTeamMob("chansey", "", "bestow", "heal_pulse", "", "", new RandRange(33), TeamMemberSpawn.MemberRole.Support), new IntRange(17, 21), 5);
            //326 Grumpig : 277 Magic Coat : 149 Psywave : 109 Confuse Ray
            poolSpawn.Spawns.Add(GetTeamMob("grumpig", "", "magic_coat", "psywave", "confuse_ray", "", new RandRange(33)), new IntRange(17, 21), 10);
            //181 Ampharos : 602 Magnetic Flux : 406 Dragon Pulse : 486 Electro Ball
            poolSpawn.Spawns.Add(GetTeamMob("ampharos", "", "magnetic_flux", "dragon_pulse", "electro_ball", "", new RandRange(33)), new IntRange(17, 21), 10);
            //059 Arcanine : 424 Fire Fang : 245 Extreme Speed : 514 Retaliate
            poolSpawn.Spawns.Add(GetTeamMob("arcanine", "", "fire_fang", "extreme_speed", "retaliate", "", new RandRange(33)), new IntRange(17, 21), 10);
            //162 Furret : 193 Foresight : 266 Follow Me : 156 Rest
            poolSpawn.Spawns.Add(GetTeamMob("furret", "", "foresight", "follow_me", "rest", "", new RandRange(36), TeamMemberSpawn.MemberRole.Support), new IntRange(17, 21), 10);
            //189 Jumpluff : 476 Rage Powder : 369 U-turn : 235 Synthesis
            poolSpawn.Spawns.Add(GetTeamMob("jumpluff", "", "rage_powder", "u_turn", "synthesis", "", new RandRange(37), TeamMemberSpawn.MemberRole.Support), new IntRange(19, 23), 10);
            //044 Gloom : 236 Moonlight : 381 Lucky Chant : 072 Mega Drain
            poolSpawn.Spawns.Add(GetTeamMob("gloom", "", "moonlight", "lucky_chant", "mega_drain", "", new RandRange(36), TeamMemberSpawn.MemberRole.Support), new IntRange(19, 23), 10);
            //192 Sunflora : 074 Growth : 076 Solar Beam
            poolSpawn.Spawns.Add(GetTeamMob("sunflora", "", "growth", "solar_beam", "", "", new RandRange(36)), new IntRange(19, 23), 15);
            //182 Bellossom : 241 Sunny Day : 345 Magical Leaf
            poolSpawn.Spawns.Add(GetTeamMob("bellossom", "", "sunny_day", "magical_leaf", "", "", new RandRange(36)), new IntRange(19, 23), 15);
            //301 Delcatty : 096 Normalize : 274 Assist : 047 Sing
            poolSpawn.Spawns.Add(GetTeamMob("delcatty", "normalize", "assist", "sing", "", "", new RandRange(36), TeamMemberSpawn.MemberRole.Support), new IntRange(19, 23), 10);
            //078 Rapidash : 083 Fire Spin : 517 Inferno
            poolSpawn.Spawns.Add(GetTeamMob("rapidash", "", "fire_spin", "inferno", "", "", new RandRange(38), TeamMemberSpawn.MemberRole.Loner), new IntRange(19, 23), 10);
            //389 Torterra : 452 Wood Hammer : 089 Earthquake : 235 Synthesis
            poolSpawn.Spawns.Add(GetTeamMob("torterra", "", "wood_hammer", "earthquake", "synthesis", "", new RandRange(36)), new IntRange(19, 23), 10);
            //398 Staraptor : 097 Agility : 515 Final Gambit : 370 Close Combat
            poolSpawn.Spawns.Add(GetTeamMob("staraptor", "", "agility", "final_gambit", "close_combat", "", new RandRange(39)), new IntRange(21, 25), 10);
            //062 Poliwrath : 358 Wake-Up Slap : 095 Hypnosis
            poolSpawn.Spawns.Add(GetTeamMob("poliwrath", "", "wake_up_slap", "hypnosis", "", "", new RandRange(39)), new IntRange(21, 25), 10);
            //337 Lunatone : 478 Magic Room : 585 Moonblast : 157 Rock Slide
            poolSpawn.Spawns.Add(GetTeamMob("lunatone", "", "magic_room", "moonblast", "rock_slide", "", new RandRange(39)), new IntRange(21, 25), 10);
            //164 Noctowl : 115 Reflect : 138 Dream Eater : 355 Roost
            poolSpawn.Spawns.Add(GetTeamMob("noctowl", "", "reflect", "dream_eater", "roost", "", new RandRange(39), TeamMemberSpawn.MemberRole.Support), new IntRange(21, 25), 10);
            //094 Gengar : 247 Shadow Ball : 095 Hypnosis : 138 Dream Eater
            poolSpawn.Spawns.Add(GetTeamMob("gengar", "", "shadow_ball", "hypnosis", "dream_eater", "", new RandRange(39)), new IntRange(21, 25), 10);
            //057 Primeape : 386 Punishment : 238 Cross Chop
            poolSpawn.Spawns.Add(GetTeamMob("primeape", "", "punishment", "cross_chop", "", "", new RandRange(42)), new IntRange(21, 25), 10);
            //302 Sableye : 212 Mean Look : 282 Knock Off
            poolSpawn.Spawns.Add(GetTeamMob("sableye", "", "mean_look", "knock_off", "", "", new RandRange(42)), new IntRange(23, 28), 10);
            //186 Politoed : 195 Perish Song : 207 Swagger
            poolSpawn.Spawns.Add(GetTeamMob("politoed", "", "perish_song", "swagger", "", "", new RandRange(42), TeamMemberSpawn.MemberRole.Loner), new IntRange(23, 28), 20);
            //065 Alakazam : 105 Recover : 094 Psychic
            poolSpawn.Spawns.Add(GetTeamMob("alakazam", "", "recover", "psychic", "", "", new RandRange(42)), new IntRange(23, 28), 10);
            //080 Slowbro : 505 Heal Pulse : 133 Amnesia : 352 Water Pulse
            poolSpawn.Spawns.Add(GetTeamMob("slowbro", "", "heal_pulse", "amnesia", "water_pulse", "", new RandRange(45)), new IntRange(25, 28), 10);
            //068 Machamp : 099 No Guard : 223 Dynamic Punch : 530 Dual Chop
            poolSpawn.Spawns.Add(GetTeamMob("machamp", "no_guard", "dynamic_punch", "", "", "", new RandRange(45)), new IntRange(25, 28), 10);
            //136 Flareon : 083 Fire Spin : 436 Lava Plume
            poolSpawn.Spawns.Add(GetTeamMob("flareon", "", "fire_spin", "lava_plume", "", "", new RandRange(45)), new IntRange(25, 28), 10);
            //202 Wobbuffet : 219 Safeguard : 068 Counter : 243 Mirror Coat : 227 Encore
            poolSpawn.Spawns.Add(GetTeamMob("wobbuffet", "", "safeguard", "counter", "mirror_coat", "encore", new RandRange(45), TeamMemberSpawn.MemberRole.Loner), new IntRange(25, 28), 10);
            //213 Shuckle : 379 Power Trick : 205 Rollout
            poolSpawn.Spawns.Add(GetTeamMob("shuckle", "", "power_trick", "rollout", "", "", new RandRange(45), TeamMemberSpawn.MemberRole.Loner), new IntRange(25, 31), 10);
            //338 Solrock : 472 Wonder Room : 083 Fire Spin : 397 Rock Polish
            poolSpawn.Spawns.Add(GetTeamMob("solrock", "", "wonder_room", "fire_spin", "rock_polish", "", new RandRange(45)), new IntRange(25, 31), 15);
            //461 Weavile : 251 Beat Up : 386 Punishment : 196 Icy Wind
            poolSpawn.Spawns.Add(GetTeamMob("weavile", "", "beat_up", "punishment", "icy_wind", "", new RandRange(47), TeamMemberSpawn.MemberRole.Loner), new IntRange(25, 31), 10);
            //392 Infernape : 370 Close Combat : 394 Flare Blitz
            poolSpawn.Spawns.Add(GetTeamMob("infernape", "", "close_combat", "flare_blitz", "", "", new RandRange(47)), new IntRange(26, 31), 10);
            //413 Wormadam : 107 Anticipation : 319 Metal Sound : 445 Captivate : 213 Attract
            poolSpawn.Spawns.Add(GetTeamMob(new MonsterID("wormadam", 2, "", Gender.Unknown), "anticipation", "metal_sound", "captivate", "attract", "", new RandRange(48), TeamMemberSpawn.MemberRole.Loner, "turret"), new IntRange(28, 35), 5);
            //413 Wormadam : 107 Anticipation : 437 Leaf Storm : 445 Captivate : 213 Attract
            poolSpawn.Spawns.Add(GetTeamMob("wormadam", "anticipation", "leaf_storm", "captivate", "attract", "", new RandRange(48), TeamMemberSpawn.MemberRole.Loner, "turret"), new IntRange(28, 35), 5);
            //217 Ursaring : 095 Quick Feet : 359 Hammer Arm : 230 Sweet Scent
            poolSpawn.Spawns.Add(GetTeamMob("ursaring", "quick_feet", "hammer_arm", "sweet_scent", "", "", new RandRange(48)), new IntRange(26, 31), 10);
            //024 Arbok : 114 Haze : 380 Gastro Acid : 254 Stockpile : 242 Crunch
            poolSpawn.Spawns.Add(GetTeamMob("arbok", "", "haze", "gastro_acid", "stockpile", "crunch", new RandRange(48), TeamMemberSpawn.MemberRole.Support), new IntRange(28, 31), 20);
            //185 Sudowoodo : 068 Counter : 452 Wood Hammer
            //make this spawn at doorsteps
            poolSpawn.Spawns.Add(GetTeamMob("sudowoodo", "", "counter", "wood_hammer", "", "", new RandRange(48), TeamMemberSpawn.MemberRole.Loner, "weird_tree"), new IntRange(28, 31), 10);
            //469 Yanmega : 003 Speed Boost : 048 Supersonic : 246 Ancient Power
            poolSpawn.Spawns.Add(GetTeamMob("yanmega", "speed_boost", "supersonic", "ancient_power", "", "", new RandRange(48)), new IntRange(31, 35), 10);
            //334 Altaria : 434 Draco Meteor : 219 Safeguard : 363 Natural Gift
            //seek berries on the map
            poolSpawn.Spawns.Add(GetTeamMob("altaria", "", "draco_meteor", "safeguard", "natural_gift", "", new RandRange(48), TeamMemberSpawn.MemberRole.Leader), new IntRange(26, 31), 10);
            //357 Tropius : 139 Harvest : 363 Natural Gift : 437 Leaf Storm
            //seek berries on the map
            poolSpawn.Spawns.Add(GetTeamMob("tropius", "harvest", "natural_gift", "leaf_storm", "", "", new RandRange(48), TeamMemberSpawn.MemberRole.Leader), new IntRange(26, 31), 10);
            //414 Mothim : 110 Tinted Lens : 483 Quiver Dance : 318 Silver Wind
            poolSpawn.Spawns.Add(GetTeamMob("mothim", "tinted_lens", "quiver_dance", "silver_wind", "", "", new RandRange(51)), new IntRange(31, 35), 10);
            //395 Empoleon : 128 Defiant : 065 Drill Peck : 453 Aqua Jet : 014 Swords Dance
            poolSpawn.Spawns.Add(GetTeamMob("empoleon", "defiant", "drill_peck", "aqua_jet", "swords_dance", "", new RandRange(51)), new IntRange(31, 35), 10);
            //062 Poliwrath : 187 Belly Drum : 358 Wake-Up Slap : 095 Hypnosis
            poolSpawn.Spawns.Add(GetTeamMob("poliwrath", "", "belly_drum", "wake_up_slap", "hypnosis", "", new RandRange(51)), new IntRange(31, 35), 10);
            //076 Golem : 089 Earthquake : 300 Mud Sport : 484 Heavy Slam : 205 Rollout
            poolSpawn.Spawns.Add(GetTeamMob("golem", "", "earthquake", "mud_sport", "heavy_slam", "rollout", new RandRange(51)), new IntRange(31, 35), 10);
            //162 Furret : 226 Baton Pass : 133 Amnesia : 156 Rest : 266 Follow Me
            poolSpawn.Spawns.Add(GetTeamMob("furret", "", "baton_pass", "amnesia", "rest", "follow_me", new RandRange(51), TeamMemberSpawn.MemberRole.Support), new IntRange(31, 35), 10);
            //045 Vileplume : 580 Grassy Terrain : 572 Petal Blizzard : 078 Stun Spore
            poolSpawn.Spawns.Add(GetTeamMob("vileplume", "", "grassy_terrain", "petal_blizzard", "stun_spore", "", new RandRange(51)), new IntRange(31, 35), 10);
            //181 Ampharos : 406 Dragon Pulse : 192 Zap Cannon : 324 Signal Beam : 178 Cotton Spore
            poolSpawn.Spawns.Add(GetTeamMob("ampharos", "", "dragon_pulse", "zap_cannon", "signal_beam", "cotton_spore", new RandRange(54)), new IntRange(31, 40), 10);
            //472 Gliscor : 103 Screech : 512 Acrobatics : 423 Ice Fang
            poolSpawn.Spawns.Add(GetTeamMob("gliscor", "", "screech", "acrobatics", "ice_fang", "", new RandRange(54)), new IntRange(31, 40), 10);
            //189 Jumpluff : 079 Sleep Powder : 262 Memento : 235 Synthesis
            poolSpawn.Spawns.Add(GetTeamMob("jumpluff", "", "sleep_powder", "memento", "synthesis", "", new RandRange(57), TeamMemberSpawn.MemberRole.Support), new IntRange(35, 40), 10);
            //302 Sableye : 158 Prankster : 511 Quash : 109 Confuse Ray : 492 Foul Play : 193 Foresight
            poolSpawn.Spawns.Add(GetTeamMob("sableye", "prankster", "quash", "confuse_ray", "foul_play", "foresight", new RandRange(57)), new IntRange(35, 40), 10);
            //078 Rapidash : 097 Agility : 517 Inferno
            poolSpawn.Spawns.Add(GetTeamMob("rapidash", "", "agility", "inferno", "", "", new RandRange(57), TeamMemberSpawn.MemberRole.Loner), new IntRange(35, 40), 10);
            //310 Manectric : 604 Electric Terrain : 435 Discharge : 424 Fire Fang
            poolSpawn.Spawns.Add(GetTeamMob("manectric", "", "electric_terrain", "discharge", "fire_fang", "", new RandRange(57)), new IntRange(35, 40), 10);


            //extra spawns here


            {
                //032 Nidoran♂ : 079 Rivalry : 043 Leer : 064 Peck
                TeamMemberSpawn teamSpawn = GetTeamMob("nidoran_m", "poison_point", "leer", "peck", "", "", new RandRange(6));
                teamSpawn.Spawn.SpawnConditions.Add(new MobCheckVersionDiff(0, 2));
                poolSpawn.Spawns.Add(teamSpawn, new IntRange(1, 5), 10);
            }
            {
                //029 Nidoran♀ : 079 Rivalry : 045 Growl : 010 Scratch
                TeamMemberSpawn teamSpawn = GetTeamMob("nidoran_f", "poison_point", "growl", "scratch", "", "", new RandRange(6));
                teamSpawn.Spawn.SpawnConditions.Add(new MobCheckVersionDiff(1, 2));
                poolSpawn.Spawns.Add(teamSpawn, new IntRange(1, 5), 10);
            }
            {
                //311 Plusle : 589 Play Nice : 270 Helping Hand : 486 Electro Ball
                TeamMemberSpawn teamSpawn = GetTeamMob("plusle", "", "play_nice", "helping_hand", "electro_ball", "", new RandRange(26));
                teamSpawn.Spawn.SpawnConditions.Add(new MobCheckVersionDiff(0, 2));
                poolSpawn.Spawns.Add(teamSpawn, new IntRange(13, 17), 5);
            }
            {
                //312 Minun : 313 Fake Tears : 270 Helping Hand : 609 Nuzzle
                TeamMemberSpawn teamSpawn = GetTeamMob("minun", "", "fake_tears", "helping_hand", "nuzzle", "", new RandRange(26));
                teamSpawn.Spawn.SpawnConditions.Add(new MobCheckVersionDiff(1, 2));
                poolSpawn.Spawns.Add(teamSpawn, new IntRange(13, 17), 5);
            }
            {
                //033 Nidorino : 038 Poison Point : 270 Helping Hand : 024 Double Kick : 040 Poison Sting
                TeamMemberSpawn teamSpawn = GetTeamMob("nidorino", "poison_point", "helping_hand", "double_kick", "poison_sting", "", new RandRange(29), TeamMemberSpawn.MemberRole.Leader);
                teamSpawn.Spawn.SpawnConditions.Add(new MobCheckVersionDiff(0, 2));
                poolSpawn.Spawns.Add(teamSpawn, new IntRange(15, 19), 5);
            }
            {
                //030 Nidorina : 038 Poison Point : 270 Helping Hand : 044 Bite : 040 Poison Sting
                TeamMemberSpawn teamSpawn = GetTeamMob("nidorina", "poison_point", "helping_hand", "bite", "poison_sting", "", new RandRange(29), TeamMemberSpawn.MemberRole.Leader);
                teamSpawn.Spawn.SpawnConditions.Add(new MobCheckVersionDiff(1, 2));
                poolSpawn.Spawns.Add(teamSpawn, new IntRange(15, 19), 5);
            }



            poolSpawn.TeamSizes.Add(1, new IntRange(0, max_floors), 12);
            poolSpawn.TeamSizes.Add(2, new IntRange(8, 12), 3);
            poolSpawn.TeamSizes.Add(2, new IntRange(12, 20), 6);
            poolSpawn.TeamSizes.Add(2, new IntRange(20, max_floors), 8);

            poolSpawn.TeamSizes.Add(3, new IntRange(24, max_floors), 3);
            poolSpawn.TeamSizes.Add(3, new IntRange(27, max_floors), 4);
            floorSegment.ZoneSteps.Add(poolSpawn);


            TileSpawnZoneStep tileSpawn = new TileSpawnZoneStep();
            tileSpawn.Priority = PR_RESPAWN_TRAP;
            tileSpawn.Spawns.Add(new EffectTile("trap_mud", false), new IntRange(0, max_floors), 10);//mud trap
            tileSpawn.Spawns.Add(new EffectTile("trap_warp", true), new IntRange(0, max_floors), 10);//warp trap
            tileSpawn.Spawns.Add(new EffectTile("trap_gust", false), new IntRange(0, max_floors), 10);//gust trap
            tileSpawn.Spawns.Add(new EffectTile("trap_chestnut", false), new IntRange(0, max_floors), 10);//chestnut trap
            tileSpawn.Spawns.Add(new EffectTile("trap_poison", false), new IntRange(0, max_floors), 10);//poison trap
            tileSpawn.Spawns.Add(new EffectTile("trap_slumber", false), new IntRange(0, max_floors), 10);//sleep trap
            tileSpawn.Spawns.Add(new EffectTile("trap_sticky", false), new IntRange(0, max_floors), 10);//sticky trap
            tileSpawn.Spawns.Add(new EffectTile("trap_seal", false), new IntRange(0, max_floors), 10);//seal trap
            tileSpawn.Spawns.Add(new EffectTile("trap_self_destruct", false), new IntRange(0, max_floors), 10);//selfdestruct trap
            tileSpawn.Spawns.Add(new EffectTile("trap_trip", true), new IntRange(0, 15), 10);//trip trap
            tileSpawn.Spawns.Add(new EffectTile("trap_trip", false), new IntRange(15, max_floors), 10);//trip trap
            tileSpawn.Spawns.Add(new EffectTile("trap_hunger", true), new IntRange(0, max_floors), 10);//hunger trap
            tileSpawn.Spawns.Add(new EffectTile("trap_apple", true), new IntRange(0, 15), 3);//apple trap
            tileSpawn.Spawns.Add(new EffectTile("trap_apple", false), new IntRange(15, max_floors), 3);//apple trap
            tileSpawn.Spawns.Add(new EffectTile("trap_pp_leech", true), new IntRange(0, max_floors), 10);//pp-leech trap
            tileSpawn.Spawns.Add(new EffectTile("trap_summon", false), new IntRange(0, max_floors), 10);//summon trap
            tileSpawn.Spawns.Add(new EffectTile("trap_explosion", false), new IntRange(0, max_floors), 10);//explosion trap
            tileSpawn.Spawns.Add(new EffectTile("trap_slow", false), new IntRange(0, max_floors), 10);//slow trap
            tileSpawn.Spawns.Add(new EffectTile("trap_spin", false), new IntRange(0, max_floors), 10);//spin trap
            tileSpawn.Spawns.Add(new EffectTile("trap_grimy", false), new IntRange(0, max_floors), 10);//grimy trap
            tileSpawn.Spawns.Add(new EffectTile("trap_trigger", true), new IntRange(0, max_floors), 20);//trigger trap
                                                                                      //pokemon trap
            tileSpawn.Spawns.Add(new EffectTile("trap_grudge", true), new IntRange(15, max_floors), 10);//grudge trap
                                                                                      //training switch
            floorSegment.ZoneSteps.Add(tileSpawn);


            SpawnList<IGenPriority> appleZoneSpawns = new SpawnList<IGenPriority>();
            appleZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("food_apple"))))), 10);
            SpreadStepZoneStep appleZoneStep = new SpreadStepZoneStep(new SpreadPlanSpaced(new RandRange(3, 5), new IntRange(0, max_floors)), appleZoneSpawns);//apple
            floorSegment.ZoneSteps.Add(appleZoneStep);

            SpawnList<IGenPriority> leppaZoneSpawns = new SpawnList<IGenPriority>();
            leppaZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("berry_leppa"))))), 10);
            SpreadStepZoneStep leppaZoneStep = new SpreadStepZoneStep(new SpreadPlanSpaced(new RandRange(3, 6), new IntRange(0, max_floors)), appleZoneSpawns);//leppa
            floorSegment.ZoneSteps.Add(leppaZoneStep);

            SpawnList<IGenPriority> assemblyZoneSpawns = new SpawnList<IGenPriority>();
            assemblyZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("machine_assembly_box"))))), 10);
            SpreadStepZoneStep assemblyZoneStep = new SpreadStepZoneStep(new SpreadPlanSpaced(new RandRange(3, 7), new IntRange(6, max_floors)), appleZoneSpawns);//assembly box
            floorSegment.ZoneSteps.Add(assemblyZoneStep);

            {
                SpawnList<IGenPriority> apricornZoneSpawns = new SpawnList<IGenPriority>();
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_blue"))))), 10);//blue apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_green"))))), 10);//green apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_brown"))))), 10);//brown apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_purple"))))), 10);//purple apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_red"))))), 10);//red apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_white"))))), 10);//white apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_yellow"))))), 10);//yellow apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_black"))))), 10);//black apricorns
                SpreadStepZoneStep apricornZoneStep = new SpreadStepZoneStep(new SpreadPlanSpaced(new RandRange(4, 7), new IntRange(0, 21)), apricornZoneSpawns);//apricorn (variety)
                floorSegment.ZoneSteps.Add(apricornZoneStep);
            }

            int max_apricorn = 20;
            for (int jj = 0; jj < 3; jj++)
            {
                SpawnList<IGenPriority> apricornZoneSpawns = new SpawnList<IGenPriority>();
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_blue"))))), 10);//blue apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_green"))))), 10);//green apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_brown"))))), 10);//brown apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_purple"))))), 10);//purple apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_red"))))), 10);//red apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_white"))))), 10);//white apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_yellow"))))), 10);//yellow apricorns
                apricornZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("apricorn_black"))))), 10);//black apricorns
                SpreadStepZoneStep apricornZoneStep = new SpreadStepZoneStep(new SpreadPlanQuota(new RandRange(1), new IntRange(1, max_apricorn)), apricornZoneSpawns);//apricorn (variety)
                floorSegment.ZoneSteps.Add(apricornZoneStep);
                max_apricorn /= 2;
            }

            SpawnList<IGenPriority> cleanseZoneSpawns = new SpawnList<IGenPriority>();
            cleanseZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("orb_cleanse"))))), 10);
            SpreadStepZoneStep cleanseZoneStep = new SpreadStepZoneStep(new SpreadPlanQuota(new RandDecay(2, 10, 60), new IntRange(3, max_floors)), cleanseZoneSpawns);//cleanse orb
            floorSegment.ZoneSteps.Add(cleanseZoneStep);

            SpawnList<IGenPriority> evoZoneSpawns = new SpawnList<IGenPriority>();
            SpreadStepZoneStep evoItemZoneStep = new SpreadStepZoneStep(new SpreadPlanQuota(new RandRange(1, 4), new IntRange(0, 15)), evoZoneSpawns);//evo items
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_fire_stone"))))), 10);//Fire Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_leaf_stone"))))), 10);//Leaf Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_water_stone"))))), 10);//Water Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_moon_stone"))))), 10);//Moon Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_sun_stone"))))), 10);//Sun Stone
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_kings_rock"))))), 10);//King's Rock
            evoZoneSpawns.Add(new GenPriority<GenStep<BaseMapGenContext>>(PR_SPAWN_ITEMS, new RandomSpawnStep<BaseMapGenContext, MapItem>(new PickerSpawner<BaseMapGenContext, MapItem>(new PresetMultiRand<MapItem>(new MapItem("evo_link_cable"))))), 10);//Link Cable
            floorSegment.ZoneSteps.Add(evoItemZoneStep);


            SpreadRoomZoneStep evoZoneStep = new SpreadRoomZoneStep(PR_GRID_GEN_EXTRA, PR_ROOMS_GEN_EXTRA, new SpreadPlanSpaced(new RandRange(2, 5), new IntRange(3, max_floors)));
            List<BaseRoomFilter> evoFilters = new List<BaseRoomFilter>();
            evoFilters.Add(new RoomFilterComponent(true, new ImmutableRoom()));
            evoFilters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
            evoZoneStep.Spawns.Add(new RoomGenOption(new RoomGenEvoSmall<MapGenContext>(), new RoomGenEvoSmall<ListMapGenContext>(), evoFilters), 10);
            floorSegment.ZoneSteps.Add(evoZoneStep);


            {
                //monster houses
                SpreadHouseZoneStep monsterChanceZoneStep = new SpreadHouseZoneStep(PR_HOUSES, new SpreadPlanChance(10, new IntRange(3, 30)));
                monsterChanceZoneStep.HouseStepSpawns.Add(new MonsterHouseStep<ListMapGenContext>(GetAntiFilterList(new ImmutableRoom(), new NoEventRoom())), 10);
                foreach (string key in IterateGummis())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, max_floors), 4);//gummis
                foreach (string key in IterateApricorns())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, max_floors), 4);//apricorns
                monsterChanceZoneStep.Items.Add(new MapItem("evo_fire_stone"), new IntRange(0, max_floors), 4);//Fire Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_water_stone"), new IntRange(0, max_floors), 4);//Water Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_moon_stone"), new IntRange(0, max_floors), 4);//Moon Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_leaf_stone"), new IntRange(0, max_floors), 4);//Leaf Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_kings_rock"), new IntRange(0, max_floors), 4);//King's Rock
                monsterChanceZoneStep.Items.Add(new MapItem("evo_link_cable"), new IntRange(0, max_floors), 4);//Link Cable
                monsterChanceZoneStep.Items.Add(new MapItem("evo_sun_stone"), new IntRange(0, max_floors), 4);//Sun Stone
                monsterChanceZoneStep.Items.Add(new MapItem("food_banana"), new IntRange(0, max_floors), 25);//banana
                for (int ii = 0; ii < specific_tms.Length; ii++)
                    monsterChanceZoneStep.Items.Add(new MapItem(specific_tms[ii].Item1), new IntRange(0, max_floors), 2);//TMs
                monsterChanceZoneStep.Items.Add(new MapItem("loot_nugget"), new IntRange(0, max_floors), 10);//nugget
                monsterChanceZoneStep.Items.Add(new MapItem("loot_pearl", 1), new IntRange(0, max_floors), 10);//pearl
                monsterChanceZoneStep.Items.Add(new MapItem("loot_heart_scale", 2), new IntRange(0, max_floors), 10);//heart scale
                monsterChanceZoneStep.Items.Add(new MapItem("key", 1), new IntRange(0, max_floors), 10);//key
                monsterChanceZoneStep.Items.Add(new MapItem("machine_recall_box"), new IntRange(0, max_floors), 30);//link box
                monsterChanceZoneStep.Items.Add(new MapItem("machine_assembly_box"), new IntRange(10, max_floors), 30);//assembly box
                monsterChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, max_floors), 10);//ability capsule


                monsterChanceZoneStep.Items.Add(new MapItem("held_mobile_scarf"), new IntRange(0, max_floors), 1);//Mobile Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_pass_scarf"), new IntRange(0, max_floors), 1);//Pass Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_cover_band"), new IntRange(0, max_floors), 1);//Cover Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_reunion_cape"), new IntRange(0, max_floors), 1);//Reunion Cape
                monsterChanceZoneStep.Items.Add(new MapItem("held_trap_scarf"), new IntRange(0, max_floors), 1);//Trap Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_grip_claw"), new IntRange(0, max_floors), 1);//Grip Claw
                monsterChanceZoneStep.Items.Add(new MapItem("held_twist_band"), new IntRange(0, max_floors), 1);//Twist Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_metronome"), new IntRange(0, max_floors), 1);//Metronome
                monsterChanceZoneStep.Items.Add(new MapItem("held_shell_bell"), new IntRange(0, max_floors), 1);//Shell Bell
                monsterChanceZoneStep.Items.Add(new MapItem("held_scope_lens"), new IntRange(0, max_floors), 1);//Scope Lens
                monsterChanceZoneStep.Items.Add(new MapItem("held_power_band"), new IntRange(0, max_floors), 1);//Power Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_special_band"), new IntRange(0, max_floors), 1);//Special Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_defense_scarf"), new IntRange(0, max_floors), 1);//Defense Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_zinc_band"), new IntRange(0, max_floors), 1);//Zinc Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_wide_lens"), new IntRange(0, max_floors), 1);//Wide Lens
                monsterChanceZoneStep.Items.Add(new MapItem("held_pierce_band"), new IntRange(0, max_floors), 1);//Pierce Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_shed_shell"), new IntRange(0, max_floors), 1);//Shed Shell
                monsterChanceZoneStep.Items.Add(new MapItem("held_x_ray_specs"), new IntRange(0, max_floors), 1);//X-Ray Specs
                monsterChanceZoneStep.Items.Add(new MapItem("held_big_root"), new IntRange(0, max_floors), 1);//Big Root
                monsterChanceZoneStep.Items.Add(new MapItem("held_weather_rock"), new IntRange(0, max_floors), 1);//Weather Rock
                monsterChanceZoneStep.Items.Add(new MapItem("held_expert_belt"), new IntRange(0, max_floors), 1);//Expert Belt
                monsterChanceZoneStep.Items.Add(new MapItem("held_choice_scarf"), new IntRange(0, max_floors), 1);//Choice Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_choice_specs"), new IntRange(0, max_floors), 1);//Choice Specs
                monsterChanceZoneStep.Items.Add(new MapItem("held_choice_band"), new IntRange(0, max_floors), 1);//Choice Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_assault_vest"), new IntRange(0, max_floors), 1);//Assault Vest
                monsterChanceZoneStep.Items.Add(new MapItem("held_life_orb"), new IntRange(0, max_floors), 1);//Life Orb
                monsterChanceZoneStep.Items.Add(new MapItem("held_heal_ribbon"), new IntRange(0, max_floors), 1);//Heal Ribbon

                //monsterChanceZoneStep.ItemThemes.Add(new ItemThemeNone(0, new RandRange(5, 11)), new ParamRange(0, max_floors), 20);
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemThemeNone(80, new RandRange(2, 4))), new IntRange(0, max_floors), 30);//no theme
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeType(ItemData.UseType.Learn, false, true, new RandRange(3, 5)),
                    new ItemThemeRange(true, true, new RandRange(0, 2), ItemArray(IterateMachines()))), new IntRange(0, 30), 10);//TMs + machines
                monsterChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(GummiState)), true, true, new RandRange(3, 7)), new IntRange(0, max_floors), 30);//gummis
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemStateType(new FlagType(typeof(EvoState)), true, true, new RandRange(2, 4))), new IntRange(0, 10), 20);//evo items
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemStateType(new FlagType(typeof(EvoState)), true, true, new RandRange(2, 4))), new IntRange(10, 20), 10);//evo items
                monsterChanceZoneStep.MobThemes.Add(new MobThemeNone(0, new RandRange(7, 13)), new IntRange(0, max_floors), 10);
                floorSegment.ZoneSteps.Add(monsterChanceZoneStep);
            }

            {
                SpreadHouseZoneStep monsterChanceZoneStep = new SpreadHouseZoneStep(PR_HOUSES, new SpreadPlanChance(10, new IntRange(5, 20)));
                monsterChanceZoneStep.HouseStepSpawns.Add(new MonsterHallStep<ListMapGenContext>(new Loc(11, 9), GetAntiFilterList(new ImmutableRoom(), new NoEventRoom())), 10);
                monsterChanceZoneStep.HouseStepSpawns.Add(new MonsterHallStep<ListMapGenContext>(new Loc(15, 13), GetAntiFilterList(new ImmutableRoom(), new NoEventRoom())), 10);

                foreach (string key in IterateGummis())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, max_floors), 4);//gummis
                foreach (string key in IterateApricorns())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, max_floors), 4);//apricorns
                monsterChanceZoneStep.Items.Add(new MapItem("evo_fire_stone"), new IntRange(0, max_floors), 4);//Fire Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_water_stone"), new IntRange(0, max_floors), 4);//Water Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_moon_stone"), new IntRange(0, max_floors), 4);//Moon Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_leaf_stone"), new IntRange(0, max_floors), 4);//Leaf Stone
                monsterChanceZoneStep.Items.Add(new MapItem("evo_kings_rock"), new IntRange(0, max_floors), 4);//King's Rock
                monsterChanceZoneStep.Items.Add(new MapItem("evo_link_cable"), new IntRange(0, max_floors), 4);//Link Cable
                monsterChanceZoneStep.Items.Add(new MapItem("evo_sun_stone"), new IntRange(0, max_floors), 4);//Sun Stone
                monsterChanceZoneStep.Items.Add(new MapItem("food_banana"), new IntRange(0, max_floors), 25);//banana
                for (int ii = 0; ii < specific_tms.Length; ii++)
                    monsterChanceZoneStep.Items.Add(new MapItem(specific_tms[ii].Item1), new IntRange(0, max_floors), 2);//TMs
                monsterChanceZoneStep.Items.Add(new MapItem("loot_nugget"), new IntRange(0, max_floors), 10);//nugget
                monsterChanceZoneStep.Items.Add(new MapItem("loot_pearl", 1), new IntRange(0, max_floors), 10);//pearl
                monsterChanceZoneStep.Items.Add(new MapItem("loot_heart_scale", 2), new IntRange(0, max_floors), 10);//heart scale
                monsterChanceZoneStep.Items.Add(new MapItem("key", 1), new IntRange(0, max_floors), 10);//key
                monsterChanceZoneStep.Items.Add(new MapItem("machine_recall_box"), new IntRange(0, max_floors), 30);//link box
                monsterChanceZoneStep.Items.Add(new MapItem("machine_assembly_box"), new IntRange(10, max_floors), 30);//assembly box
                monsterChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, max_floors), 10);//ability capsule


                monsterChanceZoneStep.Items.Add(new MapItem("held_mobile_scarf"), new IntRange(0, max_floors), 1);//Mobile Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_pass_scarf"), new IntRange(0, max_floors), 1);//Pass Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_cover_band"), new IntRange(0, max_floors), 1);//Cover Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_reunion_cape"), new IntRange(0, max_floors), 1);//Reunion Cape
                monsterChanceZoneStep.Items.Add(new MapItem("held_trap_scarf"), new IntRange(0, max_floors), 1);//Trap Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_grip_claw"), new IntRange(0, max_floors), 1);//Grip Claw
                monsterChanceZoneStep.Items.Add(new MapItem("held_twist_band"), new IntRange(0, max_floors), 1);//Twist Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_metronome"), new IntRange(0, max_floors), 1);//Metronome
                monsterChanceZoneStep.Items.Add(new MapItem("held_shell_bell"), new IntRange(0, max_floors), 1);//Shell Bell
                monsterChanceZoneStep.Items.Add(new MapItem("held_scope_lens"), new IntRange(0, max_floors), 1);//Scope Lens
                monsterChanceZoneStep.Items.Add(new MapItem("held_power_band"), new IntRange(0, max_floors), 1);//Power Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_special_band"), new IntRange(0, max_floors), 1);//Special Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_defense_scarf"), new IntRange(0, max_floors), 1);//Defense Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_zinc_band"), new IntRange(0, max_floors), 1);//Zinc Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_wide_lens"), new IntRange(0, max_floors), 1);//Wide Lens
                monsterChanceZoneStep.Items.Add(new MapItem("held_pierce_band"), new IntRange(0, max_floors), 1);//Pierce Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_shed_shell"), new IntRange(0, max_floors), 1);//Shed Shell
                monsterChanceZoneStep.Items.Add(new MapItem("held_x_ray_specs"), new IntRange(0, max_floors), 1);//X-Ray Specs
                monsterChanceZoneStep.Items.Add(new MapItem("held_big_root"), new IntRange(0, max_floors), 1);//Big Root
                monsterChanceZoneStep.Items.Add(new MapItem("held_weather_rock"), new IntRange(0, max_floors), 1);//Weather Rock
                monsterChanceZoneStep.Items.Add(new MapItem("held_expert_belt"), new IntRange(0, max_floors), 1);//Expert Belt
                monsterChanceZoneStep.Items.Add(new MapItem("held_choice_scarf"), new IntRange(0, max_floors), 1);//Choice Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_choice_specs"), new IntRange(0, max_floors), 1);//Choice Specs
                monsterChanceZoneStep.Items.Add(new MapItem("held_choice_band"), new IntRange(0, max_floors), 1);//Choice Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_assault_vest"), new IntRange(0, max_floors), 1);//Assault Vest
                monsterChanceZoneStep.Items.Add(new MapItem("held_life_orb"), new IntRange(0, max_floors), 1);//Life Orb
                monsterChanceZoneStep.Items.Add(new MapItem("held_heal_ribbon"), new IntRange(0, max_floors), 1);//Heal Ribbon

                //monsterChanceZoneStep.ItemThemes.Add(new ItemThemeNone(0, new RandRange(5, 11)), new ParamRange(0, 30), 20);
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemThemeNone(80, new RandRange(2, 4))), new IntRange(0, 30), 30);//no theme
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeType(ItemData.UseType.Learn, false, true, new RandRange(3, 5)),
                    new ItemThemeRange(true, true, new RandRange(0, 2), ItemArray(IterateMachines()))), new IntRange(0, 30), 10);//TMs + machines

                monsterChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(GummiState)), true, true, new RandRange(3, 7)), new IntRange(0, 30), 30);//gummis
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemStateType(new FlagType(typeof(EvoState)), true, true, new RandRange(2, 4))), new IntRange(0, 10), 20);//evo items
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemStateType(new FlagType(typeof(EvoState)), true, true, new RandRange(2, 4))), new IntRange(10, 20), 10);//evo items
                monsterChanceZoneStep.MobThemes.Add(new MobThemeNone(0, new RandRange(7, 13)), new IntRange(0, max_floors), 10);

                floorSegment.ZoneSteps.Add(monsterChanceZoneStep);
            }

            {
                SpreadHouseZoneStep monsterChanceZoneStep = new SpreadHouseZoneStep(PR_HOUSES, new SpreadPlanChance(20, new IntRange(28, max_floors)));
                monsterChanceZoneStep.HouseStepSpawns.Add(new MonsterHallStep<ListMapGenContext>(new Loc(11, 9), GetAntiFilterList(new ImmutableRoom(), new NoEventRoom())), 10);
                monsterChanceZoneStep.HouseStepSpawns.Add(new MonsterHallStep<ListMapGenContext>(new Loc(15, 13), GetAntiFilterList(new ImmutableRoom(), new NoEventRoom())), 10);

                foreach (string key in IterateGummis())
                    monsterChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, max_floors), 4);//gummis
                monsterChanceZoneStep.Items.Add(new MapItem("food_banana"), new IntRange(0, max_floors), 25);//banana
                for (int ii = 0; ii < specific_tms.Length; ii++)
                    monsterChanceZoneStep.Items.Add(new MapItem(specific_tms[ii].Item1), new IntRange(0, max_floors), 2);//TMs
                monsterChanceZoneStep.Items.Add(new MapItem("loot_pearl", 1), new IntRange(0, max_floors), 10);//pearl
                monsterChanceZoneStep.Items.Add(new MapItem("loot_heart_scale", 2), new IntRange(0, max_floors), 10);//heart scale
                monsterChanceZoneStep.Items.Add(new MapItem("machine_recall_box"), new IntRange(0, max_floors), 30);//link box
                monsterChanceZoneStep.Items.Add(new MapItem("machine_assembly_box"), new IntRange(10, max_floors), 30);//assembly box
                monsterChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, max_floors), 10);//ability capsule


                monsterChanceZoneStep.Items.Add(new MapItem("held_mobile_scarf"), new IntRange(0, max_floors), 1);//Mobile Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_pass_scarf"), new IntRange(0, max_floors), 1);//Pass Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_cover_band"), new IntRange(0, max_floors), 1);//Cover Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_reunion_cape"), new IntRange(0, max_floors), 1);//Reunion Cape
                monsterChanceZoneStep.Items.Add(new MapItem("held_trap_scarf"), new IntRange(0, max_floors), 1);//Trap Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_grip_claw"), new IntRange(0, max_floors), 1);//Grip Claw
                monsterChanceZoneStep.Items.Add(new MapItem("held_twist_band"), new IntRange(0, max_floors), 1);//Twist Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_metronome"), new IntRange(0, max_floors), 1);//Metronome
                monsterChanceZoneStep.Items.Add(new MapItem("held_shell_bell"), new IntRange(0, max_floors), 1);//Shell Bell
                monsterChanceZoneStep.Items.Add(new MapItem("held_scope_lens"), new IntRange(0, max_floors), 1);//Scope Lens
                monsterChanceZoneStep.Items.Add(new MapItem("held_power_band"), new IntRange(0, max_floors), 1);//Power Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_special_band"), new IntRange(0, max_floors), 1);//Special Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_defense_scarf"), new IntRange(0, max_floors), 1);//Defense Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_zinc_band"), new IntRange(0, max_floors), 1);//Zinc Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_wide_lens"), new IntRange(0, max_floors), 1);//Wide Lens
                monsterChanceZoneStep.Items.Add(new MapItem("held_pierce_band"), new IntRange(0, max_floors), 1);//Pierce Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_shed_shell"), new IntRange(0, max_floors), 1);//Shed Shell
                monsterChanceZoneStep.Items.Add(new MapItem("held_x_ray_specs"), new IntRange(0, max_floors), 1);//X-Ray Specs
                monsterChanceZoneStep.Items.Add(new MapItem("held_big_root"), new IntRange(0, max_floors), 1);//Big Root
                monsterChanceZoneStep.Items.Add(new MapItem("held_weather_rock"), new IntRange(0, max_floors), 1);//Weather Rock
                monsterChanceZoneStep.Items.Add(new MapItem("held_expert_belt"), new IntRange(0, max_floors), 1);//Expert Belt
                monsterChanceZoneStep.Items.Add(new MapItem("held_choice_scarf"), new IntRange(0, max_floors), 1);//Choice Scarf
                monsterChanceZoneStep.Items.Add(new MapItem("held_choice_specs"), new IntRange(0, max_floors), 1);//Choice Specs
                monsterChanceZoneStep.Items.Add(new MapItem("held_choice_band"), new IntRange(0, max_floors), 1);//Choice Band
                monsterChanceZoneStep.Items.Add(new MapItem("held_assault_vest"), new IntRange(0, max_floors), 1);//Assault Vest
                monsterChanceZoneStep.Items.Add(new MapItem("held_life_orb"), new IntRange(0, max_floors), 1);//Life Orb
                monsterChanceZoneStep.Items.Add(new MapItem("held_heal_ribbon"), new IntRange(0, max_floors), 1);//Heal Ribbon

                //monsterChanceZoneStep.ItemThemes.Add(new ItemThemeNone(0, new RandRange(5, 11)), new ParamRange(0, 30), 20);
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeRange(true, true, new RandRange(1, 4), "loot_pearl"), new ItemThemeNone(50, new RandRange(2, 4))), new IntRange(0, 30), 20);//no theme
                monsterChanceZoneStep.ItemThemes.Add(new ItemThemeMoney(200, new RandRange(4, 7)), new IntRange(0, max_floors), 20);
                monsterChanceZoneStep.MobThemes.Add(new MobThemeNone(0, new RandRange(7, 13)), new IntRange(0, max_floors), 10);

                floorSegment.ZoneSteps.Add(monsterChanceZoneStep);
            }

            {
                SpreadHouseZoneStep chestChanceZoneStep = new SpreadHouseZoneStep(PR_HOUSES, new SpreadPlanQuota(new RandRange(2, 5), new IntRange(6, max_floors)));
                chestChanceZoneStep.ModStates.Add(new FlagType(typeof(ChestModGenState)));
                chestChanceZoneStep.HouseStepSpawns.Add(new ChestStep<ListMapGenContext>(false, GetAntiFilterList(new ImmutableRoom(), new NoEventRoom())), 10);

                foreach (string key in IterateGummis())
                    chestChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, max_floors), 4);//gummis
                chestChanceZoneStep.Items.Add(new MapItem("apricorn_big"), new IntRange(0, max_floors), 20);//big apricorn
                chestChanceZoneStep.Items.Add(new MapItem("medicine_elixir"), new IntRange(0, max_floors), 80);//elixir
                chestChanceZoneStep.Items.Add(new MapItem("medicine_max_elixir"), new IntRange(0, max_floors), 20);//max elixir
                chestChanceZoneStep.Items.Add(new MapItem("medicine_potion"), new IntRange(0, max_floors), 20);//potion
                chestChanceZoneStep.Items.Add(new MapItem("medicine_max_potion"), new IntRange(0, max_floors), 20);//max potion
                chestChanceZoneStep.Items.Add(new MapItem("medicine_full_heal"), new IntRange(0, max_floors), 20);//full heal
                foreach (string key in IterateXItems())
                    chestChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, max_floors), 15);//X-Items
                for (int ii = 0; ii < specific_tms.Length; ii++)
                    chestChanceZoneStep.Items.Add(new MapItem(specific_tms[ii].Item1), new IntRange(0, max_floors), 2);//TMs
                chestChanceZoneStep.Items.Add(new MapItem("loot_nugget"), new IntRange(0, max_floors), 20);//nugget
                chestChanceZoneStep.Items.Add(new MapItem("loot_heart_scale", 3), new IntRange(0, max_floors), 10);//heart scale
                chestChanceZoneStep.Items.Add(new MapItem("medicine_amber_tear", 1), new IntRange(0, max_floors), 20);//amber tear
                chestChanceZoneStep.Items.Add(new MapItem("ammo_rare_fossil", 3), new IntRange(0, max_floors), 20);//rare fossil
                chestChanceZoneStep.Items.Add(new MapItem("seed_reviver"), new IntRange(0, max_floors), 20);//reviver seed
                chestChanceZoneStep.Items.Add(new MapItem("seed_joy"), new IntRange(0, max_floors), 20);//joy seed
                chestChanceZoneStep.Items.Add(new MapItem("machine_recall_box"), new IntRange(0, max_floors), 30);//link box
                chestChanceZoneStep.Items.Add(new MapItem("machine_assembly_box"), new IntRange(10, max_floors), 30);//assembly box
                chestChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, max_floors), 10);//ability capsule
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeNone(0, new RandRange(2, 5)), new IntRange(0, max_floors), 30);
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeRange(true, true, new RandRange(2, 4), "seed_reviver"), new IntRange(0, max_floors), 10);//reviver seed
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeRange(true, true, new RandRange(1, 4), "seed_joy"), new IntRange(0, max_floors), 10);//joy seed
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeRange(true, true, new RandRange(1, 3), ItemArray(IterateManmades())), new IntRange(0, max_floors), 100);//manmade items
                chestChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(EquipState)), true, true, new RandRange(1, 3)), new IntRange(0, max_floors), 20);//equip
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeRange(true, true, new RandRange(1, 3), ItemArray(IterateTypePlates())), new IntRange(0, max_floors), 10);//plates
                chestChanceZoneStep.ItemThemes.Add(new ItemThemeType(ItemData.UseType.Learn, true, true, new RandRange(1, 3)), new IntRange(0, max_floors), 10);//TMs
                chestChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(GummiState)), true, true, new RandRange(2, 5)), new IntRange(0, max_floors), 20);
                chestChanceZoneStep.ItemThemes.Add(new ItemStateType(new FlagType(typeof(RecruitState)), true, true, new RandRange(1)), new IntRange(0, max_floors), 10);

                floorSegment.ZoneSteps.Add(chestChanceZoneStep);
            }

            //switch vaults
            {
                SpreadVaultZoneStep vaultChanceZoneStep = new SpreadVaultZoneStep(PR_SPAWN_ITEMS_EXTRA, PR_SPAWN_MOBS_EXTRA, new SpreadPlanQuota(new RandRange(1, 4), new IntRange(0, 30)));

                //making room for the vault
                {
                    ResizeFloorStep<ListMapGenContext> vaultStep = new ResizeFloorStep<ListMapGenContext>(new Loc(8, 8), Dir8.DownRight, Dir8.UpLeft);
                    vaultChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_ROOMS_PRE_VAULT, vaultStep));
                }
                {
                    ClampFloorStep<ListMapGenContext> vaultStep = new ClampFloorStep<ListMapGenContext>(new Loc(0), new Loc(78, 54));
                    vaultChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_ROOMS_PRE_VAULT_CLAMP, vaultStep));
                }

                // room addition step
                {
                    SpawnList<RoomGen<ListMapGenContext>> detourRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    detourRooms.Add(new RoomGenCross<ListMapGenContext>(new RandRange(4), new RandRange(4), new RandRange(3), new RandRange(3)), 10);
                    SpawnList<PermissiveRoomGen<ListMapGenContext>> detourHalls = new SpawnList<PermissiveRoomGen<ListMapGenContext>>();
                    detourHalls.Add(new RoomGenAngledHall<ListMapGenContext>(0, new RandRange(2, 4), new RandRange(2, 4)), 10);
                    AddConnectedRoomsStep<ListMapGenContext> detours = new AddConnectedRoomsStep<ListMapGenContext>(detourRooms, detourHalls);
                    detours.Amount = new RandRange(8, 10);
                    detours.HallPercent = 100;
                    detours.Filters.Add(new RoomFilterComponent(true, new NoConnectRoom()));
                    detours.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.SwitchVault));
                    detours.RoomComponents.Set(new NoConnectRoom());
                    detours.RoomComponents.Set(new NoEventRoom());
                    detours.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.SwitchVault));
                    detours.HallComponents.Set(new NoConnectRoom());
                    detours.RoomComponents.Set(new NoEventRoom());

                    vaultChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_ROOMS_GEN_EXTRA, detours));
                }

                //sealing the vault
                {
                    SwitchSealStep<ListMapGenContext> vaultStep = new SwitchSealStep<ListMapGenContext>("sealed_block", "tile_switch", true);
                    vaultStep.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.SwitchVault));
                    vaultStep.SwitchFilters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                    vaultStep.SwitchFilters.Add(new RoomFilterComponent(true, new BossRoom()));
                    vaultChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_TILES_GEN_EXTRA, vaultStep));
                }

                //items for the vault
                {
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_elixir"), new IntRange(0, 30), 800);//elixir
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_max_elixir"), new IntRange(0, 30), 100);//max elixir
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_potion"), new IntRange(0, 30), 200);//potion
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_max_potion"), new IntRange(0, 30), 100);//max potion
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_full_heal"), new IntRange(0, 30), 200);//full heal
                    foreach (string key in IterateXItems())
                        vaultChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 50);//X-Items
                    foreach (string key in IterateGummis())
                        vaultChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, 30), 200);//gummis
                    vaultChanceZoneStep.Items.Add(new MapItem("medicine_amber_tear", 1), new IntRange(0, 30), 2000);//amber tear
                    vaultChanceZoneStep.Items.Add(new MapItem("seed_reviver"), new IntRange(0, 30), 200);//reviver seed
                    vaultChanceZoneStep.Items.Add(new MapItem("seed_joy"), new IntRange(0, 30), 200);//joy seed
                    vaultChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, 30), 200);//ability capsule
                    vaultChanceZoneStep.Items.Add(new MapItem("machine_assembly_box"), new IntRange(0, 30), 500);//assembly box
                    vaultChanceZoneStep.Items.Add(new MapItem("machine_recall_box"), new IntRange(0, 30), 500);//link box
                    vaultChanceZoneStep.Items.Add(new MapItem("evo_harmony_scarf"), new IntRange(0, 30), 100);//harmony scarf
                    vaultChanceZoneStep.Items.Add(new MapItem("orb_itemizer"), new IntRange(15, 30), 50);//itemizer orb
                    vaultChanceZoneStep.Items.Add(new MapItem("wand_transfer", 3), new IntRange(15, 30), 50);//transfer wand
                    vaultChanceZoneStep.Items.Add(new MapItem("key", 1), new IntRange(0, 30), 1000);//key
                }

                // item spawnings for the vault
                {
                    //add a PickerSpawner <- PresetMultiRand <- coins
                    List<MapItem> treasures = new List<MapItem>();
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(new MapItem("boost_iron"));
                    PickerSpawner<ListMapGenContext, MapItem> treasurePicker = new PickerSpawner<ListMapGenContext, MapItem>(new PresetMultiRand<MapItem>(treasures));


                    SpawnList<IStepSpawner<ListMapGenContext, MapItem>> boxSpawn = new SpawnList<IStepSpawner<ListMapGenContext, MapItem>>();

                    //444      ***    Light Box - 1* items
                    {
                        SpawnList<MapItem> silks = new SpawnList<MapItem>();
                        foreach (string key in IterateSilks())
                            silks.Add(new MapItem(key), 10);

                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_light", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(silks, new RandRange(1)))), 10);
                    }

                    //445      ***    Heavy Box - 2* items
                    {
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_heavy", new SpeciesItemContextSpawner<ListMapGenContext>(new IntRange(2), new RandRange(1))), 10);
                    }

                    //447      ***    Dainty Box - Stat ups, wonder gummi, nectar, golden apple, golden banana
                    {
                        SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();

                        foreach (string key in IterateGummis())
                            boxTreasure.Add(new MapItem(key), 1);

                        boxTreasure.Add(new MapItem("boost_protein"), 2);//protein
                        boxTreasure.Add(new MapItem("boost_iron"), 2);//iron
                        boxTreasure.Add(new MapItem("boost_calcium"), 2);//calcium
                        boxTreasure.Add(new MapItem("boost_zinc"), 2);//zinc
                        boxTreasure.Add(new MapItem("boost_carbos"), 2);//carbos
                        boxTreasure.Add(new MapItem("boost_hp_up"), 2);//hp up
                        boxTreasure.Add(new MapItem("boost_nectar"), 2);//nectar

                        boxTreasure.Add(new MapItem("food_apple_perfect"), 10);//perfect apple
                        boxTreasure.Add(new MapItem("food_banana_big"), 10);//big banana
                        boxTreasure.Add(new MapItem("gummi_wonder"), 4);//wonder gummi
                        boxTreasure.Add(new MapItem("seed_joy"), 10);//joy seed
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_dainty", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 3);
                    }

                    //448    Glittery Box - golden apple, amber tear, golden banana, nugget, golden thorn 9
                    {
                        SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();
                        boxTreasure.Add(new MapItem("ammo_golden_thorn"), 10);//golden thorn
                        boxTreasure.Add(new MapItem("medicine_amber_tear"), 10);//Amber Tear
                        boxTreasure.Add(new MapItem("loot_nugget"), 10);//nugget
                        boxTreasure.Add(new MapItem("seed_golden"), 10);//golden seed
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_glittery", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 2);
                    }

                    MultiStepSpawner<ListMapGenContext, MapItem> boxPicker = new MultiStepSpawner<ListMapGenContext, MapItem>(new LoopedRand<IStepSpawner<ListMapGenContext, MapItem>>(boxSpawn, new RandRange(1)));

                    //MultiStepSpawner <- PresetMultiRand
                    MultiStepSpawner<ListMapGenContext, MapItem> mainSpawner = new MultiStepSpawner<ListMapGenContext, MapItem>();
                    mainSpawner.Picker = new PresetMultiRand<IStepSpawner<ListMapGenContext, MapItem>>(treasurePicker, boxPicker);
                    vaultChanceZoneStep.ItemSpawners.SetRange(mainSpawner, new IntRange(0, 30));
                }
                vaultChanceZoneStep.ItemAmount.SetRange(new RandRange(5, 7), new IntRange(0, 30));

                // item placements for the vault
                {
                    RandomRoomSpawnStep<ListMapGenContext, MapItem> detourItems = new RandomRoomSpawnStep<ListMapGenContext, MapItem>();
                    detourItems.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.SwitchVault));
                    vaultChanceZoneStep.ItemPlacements.SetRange(detourItems, new IntRange(0, 30));
                }

                // mobs
                // Vault FOES
                {
                    //37//470 Leafeon : 320 Grass Whistle : 348 Leaf Blade : 235 Synthesis : 241 Sunny Day
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("leafeon", "", "grass_whistle", "leaf_blade", "synthesis", "sunny_day", 3), new IntRange(0, 30), 10);

                    //234 !! Stantler : 43 Leer : 95 Hypnosis : 36 Take Down : 109 Confuse Ray
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("stantler", "", "leer", "hypnosis", "take_down", "confuse_ray", 3), new IntRange(0, 10), 10);

                    //275 Shiftry : 124 Pickpocket : 018 Whirlwind : 417 Nasty Plot : 536 Leaf Tornado : 542 Hurricane
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("shiftry", "pickpocket", "whirlwind", "nasty_plot", "leaf_tornado", "hurricane", 3), new IntRange(10, 30), 10);
                    //131 Lapras : 011 Water Absorb : 058 Ice Beam : 195 Perish Song : 362 Brine
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("lapras", "water_absorb", "ice_beam", "perish_song", "brine", "", 3), new IntRange(10, 30), 10);
                    //53//452 Drapion : 367 Acupressure : 398 Poison Jab : 424 Fire Fang : 565 Fell Stinger
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("drapion", "", "acupressure", "poison_jab", "fire_fang", "fell_stinger", 4), new IntRange(5, 30), 10);
                    //148 Dragonair : 097 Agility : 239 Twister : 082 Dragon Rage
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("dragonair", "", "agility", "twister", "dragon_rage", "", 3), new IntRange(0, 20), 10);
                    //149 Dragonite : 136 Multiscale : 349 Dragon Dance : 355 Roost : 407 Dragon Rush : 401 Aqua Tail
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("dragonite", "multiscale", "dragon_dance", "roost", "dragon_rush", "aqua_tail", 3), new IntRange(15, 30), 10);
                    //327 Spinda : 077 Tangled Feet : 298 Teeter Dance : 037 Thrash
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("spinda", "tangled_feet", "teeter_dance", "thrash", "", "", 3), new IntRange(0, 30), 10);
                    //425 Drifloon : 466 Ominous Wind : 116 Focus Energy : 132 Constrict
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("drifloon", "", "ominous_wind", "focus_energy", "constrict", "", 3), new IntRange(0, 20), 10);
                    //426 Drifblim : 466 Ominous Wind : 226 Baton Pass : 254 Stockpile : 107 Minimize
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("drifblim", "", "ominous_wind", "baton_pass", "stockpile", "minimize", 3), new IntRange(15, 30), 10);
                    //045 Vileplume : 077 Poison Powder : 080 Petal Dance : 051 Acid
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("vileplume", "", "poison_powder", "petal_dance", "acid", "", 3), new IntRange(0, 30), 10);
                    //414 Mothim : 110 Tinted Lens : 483 Quiver Dance : 318 Silver Wind : 094 Psychic
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("mothim", "tinted_lens", "quiver_dance", "silver_wind", "psychic", "", 3), new IntRange(10, 30), 10);
                    //348 Armaldo : 306 Crush Claw : 404 X-Scissor : 479 Smack Down
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("armaldo", "", "crush_claw", "x_scissor", "smack_down", "", 3), new IntRange(0, 30), 10);
                    //346 Cradily : 275 Ingrain : 051 Acid : 378 Wring Out : 362 Brine
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("cradily", "", "ingrain", "acid", "wring_out", "brine", 3), new IntRange(0, 30), 10);
                    //279 Pelipper : 254 Stockpile : 255 Spit Up : 256 Swallow
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("pelipper", "", "stockpile", "spit_up", "swallow", "", 3), new IntRange(0, 30), 10);
                    //700 Sylveon : 182 Pixilate : 129 Swift : 113 Light Screen : 581 Misty Terrain : 585 Moonblast
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("sylveon", "pixilate", "swift", "light_screen", "misty_terrain", "moonblast", 3), new IntRange(0, 30), 10);
                    //134 Vaporeon : 270 Helping Hand : 392 Aqua Ring : 330 Muddy Water
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("vaporeon", "", "helping_hand", "aqua_ring", "muddy_water", "", 3), new IntRange(0, 30), 10);
                    //196 Espeon : 270 Helping Hand : 234 Morning Sun : 248 Future Sight
                    vaultChanceZoneStep.Mobs.Add(GetFOEMob("espeon", "", "helping_hand", "morning_sun", "future_sight", "", 3), new IntRange(0, 30), 10);
                }
                vaultChanceZoneStep.MobAmount.SetRange(new RandRange(7, 11), new IntRange(0, 30));

                // mob placements
                {
                    PlaceRandomMobsStep<ListMapGenContext> secretMobPlacement = new PlaceRandomMobsStep<ListMapGenContext>();
                    secretMobPlacement.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.SwitchVault));
                    secretMobPlacement.ClumpFactor = 20;
                    vaultChanceZoneStep.MobPlacements.SetRange(secretMobPlacement, new IntRange(0, 30));
                }

                floorSegment.ZoneSteps.Add(vaultChanceZoneStep);
            }



            {
                SpreadBossZoneStep bossChanceZoneStep = new SpreadBossZoneStep(PR_ROOMS_GEN_EXTRA, PR_SPAWN_ITEMS_EXTRA, new SpreadPlanQuota(new RandDecay(0, 8, 55), new IntRange(2, 30)));

                {
                    ResizeFloorStep<ListMapGenContext> resizeStep = new ResizeFloorStep<ListMapGenContext>(new Loc(8, 8), Dir8.DownRight, Dir8.UpLeft);
                    bossChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_ROOMS_PRE_VAULT, resizeStep));
                }
                {
                    ClampFloorStep<ListMapGenContext> vaultStep = new ClampFloorStep<ListMapGenContext>(new Loc(0), new Loc(78, 54));
                    bossChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_ROOMS_PRE_VAULT_CLAMP, vaultStep));
                }

                //boss rooms
                string[] customShield = new string[] {          ".XXXXXXX.",
                                                                ".........",
                                                                ".........",
                                                                ".........",
                                                                ".........",
                                                                "X.......X",
                                                                "X.......X",
                                                                "X.......X",
                                                                "XX.....XX"};
                //   skarmbliss
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    //227 Skarmory : 005 Sturdy : 319 Metal Sound : 314 Air Cutter : 191 Spikes : 092 Toxic
                    //242 Blissey : 032 Serene Grace : 069 Seismic Toss : 135 Soft-Boiled : 287 Refresh : 196 Icy Wind
                    mobSpawns.Add(GetBossMob("skarmory", "sturdy", "metal_sound", "air_cutter", "spikes", "toxic", "", new Loc(3, 2)));
                    mobSpawns.Add(GetBossMob("blissey", "serene_grace", "seismic_toss", "soft_boiled", "refresh", "icy_wind", "", new Loc(5, 2)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customShield, new Loc(4, 4), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);
                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }

                string[] customEclipse = new string[] {         "XX.....XX",
                                                                "X.......X",
                                                                ".........",
                                                                ".........",
                                                                ".~.....~.",
                                                                ".~~...~~.",
                                                                "..~~~~~..",
                                                                "X..~~~..X",
                                                                "XX.....XX"};
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    //196 Espeon : 156 Magic Bounce : 094 Psychic : 247 Shadow Ball : 115 Reflect : 605 Dazzling Gleam
                    mobSpawns.Add(GetBossMob("espeon", "magic_bounce", "psychic", "shadow_ball", "reflect", "dazzling_gleam", "", new Loc(3, 2)));
                    //197 Umbreon : 028 Synchronize : 236 Moonlight : 212 Mean Look : 555 Snarl : 399 Dark Pulse
                    mobSpawns.Add(GetBossMob("umbreon", "synchronize", "moonlight", "mean_look", "snarl", "dark_pulse", "", new Loc(5, 2)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customEclipse, new Loc(4, 4), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);
                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }

                string[] customBatteryReverse = new string[] {  ".........",
                                                                "..XXXXX..",
                                                                ".........",
                                                                ".........",
                                                                ".........",
                                                                ".........",
                                                                "....X....",
                                                                "....X....",
                                                                "..XXXXX..",
                                                                "....X....",
                                                                "....X...."};
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    //310 Manectric : 058 Minus : 604 Electric Terrain : 435 Discharge : 053 Flamethrower : 598 Eerie Impulse
                    mobSpawns.Add(GetBossMob("manectric", "minus", "electric_terrain", "discharge", "flamethrower", "eerie_impulse", "", new Loc(3, 5)));
                    //181 Ampharos : 057 Plus : 192 Zap Cannon : 406 Dragon Pulse : 324 Signal Beam : 602 Magnetic Flux
                    mobSpawns.Add(GetBossMob("ampharos", "plus", "zap_cannon", "dragon_pulse", "signal_beam", "magnetic_flux", "", new Loc(5, 5)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customBatteryReverse, new Loc(4, 3), mobSpawns, true), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);
                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }

                string[] customBattery = new string[] {         "....X....",
                                                                "....X....",
                                                                "..XXXXX..",
                                                                "....X....",
                                                                "....X....",
                                                                ".........",
                                                                ".........",
                                                                ".........",
                                                                ".........",
                                                                "..XXXXX..",
                                                                "........."};
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    //311 Plusle : 057 Plus : 087 Thunder : 129 Swift : 417 Nasty Plot : 447 Grass Knot
                    mobSpawns.Add(GetBossMob("plusle", "plus", "thunder", "swift", "nasty_plot", "grass_knot", "", new Loc(3, 5)));
                    //312 Minun : 058 Minus : 240 Rain Dance : 097 Agility : 435 Discharge : 376 Trump Card
                    mobSpawns.Add(GetBossMob("minun", "minus", "rain_dance", "agility", "discharge", "trump_card", "", new Loc(5, 5)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customBattery, new Loc(4, 7), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);
                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }


                string[] customRailway = new string[] {         ".........",
                                                                ".........",
                                                                "..X...X..",
                                                                "..X...X..",
                                                                "..X...X..",
                                                                "..X...X..",
                                                                "..X...X..",
                                                                ".........",
                                                                "........."};
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    //128 Tauros : 022 Intimidate : 099 Rage : 037 Thrash : 523 Bulldoze : 371 Payback
                    mobSpawns.Add(GetBossMob("tauros", "intimidate", "rage", "thrash", "bulldoze", "payback", "", new Loc(4, 3)));
                    //241 Miltank : 113 Scrappy : 208 Milk Drink : 215 Heal Bell : 034 Body Slam : 045 Growl
                    mobSpawns.Add(GetBossMob("miltank", "scrappy", "milk_drink", "heal_bell", "body_slam", "growl", "", new Loc(4, 2)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customRailway, new Loc(4, 5), mobSpawns, true), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);
                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }


                string[] customButterfly = new string[] {       "X.XX.XX.X",
                                                                "...X.X...",
                                                                ".........",
                                                                ".........",
                                                                "X.......X",
                                                                "XX.....XX",
                                                                ".........",
                                                                "...X.X...",
                                                                "X..X.X..X"};
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    //414 Mothim : 110 Tinted Lens : 318 Silver Wind : 483 Quiver Dance : 403 Air Slash : 094 Psychic
                    mobSpawns.Add(GetBossMob("mothim", "tinted_lens", "silver_wind", "quiver_dance", "air_slash", "psychic", "", new Loc(3, 2)));
                    //413 Wormadam : 107 Anticipation : 522 Struggle Bug : 319 Metal Sound : 450 Bug Bite : 527 Electroweb
                    mobSpawns.Add(GetBossMob(new MonsterID("wormadam", 2, "", Gender.Unknown), "anticipation", "struggle_bug", "metal_sound", "bug_bite", "electroweb", "", new Loc(5, 2)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customButterfly, new Loc(4, 4), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);
                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }


                string[] customWaterSwirl = new string[] {      "..~~~~~..",
                                                                ".~.....~.",
                                                                "~..~~~..~",
                                                                "~.~...~.~",
                                                                "~.~.....~",
                                                                "~.~....~.",
                                                                "~..~~~~..",
                                                                ".~......~",
                                                                "..~~~~~~."};
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    //186 Politoed : 002 Drizzle : 054 Mist : 095 Hypnosis : 352 Water Pulse : 058 Ice Beam
                    mobSpawns.Add(GetBossMob("politoed", "drizzle", "mist", "hypnosis", "water_pulse", "ice_beam", "", new Loc(2, 2)));
                    //062 Poliwrath : 033 Swift Swim : 187 Belly Drum : 127 Waterfall : 358 Wake-Up Slap : 509 Circle Throw
                    mobSpawns.Add(GetBossMob("poliwrath", "swift_swim", "belly_drum", "waterfall", "wake_up_slap", "circle_throw", "", new Loc(6, 2)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customWaterSwirl, new Loc(4, 4), mobSpawns, true), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);
                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }

                string[] customCrownWater = new string[] {  ".~~~.~~~.",
                                                            ".~~~.~~~.",
                                                            "..~...~..",
                                                            "..~...~..",
                                                            ".........",
                                                            ".........",
                                                            ".........",
                                                            "........."};
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    //080 Slowbro : 012 Oblivious : 505 Heal Pulse : 244 Psych Up : 352 Water Pulse : 094 Psychic
                    mobSpawns.Add(GetBossMob("slowbro", "oblivious", "heal_pulse", "psych_up", "water_pulse", "psychic", "", new Loc(3, 3)));
                    //199 Slowking : 012 Oblivious : 376 Trump Card : 347 Calm Mind : 408 Power Gem : 281 Yawn
                    mobSpawns.Add(GetBossMob("slowking", "oblivious", "trump_card", "calm_mind", "power_gem", "yawn", "", new Loc(5, 3)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customCrownWater, new Loc(4, 5), mobSpawns, false), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);
                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }

                string[] customCrown = new string[] {       ".XXX.XXX.",
                                                            ".XXX.XXX.",
                                                            "..X...X..",
                                                            "..X...X..",
                                                            ".........",
                                                            ".........",
                                                            ".........",
                                                            "........."};
                {
                    SpawnList<RoomGen<ListMapGenContext>> bossRooms = new SpawnList<RoomGen<ListMapGenContext>>();
                    List<MobSpawn> mobSpawns = new List<MobSpawn>();
                    //034 Nidoking : 125 Sheer Force : 398 Poison Jab : 224 Megahorn : 116 Focus Energy : 529 Drill Run
                    mobSpawns.Add(GetBossMob("nidoking", "sheer_force", "poison_jab", "megahorn", "focus_energy", "drill_run", "", new Loc(3, 3)));
                    //031 Nidoqueen : 079 Rivalry : 270 Helping Hand : 445 Captivate : 414 Earth Power : 482 Sludge Wave
                    mobSpawns.Add(GetBossMob("nidoqueen", "rivalry", "helping_hand", "captivate", "earth_power", "sludge_wave", "", new Loc(5, 3)));
                    bossRooms.Add(CreateRoomGenSpecificBoss<ListMapGenContext>(customCrown, new Loc(4, 5), mobSpawns, true), 10);
                    AddBossRoomStep<ListMapGenContext> detours = CreateGenericBossRoomStep(bossRooms);
                    bossChanceZoneStep.BossSteps.Add(detours, new IntRange(0, 30), 10);
                }

                //sealing the boss room and treasure room
                {
                    BossSealStep<ListMapGenContext> vaultStep = new BossSealStep<ListMapGenContext>("sealed_block", "tile_boss");
                    vaultStep.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.BossLocked));
                    vaultStep.BossFilters.Add(new RoomFilterComponent(false, new BossRoom()));
                    bossChanceZoneStep.VaultSteps.Add(new GenPriority<GenStep<ListMapGenContext>>(PR_TILES_GEN_EXTRA, vaultStep));
                }


                //items for the vault
                {
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_elixir"), new IntRange(0, max_floors), 800);//elixir
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_max_elixir"), new IntRange(0, max_floors), 100);//max elixir
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_potion"), new IntRange(0, max_floors), 200);//potion
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_max_potion"), new IntRange(0, max_floors), 100);//max potion
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_full_heal"), new IntRange(0, max_floors), 200);//full heal
                    foreach (string key in IterateXItems())
                        bossChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, max_floors), 50);//X-Items
                    foreach (string key in IterateGummis())
                        bossChanceZoneStep.Items.Add(new MapItem(key), new IntRange(0, max_floors), 200);//gummis
                    bossChanceZoneStep.Items.Add(new MapItem("medicine_amber_tear", 1), new IntRange(0, max_floors), 200);//amber tear
                    bossChanceZoneStep.Items.Add(new MapItem("seed_reviver"), new IntRange(0, max_floors), 200);//reviver seed
                    bossChanceZoneStep.Items.Add(new MapItem("seed_joy"), new IntRange(0, max_floors), 200);//joy seed
                    bossChanceZoneStep.Items.Add(new MapItem("machine_ability_capsule"), new IntRange(0, max_floors), 200);//ability capsule
                    bossChanceZoneStep.Items.Add(new MapItem("evo_harmony_scarf"), new IntRange(0, max_floors), 100);//harmony scarf
                    bossChanceZoneStep.Items.Add(new MapItem("key", 1), new IntRange(0, 30), 1000);//key

                    bossChanceZoneStep.Items.Add(new MapItem("tm_toxic"), new IntRange(0, 30), 5);//TM Toxic
                    bossChanceZoneStep.Items.Add(new MapItem("tm_rock_climb"), new IntRange(0, 30), 5);//TM Rock Climb
                    bossChanceZoneStep.Items.Add(new MapItem("tm_waterfall"), new IntRange(0, 30), 5);//TM Waterfall
                    bossChanceZoneStep.Items.Add(new MapItem("tm_charge_beam"), new IntRange(0, 30), 5);//TM Charge Beam
                    bossChanceZoneStep.Items.Add(new MapItem("tm_hone_claws"), new IntRange(0, 30), 5);//TM Hone Claws
                    bossChanceZoneStep.Items.Add(new MapItem("tm_giga_impact"), new IntRange(0, 30), 5);//TM Giga Impact
                    bossChanceZoneStep.Items.Add(new MapItem("tm_dig"), new IntRange(0, 30), 5);//TM Dig
                    bossChanceZoneStep.Items.Add(new MapItem("tm_safeguard"), new IntRange(0, 30), 5);//TM Safeguard
                    bossChanceZoneStep.Items.Add(new MapItem("tm_work_up"), new IntRange(0, 30), 5);//TM Work Up
                    bossChanceZoneStep.Items.Add(new MapItem("tm_scald"), new IntRange(0, 30), 5);//TM Scald
                    bossChanceZoneStep.Items.Add(new MapItem("tm_u_turn"), new IntRange(0, 30), 5);//TM U-turn
                    bossChanceZoneStep.Items.Add(new MapItem("tm_thunder_wave"), new IntRange(0, 30), 5);//TM Thunder Wave
                    bossChanceZoneStep.Items.Add(new MapItem("tm_roost"), new IntRange(0, 30), 5);//TM Roost
                    bossChanceZoneStep.Items.Add(new MapItem("tm_psyshock"), new IntRange(0, 30), 5);//TM Psyshock
                    bossChanceZoneStep.Items.Add(new MapItem("tm_thunder"), new IntRange(0, 30), 5);//TM Thunder
                    bossChanceZoneStep.Items.Add(new MapItem("tm_x_scissor"), new IntRange(0, 30), 5);//TM X-Scissor
                    bossChanceZoneStep.Items.Add(new MapItem("tm_taunt"), new IntRange(0, 30), 5);//TM Taunt
                    bossChanceZoneStep.Items.Add(new MapItem("tm_ice_beam"), new IntRange(0, 30), 5);//TM Ice Beam
                    bossChanceZoneStep.Items.Add(new MapItem("tm_light_screen"), new IntRange(0, 30), 5);//TM Light Screen
                    bossChanceZoneStep.Items.Add(new MapItem("tm_calm_mind"), new IntRange(0, 30), 5);//TM Calm Mind
                    bossChanceZoneStep.Items.Add(new MapItem("tm_torment"), new IntRange(0, 30), 5);//TM Torment
                    bossChanceZoneStep.Items.Add(new MapItem("tm_strength"), new IntRange(0, 30), 5);//TM Strength
                    bossChanceZoneStep.Items.Add(new MapItem("tm_cut"), new IntRange(0, 30), 5);//TM Cut
                    bossChanceZoneStep.Items.Add(new MapItem("tm_rock_smash"), new IntRange(0, 30), 5);//TM Rock Smash
                    bossChanceZoneStep.Items.Add(new MapItem("tm_bulk_up"), new IntRange(0, 30), 5);//TM Bulk Up
                    bossChanceZoneStep.Items.Add(new MapItem("tm_rest"), new IntRange(0, 30), 5);//TM Rest

                    bossChanceZoneStep.Items.Add(new MapItem("tm_psychic"), new IntRange(0, 30), 5);//TM Psychic
                    bossChanceZoneStep.Items.Add(new MapItem("tm_flamethrower"), new IntRange(0, 30), 5);//TM Flamethrower
                    bossChanceZoneStep.Items.Add(new MapItem("tm_hyper_beam"), new IntRange(0, 30), 5);//TM Hyper Beam
                    bossChanceZoneStep.Items.Add(new MapItem("tm_blizzard"), new IntRange(0, 30), 5);//TM Blizzard
                    bossChanceZoneStep.Items.Add(new MapItem("tm_rock_slide"), new IntRange(0, 30), 5);//TM Rock Slide
                    bossChanceZoneStep.Items.Add(new MapItem("tm_sludge_wave"), new IntRange(0, 30), 5);//TM Sludge Wave
                    bossChanceZoneStep.Items.Add(new MapItem("tm_swords_dance"), new IntRange(0, 30), 5);//TM Swords Dance
                    bossChanceZoneStep.Items.Add(new MapItem("tm_energy_ball"), new IntRange(0, 30), 5);//TM Energy Ball
                    bossChanceZoneStep.Items.Add(new MapItem("tm_fire_blast"), new IntRange(0, 30), 5);//TM Fire Blast
                    bossChanceZoneStep.Items.Add(new MapItem("tm_thunderbolt"), new IntRange(0, 30), 5);//TM Thunderbolt
                    bossChanceZoneStep.Items.Add(new MapItem("tm_shadow_ball"), new IntRange(0, 30), 5);//TM Shadow Ball
                    bossChanceZoneStep.Items.Add(new MapItem("tm_dark_pulse"), new IntRange(0, 30), 5);//TM Dark Pulse
                    bossChanceZoneStep.Items.Add(new MapItem("tm_earthquake"), new IntRange(0, 30), 5);//TM Earthquake
                    bossChanceZoneStep.Items.Add(new MapItem("tm_frost_breath"), new IntRange(0, 30), 5);//TM Frost Breath
                    bossChanceZoneStep.Items.Add(new MapItem("tm_dazzling_gleam"), new IntRange(0, 30), 5);//TM Dazzling Gleam
                    bossChanceZoneStep.Items.Add(new MapItem("tm_snarl"), new IntRange(0, 30), 5);//TM Snarl
                    bossChanceZoneStep.Items.Add(new MapItem("tm_focus_blast"), new IntRange(0, 30), 5);//TM Focus Blast
                    bossChanceZoneStep.Items.Add(new MapItem("tm_overheat"), new IntRange(0, 30), 5);//TM Overheat
                    bossChanceZoneStep.Items.Add(new MapItem("tm_sludge_bomb"), new IntRange(0, 30), 5);//TM Sludge Bomb
                    bossChanceZoneStep.Items.Add(new MapItem("tm_recycle"), new IntRange(0, 30), 5);//TM Solar Beam
                    bossChanceZoneStep.Items.Add(new MapItem("tm_flash_cannon"), new IntRange(0, 30), 5);//TM Flash Cannon
                    bossChanceZoneStep.Items.Add(new MapItem("tm_surf"), new IntRange(0, 30), 5);//TM Surf

                }

                // item spawnings for the vault
                {
                    //add a PickerSpawner <- PresetMultiRand <- coins
                    List<MapItem> treasures = new List<MapItem>();
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(MapItem.CreateMoney(200));
                    treasures.Add(new MapItem("loot_nugget"));
                    PickerSpawner<ListMapGenContext, MapItem> treasurePicker = new PickerSpawner<ListMapGenContext, MapItem>(new PresetMultiRand<MapItem>(treasures));

                    SpawnList<IStepSpawner<ListMapGenContext, MapItem>> boxSpawn = new SpawnList<IStepSpawner<ListMapGenContext, MapItem>>();

                    //444      ***    Light Box - 1* items
                    {
                        SpawnList<MapItem> silks = new SpawnList<MapItem>();
                        foreach(string key in IterateSilks())
                            silks.Add(new MapItem(key), 10);

                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_light", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(silks, new RandRange(1)))), 10);
                    }

                    //445      ***    Heavy Box - 2* items
                    {
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_heavy", new SpeciesItemContextSpawner<ListMapGenContext>(new IntRange(2), new RandRange(1))), 10);
                    }

                    ////446      ***    Nifty Box - all high tier TMs, ability capsule, heart scale 9, max potion, full heal, max elixir
                    //{
                    //    SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();

                    //    for (int nn = 588; nn < 700; nn++)
                    //        boxTreasure.Add(new MapItem(nn), 1);//TMs
                    //    boxTreasure.Add(new MapItem("machine_ability_capsule"), 100);//ability capsule
                    //    boxTreasure.Add(new MapItem("loot_heart_scale"), 100);//heart scale
                    //    boxTreasure.Add(new MapItem("medicine_potion"), 60);//potion
                    //    boxTreasure.Add(new MapItem("medicine_max_potion"), 30);//max potion
                    //    boxTreasure.Add(new MapItem("medicine_full_heal"), 100);//full heal
                    //    boxTreasure.Add(new MapItem("medicine_elixir"), 60);//elixir
                    //    boxTreasure.Add(new MapItem("medicine_max_elixir"), 30);//max elixir
                    //    boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_nifty", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 10);
                    //}

                    //447      ***    Dainty Box - Stat ups, wonder gummi, nectar, golden apple, golden banana
                    {
                        SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();

                        foreach (string key in IterateGummis())
                            boxTreasure.Add(new MapItem(key), 1);

                        boxTreasure.Add(new MapItem("boost_protein"), 2);//protein
                        boxTreasure.Add(new MapItem("boost_iron"), 2);//iron
                        boxTreasure.Add(new MapItem("boost_calcium"), 2);//calcium
                        boxTreasure.Add(new MapItem("boost_zinc"), 2);//zinc
                        boxTreasure.Add(new MapItem("boost_carbos"), 2);//carbos
                        boxTreasure.Add(new MapItem("boost_hp_up"), 2);//hp up
                        boxTreasure.Add(new MapItem("boost_nectar"), 2);//nectar

                        boxTreasure.Add(new MapItem("food_apple_perfect"), 10);//perfect apple
                        boxTreasure.Add(new MapItem("food_banana_big"), 10);//big banana
                        boxTreasure.Add(new MapItem("gummi_wonder"), 4);//wonder gummi
                        boxTreasure.Add(new MapItem("seed_joy"), 10);//joy seed
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_dainty", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 3);
                    }

                    //448    Glittery Box - golden apple, amber tear, golden banana, nugget, golden thorn 9
                    {
                        SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();
                        boxTreasure.Add(new MapItem("ammo_golden_thorn"), 10);//golden thorn
                        boxTreasure.Add(new MapItem("medicine_amber_tear"), 10);//Amber Tear
                        boxTreasure.Add(new MapItem("loot_nugget"), 10);//nugget
                        boxTreasure.Add(new MapItem("seed_golden"), 10);//golden seed
                        boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_glittery", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 2);
                    }

                    //449      ***    Deluxe Box - Legendary exclusive items, harmony scarf, golden items, stat ups, wonder gummi, perfect apricorn, max potion/full heal/max elixir
                    //{
                    //    SpeciesItemListSpawner<ListMapGenContext> legendSpawner = new SpeciesItemListSpawner<ListMapGenContext>(new IntRange(1, 3), new RandRange(1));
                    //    legendSpawner.Species.Add(144);
                    //    legendSpawner.Species.Add(145);
                    //    legendSpawner.Species.Add(146);
                    //    legendSpawner.Species.Add(150);
                    //    boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_deluxe", legendSpawner), 5);
                    //}
                    //{
                    //    SpawnList<MapItem> boxTreasure = new SpawnList<MapItem>();

                    //    for (int nn = 0; nn < 18; nn++)//Gummi
                    //        boxTreasure.Add(new MapItem(76 + nn), 1);

                    //    boxTreasure.Add(new MapItem("food_apple_golden"), 10);//golden apple
                    //    boxTreasure.Add(new MapItem("food_banana_golden"), 10);//gold banana
                    //    boxTreasure.Add(new MapItem("medicine_amber_tear"), 10);//Amber Tear
                    //    boxTreasure.Add(new MapItem("loot_nugget"), 10);//nugget
                    //    boxTreasure.Add(new MapItem("seed_golden"), 10);//golden seed
                    //    boxTreasure.Add(new MapItem("medicine_max_potion"), 30);//max potion
                    //    boxTreasure.Add(new MapItem("medicine_full_heal"), 100);//full heal
                    //    boxTreasure.Add(new MapItem("medicine_max_elixir"), 30);//max elixir
                    //    boxTreasure.Add(new MapItem("gummi_wonder"), 4);//wonder gummi
                    //    boxTreasure.Add(new MapItem("evo_harmony_scarf"), 1);//harmony scarf
                    //    boxTreasure.Add(new MapItem("apricorn_perfect"), 1);//perfect apricorn
                    //    boxSpawn.Add(new BoxSpawner<ListMapGenContext>("box_deluxe", new PickerSpawner<ListMapGenContext, MapItem>(new LoopedRand<MapItem>(boxTreasure, new RandRange(1)))), 10);
                    //}

                    MultiStepSpawner<ListMapGenContext, MapItem> boxPicker = new MultiStepSpawner<ListMapGenContext, MapItem>(new LoopedRand<IStepSpawner<ListMapGenContext, MapItem>>(boxSpawn, new RandRange(1)));

                    //MultiStepSpawner <- PresetMultiRand
                    MultiStepSpawner<ListMapGenContext, MapItem> mainSpawner = new MultiStepSpawner<ListMapGenContext, MapItem>();
                    mainSpawner.Picker = new PresetMultiRand<IStepSpawner<ListMapGenContext, MapItem>>(treasurePicker, boxPicker);
                    bossChanceZoneStep.ItemSpawners.SetRange(mainSpawner, new IntRange(0, max_floors));
                }
                bossChanceZoneStep.ItemAmount.SetRange(new RandRange(2, 4), new IntRange(0, max_floors));

                // item placements for the vault
                {
                    RandomRoomSpawnStep<ListMapGenContext, MapItem> detourItems = new RandomRoomSpawnStep<ListMapGenContext, MapItem>();
                    detourItems.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.BossLocked));
                    bossChanceZoneStep.ItemPlacements.SetRange(detourItems, new IntRange(0, max_floors));
                }

                floorSegment.ZoneSteps.Add(bossChanceZoneStep);
            }


            SpawnRangeList<IGenPriority> shopZoneSpawns = new SpawnRangeList<IGenPriority>();
            {
                ShopStep<MapGenContext> shop = new ShopStep<MapGenContext>(GetAntiFilterList(new ImmutableRoom(), new NoEventRoom()));
                shop.Personality = 0;
                shop.SecurityStatus = "shop_security";
                shop.Items.Add(new MapItem("berry_oran", 0, 100), 20);//oran
                shop.Items.Add(new MapItem("berry_leppa", 0, 150), 20);//leppa
                shop.Items.Add(new MapItem("berry_lum", 0, 100), 20);//lum
                shop.Items.Add(new MapItem("berry_sitrus", 0, 100), 20);//sitrus
                shop.Items.Add(new MapItem("seed_reviver", 0, 800), 15);//reviver
                shop.Items.Add(new MapItem("seed_ban", 0, 500), 15);//ban
                shop.Items.Add(new MapItem("machine_recall_box", 0, 1200), 20);//Link Box
                shop.Items.Add(new MapItem("machine_assembly_box", 0, 1200), 20);//Assembly Box
                foreach (string key in IterateApricorns())
                    shop.Items.Add(new MapItem(key, 0, 600), 2);//apricorns
                shop.Items.Add(new MapItem("apricorn_plain", 0, 800), 5);//plain apricorn
                foreach (string key in IteratePinchBerries())
                    shop.Items.Add(new MapItem(key, 0, 600), 3);//pinch berries
                foreach (string key in IterateTypeBerries())
                    shop.Items.Add(new MapItem(key, 0, 100), 1);//type berries
                shop.Items.Add(new MapItem("seed_blast", 0, 350), 20);//blast seed
                shop.Items.Add(new MapItem("seed_joy", 0, 2000), 5);//joy seed

                for(int nn = 0; nn < specific_tms.Length; nn++)
                    shop.Items.Add(new MapItem(specific_tms[nn].Item1, 0, specific_tms[nn].Item2), 2);//TMs

                foreach (string key in IterateTypeBoosters())
                    shop.Items.Add(new MapItem(key, 0, 2000), 5);//type items

                shop.ItemThemes.Add(new ItemThemeNone(100, new RandRange(3, 9)), 10);
                shop.ItemThemes.Add(new ItemThemeRange(false, true, new RandRange(3, 5), ItemArray(IterateTypeBoosters())), 10);//type items
                shop.ItemThemes.Add(new ItemThemeMultiple(new ItemThemeType(ItemData.UseType.Learn, false, true, new RandRange(3, 5)),
                    new ItemThemeRange(false, true, new RandRange(0, 3), ItemArray(IterateMachines()))), 10);//TMs + machines

                // 352 Kecleon : 16 color change : 485 synchronoise : 20 bind : 103 screech : 86 thunder wave
                shop.StartMob = GetShopMob("kecleon", "color_change", "synchronoise", "bind", "screech", "thunder_wave", new string[] { "xcl_family_kecleon_00", "xcl_family_kecleon_01", "xcl_family_kecleon_04" }, 0);
                {
                    // 352 Kecleon : 16 color change : 485 synchronoise : 20 bind : 103 screech : 86 thunder wave
                    shop.Mobs.Add(GetShopMob("kecleon", "color_change", "synchronoise", "bind", "screech", "thunder_wave", new string[] { "xcl_family_kecleon_00", "xcl_family_kecleon_01", "xcl_family_kecleon_04" }, -1), 10);
                    // 352 Kecleon : 16 color change : 485 synchronoise : 20 bind : 50 disable : 374 fling
                    shop.Mobs.Add(GetShopMob("kecleon", "color_change", "synchronoise", "bind", "disable", "fling", new string[] { "xcl_family_kecleon_00", "xcl_family_kecleon_01", "xcl_family_kecleon_04" }, -1), 10);
                    // 352 Kecleon : 168 protean : 425 shadow sneak : 246 ancient power : 510 incinerate : 168 thief
                    shop.Mobs.Add(GetShopMob("kecleon", "protean", "shadow_sneak", "ancient_power", "incinerate", "thief", new string[] { "xcl_family_kecleon_00", "xcl_family_kecleon_01", "xcl_family_kecleon_04" }, -1, "shuckle"), 10);
                    // 352 Kecleon : 168 protean : 332 aerial ace : 421 shadow claw : 60 psybeam : 364 feint
                    shop.Mobs.Add(GetShopMob("kecleon", "protean", "aerial_ace", "shadow_claw", "psybeam", "feint", new string[] { "xcl_family_kecleon_00", "xcl_family_kecleon_01", "xcl_family_kecleon_04" }, -1, "shuckle"), 10);
                }

                shopZoneSpawns.Add(new GenPriority<GenStep<MapGenContext>>(PR_SHOPS, shop), new IntRange(5, max_floors), 10);
            }
            {
                ShopStep<MapGenContext> shop = new ShopStep<MapGenContext>(GetAntiFilterList(new ImmutableRoom(), new NoEventRoom()));
                shop.Personality = 1;
                shop.SecurityStatus = "shop_security";
                shop.Items.Add(new MapItem("orb_weather", 0, 300), 20);//weather orb
                shop.Items.Add(new MapItem("orb_mobile", 0, 600), 20);//mobile orb
                shop.Items.Add(new MapItem("orb_luminous", 0, 600), 20);//luminous orb
                shop.Items.Add(new MapItem("orb_fill_in", 0, 400), 20);//fill-in orb
                shop.Items.Add(new MapItem("orb_all_aim", 0, 300), 20);//all-aim orb
                shop.Items.Add(new MapItem("orb_cleanse", 0, 300), 20);//cleanse orb
                shop.Items.Add(new MapItem("orb_one_shot", 0, 600), 20);//one-shot orb
                shop.Items.Add(new MapItem("orb_endure", 0, 500), 20);//endure orb
                shop.Items.Add(new MapItem("orb_pierce", 0, 400), 20);//pierce orb
                shop.Items.Add(new MapItem("orb_stayaway", 0, 600), 20);//stayaway orb
                shop.Items.Add(new MapItem("orb_all_protect", 0, 600), 20);//all protect orb
                shop.Items.Add(new MapItem("orb_trap_see", 0, 300), 20);//trap-see orb
                shop.Items.Add(new MapItem("orb_trapbust", 0, 300), 20);//trapbust orb
                shop.Items.Add(new MapItem("orb_slumber", 0, 500), 20);//slumber orb
                shop.Items.Add(new MapItem("orb_totter", 0, 400), 20);//totter orb
                shop.Items.Add(new MapItem("orb_petrify", 0, 400), 20);//petrify orb
                shop.Items.Add(new MapItem("orb_freeze", 0, 400), 20);//freeze orb
                shop.Items.Add(new MapItem("orb_spurn", 0, 500), 20);//spurn orb
                shop.Items.Add(new MapItem("orb_foe_hold", 0, 500), 20);//foe-hold orb
                shop.Items.Add(new MapItem("orb_nullify", 0, 400), 20);//nullify orb
                shop.Items.Add(new MapItem("orb_all_dodge", 0, 300), 20);//all-dodge orb
                shop.Items.Add(new MapItem("orb_one_room", 0, 500), 20);//one-room orb
                shop.Items.Add(new MapItem("orb_slow", 0, 400), 20);//slow orb
                shop.Items.Add(new MapItem("orb_rebound", 0, 400), 20);//rebound orb
                shop.Items.Add(new MapItem("orb_mirror", 0, 400), 20);//mirror orb
                shop.Items.Add(new MapItem("orb_foe_seal", 0, 400), 20);//foe-seal orb
                shop.Items.Add(new MapItem("orb_halving", 0, 600), 20);//halving orb
                shop.Items.Add(new MapItem("orb_rollcall", 0, 300), 20);//rollcall orb
                shop.Items.Add(new MapItem("orb_mug", 0, 300), 20);//mug orb

                shop.Items.Add(new MapItem("wand_path", 3, 180), 40);//path wand
                shop.Items.Add(new MapItem("wand_pounce", 3, 150), 40);//pounce wand
                shop.Items.Add(new MapItem("wand_whirlwind", 3, 150), 40);//whirlwind wand
                shop.Items.Add(new MapItem("wand_switcher", 3, 150), 40);//switcher wand
                shop.Items.Add(new MapItem("wand_lure", 3, 120), 40);//lure wand
                shop.Items.Add(new MapItem("wand_fear", 3, 150), 40);//slow wand
                shop.Items.Add(new MapItem("wand_topsy_turvy", 3, 150), 40);//topsy-turvy wand
                shop.Items.Add(new MapItem("wand_warp", 3, 150), 40);//warp wand
                shop.Items.Add(new MapItem("wand_purge", 3, 120), 40);//purge wand
                shop.Items.Add(new MapItem("wand_lob", 3, 120), 40);//lob wand

                shop.Items.Add(new MapItem("evo_fire_stone", 0, 2500), 50);//Fire Stone
                shop.Items.Add(new MapItem("evo_water_stone", 0, 2500), 50);//Water Stone
                shop.Items.Add(new MapItem("evo_leaf_stone", 0, 2500), 50);//Leaf Stone
                shop.Items.Add(new MapItem("evo_moon_stone", 0, 3500), 50);//Moon Stone
                shop.Items.Add(new MapItem("evo_sun_stone", 0, 3500), 50);//Sun Stone
                shop.Items.Add(new MapItem("evo_kings_rock", 0, 3500), 50);//King's Rock
                shop.Items.Add(new MapItem("evo_link_cable", 0, 3500), 50);//Link Cable

                foreach (string key in IterateTypeBoosters())
                    shop.Items.Add(new MapItem(key, 0, 2000), 10);//type items


                shop.ItemThemes.Add(new ItemThemeNone(100, new RandRange(3, 9)), 10);
                shop.ItemThemes.Add(new ItemStateType(new FlagType(typeof(EvoState)), false, true, new RandRange(3, 5)), 10);//evo items
                shop.ItemThemes.Add(new ItemThemeRange(false, true, new RandRange(3, 5), ItemArray(IterateTypeBoosters())), 10);//type items

                // Cleffa : 98 Magic Guard : 118 Metronome : 47 Sing : 204 Charm : 313 Fake Tears
                {
                    MobSpawn post_mob = new MobSpawn();
                    post_mob.BaseForm = new MonsterID("cleffa", 0, "normal", Gender.Unknown);
                    post_mob.Tactic = "shopkeeper";
                    post_mob.Level = new RandRange(5);
                    post_mob.Intrinsic = "magic_guard";
                    post_mob.SpecifiedSkills.Add("metronome");
                    post_mob.SpecifiedSkills.Add("sing");
                    post_mob.SpecifiedSkills.Add("charm");
                    post_mob.SpecifiedSkills.Add("fake_tears");
                    post_mob.SpawnFeatures.Add(new MobSpawnDiscriminator(1));
                    post_mob.SpawnFeatures.Add(new MobSpawnInteractable(new BattleScriptEvent("ShopkeeperInteract")));
                    post_mob.SpawnFeatures.Add(new MobSpawnLuaTable("{ Role = \"Shopkeeper\" }"));
                    shop.StartMob = post_mob;
                }
                {
                    // 35 Clefairy : 132 Friend Guard : 282 Knock Off : 107 Minimize : 236 Moonlight : 277 Magic Coat
                    shop.Mobs.Add(GetShopMob("clefairy", "friend_guard", "knock_off", "minimize", "moonlight", "magic_coat", new string[] { "xcl_family_clefairy_00", "xcl_family_clefairy_03" }, -1), 10);
                    // 36 Clefable : 109 Unaware : 118 Metronome : 500 Stored Power : 343 Covet : 271 Trick
                    shop.Mobs.Add(GetShopMob("clefable", "unaware", "metronome", "stored_power", "covet", "trick", new string[] { "xcl_family_clefairy_00", "xcl_family_clefairy_03" }, -1), 5);
                    // 36 Clefable : 98 Magic Guard : 118 Metronome : 213 Attract : 282 Knock Off : 266 Follow Me
                    shop.Mobs.Add(GetShopMob("clefable", "magic_guard", "metronome", "attract", "knock_off", "follow_me", new string[] { "xcl_family_clefairy_00", "xcl_family_clefairy_03" }, -1), 5);
                }

                shopZoneSpawns.Add(new GenPriority<GenStep<MapGenContext>>(PR_SHOPS, shop), new IntRange(5, max_floors), 10);
            }
            SpreadStepRangeZoneStep shopZoneStep = new SpreadStepRangeZoneStep(new SpreadPlanQuota(new RandRange(4, 14), new IntRange(2, 38)), shopZoneSpawns);
            shopZoneStep.ModStates.Add(new FlagType(typeof(ShopModGenState)));
            floorSegment.ZoneSteps.Add(shopZoneStep);

            for (int ii = 0; ii < max_floors; ii++)
            {
                GridFloorGen layout = new GridFloorGen();

                //Floor settings
                MapDataStep<MapGenContext> floorData = new MapDataStep<MapGenContext>();
                floorData.TimeLimit = 1500;
                if (ii <= 4)
                    floorData.Music = "B02. Demonstration 2.ogg";
                else if (ii <= 12)
                    floorData.Music = "Sky Peak Forest.ogg";
                else if (ii <= 16)
                    floorData.Music = "Random Dungeon Theme 3.ogg";
                else if (ii <= 20)
                    floorData.Music = "Miracle Sea.ogg";
                else if (ii <= 27)
                    floorData.Music = "B03. Demonstration 3.ogg";
                else if (ii <= 34)
                    floorData.Music = "Hidden Land.ogg";
                else
                    floorData.Music = "Hidden Highland.ogg";

                if (ii <= 8)
                    floorData.CharSight = Map.SightRange.Dark;
                else if (ii <= 12)
                    floorData.CharSight = Map.SightRange.Clear;
                else
                    floorData.CharSight = Map.SightRange.Dark;

                if (ii <= 4)
                    floorData.TileSight = Map.SightRange.Dark;
                else if (ii <= 24)
                    floorData.TileSight = Map.SightRange.Clear;
                else
                    floorData.TileSight = Map.SightRange.Dark;

                layout.GenSteps.Add(PR_FLOOR_DATA, floorData);

                //Tilesets
                if (ii <= 4)
                    AddTextureData(layout, "purity_forest_9_wall", "purity_forest_9_floor", "purity_forest_9_secondary", "normal");
                else if (ii <= 12)
                    AddTextureData(layout, "sky_peak_4th_pass_wall", "sky_peak_4th_pass_floor", "sky_peak_4th_pass_secondary", "bug");
                else if (ii <= 16)
                    AddTextureData(layout, "forest_area_wall", "forest_area_floor", "forest_area_secondary", "flying");
                else if (ii <= 20)
                    AddTextureData(layout, "forest_path_wall", "forest_path_floor", "forest_path_secondary", "grass");
                else if (ii <= 24)
                    AddTextureData(layout, "western_cave_1_wall", "western_cave_1_floor", "western_cave_1_secondary", "rock");
                else if (ii <= 27)
                    AddTextureData(layout, "western_cave_2_wall", "western_cave_2_floor", "western_cave_2_secondary", "psychic");
                else if (ii <= 34)
                    AddTextureData(layout, "treeshroud_forest_2_wall", "treeshroud_forest_2_floor", "treeshroud_forest_2_secondary", "normal");
                else
                    AddTextureData(layout, "foggy_forest_wall", "foggy_forest_floor", "foggy_forest_secondary", "fairy");

                //wonder tiles
                RandRange wonderTileRange;
                if (ii <= 12)
                    wonderTileRange = new RandRange(3, 6);
                else if (ii <= 20)
                    wonderTileRange = new RandRange(4, 7);
                else if (ii <= 34)
                    wonderTileRange = new RandRange(5, 8);
                else
                    wonderTileRange = new RandRange(0);

                if (ii <= 16)
                    AddSingleTrapStep(layout, wonderTileRange, "tile_wonder", true);
                else
                    AddSingleTrapStep(layout, wonderTileRange, "tile_wonder", false);

                //traps
                RandRange trapRange;
                if (ii <= 8)
                    trapRange = new RandRange(6, 9);
                else if (ii <= 16)
                    trapRange = new RandRange(8, 12);
                else if (ii <= 20)
                    trapRange = new RandRange(10, 14);
                else if (ii <= 24)
                    trapRange = new RandRange(12, 16);
                else
                    trapRange = new RandRange(10, 14);
                AddTrapsSteps(layout, trapRange);


                //money - Ballpark 40K
                if (ii <= 4)
                    AddMoneyData(layout, new RandRange(2, 5));
                else if (ii <= 12)
                    AddMoneyData(layout, new RandRange(4, 8));
                else if (ii <= 20)
                    AddMoneyData(layout, new RandRange(3, 7));
                else
                    AddMoneyData(layout, new RandRange(7, 11));

                if (ii >= 3 && ii < 7)
                {
                    //063 Abra : 100 Teleport
                    //always holds a TM
                    MobSpawn mob = GetGenericMob("abra", "", "teleport", "", "", "", new RandRange(10));
                    MobSpawnItem keySpawn = new MobSpawnItem(true);
                    keySpawn.Items.Add(new InvItem("tm_secret_power"), 10);//TM Secret Power
                    keySpawn.Items.Add(new InvItem("tm_hidden_power"), 10);//TM Hidden Power
                    keySpawn.Items.Add(new InvItem("tm_protect"), 10);//TM Protect
                    keySpawn.Items.Add(new InvItem("tm_double_team"), 10);//TM Double Team
                    keySpawn.Items.Add(new InvItem("tm_attract"), 10);//TM Attract
                    keySpawn.Items.Add(new InvItem("tm_captivate"), 10);//TM Captivate
                    keySpawn.Items.Add(new InvItem("tm_fling"), 10);//TM Fling
                    keySpawn.Items.Add(new InvItem("tm_taunt"), 10);//TM Taunt
                    mob.SpawnFeatures.Add(keySpawn);
                    SpecificTeamSpawner specificTeam = new SpecificTeamSpawner();
                    specificTeam.Spawns.Add(mob);

                    LoopedTeamSpawner<MapGenContext> spawner = new LoopedTeamSpawner<MapGenContext>(specificTeam);
                    {
                        spawner.AmountSpawner = new RandDecay(0, 4, 50);
                    }
                    PlaceRandomMobsStep<MapGenContext> secretMobPlacement = new PlaceRandomMobsStep<MapGenContext>(spawner);
                    layout.GenSteps.Add(PR_SPAWN_MOBS, secretMobPlacement);
                }

                if (ii >= 6 && ii < 9)
                {
                    //147 Dratini : 35 Wrap : 43 Leer
                    SpecificTeamSpawner specificTeam = new SpecificTeamSpawner();
                    specificTeam.Spawns.Add(GetGenericMob("dratini", "", "wrap", "leer", "", "", new RandRange(15)));

                    LoopedTeamSpawner<MapGenContext> spawner = new LoopedTeamSpawner<MapGenContext>(specificTeam);
                    {
                        spawner.AmountSpawner = new RandRange(1, 3);
                    }
                    PlaceDisconnectedMobsStep<MapGenContext> secretMobPlacement = new PlaceDisconnectedMobsStep<MapGenContext>(spawner);
                    secretMobPlacement.AcceptedTiles.Add(new Tile("water"));
                    layout.GenSteps.Add(PR_SPAWN_MOBS, secretMobPlacement);
                }

                //enemies
                if (ii <= 8)
                    AddRespawnData(layout, 10, 85);
                else if (ii <= 16)
                    AddRespawnData(layout, 14, 90);
                else if (ii <= 20)
                    AddRespawnData(layout, 18, 100);
                else if (ii <= 27)
                    AddRespawnData(layout, 26, 110);
                else
                    AddRespawnData(layout, 22, 130);

                //enemies
                if (ii <= 8)
                    AddEnemySpawnData(layout, 30, new RandRange(5, 7));
                else if (ii <= 16)
                    AddEnemySpawnData(layout, 30, new RandRange(7, 12));
                else if (ii <= 20)
                    AddEnemySpawnData(layout, 30, new RandRange(12, 16));
                else if (ii <= 27)
                    AddEnemySpawnData(layout, 20, new RandRange(17, 22));
                else
                    AddEnemySpawnData(layout, 20, new RandRange(13, 18));

                //items
                if (ii <= 8)
                    AddItemData(layout, new RandRange(3, 6), 25);
                else
                    AddItemData(layout, new RandRange(3, 5), 25);

                {
                    List<MapItem> specificSpawns = new List<MapItem>();

                    if (ii > 20 && ii <= 27)
                    {
                        //2 pearls scattered in the maze, 3 pearls in the later floors
                        if (ii > 24)
                            specificSpawns.Add(new MapItem("loot_pearl", 1));//Pearl
                        specificSpawns.Add(new MapItem("loot_pearl", 1));//Pearl
                        specificSpawns.Add(new MapItem("loot_pearl", 1));//Pearl
                    }
                    if (ii == 29)
                        specificSpawns.Add(new MapItem("berry_leppa"));//Leppa Berry
                    if (ii == 39)
                        specificSpawns.Add(new MapItem("loot_gracidea"));//Gracidea

                    RandomSpawnStep<MapGenContext, MapItem> specificItemZoneStep = new RandomSpawnStep<MapGenContext, MapItem>(new PickerSpawner<MapGenContext, MapItem>(new PresetMultiRand<MapItem>(specificSpawns)));
                    layout.GenSteps.Add(PR_SPAWN_ITEMS, specificItemZoneStep);
                }
                {
                    List<MapItem> specificSpawns = new List<MapItem>();

                    if (ii == 0)
                    {
                        specificSpawns.Add(new MapItem("apricorn_blue"));//blue apricorns
                        specificSpawns.Add(new MapItem("apricorn_green"));//green apricorns
                        specificSpawns.Add(new MapItem("apricorn_brown"));//brown apricorns
                        specificSpawns.Add(new MapItem("apricorn_purple"));//purple apricorns
                        specificSpawns.Add(new MapItem("apricorn_red"));//red apricorns
                        specificSpawns.Add(new MapItem("apricorn_white"));//white apricorns
                        specificSpawns.Add(new MapItem("apricorn_yellow"));//yellow apricorns
                        specificSpawns.Add(new MapItem("apricorn_black"));//black apricorns
                    }

                    RandomRoomSpawnStep<MapGenContext, MapItem> specificItemZoneStep = new RandomRoomSpawnStep<MapGenContext, MapItem>(new PickerSpawner<MapGenContext, MapItem>(new LoopedRand<MapItem>(new RandBag<MapItem>(specificSpawns), new RandRange(1))));
                    specificItemZoneStep.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                    layout.GenSteps.Add(PR_SPAWN_ITEMS, specificItemZoneStep);
                }

                SpawnList<MapItem> wallSpawns = new SpawnList<MapItem>();
                wallSpawns.Add(new MapItem("berry_oran"), 50);//oran berry
                wallSpawns.Add(new MapItem("loot_heart_scale", 1), 50);//heart scale
                wallSpawns.Add(new MapItem("ammo_cacnea_spike", 2), 20);//cacnea spike
                wallSpawns.Add(new MapItem("ammo_stick", 2), 20);//stick
                wallSpawns.Add(new MapItem("berry_jaboca"), 10);//jaboca berry
                wallSpawns.Add(new MapItem("berry_rowap"), 10);//rowap berry
                wallSpawns.Add(new MapItem("wand_path", 1), 10);//path wand
                wallSpawns.Add(new MapItem("wand_fear", 3), 10);//fear wand
                wallSpawns.Add(new MapItem("wand_transfer", 2), 10);//transfer wand
                wallSpawns.Add(new MapItem("wand_vanish", 4), 10);//vanish wand

                wallSpawns.Add(new MapItem("berry_apicot"), 10);//apicot berry
                wallSpawns.Add(new MapItem("berry_liechi"), 10);//liechi berry
                wallSpawns.Add(new MapItem("berry_ganlon"), 10);//ganlon berry
                wallSpawns.Add(new MapItem("berry_salac"), 10);//salac berry
                wallSpawns.Add(new MapItem("berry_petaya"), 10);//petaya berry
                wallSpawns.Add(new MapItem("berry_starf"), 10);//starf berry
                wallSpawns.Add(new MapItem("berry_micle"), 10);//micle berry
                wallSpawns.Add(new MapItem("berry_enigma"), 10);//enigma berry

                foreach (string key in IterateTypeBerries())
                    wallSpawns.Add(new MapItem(key), 1);

                TerrainSpawnStep<MapGenContext, MapItem> wallItemZoneStep = new TerrainSpawnStep<MapGenContext, MapItem>(new Tile("wall"));
                wallItemZoneStep.Spawn = new PickerSpawner<MapGenContext, MapItem>(new LoopedRand<MapItem>(wallSpawns, new RandRange(6, 10)));
                layout.GenSteps.Add(PR_SPAWN_ITEMS, wallItemZoneStep);

                //construct paths
                if (ii <= 4)
                {
                    //free form rooms with short halls
                    AddInitGridStep(layout, 4, 4, 8, 8);

                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(90);
                    path.BranchRatio = new RandRange(0, 25);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //blocked
                    genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(4, 8), new RandRange(4, 8), new RandRange(2), new RandRange(2)), 5);
                    //bump
                    genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8), new RandRange(50)), 10);
                    //round
                    genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8)), 10);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(50, 50));

                    AddTunnelStep<MapGenContext> tunneler = new AddTunnelStep<MapGenContext>();
                    tunneler.Halls = new RandRange(2, 5);
                    tunneler.TurnLength = new RandRange(3, 8);
                    tunneler.MaxLength = new RandRange(25);
                    layout.GenSteps.Add(PR_TILES_GEN_TUNNEL, tunneler);

                }
                else if (ii <= 8)
                {
                    //free form rooms with long halls, some of which are 2-wide
                    AddInitGridStep(layout, 4, 4, 10, 10);

                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(80);
                    path.BranchRatio = new RandRange(0, 25);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //blocked
                    genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(4, 8), new RandRange(4, 8), new RandRange(2), new RandRange(2)), 5);
                    //bump
                    genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8), new RandRange(50)), 10);
                    //round
                    genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8)), 10);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(50, 50));

                    {
                        CombineGridRoomStep<MapGenContext> step = new CombineGridRoomStep<MapGenContext>(new RandRange(2, 5), GetImmutableFilterList());
                        step.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                        step.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(1, 2), new RoomGenCave<MapGenContext>(new RandRange(10), new RandRange(18))), 10);
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(2, 1), new RoomGenCave<MapGenContext>(new RandRange(18), new RandRange(10))), 10);
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                    }

                    AddTunnelStep<MapGenContext> tunneler = new AddTunnelStep<MapGenContext>();
                    tunneler.Halls = new RandRange(2, 5);
                    tunneler.TurnLength = new RandRange(3, 8);
                    tunneler.MaxLength = new RandRange(25);
                    layout.GenSteps.Add(PR_TILES_GEN_TUNNEL, tunneler);

                }
                else if (ii <= 12)
                {
                    //wide open clearings with chokepoints
                    AddInitGridStep(layout, 5, 5, 8, 8);

                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(90);
                    path.BranchRatio = new RandRange(0, 25);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //blocked
                    genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(4, 8), new RandRange(3, 7), new RandRange(2), new RandRange(2)), 5);
                    //bump
                    genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8), new RandRange(50)), 10);
                    //round
                    genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8)), 10);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(50, 50));

                    {
                        CombineGridRoomStep<MapGenContext> step = new CombineGridRoomStep<MapGenContext>(new RandRange(2, 6), GetImmutableFilterList());
                        step.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                        step.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(2, 2), new RoomGenCave<MapGenContext>(new RandRange(16), new RandRange(16))), 10);
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(1, 2), new RoomGenCave<MapGenContext>(new RandRange(8), new RandRange(16))), 10);
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(2, 1), new RoomGenCave<MapGenContext>(new RandRange(16), new RandRange(8))), 10);
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                    }
                }
                else if (ii <= 16)
                {
                    //semi-open layout
                    AddInitGridStep(layout, 5, 5, 8, 8);

                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(90);
                    path.BranchRatio = new RandRange(0, 25);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //blocked
                    genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(4, 8), new RandRange(4, 8), new RandRange(2), new RandRange(2)), 5);
                    //bump
                    genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8), new RandRange(50)), 10);
                    //round
                    genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8)), 10);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(50, 50));

                    {
                        CombineGridRoomStep<MapGenContext> step = new CombineGridRoomStep<MapGenContext>(new RandRange(2, 4), GetImmutableFilterList());
                        step.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                        step.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(2, 2), new RoomGenCave<MapGenContext>(new RandRange(16), new RandRange(16))), 10);
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                    }

                    AddTunnelStep<MapGenContext> tunneler = new AddTunnelStep<MapGenContext>();
                    tunneler.Halls = new RandRange(2, 5);
                    tunneler.TurnLength = new RandRange(3, 8);
                    tunneler.MaxLength = new RandRange(25);
                    layout.GenSteps.Add(PR_TILES_GEN_TUNNEL, tunneler);

                }
                else if (ii <= 20)
                {
                    //add rectangular rooms
                    AddInitGridStep(layout, 5, 5, 8, 8);

                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(90);
                    path.BranchRatio = new RandRange(0, 25);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //blocked
                    genericRooms.Add(new RoomGenBlocked<MapGenContext>(new Tile("wall"), new RandRange(4, 8), new RandRange(4, 8), new RandRange(2), new RandRange(2)), 5);
                    //bump
                    genericRooms.Add(new RoomGenBump<MapGenContext>(new RandRange(4, 8), new RandRange(4, 8), new RandRange(50)), 20);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    //layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(75, 50));

                    {
                        CombineGridRoomStep<MapGenContext> step = new CombineGridRoomStep<MapGenContext>(new RandRange(2, 5), GetImmutableFilterList());
                        step.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                        step.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(2, 2), new RoomGenCross<MapGenContext>(new RandRange(4, 15), new RandRange(4, 15), new RandRange(2, 4), new RandRange(2, 4))), 10);
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(1, 2), new RoomGenCross<MapGenContext>(new RandRange(4, 15), new RandRange(4, 15), new RandRange(2, 4), new RandRange(2, 4))), 10);
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(2, 1), new RoomGenCross<MapGenContext>(new RandRange(4, 15), new RandRange(4, 15), new RandRange(2, 4), new RandRange(2, 4))), 10);
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                    }

                    AddTunnelStep<MapGenContext> tunneler = new AddTunnelStep<MapGenContext>();
                    tunneler.Halls = new RandRange(10, 18);
                    tunneler.TurnLength = new RandRange(3, 8);
                    tunneler.MaxLength = new RandRange(25);
                    layout.GenSteps.Add(PR_TILES_GEN_TUNNEL, tunneler);

                }
                else if (ii <= 24)
                {
                    //Initialize a grid of cells.
                    AddInitGridStep(layout, 13, 10, 2, 2, 2);

                    //Create a path that is composed of a branching tree
                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(100);
                    //MODDABLE: can change branching
                    path.BranchRatio = new RandRange(30);

                    //Give it some room types to place
                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //square
                    genericRooms.Add(new RoomGenSquare<MapGenContext>(new RandRange(2), new RandRange(2)), 3);
                    path.GenericRooms = genericRooms;

                    //Give it some hall types to place
                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenSquare<MapGenContext>(new RandRange(1), new RandRange(1)), 20);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    //MODDABLE: can change connectivity
                    {
                        ConnectGridBranchStep<MapGenContext> step = new ConnectGridBranchStep<MapGenContext>(80);
                        step.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        step.Filters.Add(new RoomFilterComponent(true, new NoConnectRoom()));
                        PresetPicker<PermissiveRoomGen<MapGenContext>> picker = new PresetPicker<PermissiveRoomGen<MapGenContext>>();
                        picker.ToSpawn = new RoomGenSquare<MapGenContext>(new RandRange(1), new RandRange(1));
                        step.GenericHalls = picker;
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                    }
                    {
                        SetGridPlanComponentStep<MapGenContext> step = new SetGridPlanComponentStep<MapGenContext>();
                        step.Components.Set(new NoEventRoom());
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                        //layout.GenSteps.Add(PR_GRID_GEN, new MarkAsHallStep<MapGenContext>());
                    }

                    //Combine some rooms for large rooms
                    {
                        CombineGridRoomStep<MapGenContext> step = new CombineGridRoomStep<MapGenContext>(new RandRange(3), GetImmutableFilterList());
                        step.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                        step.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(2), new RoomGenSquare<MapGenContext>(new RandRange(5), new RandRange(5))), 10);
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                    }
                }
                else if (ii <= 27)
                {
                    //Initialize a grid of cells.
                    AddInitGridStep(layout, 16, 13, 2, 2);

                    //Create a path that is composed of a branching tree
                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(100);
                    //MODDABLE: can change branching
                    path.BranchRatio = new RandRange(30);

                    //Give it some room types to place
                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //square
                    genericRooms.Add(new RoomGenSquare<MapGenContext>(new RandRange(2), new RandRange(2)), 3);
                    path.GenericRooms = genericRooms;

                    //Give it some hall types to place
                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenSquare<MapGenContext>(new RandRange(1), new RandRange(1)), 20);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    //MODDABLE: can change connectivity
                    {
                        ConnectGridBranchStep<MapGenContext> step = new ConnectGridBranchStep<MapGenContext>(80);
                        step.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        step.Filters.Add(new RoomFilterComponent(true, new NoConnectRoom()));
                        PresetPicker<PermissiveRoomGen<MapGenContext>> picker = new PresetPicker<PermissiveRoomGen<MapGenContext>>();
                        picker.ToSpawn = new RoomGenSquare<MapGenContext>(new RandRange(1), new RandRange(1));
                        step.GenericHalls = picker;
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                    }
                    {
                        SetGridPlanComponentStep<MapGenContext> step = new SetGridPlanComponentStep<MapGenContext>();
                        step.Components.Set(new NoEventRoom());
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                        //layout.GenSteps.Add(PR_GRID_GEN, new MarkAsHallStep<MapGenContext>());
                    }

                    //Special rooms
                    {
                        AddLargeRoomStep<MapGenContext> step = new AddLargeRoomStep<MapGenContext>(new RandRange(3, 7), GetImmutableFilterList());
                        step.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                        step.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        {
                            LargeRoom<MapGenContext> largeRoom = new LargeRoom<MapGenContext>(new RoomGenSquare<MapGenContext>(new RandRange(8), new RandRange(8)), new Loc(3), 9);
                            largeRoom.OpenBorders[(int)Dir4.Down][1] = true;
                            largeRoom.OpenBorders[(int)Dir4.Left][1] = true;
                            largeRoom.OpenBorders[(int)Dir4.Up][1] = true;
                            largeRoom.OpenBorders[(int)Dir4.Right][1] = true;
                            step.GiantRooms.Add(largeRoom, 10);
                        }
                        {
                            string[] custom = new string[] {  "~~~..~~~",
                                              "~~~..~~~",
                                              "~~#..#~~",
                                              "........",
                                              "........",
                                              "~~#..#~~",
                                              "~~~..~~~",
                                              "~~~..~~~"};
                            LargeRoom<MapGenContext> largeRoom = new LargeRoom<MapGenContext>(CreateRoomGenSpecific<MapGenContext>(custom), new Loc(3), 2);
                            largeRoom.OpenBorders[(int)Dir4.Down][1] = true;
                            largeRoom.OpenBorders[(int)Dir4.Left][1] = true;
                            largeRoom.OpenBorders[(int)Dir4.Up][1] = true;
                            largeRoom.OpenBorders[(int)Dir4.Right][1] = true;
                            step.GiantRooms.Add(largeRoom, 10);
                        }
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                    }
                    //Combine some rooms for large rooms
                    {
                        CombineGridRoomStep<MapGenContext> step = new CombineGridRoomStep<MapGenContext>(new RandRange(3), GetImmutableFilterList());
                        step.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                        step.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                        step.Combos.Add(new GridCombo<MapGenContext>(new Loc(2), new RoomGenSquare<MapGenContext>(new RandRange(5), new RandRange(5))), 10);
                        layout.GenSteps.Add(PR_GRID_GEN, step);
                    }
                }
                else
                {
                    //prim maze with caves
                    AddInitGridStep(layout, 5, 4, 12, 12);

                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(80);
                    path.BranchRatio = new RandRange(0, 25);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //cross
                    genericRooms.Add(new RoomGenCross<MapGenContext>(new RandRange(4, 11), new RandRange(4, 11), new RandRange(2, 6), new RandRange(2, 6)), 10);
                    //round
                    genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(5, 9), new RandRange(5, 9)), 10);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    AddTunnelStep<MapGenContext> tunneler = new AddTunnelStep<MapGenContext>();
                    tunneler.Halls = new RandRange(20, 28);
                    tunneler.TurnLength = new RandRange(3, 8);
                    tunneler.MaxLength = new RandRange(25);
                    layout.GenSteps.Add(PR_TILES_GEN_TUNNEL, tunneler);
                }

                AddDrawGridSteps(layout);

                if (ii <= 27)
                    AddStairStep(layout, false);
                else
                {
                    //the main stairs becomes the exit stairs
                    EffectTile exitTile = new EffectTile("stairs_exit_up", true);
                    exitTile.TileStates.Set(new DestState(SegLoc.Invalid));
                    var step = new FloorStairsStep<MapGenContext, MapGenEntrance, MapGenExit>(new MapGenEntrance(Dir8.Down), new MapGenExit(exitTile));
                    step.Filters.Add(new RoomFilterConnectivity(ConnectivityRoom.Connectivity.Main));
                    step.Filters.Add(new RoomFilterComponent(true, new BossRoom()));
                    layout.GenSteps.Add(PR_EXITS, step);

                    if (ii == 28)
                    {
                        //the next floor is all in visible tiles, as a secret stairs.
                        //It will always be in the same room as the exit stairs if possible
                        EffectTile secretTile = new EffectTile("stairs_go_down", true);
                        NearSpawnableSpawnStep<MapGenContext, EffectTile, MapGenExit> trapStep = new NearSpawnableSpawnStep<MapGenContext, EffectTile, MapGenExit>(new PickerSpawner<MapGenContext, EffectTile>(new PresetMultiRand<EffectTile>(secretTile)), 100);
                        layout.GenSteps.Add(PR_SPAWN_TRAPS, trapStep);
                    }
                    else if (ii <= 34)
                    {
                        //the next floor is in whatever room, but exposed
                        EffectTile secretTile = new EffectTile("stairs_go_down", true);
                        RandomSpawnStep<MapGenContext, EffectTile> trapStep = new RandomSpawnStep<MapGenContext, EffectTile>(new PickerSpawner<MapGenContext, EffectTile>(new PresetMultiRand<EffectTile>(secretTile)));
                        layout.GenSteps.Add(PR_SPAWN_TRAPS, trapStep);
                    }
                    else if (ii < 39)
                    {
                        //the next floor will be in whatever room, hidden
                        EffectTile secretTile = new EffectTile("stairs_go_down", false);
                        RandomSpawnStep<MapGenContext, EffectTile> trapStep = new RandomSpawnStep<MapGenContext, EffectTile>(new PickerSpawner<MapGenContext, EffectTile>(new PresetMultiRand<EffectTile>(secretTile)));
                        layout.GenSteps.Add(PR_SPAWN_TRAPS, trapStep);
                    }
                }

                if (ii <= 4)
                {
                    //nothing
                }
                else if (ii < 9)
                    AddWaterSteps(layout, "water", new RandRange(30), false);//water
                else if (ii <= 12)
                    AddWaterSteps(layout, "water", new RandRange(30));//water
                else if (ii <= 16)
                    AddWaterSteps(layout, "water", new RandRange(25));//water
                else if (ii <= 20)
                    AddWaterSteps(layout, "water", new RandRange(15));//water
                else
                    AddWaterSteps(layout, "water", new RandRange(22));//water



                floorSegment.Floors.Add(layout);
            }

            zone.Segments.Add(floorSegment);

            zone.GroundMaps.Add("garden_end");
        }
        #endregion

        #region CAVE OF WHISPERS
        static void FillCaveOfWhispers(ZoneData zone)
        {
            zone.Name = new LocalText("**Cave of Whispers");
            zone.Level = 5;
            zone.LevelCap = true;
            zone.Rescues = 2;
            zone.Rogue = RogueStatus.AllTransfer;

            LayeredSegment floorSegment = new LayeredSegment();
            floorSegment.IsRelevant = true;
            floorSegment.ZoneSteps.Add(new SaveVarsZoneStep(PR_EXITS_RESCUE));
            floorSegment.ZoneSteps.Add(new FloorNameIDZoneStep(PR_FLOOR_DATA, new LocalText("Cave of Whispers\nB{0}F")));

            //money
            MoneySpawnZoneStep moneySpawnZoneStep = new MoneySpawnZoneStep(PR_RESPAWN_MONEY, new RandRange(63, 72), new RandRange(21, 24));
            moneySpawnZoneStep.ModStates.Add(new FlagType(typeof(CoinModGenState)));
            floorSegment.ZoneSteps.Add(moneySpawnZoneStep);

            //items!
            ItemSpawnZoneStep itemSpawnZoneStep = new ItemSpawnZoneStep();
            itemSpawnZoneStep.Priority = PR_RESPAWN_ITEM;
            floorSegment.ZoneSteps.Add(itemSpawnZoneStep);

            //mobs
            TeamSpawnZoneStep poolSpawn = new TeamSpawnZoneStep();
            poolSpawn.Priority = PR_RESPAWN_MOB;
            //put something here
            poolSpawn.TeamSizes.Add(1, new IntRange(0, 16), 12);
            floorSegment.ZoneSteps.Add(poolSpawn);

            TileSpawnZoneStep tileSpawn = new TileSpawnZoneStep();
            tileSpawn.Priority = PR_RESPAWN_TRAP;
            floorSegment.ZoneSteps.Add(tileSpawn);


            for (int ii = 0; ii < 16; ii++)
            {
                GridFloorGen layout = new GridFloorGen();


                //Floor settings
                if (ii < 8)
                    AddFloorData(layout, "Star Cave.ogg", 3000, Map.SightRange.Clear, Map.SightRange.Dark);
                else if (ii < 12)
                    AddFloorData(layout, "Limestone Cavern.ogg", 3000, Map.SightRange.Dark, Map.SightRange.Dark);
                else
                    AddFloorData(layout, "Deep Limestone Cavern.ogg", 3000, Map.SightRange.Dark, Map.SightRange.Dark);

                //Tilesets
                if (ii < 2)
                    AddTextureData(layout, "waterfall_pond_wall", "waterfall_pond_floor", "waterfall_pond_secondary", "rock");
                else if (ii < 4)
                    AddTextureData(layout, "unused_waterfall_pond_wall", "unused_waterfall_pond_floor", "unused_waterfall_pond_secondary", "rock");
                else if (ii < 6)
                    AddTextureData(layout, "crystal_cave_1_wall", "crystal_cave_1_floor", "crystal_cave_1_secondary", "rock");
                else if (ii < 8)
                    AddTextureData(layout, "crystal_cave_2_wall", "crystal_cave_2_floor", "crystal_cave_2_secondary", "rock");
                else if (ii < 10)
                    AddTextureData(layout, "lapis_cave_wall", "lapis_cave_floor", "lapis_cave_secondary", "rock");
                else if (ii < 12)
                    AddTextureData(layout, "southern_cavern_2_wall", "southern_cavern_2_floor", "southern_cavern_2_secondary", "rock");
                else
                    AddTextureData(layout, "wish_cave_2_wall", "wish_cave_2_floor", "wish_cave_2_secondary", "rock");

                //traps
                AddSingleTrapStep(layout, new RandRange(2, 4), "tile_wonder");//wonder tile
                AddTrapsSteps(layout, new RandRange(6, 9));

                //money - 9,280P to 25,536P
                if (ii < 8)
                    AddMoneyData(layout, new RandRange(1, 5));
                else
                    AddMoneyData(layout, new RandRange(3, 7));

                //1F Key guaranteed
                //SpecificItemZoneStep<MapGenContext> specificItemZoneStep = new SpecificItemZoneStep<MapGenContext>();
                //layout.ZoneSteps.Add(specificItemZoneStep);

                //enemies! ~ up to lv 20
                AddRespawnData(layout, 3, 80);

                //enemies
                AddEnemySpawnData(layout, 20, new RandRange(2, 4));


                //items
                if (ii < 8)
                    AddItemData(layout, new RandRange(2, 6), 25);
                else
                    AddItemData(layout, new RandRange(3, 6), 25);


                //construct paths
                {
                    AddInitGridStep(layout, 4, 4, 10, 10);

                    GridPathBranch<MapGenContext> path = new GridPathBranch<MapGenContext>();
                    path.RoomComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.HallComponents.Set(new ConnectivityRoom(ConnectivityRoom.Connectivity.Main));
                    path.RoomRatio = new RandRange(90);
                    path.BranchRatio = new RandRange(0, 25);

                    SpawnList<RoomGen<MapGenContext>> genericRooms = new SpawnList<RoomGen<MapGenContext>>();
                    //cross
                    genericRooms.Add(new RoomGenCross<MapGenContext>(new RandRange(4, 11), new RandRange(4, 11), new RandRange(2, 6), new RandRange(2, 6)), 10);
                    //round
                    genericRooms.Add(new RoomGenRound<MapGenContext>(new RandRange(5, 9), new RandRange(5, 9)), 10);
                    path.GenericRooms = genericRooms;

                    SpawnList<PermissiveRoomGen<MapGenContext>> genericHalls = new SpawnList<PermissiveRoomGen<MapGenContext>>();
                    genericHalls.Add(new RoomGenAngledHall<MapGenContext>(50), 10);
                    path.GenericHalls = genericHalls;

                    layout.GenSteps.Add(PR_GRID_GEN, path);

                    layout.GenSteps.Add(PR_GRID_GEN, CreateGenericConnect(75, 50));

                }

                AddDrawGridSteps(layout);

                AddStairStep(layout, false);

                AddWaterSteps(layout, "water", new RandRange(30));//water

                floorSegment.Floors.Add(layout);
            }

            zone.Segments.Add(floorSegment);
        }
        #endregion

    }
}
