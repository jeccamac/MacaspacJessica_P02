using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseDolly : MonoBehaviour
{
    public GameObject corpseDolly;

    GameObject playerCorpse;
    public void Awake()
    {
        playerCorpse = GameObject.Find("PlayerCorpse(Clone)");
    }

    public void Update()
    {
        corpseDolly.transform.position = playerCorpse.transform.position;
    }
}
