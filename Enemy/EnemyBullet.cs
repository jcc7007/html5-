using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
    public float Speed = 0.7f;//子弹飞行的速度
    public AudioClip PlayerDamage;
    // Use this for initialization
    private Transform mTransform;
	// Use this for initialization
	void Start () {
        mTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.IsPause == false)//如果是暂停状态，子弹停止飞行
        {
            mTransform.Translate(Vector3.down * Speed * Time.deltaTime);
            if (mTransform.position.y <- 4.6f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            return;
        }
	}
    void OnTriggerEnter(Collider other)
    {//在这里用来检测碰撞
        if (other.tag == "RealPlayer1")//由于频繁获取相互组件，太过繁琐，准备分别响应碰撞事件。
        {
            AudioSource.PlayClipAtPoint(PlayerDamage, new Vector3(0, 0, -10));
            Destroy(this.gameObject,0.2f);//销毁子弹，
        }
    }
}
