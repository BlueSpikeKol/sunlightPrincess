using System;
using UnityEngine;

public static class Utils
{
    public static int EnumCount<T>()
    {
        return Enum.GetValues(typeof(T)).Length;
    }

    public static string EnumString(Enum e)
    {
        return Enum.GetName(e.GetType(), e);
    }

    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
        
    public static Vector3 GetCameraCenterPosition(Transform tr, Vector3 target)
    {
        Vector3 cameraEnd;

        MathUtilities.LinePlaneIntersection(out cameraEnd, target, -tr.forward, Vector3.up,
            tr.position);

        return cameraEnd;
    }

    public static string GetResourcesPath(string filename)
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Resources/" + filename;
#elif UNITY_ANDROID
        return Application.persistentDataPath + filename;
#elif UNITY_IPHONE
        return GetiPhoneDocumentsPath()+"/" + filename;
#else
        return Application.dataPath +"/" + filename;
#endif
    }
}


