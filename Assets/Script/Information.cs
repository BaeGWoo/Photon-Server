using Photon.Pun;
using UnityEngine.UI;

public class Information : MonoBehaviourPunCallbacks
{

    public Text roomData;


    //방이름, 현재인원, 최대인원
    public void SetInfo(string name, int currentCount,int MaxCount)
    {
        roomData.text = name + " ( " + currentCount + " / " + MaxCount + " ) ";



    }



}
