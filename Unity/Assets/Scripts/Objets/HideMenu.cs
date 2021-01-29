using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideMenu : MonoBehaviour
{
    public GameObject menu;
    public Text changingText;
    public Image image;

    public void Print(Classes.Item item)
    {
        changingText.text = item.Getdescription();
        image.sprite = item.Sprite;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        menu.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        menu.gameObject.SetActive(false);
    }



    // Start is called before the first frame update
    void Start()
    {
        menu.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey("b"))
        {
            Print(Classes.AllItem[0][1]);
        }
    }
}