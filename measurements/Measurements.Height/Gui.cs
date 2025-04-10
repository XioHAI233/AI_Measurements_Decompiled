using KKAPI.Maker;
using Measurements.Gui;

namespace Measurements.Height;

internal class Gui : TextGui
{
	public override void Initialize(MakerCategory category, MeasurementsPlugin plugin, RegisterSubCategoriesEvent e)
	{
		InitializeInternal("Height", category, plugin, e);
	}

	protected override void UpdateInternal(MeasurementsData data, MeasurementsController controller)
	{
		if (controller.UseMetricUnits)
		{
			SetText($"{data.Height:N1} cm");
			return;
		}
		float num = data.Height * TextGui.FreedomRatio;
		int num2 = (int)(num / 12f);
		float num3 = num % 12f;
		SetText($"{num2}' {num3:N0}\" ({num:N1} in)");
	}

	protected override bool ShouldBeVisible()
	{
		return true;
	}
}
