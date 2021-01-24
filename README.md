# SKDatabaseHandler
A simple, easy to use MS SQL database handler .NET standard class library to handle connecting to SQL databases and performing database operations.

- **One line database interaction**
- **Avoid having to duplicate code when interacting with your MS SQL database**
- **Automatically handles opening and closing database connections**
- **Ensures connection objects are disposed after use**

<br/>

## Dependencies ##
- System.Data.SqlClient

<br/>

## Example Usage ##

#### Testing your database connection ####
You can quickly confirm database connectivity by using the **TestConnection()** function and passing in the connection string name in either your web.config or app.config. 
```
DatabaseHandler.TestConnection("database_name");
```

<br/>

#### Retrieving data from the database ####
To get data from your database you can use the **ExecuteQuery()** function. This function returns a new instance of **DatabaseResponse** which contains the .NET DataTable object.
```
DatabaseHandler.ExecuteQuery("database_name", "SELECT * FROM tbl_Example");
```

<br/>

#### Performing an database operation ####
When wanting to perform an INSERT, UPDATE, DELETE or any other query that doesn't return a result you can use the **ExecuteOperation()** function. This function returns a new instance of **DatabaseResponse** however this time the dynamic ResponseObject property is a boolean to indicate SQL query success/failure.
```
DatabaseHandler.ExecuteQuery("database_name", "DELETE FROM tbl_Example WHERE ID = @ID", CommandType.Text, new List<SqlParameter>()
{
    new SqlParameter("@ID", ValueID)
});
```

<br/>

#### Getting a count/integer value from the database ####
When wanting to retreive an single integer value from your database you can use the **ExecuteScalar()** function. This function returns a new instance of **DatabaseResponse** however this time the dynamic ResponseObject property is a integer which is the value from the query if successful.
```
DatabaseHandler.ExecuteScalarQuery("database_name", "SELECT (COUNT ID) FROM tbl_Example WHERE FlagColumn = 0");
```

<br/>

## Checking for success ##
#### Testing connection ####
When testing a database connection, the **TestConnection** function will return a new instance of **DatabaseConnectionTest**. This class contains two properties:
```
// This boolean indicates if the connection was successful or not.
public bool Successful { get; set; }

// If the connection fails, the exception thrown will be stored in this property.
// If the connection is successful, this property will be null.
public Exception ErrorException { get; set; }
```

<br/>

#### Executing a query ####
When executing a query, the function will return a new instance of **DatabaseResponse**. This class contains two properties:
```
// This dynamic object is defined at runtime and varies depending on 
// the function (ExecuteQuery, ExecuteOperation, ExecuteScalarQuery)
public dynamic ResponseObject { get; set; }

// If the query fails, the exception thrown will be stored in this property.
// If the connection is successful, this property will be null.
public Exception ErrorException { get; set; }
```
To save you checking the ErrorException property each time, DatabaseResponse has a function called **HasErrorOccured()** which returns a boolean to indicate if the query was successful or not.

<br/>


