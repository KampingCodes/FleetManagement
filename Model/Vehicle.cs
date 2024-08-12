using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetManagement.Model
{
    internal class Vehicle
    {
        private DataRow _Row;
        public Vehicle(DataRow aRow)
        {
            int VehicleID = (int)aRow["VehicleID"];
            DataTable dt = DBEngine.GetTable("Select * From Vehicle where VehicleID=" + VehicleID.ToString());

            _Row = dt.Rows[0];
        }


        public static DataTable search(string filter)
        {
            DataTable Tbl = new DataTable();
            string SQL = "select * from Vehicle ";
            if (filter.Trim() != "") { SQL += "where " + filter.Trim(); }

            Tbl = DBEngine.GetTable(SQL);

            return Tbl;
        }
        public static DataTable VehiclesTable()
        {
            return DBEngine.GetTable("select * from Vehicle");
        }
        public static DataTable VehiclesTableWithDriverName()
        {
            return DBEngine.GetTable("select v.VehicleID,v.VehicleModel,v.RegistrationNum,v.ModYear,CONCAT(d.firstName,'  ' ,d.lastName) as DriverName From Vehicle v join Driver d On d.DriverID=v.DriverID order by v.VehicleID");

        }
        public static void createNew()
        {
            string SQL = "INSERT INTO Vehicle(VehicleModel, RegistrationNum, ModYear, DriverID) VALUES('New_Vehicle', 'Registration', 1111, 1)";
            DBEngine.Execute(SQL);
        }

        public void save()
        {

            string SQL = "UPDATE Vehicle SET VehicleModel='" + VehicleModel + "', RegistrationNum='" + RegistrationNum + "', ModYear=" + ModYear.ToString() + ", DriverID=" + DriverID.ToString() + " WHERE VehicleID=" + VehicleID.ToString();
            DBEngine.Execute(SQL);

        }

        public void delete()
        {

            string sqldel = "SELECT * FROM Assignment Where VehicleId=" + VehicleID.ToString();
            DataTable dt = DBEngine.GetTable(sqldel);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("You cannot delete this Vehicle!");
            }
            else
            {
                string SQL = "DELETE FROM Vehicle WHERE VehicleID=" + VehicleID.ToString();
                DBEngine.Execute(SQL);
            }


        }

        public int VehicleID => (int)_Row["VehicleID"];
        public string VehicleModel
        {
            get
            {
                return (string)_Row["VehicleModel"];
            }
            set
            {
                _Row["VehicleModel"] = value;
            }
        }
        public string RegistrationNum
        {
            get
            {
                return (string)_Row["RegistrationNum"];
            }
            set
            {
                _Row["RegistrationNum"] = value;
            }
        }
        public int ModYear
        {
            get
            {
                return (int)_Row["ModYear"];
            }
            set
            {
                _Row["ModYear"] = value;
            }
        }
        public int DriverID
        {
            get
            {
                return (int)_Row["DriverID"];
            }
            set
            {
                _Row["DriverID"] = value;
            }
        }
    }
}
