using UnityEngine;
using System.Collections;  //引用 系統,集合 API:延遲 - 微軟 API
public class GameManager : MonoBehaviour
{
    //定義欄位(宣告變數)
    //修飾詞 類型 名稱;
    //public 公開:顯示在屬性面板上
    //GameOject 遊戲物件:儲存階層面板內的物件
    /// <summary>
    /// 燈光群組
    /// </summary>
    [Header("燈光群組")]
    public GameObject groupLight;
    [Header("會動的寶箱")]
    public Transform chest;
    [Header("喇叭")]
    public AudioSource aud;
    [Header("木板上滑動音效")]
    public AudioClip soundWoodMove;
    [Header("敲門音效")]
    public AudioClip soundKnock;
    [Header("開門音效")]
    public AudioClip soundOpen;
    [Header("門的動畫控制器")]
    public Animator aniDoor;

    private int countDoor; //看到門的次數

    public int countChest; //看到寶箱的次數
    /// <summary>
    ///看到門
    /// </summary>
    public void LookDoor()
    {
        countDoor++;     //遞增 1

        //如果 看到的門次數 等於 1
        if (countDoor == 1)
        {
            aud.PlayOneShot(soundKnock, 5);
        }
        else if (countDoor == 2)
        {
            aud.PlayOneShot(soundOpen, 4.5f);
            aniDoor.SetTrigger("開門觸發器");
        }
    }

    //定義方法 (Method):有特定內容的功能
    //修飾詞 傳回類型 方法名稱(){敘述}
    //void 無傳回:使用方法不會得到任何資料
    //協同程序:多線程式處理方式
    /// <summary>
    /// 燈光閃爍效果
    /// </summary>
    public IEnumerator LieghtEffent()
    {
        //燈光模組,啟動設定(隱藏)
        groupLight.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        groupLight.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        groupLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        groupLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        groupLight.SetActive(false);
        yield return new WaitForSeconds(0.15f);
        groupLight.SetActive(true);
    }

    /// <summary>
    /// 開始移動寶箱
    /// </summary>
    public void StartMoveChest()
    {
        StartCoroutine(MoveChest());
    }

    //注視點或按鈕無法呼叫協程
    /// <summary>
    /// 移動寶箱
    /// </summary>
    /// <returns></returns>
    public IEnumerator MoveChest()
    {
        //GetComponent<泛型>()取得元件:可以取得物件在屬性面板上的所有元件
        //enable 元件啟動或停止 true 啟動 false 停止
        chest.GetComponent<CapsuleCollider>().enabled = false;

        //前:forward
        //右:right
        //上:up
        //for 迴圈(初始值，條件，跌代器 - 每次迴圈結束要執行的敘述)
        for (int i = 0; i < 10; i++)
        {
            chest.position -= chest.forward * 0.3f;            //寶箱.座標 遞減 寶箱.前方
            yield return new WaitForSeconds(0.001f);
        }

        //喇叭.播放一次音效(音效，音量)
        aud.PlayOneShot(soundWoodMove, 2.5f);
    }
    //事件:開始 - 播放時執行一次 ，初始化或遊戲開始需要的內容
    private void Start()
    {
        
        //LieghtEffent();    //呼叫自定義方法：一般呼叫方式     
        StartCoroutine(LieghtEffent());  //呼叫協程方式
    }
}
