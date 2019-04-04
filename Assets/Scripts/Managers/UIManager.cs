using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour {

	private static UIManager instance;
	public static UIManager Instance{get{return instance;}}


	public int score;
	public Text scoreText;


	public Text finalScoreText;

	public Text highScoreText;
	public Text starText;


	public bool gameOver;

	public Canvas gameOverCanvas;

	public GameObject drawManager;

	public GameObject ball;

	public GameObject drawLineToStartText;

	public bool pause;
	public GameObject pauseButton;
	public GameObject restartButton;
	public GameObject homeButton;

	public string sceneName;

	public GameObject handPointer;


	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		ball = GameObject.Find ("Ball").gameObject;
		ball.GetComponent<PlayerManager> ().ballAnimator.SetBool ("IsDead", false);
		ball.SetActive (true);
		ball.GetComponent<PlayerManager> ().isDead = false;
	}
		
	public void GameOver(){
		
		gameOver = true;

		if(gameOver == true){
		//	Debug.Log ("Game Over");

			if(Social.localUser.authenticated == true){
				SaveManager.Instance.state.gamesPlayed += 1;
			}

			SaveManager.Instance.state.gamesPlayedChecker += 1; // Check how many games have been played then show an ad.
			if(SaveManager.Instance.state.gamesPlayedChecker == 20 && SaveManager.Instance.state.broughtAds == false){
				SaveManager.Instance.state.gamesPlayedChecker = 0;
				AdManager.Instance.ShowAd ("video");
			}

	
			StartCoroutine(WaitForGameOver());
		}

	}


	IEnumerator WaitForGameOver(){

		pauseButton.SetActive (false); // Turn of the pause button
	
		ball.GetComponent<PlayerManager> ().Died(); // Turn the animation on, and freeze it constraints

		yield return new WaitForSeconds (1.0f); // Wait for player death animation.

		FadeManager.Instance.Fade (true, 0.5f);

		yield return new WaitForSeconds (0.5f);

		FadeManager.Instance.Fade (false, 0.5f);

		gameOverCanvas.gameObject.SetActive (true);

		ball.transform.position = new Vector2 (-20, -20); // Move the player off screen

		Destroy (ball.GetComponent<PlayerController>());

		ball.GetComponent<PlayerManager> ().ballAnimator.SetBool ("IsDead", false); // Set the player animation back to idle

		ball.SetActive (false);

        if(SaveManager.Instance.state.broughtAds == false)
        {

            AdMobManager.Instance.ShowBanner();

        }


        if (Social.localUser.authenticated == true){
			if(SaveManager.Instance.state.gamesPlayed >= 100){
				Social.ReportProgress ("CgkItpiq1OgVEAIQBg", 100, (bool success) => {});
			}

			else if(SaveManager.Instance.state.gamesPlayed >= 500){
				Social.ReportProgress ("CgkItpiq1OgVEAIQBw", 100, (bool success) => {});
			}

			else if(SaveManager.Instance.state.gamesPlayed >= 1000){
				Social.ReportProgress ("CgkItpiq1OgVEAIQCA", 100, (bool success) => {});
			}
		}


		if(SceneManager.GetActiveScene().name == "Classic"){

            if (score > SaveManager.Instance.state.classicHighScore){
				SaveManager.Instance.state.classicHighScore = score;
				SaveManager.Instance.Save ();
			}

			if(Social.localUser.authenticated){
				Social.ReportScore (SaveManager.Instance.state.classicHighScore, "CgkItpiq1OgVEAIQDw", (bool success) =>{});

                if (score >= 20){
					Social.ReportProgress ("CgkItpiq1OgVEAIQAw", 100, (bool success) => {});
				} 

				if(score >= 50){
					Social.ReportProgress ("CgkItpiq1OgVEAIQBA", 100, (bool success) => {});
				} 

				if(score >= 100){
					Social.ReportProgress ("CgkItpiq1OgVEAIQBQ", 100, (bool success) => {});
				} 
			}


			finalScoreText.text = score.ToString ();
			highScoreText.text = SaveManager.Instance.state.classicHighScore.ToString ();

		}

        else if (SceneManager.GetActiveScene().name == "Boost")
        {
            if (score > SaveManager.Instance.state.boostHighScore)
            {
                SaveManager.Instance.state.boostHighScore = score;
                SaveManager.Instance.Save();
            }

            if (Social.localUser.authenticated)
            {
                Social.ReportScore(SaveManager.Instance.state.boostHighScore, "CgkItpiq1OgVEAIQEA", (bool success) => {
                });
            }

            finalScoreText.text = score.ToString();
            highScoreText.text = SaveManager.Instance.state.boostHighScore.ToString();
        }


        else if (SceneManager.GetActiveScene().name == "Gap"){
			if(score > SaveManager.Instance.state.gapHighScore){
				SaveManager.Instance.state.gapHighScore = score;
				SaveManager.Instance.Save ();
			}

			if (Social.localUser.authenticated) {
				Social.ReportScore (SaveManager.Instance.state.gapHighScore, "CgkItpiq1OgVEAIQCQ", (bool success) => {
				});
			}

			finalScoreText.text = score.ToString ();
			highScoreText.text = SaveManager.Instance.state.gapHighScore.ToString ();
		}

		else if(SceneManager.GetActiveScene().name == "Hoop"){
			if(score > SaveManager.Instance.state.hoopHighScore){
				SaveManager.Instance.state.hoopHighScore = score;
				SaveManager.Instance.Save ();
			}

			if (Social.localUser.authenticated) {
				Social.ReportScore(SaveManager.Instance.state.hoopHighScore, "CgkItpiq1OgVEAIQCg", (bool success) => {
				});
			}
			finalScoreText.text = score.ToString ();
			highScoreText.text = SaveManager.Instance.state.hoopHighScore.ToString ();
		}

		else if(SceneManager.GetActiveScene().name == "Protect"){
			if(score > SaveManager.Instance.state.protectHighScore){
				SaveManager.Instance.state.protectHighScore = score;
				SaveManager.Instance.Save ();
			}

			if (Social.localUser.authenticated) {
				Social.ReportScore (SaveManager.Instance.state.protectHighScore, "CgkItpiq1OgVEAIQCw", (bool success) => {
				});
			}

			finalScoreText.text = score.ToString ();
			highScoreText.text = SaveManager.Instance.state.protectHighScore.ToString ();
		}

      

        starText.text = SaveManager.Instance.state.star.ToString ();


		SaveManager.Instance.Save ();

	}
		
	public void AddScore()
	{
		score++;
		scoreText.text = score.ToString();
	}

	public void Pause(){
		pause = !pause;
		if (pause == true) {
			restartButton.SetActive (true);
			homeButton.SetActive (true);
            GameObject.Find("DrawingManagerStraight").GetComponent<DrawLine>().enabled = false;

            




            Time.timeScale = 0;

		} else {
			Time.timeScale = 1;
			restartButton.SetActive (false);
			homeButton.SetActive (false);

            GameObject.Find("DrawingManagerStraight").GetComponent<DrawLine>().enabled = true;

            
        }
	}

	public void BackToMenu()
	{
		StartCoroutine (WaitToMenu());
	}

	IEnumerator WaitToMenu(){
		Time.timeScale = 1;

        if (SaveManager.Instance.state.broughtAds == false)
        {

            AdMobManager.Instance.ShowBanner();

        }


        ball.GetComponent<PlayerManager>().isDead = true; //Trigger the death animation of the player

        PlayerReset();
       
        if (SceneManager.GetActiveScene().name == "Protect"){
			ball.GetComponent<Collider2D> ().sharedMaterial = ball.GetComponent<PlayerManager>().noFriction; // Set the physic material of the player back to no friction
		}

		

		if(ball.GetComponent<PlayerController>() != null){
			Destroy (ball.GetComponent<PlayerController>()); // If the player has a playercontroller destroy it when returning to the menu
		}

		else if(ball.GetComponent<ProtectPlayerController>() != null){
			Destroy (ball.GetComponent<ProtectPlayerController>()); // If the player has a playercontroller destroy it when returning to the menu
		}

        else if (ball.GetComponent<BoostPlayerController>() != null)
        {
            Destroy(ball.GetComponent<BoostPlayerController>()); // If the player has a playercontroller destroy it when returning to the menu
        }

        FadeManager.Instance.Fade (true, 0.5f);
		yield return new WaitForSeconds (0.5f); // Wait .25 seconds half the animation

        ball.SetActive(true); // Set the player to active

        ball.GetComponent<PlayerManager>().isDead = false;
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.transform.eulerAngles = Vector3.zero; // Set the rotation of the player back to default

        ball.transform.position = ball.GetComponent<PlayerManager>().startPosition; // return the player's position back to default

        FadeManager.Instance.Fade(false, 0.5f);

        SceneManager.LoadScene ("Main Menu"); // Change scenes make to menu
	}

	public void Restart(){
		StartCoroutine (WaitToRestart());
	}

	IEnumerator WaitToRestart(){
		Time.timeScale = 1;

        if (SaveManager.Instance.state.broughtAds == false)
        {

            AdMobManager.Instance.HideBanner();

        }

       


        PlayerReset();


        FadeManager.Instance.Fade (true, 0.5f);
		yield return new WaitForSeconds (0.5f); // Wait .25 seconds half the animation

        if (ball.GetComponent<PlayerController>() != null)
        {
            Destroy(ball.GetComponent<PlayerController>()); // If the player has the playercontroller script delete it
        }

        if (ball.GetComponent<ProtectPlayerController>() != null)
        {
            Destroy(ball.GetComponent<ProtectPlayerController>()); // If the player has the protectplayercontroller delete it
        }

        if (ball.GetComponent<BoostPlayerController>() != null)
        {
            Destroy(ball.GetComponent<BoostPlayerController>()); // If the player has the boostplayercontroller delete it
        }

        ball.GetComponent<PlayerManager>().isDead = false;
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.transform.eulerAngles = Vector3.zero; // Set the rotation of the player back to default


        ball.SetActive(true); // Set the player back to active
        handPointer.SetActive(true);

        FadeManager.Instance.Fade(false, 0.5f);
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex); // Load the current scene

	}
		
	public void RestartFromGame(){
		StartCoroutine (WaitToRestartFromGame());
	}

	IEnumerator WaitToRestartFromGame(){
        Time.timeScale = 1; // Set the timescale back to 1

        ball.GetComponent<PlayerManager>().isDead = true; 

        PlayerReset();

        if (ball.GetComponent<PlayerController>() != null){
			Destroy (ball.GetComponent<PlayerController>()); // If the player has the playercontroller script delete it

		}

		if(ball.GetComponent<ProtectPlayerController>() != null){
			Destroy (ball.GetComponent<ProtectPlayerController>()); // If the player has the protectplayercontroller delete it
		}

        if (ball.GetComponent<BoostPlayerController>() != null)
        {
            Destroy(ball.GetComponent<BoostPlayerController>()); // If the player has the boostplayercontroller delete it
        }

        FadeManager.Instance.Fade (true, 0.5f);
        yield return new WaitForSeconds (0.5f); 


        ball.transform.eulerAngles = Vector3.zero; 

        FadeManager.Instance.Fade(false, 0.5f);

		handPointer.SetActive (true);

		if (SceneManager.GetActiveScene ().name == "Protect") {
			ball.transform.position = new Vector3 (0,0,0); // If scene if protect set the player position back to the starting position (0,0,0)
		} else {
			ball.transform.position = new Vector3 (0,2,0); 
		}

		ball.GetComponent<PlayerManager> ().isDead = false; 
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    }

	public void ChangeScene(string _sceneName){ // Controls which scene the buttons load
		SaveManager.Instance.Save ();
		sceneName = _sceneName;
		StartCoroutine (WaitToChangeScene()); // Change the scene to which the button is
	}
		
	IEnumerator WaitToChangeScene(){

        ball.GetComponent<PlayerManager>().isDead = true; // Set the player bool to dead

        if (ball.GetComponent<PlayerController>() != null)
        {
            Destroy(ball.GetComponent<PlayerController>()); // If the player has a playercontroller destroy it when returning to the menu
        }

        else if (ball.GetComponent<ProtectPlayerController>() != null)
        {
            Destroy(ball.GetComponent<ProtectPlayerController>()); // If the player has a playercontroller destroy it when returning to the menu
        }

        else if (ball.GetComponent<BoostPlayerController>() != null)
        {
            Destroy(ball.GetComponent<BoostPlayerController>()); // If the player has a playercontroller destroy it when returning to the menu
        }

        FadeManager.Instance.Fade (true, 0.5f);
		yield return new WaitForSeconds (0.5f); // Wait .25 seconds half the animation

        ball.SetActive(true);
  
        ball.GetComponent<PlayerManager>().isDead = false; // Set the player bool to not dead

        FadeManager.Instance.Fade(false, 0.5f);

		SceneManager.LoadScene (sceneName); // Change the scene
	}
		
	public void AddStar(int _amount){
		if (SaveManager.Instance.state.broughtDoubledStars == false) {
			SaveManager.Instance.state.star += _amount;
			SaveManager.Instance.Save ();
		} else if (SaveManager.Instance.state.broughtDoubledStars == true) {
			_amount *= 2;
			SaveManager.Instance.state.star += _amount;
			SaveManager.Instance.Save ();
		}
			
	}

    public void PlayerReset()
    {
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; // Remove all constraints of the player
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Stop the player having movement
        ball.GetComponent<Rigidbody2D>().gravityScale = 0; // Set the players gravity scale back to 0 for the reset of the 
    }

    public void ToStorePage()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.J10.lineball");
    }
}
