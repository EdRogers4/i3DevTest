using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Renderer meshRenderer;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        
    }

    private void OnMouseEnter() //Coder Reference: https://www.youtube.com/watch?v=fw7h3UBgNW4
    {
        meshRenderer.material.color = Color.green;
    }

    private void OnMouseExit() //Coder Reference: https://www.youtube.com/watch?v=fw7h3UBgNW4
    {
        meshRenderer.material.color = Color.white;
    }

    public void OnSelect()
    {

    }
}
