using UnityEngine;

public class SetUICamera : MonoBehaviour
{
    [SerializeField] private Canvas levelCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.sceneCamera != null)
        {
            levelCanvas.worldCamera = GameManager.instance.sceneCamera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
