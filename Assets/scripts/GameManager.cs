using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private Camera mainCam;
	[SerializeField]
	private Text scoreText, gameoverText;
	[SerializeField]
	private Player player;
	private int score;
	private float gameTimer;
	private bool isGameOver;
	private float camSpeed = 10;
	private float yFloor = -10;

	private void Start () {
		Time.timeScale = 1;
		player.onHitEnemy += OnHitEnemy;
		player.onHitSpike += OnGameOver;
		player.onHitOrb += OnGameWin;
		scoreText.enabled = true;
		gameoverText.enabled = false;
		scoreText.text = "Score: " + score;
	}

	private void Update () {
		mainCam.transform.position = new Vector3 (
			Mathf.Lerp(
				mainCam.transform.position.x,
				player.transform.position.x,
				Time.deltaTime * camSpeed
			),
			Mathf.Lerp(
				mainCam.transform.position.y,
				player.transform.position.y,
				Time.deltaTime * camSpeed
			),
			mainCam.transform.position.z
		);

		if (isGameOver) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name, LoadSceneMode.Single);
			}
		}

		if (player.transform.position.y < yFloor) {
			OnGameOver ();
		}
	}

	private void OnHitEnemy () {
		score += 100;
		UpdateScore ();
	}

	private void OnGameOver () {
		isGameOver = true;
		scoreText.enabled = false;
		gameoverText.enabled = true;
		gameoverText.text = "Game Over!\nPress R to Restart!";
		Time.timeScale = 0;
	}

	private void OnGameWin () {
		isGameOver = true;
		scoreText.enabled = false;
		gameoverText.enabled = true;
		gameoverText.text = "You Won!\nPress R to Restart!";
		Time.timeScale = 0;
	}

	private void UpdateScore () {
		scoreText.text = "Score: " + score;
	}
}
