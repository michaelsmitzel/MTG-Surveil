using System;
using System.Data;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace MTG_DatabaseHelper
{
    class Program
    {
        const string CONNECTION_STRING = "server=localhost;user=root;database=surveil;port=3306;password=Shibby24713!";
        const string JSON_FILES_LOCATION = "../../../../../JSON-Files";
        const string SET_FILE_NAME = "sets.json";
        static MySqlConnection connection = new MySqlConnection(CONNECTION_STRING);
        static MySqlCommand command = new MySqlCommand();
        static void Main(string[] args)
        {
            importSet();

        }

        static void importSet()
        {
            JObject jObject = JObject.Parse(File.ReadAllText(JSON_FILES_LOCATION + "/" + SET_FILE_NAME));
            JArray jArray = (JArray)jObject["data"];
            connection.Open();
            
            foreach (JObject o in jArray)
            {    
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Insert Sets";
                command.Connection = connection;
                command.Parameters.AddWithValue("p_id", o["id"]);
                command.Parameters.AddWithValue("p_code", o["code"]);
                command.Parameters.AddWithValue("p_mtgo_code", o["mtgo_code"]);
                command.Parameters.AddWithValue("p_arena_code", o["arena_code"]);
                command.Parameters.AddWithValue("p_tcgplayer_id", o["tcgplayer_id"]);
                command.Parameters.AddWithValue("p_name", o["name"]);
                command.Parameters.AddWithValue("p_uri", o["uri"]);
                command.Parameters.AddWithValue("p_scryfall_uri", o["scryfall_uri"]);
                command.Parameters.AddWithValue("p_search_uri", o["search_uri"]);
                command.Parameters.AddWithValue("p_released_at", o["released_at"]);
                command.Parameters.AddWithValue("p_set_type", o["set_type"]);
                command.Parameters.AddWithValue("p_card_count", o["card_count"]);
                command.Parameters.AddWithValue("p_digital", (bool) o["digital"]);
                command.Parameters.AddWithValue("p_nonfoil_only", (bool) o["nonfoil_only"]);
                command.Parameters.AddWithValue("p_foil_only", (bool)o["foil_only"]);
                command.Parameters.AddWithValue("p_icon_svg_uri", o["icon_svg_uri"]);
                command.Parameters.AddWithValue("p_block_code", o["block_code"]);
                command.Parameters.AddWithValue("p_block", o["block"]);
                command.Parameters.AddWithValue("p_parent_set_code", o["parent_set_code"]);
                command.Parameters.AddWithValue("p_printed_size", o["printed_size"]);

                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            connection.Close();
        }

    }
}
