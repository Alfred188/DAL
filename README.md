# DAL
### A lib to help in coding
this lib help to connect to sql server database in C#

# How to use

public static string ConnectionStr = "ConStr"; // Your connection string gose here

Select:

    DAL dal = new DAL();
    string spCom = string.Empty;
    DataTable dt = new DataTable();

    spCom = "SELECT  [ID],[Name] from [Table] where [ID]=@ID";
    dbm = new DatabaseModel();
    dbm.ParametrName = "@ID";
    dbm.Value = Id.Text;
    dbList.Add(dbm);

    int result = 0;
    dt = dal.selectTable(spCom, out result);
    if (result == (int)_EnumManager.DbState.success)
    {
        //dt is filled with data
    }

Insert, Update, Delete:

    string stb = string.Empty ;
    DAL dal = new DAL();
    string spCom = string.Empty;
    List<DatabaseModel> dbList = new List<DatabaseModel>();
    DatabaseModel dbm;
            
    spCom = "INSERT into [Table] ([Value1Col],[Value2Col],[Value3Col]) values" +
        " (@Value1,@Value2,@Value3)";
    dbm = new DatabaseModel();
    dbm.ParametrName = "@Value1";
    dbm.Value = "Var1";
    dbList.Add(dbm);
            
    dbm = new DatabaseModel();
    dbm.ParametrName = "@Value2";
    dbm.Value = "Var2";
    dbList.Add(dbm);
            
    dbm = new DatabaseModel();
    dbm.ParametrName = "@Value3";
    dbm.Value = "Var3";
    dbList.Add(dbm);
            

    int result = dal.runCommand(spCom, dbList);
    if (result == (int)_EnumManager.DbState.success)
    {
        //Success
    }
    else
    {
        //Failes
    }
