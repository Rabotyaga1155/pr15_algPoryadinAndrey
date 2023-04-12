using System.Linq;
using System.Windows.Forms;

namespace prakt15alg
{
    public partial class Form1 : Form
    {
        private List<Student> students = new List<Student>();
        public Form1()
        {
            InitializeComponent();
            fromfile();
        }

        private void fromfile()
        {
            if (File.Exists("student.txt"))
            {
                var lines = File.ReadAllLines("student.txt");
                foreach (var line in lines)
                {
                    var a = line.Split(' ');
                    if (a.Length == 4 && DateTime.TryParse(a[2], out DateTime dateOfBirth))
                    {
                        var student = new Student
                        {
                            FirstName = a[0],
                            LastName = a[1],
                            DateOfBirth = dateOfBirth,
                            PhoneNumber = a[3]
                        };
                        students.Add(student);
                    }
                }
                listBox1.DataSource = students;
            }
        }
        private void addfile()
        {
            var lines = students.Select(s => $"{s.FirstName} {s.LastName} {s.DateOfBirth.ToShortDateString()} {s.PhoneNumber}");
            File.WriteAllLines("student.txt", lines);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void addbutton_Click(object sender, EventArgs e)
        {
            var student = new Student
            {
                FirstName = textBox1.Text,
                LastName = textBox2.Text,
                DateOfBirth = dateTimePicker1.Value,
                PhoneNumber = textBox3.Text
            };

            students.Add(student);
            addfile();
            listBox1.DataSource = null;
            listBox1.DataSource = students;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void deletebutton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                students.Remove((Student)listBox1.SelectedItem);
                addfile();
                listBox1.DataSource = null;
                listBox1.DataSource = students;
            }
        }

        private void findbutton_Click(object sender, EventArgs e)
        {
            var crit = textBox4.Text.ToLower();

            if (!string.IsNullOrWhiteSpace(crit))
            {
                var poisk = students.Where(s => s.FirstName.ToLower().Contains(crit) ||
                                                        s.LastName.ToLower().Contains(crit) ||
                                                        s.PhoneNumber.ToLower().Contains(crit));

                listBox1.DataSource = null;
                listBox1.DataSource = poisk.ToList();
            }
        }

        private void sortnamebutton_Click(object sender, EventArgs e)
        {
            students.Sort((s1, s2) => s1.LastName.CompareTo(s2.LastName));
            addfile();
            listBox1.DataSource = null;
            listBox1.DataSource = students;
        }

        private void sortdatebutton_Click(object sender, EventArgs e)
        {
            students.Sort((s1, s2) => s1.DateOfBirth.CompareTo(s2.DateOfBirth));
            addfile();
            listBox1.DataSource = null;
            listBox1.DataSource = students;
        }

        private void sbrosbutton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Today;
            textBox3.Clear();
            listBox1.DataSource = null;
            listBox1.DataSource = students;
        }
    }
}