using System;
using System.Xml.Serialization;
using ModLib.Definitions;
using ModLib.Definitions.Attributes;

namespace OphiasPerkFixes
{
	public class ST_opsSettings : SettingsBase
	{
		public override string ModName { get; } = "Ophias Perk Fixes";

		public override string ModuleFolderName { get; } = "OphiasPerkFixes";

		[XmlElement]
		public override string ID { get; set; } = "OphiasPerkFixesSettings";

		public static ST_opsSettings Instance
		{
			get
			{
				return (ST_opsSettings)SettingsDatabase.GetSettings<ST_opsSettings>();
			}
		}

		[XmlElement]
		[SettingProperty("Deployment Before Battle", 0, 2, "0:Disabled 1:Enabled with Perk 2:Always Enabled")]
		public int deploymentConfig { get; set; } = 1;

		[XmlElement]
		[SettingProperty("Front Line Distance Base", 10f, 200f, "Front Line Boundary Distance to Player at 0 Tactics")]
		public float deployFrontLineBase { get; set; } = 50f;

		[XmlElement]
		[SettingProperty("Front Line Distance Factor", 0f, 5f, "Each skill value of Tactics add this value to final Deployment Boundary")]
		public float deployFrontLineFactor { get; set; } = 0.25f;

		public const string InstanceId = "OphiasPerkFixesSettings";
	}
}
