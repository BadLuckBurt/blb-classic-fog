using UnityEngine;
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

    [Invoke(StateManager.StateTypes.Start, 0)]
    public static void Init(InitParams initParams)
    {
        Mod = initParams.Mod;  // Get mod     
        Instance = new GameObject("BLBClassicFog").AddComponent<BLBClassicFog>(); // Add script to the scene.    

        float vanillaDistance = 100.0f;
        float eyeOfArgoniaDistance = 200.0f;

        float viewDistance = vanillaDistance;
        float multiplier = viewDistance / vanillaDistance;

        GameManager.Instance.MainCamera.farClipPlane = viewDistance;
        GameManager.Instance.StreamingWorld.TerrainDistance = 1;

        float vanillaStartDistance = 56.0f;
        float startDistance = vanillaStartDistance * multiplier;

        WeatherManager weatherManager = GameManager.Instance.WeatherManager;

        SunnyFogSettings.startDistance = startDistance;
        SunnyFogSettings.endDistance = viewDistance - (4 * multiplier);
        weatherManager.SunnyFogSettings = SunnyFogSettings;

        OvercastFogSettings.startDistance = startDistance - (4 * multiplier);
        OvercastFogSettings.endDistance = viewDistance - (8 * multiplier);
        weatherManager.OvercastFogSettings = OvercastFogSettings;

        RainyFogSettings.startDistance = startDistance - (8 * multiplier);
        RainyFogSettings.endDistance = viewDistance - (16 * multiplier);
        weatherManager.RainyFogSettings = RainyFogSettings;

        SnowyFogSettings.startDistance = startDistance - (16 * multiplier); 
        SnowyFogSettings.endDistance = viewDistance - (32 * multiplier);
        weatherManager.SnowyFogSettings = SnowyFogSettings;

        HeavyFogSettings.startDistance = startDistance - (24 * multiplier);
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