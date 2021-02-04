using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HideMenu : MonoBehaviour
{
    public static GameObject menu;
    public static TextMeshProUGUI changingText;
    public static Image image;
    private static MonoBehaviour singleton;
    public static void Print(Classes.Item item)
    {
        changingText.text = item.Getdescription();
        image.sprite = item.Sprite;
        singleton.StartCoroutine(Wait());
    }
    static IEnumerator Wait()
    {
        menu.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        menu.gameObject.SetActive(false);
    }
        

    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
        menu = GameObject.Find("menu");
        changingText = GameObject.Find("description").GetComponent<TextMeshProUGUI>();
        image = GameObject.Find("printsprite").GetComponent<Image>();
        menu.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}