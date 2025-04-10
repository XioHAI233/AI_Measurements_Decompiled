using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AIChara;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using CharaCustom;
using HarmonyLib;
using KKAPI;
using KKAPI.Chara;
using KKAPI.Maker;
using KKAPI.Maker.UI;
using KKAPI.Utilities;
using Measurements.BraSize;
using Measurements.Bust;
using Measurements.Dick;
using Measurements.Gui;
using Measurements.Height;
using Measurements.Hips;
using Measurements.Waist;
using Measurements.WaistToHips;
using UnityEngine;
using UnityEngine.Events;

namespace Measurements;

[BepInProcess("HoneySelect2")]
[BepInProcess("HoneySelect2VR")]
[BepInDependency("marco.kkapi", "1.20")]
[BepInPlugin("sakacheggs.measurements", "Chara Measurements", "1.2.0")]
[HelpURL("https://github.com/SakaraCheggit/AI_Measurements")]
public class MeasurementsPlugin : BaseUnityPlugin
{
	internal static class Configuration
	{
		public static ConfigEntry<bool> UseMetricUnits { get; private set; }

		public static ConfigEntry<string> Region { get; private set; }

		public static ConfigEntry<bool> DebugValues { get; private set; }

		public static void Initialize(ConfigFile configFile)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Expected O, but got Unknown
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Expected O, but got Unknown
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Expected O, but got Unknown
			//IL_008f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0099: Expected O, but got Unknown
			DebugValues = configFile.Bind<bool>("Debug", "Enable logging of measurements (Debug mode)", false, new ConfigDescription("Will log all measurements to the console.", (AcceptableValueBase)null, new object[1] { (object)new ConfigurationManagerAttributes
			{
				IsAdvanced = true
			} }));
			UseMetricUnits = configFile.Bind<bool>("General", "Use metric measurements", false, new ConfigDescription("True to show values in centimeters. False to show values in inches.", (AcceptableValueBase)null, Array.Empty<object>()));
			string[] names = Enum.GetNames(typeof(Region));
			Region = configFile.Bind<string>("General", "Region", names[0], new ConfigDescription("Cup sizes differ by region because bra makers are dumb. Select the one you like most.", (AcceptableValueBase)(object)new AcceptableValueList<string>(names), Array.Empty<object>()));
		}
	}

	private static class Hooks
	{
		private static readonly int[] s_measuredBodyShapes = new ChaFileDefine.BodyShapeIdx[17]
		{
			ChaFileDefine.BodyShapeIdx.Height,
			ChaFileDefine.BodyShapeIdx.BustSize,
			ChaFileDefine.BodyShapeIdx.BustY,
			ChaFileDefine.BodyShapeIdx.BustRotX,
			ChaFileDefine.BodyShapeIdx.BustX,
			ChaFileDefine.BodyShapeIdx.BustRotY,
			ChaFileDefine.BodyShapeIdx.BustSharp,
			ChaFileDefine.BodyShapeIdx.AreolaBulge,
			ChaFileDefine.BodyShapeIdx.BodyShoulderW,
			ChaFileDefine.BodyShapeIdx.BodyShoulderZ,
			ChaFileDefine.BodyShapeIdx.BodyUpW,
			ChaFileDefine.BodyShapeIdx.BodyUpZ,
			ChaFileDefine.BodyShapeIdx.WaistUpW,
			ChaFileDefine.BodyShapeIdx.WaistUpZ,
			ChaFileDefine.BodyShapeIdx.WaistLowW,
			ChaFileDefine.BodyShapeIdx.WaistLowZ,
			ChaFileDefine.BodyShapeIdx.ThighUp
		}.Select((ChaFileDefine.BodyShapeIdx idx) => (int)idx).ToArray();

		private static string[] s_bustGravitySliderNames = new string[2] { "ssBustSoftness", "ssBustWeight" };

		public static void InitHooks()
		{
			Harmony.CreateAndPatchAll(typeof(Hooks), (string)null);
		}

		[HarmonyPostfix]
		[HarmonyPatch(typeof(ChaControl), "SetShapeBodyValue")]
		public static void ChaControl_SetShapeBodyValue(ChaControl __instance, int index, float value)
		{
			if (MakerAPI.InsideAndLoaded && s_measuredBodyShapes.Any((int shapeIdx) => shapeIdx == index))
			{
				GetController(__instance).UpdateTexts();
			}
		}

		[HarmonyPostfix]
		[HarmonyPatch(typeof(ChaControl), "AnimPlay")]
		public static void ChaControl_AnimPlay(ChaControl __instance)
		{
			if (MakerAPI.InsideAndLoaded)
			{
				GetController(__instance).UpdateTexts();
			}
		}

		internal static void InitBustGravitySliders()
		{
			MeasurementsController controller = ((Component)MakerAPI.GetCharacterControl()).gameObject.GetComponent<MeasurementsController>();
			Type typeFromHandle = typeof(CvsB_ShapeBreast);
			Object obj = Object.FindObjectOfType(typeFromHandle);
			string[] array = s_bustGravitySliderNames;
			foreach (string name in array)
			{
				CustomSliderSet customSliderSet = (CustomSliderSet)typeFromHandle.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj);
				Action<float> onChange = customSliderSet.onChange;
				customSliderSet.onChange = delegate(float f)
				{
					onChange(f);
					controller.UpdateTexts();
				};
			}
		}
	}

	internal static class UI
	{
		internal static List<TextGui> MeasurementGuis { get; } = new List<TextGui>
		{
			new Measurements.Height.Gui(),
			new Measurements.BraSize.Gui(),
			new Measurements.Bust.Gui(),
			new Measurements.Waist.Gui(),
			new Measurements.Hips.Gui(),
			new Measurements.WaistToHips.Gui(),
			new Measurements.Dick.Gui()
		};

		internal static void Initialize(MeasurementsPlugin plugin)
		{
			MakerAPI.RegisterCustomSubCategories += delegate(object _, RegisterSubCategoriesEvent e)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0006: Invalid comparison between Unknown and I4
				if ((int)KoikatuAPI.GetCurrentGameMode() == 1)
				{
					RegisterCustomSubCategories(plugin, e);
				}
			};
			MakerAPI.MakerFinishedLoading += delegate
			{
				UpdateData();
				Hooks.InitBustGravitySliders();
			};
		}

		private static void RegisterCustomSubCategories(MeasurementsPlugin plugin, RegisterSubCategoriesEvent e)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Expected O, but got Unknown
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			MakerCategory val = new MakerCategory(Body.CategoryName, "Measurements", int.MaxValue, (string)null);
			e.AddSubCategory(val);
			AddMeasurementControls(plugin, val, e);
			((RegisterCustomControlsEvent)e).AddControl<MakerSeparator>(new MakerSeparator(val, (BaseUnityPlugin)(object)plugin));
			((UnityEvent)((RegisterCustomControlsEvent)e).AddControl<MakerButton>(new MakerButton("Refresh", val, (BaseUnityPlugin)(object)plugin)).OnClick).AddListener(new UnityAction(UpdateData));
			((RegisterCustomControlsEvent)e).AddControl<MakerSeparator>(new MakerSeparator(val, (BaseUnityPlugin)(object)plugin));
			AddConfigControls(plugin, val, e);
		}

		private static void AddMeasurementControls(MeasurementsPlugin plugin, MakerCategory category, RegisterSubCategoriesEvent e)
		{
			foreach (TextGui measurementGui in MeasurementGuis)
			{
				measurementGui.Initialize(category, plugin, e);
			}
		}

		private static void AddConfigControls(MeasurementsPlugin plugin, MakerCategory category, RegisterSubCategoriesEvent e)
		{
			new MetricUnitsGui().Initialize(category, plugin, e);
			new RegionGui().Initialize(category, plugin, e);
		}

		private static void UpdateData()
		{
			((Component)MakerAPI.GetCharacterControl()).gameObject.GetComponent<MeasurementsController>().UpdateTexts();
		}
	}

	public const string GUID = "sakacheggs.measurements";

	public const string Name = "Chara Measurements";

	public const string Version = "1.2.0";

	internal static readonly string[] Regions = Enum.GetNames(typeof(Region));

	internal static ManualLogSource Logger { get; private set; }

	public void Start()
	{
		Logger = ((BaseUnityPlugin)this).Logger;
		Configuration.Initialize(((BaseUnityPlugin)this).Config);
		CharacterApi.RegisterExtraBehaviour<MeasurementsController>("sakacheggs.measurements");
		Hooks.InitHooks();
		UI.Initialize(this);
	}

	private static MeasurementsController GetController(ChaControl chaControl)
	{
		if (chaControl == null)
		{
			return null;
		}
		return ((Component)chaControl).gameObject.GetComponent<MeasurementsController>();
	}
}
