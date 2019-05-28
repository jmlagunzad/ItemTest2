using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ItemTest2
{

    //NewChange
    public partial class Form1 : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VQVN4SH;Initial Catalog=ItemTest;Integrated Security=True;Pooling=False");
        SqlDataAdapter adapt;
        SqlCommand comm;

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                comm = new SqlCommand("INSERT INTO Fruit (id, fname, price, qty) VALUES (@id, @fname, @price, @qty)",con);
                con.Open();
                comm.Parameters.AddWithValue("@id", Int32.Parse(txtId.Text));
                comm.Parameters.AddWithValue("@fname", txtName.Text);
                comm.Parameters.AddWithValue("@price", Double.Parse(txtPrice.Text));
                comm.Parameters.AddWithValue("@qty", Int32.Parse(txtQty.Text));
                comm.ExecuteNonQuery();
                con.Close();
                UpdateTable();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Problem Adding");
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                comm = new SqlCommand("UPDATE Fruit SET fname = @fname, price = @price, qty = @qty WHERE id = @id", con);
                con.Open();
                comm.Parameters.AddWithValue("@id", Int32.Parse(txtId.Text));
                comm.Parameters.AddWithValue("@fname", txtName.Text);
                comm.Parameters.AddWithValue("@price", Double.Parse(txtPrice.Text));
                comm.Parameters.AddWithValue("@qty", Int32.Parse(txtQty.Text));
                comm.ExecuteNonQuery();
                con.Close();
                UpdateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem Editing");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                comm = new SqlCommand("DELETE Fruit WHERE id = @id", con);
                con.Open();
                comm.Parameters.AddWithValue("@id", Int32.Parse(txtId.Text));
                comm.ExecuteNonQuery();
                con.Close();
                UpdateTable();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Problem Deleting");
            }
        }


        private void UpdateTable()
        {
            DisplayData();
            ClearFields();
        }

        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("SELECT * FROM Fruit", con);
            adapt.Fill(dt);
            tblFruit.DataSource = dt;
            con.Close();
        }
        //Clear Data  
        private void ClearFields()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtPrice.Text = "";
            txtQty.Text = "";
            
        }

    }
}
