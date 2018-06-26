using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour
{
    [SerializeField]
    List<Image> buttonImages = new List<Image>();

    public void ChangeColor()
    {
        for (int i = 0; i < buttonImages.Count; i++)
        {
            buttonImages[i].color = new Color(0.62f, 0.62f, 0.62f, 1);
        }

        gameObject.GetComponent<Image>().color = Color.white;
    }
}
