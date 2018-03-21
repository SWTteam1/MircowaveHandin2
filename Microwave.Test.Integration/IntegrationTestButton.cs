using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class IntegrationTestButton
    {
        private IDoor _door;
        private ICookController _cookController;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private ILight _light;
        private IOutput _output;
        private IUserInterface _interface;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        [SetUp]
        public void Setup()
        {
        
            _door = Substitute.For<IDoor>();
            _cookController = Substitute.For<ICookController>();
            _display = Substitute.For<IDisplay>();
            _powerTube = Substitute.For<IPowerTube>();
            _timer = Substitute.For<ITimer>();
            _light = Substitute.For<ILight>();
            _output = Substitute.For<IOutput>();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _interface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);      

        }

        [Test]
        public void TimeButtonPress()
        {
            _powerButton.Press();
            _timeButton.Press();
            _display.Received().ShowTime(1,0);
        }

        [Test]
        public void ButtonPressPower()
        {
            _powerButton.Press();
            _display.Received().ShowPower(50);
           
        }

        [Test]
        public void ButtonPressCancel()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            _light.Received().TurnOff();   
        }

        [Test]
        public void ButtonPressStart()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _light.Received().TurnOn();
        }
    }
}
