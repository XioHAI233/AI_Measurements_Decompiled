using System;
using KKAPI.Maker;
using Measurements.Gui;

namespace Measurements.BraSize;

internal class Gui : TextGui
{
	public override void Initialize(MakerCategory category, MeasurementsPlugin plugin, RegisterSubCategoriesEvent e)
	{
		InitializeInternal("Bra Size", category, plugin, e);
	}

	protected override void UpdateInternal(MeasurementsData data, MeasurementsController controller)
	{
		if (controller.Region >= 0)
		{
			Region region = (Region)Enum.Parse(typeof(Region), MeasurementsPlugin.Regions[controller.Region]);
			int num = ((int)(data.Band * TextGui.FreedomRatio / 2f) + 1) * 2;
			string cupSize = CupSizeCalculator.GetCupSize(data.Bust * TextGui.FreedomRatio - (float)num, region);
			SetText(controller.UseMetricUnits ? $"{Math.Round((float)num / TextGui.FreedomRatio / 5f) * 5.0:N0}{cupSize}" : $"{num:N0}{cupSize}");
		}
	}

	protected override bool ShouldBeVisible()
	{
		return MakerAPI.GetMakerSex() == 1;
	}
}
