using System;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace OphiasPerkFixes
{
	public class PerkFixesHelper
	{
		public static void fixPerkBonus(DefaultPerks dpobj, string perkID, float primaryBonus, float secondaryBonus, SkillEffect.EffectIncrementType incrementType)
		{
			bool flag = false;
			FieldInfo field = typeof(DefaultPerks).GetField(perkID, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field != null)
			{
				PerkObject perkObject = field.GetValue(dpobj) as PerkObject;
				if (perkObject != null)
				{
					perkObject.Initialize(perkObject.Name.ToString(), perkObject.Description.ToString(), perkObject.Skill, (int)perkObject.RequiredSkillValue, perkObject.AlternativePerk, perkObject.PrimaryRole, primaryBonus, perkObject.SecondaryRole, secondaryBonus, incrementType, "");
					flag = true;
				}
			}
			if (!flag)
			{
				InformationManager.DisplayMessage(new InformationMessage("Patch Failed:" + perkID, new Color(0.8f, 0f, 0f, 1f)));
			}
		}
	}
}
