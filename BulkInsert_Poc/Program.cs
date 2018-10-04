using BulkInsert;
using BulkInsert.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsert_Poc
{
    public enum TestEnum
    {
        Valid = 1
    }

    public class Test
    {
        [BulkInsertMapping(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int IntCheck { get; set; }
        public long LongId { get; set; }
        public bool BoolCheck { get; set; }
        public decimal DecimalCheck { get; set; }
        public float FloatCheck { get; set; }
        // Enum if not excluded from mappings, Bulk Insert will fail
        [BulkInsertMapping(false)]
        public TestEnum TestEnumCheck { get; set; }
    }

    public class Test2
    {
        [BulkInsertMapping(false)]
        public int Id { get; set; }

        [BulkInsertMapping("Name", "DestName")]
        public string Name { get; set; }
        public int IntCheck { get; set; }
        public long LongId { get; set; }
        [BulkInsertMapping("BoolCheck", "DestBoolCheck")]
        public bool BoolCheck { get; set; }
        public decimal DecimalCheck { get; set; }
        public float FloatCheck { get; set; }

        // Enum if not excluded from mappings, Bulk Insert will fail
        [BulkInsertMapping(false)]
        public TestEnum TestEnumCheck { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Example1();
            Example2();
            Example3();
            Console.ReadLine();
        }

        public static void Example1()
        {
            var tests = new List<Test>();
            for (int i = 0; i < 50000; i++)
            {
                tests.Add(new Test
                {
                    BoolCheck = true,
                    DecimalCheck = 55.77M,
                    FloatCheck = 77.77F,
                    IntCheck = 77777,
                    LongId = 7843758379,
                    Name = "Test",
                    TestEnumCheck = TestEnum.Valid
                });
            }
            using (var connection = new SqlConnection(@"Data Source=DESKTOP-B288BBB\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"))
            {
                connection.BulkInsert("TestTable", tests);
            }
        }

        public static void Example2()
        {
            var tests = new List<Test2>();
            for (int i = 0; i < 50000; i++)
            {
                tests.Add(new Test2
                {
                    BoolCheck = true,
                    DecimalCheck = 55.77M,
                    FloatCheck = 77.77F,
                    IntCheck = 77777,
                    LongId = 7843758379,
                    Name = "Test",
                    TestEnumCheck = TestEnum.Valid
                });
            }
            using (var connection = new SqlConnection(@"Data Source=DESKTOP-B288BBB\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"))
            {
                connection.BulkInsert("TestTable", tests, new BulkCopySettings { BatchSize = 10000, BulkCopyTimeout = 300 });
            }
        }

        public static void Example3()
        {
            var tests = new List<Test>();
            for (int i = 0; i < 50000; i++)
            {
                tests.Add(new Test
                {
                    BoolCheck = true,
                    DecimalCheck = 55.77M,
                    FloatCheck = 77.77F,
                    IntCheck = 77777,
                    LongId = 7843758379,
                    Name = "Test",
                    TestEnumCheck = TestEnum.Valid
                });
            }
            using (var connection = new SqlConnection(@"Data Source=DESKTOP-B288BBB\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"))
            {
                connection.BulkInsert("TestTable", tests, new BulkCopySettings
                {
                    BatchSize = 10000,
                    BulkCopyTimeout = 300,
                    copyOptions = SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction
                });
            }
        }
    }
}
