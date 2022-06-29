using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendManager : MonoBehaviour
{
    public GameObject pendulum;
    public int maxOscilaciones = 60;
    public float maxOsciTiempo= 60;
    public int numberOfPendulums=6;

    private List<GameObject> pendulums = new List<GameObject>();
    private int n;
    private Vector3 pendPosition, anchor, anchor1;
    private float dy;
    

    // Start is called before the first frame update
    void Start()
    {
        dy = 0;
        while (n < numberOfPendulums)
        {
            anchor = calcAnchor(n);
            if (n != 0)
            {
                anchor1 = calcAnchor(n + 1);
                dy += anchor1.y - anchor.y;
            }

            pendPosition = new Vector3((float)n / 10, 1 - dy*0.05f, 0);
            pendulums.Add(Instantiate(pendulum, pendPosition,Quaternion.identity));
            pendulums[n].GetComponent<HingeJoint>().anchor = anchor;
            pendulums[n].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 2), ForceMode.Impulse);

            n++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 calcAnchor(int pendCount)
    {
        Vector3 NewAnchor;
        float l;
        float freq = (maxOscilaciones-pendCount) / maxOsciTiempo;
        float g = Physics.gravity.magnitude;

        l = Mathf.Pow(1 / (freq * 2 * Mathf.PI), 2) * g;

        NewAnchor = new Vector3(0, l/0.05f, 0);
        return (NewAnchor);
    }
}
