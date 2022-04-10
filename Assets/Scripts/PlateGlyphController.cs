using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateGlyphController : MonoBehaviour
{
    // Start is called before the first frame update
    public OnTriggerRagdoll MiddlePlate;
    public GameObject Glyphs;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Glyphs.SetActive(MiddlePlate.isOn);
    }
}
