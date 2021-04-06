using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashText : MonoBehaviour
{
    public float speed = 1.0f;
    private TextMeshPro text;
    private Image image;
    private float time;

    private enum ObjType
    {
        TEXTMESHPRO,
        IMAGE
    };

    private ObjType thisObjType = ObjType.TEXTMESHPRO;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<Image>())
        {
            this.thisObjType = ObjType.IMAGE;
            image = this.gameObject.GetComponent<Image>();
        }else if (this.gameObject.GetComponent<TextMeshPro>())
        {
            thisObjType = ObjType.TEXTMESHPRO;
            text = this.gameObject.GetComponent<TextMeshPro>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(thisObjType == ObjType.IMAGE)
        {
            image.color = GetAlphaColor(image.color);
        }else if (thisObjType == ObjType.TEXTMESHPRO)
        {
            text.color = GetAlphaColor(text.color);
        }
    }

    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
