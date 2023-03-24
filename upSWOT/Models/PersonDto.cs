namespace upSWOT.Models;

public class PersonDto
{
    public PersonDto(string name, string status, string species, string type, string gender,
        PersonDtoOrigin origins)
    {
        Name = name;
        Status = status;
        Species = species;
        Type = type;
        Gender = gender;
        Origins = origins;
    }

    public string Name { get; set; }
    public string Status { get; set; }
    public string Species { get; set; }
    public string Type { get; set; }
    public string Gender { get; set; }
    public PersonDtoOrigin Origins { get; set; }

    public class PersonDtoOrigin
    {
        public PersonDtoOrigin(string name, string type, string gender)
        {
            Name = name;
            Type = type;
            Gender = gender;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
    }
}