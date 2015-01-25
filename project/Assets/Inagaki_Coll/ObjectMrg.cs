using UnityEngine;
using System.Collections;

public class ObjectMrg : MonoBehaviour {

    [SerializeField] private int SHIP_MAX = 6;

    private Transform [] ships;

	void Start () {
	    ships = new Transform[SHIP_MAX];
	    for(int i=0; i < SHIP_MAX; i++) {
            ships[i] = GameObject.Find("Ship" + i ).transform;
            if(ships[i] == null) {
                Debug.LogError("▲Error ObjectMrg:オブジェクトが取得できませんでした。");
            }
        }
    
    }
	
	void Update () {
	    
	}

    //ジャッジ=====================================================================================
    public void Judge() {
        //距離を図る
        for(int i=0; i < SHIP_MAX; i++) {
            for(int f=0; f < SHIP_MAX; f++) {
                if(i==f) continue; //同じものは処理しない
                ObjectCollider ocI = ships[i].GetComponent<ObjectCollider>();
                ObjectCollider ocF = ships[f].GetComponent<ObjectCollider>();
                if(!ocI.isEnabled || !ocF.isEnabled) continue; //使用されていないものは処理しない

                //距離
                Vector3 vec = ships[f].position - ships[i].position;
                float dis_sqrt = (vec.x * vec.x + vec.y * vec.y + vec.z * vec.z); //処理(sqrt)

                //逆転判定
                if(dis_sqrt < (ocI.getReverseRange + ocF.getDefenseRange) * (ocI.getReverseRange + ocF.getDefenseRange)) {
                    int val = ocF.getPower - ocI.getPower;
                    Debug.Log("["+i+","+f+"]:ReverseHit");
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Iが逆転勝ち 
                    if((ocI.getPower % 3) == ((ocF.getPower + 1) % 3)) {
                        Debug.Log("Win: " +  ocI.name);
                        ocI.BattleWin();
                        ocF.BattleLoss();
                        return;
                    }
                }

                //正規勝敗判定
                if(dis_sqrt < (ocI.getAttackRange + ocF.getDefenseRange) * (ocI.getAttackRange + ocF.getDefenseRange)) {
                    int val = ocF.getPower - ocI.getPower;
                    Debug.Log("["+i+","+f+"]:Hit");
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -Iがまけ 
                    if((ocI.getPower % 3) == ((ocF.getPower + 1) % 3)) {
                        Debug.Log("Win: " +  ocF.name);
                        ocI.BattleLoss();
                        ocF.BattleWin();
                        return;
                    }
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -あいこ
                    if((ocI.getPower % 3) == ocF.getPower) {
                        Debug.Log("DrawGame: ");
                        ocI.BattleDraw();
                        ocF.BattleDraw();
                        return;
                    }
                }
            }
        }

    }// end function Judge
    
    //クリア=======================================================================================
    public void Clear() {
        for(int i=0; i < SHIP_MAX; i++) {
            ships[i].GetComponent<ObjectCollider>().Clear();
        }
    }
}
