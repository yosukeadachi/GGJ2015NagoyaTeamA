using UnityEngine;
using System.Collections;

public class ObjectCollider : MonoBehaviour {

    [SerializeField] private int   m_power = 5;   //強さ
    [SerializeField] private float m_AttackRange  = 0.5f; //攻撃判定の半径
    [SerializeField] private float m_DefenseRange = 0.5f; //受け判定の半径
    

    private Color    M_FAST_COLOR;
    private Renderer m_ChildRend; //子のレンダラー

    //子のパーティクル
    private ParticleSystem m_ChildPar_Att;
    private ParticleSystem m_ChildPar_Def;
    private ParticleSystem m_ChildPar_Win;
    private ParticleSystem m_ChildPar_Los;


    public Vector2 setPos{ set{ transform.position = value; } }
    public int   getPower{ get{ return m_power;  } }  
    public int   setPower{ set{ m_power = value; } }

    public float getAttackRange { get{ return m_AttackRange;  } }  
    public float getDefenseRange{ get{ return m_DefenseRange; } }

    public bool  isEnabled{ get{ return m_ChildRend.enabled; } }

	void Start () {
	    m_ChildRend  = transform.FindChild("Model").renderer;
        //M_FAST_COLOR = m_ChildRend.material.color;

        m_ChildPar_Att = transform.FindChild("Particle_Attack"  ).particleSystem;
        m_ChildPar_Def = transform.FindChild("Particle_Defense" ).particleSystem;
        m_ChildPar_Win = transform.FindChild("Particle_Win"     ).particleSystem;
        m_ChildPar_Los = transform.FindChild("Particle_Loss"    ).particleSystem;

        //パーティクルの初期設定
        m_ChildPar_Att.Play();
        m_ChildPar_Def.Play();
        m_ChildPar_Att.transform.localScale = Vector3.one * m_AttackRange  * 2;
        m_ChildPar_Def.transform.localScale = Vector3.one * m_DefenseRange * 2;

        m_ChildPar_Win.Stop();
        m_ChildPar_Los.Stop();

	}
	
	void Update () {

	}

    //勝利したときの処理
    public void BattleWin() {
        //m_ChildRend.material.color = Color.yellow;

        m_ChildPar_Att.Stop();
        m_ChildPar_Def.Stop();
        m_ChildPar_Win.Play();
        m_ChildPar_Los.Stop();
    }
    
    //負けたときの処理
    public void BattleLoss() {
        //m_ChildRend.material.color = Color.magenta;
        
        m_ChildPar_Att.Stop();
        m_ChildPar_Def.Stop();
        m_ChildPar_Win.Stop();
        m_ChildPar_Los.Play();
    }
    //相子のとき
    public void BattleDraw() {
        //m_ChildRend.material.color = Color.gray;
        
        m_ChildPar_Att.Stop();
        m_ChildPar_Def.Stop();
    }

    //初期状態へ戻す
    public void Clear() {
        //m_ChildRend.material.color = M_FAST_COLOR;
        
        m_ChildPar_Att.Play();
        m_ChildPar_Def.Play();
        m_ChildPar_Win.Stop();
        m_ChildPar_Los.Stop();
    }

    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_DefenseRange);
    
    }
}
