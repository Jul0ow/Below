using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideMenu : MonoBehaviour
{
    public GameObject menu;

    public Text changingText;
    public Image image;

    
    
    public IEnumerator Print(string text, Sprite sprite)
    {
        changingText.text = text;
        image.sprite = sprite;
        menu.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        menu.gameObject.SetActive(false);
    }



    // Start is called before the first frame update
    void Start()
    {
        menu.gameObject.SetActive(false);
    }
}
