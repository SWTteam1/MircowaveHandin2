using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;

using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{

    [TestFixture]

    class IntegrationTestCookCOntroller
    {
        private ICookController _cookController;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IOutput _output;

        [SetUp]
        public void Setup()
        {
            _timer = Substitute.For<ITimer>();
            _powerTube = Substitute.For<IPowerTube>();
            _display = Substitute.For<IDisplay>();
            //_cookController = Substitute.For<ICookController>();
            _output = Substitute.For<IOutput>();
            _cookController = new CookController(_timer, _display, _powerTube);

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
        //[TestCase(50,10)]
        public void CookControllerDisplayPower()
        {
            _cookController.StartCooking(50, 10);
            _display.Received().ShowPower(10);
            
        }

    }
}