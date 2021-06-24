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
    public bool solo;

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
        solo = false;
    }

    [PunRPC]
    public void Set_Mimique(int view1, int view)
    {
        GameObject player = PhotonView.Find(view).gameObject;
        GameObject Mimique = PhotonView.Find(view1).gameObject;
        Mimique.GetComponent<MimiqueIA>().content = content;
        Mimique.GetComponent<MimiqueIA>().Getter = player.gameObject;
        Mimique.GetComponent<MimiqueIA>().Rarity = Rarity;
        Mimique.GetComponent<MimiqueIA>().ItemReference = ItemReference;
    }


[PunRPC]
    void OpeningChest(int view)
    {
        GameObject player = PhotonView.Find(view).gameObject;
        if (player.GetComponent<CharacterThings>().klepto)
        {
            player.GetComponent<CharacterThings>().GetComponent<PhotonView>().RPC("Heal", RpcTarget.All,10);
        }
        Opened = true;
        anim.SetBool("IsOpened",true);
        GetComponent<AudioSource>().Play();
        int IsMimique = Random.Range(1, 100);
        if(IsMimique > 100)
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
            
            if (PhotonNetwork.IsMasterClient)
            {
                GameObject mimi = PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + mimique.name, transform.position, Quaternion.identity);
                GetComponent<PhotonView>().RPC("Set_Mimique", RpcTarget.All, mimi.GetComponent<PhotonView>().ViewID, view);
            }
            content.Joueur = player.gameObject;
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
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
                    if (!solo)
                    {
                        GetComponent<PhotonView>().RPC("OpeningChest", RpcTarget.All, getters[i].GetComponent<PhotonView>().ViewID);
                    }
                    else
                    {
                        if (getters[i].GetComponent<CharacterThings>().klepto)
                        {
                            getters[i].GetComponent<CharacterThings>().Heal(10);
                        }
                        Opened = true;
                        anim.SetBool("IsOpened",true);
                        GetComponent<AudioSource>().Play();
                        int IsMimique = Random.Range(1, 100);
                        if(IsMimique > 6)
                        {
                            if (getters[i].GetComponent<CharacterThings>().luck != 0 && Rarity < 3)
                            {
                                Rarity += 1;
                                ItemReference = (int) Random.Range(0, Classes.AllItem[Rarity].Count);
                                content = Classes.AllItem[Rarity][ItemReference];
                            }
                            content.Joueur = getters[i].gameObject;
                            content.AppliedEffect();
                        }
                        else
                        {
                            GameObject Mimique = Instantiate(mimique, transform.position, Quaternion.identity);
                            content.Joueur = getters[i].gameObject;
                            Mimique.GetComponent<MimiqueIA>().content = content;
                            Mimique.GetComponent<MimiqueIA>().Getter = getters[i].gameObject;
                            Mimique.GetComponent<MimiqueIA>().Rarity = Rarity;
                            Mimique.GetComponent<MimiqueIA>().solo = true;
                            Mimique.GetComponent<MimiqueIA>().ItemReference = ItemReference;
                            Destroy(gameObject);
                        }
                    }
                    
                    HideMenu.Print(Classes.AllItem[Rarity][ItemReference]);
                    getters[i].GetComponent<CharacterThings>().Inventory.Add(content);
                }
        }
    }
}