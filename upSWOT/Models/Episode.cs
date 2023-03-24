namespace upSWOT.Models;

public class Episode
{
    public Episode(int id, string name, string airDate, string episodeCode, IEnumerable<string> characters,
        string url, DateTime created)
    {
        Id = id;
        Name = name;
        AirDate = airDate;
        EpisodeCode = episodeCode;
        Characters = characters;
        Url = url;
        Created = created;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string AirDate { get; set; }
    public string EpisodeCode { get; set; }
    public IEnumerable<string> Characters { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }
}