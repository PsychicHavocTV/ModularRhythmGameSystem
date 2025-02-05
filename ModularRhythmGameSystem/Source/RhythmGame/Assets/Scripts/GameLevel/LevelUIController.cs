using TMPro;
using UnityEngine;
using EAudioSystem;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI streakText;
    [SerializeField] private Color pointsColour10;
    [SerializeField] private Color pointsColour50;
    [SerializeField] private Color pointsColour100;
    [SerializeField] private Color pointsColour150;
    [SerializeField] private Color streakColourDefault;
    [SerializeField] private Color streakColour30;
    [SerializeField] private Color streakColour150;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        streakText.text = PlayerData.playerNoteStreak.ToString();
        scoreText.text = PlayerData.playerScore.ToString();

        if (PlayerData.playerNoteStreak < 30)
        {
            streakText.color = streakColourDefault;
            PlayerData.SetScoreMultiplier(1);
        }
        else if (PlayerData.playerNoteStreak >= 30 && PlayerData.playerNoteStreak < 150)
        {
            streakText.color = streakColour30;
            PlayerData.SetScoreMultiplier(2);
        }
        else if (PlayerData.playerNoteStreak >= 150)
        {
            streakText.color = streakColour150;
            PlayerData.SetScoreMultiplier(4);
        }

        if (PlayerData.playerScore <= 10)
        {
            scoreText.color = pointsColour10;
        }
        else if (PlayerData.playerScore >= 50 && PlayerData.playerScore < 100)
        {
            scoreText.color = pointsColour50;
        }
        else if (PlayerData.playerScore >= 100 && PlayerData.playerScore < 150)
        {
            scoreText.color = pointsColour150;
        }
    }
}
