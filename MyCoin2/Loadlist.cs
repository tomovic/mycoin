using System;
using System.Threading.Tasks;
using System.Net.Http;
using Echovoice.JSON;

namespace MyCoin2
{
    internal class ID_to_enquiry
    {
        public string[] id_search = new string[10];
        public string[,] search_return = new string[3,25];
        public string[,] search_form_file = new string[3, 25];
        public string[,] master_result = new string[3, 4];

        public async void create_search()
        {
            Geko_API myvari = new Geko_API();         

            for (int a = 0; a < 3; a++)
            {
                await Task.Delay(1000);
                myvari.address_for_coin_list = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=eur&ids=" + id_search[a] + "&order=market_cap_descmarket_cap_desc%2C%20gecko_desc%2C%20gecko_asc%2C%20market_cap_asc%2C%20market_cap_desc%2C%20volume_asc%2C%20volume_desc%2C%20id_asc%2C%20id_desc&per_page=100&page=1&sparkline=true&price_change_percentage=1h";

                string result = await myvari.LoaderAsync();

                string[] coin_list_json = JSONDecoders.DecodeJsStringArray(result);

                for (int b = 0; b < 25; b++)
                {
                    search_return[a, b] = coin_list_json[b];
                }

                Console.WriteLine("--"+ a.ToString() +"----"+ id_search[a] + "--");
                Console.WriteLine(myvari.address_for_coin_list); 
                Console.WriteLine(result);
            }

            // search element form file
            Console.WriteLine("---form file--");
            Console.WriteLine(search_form_file[1, 1]);
            Console.WriteLine("---form file-ende-");

           
            for(int file_block = 0; file_block < 3; file_block++)
            {
                Console.WriteLine("----------------");
                Console.WriteLine("-x-> id: " + id_search[file_block]);
                for (int coinline_form_web = 0; coinline_form_web < 25; coinline_form_web++)     
                {
                
                    for (int search_line_form_file = 0; search_line_form_file < 4; search_line_form_file++)
                    {
                        string[] content_for_savefile = search_return[file_block, coinline_form_web].Split(new Char[] { ':' });

                        if (content_for_savefile[0].Remove(content_for_savefile[0].Length - 1, 1) == search_form_file[file_block, search_line_form_file])
                        {
                            
                            Console.WriteLine("--x-->" + content_for_savefile[0] + " : " + content_for_savefile[1]);
                        }
                    }
                    
                }
            }


        }

    }

    internal class Symbol_to_ID
    {
       
        public string[] search_ID_form_file = new string[10];
        public string[] suitable_ID_form_api = new string[10];        

        public async void create_search()
        {
            
            Geko_API mylist = new Geko_API();
            mylist.address_for_coin_list = "https://api.coingecko.com/api/v3/coins/list?include_platform=true";
            string result = await mylist.LoaderAsync();

            
            string[] coint_list_json = JSONDecoders.DecodeJsStringArray(result);
            string coin_id;
            string[,] coin_list_array = new string[9000, 8];

        int counter_for_coin_list_arry = 0;

            for (int a = 0; a < 30000; a++)
            {

                if (coint_list_json[a][0].ToString() == "{")
                {

                    if (coint_list_json[a].Substring(coint_list_json[a].Length - 1) == "}")
                    {
                        coin_id = coint_list_json[a].Remove(0, 2);
                        coin_list_array[counter_for_coin_list_arry, 0] = coin_id;   // char 2 with }
                        Console.WriteLine(1 + " " + coin_id + " end");  // char with }
                    }
                    else
                    {
                        coin_id = coint_list_json[a].Remove(0, 2);
                        coin_id = coin_id.Substring(0, coin_id.Length - 1);
                        coin_list_array[counter_for_coin_list_arry, 0] = coin_id;   // char 2 no }
                        Console.WriteLine(1 + " " + coin_id);  // char with no  }

                        for (int b = 1; b < 7; b++)
                        {
                            if (coint_list_json[a + b].Substring(coint_list_json[a + b].Length - 1) == "}") // CHECK 2 char -> }
                            {
                                coin_list_array[counter_for_coin_list_arry, b] = coint_list_json[a + b];   // char 2 no }
                                Console.WriteLine(1 + b + " " + coint_list_json[a + b] + " end");  // char with }
                                break;
                            }
                            else
                            {
                                coin_list_array[counter_for_coin_list_arry, b] = coint_list_json[a + b];   // char 2 no }
                                Console.WriteLine(1 + b + " " + coint_list_json[a + b]);            // char no }
                            }
                        }
                    }
                    counter_for_coin_list_arry++;
                }
            }        

            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 5000; b++)
                {             
                       if (coin_list_array[b, 1].Remove(0, 9) == search_ID_form_file[a])
                       {
                         Console.WriteLine("------------found-------------");                   
                         Console.WriteLine(coin_list_array[b, 0].Remove(0, 5));
                         suitable_ID_form_api[a] = coin_list_array[b, 0].Remove(0, 5);
                    }
                }
            }          
        }    
    }

    internal class Geko_API
    {
        public String address_for_coin_list;
        public String result_from_coinlist_Server;

        
        public async Task<string> LoaderAsync()
        {
           
            using (var httpClient = new HttpClient())
            result_from_coinlist_Server = await httpClient.GetStringAsync(address_for_coin_list);           
            return result_from_coinlist_Server;
        } 
    }

    
}