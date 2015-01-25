using UnityEngine;
using System.Collections;

public class ObjectCollider : MonoBehaviour {

    //power 0=グー : 1=チョキ : 2=パー
    [SerializeField] private int   m_power = 5;   //強さ
    [SerializeField] private float m_AttackRange   = 2.0f; //攻撃判定の半径
    [SerializeField] private float m_ReverseRange  = 0.8f; //逆転判定の半径
    [SerializeField] private float m_DefenseRange  = 0.5f; //受け判定の半径
    
    private Renderer m_ChildRend; //子のレンダラー

    //子のパーティクル
    private ParticleSystem m_ChildPar_Att;
    private ParticleSystem m_ChildPar_Def;
    private ParticleSystem m_ChildPar_Win;
    private ParticleSystem m_ChildPar_Los;
    private ParticleSystem m_ChildPar_RevWin;
    private ParticleSystem m_ChildPar_RevLos;
    private ParticleSystem m_ChildPar_Draw;


    //攻撃、防御パーティクル制御
    const float ROT = 0.05f;

    public Vector2 setPos{ set{ transform.position = value; } }
    public int     getPower{ get{ return m_power;  } }  
    public int     setPower{ set{ m_power = value; } }

    public float getAttackRange  { get{ return m_AttackRange;   } }  
    public float getReverseRange { get{ return m_ReverseRange;  } }  
    public float getDefenseRange { get{ return m_DefenseRange;  } }

    public bool  isEnabled{ get{ return m_ChildRend.enabled; } }

	void Start () {
	    m_ChildRend  = transform.FindChild("Model").renderer;

        m_ChildPar_Att    = transform.FindChild("Particle_Attack"  ).particleSystem;
        m_ChildPar_Def    = transform.FindChild("Particle_Defense" ).particleSystem;
        m_ChildPar_Win    = transform.FindChild("Particle_Win"     ).particleSystem;
        m_ChildPar_Los    = transform.FindChild("Particle_Loss"    ).particleSystem;
        m_ChildPar_RevWin = transform.FindChild("Particle_ComebackWin" ).particleSystem;
        m_ChildPar_RevLos = transform.FindChild("Particle_ComebackLoss").particleSystem;
        m_ChildPar_Draw   = transform.FindChild("Particle_Draw"        ).particleSystem;
        //パーティクルの初期設定
        m_ChildPar_Att.Play();
        m_ChildPar_Def.Play();
        m_ChildPar_Att.transform.localPosition = Vector3.left;
        m_ChildPar_Def.transform.localPosition = Vector3.left;

        m_ChildPar_Win.Stop();
        m_ChildPar_Los.Stop();
        m_ChildPar_RevWin.Stop();
        m_ChildPar_RevLos.Stop();
        m_ChildPar_Draw.Stop();
	}
	
	void Update () {
        //攻撃、防御パーティクル制御
        Vector3    ap;
        Quaternion sq, eq;
        Quaternion q  = new Quaternion(0, Mathf.Sin(ROT/2)     , 0, Mathf.Cos(ROT/2));
        Quaternion r  = new Quaternion(0, Mathf.Sin(ROT/2) * -1, 0, Mathf.Cos(ROT/2));

        //攻撃パーティクル制御
        ap = m_ChildPar_Att.transform.localPosition;
        sq = new Quaternion(ap.x, ap.y, ap.z, 0);
        eq = r * sq * q;
        m_ChildPar_Att.transform.localPosition = new Vector3(eq.x, eq.y, eq.z).normalized * m_AttackRange;

        //受けパーティクル制御
        ap = m_ChildPar_Def.transform.localPosition;
        sq = new Quaternion(ap.x, ap.y, ap.z, 0);
        eq = q * sq * r; //回転軸を逆さましている
        m_ChildPar_Def.transform.localPosition = new Vector3(eq.x, eq.y, eq.z).normalized * m_DefenseRange;

	}

    //勝利したときの処理
    public void BattleWin() {
        m_ChildPar_Att.Stop();
        m_ChildPar_Def.Stop();
        m_ChildPar_Win.Play();
        m_ChildPar_Los.Stop();
    }
    
    //負けたときの処理
    public void BattleLoss() {
        m_ChildPar_Att.Stop();
        m_ChildPar_Def.Stop();
        m_ChildPar_Win.Stop();
        m_ChildPar_Los.Play();
    }
    //相子のとき
    public void BattleDraw() {
        m_ChildPar_Att.Stop();
        m_ChildPar_Def.Stop();
        m_ChildPar_Draw.Play();
    }

    //逆転勝利
    public void BattleReverseWin() {
        m_ChildPar_Att.Stop();
        m_ChildPar_Def.Stop();
        m_ChildPar_RevWin.Play();
        m_ChildPar_RevLos.Stop();
    }

    //逆転負け
    public void BattleReverseLoss() {
        m_ChildPar_Att.Stop();
        m_ChildPar_Def.Stop();
        m_ChildPar_RevWin.Stop();
        m_ChildPar_RevLos.Play();
    }

    //初期状態へ戻す
    public void Clear() {
        m_ChildPar_Att.Play();
        m_ChildPar_Def.Play();
        m_ChildPar_Win.Stop();
        m_ChildPar_Los.Stop();
        m_ChildPar_RevWin.Stop();
        m_ChildPar_RevLos.Stop();
        m_ChildPar_Draw.Stop();
    }

    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, m_ReverseRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_DefenseRange);
    
    }
}
