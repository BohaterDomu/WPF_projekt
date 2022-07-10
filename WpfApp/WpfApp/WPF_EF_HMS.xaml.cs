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
    /// Logika interakcji dla klasy WPF_EF_HMS.xaml
    /// </summary>
    public partial class WPF_EF_HMS : Window
    {
        public WPF_EF_HMS()
        {
            InitializeComponent();

            HospitalManagementDBEntities1 db = new HospitalManagementDBEntities1();
            var docs = from d in db.Doctors
                       select new
                       {
                           DoctorName = d.Name,
                           Speciality = d.Specialization,

                       };

            foreach (var item in docs)
            {
                Console.WriteLine(item.DoctorName);
                Console.WriteLine(item.Speciality);
            }

            this.gridDoctors.ItemsSource = docs.ToList();
        }

        private int updatingDoctorID = 0;
        private void gridDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridDoctors.SelectedIndex >= 0)
            {
                if (this.gridDoctors.SelectedItems.Count >= 0)
                {
                    if (this.gridDoctors.SelectedItems[0].GetType() == typeof(Doctor))
                    {
                        Doctor d = (Doctor)this.gridDoctors.SelectedItems[0];
                        this.txtName2.Text = d.Name;
                        this.txtSpecialization2.Text = d.Specialization;
                        this.txtQualification2.Text = d.Qualification;
                        this.updatingDoctorID = d.Id;
                    }
                }
            }
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities1 db = new HospitalManagementDBEntities1();

            Doctor DoctorObject = new Doctor()
            {
                Name = txtName.Text,
                Qualification = txtQualification.Text,
                Specialization = txtSpecialization.Text
            };

            db.Doctors.Add(DoctorObject);
            db.SaveChanges();
        }

        private void btnLoadDoctors_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities1 db = new HospitalManagementDBEntities1();

            this.gridDoctors.ItemsSource = db.Doctors.ToList();
        }

        private void btnUpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            HospitalManagementDBEntities1 db = new HospitalManagementDBEntities1();
            var r = from d in db.Doctors
                    where d.Id == this.updatingDoctorID
                    select d;

            Doctor obj = r.SingleOrDefault();

            if (obj != null)
            {
                obj.Name = this.txtName2.Text;
                obj.Specialization = this.txtSpecialization2.Text;
                obj.Qualification = this.txtQualification2.Text;

                db.SaveChanges();
            }



        }

        private void btnDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult msgBoxResult = MessageBox.Show("Are you sure you want to Delete?",
                "Delete Doctor",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No
                );

            if (msgBoxResult == MessageBoxResult.Yes)
            {



                HospitalManagementDBEntities1 db = new HospitalManagementDBEntities1();

                var r = from d in db.Doctors
                        where d.Id == this.updatingDoctorID
                        select d;

                Doctor obj = r.SingleOrDefault();

                if (obj != null)
                {
                    db.Doctors.Remove(obj);
                    db.SaveChanges();

                }
            }

        }
    }
}
