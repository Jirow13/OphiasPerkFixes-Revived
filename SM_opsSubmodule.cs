using System;
using System.Windows.Forms;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace OphiasPerkFixes
{
	public class SM_opsSubModule : MBSubModuleBase
	{
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

		protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
			base.OnGameStart(game, gameStarterObject);
			if ( (game != null) && game.GameType is Campaign)
			{
				CampaignGameStarter campaignGameStarter = gameStarterObject as CampaignGameStarter;
			}
		}

		public override void OnMissionBehaviourInitialize(Mission mission)
		{
		}

		private bool initialized;
	}
}
