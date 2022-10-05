using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class PhotonControl : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float angleSpeed;

    [SerializeField] Camera cam;

    private Animator animator;

    public int score;

    //IsMine : 나 자신만 플레이하고 싶을때
    void Start()
    {
        animator = GetComponent<Animator>();

        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            cam.enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //현재 플레이어가 나 자신이 아니라면
        if (!photonView.IsMine) return;



        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }


        Vector3 direction = new Vector3
            (
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            );

        transform.Translate(direction.normalized * speed * Time.deltaTime);

        transform.eulerAngles += new Vector3
            (
                0,
                Input.GetAxis("Mouse X") * angleSpeed * Time.deltaTime,
                0
            );

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="Crystal(Clone)")
        {
            if (photonView.IsMine)
                score++;

            PlayFabDataSave();
                //UIManager.instance.score++;

            PhotonView view = other.gameObject.GetComponent<PhotonView>();

            if(view.IsMine)//충돌한 물체가 자기 자신이라면
            {
                //충돌한 네트워크 객체를 파괴합니다.
                PhotonNetwork.Destroy(other.gameObject);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        score = (int)stream.ReceiveNext();
    }

    public void PlayFabDataSave()
    {
        PlayFabClientAPI.UpdatePlayerStatistics
            (
                new UpdatePlayerStatisticsRequest
                {
                    Statistics = new List<StatisticUpdate>
                    {
                        new StatisticUpdate
                        {
                            StatisticName="Score",Value=score
                        },
                    }
                },
                //무명함수, 람다
                (result) => { Debug.Log("값 저장 성공"); },
                (error) => { Debug.Log("값 저장 실패"); }
            );
    }
}
