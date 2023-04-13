using LinqToExcel;

namespace DSAExcel.ExcelReader
{
    internal static class ExcelReader
    {
        private const string path = @"C:\Users\AkSharma\Desktop\Contacts.xlsx";
        internal static List<Person> GetDataFromExcel()
        {
            using(ExcelQueryFactory connection = new ExcelQueryFactory(path))
            {
                List<Row> sheet = connection.Worksheet("Sheet1").ToList();
                List<Person> data = new List<Person>();
                foreach(Row row in sheet)
                {
                    Person newData = new Person();
                    newData.id = row["Id"].ToString().Trim();
                    newData.state = row["State"].ToString().Trim();
                    newData.contact = row["Contact"].ToString().Trim();
                    newData.age = row["Age"].ToString().Trim();
                    newData.city = row["City"].ToString().Trim();
                    newData.firstName = row["First Name"].ToString().Trim();
                    newData.lastName = row["Last Name"].ToString().Trim();
                    data.Add(newData);
                }
                return data;
            }
        }
    }
}
