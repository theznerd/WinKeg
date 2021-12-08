using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using WinKeg.Data;
using WinKeg.Data.Models;

namespace WinKeg.UI.ViewModels.Controls
{
    public class BeverageImageDialogModel : INotifyPropertyChanged
    {
        public BeverageImageDialogModel()
        {
            using (var unitOfWork = new UnitOfWork(new WinKegContext()))
            {
                BeverageImages = new ObservableCollection<BeverageImage>(unitOfWork.BeverageImages.GetAll());
                unitOfWork.Dispose();
            }

            AddImage = new RelayCommand(AddImagePressed);
        }

        private ObservableCollection<BeverageImage> _beverageImages;
        public ObservableCollection<BeverageImage> BeverageImages
        {
            get => _beverageImages;
            set
            {
                _beverageImages = value;
                PropertyChanged(this, new PropertyChangedEventArgs("BeverageImages"));
            }
        }

        private BeverageImage _selectedImage;
        public BeverageImage SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedImage"));
            }
        }

        public RelayCommand AddImage { get; private set; }
        public async void AddImagePressed()
        {
            var filePicker = new FileOpenPicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            filePicker.FileTypeFilter.Add("*");
            var file = await filePicker.PickSingleFileAsync();

            if (file != null)
            {
                byte[] bytes;
                using (var stream = await file.OpenReadAsync())
                {
                    bytes = new byte[stream.Size];
                    using (var reader = new DataReader(stream))
                    {
                        await reader.LoadAsync((uint)stream.Size);
                        reader.ReadBytes(bytes);
                    }
                }
                

                BeverageImage newImage = new BeverageImage();
                newImage.Name = file.DisplayName;
                newImage.ImageBlob = bytes;

                BeverageImages.Add(newImage);
                SelectedImage = newImage;

                using(var unitOfWork = new UnitOfWork(new WinKegContext()))
                {
                    unitOfWork.BeverageImages.Add(newImage);
                    unitOfWork.Complete();
                    unitOfWork.Dispose();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
