using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {
    public GameObject[] EnemyPrefabs;//关联敌机的预设
    public GameObject[] BossPrefabs;//关联Boss的预设
    private Transform mTransform;
    public float GenerationCool = 1.0f;//设置自动生成器的冷却时间
    public float mTime = 0f;//时间标记函数
    private GameObject ret;
    private  float TimeJianshao;//随着时间的流逝，生成速度会加快
    private bool Once = true;//只生成Boss一次
    public GameObject WaringPrefab;//关联警告的预设
    private bool waringOnce = true;//只生成一次预警
    private GameObject waring;//用于关联实例生成的变量
    private GameObject Bossname;//用于关联实例生成的变量
    private float warnTime;//关联提示时间的变量
    public GameObject[] Bossnameprefab;//Boss名字预设
	// Use this for initialization
	void Start () {
        mTransform = transform;
        //GameState.BossId = GameState.MissionId;//保证实例化的boss和关卡一致
	}
	
	// Update is called once per frame
    void Update()
    {
        if (GameState.IsPause == false)
        {
            TimeJianshao += Time.deltaTime;//记住时间的流逝
            GenerationCool = 1.0f - TimeJianshao * 0.007f;
            if (TimeJianshao <= 2) { //刚刚生成战机给2秒钟适应时间嘛
                
            }
            else if (TimeJianshao>2&& TimeJianshao <= 90)//小于90秒的时候是普通战机实例化
            {
                if (GenerationCool < mTime)
                {
                    mTime = 0;
                    //通过判定不同的4种关卡，来分别实例化不同的战机
                    if (GameState.MissionId <= 4)
                    {
                        generatorEnemy();
                    }
                    else if (GameState.MissionId > 4 && GameState.MissionId <= 8)
                    {
                        generatorEnemy1();
                    }
                    else {
                        generatorEnemy2();
                    }
                }
                else
                {
                    mTime += Time.deltaTime;
                }
            }
            else if (TimeJianshao >90 && TimeJianshao <= 98) { //播放提醒动画
                if (waringOnce == true)
                {
                    waring = (GameObject)Instantiate(WaringPrefab, mTransform.position + new Vector3(0, -5.0f, 0), Quaternion.identity);
                    Bossname = (GameObject)Instantiate(Bossnameprefab[GameState.MissionId - 1], mTransform.position + new Vector3(0, -2.0f, -0.1f), Quaternion.identity);//实例化boss名字
                    waringOnce = false;//只实例化一次警告预设
                }
                warnTime += Time.deltaTime;//实例化之后再过4秒钟之后销毁
                if (warnTime >= 6.0f) {
                    Destroy(waring);
                    Destroy(Bossname);
                }
            }
            else {//当大于100秒的时候就是Boss出场了
                GameState.BossChuxian = true;//boss出现为真
                if (Once == true) {
                    Instantiate(BossPrefabs[GameState.MissionId - 1], mTransform.position , Quaternion.identity);//在这里应该用任务Id，避免上次打过boss，id没有变化
                    Once = false;//然后将Once置为假，只实例化一次Boss
                }
            }
        }
        else
        {
            return;
        }
    }
    //前四种关卡的生成函数
    void generatorEnemy() { //用来控制敌机的生成
        var prefab = getRandomPrefab();
        var randomx = Random.Range(-2.03f, 1.97f);//获取随机的X位置
        Instantiate(prefab, new Vector3(randomx, mTransform.position.y, mTransform.position.z), Quaternion.identity);//实例化敌机
    }
    public GameObject getRandomPrefab() { //获取随机的预设
        var index1 = Random.Range(0, 100);//用概率来控制每种飞机的生成
        if (index1 >= 0 && index1 <= 50)//前三种小飞机的生成概率是50%
        {
            var index = Random.Range(0, 3);//随机数包含前面不包含后面数
            ret = EnemyPrefabs[index];
        }
        else if (index1 > 50 && index1 <= 75)//三种中型飞机的概率是25%
        {
            var index = Random.Range(3, 6);
            ret = EnemyPrefabs[index];
        }
        else {//最后三种也是25%
            var index = Random.Range(6, 9);
            ret = EnemyPrefabs[index];
        }
        return ret;
    }
    //中间四种关卡的生成函数，中型战机生成概率加大
    void generatorEnemy1()
    { //用来控制敌机的生成
        var prefab = getRandomPrefab1();
        var randomx = Random.Range(-2.03f, 1.97f);//获取随机的X位置
        Instantiate(prefab, new Vector3(randomx, mTransform.position.y, mTransform.position.z), Quaternion.identity);//实例化敌机
    }
    public GameObject getRandomPrefab1()
    { //获取随机的预设
        var index1 = Random.Range(0, 100);//用概率来控制每种飞机的生成
        if (index1 >= 0 && index1 <= 40)//前三种小飞机的生成概率是40%
        {
            var index = Random.Range(9, 12);
            ret = EnemyPrefabs[index];
        }
        else if (index1 > 40 && index1 <= 70)//三种中型飞机的概率是30%
        {
            var index = Random.Range(12, 15);
            ret = EnemyPrefabs[index];
        }
        else
        {//最后三种也是30%
            var index = Random.Range(15, 18);
            ret = EnemyPrefabs[index];
        }
        return ret;
    }

    //后面四种关卡的生成函数，中型战机生成概率更大
    void generatorEnemy2()
    { //用来控制敌机的生成
        var prefab = getRandomPrefab2();
        var randomx = Random.Range(-2.03f, 1.97f);//获取随机的X位置
        Instantiate(prefab, new Vector3(randomx, mTransform.position.y, mTransform.position.z), Quaternion.identity);//实例化敌机
    }
    public GameObject getRandomPrefab2()
    { //获取随机的预设
        var index1 = Random.Range(0, 100);//用概率来控制每种飞机的生成
        if (index1 >= 0 && index1 <= 30)//前三种小飞机的生成概率是30%
        {
            var index = Random.Range(18, 21);
            ret = EnemyPrefabs[index];
        }
        else if (index1 > 30 && index1 <= 65)//三种中型飞机的概率是35%
        {
            var index = Random.Range(21, 24);
            ret = EnemyPrefabs[index];
        }
        else
        {//最后三种也是35%
            var index = Random.Range(24, 27);
            ret = EnemyPrefabs[index];
        }
        return ret;
    }
}
