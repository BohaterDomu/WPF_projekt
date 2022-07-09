using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Logika interakcji dla klasy WPF_8_EF_HMS.xaml
    /// </summary>
    public partial class WPF_8_EF_HMS : Window
    {
        public WPF_8_EF_HMS()
        {
            InitializeComponent();

            HospitalManagementDBEntities1 db = new HospitalManagementDBEntities1();
            var docs = from d in db.Doctors
                       select new
                       {
                           DoctorName = d.Name,
                           Speciality = d.Specialization
                       };

            foreach (var item in docs)
            {
                Console.WriteLine(item.DoctorName);
                Console.WriteLine(item.Speciality);
            }

            this.gridDoctors.ItemsSource = docs.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities1 db = new HospitalManagementDBEntities1();

            Doctor doctorObject = new Doctor()
            { 
                Name = txtName.Text,
                Qualification = txtQualification.Text,
                Specialization = txtSpecialization.Text
            };

            db.Doctors.Add(doctorObject);
            db.SaveChanges();


        }

        private void btnLoadDoctors_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities1 db = new HospitalManagementDBEntities1();
             
            this.gridDoctors.ItemsSource = db.Doctors.ToList();
        }

        private void GridDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(this.gridDoctors.SelectedItem);
        }

        private void btnUpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities1 db = new HospitalManagementDBEntities1();
            var r = from d in db.Doctors
                    where d.Id == 1
                    select d;

            foreach (var item in r)
            {
                MessageBox.Show(item.Name);
                item.Name = "Dr. Ahmed Updated!";
            }

            db.SaveChanges();


        }
    }
}
