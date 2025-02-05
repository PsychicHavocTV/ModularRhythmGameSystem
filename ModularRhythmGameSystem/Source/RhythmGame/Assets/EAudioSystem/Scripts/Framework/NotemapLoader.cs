using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Networking;

namespace EAudioSystem
{
    public class NotemapLoader
    {

        public static IList<Notemap> notemapFiles = new List<Notemap>();
        public static IList<string> filePaths = new List<string>();
        public static IList<string> metaFilePaths = new List<string>();
        public static IList<ScriptableObjectHandler> levelObjects = new List<ScriptableObjectHandler>();

        public static void SetUpFiles()
        {
            notemapFiles.Clear();
            filePaths.Clear();
            metaFilePaths.Clear();

            GetNotemapFiles();


            for (int i = 0; i <= filePaths.Count - 1; i++)
            {
                Notemap levelMap = new Notemap();
                levelMap = fileData(i);
                notemapFiles.Add(levelMap);

                ScriptableObjectHandler levelObject = ScriptableObject.CreateInstance<ScriptableObjectHandler>(); 
                levelObject = CreateLevelObject(levelMap);
                levelObjects.Add(levelObject);

                UpdateLevelsArray();
            }
            foreach (ScriptableObjectHandler sOH in EAudio.levels)
            {
                Debug.Log(sOH.levelName);
            }
        }

        // Update the array of levels (in EAudio) to include levels loaded from files.
        public static void UpdateLevelsArray(IList<ScriptableObjectHandler> newLevelsList = null)
        {
            int currentLevelsCount = 0;

            // Count the amount of objects in the Levels array - (From EAudio).
            foreach (ScriptableObjectHandler level in EAudio.levels)
            {
                currentLevelsCount++;
            }

            // Create a new array to temporarily hold level data using the amount of levels counted.
            ScriptableObjectHandler[] levelContainer = new ScriptableObjectHandler[currentLevelsCount];

            // Add all of the objects from the Levels array - (From EAudio) into the temporary container.
            for (int i = 0; i < currentLevelsCount; i++)
            {
                levelContainer[i] = EAudio.levels[i];
            }

            int levelsTBACount = 0;

            if (newLevelsList == null)
            {
                // Count the amount of levels read from files.
                foreach (ScriptableObjectHandler level in levelObjects)
                {
                    levelsTBACount++;
                }

                // Add up the total amount of levels.
                int newLevelTotal = (currentLevelsCount + levelsTBACount);

                // Re-create the Levels array - (From EAudio) with a size equal to the newly calculated amount of levels.
                EAudio.levels = new ScriptableObjectHandler[newLevelTotal];

                int levelIndexer = 0;

                // Add all of the objects from the temporary container into the newly resized level array.
                for (levelIndexer = 0; levelIndexer < currentLevelsCount; levelIndexer++)
                {
                    EAudio.levels[levelIndexer] = levelContainer[levelIndexer];
                }

                int secondaryIndexer = 0;

                // Add the levels read from files into the newly resized level array.
                for (int i = levelIndexer; i < newLevelTotal; i++)
                {
                    EAudio.levels[i] = levelObjects[secondaryIndexer];
                    secondaryIndexer++;
                }

                // Find the indexes of any level objects that are NULL (if any).
                IList<int> nullLevels = new List<int>();
                for (int i = 0; i < EAudio.levels.Length; i++)
                {
                    if (EAudio.levels[i] == null)
                    {
                        nullLevels.Add(i);
                    }
                }
                
                IList<ScriptableObjectHandler> replacementLevelsList = new List<ScriptableObjectHandler>();

                for (int i = 0; i < EAudio.levels.Length; i++)
                {
                    replacementLevelsList.Add(EAudio.levels[i]);
                }

                // Remove all NULL level objects using the indexes we found.
                foreach (int nullLevel in nullLevels)
                {
                    if (replacementLevelsList[nullLevel] == null)
                    {
                        replacementLevelsList.RemoveAt(nullLevel);
                    }
                }
                
                
                // Find and remove any duplicate levels.
                for (int i = 0; i < replacementLevelsList.Count; i++)
                {
                    for (int a = 0; a < replacementLevelsList.Count; a++)
                    {
                        if (a != i)
                        {
                            if (replacementLevelsList[i].levelName == replacementLevelsList[a].levelName && replacementLevelsList[i].songBPM == replacementLevelsList[a].songBPM)
                            {
                                replacementLevelsList.RemoveAt(a);
                            }
                        }
                    }
                }

                
                EAudio.levels = new ScriptableObjectHandler[replacementLevelsList.Count];
                
                for (int i = 0; i < EAudio.levels.Length; i++)
                {
                    EAudio.levels[i] = replacementLevelsList[i];
                }
            }
        }

        public static ScriptableObjectHandler CreateLevelObject(Notemap levelData)
        {
            ScriptableObjectHandler levelObject = ScriptableObject.CreateInstance<ScriptableObjectHandler>();
            levelObject.songBPM = levelData.songBPM;
            levelObject.levelName = levelData.levelName;
            levelObject.songName = levelData.songName;
            levelObject.songArtist = levelData.songArtist;
            levelObject.levelSong = levelData.song;

            int timingsCount = 0;
            foreach (float val in levelData.timings)
            {
                timingsCount++;
            }

            levelObject.songTimings = new float[timingsCount];
            for (int i = 0; i < timingsCount; i++)
            {
                levelObject.songTimings[i] = levelData.timings[i];
            }

            return levelObject;
        }

        public static void GetNotemapFiles()
        {
            foreach (string file in Directory.GetFiles(Application.persistentDataPath + "/Notemaps/", "*", SearchOption.AllDirectories))
            {
                if (file.EndsWith(".json"))
                {
                    if (!file.Contains("Meta") && !file.Contains("meta"))
                    {
                        filePaths.Add(file);
                    }
                    else
                    {
                        metaFilePaths.Add(file);
                    }
                }
            }
        }

        public static Notemap fileData(int pathIndex)
        {
            int maxsize = 2;

            Notemap notemapValues = new Notemap();
            int findExt = filePaths[pathIndex].IndexOf(".");

            // Load song file ------
            string songFilePath = filePaths[pathIndex].Replace("json", "wav");
            if (File.Exists(songFilePath))
            {
                UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip("file:///" + songFilePath, AudioType.WAV);
                req.SendWebRequest();

                while (!req.isDone) { };

                AudioClip aC = DownloadHandlerAudioClip.GetContent(req);
                notemapValues.song = aC;
            }
            // ---------------------

            // Load Metadata File ------
            string metaFilePath = filePaths[pathIndex].Insert(findExt, "Meta");
            if (File.Exists(metaFilePath))
            {
                using (StreamReader sR = new StreamReader(metaFilePath))
                {
                    using (JsonReader reader = new JsonTextReader(sR))
                    {
                        reader.SupportMultipleContent = true;
                        int maxLines = 4;
                        string output = "";
                        for (int i = 0; i < maxLines; i++)
                        {
                            output = reader.ReadAsString();
                            switch (i)
                            {
                                // BPM
                                case 0:
                                    {
                                        notemapValues.songBPM = float.Parse(output);
                                        break;
                                    }
                                // Level Name
                                case 1:
                                    {
                                        notemapValues.levelName = output;
                                        break;
                                    }
                                // Song Name
                                case 2:
                                    {
                                        notemapValues.songName = output;
                                        break;
                                    }
                                // Song Artist Name
                                case 3:
                                    {
                                        notemapValues.songArtist = output;
                                        break;
                                    }
                            }
                        }
                        reader.Close();
                    }
                    sR.Close();
                }
            }
            // ---------------------

            // Load Notemap Data ------
            using (StreamReader sR = new StreamReader(filePaths[pathIndex]))
            {
                using (JsonReader reader = new JsonTextReader(sR))
                {
                    reader.SupportMultipleContent = true;
                    for (int i = 0; i < maxsize; i++)
                    {
                        if (reader.Value != null)
                        {
                            float timing = 0;
                            timing = float.Parse(reader.Value.ToString());
                            notemapValues.timings.Add(timing);
                        }
                        if (reader.Read() != false)
                        {
                            maxsize += 2;
                        }
                    }
                    reader.Close();
                }
                sR.Close();
            }
            // ---------------------

            return notemapValues;
        }

        //public void SortTimingsList(Notemap noteTimingData)
        //{
        //    bool isSorted = false;
        //
        //    int timingCount = 0;
        //
        //    foreach (float val in noteTimingData.timings)
        //    {
        //        timingCount++;
        //    }
        //
        //    while (isSorted == false)
        //    {
        //        for (int i = 0; i < timingCount; i++)
        //        {
        //            
        //        }
        //    }
        //    return;
        //}

        public void getFileData()
        {
            //int fileCount = Directory.GetFiles(Application.persistentDataPath + "/Notemaps/", "*", SearchOption.AllDirectories).Length;
            //
            //Debug.Log("Files in notemaps folder: " + fileCount);
            //
            //JObject fileData = new JObject();
            //
            //
            //Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            //serializer.NullValueHandling = NullValueHandling.Ignore;
            //
            //Notemap newNoteMap = new Notemap();
            //
            //int maxsize = 2;
            //
            //foreach (string file in Directory.GetFiles(Application.persistentDataPath + "/Notemaps/", "*", SearchOption.AllDirectories))
            //{
            //    if (file.EndsWith(".json"))
            //    {
            //        using (StreamReader sR = new StreamReader(file))
            //        {
            //            using (JsonReader reader = new JsonTextReader(sR))
            //            {
            //                reader.SupportMultipleContent = true;
            //                //fileData = JObject.Parse(sR.ReadToEnd());
            //                for (int i = 0; i < maxsize; i++)
            //                {
            //                    if (reader.Value != null)
            //                    {
            //                        newNoteMap.timings.Add(float.Parse(reader.Value.ToString()));
            //                        //Debug.Log(reader.Value);
            //                    }
            //                    if (reader.Read() != false)
            //                    {
            //                        maxsize += 2;
            //                    }
            //                }
            //
            //                foreach (float val in newNoteMap.timings)
            //                {
            //                    Debug.Log("Timing: " + val);
            //                }
            //            }
            //            sR.Close();
            //        }
            //
            //        string jsonFile = System.IO.File.ReadAllText(file);
            //
            //        newNoteMap.songName = "someSongName";
            //        newNoteMap.songBPM = 180;
            //        newNoteMap.noteTimingsNUMS = new float[] { 0.0f, 0.5f, 1.0f };
            //
            //
            //
            //        string output = JsonConvert.SerializeObject(newNoteMap);
            //
            //
            //
            //        //IList<JToken> results = fileData["notemap"]["notemapData"].Children().ToList();
            //        //IList<JToken> mapResults = fileData["noteTimingData"]["noteTimings"].Children().ToList();
            //        Notemap deserializedNotemap = Newtonsoft.Json.JsonConvert.DeserializeObject<Notemap>(jsonFile);
            //
            //        Debug.Log("DESERIALIZED: " + deserializedNotemap.songName);
            //        Notemap nMap = new Notemap();
            //        Notemap mapArrayData = new Notemap();
            //
            //    }
            //}
        }
    }
}
