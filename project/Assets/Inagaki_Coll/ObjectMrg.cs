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

    public void Judge() {
        //距離を図る
        for(int i=0; i < SHIP_MAX; i++) {
            for(int f=0; f < SHIP_MAX; f++) {
                if(i==f) continue; //同じものは処理しない
                ObjectCollider ocI = ships[i].GetComponent<ObjectCollider>();
                ObjectCollider ocF = ships[f].GetComponent<ObjectCollider>();
                if(!ocI.isEnabled || !ocF.isEnabled) continue; //使用されていないものは処理しない

                Debug.Log("["+i+","+f+"]:------------------------------------------------");
                //距離
                Vector3 vec = ships[f].position - ships[i].position;
                float dis_sqrt = (vec.x * vec.x + vec.y * vec.y + vec.z * vec.z); //処理(sqrt)
                if(dis_sqrt  < (ocI.getAttackRange + ocF.getDefenseRange) * (ocI.getAttackRange + ocF.getDefenseRange)) {
                    //勝敗判定
                    int val = ocF.getPower - ocI.getPower;
                    Debug.Log("["+i+","+f+"]:hit i:"+ocI.getPower+" f;"+ocF.getPower+"\n " + val);
                    Debug.Log("["+i+","+f+"]:vec:" + vec);
                    Debug.Log("["+i+","+f+"]:dir:" + dis_sqrt + " < " + ((ocI.getAttackRange + ocF.getDefenseRange) * (ocI.getAttackRange + ocF.getDefenseRange)));
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -勝ち 
                    if(val < 0) {
                        Debug.Log("Win: " +  ocI.name);
                        ocI.BattleWin();
                        ocF.BattleLoss();
                        return;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -まけ 
                    if(0 < val) {
                        Debug.Log("Miss: " +  ocI.name);
                    }
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -あいこ
                    if(val == 0) {
                        Debug.Log("DrawGame: ");
                        ocI.BattleDraw();
                        ocF.BattleDraw();
                        return;
                    }
                }
            }
        }

    }// end function Judge

    public void Clear() {
        for(int i=0; i < SHIP_MAX; i++) {
            ships[i].GetComponent<ObjectCollider>().Clear();
        }
    }
}
