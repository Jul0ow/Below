using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HideMenu : MonoBehaviour
{
    public static GameObject menu;
    public static TextMeshProUGUI description;
    public static TextMeshProUGUI name;
    public static Image image;
    private static MonoBehaviour singleton;
    private static Image Rarity;
    
    public Sprite CommonImage;
    public Sprite RareImage;
    public Sprite EpicImage;
    public Sprite ReliqueImage;
    
    public static void Print(Classes.Item item)
    {
        description.text = item.Getdescription();
        name.text = item.Getname();
        image.sprite = item.Sprite;
        Rarity.sprite = item.GetRarity();
        singleton.StartCoroutine(Wait());
    }
    static IEnumerator Wait()
    {
        menu.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        menu.gameObject.SetActive(false);
    }
        
    
    void Awake()
    {
        singleton = this;
        menu = GameObject.Find("menu");
        description = GameObject.Find("description").GetComponent<TextMeshProUGUI>();
        name = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        image = GameObject.Find("printsprite").GetComponent<Image>();
        Rarity = GameObject.Find("Rarity").GetComponent<Image>();
        menu.gameObject.SetActive(false);
    }
    
}