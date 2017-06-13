using UnityEngine;
using System.Collections;

public class BossModeGene : MonoBehaviour {
    public GameObject[] BossPrefabs;//关联Boss的预设
    private Transform mTransform;
    private bool Once = true;//只生成Boss一次
    public GameObject WaringPrefab;//关联警告的预设
    private bool waringOnce = true;//只生成一次预警
    private GameObject waring;//用于关联实例生成的变量
    private GameObject Bossname;//用于关联实例生成的变量
    private float warnTime;//关联提示时间的变量
    public GameObject[] Bossnameprefab;//Boss名字预设
    private float TimeJianshao;//随着时间的流逝，关联时间
	// Use this for initialization
	void Start () {
        mTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.IsPause == false)
        {   
            //前三秒稍微准备时间
            TimeJianshao += Time.deltaTime;//记住时间的流逝
            if (TimeJianshao> 2&& TimeJianshao <= 8) { //播放提醒动画
                if (waringOnce == true)
                {
                    waring = (GameObject)Instantiate(WaringPrefab, mTransform.position + new Vector3(0, -5.0f, 0), Quaternion.identity);
                    Bossname = (GameObject)Instantiate(Bossnameprefab[GameState.BossId - 1], mTransform.position + new Vector3(0, -2.0f, 0), Quaternion.identity);//实例化boss名字
                    waringOnce = false;//只实例化一次警告预设
                }
                warnTime += Time.deltaTime;//实例化之后再过6秒钟之后销毁
                if (warnTime >= 5.0f) {
                    Destroy(waring);
                    Destroy(Bossname);
                }
            }
            else if(TimeJianshao>8){//当大于8秒的时候就是Boss出场了
                GameState.BossChuxian = true;//boss出现为真
                if (Once == true) {
                    Instantiate(BossPrefabs[GameState.BossId - 1], mTransform.position, Quaternion.identity);
                    Once = false;//然后将Once置为假，只实例化一次Boss
                }
            }
        }
        else
        {
            return;
        }
	
	}
}
