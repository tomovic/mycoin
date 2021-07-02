using System;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using Echovoice.JSON;
using System.Threading.Tasks;

namespace MyCoin2
{
    public partial class Form1 : Form
    {

        string[,] config_array_type = new string[20,20];
        string[,] config_array_key = new string[20,20];
    

        string[] coin_name_check = new string[40];
        public Form1()
        {
            InitializeComponent();        
        }
        private void Load_config_file()
        {
            int counter_for_config_file_key_and_name = 0;
            int conuter_for_config_file_block = 0;
            //   var fileName = "c:/mycoin/coingecko-client-config3.json";
            var fileName = "coingecko-client-config3.json";
            byte[] data = File.ReadAllBytes(fileName);
            Utf8JsonReader reader = new Utf8JsonReader(data);

            Console.Write(reader.Read());

            while (reader.Read())
            {
                switch (reader.TokenType)
                {

                    case JsonTokenType.StartObject:
                        break;

                    case JsonTokenType.PropertyName:
                        Console.Write($"{reader.GetString()}: ");
                        config_array_type[conuter_for_config_file_block, counter_for_config_file_key_and_name] = reader.GetString();
                        break;

                    case JsonTokenType.String:
                        Console.WriteLine(reader.GetString());
                        config_array_key[conuter_for_config_file_block, counter_for_config_file_key_and_name] = reader.GetString();
                        counter_for_config_file_key_and_name++;
                        break;

                    case JsonTokenType.EndObject:
                        counter_for_config_file_key_and_name = 0;
                        conuter_for_config_file_block++;
                        break;

                }               
            }
        }



        private async void start_Click(object sender, EventArgs e)
        {
            
            Load_config_file();

            Symbol_to_ID symbol_to_ID = new Symbol_to_ID();

            // search symbol coin in file array...
          //  int uBound0 = config_array_type.GetUpperBound(0);
           // int uBound1 = config_array_type.GetUpperBound(1);

            for (int file_block = 0; file_block <= config_array_type.GetUpperBound(0); file_block++)
            {
                for (int file_line = 0; file_line <= config_array_type.GetUpperBound(1); file_line++)
                {                 
                    if(config_array_type[file_block, file_line] == "symbol")
                    {
                        Console.WriteLine("----> " + config_array_key[file_block, file_line]);
                        symbol_to_ID.search_ID_form_file[file_block] = config_array_key[file_block, file_line];
                    }
                }               
            }

            // start id search...
            symbol_to_ID.create_search();

            await Task.Delay(5000);

            ID_to_enquiry new_convert = new ID_to_enquiry();

            // from Symbol_to_ID in ID_to_enquiry 
            for (int counter_for_symbol_ID = 0; counter_for_symbol_ID <= symbol_to_ID.suitable_ID_form_api.GetUpperBound(0); counter_for_symbol_ID++)
            {
                Console.WriteLine(symbol_to_ID.suitable_ID_form_api[counter_for_symbol_ID]);
                new_convert.id_search[counter_for_symbol_ID] = symbol_to_ID.suitable_ID_form_api[counter_for_symbol_ID];
            }

            // search rest form file
            for (int file_block = 0; file_block <= config_array_key.GetUpperBound(0); file_block++)
            {
                Console.WriteLine("-------------- Here is the block-------------");
                for (int file_line = 0; file_line <= config_array_key.GetUpperBound(1); file_line++)
                {
                    {
                        if (config_array_key[file_block, file_line] == "true")
                        {
                            Console.WriteLine("is true: " + config_array_type[file_block, file_line]);
                            new_convert.search_form_file[file_block, file_line] = config_array_type[file_block, file_line];
                        }
                    }
                }

            }

            new_convert.create_search();
            
         
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            int[] a = new int[17];
            a[0] = 0;
            a[1] = 1;
            a[2] = 2;
            a[3] = 3;
            a[4] = 4;
            a[5] = 5;
            a[6] = 7;

            //    for (int file_line = 0; file_line < 4; file_line++)
            //   {

            //              Console.WriteLine(a[file_line].ToString());
            //  }

            foreach (int number in a)
            {
                Console.WriteLine(number);
                if(number == 0)
                {
                    Console.WriteLine("NNNNNNNNNNNN");
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[,] A = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 10, 11, 12 } };
          

            int j = 0;
            foreach (int i in A)
            {
                if ((j % 2) == 1)
                    Console.WriteLine(i + " ");
                else
                    Console.Write(i + " ");
                j++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string fullPath = "xxx1211212.txt";
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("Monica Rathbun \u0022");
                writer.WriteLine("Vidya Agarwal");
                writer.WriteLine("Mahesh Chand");
                writer.WriteLine("Vijay Anand");
                writer.WriteLine("Jignesh Trivedi");
            }
            // Read a file  
            string readText = File.ReadAllText(fullPath);
            Console.WriteLine(readText);

        }
    }
}
