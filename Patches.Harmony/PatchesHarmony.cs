using Elements.Core;
using FrooxEngine;
using FrooxEngine.UIX;
using HarmonyLib;
using ProtofluxFreezerRML.Mod.Common;
using System;
using UIXDialogBuilder;

namespace ProtofluxFreezer
{
    internal class PatchesHarmony
    {
        private static IProtofluxFreezer ModInstance;

        internal static void Apply(IProtofluxFreezer instance)
        {
            ModInstance = instance;
            Harmony harmony = new Harmony("com.github.mpmxyz.ProtofluxFreezer");
            harmony.PatchAll();
        }

#pragma warning disable IDE0051 // Remove unused private members
        [HarmonyPatch(typeof(FrooxEngine.ProtoFlux.ProtoFluxTool), "GenerateMenuItems")]
        class ClassName_MethodName_Patch
        {
            [HarmonyPostfix]
            static void Postfix(InteractionHandler tool, ContextMenu menu)
            {
                if (!ModInstance.Enabled)
                {
                    return;
                }
                var item = menu.AddItem("Freeze Protoflux", (Uri)null, colorX.Black);
                item.Button.LocalPressed += (b, e) => new DialogBuilder<FreezeDialogState>()
                    .BuildWindow("Freeze Configuration", menu.World, new FreezeDialogState());
            }
        }
#pragma warning restore IDE0051 // Remove unused private members
    }
}
