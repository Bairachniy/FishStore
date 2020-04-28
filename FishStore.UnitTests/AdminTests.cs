using FishStore.Domain.Abstract;
using FishStore.Domain.Entities;
using FishStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FishStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Fishs()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IFishRepository> mock = new Mock<IFishRepository>();
            mock.Setup(m => m.Fishes).Returns(new List<Fish>
            {
                new Fish { FishId = 1, Name = "Игра1"},
                new Fish { FishId = 2, Name = "Игра2"},
                new Fish { FishId = 3, Name = "Игра3"},
                new Fish { FishId = 4, Name = "Игра4"},
                new Fish { FishId = 5, Name = "Игра5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<Fish> result = ((IEnumerable<Fish>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Игра1", result[0].Name);
            Assert.AreEqual("Игра2", result[1].Name);
            Assert.AreEqual("Игра3", result[2].Name);
        }
        [TestMethod]
        public void Can_Edit_Fish()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IFishRepository> mock = new Mock<IFishRepository>();
            mock.Setup(m => m.Fishes).Returns(new List<Fish>
    {
        new Fish { FishId = 1, Name = "Игра1"},
        new Fish { FishId = 2, Name = "Игра2"},
        new Fish { FishId = 3, Name = "Игра3"},
        new Fish { FishId = 4, Name = "Игра4"},
        new Fish { FishId = 5, Name = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Fish Fish1 = controller.Edit(1).ViewData.Model as Fish;
            Fish Fish2 = controller.Edit(2).ViewData.Model as Fish;
            Fish Fish3 = controller.Edit(3).ViewData.Model as Fish;

            // Assert
            Assert.AreEqual(1, Fish1.FishId);
            Assert.AreEqual(2, Fish2.FishId);
            Assert.AreEqual(3, Fish3.FishId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Fish()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IFishRepository> mock = new Mock<IFishRepository>();
            mock.Setup(m => m.Fishes).Returns(new List<Fish>
    {
        new Fish { FishId = 1, Name = "Игра1"},
        new Fish { FishId = 2, Name = "Игра2"},
        new Fish { FishId = 3, Name = "Игра3"},
        new Fish { FishId = 4, Name = "Игра4"},
        new Fish { FishId = 5, Name = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            Fish result = controller.Edit(6).ViewData.Model as Fish;

            // Assert
        }
        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IFishRepository> mock = new Mock<IFishRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Fish
            Fish fish = new Fish { Name = "Test" };

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(fish);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.SaveFish(fish));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IFishRepository> mock = new Mock<IFishRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Fish
            Fish fish = new Fish { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(fish);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveFish(It.IsAny<Fish>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void Can_Delete_Valid_Fishes()
        {
            // Организация - создание объекта Fish
            Fish fish = new Fish { FishId = 2, Name = "Игра2" };

            // Организация - создание имитированного хранилища данных
            Mock<IFishRepository> mock = new Mock<IFishRepository>();
            mock.Setup(m => m.Fishes).Returns(new List<Fish>
    {
        new Fish { FishId = 1, Name = "Игра1"},
        new Fish { FishId = 2, Name = "Игра2"},
        new Fish { FishId = 3, Name = "Игра3"},
        new Fish { FishId = 4, Name = "Игра4"},
        new Fish { FishId = 5, Name = "Игра5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление игры
            controller.Delete(fish.FishId);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта Fish
            mock.Verify(m => m.DeleteFish(fish.FishId));
        }
    }
}
