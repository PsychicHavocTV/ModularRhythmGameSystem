using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EAudioSystem
{
    public static class PlayerData
    {
        private static int playerHighscore = 0;
        private static int playerScore = 0;
        private static int scoreMultiplier = 1;

        public static int GetPlayerScore()
        {
            return playerScore;
        }

        public static void SetPlayerHighscore(int newHighscore)
        {
            playerHighscore = newHighscore;
            return;
        }

        public static int GetPlayerHighscore()
        {
            return playerHighscore;
        }

        public static void SetScoreMultiplier(int multiplier)
        {
            scoreMultiplier = multiplier;
            return;
        }

        public static int GetScoreMultiplier()
        {
            return scoreMultiplier;
        }


        public static void ResetScoreData()
        {
            playerHighscore = 0;
            playerScore = 0;
            scoreMultiplier = 1;
        }

        public static void UpdatePlayerScore(bool increase, int pointsGained, int pointsLost = 0)
        {
            if (increase == true)
            {
                playerScore += (pointsGained * scoreMultiplier);
            }
            else if (increase == false)
            {
                playerScore -= pointsLost;
            }

            if (playerScore < 0)
            {
                playerScore = 0;
            }    
        }


    }
}
