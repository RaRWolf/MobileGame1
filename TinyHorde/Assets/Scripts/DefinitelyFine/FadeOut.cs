using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{

    public Image myImage;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeOutIE");
    }

    // Update is called once per frame
    void Update()
    {
        //myImage.CrossFadeColor(Color.black, 1.0f, false, true);
        //myImage.CrossFadeAlpha(0.1f, 1.0f, false);


    }

    IEnumerator FadeOutIE()
    {
        for (float i = 1.25f; i >= 0; i -= .001f)
        {
            // set color with i as alpha
            myImage.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }
}
