using BepInEx;
using KKAPI.Maker;
using KKAPI.Maker.UI;

namespace Measurements.Gui;

internal class MetricUnitsGui : ConfigGui<MakerToggle, bool>
{
	public override void Initialize(MakerCategory category, MeasurementsPlugin plugin, RegisterSubCategoriesEvent e)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		MakerToggle control = new MakerToggle(category, "Use metric units", MeasurementsPlugin.Configuration.UseMetricUnits.Value, (BaseUnityPlugin)(object)plugin);
		InitializeInternal(control, e, (MeasurementsController ctrlr) => ctrlr.UseMetricUnits, delegate(MeasurementsController ctrlr, bool value)
		{
			ctrlr.UseMetricUnits = value;
		});
	}
}
