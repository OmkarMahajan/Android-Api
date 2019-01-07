using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TradeRepo.Controllers
{
    [RoutePrefix("api/repo")]
    public class ReportController : ApiController
    {
        [HttpGet]
        public JArray ErrorReport()
        {
            Dictionary<dynamic, dynamic> ErrorList = new Dictionary<dynamic, dynamic>();

            ErrorList.Add("Error", "Error");

            string serializedJson = JsonConvert.SerializeObject(ErrorList);
            var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);

            JArray array = new JArray();
            array.Add(deserializedJson);
            return array;

        }
        //GET: api/Report
        [HttpGet]
        [Route("{user}/{pass}")]
        public JArray Response(string user, string pass)
        {
            string company = "";
            int flag = 0;
            if (user == "" && pass == "")
            {
                dynamic a = ErrorReport();
                return a;
            }
            string dbname = "Tradepro_test";
            string ConnectionString = "server=43.255.152.26" + ";database=" + dbname + "; User ID=Admin2" + "; Password=" + "Fgke14#9";
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    int k = 0;
                    connection.Open();
                    string str = "SELECT Username,Password,Type " + "from " + "LoginDB";
                    SqlCommand cmd = new SqlCommand(str, connection);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string name = (string)dr["Username"];
                        string value = (string)dr["Password"];
                        company = (string)dr["Type"];
                        if ((name == user) && (value == pass))
                        {
                            flag = 1;
                            break;
                        }
                    }

                    if (flag == 1)
                    {
                        Dictionary<dynamic, dynamic> Info = new Dictionary<dynamic, dynamic>();

                        Info.Add("DBname", company);

                        string serializedJson = JsonConvert.SerializeObject(Info);
                        var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);

                        JArray db = new JArray();
                        db.Add(deserializedJson);
                        connection.Close();
                        return db;

                    }
                    else
                    {
                        dynamic arr = ErrorReport();
                        connection.Close();
                        return arr;

                    }
                }
            }
            catch
            {
                dynamic arr = ErrorReport();
                return arr;
            }

        }
        [HttpGet]
        [Route("{dbname}/query/{type}")]
        public JArray Get(string dbname, int type)
        {
            if (dbname == "" && type.ToString() == "")
            {
                return ErrorReport();
            }
            string ConnectionString = "server=43.255.152.26" + ";database=" + dbname + "; User ID=Admin2" + "; Password=" + "Fgke14#9";
            JArray array = new JArray();

            switch (type)
            {
                case 0:
                    ArrayList result = new ArrayList();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch
                        {
                            return ErrorReport();
                        }

                        string str = "SELECT top 10 ProductName,PurchasePrice,SalePrice,Safty_Stock,Inventory,Tax1 " + "from " + "ProductDB";
                        SqlCommand cmd = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["ProductName"].ToString() == null)
                            {
                                break;

                            }
                            result.Add(dr["ProductName"].ToString());
                            result.Add(dr["PurchasePrice"].ToString());
                            result.Add(dr["SalePrice"].ToString());
                            result.Add(dr["Safty_Stock"].ToString());
                            result.Add(dr["Inventory"].ToString());
                            result.Add(dr["Tax1"].ToString());
                        }
                        Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();


                        for (int j = 0; j < result.Count; j++)
                        {
                            ApiList.Add("Product_Name", result[j]);

                            ApiList.Add("Purchase_Price", result[++j]);

                            ApiList.Add("Sales_Price", result[++j]);

                            ApiList.Add("Safety_Stock", result[++j]);

                            ApiList.Add("Available_Stock", result[++j]);

                            ApiList.Add("Tax_Name", result[++j]);

                            string serializedJson = JsonConvert.SerializeObject(ApiList);
                            var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                            array.Add(deserializedJson);
                            ApiList.Clear();
                        }

                    }
                    break;



                case 1:
                    ArrayList result1 = new ArrayList();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string str = "SELECT top 10 ProductName,QTY,Total1,Total " + "from " + "TXN";
                        SqlCommand cmd = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {

                            result1.Add(dr["ProductName"].ToString());
                            result1.Add(dr["QTY"].ToString());
                            result1.Add(dr["Total1"].ToString());
                            result1.Add(dr["Total"].ToString());

                        }
                        Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();

                        for (int j = 0; j < result1.Count; j++)
                        {
                            ApiList.Add("Product_Name", result1[j]);

                            ApiList.Add("Quantity", result1[++j]);

                            ApiList.Add("Purchase_Total", result1[++j]);

                            ApiList.Add("Sell_Total", result1[++j]);

                            string serializedJson = JsonConvert.SerializeObject(ApiList);
                            var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                            array.Add(deserializedJson);
                            ApiList.Clear();
                        }
                    }
                    break;

                case 2:
                    ArrayList result2 = new ArrayList();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch
                        {
                            return ErrorReport();
                        }



                        string str = "SELECT top 10 c.CustomerName,c.PhoneNo,c.Balance_Pay,cs.col2 " + "from " + "CustomerDB c, CustSupExtraInfo cs where c.CustomerName = cs.CustSupName";
                        SqlCommand cmd = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            //if (dr["CustomerName"].ToString() == null)
                            //{
                            //    break;
                            //}
                            result2.Add(dr["CustomerName"].ToString());
                            result2.Add(dr["PhoneNo"].ToString());
                            result2.Add(dr["Balance_Pay"].ToString());
                            result2.Add(dr["col2"].ToString());

                        }

                        Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();

                        for (int j = 0; j < result2.Count; j++)
                        {
                            ApiList.Add("Customer_Name", result2[j]);

                            ApiList.Add("Phone_Number", result2[++j]);

                            ApiList.Add("Balance_Payment", result2[++j]);

                            ApiList.Add("Credit_Limit", result2[++j]);

                            string serializedJson = JsonConvert.SerializeObject(ApiList);
                            var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                            array.Add(deserializedJson);
                            ApiList.Clear();
                        }
                    }
                    break;

                case 3:
                    ;
                    ArrayList result3 = new ArrayList();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch
                        {
                            return ErrorReport();
                        }



                        string str = "SELECT top 10 s.SupplierName,s.PhoneNo,s.Balance_Due,cs.col2 " + "from " + "SupplierDB s,CustSupExtraInfo cs where s.SupplierName=cs.CustSupName";
                        SqlCommand cmd = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["SupplierName"].ToString() == null)
                            {
                                break;
                            }
                            result3.Add(dr["SupplierName"].ToString());
                            result3.Add(dr["PhoneNo"].ToString());
                            result3.Add(dr["Balance_Due"].ToString());
                            result3.Add(dr["col2"].ToString());

                        }

                        Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();

                        for (int j = 0; j < result3.Count; j++)
                        {
                            ApiList.Add("Supplier_Name", result3[j]);

                            ApiList.Add("Phone_Number", result3[++j]);

                            ApiList.Add("Balance_Dues", result3[++j]);

                            ApiList.Add("Credit_Limit", result3[++j]);

                            string serializedJson = JsonConvert.SerializeObject(ApiList);
                            var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                            array.Add(deserializedJson);
                            ApiList.Clear();
                        }
                    }
                    break;
                case 4:
                    ArrayList result4 = new ArrayList();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch
                        {
                            return ErrorReport();
                        }



                        string str = "SELECT top 10 Date,BillNoAgainst,PayNo,NetPay,PayType " + "from " + "PaySummary";
                        SqlCommand cmd = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            result4.Add(dr["Date"].ToString());
                            result4.Add(dr["BillNoAgainst"].ToString());
                            result4.Add(dr["PayNo"].ToString());
                            result4.Add(dr["NetPay"].ToString());
                            result4.Add(dr["PayType"].ToString());

                        }

                        Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();

                        for (int j = 0; j < result4.Count; j++)
                        {
                            ApiList.Add("Date", result4[j]);

                            ApiList.Add("Report_Number", result4[++j]);

                            ApiList.Add("Payment_Number", result4[++j]);

                            ApiList.Add("Amount", result4[++j]);

                            ApiList.Add("Type", result4[++j]);

                            string serializedJson = JsonConvert.SerializeObject(ApiList);
                            var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                            array.Add(deserializedJson);
                            ApiList.Clear();
                        }
                    }
                    break;

                case 5:
                    ArrayList result5 = new ArrayList();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch
                        {
                            return ErrorReport();
                        }



                        string str = "SELECT top 10 rp.Incoming,rp.Outgoing,D.Amount " + "from " + "FinalrptDaily rp,Daily_expDB D";
                        SqlCommand cmd = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            result5.Add(dr["Incoming"].ToString());
                            result5.Add(dr["Outgoing"].ToString());
                            result5.Add(dr["Amount"].ToString());
                        }

                        Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();

                        for (int j = 0; j < result5.Count; j++)
                        {
                            ApiList.Add("Daily_Incoming_Payment", result5[j]);

                            ApiList.Add("Daily_Outgoing_Payment", result5[++j]);

                            ApiList.Add("Other_Expenses", result5[++j]);

                            string serializedJson = JsonConvert.SerializeObject(ApiList);
                            var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                            array.Add(deserializedJson);
                            ApiList.Clear();
                        }
                    }

                    break;


                default:
                    array = ErrorReport();
                    break;


            }
            return array;




        }
        [HttpGet]
        [Route("{dbname}/query/{type1}/{type2}/{filtname}/{value}")]
        public JArray Getfilter(string dbname, int type1, int type2, string filtname, string value)
        {
            string str = "";
            string ConnectionString = "server=43.255.152.26" + ";database=" + dbname + "; User ID=Admin2" + "; Password=" + "Fgke14#9";
            JArray array = new JArray();

            switch (type2)
            {
                case 0:
                    ArrayList filt = new ArrayList();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch
                        {
                            return ErrorReport();
                        }

                        if (filtname == "Product_Name")
                        {
                            if (type1 == 0)
                            {
                                str = "SELECT ProductName,PurchasePrice,SalePrice,Safty_Stock,Inventory,Tax1 " + "from " + "ProductDB where ProductName like " + "'%" + value + "%'";
                                //  "where ProductName="+value;
                            }
                            else
                            {
                                str = "SELECT ProductName,QTY,Total1,Total " + "from " + "TXN where ProductName like " + "'%" + value + "%'";
                            }

                        }

                        if (filtname == "Product_Code")
                        {
                            if (type1 == 0)
                            {
                                str = "SELECT ProductName,PurchasePrice,SalePrice,Safty_Stock,Inventory,Tax1 " + "from " + "ProductDB where ProductCode like" + "'%" + value + "%'";
                            }
                            else
                            {
                                str = "SELECT p.ProductName,p.QTY,p.Total1,p.Total " + "from " + "TXN p,ProductDB pr where p.ProductCode like " + "'%" + value + "%'" + " and pr.ProductName = p.ProductName";
                            }
                        }

                        if (str == "")
                        {
                            return ErrorReport();
                        }

                        SqlCommand cmd = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (type1 == 0)
                        {
                            while (dr.Read())
                            {
                                if (dr["ProductName"].ToString() == null)
                                {
                                    break;

                                }
                                filt.Add(dr["ProductName"].ToString());
                                filt.Add(dr["PurchasePrice"].ToString());
                                filt.Add(dr["SalePrice"].ToString());
                                filt.Add(dr["Safty_Stock"].ToString());
                                filt.Add(dr["Inventory"].ToString());
                                filt.Add(dr["Tax1"].ToString());
                            }
                            Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();


                            for (int j = 0; j < filt.Count; j++)
                            {
                                ApiList.Add("Product_Name", filt[j]);

                                ApiList.Add("Purchase_Price", filt[++j]);

                                ApiList.Add("Sales_Price", filt[++j]);

                                ApiList.Add("Safety_Stock", filt[++j]);

                                ApiList.Add("Available_Stock", filt[++j]);

                                ApiList.Add("Tax_Name", filt[++j]);

                                string serializedJson = JsonConvert.SerializeObject(ApiList);
                                var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                                array.Add(deserializedJson);
                                ApiList.Clear();


                            }


                        }
                        if(type1==1)
                        {
                            while (dr.Read())
                            {

                                filt.Add(dr["ProductName"].ToString());
                                filt.Add(dr["QTY"].ToString());
                                filt.Add(dr["Total1"].ToString());
                                filt.Add(dr["Total"].ToString());

                            }
                            Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();

                            for (int j = 0; j < filt.Count; j++)
                            {
                                ApiList.Add("Product_Name", filt[j]);

                                ApiList.Add("Quantity", filt[++j]);

                                ApiList.Add("Purchase_Total", filt[++j]);

                                ApiList.Add("Sell_Total", filt[++j]);

                                string serializedJson = JsonConvert.SerializeObject(ApiList);
                                var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                                array.Add(deserializedJson);
                                ApiList.Clear();

                            }
                        }
                    }
                    if(type1==1)
                    {

                    }
                    break;

                case 1:
                    ArrayList filt1 = new ArrayList();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch
                        {
                            return ErrorReport();
                        }

                        if (filtname == "Customer_Name")
                        {
                           str = "SELECT c.CustomerName,c.PhoneNo,c.Balance_Pay,cs.col2 " + "from " + "CustomerDB c,CustSupExtraInfo cs where c.CustomerName like " + "'%" + value + "%'"+ "and c.CustomerName = cs.CustSupName";
                            //  "where ProductName="+value;

                        }
                        if(str=="")
                        {
                            return ErrorReport();
                        }
                    

                        SqlCommand cmd = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            if (dr["CustomerName"].ToString() == null)
                            {
                                break;

                            }
                            filt1.Add(dr["CustomerName"].ToString());
                            filt1.Add(dr["PhoneNo"].ToString());
                            filt1.Add(dr["Balance_Pay"].ToString());
                            filt1.Add(dr["col2"].ToString());
                        }
                        Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();


                        for (int j = 0; j < filt1.Count; j++)
                        {
                            ApiList.Add("Customer_Name", filt1[j]);

                            ApiList.Add("Phone_Number", filt1[++j]);

                            ApiList.Add("Balance_Payment", filt1[++j]);

                            ApiList.Add("Credit_Limit", filt1[++j]);

                            string serializedJson = JsonConvert.SerializeObject(ApiList);
                            var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                            array.Add(deserializedJson);
                            ApiList.Clear();


                        }


                    }
                    break;

                case 2:
                    
                    ArrayList filt2 = new ArrayList();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch
                        {
                            return ErrorReport();
                        }

                        if (filtname == "Supplier_Name")
                        {
                            str = "SELECT s.SupplierName,s.PhoneNo,s.Balance_Due,cs.col2 " + "from " + "SupplierDB s,CustSupExtraInfo cs where s.SupplierName like " + "'%" + value + "%'" + " and s.SupplierName=cs.CustSupName";
                          
                        }
                        else
                        {
                            return ErrorReport();
                        }

                        SqlCommand cmd = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd.ExecuteReader();
    
                        while (dr.Read())
                        {
                            if (dr["SupplierName"].ToString() == null)
                            {
                                break;
                            }
                            filt2.Add(dr["SupplierName"].ToString());
                            filt2.Add(dr["PhoneNo"].ToString());
                            filt2.Add(dr["Balance_Due"].ToString());
                            filt2.Add(dr["col2"].ToString());

                        }
                        Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();


                        for (int j = 0; j < filt2.Count; j++)
                        {
                            ApiList.Add("Supplier_Name", filt2[j]);

                            ApiList.Add("Phone_Number", filt2[++j]);

                            ApiList.Add("Balance_Dues", filt2[++j]);

                            ApiList.Add("Credit_Limit", filt2[++j]);

                            string serializedJson = JsonConvert.SerializeObject(ApiList);
                            var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                            array.Add(deserializedJson);
                            ApiList.Clear();


                        }


                    }
                    break;

                case 3:
                    ArrayList filt3 = new ArrayList();
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        try
                        {
                            connection.Open();
                        }
                        catch
                        {
                            return ErrorReport();
                        }

                        //IN("+"'"+filtname+"',"+"'"+value+"')"
                        str = "SELECT top 10 Date,BillNoAgainst,PayNo,NetPay,PayType " + "from " + "PaySummary where Date BETWEEN '"+filtname+"'"+" and '"+ value +"'";
                            
                        SqlCommand cmd = new SqlCommand(str, connection);
                        SqlDataReader dr = cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            filt3.Add(dr["Date"].ToString());
                            filt3.Add(dr["BillNoAgainst"].ToString());
                            filt3.Add(dr["PayNo"].ToString());
                            filt3.Add(dr["NetPay"].ToString());
                            filt3.Add(dr["PayType"].ToString());

                        }
                        Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();


                        for (int j = 0; j < filt3.Count; j++)
                        {
                            ApiList.Add("Date", filt3[j]);

                            ApiList.Add("Report_Number", filt3[++j]);

                            ApiList.Add("Payment_Number", filt3[++j]);

                            ApiList.Add("Amount", filt3[++j]);

                            ApiList.Add("Type", filt3[++j]);

                            string serializedJson = JsonConvert.SerializeObject(ApiList);
                            var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                            array.Add(deserializedJson);
                            ApiList.Clear();


                        }


                    }
                    break;



                default:
                    return ErrorReport();
                   

            }
            return array;
        }

        [HttpGet]
        [Route("{dbname}/query/{value1}/{value2}")]
        public JArray Getfilter(string dbname, string value1, string value2)
        {
            string ConnectionString = "server=43.255.152.26" + ";database=" + dbname + "; User ID=Admin2" + "; Password=" + "Fgke14#9";
            JArray array = new JArray();
            ArrayList filt = new ArrayList();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch
                {
                    return ErrorReport();
                }


                String str = "SELECT ProductName,PurchasePrice,SalePrice,Safty_Stock,Inventory,Tax1 " + "from " + "ProductDB where ProductName like " + "'%" + value1 + "%'" + " or ProductCode like "+ "'%" + value2 + "%'" ;
                                                                                                                   


                SqlCommand cmd = new SqlCommand(str, connection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    if (dr["ProductName"].ToString() == null)
                    {
                        break;

                    }
                    filt.Add(dr["ProductName"].ToString());
                    filt.Add(dr["PurchasePrice"].ToString());
                    filt.Add(dr["SalePrice"].ToString());
                    filt.Add(dr["Safty_Stock"].ToString());
                    filt.Add(dr["Inventory"].ToString());
                    filt.Add(dr["Tax1"].ToString());
                }
                Dictionary<dynamic, dynamic> ApiList = new Dictionary<dynamic, dynamic>();


                for (int j = 0; j < filt.Count; j++)
                {
                    ApiList.Add("Product_Name", filt[j]);

                    ApiList.Add("Purchase_Price", filt[++j]);

                    ApiList.Add("Sales_Price", filt[++j]);

                    ApiList.Add("Safety_Stock", filt[++j]);

                    ApiList.Add("Available_Stock", filt[++j]);

                    ApiList.Add("Tax_Name", filt[++j]);

                    string serializedJson = JsonConvert.SerializeObject(ApiList);
                    var deserializedJson = (JObject)JsonConvert.DeserializeObject<dynamic>(serializedJson);


                    array.Add(deserializedJson);
                    ApiList.Clear();


                }


            }

            return array;
        }
    }
}






