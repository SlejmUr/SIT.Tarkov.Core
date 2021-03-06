using SIT.Tarkov.Core;
using SIT.Tarkov.Core.PlayerPatches.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIT.Tarkov.Core.AI
{
    internal class IsPlayerEnemyPatch : ModulePatch
    {
        /*
         * public bool IsPlayerEnemy(GInterface66 requester)
         */
        protected override MethodBase GetTargetMethod()
        {
            return
                PatchConstants.GetMethodForType(
                PatchConstants.EftTypes.Single(x =>
                PatchConstants.GetMethodForType(x, "IsPlayerEnemy") != null), "IsPlayerEnemy");
        }

        [PatchPrefix]
        public static bool PatchPrefix()
        {
            return false;
        }

        [PatchPostfix]
        public static void PatchPostfix(ref bool __result, object player)
        {
            var p = player as EFT.Player;
            if (p != null)
            {
                if (p.AIData.IsAI)
                {
                }
            }
            var otherPlayerHealthController = HealthControllerHelpers.GetActiveHealthController(player);
            var otherPlayerIsAlive = HealthControllerHelpers.IsAlive(otherPlayerHealthController);
            __result = otherPlayerIsAlive;
        }
    }
}
