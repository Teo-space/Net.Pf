using System.Collections.Concurrent;


namespace Net.Pf.UiFacade;



public record SideBar(string Name = "SideBar")
{
    ConcurrentDictionary<string, SubMenu> __SubMenus = new();

    public SubMenu AddSubmenu(string subMenuName)
    {
        var subMenu = new SubMenu(subMenuName);
        __SubMenus.TryAdd(subMenuName, subMenu);
        return subMenu;
    }

    public List<SubMenu> SubMenus() => __SubMenus.Values.ToList();


}

