using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FishStore.Domain.Abstract;
using FishStore.Domain.Entities;
using FishStore.WebUI.Controllers;
using System.Web.Mvc;

namespace FishStore.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Организация - создание объекта Fish с данными изображения
            Fish fish = new Fish
            {
                FishId = 2,
                Name = "Игра2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            // Организация - создание имитированного хранилища
            Mock<IFishRepository> mock = new Mock<IFishRepository>();
            mock.Setup(m => m.Fishes).Returns(new List<Fish> {
                new Fish {FishId = 1, Name = "Игра1"},
                fish,
                new Fish {FishId = 3, Name = "Игра3"}
            }.AsQueryable());

            // Организация - создание контроллера
            FishController controller = new FishController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(2);

            // Утверждение
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(fish.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Организация - создание имитированного хранилища
            Mock<IFishRepository> mock = new Mock<IFishRepository>();
            mock.Setup(m => m.Fishes).Returns(new List<Fish> {
                new Fish {FishId = 1, Name = "Игра1"},
                new Fish {FishId = 2, Name = "Игра2"}
            }.AsQueryable());

            // Организация - создание контроллера
            FishController controller = new FishController(mock.Object);

            // Действие - вызов метода действия GetImage()
            ActionResult result = controller.GetImage(10);

            // Утверждение
            Assert.IsNull(result);
        }
    }
}
