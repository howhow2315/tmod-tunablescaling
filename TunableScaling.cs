using Terraria;
using Terraria.ModLoader;
using System;
using System.Linq;
using TunableScaling.Common.Configs;

namespace TunableScaling {
	public class ModScaling : GlobalNPC
	{
		int GetPlayerCount() {
			var config = ModContent.GetInstance<TunableScalingModConfig>();

			int playerCount = 0;
			foreach (var player in Main.ActivePlayers)
				playerCount++;

			return playerCount + config.Debug_PlayerCountOffset;
		}

		// Default Multiplayer Factor formula (including default base factor of 35%):
		// f(n) = n - 39/20 + 39/20(2/3)^(n-1)
		//
		// To modify the base factor b = 0.35 (35%)
		// f(n) = n - 3(1-b) + 3(1-b) * (2/3)^(n-1)
		float CalculateMutliplayerFactor(int n, float b = 0.35f) {
			return n - 3 * (1 - b) + 3 * (1 - b) * MathF.Pow(2f / 3f, n - 1);
		}

		void SetBossDefaults(NPC npc) {
			var config = ModContent.GetInstance<TunableScalingModConfig>();
			int players = Math.Max(1, GetPlayerCount() - config.Boss_MultiplayerScaling_PlayerOffset);

			float modFactor = 1f;
			if (players > 1) {
				float baseFactorPercent = config.Boss_MultiplayerScaling_BaseFactor_Percent / 100;
				if (config.Boss_MultiplayerScaling_Flat)
					modFactor = 1 + (players * baseFactorPercent);
				else 
					modFactor = CalculateMutliplayerFactor(players, baseFactorPercent);
			}


			//// Health ////

			// Normalize health to base value (remove vanilla multiplayer scaling)			
			float defaultLifeMod = 1f;
			if (Main.expertMode || Main.masterMode) {
				// Reverse Expert scaling
				defaultLifeMod = CalculateMutliplayerFactor(players);

				if (players >= 10) {
					// 10+ Players: (Multiplier * 2 + 8) / 3 = Modified Multiplier
					defaultLifeMod = (defaultLifeMod * 3 - 8 ) / 2;
				}
			}
			float modLifeMax = (npc.lifeMax / defaultLifeMod) * config.Boss_Health;

			// Reapply scaling based on the configuration
			if (config.Boss_MultiplayerScaling_Scale_Health) {
				if (players >= 10 && config.Boss_MultiplayerScaling_10PlayerAlternative) {
					// 10+ Players: (Multiplier * 2 + 8) / 3 = Modified Multiplier
					modLifeMax *= (modFactor * 2 + 8) / 3;
				} else
					modLifeMax *= modFactor;
			}

			npc.lifeMax = (int)(modLifeMax);
			npc.life = npc.lifeMax;


			//// Scale|Size ////

			float modScale = npc.scale * config.Boss_Size;
			if (config.Boss_MultiplayerScaling_Scale_Size)
				modScale *= MathF.Max(1f, modFactor / 1.5f);
			npc.scale = (int)(modScale);

			// //// Defense //// etc.
			// float modDefense = npc.scale * config.Boss_Defense;
			// if (config.Boss_MultiplayerScaling_Scale_Defense)
			// npc.defense = (int)(modDefense);
		}

		public override void SetDefaults(NPC npc) {
			// Skip friendly or town NPCs
			if (npc.friendly || npc.lifeMax <= 5)
				return;

			if (npc.boss) {
				SetBossDefaults(npc);

			} else {
				// TBA
				return;

			}
		}
	}
}