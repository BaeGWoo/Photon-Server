using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectServer : MonoBehaviourPunCallbacks
{
    private string serverName;

    [SerializeField] GameObject[] character;


    private void Start()
    {
        character[DataManager.characterCount].SetActive(true);
    }

    public void RightcharacterSelect()
    {
        DataManager.characterCount++;

        for(int i=0;i<character.Length;i++)
        {
            character[i].SetActive(false);
        }

  
        if(DataManager.characterCount>=3)
        {
            DataManager.characterCount = 0;
        }

        character[DataManager.characterCount].SetActive(true);
    }



    public void LeftcharacterSelect()
    {
        DataManager.characterCount--;

        for (int i = 0; i < character.Length; i++)
        {
            character[i].SetActive(false);
        }

        if (DataManager.characterCount <0)
        {
            DataManager.characterCount = 2;
        }
        character[DataManager.characterCount].SetActive(true);
    }


    public void SelectLobby(string text)
    {
        serverName = text;


        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Photon Room");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(new TypedLobby(serverName, LobbyType.Default));
    }
}
