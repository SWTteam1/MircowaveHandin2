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

    class IT4_CookController1
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
            _output = Substitute.For<IOutput>();
            _interface = Substitute.For<IUserInterface>();
            _timer = new MicrowaveOvenClasses.Boundary.Timer();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _interface);
        }

        [TestCase(50, 10)]
        public void CookControllerStart(int power, int time)
        {
            _cookController.StartCooking(power, time);
            _output.Received().OutputLine(Arg.Is<string>(str=>str.Contains("PowerTube works with 50 %")));
        }

        //[Test]
        //[TestCase(50, 10)]
        //public void CookControllerStartTimer(int power, int time)
        //{
        //    _cookController.StartCooking(power, time);
        //    _output.Received().OutputLine(Arg.Is<string>(str=>str.Contains("PowerTube works with 50 %")));
        //    //_timer.Received().Start(time);

        //}

        [Test]
        [TestCase(50, 10)]
        public void CookControllerStop(int power, int time)
        {
            _cookController.StartCooking(power, time);
            _cookController.Stop();
            _output.Received().OutputLine(Arg.Is<string>(str=>str.Contains("PowerTube turned off")));
        }

        [Test]
        [TestCase(50, 10)]
        public void CookControllerStopTimer(int power, int time)
        {
            _cookController.StartCooking(power, time);
            _cookController.Stop();

            //_timer.Received().Stop();

        }

        [Test]
        [TestCase(50,60000)]
        public void CookControllerDisplayTime(int power, int time)
        {
            _cookController.StartCooking(power, time);
            Thread.Sleep(1000);
            _output.Received().OutputLine(Arg.Is<string>(str=>str.Contains("Display shows: 00:59")));
        }

        [Test]
        public void CookControllerTimerExpired()
        {
            _cookController.StartCooking(50, 2);
            Thread.Sleep(2050);
            _output.Received().OutputLine(Arg.Is<string>(str=>str.Contains("PowerTube turned off")));
        }

        [Test]
        public void CookControllerCookingIsDone()
        {
            _cookController.StartCooking(50, 2);
            Thread.Sleep(2050);
            _interface.Received().CookingIsDone();
        }

    }
}