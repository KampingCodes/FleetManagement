using FleetManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetManagement.View
{
    public partial class frmVehicle : Form
    {
        private DataTable dt = new DataTable();
        private Vehicle selectedVehicle;
        public frmVehicle()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vehicle.createNew();
            showVehicles();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView Grd = dataGridView1;
                DataTable Tbl = (DataTable)Grd.DataSource;
                DataRow SelRow = Tbl.Rows[e.RowIndex];
                Vehicle bok = new Vehicle(SelRow);

                txtVehicleID.Text = bok.VehicleID.ToString();
                txtVehicleModel.Text = bok.VehicleModel;
                txtRegistrationNum.Text = bok.RegistrationNum.ToString();
                txtModYear.Text = bok.ModYear.ToString();
                drpDriver.SelectedValue = bok.DriverID;

                selectedVehicle = bok;
            }
            catch (Exception ex) { }
        }

        private void showVehicles()
        {
            // DataTable aTable = DBEngin.GetTable("select * from Vehicle");
            DataTable aTable = Vehicle.VehiclesTableWithDriverName();
            dataGridView1.DataSource = aTable;
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void frmVehicle_Load(object sender, EventArgs e)
        {
            DataTable dt = DBEngine.GetTable("Select DriverID, CONCAT(firstName,' ' ,lastName) as DriverName from Driver");
            drpDriver.DataSource = dt;
            drpDriver.DisplayMember = "DriverName";
            drpDriver.ValueMember = "DriverID";
            showVehicles();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Vehicle.search("VehicleModel like '%" + txtSearch.Text.Trim() + "%'");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //txtVehicleID.Text, txtVehicleModel.Text, txtRegistrationNum.Text, txtModYear.Text, drpDriver.SelectedValue.ToString()
            selectedVehicle.VehicleModel = txtVehicleModel.Text;
            selectedVehicle.RegistrationNum = txtRegistrationNum.Text;
            selectedVehicle.ModYear = Int32.Parse(txtModYear.Text);
            selectedVehicle.DriverID = Int32.Parse(drpDriver.SelectedValue.ToString());

            selectedVehicle.save();
            showVehicles();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete vehicle number "
               + "(" + selectedVehicle.VehicleID + ")", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                selectedVehicle.delete();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            showVehicles();
        }

        private void txtVehicleID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to Exit? "
                , "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do nothing
            }
        }
    }
}
