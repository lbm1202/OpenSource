using System;
using System.Collections.Generic;

public class Util{
  private static List<double> Random_Number_List = new List<double>();
  public static Random r = new Random();

  public static void InitRandom() => Util.Random_Number_List.Clear();

  private static bool Already_Number(double rnd){
    int index = 0;
    while (index < Util.Random_Number_List.Count){
      if (Util.Random_Number_List[index] == rnd)
        return true;
      checked { ++index; }
    }
    return false;
  }

  public static double NotOverRandomRange(double min, double MAX){
    double rnd = Util.RandomRange(min, MAX);
    while (Util.Already_Number(rnd))
      rnd = Util.RandomRange(min, MAX);
    return rnd;
  }

  public static double RandomRange(double min, double MAX) => Util.r.NextDouble() * (MAX - min) + min;

  public static int RandomRange(int min, int MAX) => Util.r.Next(min, MAX);

  public static double[] RandomArray(int Length, double min, double MAX){
    double[] numArray = new double[Length];
    int index = 0;
    while (index < Length){
      numArray[index] = Util.RandomRange(min, MAX);
      checked { ++index; }
    }
    return numArray;
  }

  public static double[] NotOverRandomArray(int Length, double min, double MAX){
    double[] numArray = new double[Length];
    int index = 0;
    while (index < Length){
      numArray[index] = Util.NotOverRandomRange(min, MAX);
      checked { ++index; }
    }
    return numArray;
  }

  public static int[] RandomArray(int Length, int min, int MAX){
    int[] numArray = new int[Length];
    int index = 0;
    while (index < Length){
      numArray[index] = Util.RandomRange(min, MAX);
      checked { ++index; }
    }
    return numArray;
  }

  public double[,] Randeom_2DArray(int Width, int Height, double min, double MAX){
    double[,] numArray = new double[Height, Width];
    int index1 = 0;
    while (index1 < Height){
      int index2 = 0;
      while (index2 < Width){
        numArray[index1, index2] = Util.RandomRange(min, MAX);
        numArray[index1, index2] = Math.Round(numArray[index1, index2], 4);
        checked { ++index2; }
      }
      checked { ++index1; }
    }
    return numArray;
  }

  public double GetTime() => (double) DateTime.Now.Millisecond;

  public static double map(
    double x,
    double in_min,
    double in_max,
    double out_min,
    double out_max){
    return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
  }

  public static double[] map_arr(
    double[] x,
    double in_min,
    double in_max,
    double out_min,
    double out_max){
    double[] numArray = new double[x.Length];
    int index = 0;
    while (index < numArray.Length){
      numArray[index] = Util.map(x[index], in_min, in_max, out_min, out_max);
      checked { ++index; }
    }
    return numArray;
  }

  public static double[] AddArray(double[] A, double[] B){
    double[] numArray = new double[A.Length];
    int index = 0;
    while (index < A.Length)
    {
      numArray[index] = A[index] + B[index];
      checked { ++index; }
    }
    return numArray;
  }

  public static void Copy_Array(double[] Source, double[] Destination){
    int index = 0;
    while (index < Source.Length)
    {
      Destination[index] = Source[index];
      checked { ++index; }
    }
  }

  public static void Copy_Array(int[] Source, int[] Destination){
    int index = 0;
    while (index < Source.Length)
    {
      Destination[index] = Source[index];
      checked { ++index; }
    }
  }

  public static void InitArray(double[] Array, double Init){
    int index = 0;
    while (index < Array.Length){
      Array[index] = Init;
      checked { ++index; }
    }
  }

  public static void Show_Array(double[] Array){
    Console.WriteLine();
    int index = 0;
    while (index < Array.Length){
      Console.Write(Array[index].ToString() + "  ");
      checked { ++index; }
    }
    Console.WriteLine("\n\n");
  }
}
