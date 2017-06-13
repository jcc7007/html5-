using UnityEngine;
using System.Collections;

public class MiddleEnemyContr1 : MonoBehaviour
{
    //此中型战机只是左右移动，释放子弹
    public float Speed = 1.0f;//定义移动的速度
    private Transform mTransform;
    private int right = 1;// 左右移动的标记
    public GameObject MiddleExplodePrefab;
    public GameObject ShuiJingPrefab;
    // Use this for initialization
    public int MiddleHealth;//获取当前关卡的中型飞机的生命值
    private GameObject getPlayer;//获取Player的位置
    public GameObject EnemyBullet4;//获取中级子弹的预设
    public float mTime;//定义变量用来比较
    private bool shoot = true;//定义是否射击
    private float angle;//定义获取的角度变量
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
       if (GameState.IsPause == false){
            //当不处于暂停状态才瞄准
        if (mTransform.position.y > 2.85f)//先出现在屏幕范围之后在攻击
        {
            mTransform.Translate(Vector3.down * Speed * Time.deltaTime);
        }
        else
        {
            if (GameState.IsLive == true)//当飞机存活时才实时获取飞机的位置，并瞄准
            {
                getPlayer = GameObject.FindGameObjectWithTag("RealPlayer1");//根据标签找到战机1
                var myPosition = getPlayer.transform;
                    if (mTransform.position.y - myPosition.position.y < 0)
                    {//当敌机处于玩家下方不能发射子弹
                        shoot = false;
                    }
                    if (shoot && mTransform.position.y < 4.0f)//如果敌机出现在屏幕范围，射击
                    {
                        angle = Mathf.Rad2Deg * Mathf.Atan((transform.position.y - myPosition.position.y) / (transform.position.x - myPosition.position.x));//获取角度值

                        if (angle < 0)
                            angle = 90 - Mathf.Abs(angle);
                        else
                            angle = -90 + Mathf.Abs(angle);//获取正确的发射角度
                        var rotation = Quaternion.identity;
                        rotation.eulerAngles = new Vector3(0, 0, angle);//将物体扭转成发射角度
                        var parent = (GameObject)Instantiate(EnemyBullet4, mTransform.position + new Vector3(0.6f, 0.6f, -0.1f), rotation);//实例化子弹的位置
                        parent.transform.DetachChildren();//由于子弹预设父物体都是空物体，所以要先消除其父物体
                        Destroy(parent);
                        mTime = 0;
                        shoot = false;//发射之后置为假
                    }
                    mTime += Time.deltaTime;
                    if (mTime > 2.0f)//发射的时间间隔
                        shoot = true;
                }

            }
    }
            else{
                return;
                   }

            if (GameState.IsPause == false)
            {
                if (mTransform.position.x <= -2.1f)//用来控制小飞机的左右移动，并向下移动
                {
                    right = 1;
                }
                else if (mTransform.position.x >= 2.1f)
                {
                    right = 2;
                }
                if (right == 1)
                {
                    mTransform.Translate(new Vector3(1, 0, 0) * Speed * Time.deltaTime);
                }
                else if (right == 2)
                {
                    mTransform.Translate(new Vector3(-1, 0, 0) * Speed * Time.deltaTime);
                }
            }
        }
    }

