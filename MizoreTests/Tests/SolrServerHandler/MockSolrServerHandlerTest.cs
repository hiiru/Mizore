﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mizore.ContentSerializer;
using Mizore.SolrServerHandler;
using MizoreTests.Mock;

namespace MizoreTests.Tests.SolrServerHandler
{
    public abstract class MockSolrServerHandlerTest : SolrServerHandlerBaseTest
    {
        protected override ISolrServerHandler CreateInstance()
        {
            return new MockSolrServerHandler(resourcePath: "MizoreTests.Resources.ResponseFiles.", contentSerializer: CreateSerializer());
        }
    }

    [TestClass]
    public class MockSolrServerHandlerTest_EasynetJavabin : MockSolrServerHandlerTest
    {
        protected override IContentSerializer CreateSerializer()
        {
            return new EasynetJavabinSerializer();
        }
    }
}
