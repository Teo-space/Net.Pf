using System.Collections.Concurrent;
using System.Security.Policy;


namespace Net.Pf.UiFacade;


public class SideBar
{

    public static ConcurrentDictionary<string, SubMenu> SubMenus { get; private set; } = new();

    public static SubMenu AddSubmenu(string subMenuName)
    {

        var subMenu = new SubMenu(subMenuName);

        SubMenus.TryAdd(subMenuName, subMenu);

        return subMenu;

    }

}


public class SubMenu
{

    public string Name { get; private set; } = "SubMenu";



    public SubMenu(string name)

    {

        Name = name;

    }



    public ConcurrentDictionary<string, string> links { get; } = new();

    public SubMenu Addlink(string Name, string url)

    {

        links[Name] = url;

        return this;

    }



    //public IEnumerable<(string Name, string Url)> GetLinks() => links.Select(x => (Name: x.Key, Url: x.Value));

    public record Link(string Name, string Url);

    public IEnumerable<Link> GetLinks() => links.Select(x => new Link(x.Key, x.Value)).OrderBy(x => x.Name);











    public bool Show { get; protected set; } = false;

    public SubMenu SetShow()

    {

        this.Show = true;

        return this;

    }

}
