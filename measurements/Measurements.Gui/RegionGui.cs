using System;
using BepInEx;
using KKAPI.Maker;
using KKAPI.Maker.UI;

namespace Measurements.Gui;

internal class RegionGui : ConfigGui<MakerDropdown, int>
{
	public override void Initialize(MakerCategory category, MeasurementsPlugin plugin, RegisterSubCategoriesEvent e)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		int num = Array.IndexOf(MeasurementsPlugin.Regions, MeasurementsPlugin.Configuration.Region.Value);
		MakerDropdown control = new MakerDropdown("Region", MeasurementsPlugin.Regions, category, num, (BaseUnityPlugin)(object)plugin);
		InitializeInternal(control, e, (MeasurementsController ctrlr) => ctrlr.Region, delegate(MeasurementsController ctrlr, int value)
		{
			ctrlr.Region = value;
		});
	}
}
