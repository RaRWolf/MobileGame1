using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    private Color randomColor;
    // Start is called before the first frame update
    void Start()
    {
        randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        gameObject.GetComponent<MeshRenderer>().material.color = randomColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
