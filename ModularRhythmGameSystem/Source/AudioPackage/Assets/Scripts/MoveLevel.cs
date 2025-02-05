using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EAudioSystem;

public class MoveLevel : MonoBehaviour
{
    bool canMove = false;
    [SerializeField] GameObject levelObjectsParent;

    public float levelSpeed;
    float secPerBeat;
    float dspSongTime;

    float songPosition;
    float songPosInBeats;

    public float[] beats;

    public GameObject noteParent;
    public Vector2 noteSpawnPos;
    public Transform noteRemovePos;

    bool readyToSpawn = false;

    public GameObject notePrefab;

    public NoteBehaviour currentNote;

    Vector3 movement;

    // Start is called before the first frame update
    void Awake()
    {
        secPerBeat = EAudioSystem.EAudio.CalculateSecPerBeat(LevelData.levelData.songBPM);
        dspSongTime = EAudioSystem.EAudio.CalculateDSPSongTime();

        EAudioSystem.LevelData.SetNotemap(beats);

    }

    public void SpawnMusicnote()
    {
        GameObject newNote = Instantiate(notePrefab, noteParent.transform);
        NoteBehaviour nb = newNote.GetComponent<NoteBehaviour>();
        currentNote = nb;
        nb.spawnPos = newNote.transform.position;
        nb.removePos = new Vector2(960, noteRemovePos.transform.position.y);
        nb.beatOfThisNote = EAudioSystem.LevelData.GetCurrentBeat();
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = EAudioSystem.EAudio.CalculateSongPosition(dspSongTime);

        songPosInBeats = EAudioSystem.EAudio.CalculateSongPosInBeats(songPosition, secPerBeat);

        EAudioSystem.LevelData.posInBeats = songPosInBeats;

        EAudioSystem.LevelData.CheckNoteSpawn(LevelData.levelData, songPosition, songPosInBeats, dspSongTime, secPerBeat, readyToSpawn);

        if (EAudioSystem.LevelData.spawnNote == true)
        {
            EAudioSystem.LevelData.spawnNote = false;
            SpawnMusicnote();
        }
    }
}
