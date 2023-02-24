namespace Tools.Helpers
{

    public static class PaginationHelper
    {
        public static List<int> PageNumbers(int Page, int Max)
        {
            var PageNumbers = new List<int>();

            PageNumbers.Add(0);
            PageNumbers.Add(1);

            PageNumbers.Add(Max - 1);
            PageNumbers.Add(Max);

            PageNumbers.Add(Page - 2);
            PageNumbers.Add(Page - 1);

            PageNumbers.Add(Page);

            PageNumbers.Add(Page + 1);
            PageNumbers.Add(Page + 2);


            for (int i = 10; i < Max; i *= 10)
            {
                PageNumbers.Add(Page / i * i);
                PageNumbers.Add(Page / i * i + i);
            }

            PageNumbers = PageNumbers
                .Where(x => x > 0 && x < Max)
                .Distinct()
                .ToList();
            PageNumbers.Sort();

            return PageNumbers;
        }
    }


}

