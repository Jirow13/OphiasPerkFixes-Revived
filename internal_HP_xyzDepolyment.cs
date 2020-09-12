using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;

namespace OphiasPerkFixes
{
	[HarmonyPatch(typeof(MissionState), "HandleOpenNew")]
	internal static class internal_HP_xyzDepolyment
	{
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
