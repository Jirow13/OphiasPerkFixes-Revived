using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;

namespace OphiasPerkFixes
{
	// Token: 0x02000004 RID: 4
	[HarmonyPatch(typeof(DefaultPerks))]
	internal class DefaultPerksFixes
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002654 File Offset: 0x00000854
		[HarmonyPostfix]
		[HarmonyPatch("InitializePerks")]
		public static void InitializePerksPostfix(DefaultPerks __instance)
		{
			PerkFixesHelper.FixPerkBonus(__instance, "OneHandedToBeBlunt", 5f, 0.5f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "OneHandedShieldWall", -20f, 5f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "OneHandedArrowCatcher", 5f, 5f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "OneHandedWayOfTheSword", 0.5f, 0.2f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "TwoHandedTerror", 20f, 10f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "TwoHandedHope", 30f, 5f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "TwoHandedWayOfTheGreatAxe", 0.5f, 0.2f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "PolearmStandardBearer", 15f, 0f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "PolearmHorseKiller", 70f, 0f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "PolearmFootwork", 2f, 0f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "PolearmLancer", 30f, 0f, SkillEffect.EffectIncrementType.AddFactor);
			PerkFixesHelper.FixPerkBonus(__instance, "ThrowingSkullCrusher", 20f, 0f, SkillEffect.EffectIncrementType.AddFactor);
		}
	}
}
