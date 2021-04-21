using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjet : MonoBehaviour
{
    private bool Opened = false;
    public int Rarity;
    public uint ItemReference;
    public MeshRenderer OpenedChest;
    public MeshRenderer ClosedChest;
    public Classes.Item content;

    // Start is called before the first frame update
    void Start()
    {
        int RarityGenerator = Random.Range(1, 100);
        if (RarityGenerator <= 49) Rarity = 0;
        else if (RarityGenerator <= 88) Rarity = 1;
        else if (RarityGenerator <= 99) Rarity = 2;
        else Rarity = 3;
        
        ItemReference = (uint) Random.Range(0, Classes.AllItem[Rarity].Count);
        content = Classes.AllItem[0][2];
    }
    
    void Update()
    {
        if(!Opened)
        {
            Collider[] getters = Physics.OverlapSphere(transform.position, 5);
            for (int i = 0; i < getters.Length; i++)
                if (getters[i].GetComponent<CharacterThings>() && Input.GetKeyDown("e"))
                {
                    content.Joueur = getters[i].gameObject;
                    content.AppliedEffect();
                    getters[i].GetComponent<CharacterThings>().Inventory.Add(content);
                    HideMenu.Print(Classes.AllItem[1][0]);
                    OpenedChest.enabled = true;
                    ClosedChest.enabled = false;
                    Opened = true;
                }
        }
    }
}