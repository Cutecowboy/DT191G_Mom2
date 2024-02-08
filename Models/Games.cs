using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace mom2.Models;

public class Games
{

    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Author { get; set; }
    [Required]
    public int Year { get; set; }

    public List<Games> game = [];


    public void Setup()
    {
        // check if file exists

        if (File.Exists("wwwroot/games.json"))
        {
            // read the JSON file
            string jsonData = File.ReadAllText("wwwroot/games.json");
            // check that its not empty
            if (!string.IsNullOrEmpty(jsonData))
            {
                // store the json data in the game variable for later usage. Is not null so ignore null warning
                game = JsonConvert.DeserializeObject<List<Games>>(jsonData)!;
            }
        }
        else  // file is does not exist
        {
            // create an emtpy json file with no data.
            File.WriteAllText("wwwroot/games.json", "");
        }


    }
    public void Save()
    {
        // if no entries, reduce bugs by replacing empty array with empty string
        if (game.Count == 0)
        {
            // empty string instead of []
            File.WriteAllText("wwwroot/games.json", "");
        }
        else
        {
            // Serialize the entries
            string json = JsonConvert.SerializeObject(game);
            // write the json data to the json file
            File.WriteAllText("wwwroot/games.json", json);
        }

    }

    // attempt to remove games
    public void DeleteGame(int id)
    {

        // if the inputted id is >= 0 and the id <= game count 
        if (game.FindAll(g => g.Id == id).Count == 1)
        {
            int index = game.FindIndex(g => g.Id == id);
            // try to 
            try
            {
                // remove the game at said index
                game.RemoveAt(index);
                // save
                Save();


            }
            // catch exceptionerror in case something goes wrong
            catch (ArgumentException)
            {

            }

        }
    }



    /// <summary>
    /// Check gamelist id's and return an unique game id 
    /// </summary>
    /// <returns>returns an game id which is unique</returns>
    public int PostId()
    {
        // if there are not games 
        if (game.Count == 0)
        {
            // return Id 0
            return 0;
        }
        else
        {
            // return the max value of the Id + 1, this to ensure correct "database" Id value
            return game.Max(t => t.Id) + 1;
        }
    }
    public void AddGame(string name, string author, int year)
    {

        // append the new game to the game list
        game.Add(new Games { Id = PostId(), Name = name, Author = author, Year = year });
        // save the new game
        Save();
        // clear terminal


    }

}