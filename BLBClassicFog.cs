using UnityEngine;
using System.Collections.Generic;
using DaggerfallConnect;
using DaggerfallWorkshop.Game;
using DaggerfallWorkshop.Game.Utility.ModSupport;   //required for modding features
using DaggerfallWorkshop;

public class BLBClassicFog : MonoBehaviour
{
    public static Mod Mod {
        get;
        private set;
    }

    public static BLBClassicFog Instance { get; private set; }

    public static Dictionary<int, float> viewDistances = new Dictionary<int, float>();

    [Invoke(StateManager.StateTypes.Start, 0)]
    public static void Init(InitParams initParams)
    {
        Mod = initParams.Mod;  // Get mod     
        Instance = new GameObject("BLBClassicFog").AddComponent<BLBClassicFog>(); // Add script to the scene.    

        var settings = Mod.GetSettings();

        viewDistances.Add(0, 64f);
        viewDistances.Add(1, 128f);

        int viewDistanceSetting = settings.GetValue<int>("ViewDistance", "ViewDistance");
        float viewDistanceMultiplier = settings.GetValue<float>("ViewDistanceMultiplier","ViewDistanceMultiplier");

        float viewDistance = viewDistances[viewDistanceSetting] * viewDistanceMultiplier;
        float fogStartDistance = viewDistance * 0.375f;

        float multiplier = viewDistance / viewDistances[0];

        GameManager.Instance.MainCamera.farClipPlane = viewDistance;
        GameManager.Instance.StreamingWorld.TerrainDistance = 1;

        WeatherManager weatherManager = GameManager.Instance.WeatherManager;

        SunnyFogSettings.startDistance = fogStartDistance - (2 * multiplier);
        SunnyFogSettings.endDistance = viewDistance - (2 * multiplier);
        weatherManager.SunnyFogSettings = SunnyFogSettings;

        OvercastFogSettings.startDistance = fogStartDistance - (4 * multiplier);
        OvercastFogSettings.endDistance = viewDistance - (4 * multiplier);
        weatherManager.OvercastFogSettings = OvercastFogSettings;

        RainyFogSettings.startDistance = fogStartDistance - (8 * multiplier);
        RainyFogSettings.endDistance = viewDistance - (8 * multiplier);
        weatherManager.RainyFogSettings = RainyFogSettings;

        SnowyFogSettings.startDistance = fogStartDistance - (16 * multiplier); 
        SnowyFogSettings.endDistance = viewDistance - (16 * multiplier);
        weatherManager.SnowyFogSettings = SnowyFogSettings;

        HeavyFogSettings.startDistance = fogStartDistance - (32 * multiplier);
        HeavyFogSettings.endDistance = viewDistance - (32 * multiplier);
        weatherManager.HeavyFogSettings = HeavyFogSettings;

        Debug.Log("BLB Classic Fog values have been set");
    }

    void Awake ()
    {
        Mod.IsReady = true;
        Debug.Log("blb-classic-fog awakened");
    }
    
    public static WeatherManager.FogSettings SunnyFogSettings = new WeatherManager.FogSettings { fogMode = FogMode.Linear, density = 0.0f, startDistance = 0, endDistance = 2560, excludeSkybox = true };
    public static WeatherManager.FogSettings OvercastFogSettings = new WeatherManager.FogSettings { fogMode = FogMode.Linear, density = 0.0f, startDistance = 0, endDistance = 2048, excludeSkybox = true };
    public static WeatherManager.FogSettings RainyFogSettings = new WeatherManager.FogSettings { fogMode = FogMode.Linear, density = 0.0f, startDistance = 0, endDistance = 1536, excludeSkybox = true };
    public static WeatherManager.FogSettings SnowyFogSettings = new WeatherManager.FogSettings { fogMode = FogMode.Linear, density = 0.0f, startDistance = 0, endDistance = 1280, excludeSkybox = true };
    public static WeatherManager.FogSettings HeavyFogSettings = new WeatherManager.FogSettings { fogMode = FogMode.Linear, density = 0.0f, startDistance = 0, endDistance = 1024, excludeSkybox = true };
    
}