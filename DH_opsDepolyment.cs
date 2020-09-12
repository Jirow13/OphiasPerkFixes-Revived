using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Missions;
using TaleWorlds.MountAndBlade.View.Screen;

namespace OphiasPerkFixes
{
	public class DH_opsDepolyment : DeploymentHandler
	{
		public DH_opsDepolyment(bool isPlayerAttacker) : base(isPlayerAttacker)
		{
		}

		public override void EarlyStart()
		{
			base.Mission.AllowAiTicking = false;
		}

		public override void AfterStart()
		{
			base.AfterStart();
		}

		public override void OnFormationUnitsSpawned(Team team)
		{
			base.OnFormationUnitsSpawned(team);
            using (IEnumerator<Formation> enumerator = team.FormationsIncludingSpecial.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.ApplyActionOnEachUnit(delegate (Agent agent)
					{
						if (!agent.IsAIControlled)
						{
							return;
						}
						agent.SetIsAIPaused(true);
					});
				}
			}
			if (team.IsPlayerTeam)
			{
				/* To Do: Figure out how to disable combat while formations are set, as the method below is not available.
				base.Mission.MainAgent.SetIsCombatActionsDisabled(true); 
				*/ 
				base.Mission.MainAgent.Controller = Agent.ControllerType.AI;
			}
			this.formationSpawned++;
			if (this.formationSpawned >= base.Mission.Teams.Count)
			{
				base.Mission.IsTeleportingAgents = true;
				base.Mission.CameraIsFirstPerson = false;
				this.SetDeploymentBoundary();
				Vec3 position = base.Mission.MainAgent.Position;
				float lookDirectionAsAngle = base.Mission.MainAgent.LookDirectionAsAngle;
				Vec2 vec = new Vec2(0f, -30f);
				vec.RotateCCW(lookDirectionAsAngle);
				MatrixFrame frame = new MatrixFrame(0f, 1f, 0f, 1f, 0f, 0f, -MathF.Sin(lookDirectionAsAngle + 3.14159274f), MathF.Cos(lookDirectionAsAngle + 3.14159274f), -MathF.Cos(1.04719758f), position.X + vec.X, position.Y + vec.Y, position.Z + 40f);
				frame.Fill();
				(ScreenManager.TopScreen as MissionScreen).UpdateFreeCamera(frame);
				MissionMode mode = base.Mission.Mode;
				InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=opsdeploy-start}Deployment Phase. Press E to Start Battle.", null).ToString(), new Color(0f, 0.8f, 0f, 1f)));
			}
			base.Mission.AllowAiTicking = false;
		}

		public override void FinishDeployment()
		{
			base.FinishDeployment();
			this.RemoveAllBoundaries();
			Mission mission = base.Mission;
			/* To Do: Re-Enable 'main agent' after deployment is done.
			mission.MainAgent.SetIsCombatActionsDisabled(false);
			*/
			mission.MainAgent.Controller = Agent.ControllerType.Player;
			mission.IsTeleportingAgents = false;
			foreach (Agent agent in mission.Agents)
			{
				if (agent.IsAIControlled)
				{
					agent.SetIsAIPaused(false);
				}
			}
			mission.AllowAiTicking = true;
			mission.RemoveMissionBehaviour(this);
			MissionBoundaryMarker missionBehaviour = mission.GetMissionBehaviour<MissionBoundaryMarker>();
			if (missionBehaviour != null)
			{
				mission.RemoveMissionBehaviour(missionBehaviour);
			}
			InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=opsdeploy-end}Battle Start!", null).ToString(), new Color(0f, 0.8f, 0f, 1f)));
		}

		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (Input.IsKeyPressed(InputKey.E))
			{
				this.FinishDeployment();
			}
		}

		public void SetDeploymentBoundary()
		{
			Agent mainAgent = base.Mission.MainAgent;
			if (mainAgent == null)
			{
				return;
			}
			if (this.boundaryAdded())
			{
				return;
			}
			Vec2 asVec = mainAgent.Position.ToWorldPosition().AsVec2;
			float lookDirectionAsAngle = mainAgent.LookDirectionAsAngle;
			List<Vec2> list = new List<Vec2>();
			float num = 150f;
			float b = ST_opsSettings.Instance.deployFrontLineBase + (float)mainAgent.Character.GetSkillValue(DefaultSkills.Tactics) * ST_opsSettings.Instance.deployFrontLineFactor;
			float num2 = 100f;
			Vec2 v = new Vec2(-num, b);
			v.RotateCCW(lookDirectionAsAngle);
			Vec2 v2 = new Vec2(-num, -num2);
			v2.RotateCCW(lookDirectionAsAngle);
			Vec2 v3 = new Vec2(num, -num2);
			v3.RotateCCW(lookDirectionAsAngle);
			Vec2 v4 = new Vec2(num, b);
			v4.RotateCCW(lookDirectionAsAngle);
			list.Add(asVec + v);
			list.Add(asVec + v2);
			list.Add(asVec + v3);
			list.Add(asVec + v4);
			base.Mission.Boundaries.Add("ops_boundary", list, true);
		}

		public new void RemoveAllBoundaries()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, ICollection<Vec2>> keyValuePair in base.Mission.Boundaries)
			{
				list.Add(keyValuePair.Key);
			}
			foreach (string text in list)
			{
				if (text.StartsWith("ops_"))
				{
					base.Mission.Boundaries.Remove(text);
				}
			}
		}

		private bool boundaryAdded()
		{
			bool result = false;
			foreach (KeyValuePair<string, ICollection<Vec2>> keyValuePair in base.Mission.Boundaries)
			{
				if (keyValuePair.Key.StartsWith("ops_"))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private int formationSpawned;
	}
}
