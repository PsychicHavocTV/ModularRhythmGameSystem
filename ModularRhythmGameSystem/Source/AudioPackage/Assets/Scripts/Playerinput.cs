using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerinput : MonoBehaviour
{

    public InputKey enumChoice;
    bool canInteract = false;
    bool collectedPoints = false;
    GameObject collectable = null;
    Image thisInteractor = null;


    [SerializeField] Color interactorColor;
    [SerializeField] Color pressedColor;

    public enum InputKey
    {
        S,
        D,
        K,
        L,
        SPACEBAR,
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            collectable = other.gameObject;
            canInteract = true;
            collectedPoints = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            if (collectedPoints == false)
            {
                Destroy(collectable.gameObject);
                collectable = null;
                EAudioSystem.PlayerData.UpdatePlayerScore(false, 0, 1);
                Debug.Log("Player Score: " + EAudioSystem.PlayerData.GetPlayerScore());
            }
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        thisInteractor = gameObject.GetComponent<Image>();
        Debug.Log("Red: " + thisInteractor.color.r + " || Green: " + thisInteractor.color.g + " || Blue: " + thisInteractor.color.b);

        EAudioSystem.PlayerData.SetScoreMultiplier(1);

    }

    void DestroyCollectable()
    {
        if (canInteract == true)
        {
            collectedPoints = true;
            EAudioSystem.PlayerData.UpdatePlayerScore(true, 1);
            Debug.Log("Player Score: " + EAudioSystem.PlayerData.GetPlayerScore());
            Destroy(collectable);
            collectable = null;
            canInteract = false;
        }
        else
        {
            EAudioSystem.PlayerData.UpdatePlayerScore(false, 0, 1);
            Debug.Log("Player Score: " + EAudioSystem.PlayerData.GetPlayerScore());
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
    }

    void InputCheck()
    {

        switch (enumChoice)
        {
            case InputKey.SPACEBAR:
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    thisInteractor.color = pressedColor;
                    DestroyCollectable();
                }

                if (Input.GetKeyUp(KeyCode.Space))
                { 
                    thisInteractor.color = interactorColor;
                }
                break;
            }
            case InputKey.S:
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    thisInteractor.color = pressedColor;
                    DestroyCollectable();
                }

                if (Input.GetKeyUp(KeyCode.S))
                {
                    thisInteractor.color = interactorColor;
                }

                break;
            }

            case InputKey.D:
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    thisInteractor.color = pressedColor;
                    DestroyCollectable();
                }

                if (Input.GetKeyUp(KeyCode.D))
                {
                    thisInteractor.color = interactorColor;
                }
                break;
            }

            case InputKey.K:
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    thisInteractor.color = pressedColor;
                    DestroyCollectable();
                }

                if (Input.GetKeyUp(KeyCode.K))
                {
                    thisInteractor.color = interactorColor;
                }
                break;
            }

            case InputKey.L:
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    thisInteractor.color = pressedColor;
                    DestroyCollectable();
                }

                if (Input.GetKeyUp(KeyCode.L))
                {
                    thisInteractor.color = interactorColor;
                }
                break;
            }
        }
    }
}
