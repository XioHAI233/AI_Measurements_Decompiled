using System;
using System.Collections.Generic;
using KKAPI.Maker;
using UnityEngine;

namespace Measurements.Band;

internal class Calculator : CalculatorBase
{
	protected override string[] BoneNames => Enum.GetNames(typeof(Bones));

	public Calculator()
	{
		base.BoneExceptions["cf_J_Spine02_s_Scale"] = (FindAssist searcher) => searcher.GetObjectFromName("cf_J_Spine02_s").transform.localScale;
	}

	protected override float GetValue(Dictionary<string, Vector3> boneVerts)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = boneVerts["cf_J_Spine02_s"];
		Vector3 val2 = boneVerts["cf_J_Spine03_s"];
		Vector3 val3 = boneVerts["cf_J_Spine02_s_Scale"];
		Vector3 val4 = default(Vector3);
		((Vector3)(ref val4))._002Ector((val.x + val2.x) / 2f, val.y, val3.z * 0.8095239f);
		Vector3 val5 = default(Vector3);
		((Vector3)(ref val5))._002Ector(val4.x, (val4.y + val2.y) / 2f, val3.z * -0.85714287f);
		Vector3 val6 = default(Vector3);
		((Vector3)(ref val6))._002Ector(val3.x * 1.0952381f, (val4.y + val5.y) / 2f, (val4.y + val5.y) / 2f);
		Vector3 pointA = default(Vector3);
		((Vector3)(ref pointA))._002Ector(-1f * val6.x, val6.y, val6.z);
		float axis = GetDistanceInCm(val4, val5) / 2f;
		float axis2 = GetDistanceInCm(pointA, val6) / 2f;
		return GetEllipseCircumference(axis, axis2) * (1f + 0.2f * (MakerAPI.GetCharacterControl().fileBody.shapeValueBody[0] - 0.5f));
	}

	protected override void SetValueInternal(ref MeasurementsData data, float value)
	{
		data.Band = value;
		DebugLog("Band", data.Band);
	}
}
