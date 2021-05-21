using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool Opened = false;
    public int Rarity;
    public int ItemReference;
    public MeshRenderer OpenedChest;
    public MeshRenderer ClosedChest;
    public MeshRenderer ClosedChestTop;
    public Light light;
    public Classes.Item content;
    public GameObject mimique;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        //GetComponent<PhotonView>().RPC("Start_chest", RpcTarget.All);
    }

    [PunRPC]
    void Start_chest(int rar, int refer)
    {
        Rarity = rar;
        ItemReference = refer;
        content = Classes.AllItem[Rarity][ItemReference];
    }
    

    [PunRPC]
    void OpeningChest(int view)
    {
        GameObject player = PhotonView.Find(view).gameObject;
        Opened = true;
        anim.SetBool("IsOpened",true);
        GetComponent<AudioSource>().Play();
        int IsMimique = Random.Range(1, 100);
        if(IsMimique > 6)
        {
            if (player.GetComponent<CharacterThings>().luck != 0 && Rarity < 3)
            {
                Rarity += 1;
                ItemReference = (int) Random.Range(0, Classes.AllItem[Rarity].Count);
                content = Classes.AllItem[Rarity][ItemReference];
            }
            content.Joueur = player;
            content.AppliedEffect();
        }
        else
        {
            GameObject Mimique = PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + mimique.name, transform.position, Quaternion.identity);
            content.Joueur = player.gameObject;
            Mimique.GetComponent<MimiqueIA>().content = content;
            Mimique.GetComponent<MimiqueIA>().Getter = player.gameObject;
            Mimique.GetComponent<MimiqueIA>().Rarity = Rarity;
            Mimique.GetComponent<MimiqueIA>().ItemReference = ItemReference;
            PhotonNetwork.Destroy(gameObject);
        }
    }
    
    void Update()
    {
        if(!Opened)
        {
            Collider[] getters = Physics.OverlapSphere(transform.position, 5);
            for (int i = 0; i < getters.Length; i++)
                if (getters[i].GetComponent<CharacterThings>() && Input.GetKeyDown("e"))
                {
                    GetComponent<PhotonView>().RPC("OpeningChest", RpcTarget.All, getters[i].GetComponent<PhotonView>().ViewID);
                    HideMenu.Print(Classes.AllItem[Rarity][ItemReference]);
                    getters[i].GetComponent<CharacterThings>().Inventory.Add(content);
                }
        }
    }
}