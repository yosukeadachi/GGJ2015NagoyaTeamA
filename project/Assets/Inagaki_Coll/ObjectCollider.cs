using UnityEngine;
using System.Collections;

public class ObjectCollider : MonoBehaviour {

    [SerializeField] private int   m_power = 5;   //強さ
    [SerializeField] private float m_AttackRange  = 0.5f; //攻撃判定の半径
    [SerializeField] private float m_DefenseRange = 0.5f; //受け判定の半径
    

    private Renderer m_ChildRend; //子のレンダラー


    public Vector2 setPos{ set{ transform.position = value; } }
    public int   getPower{ get{ return m_power;  } }  
    public int   setPower{ set{ m_power = value; } }

    public float getAttackRange { get{ return m_AttackRange;  } }  
    public float getDefenseRange{ get{ return m_DefenseRange; } }  

	void Start () {
	    m_ChildRend = transform.FindChild("Model").renderer;
	}
	
	void Update () {
	
	}

    //勝利したときの処理
    public void BattleWin() {
        m_ChildRend.material.color = Color.yellow;
    }
    
    //負けたときの処理
    public void BattleLoss() {
        m_ChildRend.material.color = Color.magenta;
    }
    //相子のとき
    public void BattleDraw() {
        m_ChildRend.material.color = Color.gray;
    }


    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_DefenseRange);
    
    }
}
