using System.Collections.Generic;
using UnityEngine;

public class TilesetsMapper_Settings : ScriptableObject
{
    public enum TilesPreviewSizeEnum
    {
        Size32,
        Size64,
        Size128,
        Size256
    }

    public Dictionary<TilesPreviewSizeEnum, int> TilesPreviewReferences = new Dictionary<TilesPreviewSizeEnum, int>()
        {
            { TilesPreviewSizeEnum.Size32, 32 },
            { TilesPreviewSizeEnum.Size64, 64 },
            { TilesPreviewSizeEnum.Size128, 128},
            { TilesPreviewSizeEnum.Size256, 256}
        };

    // Editor Windows
    public bool AsUtilityWindow = true;

    // Preview
    public TilesPreviewSizeEnum TilesPreviewSize = TilesPreviewSizeEnum.Size64;

    // Highlight Layer
    public Color HighlightColor = Color.red;

    // Platform Effectuer Settings
    public LayerMask PlatformLayers = ~(1 << 0);
    public bool UseSurfaceArc;
    public float SurfaceArcValue = 180f;
    public bool UseSideBounce;
}