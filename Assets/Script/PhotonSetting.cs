using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
//using TMPro;
using UnityEngine.EventSystems;




public class PhotonSetting : MonoBehaviour
{
    EventSystem eventSystem;
    public Selectable firstInput;

    [SerializeField] InputField email;
    [SerializeField] InputField password;
    [SerializeField] InputField username;
    [SerializeField] Dropdown region;
    //public TMP_Dropdown


    private void Awake()
    {
        eventSystem = EventSystem.current;
        //처음은 Email Input Field를 선택하도록 설정합니다.
        firstInput.Select();

        PlayFabSettings.TitleId = "54ABD";
       
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)&&Input.GetKey(KeyCode.LeftShift))
        {
            //TAB + LeftShift는 위의 Selectable 객체를 선택합니다.
            Selectable next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
        
            if(next!=null)
            {
                next.Select();
            }
        }

        else if(Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next=eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
        
            if(next!=null)
            {
                next.Select();
            }
        }



    }


    //LoginResult <- 로그인 성공 여부 반환(playfab에 내장되어있다.)
    public void LoginSuccess(LoginResult result)
    {


        //마스터 클라이언트를 기준으로 씬을 동기화할 지 안할지 결정하는 기능
        //fasle=동기화를 하지 않겠다.
        PhotonNetwork.AutomaticallySyncScene = false;

        //같은 버전의 유저끼리 접속을 허용합니다.
        PhotonNetwork.GameVersion = "1.0f";

        //유저 아이디 설정
        PhotonNetwork.NickName = username.text;

        //입력한 지역을 설정합니다.
        //dropdown형태일 경우, 
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.options[region.value].text;


        //서버 접속
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void LoginFailure(PlayFabError error)
    {
        PopUp.Show("LOGIN FAILURE", "Login Failed.\n Please login again");
    }
    
    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        PopUp.Show
             (
            "MEMBERSHIP\n",
            "Congratulation on your\nSuccessful Membership Registeration"
            );
    }

    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("회원가입 실패");
    }

    public void SignUp()
    {
        //RegisterPlayFabUserRequest : 서버에 유저를 등록하기 위한 클래스
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            Username = username.text
        };

        PlayFabClientAPI.RegisterPlayFabUser
        (
            request,
            SignUpSuccess,
            SignUpFailure
        );
    }
   
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password=password.text,
         };

        PlayFabClientAPI.LoginWithEmailAddress
            (
                request,
                LoginSuccess,
                LoginFailure
            );
    }
}
