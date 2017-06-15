using UnityEngine;
using System.Collections;

public class HelpBgController : MonoBehaviour {
    private Transform mTransfrom;
    public float Speed; //设置背景滚动的速度
    private Vector3 mOffset;//三维变量记录背景的滚定过程中的实时位置
	// Use this for initialization
	void Start () {
        mTransfrom = transform;
	}
	
	// Update is called once per frame
	void Update () {
        mOffset = Time.deltaTime * Speed * Vector3.left;
        mTransfrom.Translate(mOffset);//向左缓慢滚动
        if (mTransfrom.position.x < -5.6f)
        {
            mOffset = new Vector3(5.2f,0, 0);
            mTransfrom.localPosition = mOffset;
        }
	}
}
