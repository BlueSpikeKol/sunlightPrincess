using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LiquidAreaHelper))]
public class LiquidAreaHelperEditor : Editor
{
	public override void OnInspectorGUI()
	{
        base.OnInspectorGUI();

        var t = target as LiquidAreaHelper;

        var size = t.size;
        var collider = t.Collider;
        var surfaceRenderer = t.SurfaceRenderer;
        var areaRenderer = t.AreaRenderer;

        if(collider == null || surfaceRenderer == null || areaRenderer == null) return;

        collider.size = new Vector2(size.x, size.y + 0.5f);
        collider.offset = new Vector2(size.x / 2f, -size.y / 2f + 0.25f);

        areaRenderer.size = size;
        areaRenderer.transform.localPosition = new Vector3(size.x / 2f, -size.y / 2f);
        areaRenderer.transform.localScale = Vector3.one;

        surfaceRenderer.size = new Vector2(size.x, 1);
        surfaceRenderer.transform.localPosition = new Vector3(size.x / 2f, 0.5f);
        surfaceRenderer.transform.localScale = Vector3.one;
	}
}
