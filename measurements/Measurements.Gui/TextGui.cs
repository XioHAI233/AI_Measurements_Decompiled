using BepInEx;
using KKAPI.Maker;
using KKAPI.Maker.UI;

namespace Measurements.Gui;

internal abstract class TextGui : IGui<MakerText>
{
	protected const int GENDER_FEMALE = 1;

	protected const int GENDER_MALE = 0;

	protected static readonly float FreedomRatio = 0.39370078f;

	private MakerText _control;

	private string _initialText;

	public abstract void Initialize(MakerCategory category, MeasurementsPlugin plugin, RegisterSubCategoriesEvent e);

	protected abstract void UpdateInternal(MeasurementsData data, MeasurementsController controller);

	internal void Update(MeasurementsData data, MeasurementsController controller)
	{
		SetVisibility();
		if (IsVisible())
		{
			UpdateInternal(data, controller);
		}
	}

	protected void InitializeInternal(string initialText, MakerCategory category, MeasurementsPlugin plugin, RegisterSubCategoriesEvent e)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		_initialText = initialText;
		_control = new MakerText(initialText + ":", category, (BaseUnityPlugin)(object)plugin);
		((RegisterCustomControlsEvent)e).AddControl<MakerText>(_control);
	}

	protected void SetText(string text)
	{
		if (_control != null)
		{
			_control.Text = _initialText + ": " + text;
		}
	}

	protected abstract bool ShouldBeVisible();

	private bool IsVisible()
	{
		return ((BaseGuiEntry)_control).Visible.Value;
	}

	private void SetVisibility()
	{
		bool flag = ShouldBeVisible();
		bool flag2 = IsVisible();
		if ((flag && !flag2) || (!flag && flag2))
		{
			((BaseGuiEntry)_control).Visible.OnNext(!flag2);
		}
	}
}
