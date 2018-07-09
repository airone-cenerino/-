using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateObject: MonoBehaviour {

	public GameObject[] objects = new GameObject[4];
	public Text scorePointText;
	int randomNum;	
	int max = 5;			//objectの種類数
	int staticNumber = 0;	//静止したobjectの数
	int objectNumber = 0;	//場にあるobjectの数
	int scorePoint = 0;
	bool firstDrop = true;
	bool dropped = false;
	float notStaticTime = 10.0f;	//なぜか次のターンに行かないときは10秒で強制的に進める



	// Update is called once per frame
	void Update () {


		Debug.Log ("static:" + staticNumber + "  object:" + objectNumber);

		// 落とした後、10秒たってもオブジェクトが静止しなければ、強制的に進める
		if (dropped) {
			notStaticTime -= Time.deltaTime;
			if (notStaticTime <= 0.0f) {
				staticNumber = objectNumber;
			}
		}
			
		// 場にあるオブジェクトの数と静止したオブジェクトの数が等しくなったときに呼ばれる
		if (staticNumber == objectNumber) {
			randomNum = Random.Range (0, max);
			Instantiate (objects [randomNum], this.transform.position, this.transform.rotation);
			objectNumber++;
			staticNumber = 0;
			dropped = false;
			notStaticTime = 10.0f;

			if (firstDrop) {
				firstDrop = false;
			} else {
				scorePoint += 100;
			}
		}

		scorePointText.text = scorePoint.ToString();
	}

	public void staticObject(){
		staticNumber++;
	}

	public void movingObject(){
		staticNumber--;
	}



	// オブジェクトが落下したら、呼ばれる
	public void droppedControl(){
		dropped = true;

		// 場にいる全てのオブジェクトに落ちたことを報告
		GameObject[] fieldObjects = GameObject.FindGameObjectsWithTag ("Object");
		for (int i = 0; i < fieldObjects.Length; i++) {
			fieldObjects [i].GetComponent<ObjectController> ().droppedReceiver ();
		}
	}

	//  ゲームオーバー後にオブジェクトが生成されるのを防ぐ
	public void gameOverTimeControl(){
		notStaticTime = 10000;
	}
}
