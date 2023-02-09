using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Pages.Bootstrap.Tables
{
    public class TableModel : PageModel
    {
        public DataTable dataTable = new();
        public void OnGet()
        {
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Value");


            for(int i = 0; i < 60; i++)
            {
                var row = dataTable.NewRow();

                foreach(DataColumn column in dataTable.Columns)
                {
                    row[column] = $"{i}";
                }
                dataTable.Rows.Add(row);
            }

        }
    }
}
