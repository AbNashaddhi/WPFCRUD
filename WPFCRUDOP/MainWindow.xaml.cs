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

        private void DisplayEmployee(Employee employee)
        {
            try
            {
                txtId.Text = employee.Id.ToString();
                txtFname.Text = employee.FirstName;
                txtLname.Text = employee.LastName;
                txtAge.Text = employee.Age;
                txtSalary.Text = employee.Salary;
                txtDesignation.Text = employee.Designation;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Employee AddEmployee()
        {
                Employee employee = new Employee();
            employee.FirstName = txtFname.Text;
            employee.LastName = txtLname.Text;
            employee.Age = txtAge.Text;
            employee.Salary = txtSalary.Text;
            employee.Designation = txtDesignation.Text;
                return employee;
        }
        private void btnUpadate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ISession session = SessionClass.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        Employee employeeGrid = EmployeeGrid.SelectedItem as Employee;
                        Employee employee = AddEmployee();
                        employee.Id = employeeGrid.Id;
                        session.Update(employee);
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
            
            try
            {
                using (ISession session = SessionClass.OpenSession())
                {
                    Employee employee = AddEmployee();
                    session.Save(employee);
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
                        var employee = EmployeeGrid.SelectedItem as Employee;
                        session.Delete(employee);
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
