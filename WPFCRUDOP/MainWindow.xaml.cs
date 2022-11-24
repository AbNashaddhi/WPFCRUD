using NHibernate;
using System;
using System.Linq;
using System.Windows;

namespace WPFCRUDOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           
            InitializeComponent();
            GetAllEmployees();
        }
        private void GetAllEmployees()
        {
            try
            {
                ISession session = SessionClass.OpenSession();
                var employeeList = session.Query<Employee>().ToList();
                EmployeeGrid.ItemsSource = employeeList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayEmployee(Employee emp)
        {
            try
            {
                txtId.Text = emp.Id.ToString();
                txtFname.Text = emp.FirstName;
                txtLname.Text = emp.LastName;
                txtAge.Text = emp.Age;
                txtSalary.Text = emp.Salary;
                txtDesignation.Text = emp.Designation;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Employee AddEmployee()
        {
                Employee emp = new Employee();
                emp.FirstName = txtFname.Text;
                emp.LastName = txtLname.Text;
                emp.Age = txtAge.Text;
                emp.Salary = txtSalary.Text;
                emp.Designation = txtDesignation.Text;
                return emp;
        }
        private void btnUpadate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ISession session = SessionClass.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        Employee employee = EmployeeGrid.SelectedItem as Employee;
                        Employee emp = AddEmployee();
                        emp.Id = employee.Id;
                        session.Update(emp);
                        transaction.Commit();
                        MessageBox.Show("Employee Details have been Updated");
                        GetAllEmployees();

                    }
                }
                   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Edit_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee = EmployeeGrid.SelectedItem;
                DisplayEmployee(employee as Employee);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //string fname = txtFname.Text;
            //string lname = txtLname.Text;
            //int age = Convert.ToInt32(txtAge.Text);
            //int salary = Convert.ToInt32(txtSalary.Text);
            //string designation = txtDesignation.Text;

            try
            {
                using (ISession session = SessionClass.OpenSession())
                {
                    Employee emp = AddEmployee();
                    session.Save(emp);
                    MessageBox.Show("Employee Details have been added");
                    ClearFields();
                    GetAllEmployees();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (ISession session = SessionClass.OpenSession())
                {
                    using(ITransaction transaction = session.BeginTransaction())
                    {
                        var Employee = EmployeeGrid.SelectedItem as Employee;
                        session.Delete(Employee);
                        transaction.Commit();
                        MessageBox.Show("Employee Details have been deleted");
                        GetAllEmployees();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            ClearFields();
        }
        private void ClearFields()
        {
            txtFname.Text = string.Empty;
            txtLname.Text = string.Empty;
            txtAge.Text = "0";
            txtSalary.Text = "0.00";
            txtDesignation.Text = string.Empty;
        }
        
    }
}
