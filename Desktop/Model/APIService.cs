using Persistence.DTO;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Persistence.Models;

namespace Desktop.Model
{
    public class APIService
    {
        private readonly HttpClient _client;
        private List<WorkshopDTO> _workshops;
        private List<ReservationDTO> _reservations;

        public APIService(string baseAddress)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };

            IsUserLoggedIn = false;
        }

        public IReadOnlyList<WorkshopDTO> Workshops
        {
            get { return _workshops; }
        }

        public IReadOnlyList<ReservationDTO> Reservations
        {
            get { return _reservations; }
        }

        public Boolean IsUserLoggedIn { get; private set; }

        #region Auth

        public async Task<bool> LoginAsync(string name, string password)
        {
            LoginDTO user = new LoginDTO
            {
                UserName = name,
                Password = password
            };

            HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/Login", user);

            if (response.IsSuccessStatusCode)
            {
                IsUserLoggedIn = true;
                return IsUserLoggedIn;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        public async Task LogoutAsync()
        {
            HttpResponseMessage response = await _client.PostAsync("api/Account/Logout", null);

            if (response.IsSuccessStatusCode)
            {
                IsUserLoggedIn = false;

                return;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        #endregion

        #region Stuff

        public async Task LoadAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/workshops/");
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<WorkshopDTO> workshops = await response.Content.ReadAsAsync<IEnumerable<WorkshopDTO>>();
                    _workshops = workshops.ToList();
                }
                else
                {
                    throw new NetworkException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new NetworkException("NetworkException: " + ex);
            }
        }

        public async Task LoadReservationByIDAsync(Int32 id, CarserviceDTO serviceType)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("api/workshops/" + id);
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<ReservationDTO> reservations = await response.Content.ReadAsAsync<IEnumerable<ReservationDTO>>();

                    if (serviceType.AirConCharging)
                    {
                        _reservations = reservations.Where(res => res.ProvidedService.AirConCharging).ToList();
                    } 
                    else if (serviceType.PunctureRepair)
                    {
                        _reservations = reservations.Where(res => res.ProvidedService.PunctureRepair).ToList();
                    } 
                    else if (serviceType.SuspensionAdjustment)
                    {
                        _reservations = reservations.Where(res => res.ProvidedService.SuspensionAdjustment).ToList();
                    } 
                    else if (serviceType.TireReplacement)
                    {
                        _reservations = reservations.Where(res => res.ProvidedService.TireReplacement).ToList();
                    }
                }
                else
                {
                    throw new NetworkException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new NetworkException("NetworkException: " + ex);
            }
        }


        public async Task DeleteReservationAsync(Int32 id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync("api/reservation/" + id);
                if (response.IsSuccessStatusCode)
                    return;
                else
                {
                    throw new NetworkException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new NetworkException("NetworkException: " + ex);
            }
        }

        public async Task SaveNewWorkshop(WorkshopDTO newWorkshop)
        {
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/workshops/", newWorkshop);

                if (response.IsSuccessStatusCode)
                    return;
                else
                {
                    throw new NetworkException("Service returned response: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new NetworkException("NetworkException: " + ex);
            }
        }

        #endregion
    }
}
