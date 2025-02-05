using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EAudioSystem;

public class NoteDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        destroyNote(other.gameObject);
    }

    private void destroyNote(GameObject note)
    {
        Destroy(note);
        PlayerData.UpdatePlayerScore(false, 0, 5);
        PlayerData.playerNoteStreak = 0;
    }    

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
