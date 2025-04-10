using System;
using System.Collections.Generic;
using UnityEngine;

namespace Measurements.Bust;

internal class Calculator : CalculatorBase
{
	private static readonly float s_rotationForSideBoob = (float)Math.PI / 2f;

	protected override string[] BoneNames => Enum.GetNames(typeof(Bones));

	protected override float GetValue(Dictionary<string, Vector3> boneVerts)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 leftSideBoob = GetLeftSideBoob(boneVerts["cf_J_Mune02_L"], boneVerts["cf_J_Mune_Nip01_L"], (boneVerts["N_Back_L"].y + boneVerts["cf_J_Mune02_L"].y) / 2f);
		Vector3 rightSideBoob = GetRightSideBoob(boneVerts["cf_J_Mune02_R"], boneVerts["cf_J_Mune_Nip01_R"], (boneVerts["N_Back_R"].y + boneVerts["cf_J_Mune02_R"].y) / 2f);
		TitData titData = default(TitData);
		titData.Nipple = boneVerts["cf_J_Mune_Nip01_R"];
		titData.SideBoob = rightSideBoob;
		titData.Lat = boneVerts["N_Back_R"];
		TitData titData2 = titData;
		titData = default(TitData);
		titData.Nipple = boneVerts["cf_J_Mune_Nip01_L"];
		titData.SideBoob = leftSideBoob;
		titData.Lat = boneVerts["N_Back_L"];
		TitData titData3 = titData;
		return GetDistanceInCm(titData2.Nipple, titData2.SideBoob) + GetDistanceInCm(titData2.SideBoob, titData2.Lat) + GetDistanceInCm(titData2.Lat, titData3.Lat) + GetDistanceInCm(titData3.Lat, titData3.SideBoob) + GetDistanceInCm(titData3.SideBoob, titData3.Nipple) + GetDistanceInCm(titData3.Nipple, titData2.Nipple);
	}

	private Vector3 GetRightSideBoob(Vector3 titCenter, Vector3 nipple, float targetY)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return GetSideBoob(titCenter, nipple, targetY, s_rotationForSideBoob);
	}

	private Vector3 GetLeftSideBoob(Vector3 titCenter, Vector3 nipple, float targetY)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return GetSideBoob(titCenter, nipple, targetY, -1f * s_rotationForSideBoob);
	}

	private Vector3 GetSideBoob(Vector3 titCenter, Vector3 nipple, float targetY, double thetaXZAdjustment)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		double num = Math.Sqrt(Math.Pow(Vector3.Distance(titCenter, nipple), 2.0) - Math.Pow(targetY - titCenter.y, 2.0));
		double num2 = Math.Atan((nipple.x - titCenter.x) / (nipple.z - titCenter.z)) + thetaXZAdjustment;
		return new Vector3(titCenter.x + (float)(num * Math.Sin(num2)), targetY, titCenter.z + (float)(num * Math.Cos(num2)));
	}

	protected override void SetValueInternal(ref MeasurementsData data, float value)
	{
		data.Bust = value;
		DebugLog("Bust", data.Bust);
	}
}
