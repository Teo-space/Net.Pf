using System.Collections.Concurrent;


namespace Net.Pf.UiFacade;


public record SubMenu(string Name = "SubMenu")
{
    ConcurrentDictionary<string, string> links { get; } = new();

    public SubMenu Addlink(string Name, string url)

    {

        links[Name] = url;

        return this;

    }



    public record Link(string Name, string Url);

    public IEnumerable<Link> GetLinks() => links.Select(x => new Link(x.Key, x.Value)).OrderBy(x => x.Name);




    public bool Show { get; protected set; } = false;

    public SubMenu SetShow()
    {

        this.Show = true;

        return this;

    }

}
