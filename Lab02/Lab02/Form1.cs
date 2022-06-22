using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02
{
    public partial class Form1 : Form
    {
        static int id;
        public Form1()
        {
            InitializeComponent();
        }

        public void loadFromDB()
        {
            dgvStudent.Rows.Clear();
            var context = new LabContext();
            List<Student> list = getListByFilter(cboMajor2.SelectedIndex);
            foreach (Student s in list)
            {
                
                if(s.Male == true)
                {
                    dgvStudent.Rows.Add(s.Id, s.Name, "Male", s.Dob, s.Major, s.Scholarship, s.Active);
                }
                else
                {
                    dgvStudent.Rows.Add(s.Id, s.Name, "Female", s.Dob, s.Major, s.Scholarship, s.Active);
                }
            }
            List<Major> majors = context.Majors.ToList();
            majors.Insert(0, new Major() { Title = "Select major" });
            cboMajor.DisplayMember = "Title";
            cboMajor.ValueMember = "Code";
            cboMajor.DataSource = majors;
            List<Major> majors2 = context.Majors.ToList();
            majors2.Insert(0, new Major() { Title = "all major" });
            cboMajor2.DisplayMember = "Title";
            cboMajor2.ValueMember = "Code";
            cboMajor2.DataSource = majors2;
        }

        public List<Student> getListByFilter(int Index)
        {
            var context = new LabContext();
            List<Student> list;
            if(Index == 0)
            {
                list = context.Students.ToList();
            }
            else
            {
                list = context.Students.Where(s => s.Major == Index).ToList();
            }
            return list;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            loadFromDB();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == string.Empty)
                {
                    MessageBox.Show("Not Empty");
                }
                else if(rdbMale.Checked == false && rdbFemale.Checked == false)
                {
                    MessageBox.Show("Not Empty");
                }
                else if(cboMajor.SelectedIndex == 0)
                {
                    MessageBox.Show("Not Empty");
                }
                else if(nudScholarship.Value < 0)
                {
                    MessageBox.Show("Must be 0 - 100");
                }
                else
                {
                    Student s = new Student();
                    s.Name = txtName.Text;
                    if (rdbMale.Checked)
                    {
                        s.Male = true;
                    }
                    if (rdbFemale.Checked)
                    {
                        s.Male = false;
                    }
                    s.Dob = dtpDoB.Value;
                    s.Major = int.Parse(cboMajor.SelectedValue.ToString());
                    s.Scholarship = Convert.ToInt32(nudScholarship.Value);
                    s.Active = cbActive.Checked;
                    var context = new LabContext();
                    context.Students.Add(s);
                    context.SaveChanges();
                    loadFromDB();
                }
                
            }
            catch (Exception)
            {

                MessageBox.Show("AddException");
            }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            id = int.Parse(dgvStudent.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtId.Text = dgvStudent.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvStudent.Rows[e.RowIndex].Cells[1].Value.ToString();
            if(Boolean.Parse(dgvStudent.Rows[e.RowIndex].Cells[2].Value.ToString()) == true)
            {
                rdbMale.Checked = true;
            }
            else
            {
                rdbFemale.Checked = true;
            }
            dtpDoB.Value = Convert.ToDateTime(dgvStudent.Rows[e.RowIndex].Cells[3].Value.ToString());
            cboMajor.SelectedIndex = int.Parse(dgvStudent.Rows[e.RowIndex].Cells[4].Value.ToString());
            nudScholarship.Value = int.Parse(dgvStudent.Rows[e.RowIndex].Cells[5].Value.ToString());
            if (Boolean.Parse(dgvStudent.Rows[e.RowIndex].Cells[6].Value.ToString()) == true)
            {
                cbActive.Checked = true;
            }
            else
            {
                cbActive.Checked = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Student s = new Student();
                var context = new LabContext();
                s = context.Students.Where(s => s.Id == id).FirstOrDefault();
                s.Name = txtName.Text;
                if (rdbMale.Checked)
                {
                    s.Male = true;
                }
                if (rdbFemale.Checked)
                {
                    s.Male = false;
                }
                s.Dob = dtpDoB.Value;
                s.Major = int.Parse(cboMajor.SelectedValue.ToString());
                s.Scholarship = Convert.ToInt32(nudScholarship.Value);
                s.Active = cbActive.Checked;
                context.SaveChanges();
                loadFromDB();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Student s = new Student();
            var context = new LabContext();
            s = context.Students.Where(s => s.Id == id).FirstOrDefault();
            context.Students.Remove(s);
            context.SaveChanges();
            loadFromDB();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            rdbMale.Checked = false;
            rdbFemale.Checked = false;
            dtpDoB.Value = DateTime.Now;
            cboMajor.SelectedIndex = 0;
            nudScholarship.Value = 0;
            cbActive.Checked = false;
        }

        private void cboMajor2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvStudent.Rows.Clear();
            var context = new LabContext();
            List<Student> list = getListByFilter(cboMajor2.SelectedIndex);
            foreach (Student s in list)
            {
                if (s.Male == true)
                {
                    dgvStudent.Rows.Add(s.Id, s.Name, "Male", s.Dob, s.Major, s.Scholarship, s.Active);
                }
                else
                {
                    dgvStudent.Rows.Add(s.Id, s.Name, "Female", s.Dob, s.Major, s.Scholarship, s.Active);
                }
            }
        }
    }
}
