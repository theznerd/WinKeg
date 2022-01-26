using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinKeg.Hardware.Base
{
    public class GPIO
    {
        private GpioController GpioController;
        private int PinId;
        public event PinChangeEventHandler? GpioPinPulsed;

        public GPIO(int pin)
        {
            GpioController = new GpioController(PinNumberingScheme.Logical);
            PinId = pin;
        }

        // Open the pin for action
        private void OpenPin()
        {
            GpioController.OpenPin(PinId);
        }

        private void ClosePin()
        {
            GpioController.ClosePin(PinId);
        }

        public void SetPinHigh()
        {
            OpenPin();
            GpioController.SetPinMode(PinId, PinMode.Output);
            GpioController.Write(PinId, PinValue.High);
            ClosePin();
        }
        public void SetPinLow()
        {
            OpenPin();
            GpioController.SetPinMode(PinId, PinMode.Output);
            GpioController.Write(PinId, PinValue.Low);
            ClosePin();
        }

        public int ReadPin()
        {
            OpenPin();
            PinValue lastValue = GpioController.Read(PinId);
            ClosePin();
            
            switch(lastValue.ToString())
            {
                case "Low":
                    return 0;
                case "High":
                    return 1;
                default:
                    return 0;
            }
        }

        // Handling pulses for hall effect sensors
        public void StartPulseRead()
        {
            OpenPin();

            // Set the pin to input, pull down
            // the hall effect sensor will pulse high
            // so we want any float in voltage (maybe from
            // interference from a different hall effect
            // sensor to be pulled down
            GpioController.SetPinMode(PinId, PinMode.InputPullDown);
            GpioController.RegisterCallbackForPinValueChangedEvent(PinId, PinEventTypes.Rising, GpioPinPulsed);
        }

        public void StopPulseRead()
        {
            GpioController.UnregisterCallbackForPinValueChangedEvent(PinId, GpioPinPulsed);
        }
    }
}
