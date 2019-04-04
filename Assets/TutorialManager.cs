using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    private string sceneName;



    void Start()
    {

        
    }

    void Update()
    {

    }

    public void ChangeScene(string _name)
    {
        sceneName = _name;

        if (_name == "Classic")
        {
            AdMobManager.Instance.HideBanner();
        }


        StartCoroutine(WaitToChangeScene());

    }



    IEnumerator WaitToChangeScene()
    {
        FadeManager.Instance.Fade(true, 0.5f);
        yield return new WaitForSeconds(0.5f); // Wait .25 seconds half the animation
        FadeManager.Instance.Fade(false, 0.5f);
        SceneManager.LoadScene(sceneName);
    }
}
