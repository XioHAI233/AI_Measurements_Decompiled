using KKAPI.Maker;
using Measurements.Gui;

namespace Measurements.Dick;

internal class Gui : TextGui
{
	public override void Initialize(MakerCategory category, MeasurementsPlugin plugin, RegisterSubCategoriesEvent e)
	{
		string initialText = ((MakerAPI.GetMakerSex() == 0) ? "Dick" : "Dick (futa)");
		InitializeInternal(initialText, category, plugin, e);
	}

	protected override void UpdateInternal(MeasurementsData data, MeasurementsController controller)
	{
		if (controller.UseMetricUnits)
		{
			SetText($"{data.Dick:N1} cm");
		}
		else
		{
			SetText($"{data.Dick * TextGui.FreedomRatio:N1}\"");
		}
	}

	protected override bool ShouldBeVisible()
	{
		bool num = MakerAPI.GetMakerSex() == 0;
		bool futanari = MakerAPI.GetCharacterControl().chaFile.parameter.futanari;
		return num || futanari;
	}
}
