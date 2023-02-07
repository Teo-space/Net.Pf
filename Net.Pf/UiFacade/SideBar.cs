using System.Collections.Concurrent;


namespace Net.Pf.UiFacade;



public record SideBar(string Name = "SideBar")
{


    public IEnumerable<SubMenu> SubMenus
    {
        get => __SubMenus.OrderBy(x => x.Key).Select(x => x.Value).ToList();
    }
    ConcurrentDictionary<string, SubMenu> __SubMenus { get; set; } = new();


    public SubMenu AddSubmenu(string subMenuName)
    {

        var subMenu = new SubMenu(subMenuName);

        __SubMenus.TryAdd(subMenuName, subMenu);

        return subMenu;
    }

}
