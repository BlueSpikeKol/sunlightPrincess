﻿using System.Collections.Generic;
using Random = System.Random;

public static class Collections
{
    // Taken from: http://stackoverflow.com/questions/5383498/shuffle-rearrange-randomly-a-liststring
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        Random rnd = new Random();
        while(n > 1)
        {
            int k = (rnd.Next(0, n) % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
