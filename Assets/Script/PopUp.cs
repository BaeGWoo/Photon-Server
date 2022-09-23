using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PopUp : MonoBehaviour
{
    //Text <- 없어질 수가 있다.
    //TextMeshPro <- 조금 더 업그레이드 된 버전

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;

    private static GameObject gamePanel;


    //PopUp 스크립트로 전역에서 접근할 수 있는 함수
    public static PopUp Show(string title, string message)
    {
        if(gamePanel==null)
        {
           gamePanel = Resources.Load<GameObject>("Game Panel");
        }


        GameObject obj = Instantiate(gamePanel);


        PopUp window = obj.GetComponent<PopUp>();
        window.UpdateContent(title, message);

        return window;
      
    }

    //팝업의 내용을 업데이트하는 함수
    public void UpdateContent(string titleMessage, string contentMessage)
    {
        title.text = titleMessage;
        content.text = contentMessage;

    }

    //팝업을 닫는 함수
    public void Cancle()
    {
        Destroy(gameObject);
    }

}
