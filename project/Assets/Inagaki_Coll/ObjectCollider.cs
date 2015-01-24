using UnityEngine;
using System.Collections;

public class ObjectCollider : MonoBehaviour {

    [SerializeField]
    private int m_power = 5;

    private Renderer m_ChildRend;


    public Vector2 setPos{ set{ transform.position = value; } }
    public int getPower{ get{ return m_power;  } }  
    public int setPower{ set{ m_power = value; } }  

	void Start () {
	    m_ChildRend = transform.FindChild("Model").renderer;
	}
	
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D c) {
        //強さの判定
        int val = c.GetComponent<ObjectCollider>().getPower - m_power;
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -勝ち 
        if(val > 0) {
            Debug.Log(""+gameObject.name+":かち");
            m_ChildRend.material.color = Color.yellow;
            return;
        }
        
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -まけ 
        if(0 > val) {
            Debug.Log(""+gameObject.name+":まけ");
            m_ChildRend.material.color = Color.magenta;
            return;
        }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -あいこ
        if(val == 0){
            Debug.Log(""+gameObject.name+":あいこ");
            m_ChildRend.material.color = Color.grey;
            return;
        }
    }
}
