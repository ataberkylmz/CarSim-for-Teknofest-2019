using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{

    public GameObject [] trafficLightR1, trafficLightR2;
    private int [] state = new int[2];
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0 && timer < 35)
        {
            if(timer < 15)
            {
                turnGreen(trafficLightR1);
                turnRed(trafficLightR2);
                state[0] = 1;
                state[1] = 0;

            }
            else if (timer < 20)
            {
                turnYellow(trafficLightR1);
                turnYellow(trafficLightR2);
            }
            else
            {
                turnRed(trafficLightR1);
                turnGreen(trafficLightR2);
                state[0] = 0;
                state[1] = 1;
            }
                
        }
        else if (timer >= 35)
        {
            timer = 0;
        }
            

    }

    private void turnRed(GameObject[] lights)
    {
        MeshRenderer [] lightBaseMesh;
        Light [] lightBaseLights;

        for(int i=0; i<lights.Length; i++)
        {
            lightBaseMesh = lights[i].GetComponentsInChildren<MeshRenderer>();
            lightBaseMesh[1].material.mainTextureOffset = new Vector2(1, 0);
            lightBaseLights = lights[i].GetComponentsInChildren<Light>(true);
            foreach(Light l in lightBaseLights)
            {
                if (l.color == Color.red)
                    l.enabled = true;
                else
                    l.enabled = false;
            }
        }
    }
    private void turnYellow(GameObject[] lights)
    {
        MeshRenderer [] lightBaseMesh;
        Light[] lightBaseLights;

        for (int i = 0; i < lights.Length; i++)
        {
            lightBaseMesh = lights[i].GetComponentsInChildren<MeshRenderer>();
            lightBaseMesh[1].material.mainTextureOffset = new Vector2(1.33f, 0);
            lightBaseLights = lights[i].GetComponentsInChildren<Light>(true);
            foreach (Light l in lightBaseLights)
            {
                if (l.color == Color.yellow)
                    l.enabled = true;
                else
                    l.enabled = false;
            }
        }
    }

    private void turnGreen(GameObject[] lights)
    {
        MeshRenderer [] lightBaseMesh;
        Light [] lightBaseLights;

        for (int i = 0; i < lights.Length; i++)
        {
            lightBaseMesh = lights[i].GetComponentsInChildren<MeshRenderer>();
            lightBaseMesh[1].material.mainTextureOffset = new Vector2(1.66f, 0);
            lightBaseLights = lights[i].GetComponentsInChildren<Light>(true);
            foreach (Light l in lightBaseLights)
            {
                if (l.color == Color.green)
                    l.enabled = true;
                else
                    l.enabled = false;
            }
        }
    }

    public int[] getState()
    {
        return state;
    }
}
