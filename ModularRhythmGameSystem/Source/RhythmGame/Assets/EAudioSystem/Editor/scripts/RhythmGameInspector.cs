using UnityEngine;
using UnityEditor;
using EAudioSystem;

[CustomEditor(typeof(DataInspector))]
class RhythmGameInspector : Editor
{
    
    ScriptableObjectHandler levelObject;
    public AudioSource musicPlayer;
    private AudioClip song;

    public override void OnInspectorGUI()
    {
        DataInspector dataInspector = (DataInspector)target;

        GUILayout.Space(10);

        dataInspector.levelObjectCount = EditorGUILayout.IntField(dataInspector.levelObjectCount);

        // Button to remove a level ScriptableObject from the array.
        if (GUILayout.Button(new GUIContent("-", "Remove a level from the array.")))
        {
            // If the 'levels' array from dataInspector is NOT empty
            if (dataInspector.levels.Length != 0)
            {
                for (int i = 0; i < dataInspector.levels.Length; i++)
                {
                    if (dataInspector.levels.Length <= 1)
                    {
                        dataInspector.levels = new ScriptableObjectHandler[0];
                        dataInspector.levelObjectCount = 0;
                    }
                }
            }
            if (dataInspector.levels.Length > 0)
            {
                ScriptableObjectHandler[] tempLevels = new ScriptableObjectHandler[dataInspector.levels.Length];

                if (dataInspector.levels.Length > 1)
                {
                    for (int i = 0; i < dataInspector.levels.Length - 1; i++)
                    {
                        tempLevels[i] = dataInspector.levels[i];
                    }
                    
                    dataInspector.levels = new ScriptableObjectHandler[tempLevels.Length - 1];

                    for (int i = 0; i < tempLevels.Length - 1; i++)
                    {
                        dataInspector.levels[i] = tempLevels[i];
                    }
                }
                else if (dataInspector.levels.Length == 1)
                {
                    dataInspector.levels = new ScriptableObjectHandler[0];
                    dataInspector.levelObjectCount = 0;
                }

            }
        }

        // Button to add a level ScriptableObject to the array.
        if (GUILayout.Button(new GUIContent("+", "Add a new level to the array.")))
        {
            ScriptableObjectHandler[] tempLevels = new ScriptableObjectHandler[dataInspector.levels.Length];

            for (int i = 0; i < dataInspector.levels.Length; i++)
            {
                tempLevels[i] = dataInspector.levels[i];
            }

            dataInspector.levels = new ScriptableObjectHandler[tempLevels.Length + 1];

            for (int i = 0; i < tempLevels.Length; i++)
            {
                dataInspector.levels[i] = tempLevels[i];
            }
        }

        dataInspector.levelObjectCount = dataInspector.levels.Length;
        GUILayout.Space(5);

        if (dataInspector.levelObjectCount > 0)
        {
            for (int i = 0; i < dataInspector.levelObjectCount; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Level Data Object", GUILayout.Width(105));
                dataInspector.levels[i] = (ScriptableObjectHandler)EditorGUILayout.ObjectField(dataInspector.levels[i], typeof(ScriptableObjectHandler), true, GUILayout.Width(300));
                GUILayout.EndHorizontal();
            }
        }

        GUILayout.Space(10);

        GUILayout.Label(new GUIContent("Music Player", "A GameObject holds the audio source for all levels."));
        dataInspector.m_audioSource = (AudioSource)EditorGUILayout.ObjectField(dataInspector.m_audioSource, typeof(AudioSource), true);
        EAudioSystem.EAudio.source = dataInspector.m_audioSource;

        GUILayout.Space(3);

        // Song Playback Speed
        GUILayout.Label("Song Playback Speed");
        GUILayout.Space(0.5f);
        GUILayout.BeginHorizontal();
        dataInspector.levelSpeed = EditorGUILayout.IntSlider((int)dataInspector.levelSpeed, -2, 3);
        LevelData.levelSpeed = dataInspector.levelSpeed;
        GUILayout.Space(0.5f);
        if (GUILayout.Button(new GUIContent("Confirm New Playback Speed", "Applies any changes made to the playback speed.")))
        {
            if (dataInspector.levelSpeed == 0 || LevelData.levelSpeed == 0)
            {
                dataInspector.levelSpeed = 1;
                LevelData.levelSpeed = 1;
            }
            EAudioSystem.EAudio.SetSoundSpeed(LevelData.source.outputAudioMixerGroup, LevelData.source, LevelData.levelSpeed);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(3);

        // Delay between the level starting and the song starting.
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Song Delay Duration (Seconds)", "The amount of seconds to wait before the levels song starts playing."));
        dataInspector.levelStartDelay = EditorGUILayout.IntField(dataInspector.levelStartDelay, GUILayout.Width(150));
        EAudioSystem.EAudio.levelStartDelaySeconds = dataInspector.levelStartDelay;
        GUILayout.EndHorizontal();

        GUILayout.Space(5);

        foreach (ScriptableObjectHandler sOH in dataInspector.levels)
        {
            GUILayout.Space(5);
            if (sOH != null)
            {
                GUILayout.Label("LEVEL DATA");
                GUILayout.Space(3);
                GUILayout.BeginHorizontal();

                // Level Name Editable Text Box.
                GUILayout.Label("Level Name", GUILayout.Width(75));
                string currentLevelName = sOH.levelName;
                currentLevelName = GUILayout.TextField(currentLevelName, 100, GUILayout.Width(150));
                sOH.levelName = currentLevelName;
                GUILayout.EndHorizontal();

                GUILayout.Space(3);

                // Song Name Editable Text Box.
                GUILayout.BeginHorizontal();
                string currentSongName = sOH.songName;
                GUILayout.Label("Song Name", GUILayout.Width(75));
                currentSongName = GUILayout.TextField(currentSongName, 100, GUILayout.Width(150));
                sOH.songName = currentSongName;
                GUILayout.EndHorizontal();

                GUILayout.Space(3);

                // Song Artist Editable Textbox.
                GUILayout.BeginHorizontal();
                string currentArtistName = sOH.songArtist;
                GUILayout.Label("Song Artist", GUILayout.Width(75));
                currentArtistName = GUILayout.TextField(currentArtistName, 100, GUILayout.Width(150));
                sOH.songArtist = currentArtistName;
                GUILayout.EndHorizontal();

                GUILayout.Space(3);

                // Song File.
                GUILayout.BeginHorizontal();
                GUILayout.Label("Sound File", GUILayout.Width(75));
                song = sOH.levelSong;
                song = (AudioClip)EditorGUILayout.ObjectField(song, typeof(AudioClip), true, GUILayout.Width(300));
                if (song != null)
                {
                    sOH.levelSong = song;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.Space(5);
        }
        GUILayout.Space(10);
    }
}
