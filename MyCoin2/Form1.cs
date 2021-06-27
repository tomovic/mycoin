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
            for (int file_block = 0; file_block < 3; file_block++)
            {
                for (int file_line = 0; file_line < 4; file_line++)
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
            for (int a = 0; a < 3; a++)
            {
                Console.WriteLine(symbol_to_ID.suitable_ID_form_api[a]);
                new_convert.id_search[a] = symbol_to_ID.suitable_ID_form_api[a];
            }

            // search rest form file
            for (int file_block = 0; file_block < 3; file_block++)
            {
                Console.WriteLine("-------------- Here is the block-------------");
                for (int file_line = 0; file_line < 4; file_line++)
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
    
            

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {           
        }
    }
}
