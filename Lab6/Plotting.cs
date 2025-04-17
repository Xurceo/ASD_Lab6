using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace Lab6
{
    public static class Plotting
    {
        public static void Plot(long val1, long val2, long val3, string name)
        {
            // Ensure ExcelPackage is licensed
            ExcelPackage.License.SetNonCommercialPersonal("blablabla");

            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a worksheet
                var worksheet = package.Workbook.Worksheets.Add("Histogram");

                // Add data for the histogram
                worksheet.Cells[1, 1].Value = "Category";
                worksheet.Cells[1, 2].Value = "Value";
                worksheet.Cells[2, 1].Value = "Value 1";
                worksheet.Cells[2, 2].Value = val1;
                worksheet.Cells[3, 1].Value = "Value 2";
                worksheet.Cells[3, 2].Value = val2;
                worksheet.Cells[4, 1].Value = "Value 3";
                worksheet.Cells[4, 2].Value = val3;

                // Add a bar chart
                var chart = worksheet.Drawings.AddChart("Histogram", eChartType.ColumnClustered) as ExcelBarChart;
                chart.Title.Text = "Histogram";
                chart.SetPosition(6, 0, 0, 0);
                chart.SetSize(600, 400);
                chart.Series.Add(worksheet.Cells[2, 2, 4, 2], worksheet.Cells[2, 1, 4, 1]);
                chart.XAxis.Title.Text = "Categories";
                chart.YAxis.Title.Text = "Values";

                // Save the Excel file
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{name}.xlsx");
                package.SaveAs(new FileInfo(filePath));

                Console.WriteLine($"Histogram saved to {filePath}");
            }
        }
    }
}
