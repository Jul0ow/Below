using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool Opened = false;
    public int Rarity;
    public uint ItemReference;
    public MeshRenderer OpenedChest;
    public MeshRenderer ClosedChest;
    public MeshRenderer ClosedChestTop;
    public Light light;
    public Classes.Item content;
    public GameObject mimique;
    
    void Start()
    {
        int RarityGenerator = Random.Range(1, 100);
        if (RarityGenerator <= 49) Rarity = 0;
            else if (RarityGenerator <= 88) Rarity = 1;
                    else if (RarityGenerator <= 99) Rarity = 2;
                            else Rarity = 3;
        
        ItemReference = (uint) Random.Range(0, Classes.AllItem[Rarity].Count);
        content = Classes.AllItem[Rarity][ItemReference];
    }
    
    void Update()
    {
        if(!Opened)
        {
            Collider[] getters = Physics.OverlapSphere(transform.position, 5);
            for (int i = 0; i < getters.Length; i++)
                if (getters[i].GetComponent<CharacterThings>() && Input.GetKeyDown("e"))
                {
                    int IsMimique = Random.Range(1, 100);
                    if(IsMimique > 6)
                    {
                        if (getters[i].GetComponent<CharacterThings>().luck != 0 && Rarity < 3)
                        {
                            Rarity += 1;
                            ItemReference = (uint) Random.Range(0, Classes.AllItem[Rarity].Count);
                            content = Classes.AllItem[Rarity][ItemReference];
                        }

                        content.Joueur = getters[i].gameObject;
                        content.AppliedEffect();
                        getters[i].GetComponent<CharacterThings>().Inventory.Add(content);
                        HideMenu.Print(Classes.AllItem[Rarity][ItemReference]);
                        OpenedChest.enabled = false;
                        ClosedChest.enabled = true;
                        ClosedChestTop.enabled = true;
                        light.enabled = true;
                        Opened = true;
                    }
                    else
                    {
                        GameObject Mimique = PhotonNetwork.Instantiate("PhotonPrefabs/Mob/" + mimique.name, transform.position, Quaternion.identity);
                        content.Joueur = getters[i].gameObject;
                        Mimique.GetComponent<MimiqueIA>().content = content;
                        Mimique.GetComponent<MimiqueIA>().Getter = getters[i].gameObject;
                        Mimique.GetComponent<MimiqueIA>().Rarity = Rarity;
                        Mimique.GetComponent<MimiqueIA>().ItemReference = ItemReference;
                        Destroy(gameObject);
                    }
                }
        }
    }
}