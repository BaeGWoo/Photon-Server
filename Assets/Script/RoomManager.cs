using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;//어느 서버에 접속했을때 이벤트를 호출하는 라이브러리


public class RoomManager : MonoBehaviourPunCallbacks
{

    [SerializeField] InputField roomName, roomPerson;
    [SerializeField] Button roomCreate, roomJoin;
    
    
    [SerializeField] GameObject roomPrefab;
    [SerializeField] Transform roomContent;

    //시간복잡도가 O(1)의 시간복잡도를 가집니다.
    //Dictionary : Key_Value 형태의 값을 저장할 수 있는 자료구조입니다.
    Dictionary<string, RoomInfo> roomCatalog = new Dictionary<string, RoomInfo>();

   



   
    void Update()
    {
     if(roomName.text.Length>0)
        {
            //방 접속 버튼을 활성화합니다.
            roomJoin.interactable = true;
        }
        else
        {
            roomJoin.interactable = false;
        }

     //OnJoinRandomFailed : 방 접속에 실패했을 때 호출되는 함수

     if(roomName.text.Length>0&&roomPerson.text.Length>0)
        {
            roomCreate.interactable = true;
        }

        else
        {
            roomCreate.interactable = false;
        }
    }

    public void OnClickCreateRoom()
    {
        //룸 옵션을 설정합니다.
        RoomOptions room = new RoomOptions();

        //최대 접속자의 수를 설정합니다.
        room.MaxPlayers = byte.Parse(roomPerson.text);

        //룸의 접속 여부를 설정합니다.
        room.IsOpen = true;

        //로비에서 룸 목록을 노출 시킬 지 결정합니다.
        room.IsVisible = true;

        //룸을 생성하는 함수
        PhotonNetwork.CreateRoom(roomName.text, room);

    }

    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("created Room");
    }

    public void AllDeleteRoom()
    {
        //Transform 오브젝트(Content)에 있는 하위 오브젝트에 접근하여 전체 룸을 삭제합니다.

        //                element  <- [room]
        foreach(Transform element in roomContent)
        {
            //Transform이 가지고 있는 게임 오브젝트를 삭제합니다.
            Destroy(element.gameObject);
        }
    }

    //룸에 입장했을 때 호출되는 콜백함수
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");

    }

    public void CreateRoomObject()
    {
        //roomCatalog에 여러 개의 value값이 들어가 있다면 RoomInfo에 넣어줍니다.
        foreach(RoomInfo info in roomCatalog.Values)
        {
            GameObject room = Instantiate(roomPrefab);

            //roomContent의 하위 오브젝트로 넣어줍니다.
            room.transform.SetParent(roomContent);

            //룸 정보를 입력합니다.
            //info는 Photon에서 제공해주는 정보
            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
            
        }

       
    }


    //룸 목록을 갱신하는 함수
    public void UpdateRoom(List<RoomInfo> roomList)
    {

        //포톤 서버에 룸이 있다면
        for(int i=0;i<roomList.Count;i++)
        {
            //해당 이름이 roomCatalog의 key값으로 설정되어 있다면
            if(roomCatalog.ContainsKey(roomList[i].Name))
            {
                //RemoveFromList : (true) 룸에서 삭제가 되었을 대
                if(roomList[i].RemovedFromList)
                {
                    //딕셔너리에 있는 데이터를 삭제합니다.
                    roomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }
            //룸이 없다면 roominfo를 roomCatalog에 추가합니다.
            roomCatalog[roomList[i].Name]=roomList[i];
        }
    }

    //해당 로비에 방 목록의 변경 사항이 있으면 호출(추가, 삭제, 참가)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        AllDeleteRoom();
        UpdateRoom(roomList);
        CreateRoomObject();
    }

}
