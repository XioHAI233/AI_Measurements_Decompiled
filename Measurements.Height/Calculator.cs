using System;
using System.Collections.Generic;
using UnityEngine;

namespace Measurements.Height;

internal class Calculator : CalculatorBase
{
	protected override string[] BoneNames => Enum.GetNames(typeof(Bones));

	protected override float GetValue(Dictionary<string, Vector3> boneVerts)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Vector3 pointA = boneVerts["N_Head_top"];
		return GetDistanceInCm(pointA, new Vector3(0f, 0f, 0f));
	}

	protected override void SetValueInternal(ref MeasurementsData data, float value)
	{
		data.Height = value;
		DebugLog("Height", data.Height);
	}
}
