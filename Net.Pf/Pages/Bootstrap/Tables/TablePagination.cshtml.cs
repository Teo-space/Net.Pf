using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Net.Pf.Pages.Bootstrap.Tables
{
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


            PageSize = Math.Clamp(HttpContext?.Session?.GetInt32("PageSize") ?? 20, 20, 100);

            int max = (dataTable.Rows.Count / PageSize) * PageSize;

            PageNumber = Math.Clamp(HttpContext?.Session?.GetInt32("PageNumber") ?? 0, 0, max);
        }





        public void OnGet()
        {

        }

        //[BindProperty(SupportsGet = true)]

        public int PageSize = 20;
        /// <summary>
        /// Строго проверяй имя хендлера и переменных
        /// </summary>
        /// <param name="PageSize"></param>
        public void OnGetSetPageSize(int PageSize)
        {
            int i = this.PageNumber * this.PageSize;

            PageSize = Math.Clamp(PageSize, 20, 100);
            this.PageSize = PageSize;
            this.PageNumber = i / this.PageSize;

            HttpContext.Session.SetInt32("PageSize", PageSize);
        }


        public int PageNumber = 0;
        public void OnGetSetPageNumber(int PageNumber)
        {
            int max = (dataTable.Rows.Count / PageSize) * this.PageSize;
            PageNumber = Math.Clamp(PageNumber, 0, max);

            this.PageNumber = PageNumber;
            HttpContext.Session.SetInt32("PageNumber", PageNumber);
        }

        public void OnGetFirstPage()
        {
            this.PageNumber = 0;
            HttpContext.Session.SetInt32("PageNumber", PageNumber);
        }

        public void OnGetLastPage()
        {
            this.PageNumber = (dataTable.Rows.Count / PageSize) * PageSize;
            HttpContext.Session.SetInt32("PageNumber", PageNumber);
        }


        public void OnGetPreviousPage()
        {
            int max = (dataTable.Rows.Count / PageSize) * PageSize;
            this.PageNumber = Math.Clamp(this.PageNumber-1, 0, max);
            HttpContext.Session.SetInt32("PageNumber", PageNumber);
        }


        public void OnGetNextPage()
        {
            int max = (dataTable.Rows.Count / PageSize) * PageSize;
            this.PageNumber = Math.Clamp(this.PageNumber+1, 0, max);
            HttpContext.Session.SetInt32("PageNumber", PageNumber);
        }





    }


}


