using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace StudentMgtSystemWebForm
{
    public partial class Welcome : Form
    {
        string filename;

        public Welcome()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult link1 = MessageBox.Show("Do you really want to exit from the application?", "", MessageBoxButtons.YesNo);
            if (link1 == DialogResult.Yes)
            {
                this.Close();
                Form1 frmObj = new Form1();
                frmObj.Close();
            }

        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            Form1 formObj = new Form1();
            formObj.Close();
            MessageBox.Show("You are successfully logged in. Welcome to Transmax application.", "Welcome to Transmax!", MessageBoxButtons.OK);
            listBox1.Hide();
            listBox2.Hide();
            label3.Hide();
            label4.Hide();
            label5.Hide();
            button1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Checking whether the file is Empty
            if (new FileInfo(filename).Length != 0)
            {
                listBox1.Show();
                listBox2.Show();
                label4.Show();
                label5.Show();
                label4.Text = "Input data:";
                label5.Text = "Sorted Data:";

                // Read the input data from a file as one string.
                //string strdata = System.IO.File.ReadAllText(@"H:\StudentMgtSystem\StudentDataInput.txt");
                string strdata = System.IO.File.ReadAllText(filename);

                // Getting each line as a rowdata to finally spilt them into columns
                string[] rowdata = strdata.Split("\r\n\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


                //Delit Characters can be changed as per the input requirement
                //char[] delimiterChars = { ',', '.', ':', '\t' };
                char[] delimiterChars = { ',', '\t' };

                // Saving each row as a student record by grepping the student data like FirstName, LastName and Marks.
                // Scope is there to add more fields to the class.
                List<Student> studentlist = new List<Student>();

                // Printing the input data to the console:
                //System.Console.WriteLine("***************************** INPUT DATA ******************************");
                foreach (string studentrecord in rowdata)
                {
                    //System.Console.WriteLine(studentrecord);
                    listBox1.Items.Add(studentrecord);

                    //Spilts each rowdata as per the delimiter provided and saves them to the student object
                    Student studentobject = new Student();
                    string[] splitdata = studentrecord.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                    //Checking whether record is in the expected format or not 
                    if (splitdata.Length != 3)
                    {
                        MessageBox.Show("File data is not as per the standard [LastName,Firstname,Marks]. Please check and re-execute.", "File data format error!", MessageBoxButtons.OK);
                        listBox1.Items.Clear();
                        listBox1.Hide();
                        listBox2.Hide();
                        label3.Hide();
                        label4.Hide();
                        label5.Hide();
                        button1.Hide();
                        break;
                    }

                    studentobject.FirstName = splitdata[0].Replace(" ", string.Empty);
                    studentobject.LastName = splitdata[1].Replace(" ", string.Empty);
                    studentobject.Marks = int.Parse(splitdata[2].Replace(" ", string.Empty));
                    studentlist.Add(studentobject);
                }
                //System.Console.WriteLine("***********************************************************************\n\n");


                //The sorted data will be copied to the new var list 
                var sortedlist = studentlist.OrderByDescending(s => s.Marks).ThenBy(s1 => s1.FirstName);

                //Writing the sported output data to both console and output file.
                //System.Console.WriteLine("**************************** SORTED DATA ******************************");

                // Finally writing the sorted data to the output file.
                // NOTE: It is always advisable to not to use FileStream for text files because it writes bytes, but StreamWriter
                // encodes the output as text.
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"H:\StudentMgtSystem\StudentDataInput-graded.txt"))
                {
                    foreach (Student s in sortedlist)
                    {
                        string sortedrecord = s.FirstName + ", " + s.LastName + ", " + s.Marks;
                        //System.Console.WriteLine(sortedrecord);
                        listBox2.Items.Add(sortedrecord);
                        file.WriteLine(sortedrecord);
                    }
                }
            }
            else
            {
                MessageBox.Show("File is empty. Please check and re-execute.", "File is empty!", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();

            if(OpenFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename=OpenFileDialog1.FileName;
                label3.Show();
                label3.Text = "Input File:" + filename;
                button1.Show();


            }
        }
    }
}

//Student class is to load the student details in the obejct.
//Provison to add more fields in the future.
public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Marks { get; set; }
}

