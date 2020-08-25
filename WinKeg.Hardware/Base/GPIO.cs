using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace WinKeg.Hardware.Base
{
    public class GPIO
    {
        private GpioController GpioController;
        private int PinId;
        private GpioPin GpioPin;
        public event EventHandler GpioPinPulsed;

        public GPIO(int pin)
        {
            // Load the GPIO Controller
            GpioController = GpioController.GetDefault();

            // Set the pin for this instance
            PinId = pin;
        }

        // Open the pin for action
        private void OpenPin()
        {
            if(null != GpioPin)
            {
                // Open the pin exclusively so that we can set the value
                // in shared mode we can only read values
                GpioPin = GpioController.OpenPin(PinId, GpioSharingMode.Exclusive);
            }
        }

        private void ClosePin()
        {
            if(null != GpioPin)
            {
                GpioPin.Dispose(); // this closes the connection
                GpioPin = null;
            }
        }

        public void SetPinHigh()
        {
            OpenPin();
            GpioPin.Write(GpioPinValue.High);
            GpioPin.SetDriveMode(GpioPinDriveMode.Output);
            ClosePin();
        }

        public void SetPinLow()
        {
            OpenPin();
            GpioPin.Write(GpioPinValue.Low);
            GpioPin.SetDriveMode(GpioPinDriveMode.Output);
            ClosePin();
        }

        public int ReadPin()
        {
            OpenPin();
            GpioPinValue lastValue = GpioPin.Read();
            ClosePin();
            switch (lastValue)
            {
                case GpioPinValue.High:
                    return 1;
                case GpioPinValue.Low:
                    return 0;
                default:
                    return 0;
            }
        }

        // Handling Hall Effect Pulses
        public void StartPulseRead()
        {
            OpenPin();

            // Set the pin to input, pull down
            // the hall effect sensor will pulse high
            // so we want any float in voltage (maybe from
            // interference from a different hall effect
            // sensor to be pulled down
            GpioPin.SetDriveMode(GpioPinDriveMode.InputPullDown);

            GpioPin.ValueChanged += SensorPulsed;
        }

        public void StopPulseRead()
        {
            // unsubscribe me from cat facts
            GpioPin.ValueChanged -= SensorPulsed;
            ClosePin();
        }

        private void SensorPulsed(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if(args.Edge == GpioPinEdge.RisingEdge) // the voltage pulsed up to high
            {
                // notify anyone subscribing to cat facts
                // that the sensor has pulsed
                GpioPinPulsed?.Invoke(this, null);
            }
        }
    }
}
