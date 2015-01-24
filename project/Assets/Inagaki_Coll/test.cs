using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    GameObject a, b;

	void Start () {
	    a = GameObject.Find("A");
	    b = GameObject.Find("B");
	}
	
	void Update () {
	    if(Input.GetKeyDown(KeyCode.A)) {
            a.GetComponent<ObjectCollider>().setPos = new Vector2(0,1);
            b.GetComponent<ObjectCollider>().setPos = new Vector2(0,0);
            Debug.Log("----------------------");
        }
        if(Input.GetKeyDown(KeyCode.S)) {
            a.GetComponent<ObjectCollider>().setPos = new Vector2(0,5);
            b.GetComponent<ObjectCollider>().setPos = new Vector2(0,0);
            Debug.Log("----------------------");
        }

	}
}
