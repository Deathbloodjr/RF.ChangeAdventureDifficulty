using HarmonyLib;
using Scripts.Adventure;
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
        [HarmonyPatch(typeof(AdvManager))]
        [HarmonyPatch(nameof(AdvManager.SetEnsoData))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPrefix]
        public static void AdvManager_SetEnsoData_Prefix(AdvManager __instance, ref EnsoData.EnsoLevelType ensoLevelType)
        {
            Logger.Log("AdvManager_SetEnsoData_Prefix");
            ensoLevelType = EnsoData.EnsoLevelType.Mania;
            
            //Plugin.Log.LogInfo("");

            //if (overrideSongTitle)
            //{
            //    __instance.SongNames[(int)CurrentLanguage] = GetSongTitleFromLanguage(musicinfo, SongTitleOverride);
            //}
            //__instance.SongNames[0] = musicinfo.SongNameEN;
            //__instance.SongNames[1] = musicinfo.SongNameJP;
        }
    }
}
