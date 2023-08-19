using BepInEx;
using BepInEx.Logging;
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
        public const string pluginVersion = "1.0.0";

        private enum SongType { mainmenu, battle, chill, tense };

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
            fld_Plugins = Path.GetDirectoryName(GetType().Assembly.Location);

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
    }
}
