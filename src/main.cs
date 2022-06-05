using System;

public class main{
	public static void Main() {
		
		int num_of_dataset = 49;
		int row = 5;
		int column = 5;
		int input_data_length = (row+1)/2*column+(column+1)/2*row;
		int result_data_length = row*column;
		
		int[] Class_Size = new int[3]{input_data_length, 100, result_data_length};
		String[] Active_Function_List = new String[3]{"Sigmoid","Sigmoid"," "};
		ANN ann = new ANN(50, Class_Size, result_data_length, Active_Function_List);
		ann.Init();
		
		string path;
		string[] File_Data;
		double[][] Input_Data = new double[num_of_dataset][];
		double[][] Result_Data = new double[num_of_dataset][];
		for (int i = 1; i<=num_of_dataset; i++){
			path = @"/workspace/tst/dataset/input_data_"+i.ToString()+".txt";
			// 절대경로로 지정된 dataset path, 다시 수정해야됨
			File_Data  = System.IO.File.ReadAllLines(path);
			Input_Data[i-1] = ANNIO.Get_Input_Data(row, column, File_Data);
			Result_Data[i-1] = ANNIO.Get_Result_Data(row, column, File_Data);
		}
		
		ANNIO.View_Data(num_of_dataset, Input_Data, Result_Data);
		
		double a = 0.001;
		double b = 0.0005;
		
		int learning_data = 47;
	
		
		for (int count = 0; count < 1000000; count++){
			ann.Propagate(Input_Data[count%learning_data]);
			ann.Update(ann.Error_Function(Result_Data[count%learning_data], ann.Result), Input_Data[count%learning_data]);
			if (count%learning_data == 0){
				a = a*0.99995;
				ann.Set_Learning(a,b);
			}
			if (count%10000==0){
				Console.Write((double)count/10000+"%\n");
			}
		}
		
		
		int not_learned = learning_data;
		while (not_learned<num_of_dataset){
			Console.Write("Not Learned Data Set:\n");
			ANNIO.View_ANN_Nth_Data(ann, Input_Data, Result_Data, not_learned);
			not_learned++;
		}
			
		
		Console.Write("Learned data set:\n");		
		ANNIO.View_ANN_Nth_Data(ann, Input_Data, Result_Data, 0);
	}
}