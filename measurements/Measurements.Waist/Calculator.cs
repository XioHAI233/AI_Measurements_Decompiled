using System;
using System.Collections.Generic;
using UnityEngine;

namespace Measurements.Waist;

internal class Calculator : CalculatorBase
{
	protected override string[] BoneNames => Enum.GetNames(typeof(Bones));

	protected override float GetValue(Dictionary<string, Vector3> boneVerts)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		Vector3 pointA = boneVerts["N_Waist_L"];
		Vector3 pointB = boneVerts["N_Waist_R"];
		Vector3 pointA2 = boneVerts["N_Waist_f"];
		Vector3 pointB2 = boneVerts["N_Waist_b"];
		float axis = GetDistanceInCm(pointA2, pointB2) / 2f;
		float axis2 = GetDistanceInCm(pointA, pointB) / 2f;
		return GetEllipseCircumference(axis, axis2);
	}

	protected override void SetValueInternal(ref MeasurementsData data, float value)
	{
		data.Waist = value;
		DebugLog("Waist", data.Waist);
	}
}
