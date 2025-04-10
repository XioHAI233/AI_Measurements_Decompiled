using KKAPI.Maker;
using Measurements.Gui;

namespace Measurements.Hips;

internal class Gui : TextGui
{
	public override void Initialize(MakerCategory category, MeasurementsPlugin plugin, RegisterSubCategoriesEvent e)
	{
		InitializeInternal("Hips", category, plugin, e);
	}

	protected override void UpdateInternal(MeasurementsData data, MeasurementsController controller)
	{
		SetText(controller.UseMetricUnits ? $"{data.Hips:N0} cm" : $"{data.Hips * TextGui.FreedomRatio:N0}\"");
	}

	protected override bool ShouldBeVisible()
	{
		return MakerAPI.GetMakerSex() == 1;
	}
}
