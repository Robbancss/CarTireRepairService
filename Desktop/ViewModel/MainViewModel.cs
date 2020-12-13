using Desktop.Model;
using Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private APIService _model;
        private ObservableCollection<WorkshopDTO> _workshops;
        private WorkshopDTO _selectedWorkshop;
        private List<string> _serviceNames;
        private string _selectedServiceName;
        private CarserviceDTO _carservice;
        private ObservableCollection<ReservationDTO> _reservations;
        private ReservationDTO _selectedReservation;

        public WorkshopDTO EditedWorkshop { get; private set; }

        public ReservationDTO SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                if (_selectedReservation != value)
                {
                    _selectedReservation = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ReservationDTO> Reservations
        {
            get { return _reservations; }
            private set
            {
                if (_reservations != value)
                {
                    _reservations = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedServiceName
        {
            get { return _selectedServiceName; }
            set
            {
                if (_selectedServiceName != value)
                {
                    _selectedServiceName = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<WorkshopDTO> Workshops
        {
            get { return _workshops; }
            private set
            {
                if (_workshops != value)
                {
                    _workshops = value;
                    OnPropertyChanged();
                }
            }
        }

        public WorkshopDTO SelectedWorkshop
        {
            get { return _selectedWorkshop; }
            set
            {
                if (_selectedWorkshop != value)
                {
                    _selectedWorkshop = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> ServiceNames
        {
            get { return _serviceNames; }
        }

        #region DelegateCommands (amiket a nézetbe belerakunk
        
        public DelegateCommand LogoutCommand { get; private set; }

        public DelegateCommand WorkshopDetailsCommand { get; private set; }

        public DelegateCommand LoadCommand { get; private set; }

        public DelegateCommand DeleteReservationCommand { get; private set; }

        public DelegateCommand OpenBuildWorkshopWindowCommand { get; private set; }

        public DelegateCommand CancelBuildWindowCommand { get; private set; }

        public DelegateCommand SaveBuildWindowCommand { get; private set; }

        #endregion

        #region EventHandlers (amin keresztül üzenünk az App.xml-nek)

        public event EventHandler LogoutSucceeded;

        public event EventHandler WorkshopDetailsLoaded;

        public event EventHandler OpenBuildWorkshopWindow;

        public event EventHandler CancelBuildWorkshopWindow;

        public event EventHandler SaveBuildBuildWorkshopWindow;

        #endregion

        #region OnSomethingEvent (üzenet aktiválása az App.xml-nek)

        private void OnSaveBuildWorkshopWindow()
        {
            SaveBuildBuildWorkshopWindow?.Invoke(this, EventArgs.Empty);
        }

        private void OnCancelBuildWorkshopWindow()
        {
            CancelBuildWorkshopWindow?.Invoke(this, EventArgs.Empty);
        }

        private void OnOpenBuildWorkshopWindow()
        {
            OpenBuildWorkshopWindow?.Invoke(this, EventArgs.Empty);
        }

        private void OnLogoutSuccess()
        {
            LogoutSucceeded?.Invoke(this, EventArgs.Empty);
        }

        private void OnWorkshopDetailsLoaded()
        {
            WorkshopDetailsLoaded?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        public MainViewModel(APIService model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            _carservice = new CarserviceDTO();
            _carservice.AirConCharging = false;
            _carservice.PunctureRepair = false;
            _carservice.SuspensionAdjustment = false;
            _carservice.TireReplacement = false;

            _serviceNames = new List<string>();
            _serviceNames.Add(nameof(CarserviceDTO.AirConCharging));
            _serviceNames.Add(nameof(CarserviceDTO.PunctureRepair));
            _serviceNames.Add(nameof(CarserviceDTO.SuspensionAdjustment));
            _serviceNames.Add(nameof(CarserviceDTO.TireReplacement));

            _model = model; 

            LogoutCommand = new DelegateCommand(param => LogoutAsync());
            WorkshopDetailsCommand = new DelegateCommand(param => WorkshopDetails());
            LoadCommand = new DelegateCommand(param => LoadAsync());
            DeleteReservationCommand = new DelegateCommand(param => DeleteReservation());
            OpenBuildWorkshopWindowCommand = new DelegateCommand(param => LoadBuildWorkshopWindow());
            CancelBuildWindowCommand = new DelegateCommand(param => CancelBuildWindow());
            SaveBuildWindowCommand = new DelegateCommand(param => SaveBuildWindow());
        }

        public async void SaveBuildWindow()
        {
            try
            {
                await _model.SaveNewWorkshop(EditedWorkshop);

                LoadAsync();
                OnSaveBuildWorkshopWindow();
            }
            catch (Exception ex)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        public async void CancelBuildWindow()
        {
            try
            {
                OnCancelBuildWorkshopWindow();
            }
            catch (Exception ex)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void LoadBuildWorkshopWindow()
        {
            try
            {
                EditedWorkshop = new WorkshopDTO();
                EditedWorkshop.ProvidedServices = new Persistence.Models.CarServices();
                OnOpenBuildWorkshopWindow();
            }
            catch (Exception ex)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void LogoutAsync()
        {
            try
            {
                await _model.LogoutAsync();
                OnLogoutSuccess();
            }
            catch (Exception ex)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void WorkshopDetails()
        {
            try
            {
                if (SelectedWorkshop == null || SelectedServiceName == null)
                    return;

                _carservice = new CarserviceDTO();

                if (SelectedServiceName == nameof(CarserviceDTO.AirConCharging))
                {
                    _carservice.AirConCharging = true;
                }
                else if (SelectedServiceName == nameof(CarserviceDTO.PunctureRepair))
                {
                    _carservice.PunctureRepair = true;
                }
                else if (SelectedServiceName == nameof(CarserviceDTO.SuspensionAdjustment))
                {
                    _carservice.SuspensionAdjustment = true;
                }
                else if (SelectedServiceName == nameof(CarserviceDTO.TireReplacement))
                {
                    _carservice.TireReplacement = true;
                }

                await _model.LoadReservationByIDAsync(SelectedWorkshop.ID, _carservice);
                Reservations = new ObservableCollection<ReservationDTO>(_model.Reservations);

                OnWorkshopDetailsLoaded();
            }
            catch (Exception)
            {
                OnMessageApplication("Something went wrong");
            }
        }

        private async void DeleteReservation()
        {
            if (SelectedReservation == null)
                return;

            if (OkCancelDialog("You are going to delete this reservation time:" + SelectedReservation.ReservationTime.ToString(), "Delete reservation"))
            {
                try
                {
                    await _model.DeleteReservationAsync(SelectedReservation.ID);
                    Reservations.Remove(SelectedReservation);
                    SelectedReservation = null;
                }
                catch (Exception)
                {
                    OnMessageApplication("Something went wrong");
                }
            }

        }

        private async void LoadAsync()
        {
            try
            {
                await _model.LoadAsync();
                Workshops = new ObservableCollection<WorkshopDTO>(_model.Workshops);
            }
            catch (Exception)
            {
                OnMessageApplication("Something went wrong");
            }
        }
    }
}
