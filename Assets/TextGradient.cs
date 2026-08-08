using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGradient : BaseMeshEffect
{
    public Color topColor = Color.white;
    public Color bottomColor = Color.yellow;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive()) return;

        List<UIVertex> vertices = new List<UIVertex>();
        vh.GetUIVertexStream(vertices);

        if (vertices.Count == 0) return;

        float minY = vertices[0].position.y;
        float maxY = vertices[0].position.y;

        foreach (UIVertex vertex in vertices)
        {
            minY = Mathf.Min(minY, vertex.position.y);
            maxY = Mathf.Max(maxY, vertex.position.y);
        }

        for (int i = 0; i < vertices.Count; i++)
        {
            UIVertex vertex = vertices[i];

            float t = Mathf.InverseLerp(minY, maxY, vertex.position.y);
            vertex.color = Color.Lerp(bottomColor, topColor, t);

            vertices[i] = vertex;
        }

        vh.Clear();
        vh.AddUIVertexTriangleStream(vertices);
    }
}