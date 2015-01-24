using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void judge() {
		_hideJudgeUI();
	}

	void _hideJudgeUI(){
		GameObject.Find("ButtonJudgeOK").SetActive(false);
		GameObject.Find("ImageJudgeLogo").SetActive(false);
		GameObject.Find("ImageCornerLeftTop").SetActive(false);
	}
}
