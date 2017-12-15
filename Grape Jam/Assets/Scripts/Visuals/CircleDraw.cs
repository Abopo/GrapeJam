using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDraw : MonoBehaviour {
    float theta_scale = 0.01f;        //Set lower to add more points
    int size; //Total number of points in circle
    float radius = 3f;
    LineRenderer lineRenderer;

    GrapeSwarm _grapeSwarm;

    void Awake() {
        float sizeValue = (2.0f * Mathf.PI) / theta_scale;
        size = (int)sizeValue;
        size++;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        //lineRenderer.SetWidth(0.02f, 0.02f); //thickness of line
        lineRenderer.positionCount = size;
        //lineRenderer.SetVertexCount(size);
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        _grapeSwarm = GetComponent<GrapeSwarm>();
        radius = _grapeSwarm.AverageDistance();
    }

    void Update() {
        radius = _grapeSwarm.AverageDistance() + 2;
        Vector3 pos;
        float theta = 0f;
        for (int i = 0; i < size; i++) {
            theta += (2.0f * Mathf.PI * theta_scale);
            float x = radius * Mathf.Cos(theta);
            float z = radius * Mathf.Sin(theta);
            x += transform.position.x;
            z += transform.position.z;
            pos = new Vector3(x, transform.position.y, z);
            lineRenderer.SetPosition(i, pos);
        }
    }
}
