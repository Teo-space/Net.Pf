using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Net.Pf.Pages.Bootstrap.Tables;

public class TablePaginationModel : PageModel
{
    public DataTable dataTable = new();

    public TablePaginationModel()
    {
        dataTable.Columns.Add("Name");

        dataTable.Columns.Add("Value0");
        dataTable.Columns.Add("Value1");
        dataTable.Columns.Add("Value2");
        dataTable.Columns.Add("Value3");
        dataTable.Columns.Add("Value4");
        dataTable.Columns.Add("Value5");
        dataTable.Columns.Add("Value6");
        dataTable.Columns.Add("Value7");
        dataTable.Columns.Add("Value8");
        dataTable.Columns.Add("Value9");
        dataTable.Columns.Add("Value10");

        dataTable.Columns.Add("Value11");
        dataTable.Columns.Add("Value12");
        dataTable.Columns.Add("Value13");
        dataTable.Columns.Add("Value14");
        dataTable.Columns.Add("Value15");
        dataTable.Columns.Add("Value16");
        dataTable.Columns.Add("Value17");
        dataTable.Columns.Add("Value18");
        dataTable.Columns.Add("Value19");
        dataTable.Columns.Add("Value20");

        int v = 0;
        for (int i = 0; i < 495; i++)
        {
            var row = dataTable.NewRow();

            foreach (DataColumn column in dataTable.Columns)
            {
                row[column] = $"{v++}";
            }
            dataTable.Rows.Add(row);
        }
    }

    public int PageSize = 20;
    public int PageNumber = 0;
    public List<int> PageNumbers = new List<int>();

    public void OnGet()
    {

    }

    public void OnGetOpenPageNumber(int PageNumber, int PageSize = 20)
    {
        this.PageSize = PageSize;

        int max = dataTable.Rows.Count / this.PageSize;
        PageNumber = Math.Clamp(PageNumber, 0, max);

        this.PageNumber = PageNumber;
    }


}


