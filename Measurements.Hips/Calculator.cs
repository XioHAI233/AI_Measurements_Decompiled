using System;
using System.Collections.Generic;
using UnityEngine;

namespace Measurements.Hips;

internal class Calculator : CalculatorBase
{
	protected override string[] BoneNames => Enum.GetNames(typeof(Bones));

	protected override float GetValue(Dictionary<string, Vector3> boneVerts)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		HipData hipData = default(HipData);
		hipData.Front = boneVerts["cf_J_Legsk_07_00"];
		hipData.Side = boneVerts["cf_J_Legsk_06_00"];
		hipData.Ass = boneVerts["cf_J_Legsk_05_00"];
		HipData hipData2 = hipData;
		hipData = default(HipData);
		hipData.Front = boneVerts["cf_J_Legsk_01_00"];
		hipData.Side = boneVerts["cf_J_Legsk_02_00"];
		hipData.Ass = boneVerts["cf_J_Legsk_03_00"];
		HipData hipData3 = hipData;
		return GetDistanceInCm(hipData2.Front, hipData2.Side) + GetDistanceInCm(hipData2.Side, hipData2.Ass) + GetDistanceInCm(hipData2.Ass, hipData3.Ass) + GetDistanceInCm(hipData3.Ass, hipData3.Side) + GetDistanceInCm(hipData3.Side, hipData3.Front) + GetDistanceInCm(hipData3.Front, hipData2.Front);
	}

	protected override void SetValueInternal(ref MeasurementsData data, float value)
	{
		data.Hips = value;
		DebugLog("Hips", data.Hips);
	}
}
