using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperZapatos.Exceptions;
using SuperZapatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SuperZapatos.Models.Tests
{
    [TestClass()]
    public class ErrorModelTests
    {
        [TestMethod()]
        public void ExpectUnauthorizedExceptionFromErrorHandleTest()
        {
            // Expects
            bool correctException;

            // Arrange
            string json = @"{'Sucess':false,'ErrorCode':'401','ErrorMessage':'Not authorized'}";
            HttpResponseMessage message = new HttpResponseMessage();
            HttpContent content = new StringContent(json);
            message.Content = content;

            // Act
            Exception ex = ErrorModel.HandleError(message);
            try
            {
                throw ex;
            }
            catch (UnauthorizedException)
            {
                correctException = true;
            }

            // Assert
            Assert.AreEqual(true, correctException);
        }

        [TestMethod()]
        public void ExpectRecordNotFoundExceptionFromErrorHandleTest()
        {
            // Expects
            bool correctException;

            // Arrange
            string json = @"{'Sucess':false,'ErrorCode':'404','ErrorMessage':'Record not found'}";
            HttpResponseMessage message = new HttpResponseMessage();
            HttpContent content = new StringContent(json);
            message.Content = content;

            // Act
            Exception ex = ErrorModel.HandleError(message);
            try
            {
                throw ex;
            }
            catch (RecordNotFoundException)
            {
                correctException = true;
            }

            // Assert
            Assert.AreEqual(true, correctException);
        }

        [TestMethod()]
        public void UnexpectedExceptionFromErrorHandleTest()
        {
            // Expects
            bool correctException;
            string details = string.Empty;

            // Arrange
            string json = @"{'Message':'Forbbiden'}";
            HttpResponseMessage message = new HttpResponseMessage();
            HttpContent content = new StringContent(json);
            message.Content = content;

            // Act
            Exception ex = ErrorModel.HandleError(message);
            try
            {
                throw ex;
            }
            catch (Exception exception)
            {
                correctException = true;
                details = exception.Message;
            }

            // Assert
            Assert.AreEqual(true, correctException);
            Assert.AreNotEqual(string.Empty, details);
        }
    }
}