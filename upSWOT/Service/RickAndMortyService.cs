using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using upSWOT.Models;
using static upSWOT.Models.PersonDto;

namespace upSWOT.Service
{
    public class RickAndMortyService: BaseService, IRickAndMortyService
    {
        private readonly IMemoryCache _cache;

        public RickAndMortyService(IMemoryCache memoryCache, string baseAddress = "https://rickandmortyapi.com/") : base(baseAddress)
        {
            _cache = memoryCache;
        }

        public async Task<bool?> IsPresent(ResponseCheckPerson responseCheckPerson)
        {

            if (string.IsNullOrWhiteSpace(responseCheckPerson.EpisodeName))
            {
                throw new ArgumentNullException(nameof(responseCheckPerson.EpisodeName), "Episode name cannot be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(responseCheckPerson.PersonName))
            {
                throw new ArgumentNullException(nameof(responseCheckPerson.PersonName), "Hero name cannot be null or empty.");
            }

            var cacheKey = $"IsPresent_{responseCheckPerson.EpisodeName}_{responseCheckPerson.PersonName}";

            if (_cache.TryGetValue(cacheKey, out bool cacheResult))
            {
                return cacheResult;
            }
            var characters = await _cache.GetOrCreateAsync("characters", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await GetPages<Character>("api/character/");
            });

            var episodes = await _cache.GetOrCreateAsync("episodes", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                return await GetPages<Episode>("api/episode");
            });

            var character = characters!.FirstOrDefault(c => c.Name == responseCheckPerson.PersonName);

            var episode = episodes!.FirstOrDefault(e => string.Equals(e.Name, responseCheckPerson.EpisodeName, StringComparison.CurrentCultureIgnoreCase));
            if (episode == null || character == null)
            {
                return null;
            }
            var result = episode.Characters.FirstOrDefault(ec => ec == $"https://rickandmortyapi.com/api/character/{character.Id}") != null;
            _cache.Set(cacheKey, result);
            return result;
        }

        public async Task<IEnumerable<PersonDto>?> GetHeroByName(string name)
        {
            var cacheKey = $"getHeroByName{name}";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<PersonDto>? cacheResult))
            {
                return cacheResult;
            }
            var characters = await GetPages<Character>($"https://rickandmortyapi.com/api/character/?name={name}");
            if (characters == null)
            {
                return null;
            }
            var result = new List<PersonDto>();
            

            
            foreach (var character in characters)
            {
                var location = new LocationResponse();
                if (!string.IsNullOrWhiteSpace(character.Origin.Url))
                {
                    location = await Get<LocationResponse>(character.Origin.Url);
                }
                
                result.Add(new PersonDto(
                        character.Name,
                        character.Status,
                        character.Species,
                        character.Type,
                        character.Gender,
                        new PersonDtoOrigin(location.Name,location.Type,location.Dimension)
                    )
                );
               
            }
            _cache.Set(cacheKey, result);

            return result;
        }
    }   
}
