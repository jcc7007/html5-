sing UnityEngine;
using System.Collections;

public class FailButton : MonoBehaviour {
   //在失败界面只关联总水晶标签，其他都是默认为0
    public UILabel ShuiJingLable;
	// Use this for initialization
	void Start () {
        ShuiJingLable.text = GameState.shuijingNumber.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void BackButton()
    {//返回按钮的响应函数
        StartCoroutine(BackMain());
    }
    IEnumerator BackMain()
    {//延迟函数
        yield return new WaitForSeconds(0.5f);//延迟时间
        Application.LoadLevel(0);//跳回主菜单界面
    }
}
