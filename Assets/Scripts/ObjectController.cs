using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour {

	Rigidbody body;
	GameObject objectGenerator;
	bool dropped = false;			// 落とされたかどうか
	bool stopped = false;			// 止まっているかどうか　毎ターンの最初にfalseになる
	float vertical = 0;
	float horizontal = 0;
	float time = 10.0f;
	GameObject timeText;
	Text timerText;
	int screenTime;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
		objectGenerator = GameObject.Find ("ObjectGenerator");
		timeText = GameObject.Find ("TimeText");
		timerText = timeText.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		//生成されたターンのみ呼ばれる
		if (dropped == false) {
			// 落ちるまでの残り時間の表示
			time -= Time.deltaTime;
			screenTime = (int)time;
			timerText.text = screenTime.ToString ();

			// 上にいるオブジェクトを移動させるための記述
			if (Input.GetAxis ("Vertical") != 0.0f || Input.GetAxis ("Horizontal") != 0.0f) {
				if (Input.GetKey (KeyCode.Z)) {
					vertical += Input.GetAxis ("Vertical") * 0.1f;
					horizontal += Input.GetAxis ("Horizontal") * 0.1f;
				} else {
					vertical += Input.GetAxis ("Vertical") * 0.5f;
					horizontal += Input.GetAxis ("Horizontal") * 0.5f;
				}
				vertical = Mathf.Clamp (vertical, -3.0f, 3.0f);
				horizontal = Mathf.Clamp (horizontal, -3.0f, 3.0f);
				transform.position = new Vector3 (horizontal, 12, vertical);
			}
			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				transform.Rotate (new Vector3 (30, 0, 0));
			}
			if (Input.GetKeyDown (KeyCode.RightShift)) {
				transform.Rotate (new Vector3 (0, 0, 30));
			}

			//上にいるオブジェクトが落ちたら呼ばれる
			if (Input.GetKeyDown ("space") || time <= 0.0f){
				body.useGravity = true;
				objectGenerator.GetComponent<CreateObject> ().droppedControl ();
				dropped = true;
			}
		}
			
		// 毎ターン静止したらObjectGeneratorに報告
		if (body.IsSleeping() && !stopped && dropped) {
			objectGenerator.GetComponent<CreateObject> ().staticObject ();
			stopped = true;
		}

		// 1度止まったのにまた動き出したらそれを報告
		if (!body.IsSleeping() && stopped && dropped) {
			objectGenerator.GetComponent<CreateObject> ().movingObject ();
			stopped = false;
		}
	}
		
	public void droppedReceiver(){
		stopped = false;
	}
}