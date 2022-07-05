﻿using NUnit.Framework;
using Shared.Util.Common.Constants;

namespace Shared.Util.Test.Common.Constantes
{
    [TestFixture]
    public class ConstMessagesTest
    {
        [TestCase("Internal server error. Please try again, if the problem persists contact support.", ExpectedResult = true)]
        [TestCase("Smarkets com br", ExpectedResult = false)]
        public bool ErrorDefault(string value)
        {
            //Arrange
            var message = string.Empty;

            //Act
            message = Messages.ErrorDefault;

            //Assert
            return message == value;
        }
    }
}
