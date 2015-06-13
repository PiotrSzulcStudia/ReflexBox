using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private float leftBound;
	private float rightBound;
	private float spawnPoint;
	private float endPoint;

	public enum GameState { Playing = 0, Paused, GameOver };
	public enum BoxType { Left = 0, Right, All, Bonus };
	public enum BonusType { DoubleScore = 0, Life, SlowTime };

	private GameState gameState;

	public GameObject eventSystem;
	
	private int livesLeft;
	private int score;
	private int highScore;

	private float deltaSpawnTime;
	private float currentSpawnTime;
	private const float minSpawnTime = 0.75f;
	private const float maxSpawnTime = 2.0f;
	private const float stepRate = 0.10f;

	// Prefabs
	public GameObject LeftCube;
	public GameObject RightCube;

	// 
	private GameObject leftBoundObject;
	private GameObject rightBoundObject;


	private int scorePerBox = 100;
	private int scoringFactor = 1;

	public void AddPoints() {
		if (gameState == GameState.Playing)
			score += scorePerBox * scoringFactor;

		eventSystem.SendMessage("UpdateScore", score);
	}

	public void LoseLife() {
		livesLeft--;
		if (livesLeft <= 0) {
			livesLeft = 0;
			gameState = GameState.GameOver;
		}

		eventSystem.SendMessage("UpdateLives", livesLeft);

	}

	public int GetScore() {
		return score;
	}
    
	// Use this for initialization
	void Start () {
		currentSpawnTime = 1.0f;
		deltaSpawnTime = maxSpawnTime;
		spawnPoint = 3.0f;

		resolvePlacement();
		score = 0;
		livesLeft = 3;
		gameState = GameState.Playing;

	}

	private void spawnBox() {

		float r = Random.value;
		if (r < 0.45) {
			Instantiate(LeftCube, new Vector3(0, spawnPoint, 0), Quaternion.identity);
		} else if ( r < 0.9) {
			Instantiate(RightCube, new Vector3(0, spawnPoint, 0), Quaternion.identity);
		} else {
			float bonus = Random.value;
		}
	}

	private void timeCheckToSpawn() {
		if (currentSpawnTime < 0) {
			spawnBox();
			currentSpawnTime = deltaSpawnTime;
			deltaSpawnTime -= stepRate;
			if (deltaSpawnTime < minSpawnTime)
				deltaSpawnTime = minSpawnTime;
        } else {
			currentSpawnTime -= Time.deltaTime;
		}
	}

	// Update is called once per frame
	void Update () {
		if (gameState == GameState.Playing) {
			timeCheckToSpawn();
		} 

	
	}

	private void resolvePlacement() {

		leftBoundObject = GameObject.FindGameObjectWithTag(Tags.LEFT_BOUND);
		rightBoundObject = GameObject.FindGameObjectWithTag(Tags.RIGHT_BOUND);

		leftBoundObject.transform.position = new Vector3(- Camera.main.orthographicSize ,0 ,0);
		rightBoundObject.transform.position = new Vector3(Camera.main.orthographicSize,0 ,0);

		float l = Screen.width;

	}
}
