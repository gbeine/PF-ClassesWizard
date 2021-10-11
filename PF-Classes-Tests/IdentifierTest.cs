using NUnit.Framework;
using PF_Classes.Identifier;

namespace PF_Classes_Tests
{
    public class IdentifierTests
    {
        [Test]
        public void TestArchetypes()
        {
            Archetypes i = Archetypes.INSTANCE;
            
            Assert.AreEqual(48, i.AllIdentifiers.Count);
            Assert.AreEqual("6bfb7e74b530f3749b590286dd2b9b30", i.GetGuidFor("CLERIC_CRUSADER"));
        }
        
        [Test]
        public void TestCharacterClasses()
        {
            CharacterClasses i = CharacterClasses.INSTANCE;
            
            Assert.AreEqual(16, i.AllIdentifiers.Count);
            Assert.AreEqual("0937bec61c0dabc468428f496580c721", i.GetGuidFor("ALCHEMIST"));
        }
        
        [Test]
        public void TestFeatures()
        {
            Features i = Features.INSTANCE;
            
            Assert.AreEqual(175, i.AllIdentifiers.Count);
            Assert.AreEqual("9c37279588fd9e34e9c4cb234857492c", i.GetGuidFor("WEAPON_PROFICIENCY_DUELLING_SWORD"));
        }
        
        [Test]
        public void TestPrestigeClasses()
        {
            PrestigeClasses i = PrestigeClasses.INSTANCE;
            
            Assert.AreEqual(7, i.AllIdentifiers.Count);
            Assert.AreEqual("d5917881586ff1d4d96d5b7cebda9464", i.GetGuidFor("STALWART_DEFENDER"));
        }
        
        [Test]
        public void TestSpellbooks()
        {
            Spellbooks i = Spellbooks.INSTANCE;
            
            Assert.AreEqual(25, i.AllIdentifiers.Count);
            Assert.AreEqual("762858a4a28eaaf43aa00f50441d7027", i.GetGuidFor("RANGER_SPELLBOOK"));
        }
        
        [Test]
        public void TestSpellLists()
        {
            SpellLists i = SpellLists.INSTANCE;
            
            Assert.AreEqual(1, i.AllIdentifiers.Count);
            Assert.AreEqual("ba0401fdeb4062f40a7aa95b6f07fe89", i.GetGuidFor("WIZARD_SPELLLIST"));
        }
        
        [Test]
        public void TestSpells()
        {
            Spells i = Spells.INSTANCE;
            
            Assert.AreEqual(29, i.AllIdentifiers.Count);
            Assert.AreEqual("52b5df2a97df18242aec67610616ded0", i.GetGuidFor("SUMMON_MONSTER_IX_BASE"));
        }
    }
}
