using System;
using System.Xml.Serialization;
using ModLib.Definitions;
using ModLib.Definitions.Attributes;

namespace OphiasPerkFixes
{
	// Token: 0x02000007 RID: 7
	public class ST_opsSettings : SettingsBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000028B4 File Offset: 0x00000AB4
		public override string ModName { get; } = "Ophias Perk Fixes";

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000028BC File Offset: 0x00000ABC
		public override string ModuleFolderName { get; } = "OphiasPerkFixes";

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000028C4 File Offset: 0x00000AC4
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000028CC File Offset: 0x00000ACC
		[XmlElement]
		public override string ID { get; set; } = "OphiasPerkFixesSettings";

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000028D5 File Offset: 0x00000AD5
		public static ST_opsSettings Instance
		{
			get
			{
				return (ST_opsSettings)SettingsDatabase.GetSettings<ST_opsSettings>();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000028E1 File Offset: 0x00000AE1
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000028E9 File Offset: 0x00000AE9
		[XmlElement]
		[SettingProperty("Deployment Before Battle", 0, 2, "0:Disabled 1:Enabled with Perk 2:Always Enabled")]
		public int deploymentConfig { get; set; } = 1;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000028F2 File Offset: 0x00000AF2
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000028FA File Offset: 0x00000AFA
		[XmlElement]
		[SettingProperty("Front Line Distance Base", 10f, 200f, "Front Line Boundary Distance to Player at 0 Tactics")]
		public float deployFrontLineBase { get; set; } = 50f;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002903 File Offset: 0x00000B03
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000290B File Offset: 0x00000B0B
		[XmlElement]
		[SettingProperty("Front Line Distance Factor", 0f, 5f, "Each skill value of Tactics add this value to final Deployment Boundary")]
		public float deployFrontLineFactor { get; set; } = 0.25f;

		// Token: 0x04000003 RID: 3
		public const string InstanceId = "OphiasPerkFixesSettings";
	}
}
