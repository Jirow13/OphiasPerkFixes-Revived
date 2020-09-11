using System;
using System.Windows.Forms;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace OphiasPerkFixes
{
	// Token: 0x02000006 RID: 6
	public class SM_opsSubModule : MBSubModuleBase
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002830 File Offset: 0x00000A30
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();
			if (!this.initialized)
			{
				try
				{
					new Harmony("mod.ophias.perkfixes").PatchAll();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error Initialising OphiasPerkFixes:\n\n" + ex.ToString());
				}
				this.initialized = true;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000288C File Offset: 0x00000A8C
		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			base.OnGameStart(game, gameStarterObject);
			if (game.GameType is Campaign)
			{
				CampaignGameStarter campaignGameStarter = gameStarterObject as CampaignGameStarter;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000028AA File Offset: 0x00000AAA
		public override void OnMissionBehaviourInitialize(Mission mission)
		{
		}

		// Token: 0x04000002 RID: 2
		private bool initialized;
	}
}
