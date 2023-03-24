namespace upSWOT.Models;

public class PageDto<T>
{
    public PageDto(PageInfoDto info, IEnumerable<T> results)
    {
        Info = info;
        Results = results;
    }

    public PageInfoDto Info { get; set; }
    public IEnumerable<T> Results { get; set; }
}