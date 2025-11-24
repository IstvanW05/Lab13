using UnityEngine;

namespace GameAnalyticsSDK
{
    public class GameManager : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameAnalytics.Initialize();
        }
        public void LevelCompleted(int levelNum)
        {
            GameAnalytics.NewDesignEvent("LevelComplete", levelNum);
        }
    }
}

