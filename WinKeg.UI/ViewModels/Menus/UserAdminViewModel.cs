using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinKeg.Data.Models;
using WinKeg.Data;
using WinKeg.UI.Views;

namespace WinKeg.UI.ViewModels.Menus
{
    public class UserAdminViewModel : INotifyPropertyChanged
    {
        private readonly INavService _navService;

        public UserAdminViewModel(INavService nav)
        {
            _navService = nav;

            // Load the user data
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                Users = new ObservableCollection<User>(unitOfWork.Users.GetAll());
                unitOfWork.Dispose();
            }

            // Wire up controls
            AddUser = new RelayCommand(AddUserPressed);
            DeleteUser = new RelayCommand(DeleteUserPressed, CanDeleteUser);
            SaveUser = new RelayCommand(SaveUserPressed, CanSaveUser);
            SetPasscode = new RelayCommand(SetPasscodePressed, CanSetPasscode);
            CloseMenu = new RelayCommand(CloseMenuPressed);

        }

        private User tempUser;

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Users"));
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedUser"));
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                // Crappy method to revert unsaved changes
                if(tempUser != null)
                {
                    Users.Where(u => u.Id == tempUser.Id).First().Name = tempUser.Name;
                    Users.Where(u => u.Id == tempUser.Id).First().IsBeverageRestricted = tempUser.IsBeverageRestricted;
                    Users.Where(u => u.Id == tempUser.Id).First().IsAdministrator = tempUser.IsAdministrator;
                    Users.Where(u => u.Id == tempUser.Id).First().PCSalt = tempUser.PCSalt;
                    Users.Where(u => u.Id == tempUser.Id).First().EncryptedPasscode = tempUser.EncryptedPasscode;
                }
                if(value != null)
                {
                    tempUser = new User()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        IsAdministrator = value.IsAdministrator,
                        IsBeverageRestricted = value.IsBeverageRestricted,
                        EncryptedPasscode = value.EncryptedPasscode,
                        PCSalt = value.PCSalt,
                        HistoryEvents = value.HistoryEvents,
                        LastModified = value.LastModified
                    };
                }

                _selectedUser = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedUser"));
                SetPasscode.RaiseCanExecuteChanged();
                SaveUser.RaiseCanExecuteChanged();
                DeleteUser.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand AddUser { get; private set; }
        public void AddUserPressed()
        {
            User newUser = new User();
            Users.Add(newUser);
            PropertyChanged(this, new PropertyChangedEventArgs("Users"));
            SaveUser.RaiseCanExecuteChanged();
            DeleteUser.RaiseCanExecuteChanged();
            SelectedUser = newUser;
        }

        public RelayCommand DeleteUser { get; private set; }
        public void DeleteUserPressed()
        {
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                unitOfWork.Users.Delete(SelectedUser);
                unitOfWork.Complete();
                unitOfWork.Dispose();
            }
            tempUser = null;
            DeleteUser.RaiseCanExecuteChanged();
            SaveUser.RaiseCanExecuteChanged();
            SetPasscode.RaiseCanExecuteChanged();
            Users.Remove(SelectedUser);
        }
        public bool CanDeleteUser()
        {
            if(SelectedUser == null) { return false; }
            if(Users.Where(u => u.IsAdministrator && u.Id != 0).Count() == 1 && SelectedUser.Id != 0 && SelectedUser.IsAdministrator) { return false; } // Don't let last admin delete themselves!
            if(Users.Where(u => u.Id != 0).Count() > 1) { return true; }
            return false;
        }

        public RelayCommand SaveUser { get; private set; }
        public void SaveUserPressed()
        {
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                SelectedUser.LastModified = DateTime.Now;
                if (SelectedUser.Id == 0)
                {
                    unitOfWork.Users.Add(SelectedUser);
                }
                else
                {
                    if (Users.Where(u => u.IsAdministrator && u.Id != 0).Count() == 0 && SelectedUser.Id != 0 && !SelectedUser.IsAdministrator) { SelectedUser.IsAdministrator = true; } // Make sure there's always one admin
                    unitOfWork.Users.Edit(SelectedUser);
                }
                unitOfWork.Complete();
                unitOfWork.Dispose();
            }
            tempUser = null;
            SelectedUser = null;
            DeleteUser.RaiseCanExecuteChanged();
            SaveUser.RaiseCanExecuteChanged();
            SetPasscode.RaiseCanExecuteChanged();
        }
        public bool CanSaveUser()
        {
            if(SelectedUser == null) { return false; }
            if (SelectedUser.PCSalt == null || SelectedUser.EncryptedPasscode == null)
                return false;
            return true;
        }

        public RelayCommand SetPasscode { get; private set; }
        public async void SetPasscodePressed()
        {
            Dialogs.PasscodeDialog passcodeDialog = new Dialogs.PasscodeDialog(SelectedUser);
            passcodeDialog.XamlRoot = App.rootFrame.XamlRoot;
            await passcodeDialog.ShowAsync();
            SaveUser.RaiseCanExecuteChanged();
        }

        public bool CanSetPasscode()
        {
            return (SelectedUser != null);
        }

        public RelayCommand CloseMenu { get; private set; }
        public void CloseMenuPressed()
        {
            _navService.Navigate(typeof(MenuPageView));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
