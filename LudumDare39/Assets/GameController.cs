using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Text scoreText;
    public PowerArray[] paray;
    public Text multiplyPL, speedPL, recoveryPL, laserPL;
    int playerScore;
    public PowerUps multiplyPU, speedPU, recoveryPU, laserPU;
    public Button multiplyButton, speedButton, recoveryButton, laserButton;

    int lastPUIndex;

    void Awake() {
        instance = this;
        DontDestroyOnLoad (transform.gameObject);
        multiplyPU = new PowerUps (0);
        multiplyPU.SetEffect (paray);
        speedPU = new PowerUps (1);
        speedPU.SetEffect (paray);
        recoveryPU = new PowerUps (2);
        recoveryPU.SetEffect (paray);
        laserPU = new PowerUps (3);
        laserPU.SetEffect (paray);
    }

    void Start() {
        playerScore = 0;
        scoreText.text = "Score: " + playerScore.ToString();

        

    }

    public void AddToPlayerScore(int scoreToBeAdded) {
        scoreToBeAdded *= (int) multiplyPU.effect;
        playerScore += scoreToBeAdded;
        scoreText.text = "Score: " + playerScore.ToString();
    }

    public int GetPlayerScore() {
        return playerScore;
    }

    public void DecreaseLevel(int index) {
        lastPUIndex = index;
        switch (index) {
            case 0:     //multiply
                multiplyPU.level -= 1;
                multiplyPL.text = multiplyPU.level.ToString ();
                if(multiplyPU.level <= 0) {
                    multiplyButton.interactable = false;
                }
                multiplyPU.SetEffect (paray);
                Debug.Log ("Multiply Power: " + multiplyPU.effect.ToString ());
                break;

            case 1:     //speed
                speedPU.level -= 1;
                speedPL.text = speedPU.level.ToString();
                if (speedPU.level <= 0) {
                    speedButton.interactable = false;
                }
                speedPU.SetEffect (paray);
                Debug.Log ("Speed Power: " + speedPU.effect.ToString ());
                break;

            case 2:     //recovery
                recoveryPU.level -= 1;
                recoveryPL.text = recoveryPU.level.ToString();
                if (recoveryPU.level <= 0) {
                    recoveryButton.interactable = false;
                }
                recoveryPU.SetEffect (paray);
                Debug.Log ("Recovery Power: " + recoveryPU.effect.ToString ());
                break;

            case 3:     //laser
                laserPU.level -= 1;
                laserPL.text = laserPU.level.ToString();
                if (laserPU.level <= 0) {
                    laserButton.interactable = false;
                }
                laserPU.SetEffect (paray);
                Debug.Log ("Laser Power: " + laserPU.effect.ToString ());
                break;

            default:
                break;
        }
    }

    public void ShowPowerUps() {

        if(multiplyPU.level > 0) {
            multiplyButton.interactable = true;
        }
        if (speedPU.level > 0) {
            speedButton.interactable = true;
        }
        if (recoveryPU.level > 0) {
            recoveryButton.interactable = true;
        }
        if (laserPU.level > 0) {
            laserButton.interactable = true;
        }

        switch (lastPUIndex) {
            case 0:
                multiplyButton.interactable = false;
                break;
            case 1:
                speedButton.interactable = false;
                break;
            case 2:
                recoveryButton.interactable = false;
                break;
            case 3:
                laserButton.interactable = false;
                break;
        }
    }
}

public class PowerUps {

    public int level;
    public float effect;

    int arrayToRef;

    public PowerUps (int ator) {
        level = 3;
        arrayToRef = ator;
    }

    public void SetEffect(PowerArray[] pa) {
        effect = pa[arrayToRef].powerArray[level];
    }
}

[System.Serializable]
public class PowerArray {
    public float[] powerArray;
}
