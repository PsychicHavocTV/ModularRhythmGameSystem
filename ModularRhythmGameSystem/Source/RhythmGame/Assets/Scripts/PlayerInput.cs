using EAudioSystem;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputKey primaryKey;
    [SerializeField] private InputKey secondaryKey;
    [SerializeField] private Color interactorColour;
    [SerializeField] private Color pressedColour;
    [SerializeField] private GameObject hitParticleObject;
    private ParticleSystem hitParticle;
    private bool hasSpawnedParticle = false;
    private bool canInteract = false;
    private bool collectedPoints = false;
    private bool noteCollected = false;
    private GameObject collectable = null;
    private GameObject hpP;
    private Image thisInteractor = null;

    public enum InputKey
    {
        LEFTSHIFT,
        RIGHTSHIFT,
        NULL,
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.ToLower() == "collectable")
        {
            collectable = other.gameObject;
            noteCollected = false;
            collectedPoints = false;
            canInteract = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.ToLower() == "collectable" && collectedPoints == false && noteCollected == false)
        {
            //Destroy(other.gameObject);
            //hitParticle = null;
            //collectable = null;
            //if (noteCollected == false)
            //{
            //    DestroyCollectable();
            //}
        }
    }

    private void Awake()
    {
        thisInteractor = gameObject.GetComponent<Image>();
        EAudioSystem.PlayerData.SetScoreMultiplier(1);
        EAudioSystem.PlayerData.ResetScoreData();
    }

    void DestroyCollectable()
    {
        if (canInteract == true)
        {
            collectedPoints = true;
            noteCollected = true;
            DestroyImmediate(collectable);
            collectable = null;
            EAudioSystem.PlayerData.UpdatePlayerScore(true, 10);
            PlayerData.playerNoteStreak += 1;
            canInteract = false;
            hitParticle = hitParticleObject.GetComponent<ParticleSystem>();
            hitParticle.Play();
        }
        else
        {
            PlayerData.playerNoteStreak = 0;
            EAudioSystem.PlayerData.UpdatePlayerScore(false, 0, 5);
        }
    }

    void InputCheck()
    {
        switch (primaryKey)
        {
            case InputKey.LEFTSHIFT:
            {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        thisInteractor.color = pressedColour;
                        DestroyCollectable();
                    }

                    if (Input.GetKeyUp(KeyCode.LeftShift)) 
                    { 
                        thisInteractor.color = interactorColour; 
                    }
                    break;
            }

            case InputKey.RIGHTSHIFT:
            {
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        thisInteractor.color = pressedColour;
                        DestroyCollectable();
                    }

                    if (Input.GetKeyUp(KeyCode.RightShift))
                    {
                        thisInteractor.color = interactorColour;
                    }
                    break;
            }
        }

        switch (secondaryKey)
        {
            case InputKey.LEFTSHIFT:
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        thisInteractor.color = pressedColour;
                        DestroyCollectable();
                    }

                    if (Input.GetKeyUp(KeyCode.LeftShift))
                    {
                        thisInteractor.color = interactorColour;
                    }
                    break;
                }

            case InputKey.RIGHTSHIFT:
                {
                    if (Input.GetKeyDown(KeyCode.RightShift))
                    {
                        thisInteractor.color = pressedColour;
                        DestroyCollectable();
                    }

                    if (Input.GetKeyUp(KeyCode.RightShift))
                    {
                        thisInteractor.color = interactorColour;
                    }
                    break;
                }
        }
    }

    private void Update()
    {
        InputCheck();
    }
}
