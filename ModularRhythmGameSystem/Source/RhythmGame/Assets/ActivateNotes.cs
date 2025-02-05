using EAudioSystem;
using UnityEngine;

public class ActivateNotes : MonoBehaviour
{
    public GameObject noteController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelData.presongTimerFinished == true)
        {
            if (noteController.activeSelf == false)
            {
                noteController.SetActive(true);
                MoveNotes mN = noteController.GetComponent<MoveNotes>();
                if (mN.enabled == false)
                {
                    mN.enabled = true;
                }
            }
        }
    }
}
