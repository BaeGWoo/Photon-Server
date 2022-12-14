using System.Collections;
using UnityEngine;
using Photon.Pun;

public class CreateCrystal : MonoBehaviour
{

    [SerializeField] Transform[] point;

    private void Start()
    {
        StartCoroutine(nameof(ObjectCreation));
    }

    private IEnumerator ObjectCreation()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            while(true)
            {
                PhotonNetwork.Instantiate
                    (
                        "Crystal",
                        point[Random.Range(0,4)].position,
                        Quaternion.identity
                    );
                yield return new WaitForSeconds(5f);
            }
        }
    }


}
