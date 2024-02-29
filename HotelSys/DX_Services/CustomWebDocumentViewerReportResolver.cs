using DataModels;
using DevExpress.DataAccess.ObjectBinding;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using HotelSys.DataSources;
using HotelSys.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Parameter2 = DevExpress.XtraReports.Parameters.Parameter;


namespace HotelSys
{
    //Ì” œ⁄Ï ﬁ»·  Õ„Ì· «”„ «· ﬁ—Ì—
    public class CustomWebDocumentViewerReportResolver : IWebDocumentViewerReportResolver
    {
        protected HotelAlkheerDB db { get; set; }
        public CustomWebDocumentViewerReportResolver(HotelAlkheerDB dbContext)
        {
            this.db = dbContext;
           // Items = InitializeList(DateTime.Now, DateTime.Now);
        }

        public XtraReport Resolve(string reportEntry)
        {
            //if (reportEntry.StartsWith("CusInHotelRPT"))
            //{
            //    XtraReport rep = CreateReport(reportEntry);
            //    rep.DataSource = CreateObjectDataSource(reportEntry);
            //    return rep;
            //}
            //else if (reportEntry.StartsWith("XtraReport1"))
            //{
            //    XtraReport rep = CreateReport(reportEntry);
            //    rep.DataSource = CreateObjectDataSource(reportEntry);
            //    return rep;
            //}
            //else if (reportEntry.StartsWith("_AccountBalaceRPT"))
            //{
            //    XtraReport rep = CreateReport(reportEntry);
            //    rep.DataSource = CreateObjectDataSource(reportEntry);
            //    return rep;

            //    //var parameter = reportEntry.Replace("_AccountBalaceRPT", "");
            //    //int value;
            //    //XtraReport rep = CreateReport(reportEntry);
            //    //if (int.TryParse(parameter, out value))
            //    //{


            //    //    rep.DataSource = /*datasource1(value);*/ CreateObjectDataSource(reportEntry);
            //    //}
            //    //return rep;
            //}
            //else if (reportEntry.StartsWith("ClientsBalanceRPT"))
            //{
            //    XtraReport rep = CreateReport(reportEntry);
            //    rep.DataSource = CreateObjectDataSource(reportEntry);
            //    return rep;
            //}
            //else 
            
            if (reportEntry.StartsWith("CB_RPT"))
            {
                var parameter = reportEntry.Replace("CB_RPT", "");
                int value;
                    XtraReport rep1 = CreateReport(reportEntry);
                if (int.TryParse(parameter, out value))
                {


                    rep1.DataSource = /*datasource1(value);*/ CreateObjectDataSource(reportEntry);
                }
                return rep1;
            }



            XtraReport rep = CreateReport(reportEntry);
            rep.DataSource = CreateObjectDataSource(reportEntry);
            return rep; ;
        }

       
        private object CreateObjectDataSource(string reportName,string? param=null)
        {
            if (reportName == "CusInHotelRPT")
            {
                //ObjectDataSource dataSource = new ObjectDataSource();
                //dataSource.Name = "EmployeeObjectDS";
                //dataSource.DataSource = typeof(EmployeeList);
                //dataSource.Constructor = ObjectConstructorInfo.Default;
                //dataSource.DataMember = "Items";
                //return dataSource;

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Name = "EmployeeObjectDS";
                //dataSource.DataSource = new mymodel(db);

                dataSource.DataSource = typeof(CusInHotelDS);

                List<Parameter> para = new List<Parameter>();

                //// Map data source parameter to report's parameter.
                //var parameter = new Parameter()
                //{
                //    Name = "noOfItems",
                //    Type = typeof(DevExpress.DataAccess.Expression),
                //    Value = new DevExpress.DataAccess.Expression("?parameterNoOfItems", typeof(int))
                //};



                var parameterFrom = new Parameter()
                {
                    Name = "FromDate",
                    Type = typeof(DevExpress.DataAccess.Expression),
                    Value = new DevExpress.DataAccess.Expression("?FromDate", typeof(DateTime))
                };
                var parameterTo = new Parameter()
                {
                    Name = "ToDate",
                    Type = typeof(DevExpress.DataAccess.Expression),
                    Value = new DevExpress.DataAccess.Expression("?ToDate", typeof(DateTime))
                };




                para.Add(parameterFrom);
                para.Add(parameterTo);

                dataSource.Constructor = new ObjectConstructorInfo(para);

                // dataSource.Constructor = ObjectConstructorInfo.Default;
                dataSource.DataMember = "Items";
                return dataSource;
            }

            else if (reportName == "CusInHotelRPT_now")
            {
                //ObjectDataSource dataSource = new ObjectDataSource();
                //dataSource.Name = "EmployeeObjectDS";
                //dataSource.DataSource = typeof(EmployeeList);
                //dataSource.Constructor = ObjectConstructorInfo.Default;
                //dataSource.DataMember = "Items";
                //return dataSource;

                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Name = "EmployeeObjectDS";
                //dataSource.DataSource = new mymodel(db);

                dataSource.DataSource = typeof(CusInHotelDS_now);

                //List<Parameter> para = new List<Parameter>();





                //var parameterFrom = new Parameter()
                //{
                //    Name = "FromDate",
                //    Type = typeof(DevExpress.DataAccess.Expression),
                //    Value = new DevExpress.DataAccess.Expression("?FromDate", typeof(DateTime))
                //};
                //var parameterTo = new Parameter()
                //{
                //    Name = "ToDate",
                //    Type = typeof(DevExpress.DataAccess.Expression),
                //    Value = new DevExpress.DataAccess.Expression("?ToDate", typeof(DateTime))
                //};




                //para.Add(parameterFrom);
                //para.Add(parameterTo);

                //dataSource.Constructor = new ObjectConstructorInfo(para);

                dataSource.Constructor = ObjectConstructorInfo.Default;
                dataSource.DataMember = "Items";
                return dataSource;
            }

            else if (reportName == "CB_RPT")
            {

                var objectDataSource = new ObjectDataSource();
                objectDataSource.Name = "Employees";
                objectDataSource.DataSource = typeof(EmployeeDataSource);
              
             
                var parameterNoOfItems = new Parameter()
                {
                    Name = "employeePosition",
                    Type = typeof(DevExpress.DataAccess.Expression),
                    Value = new DevExpress.DataAccess.Expression("?employeePosition", typeof(string))
                };


                //    objectDataSource.Parameters.Add(parameterNoOfItems);

                objectDataSource.Constructor = new ObjectConstructorInfo(parameterNoOfItems);

                objectDataSource.DataMember = "Items";


                // Create a report instance and add the parameter to the report's Parameters collection.
                //var report = new XtraReport1();
                //report.Parameters.Add(param);

                return objectDataSource;
            }


            else if (reportName.Contains("_AccountBalaceRPT"))
            {
                ObjectDataSource dataSource = new ObjectDataSource();

                var parameter = reportName.Replace("_AccountBalaceRPT", "");

                var strSplit= parameter.Split('&');
                int value = -1;

                DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1);
                DateTime end = DateTime.Now;

                bool isAll = true;

                int typeRPT = 1;


                if (strSplit.Length > 1)
                {
                   int.TryParse(strSplit[1], out value);

                     DateTime.TryParse( strSplit[2],out start);
                      DateTime.TryParse(strSplit[3],out end);
                    bool.TryParse(strSplit[4], out isAll);

                    int.TryParse(strSplit[5], out typeRPT);

                }

                

              

                dataSource.Name = "EmployeeObjectDS";
                

                dataSource.DataSource = new AccountBalanceDataSet(db, value, start, end, isAll, typeRPT);

                List<Parameter> para = new List<Parameter>();

                // Map data source parameter to report's parameter.
                //var parameter = new Parameter()
                //{
                //    Name = "noOfItems",
                //    Type = typeof(DevExpress.DataAccess.Expression),
                //    Value = new DevExpress.DataAccess.Expression("?parameterNoOfItems", typeof(int))
                //};



                var parameterFrom = new Parameter()
                {
                    Name = "FromDate",
                    Type = typeof(DevExpress.DataAccess.Expression),
                    Value = new DevExpress.DataAccess.Expression("?FromDate", typeof(DateTime))
                };

                var parameterTo = new Parameter()
                {
                    Name = "ToDate",
                    Type = typeof(DevExpress.DataAccess.Expression),
                    Value = new DevExpress.DataAccess.Expression("?ToDate", typeof(DateTime))
                };



                //var parameterAcc = new Parameter()
                //{
                //    Name = "account",
                //    Type = typeof(List<_AccountShortViewModel>),
                //    Value = new DevExpress.DataAccess.Expression("?account", typeof(List<_AccountShortViewModel>))
                //};


                para.Add(parameterFrom);
                para.Add(parameterTo);

                //  para.Add(parameterAcc);

                // dataSource.Constructor = new ObjectConstructorInfo(para);

                //  dataSource.Constructor = ObjectConstructorInfo.Default;
                dataSource.DataMember = "Items";



                return dataSource;
            }
          
            
            else if (reportName == "ClientsBalanceRPT")
            {


                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Name = "clientsBalanceDS";
                dataSource.DataSource = new clientsBalanceDS(db);

                dataSource.Constructor = ObjectConstructorInfo.Default;
                dataSource.DataMember = "Items";
                return dataSource;
            }
            //else if (reportName.StartsWith("XtraReport1"))
            //{



            //    //ObjectDataSource dataSource = new ObjectDataSource();
            //    //dataSource.Name = "EmployeeObjectDS";
            //    //dataSource.DataSource = new mymodel(db);


            //    // Map data source parameter to report's parameter.
            //    //var parameter = new Parameter()
            //    //{
            //    //    Name = "noOfItems",
            //    //    Type = typeof(DevExpress.DataAccess.Expression),
            //    //    Value = new DevExpress.DataAccess.Expression("?parameterNoOfItems", typeof(int))
            //    //};
            //    dataSource.Constructor = ObjectConstructorInfo.Default;
            //    dataSource.DataMember = "Items";
            //    return dataSource;
            //}

            else if (reportName.Contains("BillReceptionRPT"))
            {
                ObjectDataSource dataSource = new ObjectDataSource();

                var parameter = reportName.Replace("BillReceptionRPT", "");

                var strSplit = parameter.Split('&');
                long idReception = -1;

             

                if (strSplit.Length > 1)
                {
                    long.TryParse(strSplit[1], out idReception);

                  
                }





                dataSource.Name = "EmployeeObjectDS";


                dataSource.DataSource = new _billsReceptionDataSet(db, idReception);
              
                //  para.Add(parameterAcc);

                // dataSource.Constructor = new ObjectConstructorInfo(para);

                //  dataSource.Constructor = ObjectConstructorInfo.Default;
                dataSource.DataMember = "Items";



                return dataSource;
            }


            else
          if (reportName.EndsWith("7"))
            {
                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Name = "EmployeeObjectDS";
                dataSource.DataSource = typeof(EmployeeList);
                // Specify the parameter's default value.
                var parameter = new Parameter("noOfItems", typeof(int), 7);
                dataSource.Constructor = new ObjectConstructorInfo(parameter);
                dataSource.DataMember = "Items";
                return dataSource;
            }
            else
            if (reportName.EndsWith("Parameter"))
            {
                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Name = "EmployeeObjectDS";
                dataSource.DataSource = typeof(EmployeeList);
                // Map data source parameter to report's parameter.
                var parameter = new Parameter()
                {
                    Name = "noOfItems",
                    Type = typeof(DevExpress.DataAccess.Expression),
                    Value = new DevExpress.DataAccess.Expression("?parameterNoOfItems", typeof(int))
                };
                dataSource.Constructor = new ObjectConstructorInfo(parameter);
                dataSource.DataMember = "Items";
                return dataSource;
            }
            else
            {
                ObjectDataSource dataSource = new ObjectDataSource();
                dataSource.Name = "EmployeeObjectDS";
                dataSource.DataSource = typeof(EmployeeList);
                var parameterNoOfItems = new Parameter("noOfItems", typeof(int), 12);
                dataSource.Parameters.Add(parameterNoOfItems);
                dataSource.Constructor = ObjectConstructorInfo.Default;
                dataSource.DataMember = "GetData";
                return dataSource;
            }
        }

        private XtraReport CreateReport(string reportEntry)
        {

            
            if (reportEntry.Contains("Parameter"))
            {
                XtraReport report = new PredefinedReports.CusInHotelRPT();
                DevExpress.XtraReports.Parameters.Parameter param =
                    new DevExpress.XtraReports.Parameters.Parameter()
                    {
                        Name = "parameterNoOfItems",
                        Type = typeof(int),
                        Value = 10
                    };
                param.Description = "Number of Items";
                report.Parameters.Add(param);
                report.RequestParameters = false;
                return report;
            }
            else if (reportEntry.Contains("XtraReport1"))
            {
                XtraReport report = new PredefinedReports.XtraReport1();
                DevExpress.XtraReports.Parameters.Parameter param =
                    new DevExpress.XtraReports.Parameters.Parameter()
                    {
                        Name = "parameterNoOfItems",
                        Type = typeof(int),
                        Value = 10
                    };
                param.Description = "Number of Items";
                report.Parameters.Add(param);
                report.RequestParameters = false;
                return report;
            }
            else if (reportEntry=="CusInHotelRPT")
            {
                XtraReport report = new PredefinedReports.CusInHotelRPT();
                //DevExpress.XtraReports.Parameters.Parameter param =
                //    new DevExpress.XtraReports.Parameters.Parameter()
                //    {
                //        Name = "parameterNoOfItems",
                //        Type = typeof(int),
                //        Value = 10
                //    };
                //param.Description = "Number of Items";
                //report.Parameters.Add(param);

                DevExpress.XtraReports.Parameters.Parameter param2 =
                   new DevExpress.XtraReports.Parameters.Parameter()
                   {
                       Name = "FromDate",
                       Type = typeof(DateTime),
                       Value = DateTime.Now
                   };
                param2.Description = "„‰  «—ÌŒ";
                report.Parameters.Add(param2);


                DevExpress.XtraReports.Parameters.Parameter param3 =
                   new DevExpress.XtraReports.Parameters.Parameter()
                   {
                       Name = "ToDate",
                       Type = typeof(DateTime),
                       Value = DateTime.Now
                   };
                param3.Description = "«·Ï  «—ÌŒ";
                report.Parameters.Add(param3);


                report.RequestParameters = false;
                return report;
            }
            else if (reportEntry == "CusInHotelRPT_now")
            {
                XtraReport report = new PredefinedReports.CusInHotelRPT();
                //DevExpress.XtraReports.Parameters.Parameter param =
                //    new DevExpress.XtraReports.Parameters.Parameter()
                //    {
                //        Name = "parameterNoOfItems",
                //        Type = typeof(int),
                //        Value = 10
                //    };
                //param.Description = "Number of Items";
                //report.Parameters.Add(param);

                //DevExpress.XtraReports.Parameters.Parameter param2 =
                //   new DevExpress.XtraReports.Parameters.Parameter()
                //   {
                //       Name = "FromDate",
                //       Type = typeof(DateTime),
                //       Value = DateTime.Now
                //   };
                //param2.Description = "„‰  «—ÌŒ";
                //report.Parameters.Add(param2);


                //DevExpress.XtraReports.Parameters.Parameter param3 =
                //   new DevExpress.XtraReports.Parameters.Parameter()
                //   {
                //       Name = "ToDate",
                //       Type = typeof(DateTime),
                //       Value = DateTime.Now
                //   };
                //param3.Description = "«·Ï  «—ÌŒ";
                //report.Parameters.Add(param3);


                report.RequestParameters = false;
                return report;
            }


            


            else if (reportEntry.Contains("_AccountBalaceRPT"))
            {
                XtraReport report = new PredefinedReports._AccountBalaceRPT();
                DevExpress.XtraReports.Parameters.Parameter param =
                    new DevExpress.XtraReports.Parameters.Parameter()
                    {
                        Name = "DateFrom",
                        Type = typeof(DateTime),
                        Value = DateTime.Now
                    };
                param.Description = "„‰  «—ÌŒ";
                report.Parameters.Add(param);

                
                DevExpress.XtraReports.Parameters.Parameter param2 =
                    new DevExpress.XtraReports.Parameters.Parameter()
                    {
                        Name = "DateTo",
                        Type = typeof(DateTime),
                        Value = DateTime.Now
                    };
                param2.Description = "«·Ï  «—ÌŒ";
                report.Parameters.Add(param2);


                //DevExpress.XtraReports.Parameters.Parameter param3 =
                //    new DevExpress.XtraReports.Parameters.Parameter()
                //    {
                //        Name = "account",
                //        Type = typeof(List<_AccountShortViewModel>),
                //        Value =new _AccountShortViewModel { Id=-1,Name="Select Account"}
                //    };
                //param3.Description = "«·Õ”«»";
                //report.Parameters.Add(param3);


                report.RequestParameters = false;
                return report;
            }




            else if (reportEntry.Contains("CB_RPT"))
            {
                XtraReport report = new PredefinedReports._AccountBalaceRPT();


                var objectDataSource = new ObjectDataSource();
                objectDataSource.Name = "Employees";
                objectDataSource.DataSource = typeof(EmployeeDataSource);
              //  objectDataSource.DataMember = "Item";

              
                //  objectDataSource.Constructor = new ObjectConstructorInfo();
                //objectDataSource.Fill();

                // Create a report parameter.
                var param = new DevExpress.XtraReports.Parameters.Parameter();
                param.Name = "employeePosition";
                param.Description = "Employee position:";
                param.Type = typeof(string);

                // Create a DynamicListLookUpSettings instance and
                // set up its properties.
                //var lookupSettings = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
                //lookupSettings.DataSource = objectDataSource;
                //lookupSettings.ValueMember = "Name";
                //lookupSettings.DisplayMember = "Position";


                // Assign data storage settings to the parameter's ValueSourceSettings property.
               // param.ValueSourceSettings = lookupSettings;






                report.Parameters.Add(param);


                report.RequestParameters = false;
                return report;
            }




            else if (reportEntry.Contains("ClientsBalanceRPT"))
            {
                XtraReport report = new PredefinedReports.ClientsBalanceRPT();
               

                report.RequestParameters = false;
                return report;
            }

            else if (reportEntry.Contains("BillReceptionRPT"))
            {
                XtraReport report = new PredefinedReports.BillReceptionRPT();


                report.RequestParameters = false;
                return report;
            }

            else
                return new PredefinedReports.XtraReport1();
        }
    }

}
