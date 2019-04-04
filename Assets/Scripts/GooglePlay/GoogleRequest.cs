using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleRequest : MonoBehaviour {

	
    public void LeaderBoardRequest()
    {
        GooglePlayManager.Instance.ShowLeaderboard();
    }

    public void AchievementRequest()
    {
        GooglePlayManager.Instance.ShowAchievements();
    }
}
