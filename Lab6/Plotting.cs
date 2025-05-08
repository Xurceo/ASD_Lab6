using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace Lab6
{
    public static class Plotting
    {
        public static void Plot(List<long> values, string name)
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

                int i = 0;

                foreach (var value in values)
                {
                    worksheet.Cells[i + 2, 1].Value = $"Value {i + 1}";
                    worksheet.Cells[i + 2, 2].Value = value;
                    i++;
                }

                // Add a bar chart
                var chart = worksheet.Drawings.AddChart("Histogram", eChartType.Line) as ExcelLineChart;
                chart.Title.Text = "Histogram";
                chart.SetPosition(20, 0, 0, 0);
                chart.SetSize(600, 400);
                chart.Series.Add(worksheet.Cells[2, 2, i + 1, 2], worksheet.Cells[2, 1, i + 1, 1]);
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
