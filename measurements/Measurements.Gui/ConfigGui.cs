using System;
using System.Collections.Generic;
using KKAPI.Chara;
using KKAPI.Maker;
using KKAPI.Maker.UI;

namespace Measurements.Gui;

internal abstract class ConfigGui<TControl, TValue> : IGui<BaseEditableGuiEntry<TValue>> where TControl : BaseEditableGuiEntry<TValue>
{
	public abstract void Initialize(MakerCategory category, MeasurementsPlugin plugin, RegisterSubCategoriesEvent e);

	protected void InitializeInternal(TControl control, RegisterSubCategoriesEvent e, Func<MeasurementsController, TValue> getValue, Action<MeasurementsController, TValue> setValue)
	{
		((RegisterCustomControlsEvent)e).AddControl<TControl>(control);
		CharacterExtensions.BindToFunctionController<MeasurementsController, TValue>((BaseEditableGuiEntry<TValue>)control, (GetValueForInterface<MeasurementsController, TValue>)((MeasurementsController ctrlr) => getValue(ctrlr)), (SetValueToController<MeasurementsController, TValue>)delegate(MeasurementsController ctrlr, TValue value)
		{
			TValue x = getValue(ctrlr);
			if (!EqualityComparer<TValue>.Default.Equals(x, value))
			{
				setValue(ctrlr, value);
				OnMakerSettingsChanged(ctrlr);
			}
		});
	}

	public static void OnMakerSettingsChanged(MeasurementsController controller)
	{
		controller.UpdateTexts();
	}
}
