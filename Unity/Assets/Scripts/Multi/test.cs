using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class test : MonoBehaviour
{
    public GameObject mob;
    public bool solo = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!solo && !PhotonNetwork.IsMasterClient)
        {
            return;
        }

        Chest chest1;
        if (solo)
        {
            chest1 = Instantiate(mob, transform.position, Quaternion.identity).GetComponent<Chest>();
        }
        else
        {
            chest1 = PhotonNetwork.Instantiate("PhotonPrefabs/" + mob.name, transform.position, Quaternion.identity).GetComponent<Chest>();
        }
        
        int rarity, refer;
        Random random = new Random();
        int RarityGenerator = Random.Range(0, 100);
        if (RarityGenerator <= 48) rarity = 0;
        else if (RarityGenerator <= 87) rarity = 1;
        else if (RarityGenerator <= 98) rarity = 2;
        else rarity = 3;
        
        refer = Random.Range(0, Classes.AllItem[rarity].Count);
        if (solo)
        {
            chest1.content = Classes.AllItem[rarity][refer];
            chest1.Rarity = rarity;
            chest1.ItemReference = refer;
            chest1.solo = true;
        }
        else
        {
            chest1.GetComponent<PhotonView>().RPC("Start_chest", RpcTarget.All,rarity, refer);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
