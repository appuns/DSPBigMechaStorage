using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System;
using System.IO;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;
using xiaoye97;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]


namespace DSPBigMechaStorage
{

    [BepInPlugin("Appun.DSP.plugin.BigMechaStorage", "DSPBigMechaStorage", "1.0.3")]
    [BepInProcess("DSPGAME.exe")]




    public class DSPBigMechaStorage : BaseUnityPlugin
    {
        //public static ConfigEntry<int> reactorStorageRow;
        //public static ConfigEntry<int> warpStorageRow;
        public static int reactorStorageSize = 24;
        public static int warpStorageSize = 5;

        public void Start()
        {
            LogManager.Logger = Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

            //reactorStorageRow = Config.Bind("General", "reactorStorageRow", 6, "Mecha Fuel Chamber Slot Row Count");
            //warpStorageRow = Config.Bind("General", "warpStorageRow", 6, "Mecha Warper Slot Row Count");


            GameObject mechaGgroup = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/mecha-group");
            mechaGgroup.transform.localPosition = new Vector3(85f, 75f, 0f);
            mechaGgroup.transform.localScale = new Vector3(0.7f, 0.7f, 0f);
            GameObject information = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/information");
            information.transform.localPosition = new Vector3(75f, -125f, 0f);
            GameObject drone = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/drone");
            drone.transform.localPosition = new Vector3(290f, 60f, 0f);
            GameObject appearance = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/appearance");
            appearance.transform.localPosition = new Vector3(250f, -110f, 0f);

            //燃料ストレージ
            GameObject fuelGroup = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/fuel-group");
            fuelGroup.transform.localPosition = new Vector3(-180f, -25f, 0f);
            fuelGroup.GetComponent<RectTransform>().sizeDelta = new Vector2(270f, 316f);
            GameObject fuelStorage = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/fuel-group/fuel-storage");
            fuelStorage.GetComponent<RectTransform>().sizeDelta = new Vector2(216f, 312f);
            GameObject fuel = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/fuel-group/fuel");
            fuel.transform.localPosition = new Vector3(77f, 157f, 0f);

            //ワーパーストレージ
            GameObject warpStorage = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/fuel-group/warp-storage");
            warpStorage.transform.localPosition = new Vector3(77f, 103f, 0f);
            warpStorage.GetComponent<RectTransform>().sizeDelta = new Vector2(54f, 270f);
            GameObject border = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/fuel-group/warp-storage/bg/border");
            border.gameObject.SetActive(true);
            border.GetComponent<Image>().enabled = true;
            GameObject bg = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/fuel-group/warp-storage/bg");
            bg.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.298f);
            bg.GetComponent<Image>().enabled = true;
            GameObject iconBg = GameObject.Find("UI Root/Overlay Canvas/In Game/Windows/Mecha Window/fuel-group/warp-storage/icon-bg");
            iconBg.GetComponent<Image>().color = new Color(0.6557f, 0.9145f, 1f, 0.0627f);


        }

        [HarmonyPatch(typeof(UIMechaWindow), "_OnOpen")]
        public static class UIMechaWindow_OnOpen

        {
            [HarmonyPostfix]

            public static void Postfix(UIMechaWindow __instance)
            {


                __instance.fuelGrid.storage.SetSize(reactorStorageSize);
                __instance.fuelGrid.colCount = 4;
                __instance.warpGrid.storage.SetSize(warpStorageSize);


            }
        }


    }


    public class LogManager
    {
        public static ManualLogSource Logger;
    }

}