using UnityEngine;
using System.Collections;

public class SmallEnemyController : MonoBehaviour
{
    public float Speed = 1.0f;//定义移动的速度
    private Transform mTransform;
    private int right= 1;// 左右移动的标记
    public GameObject SmallExplodePrefab;
    public GameObject ShuiJingPrefab;
    // Use this for initialization
    public int SmallHealth;//获取当前关卡的小飞机的生命值
    void Start()
    {
        mTransform = transform;
        SmallHealth = GameState.SmallEnemyHealth[GameState.MissionId - 1];//获取对应关卡的生命值
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "PlayerBullet" || other.tag == "LiaoJiBullet") {
            healthDamage(1);//受到伤害为1
        }
    }
    void healthDamage(int damage)
    { //机体受到伤害的计算
       SmallHealth -= damage;
        if (SmallHealth <= 0)
        { //如果生命值小于0，机体爆炸，游戏结束，延迟一段时间再跳向失败界面
            Explode();//调用爆炸函数
        }
    }
    void Explode() {
        Destroy(gameObject);
        var explode = Instantiate(SmallExplodePrefab, mTransform.position, Quaternion.identity);//实例化爆炸
        Destroy(explode, 0.3f);//延迟销毁爆炸物体
        GameState.CurrentScore +=100;//获得分数是100
        GameState.KillEnemyNumber += 1;//击杀敌机数量加1
        Instantiate(ShuiJingPrefab, mTransform.position+new Vector3(0,0,-0.1f), Quaternion.identity);//实例化水晶
    }

    // Update is called once per frame
    void Update()
    {
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
                mTransform.Translate(new Vector3(1,-1,0) * Speed * Time.deltaTime);
            }
            else if (right == 2)
            {
                mTransform.Translate(new Vector3(-1,-1,0) * Speed * Time.deltaTime);
            }
            if (mTransform.position.y < -4.6f)
            {//超出屏幕范围，销毁
                Destroy(gameObject);
            }
        }
    }
}
