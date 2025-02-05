using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    public Vector2 spawnPos;
    public Vector2 removePos;

    public float beatOfThisNote;
    float posInBeats = 0;

    private void Update()
    {
        if (spawnPos != null && removePos != null)
        {
            transform.position = Vector2.Lerp(
                spawnPos,
                removePos,
                EAudioSystem.LevelData.beatsShownInAdvance - (beatOfThisNote - EAudioSystem.LevelData.posInBeats)) / EAudioSystem.LevelData.beatsShownInAdvance;
        }
    }
}
