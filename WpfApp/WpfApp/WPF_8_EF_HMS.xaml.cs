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

            HospitalManagementDBEntities db = new HospitalManagementDBEntities();
            var docs = from d in db.Doctor
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
    }
}
