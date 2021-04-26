using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugChest : MonoBehaviour
{
    private bool Opened = false;
    public int Rarity;
    public uint ItemReference;
    public MeshRenderer OpenedChest;
    public MeshRenderer ClosedChest;
    public MeshRenderer ClosedChestTop;
    public Light light;
    public Classes.Item content;

    void Start()
    {
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
                    Opened = true;
                    content.Joueur = getters[i].gameObject;
                        content.AppliedEffect();
                        getters[i].GetComponent<CharacterThings>().Inventory.Add(content);
                        HideMenu.Print(Classes.AllItem[Rarity][ItemReference]);
                        OpenedChest.enabled = false;
                        ClosedChest.enabled = true;
                        ClosedChestTop.enabled = true;
                        light.enabled = true;
                }
        }
    }
}
