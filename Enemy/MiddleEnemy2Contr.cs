using UnityEngine;
using System.Collections;

public class MiddleEnemy2Contr : MonoBehaviour
{
    //此中型战机出来，在固定位置上释放子弹，速度稍微快一点
    public float Speed = 1.0f;//定义移动的速度
    private Transform mTransform;
    public GameObject MiddleExplodePrefab;
    public GameObject ShuiJingPrefab;
    // Use this for initialization
    public int MiddleHealth;//获取当前关卡的中型飞机的生命值
    private GameObject getPlayer;//获取Player的位置
    public GameObject EnemyBullet4;//获取中级子弹的预设
    public float mTime;//定义变量用来比较
    public bool shoot = true;//射击的标记
    private float mTime2;//用来标记战机停留的时间
    void Start()
    {
        mTransform = transform;
        MiddleHealth = GameState.MiddleEnemyHealth[GameState.MissionId - 1];//获取对应关卡的生命值
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" || other.tag == "LiaoJiBullet")
        {
            healthDamage(1);//受到伤害为1
        }
    }
    void healthDamage(int damage)
    { //机体受到伤害的计算
        MiddleHealth -= damage;
        if (MiddleHealth <= 0)
        { //如果生命值小于0，机体爆炸，游戏结束，延迟一段时间再跳向失败界面
            Explode();//调用爆炸函数
        }
    }
    void Explode()
    {
        Destroy(gameObject);
        var explode = Instantiate(MiddleExplodePrefab, mTransform.position, Quaternion.identity);//实例化爆炸
        Destroy(explode, 0.3f);//延迟销毁爆炸物体
        GameState.CurrentScore += 200;//获得分数是200，因为是中型飞机
        GameState.KillEnemyNumber += 1;//击杀敌机数量加1
        Instantiate(ShuiJingPrefab, mTransform.position + new Vector3(-0.3f, 0, -0.1f), Quaternion.identity);//中型飞机实例化2个水晶
        Instantiate(ShuiJingPrefab, mTransform.position + new Vector3(0.3f, 0, -0.1f), Quaternion.identity);//实例化水晶
    }
    // Update is called once per frame
    void Update()
    {
        mTime2 += Time.deltaTime;
        if (GameState.IsPause == false)
        {//当不处于暂停状态才发射子弹
            if (mTransform.position.y > 2.45f)//先出现在屏幕范围之后在攻击
            {
                mTransform.Translate(Vector3.down * Speed * Time.deltaTime);
            }
            else if (mTransform.position.y<=2.45f&& mTime2 < 5.0f)//在固定的位置上射击5秒钟。如果没有死继续往下飞
            {
                if (shoot)
                {
                    var parent = (GameObject)Instantiate(EnemyBullet4, mTransform.position + new Vector3(0.68f, 0.3f, -0.1f), Quaternion.identity);//实例化子弹的位置
                    parent.transform.DetachChildren();//由于子弹预设父物体都是空物体，所以要先消除其父物体
                    Destroy(parent);
                    mTime = 0;
                    shoot = false;//发射之后置为假
                }
                mTime += Time.deltaTime;
                if (mTime > 1.2f)//发射的时间间隔
                    shoot = true;
            }
            else
            {
                mTransform.Translate(Vector3.down * (Speed/2) * Time.deltaTime);
                if (mTransform.position.y < -4.6f)
                {//超出屏幕范围，销毁
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            return;
        }
    }
}
