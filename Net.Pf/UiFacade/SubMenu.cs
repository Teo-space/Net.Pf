using System.Collections.Concurrent;


namespace Net.Pf.UiFacade;


public record SubMenu(string Name = "SubMenu")
{
    public record Link(string Name, string Url, bool blank);

    public ConcurrentDictionary<string, Link> links2 { get; } = new();

    public SubMenu Addlink(string Name, string url, bool blank = false)
    {
        links2[Name] = new Link(Name, url, blank);

        return this;
    }
    public IEnumerable<Link> GetLinks() => links2.Values.OrderBy(x => x.Name);



    public bool Show { get; protected set; } = false;

    public SubMenu SetShow()
    {
        this.Show = true;
        return this;
    }

}
