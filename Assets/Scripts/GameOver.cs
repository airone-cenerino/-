using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public GameObject canvas;
	bool gameOver = false;
	float time = 3.0f;
	GameObject objectGenerator;

	void Start(){
		objectGenerator = GameObject.Find ("ObjectGenerator");
	}

	void Update(){
		if (gameOver) {
			time -= Time.deltaTime;
			if (time <= 0.0f) {
				SceneManager.LoadScene ("Start");
			}
		}
	}

	// 物が落ちてきたら呼ばれる
	void OnTriggerEnter(){
		canvas.SetActive (true);
		gameOver = true;
		objectGenerator.GetComponent<CreateObject> ().gameOverTimeControl ();
	}
}