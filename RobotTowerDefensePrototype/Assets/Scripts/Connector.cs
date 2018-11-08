using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour {
    public Vector3 firstConnection, secondConnection;

    private LineRenderer renderer;
	void Start () {
        renderer = GetComponent<LineRenderer>();
	}

    public void UpdateColour(Color Colorstart, Color Colorend)
    {
        renderer.startColor = Colorstart;
        renderer.endColor = Colorend;
    }
    public void ActivateRenderer(bool set)
    {
        renderer.enabled = set;
        if (set)
        {
            RenderLines();
        }
    }
    public void RenderLines()
    {
        Vector3[] temp = { firstConnection, secondConnection };
        renderer.SetPositions(temp);
    }
    public void DestroyMe()
    {

    }
}
