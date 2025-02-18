using System.Collections.Generic;
using Lessons.Meta.Lesson_Inventory;
using NUnit.Framework;
using UnityEngine;

namespace Lessons.Meta.Lesson_TDD
{
    [TestFixture]
    public class CraftingTest
    {
        private Inventory _inventory;
        private InventoryItem _stick;
        private InventoryItem _stone;
        private InventoryItem _axe;
        private InventoryItem _sword;
        private InventoryItem _iron;

        [SetUp]
        public void Init()
        {
            _inventory = new Inventory();
            
            _stick = new InventoryItem("stick");
            _stone = new InventoryItem("stone");
            _axe = new InventoryItem("axe");
            _sword = new InventoryItem("sword");
            _iron = new InventoryItem("iron");
        }
        
        [Test]
        public void WhenCraftItem_AndHasAllItemsInInventory_ThenAxeCountEqualsOne()
        {
            //Arrange:(Подготовка условий)
            _inventory.AddItem(_stick);
            _inventory.AddItem(_stick);
            _inventory.AddItem(_stone);

            var axeReceipt = CraftTestHelper.CreateAxeReceipt();

            //Act:(Действие)
            //Что-то сделать, чтобы пройти тест
            CraftUseCases.CraftItem(_inventory, axeReceipt);
            
            //Assert:(Подтверждение)
            int axeCount = _inventory.GetCount("axe");
            Assert.AreEqual(1, axeCount);
            Assert.AreEqual(0, _inventory.GetCount(_stick.Name));
            Assert.AreEqual(0, _inventory.GetCount(_stone.Name));
        }

        [Test]
        public void WhenCraftItem_AndHasNotItemsInInventory_ThenNotCraftItem()
        {
            // Arrange:
            var axeReceipt = CraftTestHelper.CreateAxeReceipt();

            // Act:
            CraftUseCases.CraftItem(_inventory, axeReceipt);

            // Assert:
            int axeCount = _inventory.GetCount("axe");
            Assert.AreEqual(0, axeCount);
            Assert.AreEqual(0, _inventory.GetCount(_stick.Name));
            Assert.AreEqual(0, _inventory.GetCount(_stone.Name));
        }

        [Test]
        public void WhenCraftItem_AndHasReceipt_ThenCraftItem()
        {
            // Arrange:
            _inventory.AddItem(_iron);
            _inventory.AddItem(_iron);
            _inventory.AddItem(_stick);
            var receipt = CraftTestHelper.CreateSwordReceipt();

            // Act:
            CraftUseCases.CraftItem(_inventory, receipt);

            // Assert:
            Assert.AreEqual(1, _inventory.GetCount(_sword.Name));
            Assert.AreEqual(0, _inventory.GetCount(_stick.Name));
            Assert.AreEqual(0, _inventory.GetCount(_iron.Name));
        }
        
        [Test]
        public void WhenCraftItem_AndHasStackableItems_AndSwordReceipt_ThenCraftItem()
        {
            // Arrange:
            _iron.Flags = InventoryItemFlags.Stackable;
            _iron.ItemComponents = new IItemComponent[]
            {
                new StackComponent()
                {
                    MaxCount = 5,
                }
            };
            
            _inventory.AddItem(_iron);
            _inventory.AddItem(_iron);
            _inventory.AddItem(_stick);
            var receipt = CraftTestHelper.CreateSwordReceipt();

            // Act:
            CraftUseCases.CraftItem(_inventory, receipt);

            // Assert:
            Assert.AreEqual(1, _inventory.GetCount(_sword.Name));
            Assert.AreEqual(0, _inventory.GetCount(_stick.Name));
            Assert.AreEqual(0, _inventory.GetCount(_iron.Name));
        }
    }

    public class CraftTestHelper
    {
        public static InventoryItemConfig CreateItemConfig(string name)
        {
            var itemConfig = ScriptableObject.CreateInstance<InventoryItemConfig>();
            itemConfig.Prototype = new InventoryItem(name);

            return itemConfig;
        }
        public static ItemReceipt CreateAxeReceipt()
        {
            var stick = CreateItemConfig("stick");
            var stone = CreateItemConfig("stone");

            var axeReceipt = ScriptableObject.CreateInstance<ItemReceipt>();
            var axeConfig = CreateItemConfig("axe");
            
            axeReceipt.ResultItemConfig = axeConfig;
            axeReceipt.Ingredients = new List<ItemIngredient>()
            {
                new ItemIngredient(){ItemConfig = stick, Count = 2},
                new ItemIngredient(){ItemConfig = stone, Count = 1},
            };

            return axeReceipt;
        }
        
        public static ItemReceipt CreateSwordReceipt()
        {
            var stick = CreateItemConfig("stick");
            var iron = CreateItemConfig("iron");
            var swordConfig = CreateItemConfig("sword");

            var swordReceipt = ScriptableObject.CreateInstance<ItemReceipt>();

            swordReceipt.ResultItemConfig = swordConfig;
            swordReceipt.Ingredients = new List<ItemIngredient>()
            {
                new ItemIngredient(){ItemConfig = stick, Count = 1},
                new ItemIngredient(){ItemConfig = iron, Count = 2},
            };

            return swordReceipt;
        }
    }
}
