using EAudioSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI levelName;
    public TextMeshProUGUI songName;
    public TextMeshProUGUI songArtist;

    //[SerializeField] HandleScenes sM;

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject startLevelPrefab;
    public GameObject buttonParent;
    public GameObject startLevelButtonParent;

    private ScriptableObjectHandler selectedLevel;
    private Button[] levelSelectionButtons = new Button[0];
    private Button startLevelButton;
    private Button newButton;
    private bool setList = false;

    private int buttonIndexer;

    // Start is called before the first frame update
    void Awake()
    {
        levelName.text = "";
        songName.text = "";
        songArtist.text = "";
        selectedLevel = null;
        startLevelButton = null;
        setList = false;
    }

    void OnSelectionClick(Button bttn)
    {
        int i = 0;
        for (int a = 0; a < levelSelectionButtons.Length; a++)
        {
            if (levelSelectionButtons[a] == bttn)
            {
                i = a;
                break;
            }
        }

        selectedLevel = EAudioSystem.EAudio.levels[i];

        if (startLevelButton == null)
        {
            startLevelButton = Instantiate(startLevelPrefab.GetComponent<Button>(), startLevelButtonParent.transform);
            startLevelButton.transform.SetParent(startLevelButtonParent.transform);
        }

        LevelData.levelData = EAudioSystem.EAudio.levels[i];

        levelName.text = EAudioSystem.EAudio.levels[i].levelName;
        songName.text = EAudioSystem.EAudio.levels[i].songName;
        songArtist.text = EAudioSystem.EAudio.levels[i].songArtist;

        
        return;
    }

    void HideUserInterface()
    {
        EAudioSystem.EAudio.readyToLoadLevel = true;
        //sM.changeToGameScene();
        EAudio.currentLevel = selectedLevel;
        EAudioSystem.LevelData.StartLevel(selectedLevel);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (setList == false)
        //{
        //    setList = true;
        if (levelSelectionButtons.Length <= 0)
        {
            levelSelectionButtons = new Button[EAudioSystem.EAudio.levels.Length];
            for (int i = 0; i < levelSelectionButtons.Length; i++)
            {
                if (levelSelectionButtons[i] == null)
                {
                    levelSelectionButtons[i] = Instantiate(buttonPrefab.GetComponent<Button>(), buttonParent.transform);
                    levelSelectionButtons[i].transform.SetParent(buttonParent.transform);
                }
            }
        }
        //}

        foreach (Button bttn in levelSelectionButtons)
        {
            bttn.onClick.AddListener(() => OnSelectionClick(bttn));
        }

        if (startLevelButton != null && selectedLevel != null)
        {
            startLevelButton.onClick.AddListener(() => HideUserInterface());
        }
    }
}
