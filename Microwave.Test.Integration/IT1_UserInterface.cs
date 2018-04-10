using System;
using NUnit.Framework;
using NSubstitute;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT1_UserInterface
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IDisplay _display;
        private ICookController _cookController;
        private IOutput _output;
        private Light _light;
        private UserInterface _uutUserInterface;

        [SetUp]
        public void SetUp()
        {
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _door = Substitute.For<IDoor>();
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _uutUserInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, 
                _door, _display, _light, _cookController);
        }

        [Test]
        public void DoorOpen_LightOn()
        {
            _uutUserInterface.OnDoorOpened(_door, EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void DoorClosed_LightOff()
        {
            //Sets state to "DOOROPEN"
            _uutUserInterface.OnDoorOpened(_door, EventArgs.Empty);

            _uutUserInterface.OnDoorClosed(_door, EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));
        }
    }
}