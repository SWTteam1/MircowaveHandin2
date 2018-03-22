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

    [TestFixture]

    class IT4_CookController
    {
        private ICookController _cookController;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IOutput _output;
        private IUserInterface _interface;

        [SetUp]
        public void Setup()
        {
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _display = Substitute.For<IDisplay>();
            _output = Substitute.For<IOutput>();
            _interface = Substitute.For<IUserInterface>();
            _cookController = new CookController(_timer, _display, _powerTube, _interface);

        }

        [Test]
        [TestCase(50, 10)]
        public void CookControllerStart(int power, int time)
        {
            _cookController.StartCooking(power, time);
            _powerTube.Received().TurnOn(power);

        }

        [Test]
        [TestCase(50, 10)]
        public void CookControllerStartTimer(int power, int time)
        {
            _cookController.StartCooking(power, time);
            _timer.Received().Start(time);

        }

        [Test]
        public void CookControllerStop()
        {
            _cookController.Stop();
            _powerTube.Received().TurnOff();
        }

        [Test]
        public void CookControllerStopTimer()
        {
            _cookController.Stop();
            _timer.Received().Stop();

        }

        [Test]
        public void CookControllerDisplayTime()
        {
            _cookController.StartCooking(50, 6000);
            _timer.TimeRemaining.Returns(10000);
            _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            _display.Received().ShowTime(00, 10);

        }

        [Test]
        public void CookControllerTimerExpired()
        {
            _cookController.StartCooking(50, 0);
            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);
            _powerTube.Received().TurnOff();

        }
        [Test]
        public void CookControllerCookingIsDone()
        {
            _cookController.StartCooking(50, 0);
            _timer.Expired += Raise.EventWith(this, EventArgs.Empty);
            _interface.Received().CookingIsDone();

        }

    }
}