using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    GameObject a, b;

	void Start () {
	    a = GameObject.Find("ObjectMrg");
	}
	
	void Update () {
	    if(Input.GetKeyDown(KeyCode.A)) {
            a.GetComponent<ObjectMrg>().Judge();
            Debug.Log("Judge----------------------");
        }
        if(Input.GetKeyDown(KeyCode.S)) {
            a.GetComponent<ObjectMrg>().Clear();
            Debug.Log("Clear----------------------");
        }

	}
}
