using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
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
        content = Classes.AllItem[Rarity][ItemReference];
    }

    // Update is called once per frame
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
                    HideMenu.Print(Classes.AllItem[Rarity][ItemReference]);
                    OpenedChest.enabled = true;
                    ClosedChest.enabled = false;
                    Opened = true;
                }
        }
    }
    
    
}
