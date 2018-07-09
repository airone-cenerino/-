using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

	public void buttonGameStart(){
		SceneManager.LoadScene("main");
	}
}
