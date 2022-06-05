using System;

public class Full_Connected_NN{
  public Neuron_Layer[] NL;
  public double[] Result;
  private string[] Active_Function_List;
  private int Class_Length;
  private int BP_Limit;
  private Util util = new Util();

  public void Set_Learning(double a, double b){
    int index = 0;
    while (index < this.NL.Length){
      this.NL[index].Set_Learning(a, b);
      checked { ++index; }
    }
  }

  public Full_Connected_NN(int Result_Length, int[] Class_Size){
    Console.WriteLine("Full Connected NN Construct Start\n");
    this.Class_Length = checked (Class_Size.Length - 1);
    this.NL = new Neuron_Layer[checked (Class_Size.Length - 1)];
    this.Result = new double[Result_Length];
    this.BP_Limit = checked (this.NL.Length - 3) < 3 ? 0 : checked (this.NL.Length - 3);
    Console.WriteLine("Full_Connected_NN Input Length  " + Class_Size[0].ToString());
    Console.WriteLine("Full Connected NN Class_Length " + this.Class_Length.ToString() + "\n");
    Class_Size[checked (Class_Size.Length - 1)] = Result_Length;
    Console.WriteLine("Full Connected NN Result_Length " + Result_Length.ToString());
    int index1 = 0;
    while (index1 < this.Class_Length){
      Console.WriteLine("Layer " + index1.ToString() + " Input Length " + Class_Size[index1].ToString() + " Layer Length " + Class_Size[checked (index1 + 1)].ToString());
      this.NL[index1] = new Neuron_Layer(Class_Size[index1], Class_Size[checked (index1 + 1)]);
      checked { ++index1; }
    }
    Console.WriteLine("\n\n");
    this.Init();
    double[] Signal = new double[Class_Size[0]];
    double num1 = 1.0;
    double num2 = -1.0;
    int index2 = 0;
    while (index2 < Class_Size[0]){
      Signal[index2] = Util.r.NextDouble() * (num1 - num2) + num2;
      checked { ++index2; }
    }
    double time = this.util.GetTime();
    double[] numArray = this.Propagate_SingleThreading(Signal);
    Console.WriteLine("Full_Connected_NN Working Test Time " + (this.util.GetTime() - time).ToString() + "ms");
    int index3 = 0;
    while (index3 < numArray.Length){
      Console.Write(Math.Round(numArray[index3], 3).ToString() + "  ");
      checked { ++index3; }
    }
    Console.WriteLine("\n\n");
    Console.WriteLine("Full Connected NN Construct Completed \n");
  }

  public Full_Connected_NN(int Result_Length, int[] Class_Size, string[] Active_Function_Name_List){
    Console.WriteLine("Full Connected NN Construct Start\n");
    this.Class_Length = checked (Class_Size.Length - 1);
    this.NL = new Neuron_Layer[checked (Class_Size.Length - 1)];
    this.Result = new double[Result_Length];
    this.Active_Function_List = Active_Function_Name_List;
    this.BP_Limit = checked (this.NL.Length - 3) < 3 ? 0 : checked (this.NL.Length - 3);
    Console.WriteLine("Full_Connected_NN Input Length  " + Class_Size[0].ToString());
    Console.WriteLine("Full Connected NN Class_Length " + this.Class_Length.ToString() + "\n");
    Class_Size[checked (Class_Size.Length - 1)] = Result_Length;
    Console.WriteLine("Full Connected NN Result_Length " + Result_Length.ToString());
    int index1 = 0;
    while (index1 < this.Class_Length){
      Console.WriteLine("Layer " + index1.ToString() + " Input Length " + Class_Size[index1].ToString() + " Layer Length " + Class_Size[checked (index1 + 1)].ToString());
      this.NL[index1] = new Neuron_Layer(Class_Size[index1], Class_Size[checked (index1 + 1)], Active_Function_Name_List[index1]);
      checked { ++index1; }
    }
    Console.WriteLine("\n\n");
    this.Init();
    double[] Signal = new double[Class_Size[0]];
    double num1 = 1.0;
    double num2 = -1.0;
    int index2 = 0;
    while (index2 < Class_Size[0]){
      Signal[index2] = Util.r.NextDouble() * (num1 - num2) + num2;
      checked { ++index2; }
    }
    double time = this.util.GetTime();
    double[] numArray = this.Propagate_SingleThreading(Signal);
    Console.WriteLine("Full_Connected_NN Working Test Time " + (this.util.GetTime() - time).ToString() + "ms");
    int index3 = 0;
    while (index3 < numArray.Length){
      Console.Write(Math.Round(numArray[index3], 3).ToString() + "  ");
      checked { ++index3; }
    }
    Console.WriteLine("\n\n");
    Console.WriteLine("Full Connected NN Construct Completed \n");
  }

  public void Init(){
    int index = 0;
    while (index < this.NL.Length){
      this.NL[index].Init();
      checked { ++index; }
    }
    Util.InitRandom();
  }

  public double[] Propagate_SingleThreading(double[] Signal){
    double[] Signal1 = Signal;
    int index = 0;
    while (index < this.Class_Length){
      Signal1 = this.NL[index].Propagate_SingleThreading(Signal1);
      checked { ++index; }
    }
    return Signal1;
  }

  public void Update_Commit(){
    int index = 0;
    while (index < this.NL.Length){
      this.NL[index].Update_Commit();
      checked { ++index; }
    }
  }

  public void Update(double[] Error, double[] Signal) => this.BP_Update(Error);

  public void Update(double[] Signal){
    this.NL[0].Hebb_Rule(Signal);
    double[] Signal1 = this.NL[0].Propagate_SingleThreading(Signal);
    int index = 1;
    while (index < this.NL.Length){
      this.NL[index].Hebb_Rule(Signal1);
      Signal1 = this.NL[index].Propagate_SingleThreading(Signal1);
      checked { ++index; }
    }
  }

  public void BP_Update(double[] Error){
    this.NL[checked (this.Class_Length - 1)].BP_Update(Error);
    int index = checked (this.NL.Length - 2);
    while (index >= this.BP_Limit){
      this.Layer_BP_Update(this.NL[index].N, this.NL[checked (index + 1)].N);
      checked { --index; }
    }
  }

  public void BP_Update(double[] Error){
    this.NL[checked (this.Class_Length - 1)].BP_Update(Error);
    int index = checked (this.NL.Length - 2);
    while (index >= 0){
      this.Layer_BP_Update(this.NL[index].N, this.NL[checked (index + 1)].N);
      checked { --index; }
    }
  }

  public void Layer_BP_Update(Neuron[] N, Neuron[] Next){
    double num = 0.0;
    int index1 = 0;
    while (index1 < N.Length){
      double Error_Delta = 0.0;
      int index2 = 0;
      while (index2 < Next.Length){
        Error_Delta += Next[index2].Error * Next[index2].Weight[index1];
        checked { ++index2; }
      }
      N[index1].BP_Update(Error_Delta);
      num = 0.0;
      checked { ++index1; }
    }
  }

  public void SOM_Update(double[] Signal){
    this.NL[0].Self_Organizing(Signal);
    double[] Signal1 = this.NL[0].Propagate_SingleThreading(Signal);
    int index = 1;
    while (index < this.BP_Limit){
      this.NL[index].Self_Organizing(Signal1);
      Signal1 = this.NL[index].Propagate_SingleThreading(Signal1);
      checked { ++index; }
    }
  }

  public void Hebb_Update(double[] Signal){
    this.NL[0].Hebb_Rule(Signal);
    double[] Signal1 = this.NL[0].Propagate_SingleThreading(Signal);
    int index = 1;
    while (index < this.BP_Limit){
      this.NL[index].Hebb_Rule(Signal1);
      Signal1 = this.NL[index].Propagate_SingleThreading(Signal1);
      checked { ++index; }
    }
  }

  public void Hebb_With_SOM(double[] Signal){
    this.NL[0].Hebb_With_SOM(Signal);
    double[] Signal1 = this.NL[0].Propagate_SingleThreading(Signal);
    int index = 1;
    while (index < this.BP_Limit){
      this.NL[index].Hebb_With_SOM(Signal1);
      Signal1 = this.NL[index].Propagate_SingleThreading(Signal1);
      checked { ++index; }
    }
  }

  public void Show_Weight(){
    int index = 0;
    while (index < this.Class_Length){
      this.NL[index].Show_Weight();
      checked { ++index; }
    }
    Console.WriteLine();
  }

  public double Get_Weight(int Layer, int Enable_Neuron, int Enable_Weight) => this.NL[Layer].N[Enable_Neuron].Weight[Enable_Weight];

  public void Set_Weight(int Layer, int Enable_Neuron, int Enable_Weight, double Data) => this.NL[Layer].N[Enable_Neuron].Weight[Enable_Weight] = Data;
}
