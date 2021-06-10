using System;
using System.Threading.Tasks;
using System.Net.Http;
using Echovoice.JSON;

namespace MyCoin2
{
    internal class Loadlist
    {
        public String address_for_coin_list;
        public String result_from_coinlist_Server;

        public string[] coin_list_id_array = new string[8000];
        public string[] coin_list_name_array = new string[8000];
        public async Task<string> LoaderAsync()
        {
           
            using (var httpClient = new HttpClient())
                result_from_coinlist_Server = await httpClient.GetStringAsync(address_for_coin_list);
            //  Console.Write(result);

            string[] coint_list_json = JSONDecoders.DecodeJsStringArray(result_from_coinlist_Server);

            string coin_id;
            string coin_name;

            int counter_for_coin_list_arry = 0;

            for (int a = 0; a < 30000; a++)
            {

                if (coint_list_json[a][0].ToString() == "{")
                {
                    coin_id = coint_list_json[a].Remove(coint_list_json[a].Length - 1, 1);
                    coin_id = coin_id.Remove(0, 7);
                    coin_list_id_array[counter_for_coin_list_arry] = coin_id;
                    Console.WriteLine(coin_id);

                    a = a + 2;
                    coin_name = coint_list_json[a].Remove(0, 7);
                    coin_list_name_array[counter_for_coin_list_arry] = coin_name;
                    Console.WriteLine(coin_name);

                    counter_for_coin_list_arry++;
                }
            }
            return result_from_coinlist_Server;
        } 
    }

    
}