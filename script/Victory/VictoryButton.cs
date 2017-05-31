using UnityEngine;
using System.Collections;

public class VictoryButton : MonoBehaviour {
    public UILabel KillEnemyLable;//分别关联杀敌，水晶和得分标签
    public UILabel ShuiJingLable;
    public UILabel ScoreLable;
	// Use this for initialization
    void Start()
    {//由于只需要显示一次，所以只需要初始化即可
        KillEnemyLable.text = GameState.KillEnemyNumber.ToString();
        ShuiJingLable.text = GameState.shuijingNumber.ToString();
        ScoreLable.text = GameState.CurrentScore.ToString();
        //只有是任务模式时，才将得分存入数组，挑战模式不存入，存入也不是不可以，不可能分数更高好像
            if (GameState.CurrentScore > GameState.MissionScore[GameState.MissionId - 1])//如果大于以前的成绩，则存入新的成绩
            {
                GameState.MissionScore[GameState.MissionId - 1] = GameState.CurrentScore;//将当前关卡的得分存进数组
                GameState.Save();
            }
            else
            {
                GameState.Save();
                return;
            }
        }
	// Update is called once per frame
	void Update () {
        
	}
    void BackButton() {//返回按钮的响应函数
        StartCoroutine(BackMain());
    }
    IEnumerator BackMain()
    {//延迟函数
        yield return new WaitForSeconds(0.5f);//延迟时间
        Application.LoadLevel(0);//跳回主菜单界面
    }
}
