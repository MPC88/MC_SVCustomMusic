using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace MC_SVCustomMusic
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string pluginGuid = "mc.starvalor.custommusic";
        public const string pluginName = "SV Custom Music";
        public const string pluginVersion = "1.1.0";

        private enum SongType { mainmenu, battle, chill, tense };

        private static ConfigEntry<bool> cfgRandomise;
        private static ConfigEntry<bool> cfgReplace;

        private string fld_Plugins = "";
        private bool startedLoading = false;
        private readonly string[] folders =
        {
            "/MCSVCustomMusic/MainMenu/",
            "/MCSVCustomMusic/Battle/",
            "/MCSVCustomMusic/Chill/",
            "/MCSVCustomMusic/Tense/"
        };
        private readonly bool[] loadedFlags = { false, false, false, false };
        private readonly int[] todoCounts = { 0, 0, 0, 0 };
        private readonly int[] doneCnts = { 0, 0, 0, 0 };
        private readonly List<AudioClipData>[] clipData = new List<AudioClipData>[4];        

        private static ManualLogSource log = BepInEx.Logging.Logger.CreateLogSource(pluginName);

        public void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(Main));

            fld_Plugins = Path.GetDirectoryName(GetType().Assembly.Location);

            cfgReplace = Config.Bind<bool>(
                "Config",
                "Replace original tracks",
                false,
                "Replace original tracks or add new tracks to existing pool.");
            cfgRandomise = Config.Bind<bool>(
                "Config",
                "Randomise next track",
                false,
                "Randomise next track or use default sequential selection.");

            foreach (string folder in folders)
                if (!Directory.Exists(fld_Plugins + folder))
                    Directory.CreateDirectory(fld_Plugins + folder);
        }

        public void Update()
        {
            if (!startedLoading && GameManager.instance != null)
            {
                LoadSongs(SongType.mainmenu, GameManager.instance.musicData.mainMenu);
                LoadSongs(SongType.battle, GameManager.instance.musicData.battle);
                LoadSongs(SongType.chill, GameManager.instance.musicData.chill);
                LoadSongs(SongType.tense, GameManager.instance.musicData.tense);
                startedLoading = true;
            }

            if(startedLoading)
            {
                if (!loadedFlags[(int)SongType.mainmenu] &&
                doneCnts[(int)SongType.mainmenu] >= todoCounts[(int)SongType.mainmenu])
                {
                    GameManager.instance.musicData.mainMenu = clipData[(int)SongType.mainmenu].ToArray();
                    loadedFlags[(int)SongType.mainmenu] = true;
                }

                if (!loadedFlags[(int)SongType.battle] &&
                    doneCnts[(int)SongType.battle] >= todoCounts[(int)SongType.battle])
                {
                    GameManager.instance.musicData.battle = clipData[(int)SongType.battle].ToArray();
                    loadedFlags[(int)SongType.battle] = true;
                }

                if (!loadedFlags[(int)SongType.chill] &&
                    doneCnts[(int)SongType.chill] >= todoCounts[(int)SongType.chill])
                {
                    GameManager.instance.musicData.chill = clipData[(int)SongType.chill].ToArray();
                    loadedFlags[(int)SongType.chill] = true;
                }

                if (!loadedFlags[(int)SongType.tense] &&
                    doneCnts[(int)SongType.tense] >= todoCounts[(int)SongType.tense])
                {
                    GameManager.instance.musicData.tense = clipData[(int)SongType.tense].ToArray();
                    loadedFlags[(int)SongType.tense] = true;
                }
            }
        }

        private void LoadSongs(SongType type, AudioClipData[] originals)
        {
            string[] fileList = Directory.GetFiles(fld_Plugins + folders[(int)type]);
            if(cfgReplace.Value && fileList.Length > 0)
                clipData[(int)type] = new List<AudioClipData>();
            else
                clipData[(int)type] = new List<AudioClipData>(originals);
            todoCounts[(int)type] = fileList.Length;

            if (fileList != null)
                foreach (string file in fileList)
                    StartCoroutine(LoadSong(type, file));
        }

        private IEnumerator<UnityWebRequestAsyncOperation> LoadSong(SongType type, string file)
        {
            AudioClip newClip = null;
            using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(string.Format("file://{0}", file), UnityEngine.AudioType.UNKNOWN))
            {
                yield return webRequest.SendWebRequest();
                
                newClip = DownloadHandlerAudioClip.GetContent(webRequest);

                if (newClip == null)
                {
                    log.LogError("Failed to load custom music " + file);
                    doneCnts[(int)type]++;
                    yield break;
                }
            }
            AudioClipData acd = new AudioClipData() {clip = newClip, playOnStreamerMode = true};
            clipData[(int)type].Add(acd);
            log.LogInfo("Successfully loaded custom music " + file);
            doneCnts[(int)type]++;
        }

        [HarmonyPatch(typeof(Music), nameof(Music.Play))]
        [HarmonyPostfix]
        private static void MusicPlay_Post(int type, ref int ___nextMenu, ref int ___nextChill, ref int ___nextTense, ref int ___nextBattle)
        {
            if (!cfgRandomise.Value)
                return;

            //0 Main, 2 = chill, 3 = tense, 4 = battle
            switch(type)
            {
                case 0:
                    ___nextMenu = Random.Range(0, GameManager.instance.musicData.mainMenu.Length - 1);
                    break;
                case 2:
                    ___nextChill = Random.Range(0, GameManager.instance.musicData.chill.Length - 1);
                    break;
                case 3:
                    ___nextTense = Random.Range(0, GameManager.instance.musicData.tense.Length - 1);
                    break;
                case 4:
                    ___nextBattle = Random.Range(0, GameManager.instance.musicData.battle.Length - 1);
                    break;
            }
        }
    }
}
