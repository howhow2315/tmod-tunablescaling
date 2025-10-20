using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace TunableScaling.Common.Configs
{
	public class TunableScalingModConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;


		// [Header("Enemy")]
		
		// [Label("Enemy Health")]
		// [Range(1, 25)]
		// [Increment(1)]
		// [DefaultValue(1)]
		// [Slider]
		// [DrawTicks]
		// public int Enemy_Health;

		// [Label("Enemy Damage")]
		// [Range(1, 25)]
		// [Increment(1)]
		// [DefaultValue(1)]
		// [Slider]
		// [DrawTicks]
		// public int Enemy_Damage;

		// [Label("Enemy Loot")]
		// [Range(1, 5)]
		// [Increment(1)]
		// [DefaultValue(1)]
		// [Slider]
		// [DrawTicks]
		// public int Enemy_Loot;
		
		// [Label("Enemy Defense")]
		// [Range(1, 10)]
		// [Increment(1)]
		// [DefaultValue(1)]
		// [Slider]
		// [DrawTicks]
		// public int Enemy_Defense;

		// [Label("Enemy Spawn Rate")]
		// [Tooltip("Lower = Less Spawns, useful for minimizing lag without compromising loot if used with 'Enemy Loot'")]
		// [Range(0.05f, 1f)]
		// [Increment(0.05f)]
		// [DefaultValue(1f)]
		// [DrawTicks]
		// public float Enemy_SpawnRate;

		// [Label("Enemy Size")]
		// [Range(0.5f, 5f)]
		// [Increment(0.5f)]
		// [DefaultValue(1f)]
		// [DrawTicks]
		// public float Enemy_Size;


		[Header("Boss")]

		[Label("Boss Health")]
		[Range(1, 25)]
		[Increment(1)]
		[DefaultValue(1)]
		[Slider]
		[DrawTicks]
		public int Boss_Health;

		// [Label("Boss Damage")]
		// [Range(1, 25)]
		// [Increment(1)]
		// [DefaultValue(1)]
		// [Slider]
		// [DrawTicks]
		// public int Boss_Damage;

		// [Label("Boss Defense")]
		// [Range(1, 10)]
		// [Increment(1)]
		// [DefaultValue(1)]
		// [Slider]
		// [DrawTicks]
		// public int Boss_Defense;

		// [Label("Boss Loot")]
		// [Range(1, 5)]
		// [Increment(1)]
		// [DefaultValue(1)]
		// [Slider]
		// [DrawTicks]
		// public int Boss_Loot;

		[Label("Boss Size")]
		[Range(0.5f, 5f)]
		[Increment(0.5f)]
		[DefaultValue(1f)]
		[DrawTicks]
		public float Boss_Size;


		[Header("MultiplayerScaling")]
		// The goal here should be to change multiplayer scaling to work as a arbitruary factor instead of being specifically for HP

		// I'd also like to add custom Enemy multiplayer scaling, but the formulas arent listed, I'll look into this later


		// Scaling toggles

		[Label("Scale Boss Health")]
		[DefaultValue(true)]
		public bool Boss_MultiplayerScaling_Scale_Health;

		// [Label("Scale Boss Damage")]
		// [DefaultValue(false)]
		// public bool Boss_MultiplayerScaling_Scale_Damage;

		// [Label("Scale Boss Defense")]
		// [DefaultValue(false)]
		// public bool Boss_MultiplayerScaling_Scale_Defense;

		[Label("Scale Boss Size")]
		[DefaultValue(false)]
		public bool Boss_MultiplayerScaling_Scale_Size;


		// Scaling configuration

		[Label("Boss Scaling Player Offset")]
		[Tooltip("Boss scaling will act as though this many fewer players are present. Does not prevent scaling, but reduces its early impact.")]
		[DefaultValue(0)]
		public int Boss_MultiplayerScaling_PlayerOffset;

		[Label("Boss Base Factor Percent%")]
		[Range(5, 100)]
		[Increment(5)]
		[DefaultValue(35)]
		[Slider]
		[DrawTicks]
		public int Boss_MultiplayerScaling_BaseFactor_Percent;

		// Disables default factored scaling in favor of a flat additional modifier
		[Label("Boss Flat Scaling")]
		[Tooltip("The normal formula is 'f(n) = n - 39/20 + 39/20(2/3)^(n-1)', this changes it to be simply +(Base Factor)% per player.")]
		[DefaultValue(false)]
		public bool Boss_MultiplayerScaling_Flat;

		// When there are 10+ players in a server, by default the scaling formula changes to
		// (MultiplayerFactor * 2 + 8) / 3 = ModifiedMultiplayerFactor
		[Label("10+ Player Alternative")]
		[Tooltip("When there are 10+ players in a server, by (Terraria) default the scaling formula changes to (MultiplayerFactor * 2 + 8) / 3 = ModifiedMultiplayerFactor.")]
		[DefaultValue(true)]
		public bool Boss_MultiplayerScaling_10PlayerAlternative;


		[Header("Debug")]

		[Label("Debug Player Count Offset")]
		[Tooltip("playerCount + playerCountOffset.")]
		[DefaultValue(0)]
		public int Debug_PlayerCountOffset;
	}
}
