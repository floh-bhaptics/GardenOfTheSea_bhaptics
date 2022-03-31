using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MelonLoader;
using HarmonyLib;
using MyBhapticsTactsuit;

namespace GardenOfTheSea_bhaptics
{
    public class GardenOfTheSea_bhaptics : MelonMod
    {
        public static TactsuitVR tactsuitVr;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            tactsuitVr = new TactsuitVR();
            tactsuitVr.PlaybackHaptics("HeartBeat");
        }
        
        [HarmonyPatch(typeof(Tool_Hoe), "OnTriggerEnter", new Type[] { typeof(UnityEngine.Collider) })]
        public class bhaptics_ToolDig
        {
            [HarmonyPostfix]
            public static void Postfix(Tool_Hoe __instance)
            {
                if (__instance == null) return;
                if (__instance.GetHand() == null) return;
                tactsuitVr.Handle("Dig", (__instance.GetHand().IsRightHand));
            }
        }

        [HarmonyPatch(typeof(Tool_Axe), "OnTriggerEnter", new Type[] { typeof(UnityEngine.Collider) })]
        public class bhaptics_ToolAxe
        {
            [HarmonyPostfix]
            public static void Postfix(Tool_Axe __instance)
            {
                if (__instance == null) return;
                if (__instance.GetHand() == null) return;
                tactsuitVr.Handle("Axe", (__instance.GetHand().IsRightHand));
            }
        }

        [HarmonyPatch(typeof(Tool_FishingRod), "StartFishBite", new Type[] {  })]
        public class bhaptics_ToolFish
        {
            [HarmonyPostfix]
            public static void Postfix(Tool_FishingRod __instance)
            {
                if (__instance == null) return;
                if (__instance.GetHand() == null) return;
                tactsuitVr.Handle("FishBite", (__instance.GetHand().IsRightHand));
            }
        }

        [HarmonyPatch(typeof(PlayerInventory), "Add", new Type[] { typeof(Stashable) })]
        public class bhaptics_InventoryAdd
        {
            [HarmonyPostfix]
            public static void Postfix(PlayerInventory __instance)
            {
                if (__instance == null) return;
                if (__instance.hand == null) return;
                tactsuitVr.Handle("Inventory", (__instance.hand.IsRightHand));
            }
        }

        [HarmonyPatch(typeof(PlayerInventory), "Remove", new Type[] { typeof(Stashable) })]
        public class bhaptics_InventoryRemove
        {
            [HarmonyPostfix]
            public static void Postfix(PlayerInventory __instance)
            {
                if (__instance == null) return;
                if (__instance.hand == null) return;
                tactsuitVr.Handle("Inventory", (__instance.hand.IsRightHand));
            }
        }

        [HarmonyPatch(typeof(LocationTracker), "OnTeleport", new Type[] { typeof(PlayerTeleportEvent) })]
        public class bhaptics_TeleportTo
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlaybackHaptics("Teleport");
            }
        }

        [HarmonyPatch(typeof(CraftingMachine), "LeverPulled", new Type[] { typeof(Rotatable2) })]
        public class bhaptics_SuccessfulCraft
        {
            [HarmonyPostfix]
            public static void Postfix(CraftingMachine __instance)
            {
                tactsuitVr.PlaybackHaptics("Crafting");
            }
        }

        [HarmonyPatch(typeof(EatingBehavior), "Eat", new Type[] { typeof(Grabbable) })]
        public class bhaptics_Eat
        {
            [HarmonyPostfix]
            public static void Postfix()
            {
                tactsuitVr.PlaybackHaptics("Eating");
            }
        }

        [HarmonyPatch(typeof(AnimalTameMeter), "PlayLevelUpEffect", new Type[] {  })]
        public class bhaptics_AnimalLevelUp
        {
            [HarmonyPostfix]
            public static void Postfix(CraftingMachine __instance)
            {
                tactsuitVr.PlaybackHaptics("AnimalLove");
            }
        }

        [HarmonyPatch(typeof(Boat), "UpdateSpeed", new Type[] {  })]
        public class bhaptics_BoatSpeed
        {
            [HarmonyPostfix]
            public static void Postfix(Boat __instance)
            {
                if (__instance.boatIsStopped) { tactsuitVr.StopBlender(); return; }
                if (__instance.currentState == Boat.BoatMovementState.StandingStill) { tactsuitVr.StopBlender(); return; }
                if (__instance.currentSpeed == 0.0f) { tactsuitVr.StopBlender(); return; }
                if (__instance.currentSpeed > 0.0f) tactsuitVr.StartBlender();
            }
        }

    }
}
