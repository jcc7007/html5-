using UnityEngine;
using System.Collections;

public class BackMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void backMainMenu() {
        StartCoroutine(backLevel());//为了让按钮的声音播放出来，延迟一下再加载下一个场景 
    }
    IEnumerator backLevel()
    {//延迟函数
        yield return new WaitForSeconds(0.5f);
        Application.LoadLevel(0);
    }
}
