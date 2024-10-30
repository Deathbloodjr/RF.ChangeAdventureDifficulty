using HarmonyLib;
using Scripts.Adventure;
using Scripts.GameSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MusicDataInterface;

namespace ChangeAdventureDifficulty.Plugins
{
    internal class ChangeAdventureDifficultyPatch
    {
        static EnsoData.EnsoLevelType selectedLevel = EnsoData.EnsoLevelType.Num;

        [HarmonyPatch(typeof(AdvManager))]
        [HarmonyPatch(nameof(AdvManager.SetEnsoData))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPrefix]
        public static void AdvManager_SetEnsoData_Prefix(AdvManager __instance, string selectMusicUID, ref EnsoData.EnsoLevelType ensoLevelType)
        {
            SetSelectedLevel();

            if (selectedLevel == EnsoData.EnsoLevelType.Ura)
            {
                var infoById = SingletonMonoBehaviour<CommonObjects>.Instance.MyDataManager.MusicData.GetInfoById(selectMusicUID);
                if (infoById.Stars[(int)EnsoData.EnsoLevelType.Ura] == 0)
                {
                    ensoLevelType = EnsoData.EnsoLevelType.Mania;
                }
                else
                {
                    ensoLevelType = EnsoData.EnsoLevelType.Ura;
                }
            }
            else
            {
                ensoLevelType = selectedLevel;
            }
        }

        static void SetSelectedLevel()
        {
            if (selectedLevel == EnsoData.EnsoLevelType.Num)
            {
                var level = Plugin.Instance.ConfigSelectedDifficulty.Value;
                switch (level)
                {
                    case "Easy": selectedLevel = EnsoData.EnsoLevelType.Easy; return;
                    case "Normal": selectedLevel = EnsoData.EnsoLevelType.Normal; return;
                    case "Hard": selectedLevel = EnsoData.EnsoLevelType.Hard; return;
                    case "Oni": selectedLevel = EnsoData.EnsoLevelType.Mania; return;
                    case "Ura": selectedLevel = EnsoData.EnsoLevelType.Ura; return;
                }
            }
        }
    }
}
