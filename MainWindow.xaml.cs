using AutoLotModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Car_Office
{
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        AutoLotEntitiesModel ctx = new AutoLotEntitiesModel();
        CollectionViewSource driverVSource;
        CollectionViewSource carVSource;
        CollectionViewSource permissionVSource;
        CollectionViewSource bookingVSource;
        CollectionViewSource registrationVSource;
        CollectionViewSource carRegistrationVSource;
        //private System.Collections.Generic.List<string> driver = new System.Collections.Generic.List<string>();
        //private System.Collections.Generic.List<string> car = new System.Collections.Generic.List<string>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            driverVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("driverViewSource")));
            driverVSource.Source = ctx.Drivers.Local;
            ctx.Drivers.Load();
            carRegistrationVSource =
((System.Windows.Data.CollectionViewSource)(this.FindResource("carRegistrationViewSource")));

            //carRegistrationVSource.Source = ctx.Registrations.Local;
            ctx.Registrations.Load();
            ctx.Permissions.Load();
            cmbCar.ItemsSource = ctx.Cars.Local;
            //cmbCar.DisplayMemberPath = "Class";
            //cmbCar.DisplayMemberPath = "Manufacturer";
            //cmbCar.DisplayMemberPath = "Model";
            // cmbCar.DisplayMemberPath = "Year";
            //cmbCar.DisplayMemberPath = "Color";
            cmbCar.SelectedValuePath = "Car_Id";
            cmbPermission.ItemsSource = ctx.Permissions.Local;
            // cmbPermission.DisplayMemberPath = "Car_Registration_Documents";
            //cmbPermission.DisplayMemberPath = "Phone_No";
            //cmbPermission.DisplayMemberPath = "Worker_Id";
            cmbPermission.SelectedValuePath = "Perm_Id";
            BindDataGrid();




            System.Windows.Data.CollectionViewSource driverViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("driverViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // driverViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource carViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("carViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // carViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource bookingViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("bookingViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // bookingViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource permissionViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("permissionViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // permissionViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource registrationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("registrationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // registrationViewSource.Source = [generic data source]
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
        }
        private void btnEditO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(car_IdTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(classTextBox, TextBox.TextProperty);
            SetValidationBinding();

        }
        private void btnDeleteO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            driverVSource.View.MoveCurrentToNext();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            driverVSource.View.MoveCurrentToPrevious();
        }
        private void SaveDrivers()
        {
            Driver driver = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    driver = new Driver()
                    {
                        Full_Name = full_NameTextBox.Text.Trim(),
                        Post_Code = int.Parse(post_CodeTextBox.Text.Trim()),
                        Birth_Date = birth_DateDatePicker.SelectedDate,
                        License_Class = license_ClassTextBox.Text.Trim(),
                        Email = emailTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Drivers.Add(driver);
                    driverVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    driver = (Driver)driverDataGrid.SelectedItem;
                    driver.Full_Name = full_NameTextBox.Text.Trim();
                    driver.Post_Code = int.Parse(post_CodeTextBox.Text.Trim());
                    driver.Birth_Date = birth_DateDatePicker.SelectedDate;
                    driver.License_Class = license_ClassTextBox.Text.Trim();
                    driver.Email = emailTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    driver = (Driver)driverDataGrid.SelectedItem;
                    ctx.Drivers.Remove(driver);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                driverVSource.View.Refresh();
            }

        }
        private void btnNextCar_Click(object sender, RoutedEventArgs e)
        {
            carVSource.View.MoveCurrentToNext();
        }
        private void btnPreviousCar_Click(object sender, RoutedEventArgs e)
        {
            carVSource.View.MoveCurrentToPrevious();
        }
        private void SaveCars()
        {
            Car car = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    car = new Car()
                    {
                        Class = classTextBox.Text.Trim(),
                        Manufacturer = manufacturerTextBox.Text.Trim(),
                        Model = modelTextBox.Text.Trim(),
                        Year = int.Parse(yearTextBox.Text.Trim()),
                        Color = colorTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Cars.Add(car);
                    carVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    car = (Car)carDataGrid.SelectedItem;
                    car.Class = classTextBox.Text.Trim();
                    car.Manufacturer = manufacturerTextBox.Text.Trim();
                    car.Model = modelTextBox.Text.Trim();
                    car.Year = int.Parse(yearTextBox.Text.Trim());
                    car.Color = colorTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    car = (Car)carDataGrid.SelectedItem;
                    ctx.Cars.Remove(car);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                carVSource.View.Refresh();
            }

        }
        private void btnNextB_Click(object sender, RoutedEventArgs e)
        {
            bookingVSource.View.MoveCurrentToNext();
        }
        private void btnPreviousB_Click(object sender, RoutedEventArgs e)
        {
            bookingVSource.View.MoveCurrentToPrevious();
        }
        private void SaveBookings()
        {
            Booking booking = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    booking = new Booking()
                    {

                        Date_Time = date_TimeDatePicker.SelectedDate,
                        Location = locationTextBox.Text.Trim(),
                        Full_Name = full_NameTextBox.Text.Trim(),
                        License_Id = int.Parse(license_IdTextBox.Text.Trim()),
                        Car_Id = int.Parse(car_IdTextBox.Text.Trim())

                    };
                    //adaugam entitatea nou creata in context
                    ctx.Bookings.Add(booking);
                    bookingVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    booking = (Booking)bookingDataGrid.SelectedItem;
                    booking.Date_Time = date_TimeDatePicker.SelectedDate;
                    booking.Location = locationTextBox.Text.Trim();
                    booking.Full_Name = full_NameTextBox.Text.Trim();
                    booking.License_Id = int.Parse(license_IdTextBox.Text.Trim());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    booking = (Booking)bookingDataGrid.SelectedItem;
                    ctx.Bookings.Remove(booking);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                bookingVSource.View.Refresh();
            }

        }
        private void btnNextP_Click(object sender, RoutedEventArgs e)
        {
            permissionVSource.View.MoveCurrentToNext();
        }
        private void btnPreviousP_Click(object sender, RoutedEventArgs e)
        {
            permissionVSource.View.MoveCurrentToPrevious();
        }
        private void SavePermission()
        {
            Permission permission = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    permission = new Permission()
                    {
                        Car_Registration_Documents = int.Parse(car_Registration_DocumentsTextBox.Text.Trim()),
                        Phone_No = int.Parse(phone_NoTextBox.Text.Trim()),
                        Worker_Id = int.Parse(worker_IdTextBox.Text.Trim())
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Permissions.Add(permission);
                    permissionVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    permission = (Permission)permissionDataGrid.SelectedItem;
                    permission.Car_Registration_Documents = int.Parse(car_Registration_DocumentsTextBox.Text.Trim());
                    permission.Phone_No = int.Parse(phone_NoTextBox.Text.Trim());
                    permission.Worker_Id = int.Parse(worker_IdTextBox.Text.Trim());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    permission = (Permission)permissionDataGrid.SelectedItem;
                    ctx.Permissions.Remove(permission);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                permissionVSource.View.Refresh();
            }

        }


        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrlAutoLo.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Drivesr":
                    SaveDrivers();
                    break;
                case "Cars":
                    SaveCars();
                    break;
                case "Bookings":
                    SaveBookings();
                    break;
                case "Permission":
                    SavePermission();
                    break;
                case "Registration":
                    break;
            }
            ReInitialize();
        }
        private void SaveRegistration()
        {
            Registration registration = null;
            if (action == ActionState.New)
            {
                try
                {
                    Car car = (Car)cmbCar.SelectedItem;
                    Permission permission = (Permission)cmbPermission.SelectedItem;
                    //instantiem Order entity
                    registration = new Registration()
                    {

                        Car_Id = car.Car_Id,
                        Perm_Id = permission.Perm_Id
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Registrations.Add(registration);
                    //salvam modificarile
                    ctx.SaveChanges();
                    BindDataGrid();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
if (action == ActionState.Edit)
            {
                dynamic selectedRegistration = registrationDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedRegistration.Reg_Id;
                    var editedRegistration = ctx.Registrations.FirstOrDefault(s => s.Reg_Id == curr_id);
                    if (editedRegistration != null)
                    {
                        editedRegistration.Car_Id = Int32.Parse(cmbCar.SelectedValue.ToString());
                        editedRegistration.Perm_Id = Convert.ToInt32(cmbPermission.SelectedValue.ToString());
                        //salvam modificarile
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                // pozitionarea pe item-ul curent
                carRegistrationVSource.View.MoveCurrentTo(selectedRegistration);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedRegistration = registrationDataGrid.SelectedItem;
                    int curr_id = selectedRegistration.Reg_Id;
                    var deletedRegistration = ctx.Registrations.FirstOrDefault(s => s.Reg_Id == curr_id);
                    if (deletedRegistration != null)
                    {
                        ctx.Registrations.Remove(deletedRegistration);
                        ctx.SaveChanges();
                        MessageBox.Show("Registration Deleted Successfully", "Message");
                        BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }
        private void BindDataGrid()
        {
            var queryOrder = from reg in ctx.Registrations
                             join car in ctx.Cars on reg.Car_Id equals car.Car_Id
                             join perm in ctx.Permissions on reg.Perm_Id equals perm.Perm_Id
                             select new
                             {
                                 reg.Reg_Id,
                                 reg.Perm_Id,
                                 reg.Car_Id,
                                 car.Class,
                                 car.Manufacturer,
                                 car.Model,
                                 car.Year,
                                 car.Color,
                                 perm.Car_Registration_Documents,
                                 perm.Phone_No,
                                 perm.Worker_Id
                             };
            carRegistrationVSource.Source = queryOrder.ToList();
        }
        private void SetValidationBinding()
        {
            Binding car_IdValidationBinding = new Binding();
            car_IdValidationBinding.Source = carVSource;
            car_IdValidationBinding.Path = new PropertyPath("car_Id");
            car_IdValidationBinding.NotifyOnValidationError = true;
            car_IdValidationBinding.Mode = BindingMode.TwoWay;
            car_IdValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //string required
            car_IdValidationBinding.ValidationRules.Add(new StringNotEmpty());
            car_IdTextBox.SetBinding(TextBox.TextProperty, car_IdValidationBinding);
            Binding classValidationBinding = new Binding();
            classValidationBinding.Source = carVSource;
            classValidationBinding.Path = new PropertyPath("class");
            classValidationBinding.NotifyOnValidationError = true;
            classValidationBinding.Mode = BindingMode.TwoWay;
            classValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //string min length validator
            classValidationBinding.ValidationRules.Add(new
           StringMinLengthValidator());
            classTextBox.SetBinding(TextBox.TextProperty,
           classValidationBinding); //setare binding nou
        }
    }
}
