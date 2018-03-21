using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Boundary;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    //class program
    //{
    //    static void Main(string[] args)
    //    {

    //    }
    //}

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
            _cookController = Substitute.For<ICookController>();
            _output = Substitute.For<IOutput>();

        }

        [Test]
        [TestCase(50,10)]
        public void CookControllerStartup(int power, int time)
        {
            _cookController.StartCooking(power,time);
            _timer.Start(time);
            _powerTube.TurnOn(power);
        }


    }
}
