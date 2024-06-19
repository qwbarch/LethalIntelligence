﻿using UnityEngine;
using HarmonyLib;
using Unity.Netcode;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using LethalNetworkAPI;
using System.Collections.Generic;
using static LethalIntelligence.Patches.MaskedAIRevamp;
using System.Runtime.CompilerServices;
using Steamworks.ServerList;

namespace LethalIntelligence.Patches
{
    [HarmonyPatch(typeof(FlowermanAI))]
    internal class BrackenPatches
    {
        private enum Personality
        {
            None,
            Normal,
            Angry,
            Calm,
            Stealthy,
            Friendly
        }

        public static LethalNetworkVariable<int> BrackenPersonalityInt = new LethalNetworkVariable<int>("BrackenPersonalityInt");

        static List<Personality> useablePersonalities = new List<Personality>();
        static Personality brackenPersonality, lastBrackenPersonality;
        static FlowermanAI brackenEnemy;
        static float angerModifier = 0f;

        static string lastDebugMsg, currentMoon, currentInterior;

        private static bool stopStatusReporting = false;

        /*[HarmonyPrefix]
        [HarmonyPatch("Awake")]
        private static bool Awake_Prefix(EnemyAI __instance)
        {
            Plugin.mls.LogInfo("LI-BrackenAWAKE!");
            brackenEnemy = (FlowermanAI)__instance;
            return true; // dont skip original
        }*/

        [HarmonyPrefix]
        [HarmonyPatch("Start")]
        private static bool Start_Prefix(EnemyAI __instance)
        {
            brackenEnemy = (FlowermanAI)__instance;
            selectAvailablePersonalities();
            return true; // dont skip original
        }


        [HarmonyPrefix]
        [HarmonyPatch("Update")]
        private static bool Update_Prefix()
        {
            BrackenStatusReport();
            if (brackenPersonality == Personality.None)
            {
                BrackenPersonalityInt.Value = Random.Range(0, useablePersonalities.Count);
            }
            brackenPersonality = useablePersonalities[BrackenPersonalityInt.Value];
            if (lastBrackenPersonality != brackenPersonality)
            {
                lastBrackenPersonality = brackenPersonality;
                if (brackenPersonality == Personality.Angry)
                {
                    angerModifier = 2f;
                }
                else if(brackenPersonality == Personality.Calm)
                {
                    angerModifier = -0.015f;
                }
                Plugin.mls.LogInfo("Bracken '" + brackenEnemy.GetInstanceID() + "' personality changed to '" + brackenPersonality.ToString() + "'");
            }
            return true; // dont skip original
        }

        [HarmonyPrefix]
        [HarmonyPatch("AddToAngerMeter")]
        private static bool AddToAngerMeter_Prefix(ref float amountToAdd)
        {
            //Plugin.mls.LogError("AngerToAdd = " + amountToAdd);
            amountToAdd += angerModifier;
            //Plugin.mls.LogError("ModifiedAngerToAdd = " + amountToAdd);
            return true; // dont skip original
        }


        private static void selectAvailablePersonalities()
        {
            useablePersonalities = new List<Personality>();
            if (Plugin.enableBrackenNormal)
            {
                useablePersonalities.Add(Personality.Normal);
            }
            if (Plugin.enableBrackenAngry)
            {
                useablePersonalities.Add(Personality.Angry);
            }
            if (Plugin.enableBrackenCalm)
            {
                useablePersonalities.Add(Personality.Calm);
            }
            if (Plugin.enableBrackenStealthy)
            {
                useablePersonalities.Add(Personality.Stealthy);
            }
            if (Plugin.enableBrackenFriendly)
            {
                useablePersonalities.Add(Personality.Friendly);
            }
        }

        public static void BrackenStatusReport()
        {
            if (Plugin.DebugMode && !stopStatusReporting && Plugin.delayUpdate())
            {
                string targetPlayer;

                if (brackenEnemy.targetPlayer == null)
                {
                    targetPlayer = "null";
                }
                else
                {
                    targetPlayer = brackenEnemy.targetPlayer.ToString();
                }

                string debugMsg =
                "\n===== BrackenStatusReport() Start =====" +
                "\nBrackenID = " + brackenEnemy.GetInstanceID() +
                "\nBrackenPersonality = " + brackenPersonality.ToString() +
                "\nMoon = " + currentMoon +
                "\nInterior = " + currentInterior +
                "\nisDead = " + brackenEnemy.isEnemyDead +
                "\n\nisOutside = " + brackenEnemy.isOutside +
                "\nisInsidePlayerShip = " + brackenEnemy.isInsidePlayerShip +
                "\n\nAngerLevel = " + brackenEnemy.angerMeter.ToString() +
                "\nIsInAngerMode = " +brackenEnemy.isInAngerMode.ToString() +
                "\nTargetPlayer = " + targetPlayer +
                "\n===== BrackenStatusReport() End =======";
                if (debugMsg != lastDebugMsg)
                {
                    lastDebugMsg = debugMsg;
                    Plugin.mls.LogInfo(debugMsg);
                }
                if (brackenEnemy.isEnemyDead)
                {
                    Plugin.mls.LogInfo("Bracken " + brackenPersonality.ToString() + "(" + brackenEnemy.GetInstanceID() + ") is now dead, status reporting will cease for this bracken");
                    stopStatusReporting = true;
                }
            }
        }
    }
}
