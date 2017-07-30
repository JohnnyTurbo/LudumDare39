using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    public GameObject endOfLevelUI;
    public float levelTime, bonusScore;

    float startTime;

    void Start() {
        startTime = Time.time;
    }

	public void EndLevel() {
        endOfLevelUI.SetActive (true);
        float timeBonus = ((Time.time - startTime) / levelTime) * bonusScore;
        Mathf.Round (timeBonus);
        if(timeBonus <= 0) {
            timeBonus = 0;
        }
    }
	
}
