using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CYQ;
using CYQ.Data;
using CYQ.Data.Table;
namespace QuartzTest.DbHelp
{
    class DbHelper
    {        
       public void select(){
           String con = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.16)(PORT = 1521)))(CONNECT_DATA =(SID = hdwms)(SERVICE_NAME=hdWMS)));User ID=HD40;password=dgsjwms9076";
           MDataTable dt = null;
           using (MAction action = new MAction("MIS_WM_TALCNTC", con)) {
               dt=action.Select();         
           }
           Console.WriteLine("123");
           Console.WriteLine(dt.ToJson());

       }

       public void insert()
       {
           String con = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.1.31)(PORT = 1521)))(CONNECT_DATA =(SID = fluxtms1)(SERVICE_NAME=fluxtms)));User ID=TMS_ZC;password=TMS_ZC";
           MDataTable dt = null;
           using (MAction action = new MAction("TMP_CODE1", con))
           {
               action.BeginTransation();
               action.Set("FCODE1", 1);
               action.Set("FCODE2", 1);
               action.Set("FCODE3", 1);
               action.Insert(InsertOp.None);
               action.EndTransation();
           }
           Console.WriteLine("123");
       }



    }
}
