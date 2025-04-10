using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KKAPI;
using KKAPI.Chara;
using Measurements.Band;
using Measurements.Bust;
using Measurements.Dick;
using Measurements.Gui;
using Measurements.Height;
using Measurements.Hips;
using Measurements.Waist;
using UnityEngine;

namespace Measurements;

public class MeasurementsController : CharaCustomFunctionController
{
	private readonly FindAssist _boneSearcher = new FindAssist();

	private bool _initialized;

	private bool _waiting;

	private static readonly List<CalculatorBase> s_calculators = new List<CalculatorBase>
	{
		new Measurements.Height.Calculator(),
		new Measurements.Bust.Calculator(),
		new Measurements.Band.Calculator(),
		new Measurements.Waist.Calculator(),
		new Measurements.Hips.Calculator(),
		new Measurements.Dick.Calculator()
	};

	private static Dictionary<string, Vector3> s_prevVerts;

	public bool UseMetricUnits { get; internal set; }

	public int Region { get; internal set; }

	protected override void OnCardBeingSaved(GameMode currentGameMode)
	{
	}

	private void Initialize()
	{
		if (!_initialized)
		{
			_boneSearcher.Initialize(((Component)((CharaCustomFunctionController)this).ChaControl).transform);
			_initialized = true;
		}
	}

	protected override void OnReload(GameMode currentGameMode)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		UpdateTexts();
		((CharaCustomFunctionController)this).OnReload(currentGameMode);
	}

	internal void UpdateTexts()
	{
		if (!_initialized)
		{
			Initialize();
		}
		if (!_waiting)
		{
			_waiting = true;
			((MonoBehaviour)this).StartCoroutine(CoUpdateTexts());
		}
	}

	private IEnumerator CoUpdateTexts()
	{
		while (IsDelayRequired())
		{
			yield return (object)new WaitForSeconds(0.1f);
		}
		MeasurementsData data = default(MeasurementsData);
		if (MeasurementsPlugin.Configuration.DebugValues.Value)
		{
			MeasurementsPlugin.Logger.LogInfo((object)">>> Measurement Values <<<");
		}
		foreach (CalculatorBase s_calculator in s_calculators)
		{
			s_calculator.SetValue(ref data, _boneSearcher);
		}
		foreach (TextGui measurementGui in MeasurementsPlugin.UI.MeasurementGuis)
		{
			measurementGui.Update(data, this);
		}
		yield return (object)new WaitForSeconds(0.1f);
		_waiting = false;
	}

	private bool IsDelayRequired()
	{
		Dictionary<string, Vector3> currentVerts = s_calculators.SelectMany((CalculatorBase calculator) => calculator.GetBoneVertices(_boneSearcher)).ToDictionary((KeyValuePair<string, Vector3> bone) => bone.Key, (KeyValuePair<string, Vector3> bone) => bone.Value);
		bool num = currentVerts.Keys.Any((string key) => Vector3.Distance(s_prevVerts?[key] ?? Vector3.zero, currentVerts[key]) > 0.001f);
		if (num)
		{
			s_prevVerts = currentVerts;
		}
		return num;
	}
}
