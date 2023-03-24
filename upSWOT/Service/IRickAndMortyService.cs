using System.Net;
using upSWOT.Models;

namespace upSWOT.Service
{
    public interface IRickAndMortyService
    {
        Task<bool?> IsPresent(ResponseCheckPerson responseCheckPerson);
        Task<IEnumerable<PersonDto>?> GetHeroByName(string name);
    }
}
