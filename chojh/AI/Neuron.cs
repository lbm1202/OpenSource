using System;

public class Neuron{
  public int Input_Length = 16;
  public double Result;
  private string Active_Function_Name;
  public double[] Input_Signal;
  public double[] Weight;
  private double[] Update_Weight;
  private double[] Moment_Term;
  private double Bias_Input;
  public static double Threhold = 0.5;
  public static double Learning_Rate = 0.001;
  public static double Beta = 0.0005;
  public double Error;

  public void Set_Learning(double a, double b){
    Neuron.Learning_Rate = a;
    Neuron.Beta = b;
  }

  public Neuron(int Input_Length){
    this.Input_Length = Input_Length;
    this.Input_Signal = new double[Input_Length];
    this.Weight = new double[checked (Input_Length + 1)];
    this.Update_Weight = new double[checked (Input_Length + 1)];
    this.Moment_Term = new double[checked (Input_Length + 1)];
    this.Init();
  }

  public Neuron(int Input_Length, string Active_Function_Name){
    this.Input_Length = Input_Length;
    this.Input_Signal = new double[Input_Length];
    this.Weight = new double[checked (Input_Length + 1)];
    this.Update_Weight = new double[checked (Input_Length + 1)];
    this.Active_Function_Name = Active_Function_Name;
    this.Moment_Term = new double[checked (Input_Length + 1)];
    this.Init();
  }

  public void Set_Input_Length(int Length){
    this.Input_Length = Length;
    this.Input_Signal = new double[Length];
    this.Weight = new double[checked (Length + 1)];
    this.Update_Weight = new double[checked (this.Input_Length + 1)];
    this.Moment_Term = new double[checked (this.Input_Length + 1)];
    this.Init();
  }

  public void Init(){
    double num = 1.0 / Math.Sqrt((double) this.Input_Length);
    double MAX = num;
    this.Weight = Util.NotOverRandomArray(checked (this.Input_Length + 1), -num, MAX);
    int index = 0;
    while (index < this.Moment_Term.Length){
      this.Moment_Term[index] = 0.0;
      checked { ++index; }
    }
    this.Bias_Input = 1.0;
  }

  public double Propagate(double[] Signal){
    double num = 0.0;
    int index = 0;
    while (index < Signal.Length){
      num += Signal[index] * this.Weight[index];
      this.Input_Signal[index] = Signal[index];
      checked { ++index; }
    }
    double X = num + this.Bias_Input * this.Weight[this.Input_Length];
    switch (this.Active_Function_Name){
      case "TanH":
        this.Result = this.TanH(X);
        break;
      case "Sigmoid":
        this.Result = this.Sigmoid(X);
        break;
      case "ReLU":
        this.Result = this.ReLU(X);
        break;
      case "Linear":
        this.Result = this.Linear(X);
        break;
      default:
        this.Result = this.TanH(X);
        break;
    }
    return this.Result;
  }

  public void Propagate_Thread() => this.Result = this.Propagate(this.Input_Signal);

  public void Update_Commit(){
    int index = 0;
    while (index < this.Input_Length)
    {
      this.Weight[index] = this.Update_Weight[index];
      checked { ++index; }
    }
    this.Weight[this.Input_Length] = this.Update_Weight[this.Input_Length];
  }

  public void BP_Update(double Error_Delta){
    double num;
    switch (this.Active_Function_Name){
      case "TanH":
        num = 1.0 - Math.Pow(this.Result, 2.0);
        break;
      case "Sigmoid":
        num = this.Result * (1.0 - this.Result);
        break;
      case "ReLU":
        num = 1.0;
        break;
      case "Linear":
        num = 1.0;
        break;
      default:
        num = 1.0 - Math.Pow(this.Result, 2.0);
        break;
    }
    int index = 0;
    while (index < this.Input_Length){
      this.Weight[index] = this.Weight[index] + Neuron.Learning_Rate * Error_Delta * this.Input_Signal[index] * num;
      checked { ++index; }
    }
    this.Weight[this.Input_Length] = this.Weight[this.Input_Length] + Neuron.Learning_Rate * Error_Delta * this.Bias_Input * num;
    this.Error = Error_Delta;
  }

  public double Distance_Function(double[] Signal){
    double num = 0.0;
    int index = 0;
    while (index < Signal.Length){
      num += this.Weight[index] - Signal[index];
      checked { ++index; }
    }
    return num + (this.Weight[this.Input_Length] - 1.0);
  }

  public void Self_Organizing(double[] Signal){
    this.Propagate(Signal);
    double num = 1.0;
    int index = 0;
    while (index < Signal.Length){
      this.Weight[index] = this.Weight[index] + Neuron.Beta * (Signal[index] - this.Weight[index]) * 0.5 * num;
      checked { ++index; }
    }
    this.Weight[this.Input_Length] = this.Weight[this.Input_Length] + Neuron.Learning_Rate * (1.0 - this.Weight[this.Input_Length]) * num;
  }

  public void Hebb_Rule(double[] Signal){
    double num1 = this.Propagate(Signal);
    double num2 = 1.0;
    int index = 0;
    while (index < Signal.Length){
      this.Weight[index] = this.Weight[index] + Neuron.Learning_Rate * num1 * (Neuron.Beta * Signal[index] - this.Weight[index]) * num2;
      checked { ++index; }
    }
    this.Weight[this.Input_Length] = this.Weight[this.Input_Length] + Neuron.Learning_Rate * num1 * (Neuron.Beta * this.Bias_Input - this.Weight[this.Input_Length]) * num2;
  }

  private double TanH(double X) => Math.Tanh(X);

  private double Sigmoid(double X) => 1.0 / (1.0 + Math.Exp(-X));

  private double ReLU(double X) => X > 0.0 ? X : 0.0;

  private double Linear(double X) => X;

  public void Show_Weight(){
    int index = 0;
    while (index < this.Weight.Length){
      Console.Write(Math.Round(this.Weight[index], 6).ToString() + " ");
      checked { ++index; }
    }
    Console.WriteLine();
  }
}
