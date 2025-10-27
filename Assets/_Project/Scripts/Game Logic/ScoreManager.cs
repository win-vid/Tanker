using UnityEngine;

// Singleton Score Manager to handle player scores
public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
        string playerName = "Player_Name";

        void Awake()
        {
            instance = this;
            playerName = PlayerPrefs.GetString(name);
        }
        void Update()
        {
            
        }
        
        void setPlayerName(string newName)
        {
            playerName = newName;
            PlayerPrefs.SetString("Player_Name", playerName);
        }
    
}
