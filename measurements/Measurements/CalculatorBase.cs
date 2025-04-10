using System;
using System.Collections.Generic;
using UnityEngine;

namespace Measurements;

internal abstract class CalculatorBase
{
	protected abstract string[] BoneNames { get; }

	protected Dictionary<string, Func<FindAssist, Vector3>> BoneExceptions { get; } = new Dictionary<string, Func<FindAssist, Vector3>>();

	protected abstract float GetValue(Dictionary<string, Vector3> boneVerts);

	protected abstract void SetValueInternal(ref MeasurementsData data, float value);

	public void SetValue(ref MeasurementsData data, FindAssist boneSearcher)
	{
		Dictionary<string, Vector3> boneVertices = GetBoneVertices(boneSearcher);
		float value = GetValue(boneVertices);
		SetValueInternal(ref data, value);
	}

	internal Dictionary<string, Vector3> GetBoneVertices(FindAssist boneSearcher)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		Dictionary<string, Vector3> dictionary = new Dictionary<string, Vector3>();
		string[] boneNames = BoneNames;
		foreach (string text in boneNames)
		{
			if (BoneExceptions.ContainsKey(text))
			{
				dictionary[text] = BoneExceptions[text](boneSearcher);
				continue;
			}
			GameObject objectFromName = boneSearcher.GetObjectFromName(text);
			if ((Object)(object)objectFromName != (Object)null)
			{
				dictionary[text] = objectFromName.transform.position;
			}
		}
		return dictionary;
	}

	protected float GetDistanceInCm(Vector3 pointA, Vector3 pointB)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return Vector3.Distance(pointA, pointB) * 10.5f;
	}

	protected float GetEllipseCircumference(float axis1, float axis2)
	{
		return (float)(Math.PI * 2.0 * Math.Sqrt((Math.Pow(axis1, 2.0) + Math.Pow(axis2, 2.0)) / 2.0));
	}

	protected void DebugLog(string name, float value)
	{
		if (MeasurementsPlugin.Configuration.DebugValues.Value)
		{
			MeasurementsPlugin.Logger.LogInfo((object)$"{name} = {value}");
		}
	}
}
