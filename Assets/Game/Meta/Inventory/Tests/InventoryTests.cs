using Game.Meta;
using NUnit.Framework;

namespace Game.Tests
{
    public static class InventoryTestHelper
    {
        public static InventoryItem CreateItem(string id)
        {
            var item = new InventoryItem();
            item.Id = id;
            item.MetaData = new MetaDataInventoryItem();
            item.InitComponents();
            return item;
        }
    }
    
    [TestFixture]
    public class InventoryTests
    {
        [Test]
        public void WhenCreateInventory_AndNoItem_ThenInventoryIsEmpty()
        {
            // Arrange.

            // Act.
            var inventory = new Inventory();

            // Assert.
            Assert.AreEqual(0, inventory.Count);
        }

        [Test]
        public void WhenAddItemToInventory_ThenHasOneItem()
        {
            // Arrange.
            var inventory = new Inventory();
            var inventoryItem = new InventoryItem();
            inventoryItem.Id = "TestItem";
            inventoryItem.MetaData = new MetaDataInventoryItem();

            // Act.
            inventory.AddItems(inventoryItem);

            // Assert.
            Assert.AreEqual(1, inventory.Count);
        }

        [Test]
        public void WhenAddItemToInventory_AndItemHasName_ThenItemHasName()
        {
            // Arrange.
            var inventory = new Inventory();
            var inventoryItem = new InventoryItem();
            inventoryItem.Id = "FamilyPhoto";
            inventoryItem.MetaData = new MetaDataInventoryItem();
            
            // Act.
            inventory.AddItems(inventoryItem);
            
            // Assert.
            Assert.AreEqual(1, inventory.Count);
            Assert.IsTrue(inventory.TryFindItem(inventoryItem, out var item));
        }

        [Test]
        public void WhenRemoveItem_AndHasItem_ThenNoItem()
        {
            // Arrange:
            var inventory = new Inventory();
            var inventoryItem = new InventoryItem();
            inventoryItem.Id = "TestItem";
            inventoryItem.MetaData = new MetaDataInventoryItem();
            inventory.AddItems(inventoryItem);
            
            // Act:
            var result = inventory.RemovePrototypeItem(inventoryItem);

            // Assert:
            Assert.AreEqual(0, inventory.Count);
            Assert.IsTrue(result);
        }

        [Test]
        public void WhenAddItem_AndHasItem_ThenAddOneMoreItem()
        {
            // Arrange:
            var inventory = new Inventory();

            var inventoryItem = new InventoryItem();
            inventoryItem.Id = "TestItem";
            inventoryItem.MetaData = new MetaDataInventoryItem();
            inventory.AddItems(inventoryItem);

            // Act:
            inventory.AddItems(inventoryItem);

            // Assert:
            Assert.AreEqual(2, inventory.GetItemCount(inventoryItem));
        }

        [Test]
        public void WhenAddItem_AndCountMoreMaxCapacity_ThenReject()
        {
            // Arrange:
            var inventory = new Inventory();
            inventory.MaxCount = 0;
            
            var inventoryItem = InventoryTestHelper.CreateItem("TestItem");

            // Act:
            inventory.AddItems(inventoryItem);

            // Assert:
            Assert.AreEqual(0, inventory.Count);
        }
        
        [Test]
        public void WhenAddStackableItem_AndCountMoreMaxCapacity_ThenReject()
        {
            // Arrange:
            var inventory = new Inventory();
            inventory.MaxCount = 1;
            
            var inventoryItem = InventoryTestHelper.CreateItem("TestItem");
            inventoryItem.AddComponent(new StackableComponent()
            {
                Count = 1,
                MaxCount = 5,
            });

            // Act:
            inventory.AddItems(inventoryItem);
            inventory.AddItems(inventoryItem);

            // Assert:
            var itemCount = inventory.GetItemCount(inventoryItem);
            Assert.AreEqual(2, itemCount);
        }

        // [Test]
        // public void WhenAddItem_AndHasDamageComponent_ThenIncreaseDamage()
        // {
        //     // Arrange:
        //     var inventory = new Inventory();
        //     inventory.MaxCount = 5;
        //     
        //     var inventoryItem = new InventoryItem();
        //     inventoryItem.AddComponent(new DamageComponent()
        //     {
        //         StatType = ReactiveStatType.Sum,
        //         Value = new ReactiveVariable<float>(5f),
        //     });
        //
        //     var player = new Entity();
        //     var weapon = new Entity();
        //     weapon.AddDamageStat(new ReactiveFloatStat());
        //     player.AddWeapon(new ReactiveVariable<IEntity>(weapon));
        //
        //     var damageObserver = new ItemDamageObserver();
        //     damageObserver.Init(inventory, player);
        //
        //     //Act:
        //     inventory.AddItem(inventoryItem);
        //     
        //     //Assert:
        //     Assert.AreEqual(5f, player.GetWeapon().Value.GetDamageStat().Value);
        // }
        
        // [Test]
        // public void WhenRemoveItem_AndHasDamageComponent_ThenDecreaseDamage()
        // {
        //     // Arrange:
        //     var inventory = new Inventory();
        //     inventory.MaxCount = 5;
        //     
        //     var inventoryItem = new InventoryItem();
        //     inventoryItem.Id = "TestItem";
        //     inventoryItem.MetaData = new MetaDataInventoryItem();
        //     
        //     inventoryItem.AddComponent(new DamageComponent()
        //     {
        //         StatType = ReactiveStatType.Sum,
        //         Value = new ReactiveVariable<float>(5f),
        //     });
        //
        //     var player = new Entity();
        //     var weapon = new Entity();
        //     weapon.AddDamageStat(new ReactiveFloatStat());
        //     player.AddWeapon(new ReactiveVariable<IEntity>(weapon));
        //
        //     var damageObserver = new ItemDamageObserver();
        //     damageObserver.Init(inventory, player);
        //     inventory.AddItems(inventoryItem);
        //
        //     //Act:
        //     inventory.RemoveItem(inventoryItem);
        //     
        //     //Assert:
        //     Assert.AreEqual(0f, player.GetWeapon().Value.GetDamageStat().Value);
        // }
    }
}