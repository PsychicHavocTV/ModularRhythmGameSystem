using UnityEngine;
using EAudioSystem;

public class NoteBehaviour : MonoBehaviour
{
    public Vector2 spawnPos;
    public Vector2 removePos;

    public float beatOfThisNote;
    float posInBeats = 0;

    private void Awake()
    {
        spawnPos = this.transform.position;
    }

    private void Update()
    {
        if (spawnPos != null && removePos != null)
        {
            float percent = (LevelData.beatsShownInAdvance - (beatOfThisNote - LevelData.posInBeats)) / LevelData.beatsShownInAdvance;
            float newY = Mathf.Lerp(spawnPos.y, removePos.y, percent);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
