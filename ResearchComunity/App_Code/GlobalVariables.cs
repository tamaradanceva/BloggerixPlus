using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GlobalVariables
/// </summary>

public static class GlobalVariables
{
    public static Hashids Hash = new Hashids("5gfyh6dsv%vs*", 8);

    public static int getInt(int [] array)
    {
        int id = 0;
        int multiplier=1;
        for (int i = 0; i < array.Length; i++) {
            id += multiplier * array[i];
            multiplier *= 10;
        }
            return id;
    }
    public static int[] getArray(int i) {
        int num = i.ToString().Trim().Length;
        int[] array = null;
        if (num > 0)
        {
            array = new int[num];

            if (i > 0)
            {
                int counter = 1;
                while (i > 0)
                {
                    int digit = i % 10;
                    i = (int)i / 10;
                    array[counter - 1] = digit;
                    counter++;

                }
            }
        }

        return array;
    }
}
