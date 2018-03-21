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
    class IntegrationUserInferface
    {
        private IUserInterface _interface;
        private ILight _light;
        private IDisplay _display;
        private ICookController _cookController;
        private IButton _powerButton, _timeButton,_scButton;

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
            _interface = new UserInterface(_powerButton, _timeButton, _scButton,_door,_display,_light, _cookController);

        }

        [Test]
        public void UserInterface_scButton_CookControllerStart()
        {
            _interface.OnPowerPressed(_powerButton, EventArgs.Empty);
            _interface.OnTimePressed(_timeButton, EventArgs.Empty);
            _interface.OnStartCancelPressed(_scButton, EventArgs.Empty);
            _cookController.Received().StartCooking(50,60);
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
        public void 
    }
}
