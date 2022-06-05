using System;

public class ANN
{
  private Full_Connected_NN FCNN;
  public double[] Result;

  public void Set_Learning(double a, double b) => this.FCNN.Set_Learning(a, b);

  public ANN(int Data_Length, int[] Class_Size, int Result_Length){
    Console.WriteLine("Neural Network Construct Start \n\n");
    this.Result = new double[Result_Length];
    this.FCNN = new Full_Connected_NN(Result_Length, Class_Size);
    Console.WriteLine("Neural Network Construct Completed \n\n");
  }

  public ANN(
    int Data_Length,
    int[] Class_Size,
    int Result_Length,
    string[] Active_Function_List){
    Console.WriteLine("Neural Network Construct Start \n\n");
    this.Result = new double[Result_Length];
    this.FCNN = new Full_Connected_NN(Result_Length, Class_Size, Active_Function_List);
    Console.WriteLine("Neural Network Construct Completed \n\n");
  }

  public void Init(){
    if (this.FCNN == null)
      return;
    this.FCNN.Init();
  }

  public double[] Propagate(double[] Raw_Image){
    this.Result = this.FCNN.Propagate_SingleThreading(Raw_Image);
    return this.Result;
  }

  public void Update_Commit() => this.FCNN.Update_Commit();

  public void Update(double[] Error, double[] Signal) => this.FCNN.Update(Error, Signal);

  public double[] Error_Function(double[] Target, double[] Result){
    double[] numArray = new double[Target.Length];
    for (int index = 0; index < numArray.Length; ++index)
      numArray[index] = Target[index] - Result[index];
    return numArray;
  }

  public double Cost_Function(double[] Target, double[] Result){
    double num = 0.0;
    for (int index = 0; index < Target.Length; ++index)
      num += Math.Pow(Target[index] - Result[index], 2.0);
    return num;
  }

  public void Show_Weight() => this.FCNN.Show_Weight();

  public double Get_Weight(int Layer, int Enable_Neuron, int Enable_Weight) => this.FCNN.Get_Weight(Layer, Enable_Neuron, Enable_Weight);

  public void Set_Weight(int Layer, int Enable_Neuron, int Enable_Weight, double Data) => this.FCNN.Set_Weight(Layer, Enable_Neuron, Enable_Weight, Data);
}
