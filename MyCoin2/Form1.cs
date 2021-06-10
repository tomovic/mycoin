using System;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using Echovoice.JSON;

namespace MyCoin2
{
    public partial class Form1 : Form
    {

        string[] config_array_type = new string[20];
        string[] config_array_key = new string[20];

        string[] coin_name_check = new string[40];
        public Form1()
        {
            InitializeComponent();        
        }
        private void Load_config_file()
        {
            int conuter_for_config_file = 0;
            var fileName = "c:/mycoin/coingecko-client-config2.json";
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
                        config_array_type[conuter_for_config_file] = reader.GetString();
                        break;

                    case JsonTokenType.String:
                        Console.WriteLine(reader.GetString());
                        config_array_key[conuter_for_config_file] = reader.GetString();
                        conuter_for_config_file++;
                        break;

                    case JsonTokenType.EndObject:                        
                        break;

                }

            }
        }

        private void Load_coin_check_file()
        {
            int conuter_for_array = 0;
            var fileName = "c:/mycoin/coingecko-check_coin.json";
            byte[] data = File.ReadAllBytes(fileName);
            Utf8JsonReader reader = new Utf8JsonReader(data);
            Console.Write(reader.Read());

            while (reader.Read())
            {

                switch (reader.TokenType)
                {
                    case JsonTokenType.StartObject:
                        break;                   

                    case JsonTokenType.String:
                        Console.WriteLine(reader.GetString());
                        coin_name_check[conuter_for_array] = reader.GetString();
                        break;

                    case JsonTokenType.EndObject:
                        conuter_for_array++;
                        break;
                }

            }
        }
      
        private async void start_Click(object sender, EventArgs e)
        {
            Load_config_file();
            Load_coin_check_file();

            Loadlist mylist = new Loadlist();
            mylist.address_for_coin_list = "https://api.coingecko.com/api/v3/coins/list?include_platform=true";
            string result = await mylist.LoaderAsync(); 
            Console.WriteLine(result); 
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
