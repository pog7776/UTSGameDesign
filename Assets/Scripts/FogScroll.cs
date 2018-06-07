using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScroll : MonoBehaviour {
    public Material myMaterial;
    public float speed;
    // Use this for initialization
    void Start () {
        myMaterial = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update () {
        myMaterial.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
