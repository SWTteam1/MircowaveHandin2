using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using NUnit.Framework;
using NSubstitute;

namespace Microwave.Test.Integration
{
    class IntegrationUserInferface
    {
        private IUserInterface _interface;
        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;
        private IButton _powerButton, _timeButton, _scButton;

        private IDoor _door;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _scButton = Substitute.For<IButton>();
            _cookController = Substitute.For<ICookController>();
            _interface = new UserInterface(_powerButton, _timeButton, _scButton, _door, _display, _light,
                _cookController);

        }

        [Test]
        public void UserInterface_scButton_CookControllerStart()
        {
            _interface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _interface.OnTimePressed(_timeButton, EventArgs.Empty);
            _interface.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _cookController.Received().StartCooking(50, 60);
        }

        [Test]
        public void UserInterface_scButton_CookControllerCancel()
        {
            _interface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _interface.OnTimePressed(_timeButton, EventArgs.Empty);
            _interface.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _interface.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _cookController.Received().Stop();

        }

        [Test]
        public void UserInterface_LightOn()
        {
            _interface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _interface.OnTimePressed(_timeButton, EventArgs.Empty);
            _interface.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _light.Received().TurnOn();
        }


        [Test]
        public void UserInterface_LightOff()
        {
            _interface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _interface.OnTimePressed(_timeButton, EventArgs.Empty);
            _interface.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _interface.CookingIsDone();
            _light.Received().TurnOff();
        }


        [Test]
        public void UserInterface_DoorOpen_LightOn()
        {
            _interface.OnDoorOpened(_door, EventArgs.Empty);
            _light.Received().TurnOn();
        }

        [Test]
        public void UserInterface_DoorClosed_LightOff()
        {
            _interface.OnDoorOpened(_door, EventArgs.Empty);
            _interface.OnDoorClosed(_door, EventArgs.Empty);
            _light.Received().TurnOff();
        }



        [Test]
        public void UserInterface_DisplayShowTime()
        {
            _interface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _interface.OnTimePressed(_timeButton, EventArgs.Empty);
            _display.Received().ShowTime(1,0);
        }

        [Test]
        public void UserInterface_CookingIsDone_LightOff()
        {
            _interface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _interface.OnTimePressed(_timeButton, EventArgs.Empty);
            _interface.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _interface.CookingIsDone();
            _light.Received().TurnOff();
        }


        [Test]
        public void UserInterface_CookingDoneDisplayClear()
        { 
            _interface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _interface.OnTimePressed(_timeButton, EventArgs.Empty);
            _interface.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _interface.CookingIsDone();
            _display.Received().Clear();
        }

        [Test]
        public void UserInterface_DisplayShowPower()
        {
            _interface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _display.Received().ShowPower(50);
        }

    }
}
