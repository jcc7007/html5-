using UnityEngine;
using System.Collections;

public class BossSelectButton : MonoBehaviour {
    public UILabel BossIdLable;//将显示BossId的标签关联起来，方便修改实时数值
    private Vector3 BossNamePosition = new Vector3(0, 3.46f, -0.1f);//调整boss名字预设的实例化位置
    private Vector3 BossPosition = new Vector3(0,1.52f,-0.1f);//记录Boss动画实例化的位置
    private GameObject preBossName;//记录前一个BossName对象，针对右按钮
    private GameObject nexBossName;//记录后一个BossName对象，针对左按钮
    private GameObject defaultBossName;//默认BossName对象，针对第一个
    private GameObject preBoss;//记录前一个Boss模型对象
    private GameObject nexBoss;//记录后一个Boss模型对象
    private GameObject defaultBoss;//默认Boss模型对象，针对第一个
    public GameObject[] BossNamePrefab;//Boss名字的预设
    public GameObject[] BossPrefab;//Boss模型的预设
	// Use this for initialization
	void Start () {
        GameState.BossId = 1;//每次跳向Boss选择界面，默认显示的Boss是第一个
        defaultBossName = (GameObject)Instantiate(BossNamePrefab[GameState.BossId - 1],BossNamePosition , Quaternion.identity);//初始化的时候默认实例化第一个
        preBossName = defaultBossName;
        nexBossName = defaultBossName;
        //将前后指针都指向第一个默认的
        defaultBoss = (GameObject)Instantiate(BossPrefab[GameState.BossId - 1], BossPosition, Quaternion.identity);//初始化的时候默认实例化第一个
        preBoss = defaultBoss;
        nexBoss = defaultBoss;
	}
	
	// Update is called once per frame
	void Update () {
	   BossIdLable.text = GameState.BossId.ToString();//实时显示当前BossId号
       
	}
    void RightButton() { //右边按钮的响应函数
        GameState.BossId ++;//每次点击将BossId加1
        Destroy(nexBossName);
        Destroy(nexBoss);
        if (GameState.BossId >= 12)//BossId不能超过12，等于12的时候特殊处理
        {
            GameState.BossId = 12;
            Destroy(preBossName);//销毁前一个BossName预设，虽然每次都要实例化和销毁，但还是可以实现效果
            preBossName = (GameObject)Instantiate(BossNamePrefab[11], BossNamePosition, Quaternion.identity);
            Destroy(preBoss);//销毁前一个Boss模型预设
            preBoss = (GameObject)Instantiate(BossPrefab[11], BossPosition, Quaternion.identity);
            return ;
        }
        Destroy(preBoss);//销毁前一个Boss模型预设
        preBoss =(GameObject)Instantiate(BossPrefab[GameState.BossId - 1], BossPosition, Quaternion.identity);//实例化Boss对象
        Destroy(preBossName);//销毁前一个预设
        preBossName = (GameObject)Instantiate(BossNamePrefab[GameState.BossId - 1], BossNamePosition, Quaternion.identity);//实例化名字预设
    }
    void LeftButton() { //右边按钮的响应函数
        GameState.BossId--;//每次点击将BossId减1
        Destroy(preBossName);
        Destroy(preBoss);
        if (GameState.BossId <= 1)//BossId不能小于1
        {
            GameState.BossId = 1;
            Destroy(nexBossName);//销毁后一个预设，虽然每次都要实例化和销毁，但还是可以实现效果
            nexBossName = (GameObject)Instantiate(BossNamePrefab[0], BossNamePosition, Quaternion.identity);
            Destroy(nexBoss);
            nexBoss = (GameObject)Instantiate(BossPrefab[0], BossPosition, Quaternion.identity);//实例化名字预设
            return;
        }
        Destroy(nexBoss);
        nexBoss = (GameObject)Instantiate(BossPrefab[GameState.BossId - 1], BossPosition, Quaternion.identity);//实例化名字预设
        Destroy(nexBossName);//销毁后一个预设，虽然每次都要实例化和销毁，但还是可以实现效果
        nexBossName = (GameObject)Instantiate(BossNamePrefab[GameState.BossId - 1], BossNamePosition, Quaternion.identity);
    }

    void jumpPlaneReady() { //出击按钮的响应函数
        StartCoroutine(startLevel());//为了让按钮的声音播放出来，延迟一下再加载下一个场景 
    }
    IEnumerator startLevel()
    {//延迟函数
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(5);//跳向战机准备界面
        GameState.ReadyBackScene = false;//false表示从Boss选择跳转而来
    }
    void backMainMenu()//返回主菜单界面的按钮响应函数
    {
        StartCoroutine(backLevel());//为了让按钮的声音播放出来，延迟一下再加载下一个场景 
    }
    IEnumerator backLevel()
    {//延迟函数
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(0);//跳向主菜单界面
    }
}
