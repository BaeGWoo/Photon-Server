using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


//eddabf98-70df-48a2-aade-8d4b2365f15d

public class WebLoad : MonoBehaviour
{
    //웹에서 이미지를 불러올 때는 Raw Image를 사용해야 합니다.
    [SerializeField] RawImage[] webImage;

    [SerializeField] string[] imageAddress;



    //Awake : 게임 시작 전 데이터를 로드할 때 사용
    //Start : 캐릭터 위치 값 등 설정 후 게임 실행
    void Awake()
    {
        imageAddress[0]= "https://github.com/Unity2033/Unity-3D-Example/blob/main/Assets/Class/Photon%20Server/Texture/Ice%20Kingdom.jpg?raw=true";
        imageAddress[1] = "https://raw.githubusercontent.com/Unity2033/Unity-3D-Example/main/Assets/Class/Photon%20Server/Texture/Leader%20Board%20Panel.png";
        imageAddress[2] = "https://raw.githubusercontent.com/Unity2033/Unity-3D-Example/main/Assets/Class/Photon%20Server/Texture/Leader%20Board%20Panel.png";
        imageAddress[3] = "https://raw.githubusercontent.com/Unity2033/Unity-3D-Example/main/Assets/Class/Photon%20Server/Texture/Leader%20Board%20Panel.png";
        for (int i = 0; i < imageAddress.Length; i++)
        {
            StartCoroutine(WebImageLoad(imageAddress[i]));
        }
    }

    private void Start()
    {
        
    }


    //외부함수를 쓰기위해서 코루틴함수 사용
    private IEnumerator WebImageLoad(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        yield return www.SendWebRequest();


        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }

        else
        {
            for (int i = 0; i < webImage.Length; i++)
            {
                webImage[i].texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }

    }

}
