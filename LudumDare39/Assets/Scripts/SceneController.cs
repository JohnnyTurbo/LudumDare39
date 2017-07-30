using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    public static SceneController instance;

    public GameObject endOfLevelUI;
    public float levelTime, bonusScore;
    public Text scoreText;

    float startTime;

    void Awake() {
        instance = this;
    }

    void Start() {
        startTime = Time.time;
        HideUI ();
        GameController.instance.HUDGO.SetActive (true);
    }

	public void EndLevel() {
        endOfLevelUI.SetActive (true);
        Time.timeScale = 0f;
        float timeBonus = ((1-(Time.time - startTime) / levelTime)) * bonusScore;
        timeBonus = Mathf.Round (timeBonus);
        if(timeBonus <= 0) {
            timeBonus = 0;
        }
        scoreText.text = timeBonus.ToString ();
    }
	
    public void HideUI() {
        endOfLevelUI.SetActive (false);
    }

    public void ShowPowerOps() {
        GameController.instance.ShowPowerUps ();
    }
}
