using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDisplayer : MonoBehaviour
{
    public GameObject line;
    public GameObject arrow;
    public GameObject corner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CancelNode()
    {
        line.SetActive(false);
        arrow.SetActive(false);
        corner.SetActive(false);
    }
}
