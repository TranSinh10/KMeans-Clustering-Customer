using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CsvHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Do_an
{
    public partial class Form1 : Form
    {
        private List<Customer> customers;
        private List<List<Customer>> clusters;
        private Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "3";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (customers == null)
            {
                MessageBox.Show("Please load data first.");
                return;
            }
        }
        string FileName = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.Title = "Select a CSV File";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                textBox2.Text = FileName;
                // Read the contents of the selected CSV file into a DataTable.
                DataTable dataTable = new DataTable();
                using (StreamReader streamReader = new StreamReader(openFileDialog.FileName))
                {
                    string[] headers = streamReader.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }
                    while (!streamReader.EndOfStream)
                    {
                        string[] rows = streamReader.ReadLine().Split(',');
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dataRow[i] = rows[i];
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }

                // Bind the DataTable to the DataGridView to display the contents.
                dataGridView1.DataSource = dataTable;
            }
        }
        private List<Customer> LoadDataFromCsv(string filename)
        {
            List<Customer> customers = new List<Customer>();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    int id = int.Parse(fields[0]);
                    string name = fields[1];
                    double age = double.Parse(fields[2]);
                    double income = double.Parse(fields[3]);
                    Customer customer = new Customer(id, name, age, income);
                    customers.Add(customer);
                }
            }

            return customers;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}