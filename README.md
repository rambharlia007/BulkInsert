# SqlBulkInsert Extension
Inserts List of data into the database using sql bulk insert. Internally maps the List to datatable and column mappings

```    
	Install-Package Rmb.Sql.Extension.BulkInsert
```

## Code Example
Sample usage example

```C#
// Add Sql Column mapping using BulkInsertMapping Attribute
// To Exclude property from column mapping use [BulkInsertMapping(false)]
// If BulkInsertMapping attribute is not present, both source and destination column name will be equivalent to property name
// If Property name and table column name are different then use 
// [BulkInsertMapping("Source", "Destination", true)]

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

        [BulkInsertMapping("Name", "DestName", true)]
        public string Name { get; set; }
        public int IntCheck { get; set; }
        public long LongId { get; set; }
        [BulkInsertMapping("BoolCheck", "DestBoolCheck", true)]
        public bool BoolCheck { get; set; }
        public decimal DecimalCheck { get; set; }
        public float FloatCheck { get; set; }

        // Enum if not excluded from mappings, Bulk Insert will fail
        [BulkInsertMapping(false)]
        public TestEnum TestEnumCheck { get; set; }
    }


 using (var connection = new SqlConnection(connectionString))
 {
    connection.BulkInsert("DestinationTableName", ListData);
 }
 
 using (var connection = new SqlConnection(connectionString))
 {
   connection.BulkInsert("DestinationTableName", ListData, new BulkCopySettings 
   { 
     BatchSize = 10000, 
     BulkCopyTimeout = 300 
    });
 }

 using (var connection = new SqlConnection(connectionString))
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

```	

