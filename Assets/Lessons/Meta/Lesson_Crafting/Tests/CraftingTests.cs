using Lessons.Meta.Lesson_Inventory;
using NUnit.Framework;
using UnityEngine;

namespace Lessons.Meta.Lesson_Crafting
{
    [TestFixture]
    public class CraftingTests
    {
        private Inventory _inventory;
        private InventoryItem _stick;
        private InventoryItem _stone;
        private InventoryItem _iron;
        private InventoryItem _sword;
        private InventoryItem _axe;

        [SetUp]
        public void Construct()
        {
            _inventory = new Inventory();
            _stick = new InventoryItem("stick");
            _stone = new InventoryItem("stone");
            _iron = new InventoryItem("iron");
            _sword = new InventoryItem("sword");
            _axe = new InventoryItem("axe");
        }
        
        [Test]
        public void WhenCraftAxe_AndHas2Sticks1stone_ThenAddAxe()
        {
            //Arrange:
            InventoryUseCases.AddItem(_inventory, _stick.Clone());
            InventoryUseCases.AddItem(_inventory, _stick.Clone());
            InventoryUseCases.AddItem(_inventory, _stone.Clone());

            var itemReceipt = ScriptableObject.CreateInstance<ItemReceipt>();
            
            itemReceipt.ResultItem = CraftingUseCases.CreateItemConfig("axe");
            
            var stickConfig = CraftingUseCases.CreateItemConfig("stick");
            itemReceipt.Ingredients.Add(new ReceiptIngredient()
            {
                Config = stickConfig,
                Count = 2
            });

            var stoneConfig = CraftingUseCases.CreateItemConfig("stone");
            itemReceipt.Ingredients.Add(new ReceiptIngredient()
            {
                Config = stoneConfig,
                Count = 1,
            });

            //Act:
            CraftingUseCases.Craft(_inventory, itemReceipt);
            
            //Assert:
            var hasItem = InventoryUseCases.HasItem(_inventory, _axe);
            Assert.IsTrue(hasItem);
            Assert.IsFalse(InventoryUseCases.HasItem(_inventory, _stick));
            Assert.IsFalse(InventoryUseCases.HasItem(_inventory, _stone));
        }
        
        [Test]
        public void WhenCraftSword_AndHas1Irons1Stick_ThenAddSword()
        {
            // Arrange:
            InventoryUseCases.AddItem(_inventory, _iron.Clone());
            InventoryUseCases.AddItem(_inventory, _stick.Clone());
            
            ItemReceipt itemReceipt = GetSwordItemReceipt();

            //Act:
            CraftingUseCases.Craft(_inventory, itemReceipt);
            
            //Assert:
            var hasItem = InventoryUseCases.HasItem(_inventory, _sword);
            Assert.IsFalse(hasItem);
            var ironCount = InventoryUseCases.GetItemCount(_inventory, _iron);
            Assert.AreEqual(1, ironCount);
            var stickCount = InventoryUseCases.GetItemCount(_inventory, _stick);
            Assert.AreEqual(1, stickCount);
        }

        //2 железо + 1 стик = меч
        [Test]
        public void WhenCraftSword_AndNoItems_ThenNothing()
        {
            // Arrange:
            ItemReceipt itemReceipt = GetSwordItemReceipt();

            //Act:
            CraftingUseCases.Craft(_inventory, itemReceipt);

            // Assert:
            var hasItem = InventoryUseCases.HasItem(_inventory, _sword);
            Assert.IsFalse(hasItem);
            Assert.IsFalse(InventoryUseCases.HasItem(_inventory, _stick));
            Assert.IsFalse(InventoryUseCases.HasItem(_inventory, _iron));
            
            // var ironCount = InventoryUseCases.GetItemCount(inventory, iron);
            // Assert.AreEqual(2, ironCount);
        }

        private static ItemReceipt GetSwordItemReceipt()
        {
            var itemReceipt = ScriptableObject.CreateInstance<ItemReceipt>();
            itemReceipt.ResultItem = CraftingUseCases.CreateItemConfig("sword");
            
            var stickConfig = CraftingUseCases.CreateItemConfig("stick");
            itemReceipt.Ingredients.Add(new ReceiptIngredient()
            {
                Config = stickConfig,
                Count = 1
            });
            
            
            var ironConfig = CraftingUseCases.CreateItemConfig("iron");
            itemReceipt.Ingredients.Add(new ReceiptIngredient()
            {
                Config = ironConfig,
                Count = 2,
            });
            
            return itemReceipt;
        }
    }
}
