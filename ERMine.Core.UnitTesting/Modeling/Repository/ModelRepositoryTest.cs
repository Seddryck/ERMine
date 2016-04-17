﻿using ERMine.Core.Modeling;
using ERMine.Core.Modeling.Factory;
using ERMine.Core.Modeling.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMine.UnitTesting.Core.Modeling.Repository
{
    [TestClass]
    public class ModelRepositoryTest
    {
        [TestMethod]
        public void Merge_Entity_Loaded()
        {
            var repository = new ModelRepository();

            var entity = new Entity("Student");
            repository.MergeEntity(entity);

            Assert.AreEqual(1, repository.Get().Entities.Count);
        }

        [TestMethod]
        public void Merge_SameEntityTwice_LoadedOnce()
        {
            var repository = new ModelRepository();

            for (int i = 0; i < 2; i++)
            {
                var entity = new Entity("Student");
                repository.MergeEntity(entity);
            }

            Assert.AreEqual(1, repository.Get().Entities.Count);
        }

        [TestMethod]
        public void Merge_DistinctEntities_Loaded()
        {
            var repository = new ModelRepository();

            for (int i = 0; i < 2; i++)
            {
                var entity = new Entity("Student" + i.ToString());
                repository.MergeEntity(entity);
            }

            Assert.AreEqual(2, repository.Get().Entities.Count);
        }

        [TestMethod]
        public void Merge_EntityRelationship_Loaded()
        {
            var repository = new ModelRepository();

            var items = new List<IEntityRelationship>();
            items.Add(new Entity("Student"));
            repository.Merge(items);

            Assert.AreEqual(1, repository.Get().Entities.Count);
        }

        [TestMethod]
        public void Merge_SameEntityRelationshipTwice_LoadedOnce()
        {
            var repository = new ModelRepository();

            var items = new List<IEntityRelationship>();
            for (int i = 0; i < 2; i++)
                items.Add(new Entity("Student"));
            repository.Merge(items);

            Assert.AreEqual(1, repository.Get().Entities.Count);
        }

        [TestMethod]
        public void Merge_TwoEntitiesAndRelationship_Loaded()
        {
            var repository = new ModelRepository();

            var relationship = new RelationshipFactory().Create("write", "User", Cardinality.ZeroOrMore, "Post", Cardinality.ExactyOne);

            var items = new List<IEntityRelationship>();
            items.Add(new Entity("User"));
            items.Add(relationship);
            items.Add(new Entity("Post"));
            repository.Merge(items);

            Assert.AreEqual(2, repository.Get().Entities.Count);
            Assert.AreEqual(1, repository.Get().Relationships.Count);
            Assert.AreSame(repository.Get().Entities[0], repository.Get().Relationships[0].Entities.ElementAt(0));
            Assert.AreSame(repository.Get().Entities[1], repository.Get().Relationships[0].Entities.ElementAt(1));
        }

        [TestMethod]
        public void Merge_ThreeEntitiesAndTwoRelationship_Loaded()
        {
            var repository = new ModelRepository();

            var relationship1 = new RelationshipFactory().Create("write", "User", Cardinality.ZeroOrMore, "Post", Cardinality.ExactyOne);
            var relationship2 = new RelationshipFactory().Create("write", "User", Cardinality.ZeroOrMore, "Job", Cardinality.ZeroOrMore);

            var items = new List<IEntityRelationship>();
            items.Add(new Entity("User"));
            items.Add(relationship1);
            items.Add(new Entity("Post"));
            items.Add(relationship2);
            repository.Merge(items);

            Assert.AreEqual(3, repository.Get().Entities.Count);
            Assert.AreEqual(2, repository.Get().Relationships.Count);
        }

        //[TestMethod]
        //public void Merge_ThreeEntitiesAndTwoRelationshipInTwoLists_Loaded()
        //{
        //    var repository = new ModelRepository();

        //    var relationship1 = new RelationshipFactory().Create("write", "User", Cardinality.ZeroOrMore, "Post", Cardinality.ExactyOne);
        //    var relationship2 = new RelationshipFactory().Create("write", "User", Cardinality.ZeroOrMore, "Job", Cardinality.ZeroOrMore);

        //    var items1 = new List<IEntityRelationship>();
        //    items1.Add(new Entity("User"));
        //    items1.Add(relationship1);
        //    var items2 = new List<IEntityRelationship>();
        //    items2.Add(new Entity("Post"));
        //    items2.Add(relationship2);
        //    var list = new List<IEnumerable<IEntityRelationship>>();
        //    list.Add(items1);
        //    list.Add(items2);
        //    repository.Merge(list);

        //    Assert.AreEqual(3, repository.Get().Entities.Count);
        //    Assert.AreEqual(2, repository.Get().Relationships.Count);
        //}
    }
}