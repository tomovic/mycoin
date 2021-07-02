using System;
using System.Threading.Tasks;
using System.Net.Http;
using Echovoice.JSON;
using System.IO;

namespace MyCoin2
{
    internal class ID_to_enquiry
    {
        public string[] id_search = new string[10];
        public string[,] search_return = new string[3,25];
        public string[,] search_form_file = new string[3, 25];
        public string[,] master_result = new string[3, 4];

        public string[] saving_string = new string[100];
        public int saving_counter= 0;


        public async void create_search()
        {
            Geko_API search_element = new Geko_API();         

            for (int counter_for_search = 0; counter_for_search <= search_return.GetUpperBound(0); counter_for_search++)
            {
                await Task.Delay(1000);
                search_element.address_for_coin_list = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=eur&ids=" + id_search[counter_for_search] + "&order=market_cap_descmarket_cap_desc%2C%20gecko_desc%2C%20gecko_asc%2C%20market_cap_asc%2C%20market_cap_desc%2C%20volume_asc%2C%20volume_desc%2C%20id_asc%2C%20id_desc&per_page=100&page=1&sparkline=true&price_change_percentage=1h";

                string result = await search_element.LoaderAsync();

                string[] coin_list_json = JSONDecoders.DecodeJsStringArray(result);

                for (int counter_for_retrun_elemente = 0; counter_for_retrun_elemente <= search_return.GetUpperBound(1); counter_for_retrun_elemente++)
                {
                    search_return[counter_for_search, counter_for_retrun_elemente] = coin_list_json[counter_for_retrun_elemente];
                }

                Console.WriteLine("--"+ counter_for_search.ToString() +"----"+ id_search[counter_for_search] + "--");
                Console.WriteLine(search_element.address_for_coin_list); 
                Console.WriteLine(result);
            }

            // search element form file
            Console.WriteLine("---form file--");
            Console.WriteLine(search_form_file[1, 1]);
            Console.WriteLine("---form file-ende-");

           
            for(int file_block = 0; file_block <= search_return.GetUpperBound(0); file_block++)
            {
                Console.WriteLine("----------------");
                Console.WriteLine("-x-> id: " + id_search[file_block]);
                saving_string[saving_counter] = "    {";
                saving_counter++;
                saving_string[saving_counter] = "      \u0022ID\u0022: \u0022" + id_search[file_block] + "\u0022,";
                saving_counter++;

                for (int coinline_form_web = 0; coinline_form_web <= search_return.GetUpperBound(1); coinline_form_web++)     
                {
                
                    for (int search_line_form_file = 0; search_line_form_file <= search_form_file.GetUpperBound(1); search_line_form_file++)
                    {
                        string[] content_for_savefile = search_return[file_block, coinline_form_web].Split(new Char[] { ':' });

                        if (content_for_savefile[0].Remove(content_for_savefile[0].Length - 1, 1) == search_form_file[file_block, search_line_form_file])
                        {
                            
                            Console.WriteLine("--x-->" + content_for_savefile[0] + " : " + content_for_savefile[1]);
                            saving_string[saving_counter] = "      \u0022" + content_for_savefile[0] + ": \u0022" + content_for_savefile[1] + "\u0022,";
                            saving_counter++;
                        }
                    }
                    
               
                }
                saving_string[saving_counter] = "    },";
                saving_counter++;
            }

            string fullPath = "output.json";
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine("{");
                    writer.WriteLine("  \u0022coins\u0022: [");
                    for (int counter_for_writefile = 0; counter_for_writefile < saving_counter; counter_for_writefile++)
                {
                    writer.WriteLine(saving_string[counter_for_writefile]);
                }
                writer.WriteLine("    ],");

                writer.WriteLine("}");

            }
            // Read a file  
            string readText = File.ReadAllText(fullPath);
            Console.WriteLine(readText);


        }

    }

    internal class Symbol_to_ID
    {
       
        public string[] search_ID_form_file = new string[10];
        public string[] suitable_ID_form_api = new string[10];        

        public async void create_search()
        {
            
            Geko_API coinlist = new Geko_API();
            coinlist.address_for_coin_list = "https://api.coingecko.com/api/v3/coins/list?include_platform=true";
            string result = await coinlist.LoaderAsync();

            
            string[] coin_list_json = JSONDecoders.DecodeJsStringArray(result);
            string coin_id;
            string[,] coin_list_array = new string[9000, 8];

        int counter_for_coin_list_arry = 0;

            for (int counter_for_coinlist = 0; counter_for_coinlist <= coin_list_json.GetUpperBound(0); counter_for_coinlist++)
            {

                if (coin_list_json[counter_for_coinlist][0].ToString() == "{")
                {

                    if (coin_list_json[counter_for_coinlist].Substring(coin_list_json[counter_for_coinlist].Length - 1) == "}")
                    {
                        coin_id = coin_list_json[counter_for_coinlist].Remove(0, 2);
                        coin_list_array[counter_for_coin_list_arry, 0] = coin_id;   // char 2 with }
                        Console.WriteLine(1 + " " + coin_id + " end");  // char with }
                    }
                    else
                    {
                        coin_id = coin_list_json[counter_for_coinlist].Remove(0, 2);
                        coin_id = coin_id.Substring(0, coin_id.Length - 1);
                        coin_list_array[counter_for_coin_list_arry, 0] = coin_id;   // char 2 no }
                        Console.WriteLine(1 + " " + coin_id);  // char with no  }

                        for (int counter_for_more_elemente = 1; counter_for_more_elemente <= coin_list_array.GetUpperBound(1); counter_for_more_elemente++)
                        {
                            if (coin_list_json[counter_for_coinlist + counter_for_more_elemente].Substring(coin_list_json[counter_for_coinlist + counter_for_more_elemente].Length - 1) == "}") // CHECK 2 char -> }
                            {
                                coin_list_array[counter_for_coin_list_arry, counter_for_more_elemente] = coin_list_json[counter_for_coinlist + counter_for_more_elemente];   // char 2 no }
                                Console.WriteLine(1 + counter_for_more_elemente + " " + coin_list_json[counter_for_coinlist + counter_for_more_elemente] + " end");  // char with }
                                break;
                            }
                            else
                            {
                                coin_list_array[counter_for_coin_list_arry, counter_for_more_elemente] = coin_list_json[counter_for_coinlist + counter_for_more_elemente];   // char 2 no }
                                Console.WriteLine(1 + counter_for_more_elemente + " " + coin_list_json[counter_for_coinlist + counter_for_more_elemente]);            // char no }
                            }
                        }
                    }
                    counter_for_coin_list_arry++;
                }
            }        

            for (int counter_for_search_ID = 0; counter_for_search_ID <= search_ID_form_file.GetUpperBound(0); counter_for_search_ID++)
            {
                for (int counter_for_complet_id_list = 0; counter_for_complet_id_list < coin_list_array.GetUpperBound(0)-4000; counter_for_complet_id_list++)
                {             
                       if (coin_list_array[counter_for_complet_id_list, 1].Remove(0, 9) == search_ID_form_file[counter_for_search_ID])
                       {
                         Console.WriteLine("------------found-------------");                   
                         Console.WriteLine(coin_list_array[counter_for_complet_id_list, 0].Remove(0, 5));
                         suitable_ID_form_api[counter_for_search_ID] = coin_list_array[counter_for_complet_id_list, 0].Remove(0, 5);
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