using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

   
    public Text scoreText;
    public Text leaderBorderText;

    //싱글톤에서는 Awake함수로 데이터를 받아둬야한다.
    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

}
