using System;
using System.Collections.Generic;
using UnityEngine;

namespace Measurements.Dick;

internal class Calculator : CalculatorBase
{
	protected override string[] BoneNames => Enum.GetNames(typeof(Bones));

	protected override float GetValue(Dictionary<string, Vector3> boneVerts)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Vector3 pointA = boneVerts["cm_J_dan100_00"];
		Vector3 pointB = boneVerts["cm_J_dan109_00"];
		return GetDistanceInCm(pointA, pointB);
	}

	protected override void SetValueInternal(ref MeasurementsData data, float value)
	{
		data.Dick = value;
		DebugLog("Dick", data.Dick);
	}
}
