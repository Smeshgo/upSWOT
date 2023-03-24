namespace upSWOT.Models;

public class CharacterOrigin
{
    public string Name { get; set; }
    public string Url { get; set; }

    public CharacterOrigin(string name, string url)
    {
        Name = name;
        Url = url;
    }
}