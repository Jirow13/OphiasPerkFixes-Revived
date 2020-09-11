using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;

namespace OphiasPerkFixes
{
	// Token: 0x02000003 RID: 3
	[HarmonyPatch(typeof(MissionState), "HandleOpenNew")]
	internal static class internal_HP_xyzDepolyment
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002590 File Offset: 0x00000790
		private static void Postfix(MissionState __instance, ref Mission __result)
		{
			PartyBase mainParty = PartyBase.MainParty;
			bool flag;
			if (mainParty == null)
			{
				flag = false;
			}
			else
			{
				Hero leaderHero = mainParty.LeaderHero;
				bool? flag2 = (leaderHero != null) ? new bool?(leaderHero.GetPerkValue(DefaultPerks.Tactics.OneStepAhead)) : null;
				bool flag3 = true;
				flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
			}
			bool flag4 = flag;
			if (__instance.MissionName == "Battle" && ((flag4 && ST_opsSettings.Instance.deploymentConfig == 1) || ST_opsSettings.Instance.deploymentConfig == 2))
			{
				bool isPlayerAttacker = MapEvent.PlayerMapEvent.AttackerSide.LeaderParty == PartyBase.MainParty;
				__result.AddMissionBehaviour(new DH_opsDepolyment(isPlayerAttacker));
				__result.AddMissionBehaviour(new MissionBoundaryMarker(new FlagFactory("swallowtail_banner"), 2f));
			}
		}
	}
}
