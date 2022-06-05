using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
public class ANNIO{
	public static double[] Get_Input_Data(int row, int column, String[] File_Data){
		double[] Input_Data = new double[(row+1)/2*column+(column+1)/2*row];
		int count = 0;
		for (int index1 = 0; index1<File_Data.Length; index1++){
			string[] One_Line = File_Data[index1].Split(' ');
			for (int index2 = 0; index2<One_Line.Length; index2++){
				if (index1<(row+1)/2){
					if (index2>=(column+1)/2){
						Input_Data[count] = double.Parse(One_Line[index2]);
						count++;
					}
				}
				else{
					if (index2<(column+1)/2){
						Input_Data[count] = double.Parse(One_Line[index2]);
						count++;
					}
				}
			}
		}
		return Input_Data;
	}
	
	public static double[] Get_Result_Data(int row, int column, String[] File_Data){
		double[] Result_Data = new double[row*column];
		int count = 0;
		for (int index1 = 0; index1<File_Data.Length; index1++){
			string[] One_Line = File_Data[index1].Split(' ');
			for (int index2 = 0; index2<One_Line.Length; index2++){
				if (index1>=(row+1)/2){
					if (index2>=(column+1)/2){
						Result_Data[count] = double.Parse(One_Line[index2]);
						count++;
					}
				}
			}
		}
		return Result_Data;
	}
	
	public static void View_Data(int num_of_dataset, double[][] Input_Data, double[][] Result_Data){
		int count1 = 0;
		int count2 = 0;
		for (int index3 = 0; index3<num_of_dataset; index3++){
            Console.Write("Data set["+(index3+1)+"]\n");
			for (int index1 = 0; index1<8; index1++){
				for (int index2 = 0; index2<8; index2++){
                    if(index1<3){
                        if(index2<3){
                            Console.Write("0 ");
                        }
                        else{
                            Console.Write(Input_Data[index3][count1]+" ");
                            count1++;
                	    }
                	}
                    
                    else{
                        if(index2<3){
                            Console.Write(Input_Data[index3][count1]+" ");
                            count1++;
                        }
                        else{
                            Console.Write(Result_Data[index3][count2]+" ");
                            count2++;
                        }
					
                    }

                }
			    Console.Write("\n");
            }
            Console.Write("\n");
            count1 = 0;
			count2 = 0;
		}
	}
	
	public static void View_ANN_Nth_Data(ANN ann, double[][] Input_Data, double[][] Result_Data, int N){
		ann.Propagate(Input_Data[N]);
		for (int index = 0; index < ann.Result.Length; index++){
			Console.Write(Math.Round(ann.Result[index], 3).ToString() + "  ");
		}
		Console.Write("\n\n -Nth Result- \n\n");
		for (int index = 0; index < ann.Result.Length; index++){
			Console.Write(Math.Round(ann.Error_Function(Result_Data[N], ann.Result)[index], 3).ToString() + "  ");
		}
		Console.Write("\n\n -Nth Error Rate- \n\n");




		Console.Write(N+"th data set");
		ann.Propagate(Input_Data[N]);
		int indexcount = 0;
		while (indexcount<15){
			if (indexcount%5==0){
				Console.Write("\n0 0 0 ");
			}
			Console.Write(Input_Data[N][indexcount]+" ");
			indexcount++;
		}
		for (int index = 0; index<ann.Result.Length; index++){
			if (index%5==0){
				Console.Write("\n");
				for(int i = 0; i<3; i++){
				Console.Write(Input_Data[N][indexcount]+" ");
				indexcount++;
				}
			}
			if (ann.Result[index]>=0.5){
				Console.Write("1 ");
			}
			else{
				Console.Write("0 ");
			}
		}

		Console.Write("\n\n\n");
	}
	
	
}