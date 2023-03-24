namespace upSWOT.Models;

public class PageInfoDto
{
    public PageInfoDto(int count, int pages, string next, string prev)
    {
        Count = count;
        Pages = pages;
        Next = next;
        Prev = prev;
    }

    public int Count { get; set; }
    public int Pages { get; set; }
    public string Next { get; set; }
    public string Prev { get; set; }
}