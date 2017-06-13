using UnityEngine;
using System.Collections;

public class SmallEnemyBulletContr : MonoBehaviour {
    private GameObject getPlayer;//获取Player的位置
    public GameObject EnemyBullet1;//获取子弹的预设
    private Transform mTransform;
    public float mTime;//定义变量用来比较
    private bool shoot= true;//定义是否射击
    private float angle;//定义获取的角度变量
	// Use this for initialization
    void Start()
    {
        mTransform = transform;
    }
	// Update is called once per frame
	void Update () {
        if (GameState.IsLive == true)//当飞机存活时才实时获取飞机的位置，并瞄准
        {
           getPlayer = GameObject.FindGameObjectWithTag("RealPlayer1");//根据标签找到战机1
           var myPosition = getPlayer.transform;
           if (GameState.IsPause == false)//当不处于暂停状态才瞄准
           {
               if (mTransform.position.y - myPosition.position.y < 0) {//当敌机处于玩家下方不能发射子弹
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
                   Instantiate(EnemyBullet1, mTransform.position+new Vector3(0,-0.2f,-0.1f), rotation);
                   mTime = 0;
                   shoot = false;//发射之后置为假
               }
               mTime += Time.deltaTime;
               if (mTime > 2)//发射的时间间隔
                   shoot = true;
           }
           else {
               return;
           }
               
            }
        }
	}

