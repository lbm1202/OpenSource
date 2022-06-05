using System;

public class Neuron_Layer{
  public Neuron[] N;
  public static double Alpha = 0.15;
  public double[] Result;
  public int Neuron_Length;
  public int Input_Length;
  private string Active_Function_List;
  private int Update_Radius;
  private int Enbale_Neuron;

  public void Set_Learning(double a, double b){
    int index = 0;
    while (index < this.N.Length){
      this.N[index].Set_Learning(a, b);
      checked { ++index; }
    }
  }

  public Neuron_Layer(int Input_Length, int Neuron_Length){
    this.Input_Length = Input_Length;
    this.Neuron_Length = Neuron_Length;
    this.Enbale_Neuron = checked ((int) unchecked ((double) Neuron_Length * Neuron_Layer.Alpha));
    if (this.Enbale_Neuron < 10)
      this.Enbale_Neuron = Neuron_Length;
    this.Update_Radius = checked ((int) unchecked ((double) this.Enbale_Neuron * 0.5));
    if (this.Update_Radius < 1)
      this.Update_Radius = 1;
    this.Result = new double[Neuron_Length];
    this.N = new Neuron[Neuron_Length];
    int index = 0;
    while (index < Neuron_Length){
      this.N[index] = new Neuron(Input_Length);
      checked { ++index; }
    }
    Console.WriteLine("뉴런 길이=" + Neuron_Length.ToString() + "  활성반경=" + this.Update_Radius.ToString());
  }

  public Neuron_Layer(int Input_Length, int Neuron_Length, string Active_Function_Name){
    this.Input_Length = Input_Length;
    this.Neuron_Length = Neuron_Length;
    this.Active_Function_List = Active_Function_Name;
    this.Enbale_Neuron = checked ((int) unchecked ((double) Neuron_Length * Neuron_Layer.Alpha));
    if (this.Enbale_Neuron < 10)
      this.Enbale_Neuron = Neuron_Length;
    this.Update_Radius = checked ((int) unchecked ((double) this.Enbale_Neuron * 0.5));
    if (this.Update_Radius < 1)
      this.Update_Radius = 1;
    this.Result = new double[Neuron_Length];
    this.N = new Neuron[Neuron_Length];
    int index = 0;
    while (index < Neuron_Length){
      this.N[index] = new Neuron(Input_Length, Active_Function_Name);
      checked { ++index; }
    }
    Console.WriteLine("뉴런 길이=" + Neuron_Length.ToString() + "  활성반경=" + this.Update_Radius.ToString() + " 활성함수= " + Active_Function_Name);
  }

  public void Neuron_Input_Length_Set(int Length){
    int index = 0;
    while (index < this.N.Length){
      this.N[index].Set_Input_Length(Length);
      checked { ++index; }
    }
  }

  public void Init(){
    int index = 0;
    while (index < this.N.Length){
      this.N[index].Init();
      checked { ++index; }
    }
  }

  public double[] Propagate_SingleThreading(double[] Signal){
    int index1 = 0;
    while (index1 < this.Neuron_Length){
      this.Result[index1] = this.N[index1].Propagate(Signal);
      checked { ++index1; }
    }
    switch (this.Active_Function_List){
      case "Softmax":
        double num1 = 0.0;
        int index2 = 0;
        while (index2 < this.Neuron_Length){
          num1 += Math.Exp(this.Result[index2]);
          checked { ++index2; }
        }
        int index3 = 0;
        while (index3 < this.Neuron_Length){
          this.Result[index3] = Math.Exp(this.Result[index3]) / num1;
          checked { ++index3; }
        }
        break;
      case "Max":
        double num2 = this.Result[0];
        int index4 = 0;
        int index5 = 0;
        while (index5 < this.Neuron_Length){
          if (num2 < this.Result[index5]){
            num2 = this.Result[index5];
            index4 = index5;
          }
          this.Result[index5] = 0.0;
          checked { ++index5; }
        }
        this.Result[index4] = 1.0;
        break;
    }
    return this.Result;
  }

  public void Update_Commit(){
    int index = 0;
    while (index < this.N.Length){
      this.N[index].Update_Commit();
      checked { ++index; }
    }
  }

  public void BP_Update(double[] Error){
    int index = 0;
    while (index < this.N.Length){
      this.N[index].BP_Update(Error[index]);
      checked { ++index; }
    }
  }

  public void BP_Update(double Error_Delta){
    int index = 0;
    while (index < this.Neuron_Length){
      this.N[index].BP_Update(Error_Delta);
      checked { ++index; }
    }
  }

  public void Self_Organizing(double[] Signal){
    double num1 = this.N[0].Distance_Function(Signal);
    int index1 = 0;
    int index2 = 0;
    while (index2 < this.N.Length){
      double num2 = this.N[index2].Distance_Function(Signal);
      if (num1 < num2){
        num1 = num2;
        index1 = index2;
      }
      checked { ++index2; }
    }
    this.N[index1].Self_Organizing(Signal);
    if (checked (index1 - this.Update_Radius) > 0){
      int index3 = checked (index1 - this.Update_Radius);
      while (index3 < index1){
        this.N[index3].Self_Organizing(Signal);
        checked { ++index3; }
      }
    }
    else{
      int index4 = 0;
      while (index4 < index1){
        this.N[index4].Self_Organizing(Signal);
        checked { ++index4; }
      }
    }
    if (checked (index1 + this.Update_Radius) < this.Neuron_Length){
      int index5 = checked (index1 + 1);
      while (index5 < checked (index1 + this.Update_Radius)){
        this.N[index5].Self_Organizing(Signal);
        checked { ++index5; }
      }
    }
    else{
      int index6 = checked (index1 + 1);
      while (index6 < this.Neuron_Length){
        this.N[index6].Self_Organizing(Signal);
        checked { ++index6; }
      }
    }
  }

  public void Hebb_With_SOM(double[] Signal){
    double num1 = this.N[0].Distance_Function(Signal);
    int index1 = 0;
    int index2 = 0;
    while (index2 < this.N.Length){
      double num2 = this.N[index2].Distance_Function(Signal);
      if (num1 < num2){
        num1 = num2;
        index1 = index2;
      }
      checked { ++index2; }
    }
    this.N[index1].Hebb_Rule(Signal);
    if (checked (index1 - this.Update_Radius) > 0){
      int index3 = checked (index1 - this.Update_Radius);
      while (index3 < index1){
        this.N[index3].Hebb_Rule(Signal);
        checked { ++index3; }
      }
    }
    else{
      int index4 = 0;
      while (index4 < index1){
        this.N[index4].Hebb_Rule(Signal);
        checked { ++index4; }
      }
    }
    if (checked (index1 + this.Update_Radius) < this.Neuron_Length){
      int index5 = checked (index1 + 1);
      while (index5 < checked (index1 + this.Update_Radius)){
        this.N[index5].Hebb_Rule(Signal);
        checked { ++index5; }
      }
    }
    else{
      int index6 = checked (index1 + 1);
      while (index6 < this.Neuron_Length){
        this.N[index6].Hebb_Rule(Signal);
        checked { ++index6; }
      }
    }
  }

  public void Hebb_Rule(double[] Signal){
    int index = 0;
    while (index < this.N.Length){
      this.N[index].Hebb_Rule(Signal);
      checked { ++index; }
    }
  }

  public void Show_Weight(){
    int index = 0;
    while (index < this.Neuron_Length){
      this.N[index].Show_Weight();
      checked { ++index; }
    }
    Console.WriteLine();
  }
}
