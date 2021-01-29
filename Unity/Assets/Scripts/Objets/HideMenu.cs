using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideMenu : MonoBehaviour
{
    public static GameObject menu;
    public static Text changingText;
    public static Image image;

    public static void Print(Classes.Item item)
    {
        changingText.text = item.Getdescription();
        image.sprite = item.Getsprite();
        menu.gameObject.SetActive(true);
    }


    private static IEnumerator Wait()
    {
        if(menu.activeSelf)
        {
            menu.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            menu.gameObject.SetActive(false);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        menu = gameObject;
        changingText = GameObject.Find("Text").GetComponent<Text>();
        image = GameObject.Find("Image").GetComponent<Image>();
        menu.gameObject.SetActive(false);
    }

    void Update()
    {
        Wait();
    }
}