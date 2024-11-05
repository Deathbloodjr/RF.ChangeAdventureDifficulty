using Blittables;
using HarmonyLib;
using Scripts.Adventure;
using Scripts.GameSystem;
using Scripts.UserData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeAdventureDifficulty.Plugins
{
    internal class SaveScenarioPlay
    {
        [HarmonyPatch(typeof(EnsoGameManager))]
        [HarmonyPatch(nameof(EnsoGameManager.SetResults))]
        [HarmonyPatch(MethodType.Normal)]
        [HarmonyPrefix]
        public static void EnsoGameManager_SetResults_Prefix(EnsoGameManager __instance)
        {
            Logger.Log("EnsoGameManager_SetResults_Prefix");
            if (__instance.settings.ensoType == EnsoData.EnsoType.Scenario)
            {
                Logger.Log("__instance.settings.ensoType == EnsoData.EnsoType.Scenario");

                if (__instance.ensoParam.EnsoEndType == EnsoPlayingParameter.EnsoEndTypes.Normal)
                {
                    TaikoCoreFrameResults frameResults = __instance.ensoParam.GetFrameResults();
                    EachPlayer eachPlayer = frameResults.eachPlayer[0];
                    HiScoreRecordInfo record = new HiScoreRecordInfo();
                    record.score = (int)eachPlayer.score;
                    record.excellent = (short)eachPlayer.countRyo;
                    record.good = (short)eachPlayer.countKa;
                    record.bad = (short)eachPlayer.countFuka;
                    record.combo = (short)eachPlayer.maxCombo;
                    record.renda = (short)eachPlayer.countRenda;

                    DataConst.CrownType crown = DataConst.CrownType.None;
                    if (eachPlayer.tamashii >= (float)eachPlayer.constTamashiiNorm)
                    {
                        crown = DataConst.CrownType.Silver;
                    }
                    if (crown == DataConst.CrownType.Silver && record.bad == 0)
                    {
                        crown = DataConst.CrownType.Gold;
                    }
                    if (crown == DataConst.CrownType.Gold && record.good == 0)
                    {
                        crown = DataConst.CrownType.Rainbow;
                    }

                    MusicsData musicData = SingletonMonoBehaviour<CommonObjects>.Instance.MusicData;
                    musicData.UpdateNormalRecordInfo(__instance.settings.musicUniqueId, __instance.settings.ensoPlayerSettings[0].courseType, __instance.settings.ensoPlayerSettings[0].shinuchi == DataConst.OptionOnOff.On, ref record, crown);
                }
            }
        }
    }
}
