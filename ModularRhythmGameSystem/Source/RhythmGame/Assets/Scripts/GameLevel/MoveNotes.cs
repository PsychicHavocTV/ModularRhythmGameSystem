using UnityEngine;
using EAudioSystem;

public class MoveNotes : MonoBehaviour
{
    [SerializeField] private GameObject levelObjectsParent;
    [SerializeField] private GameObject noteParent;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform noteRemovePos;
    private NoteBehaviour currentNote;
    private Vector2 noteSpawnPos;
    private Vector3 movement;
    private bool readyToSpawn = false;
    private bool canMove = false;
    private float[] beats;
    private float levelSpeed;
    private float secPerBeat;
    private float dspSongTime;
    private float songPosition;
    private float songPosInBeats;



    private void Awake()
    {
        secPerBeat = EAudio.CalculateSecPerBeat(LevelData.levelData.songBPM);
        dspSongTime = EAudio.CalculateDSPSongTime();
        LevelData.SetNotemap(EAudio.selectedLevel.songTimings);
        LevelData.notes[0] += 0.15f;
    }

    public void SpawnMusicNote()
    {
        GameObject newNote = Instantiate(notePrefab, noteParent.transform);

        NoteBehaviour nb = newNote.GetComponent<NoteBehaviour>();
        nb.removePos = new Vector2(960, noteRemovePos.transform.position.y);
        nb.beatOfThisNote = LevelData.GetCurrentBeat();
        currentNote = nb;
    }

    private void Update()
    {
        songPosition = EAudio.CalculateSongPosition(dspSongTime);
        songPosInBeats = EAudio.CalculateSongPosInBeats(songPosition, secPerBeat);
        LevelData.posInBeats = songPosInBeats;
        LevelData.CheckNoteSpawn(LevelData.levelData, songPosition, songPosInBeats, dspSongTime, secPerBeat, readyToSpawn);

        if (LevelData.spawnNote == true)
        {
            LevelData.spawnNote = false;
            SpawnMusicNote();
        }
    }
}
