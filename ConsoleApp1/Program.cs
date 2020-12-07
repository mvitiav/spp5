﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static ConsoleApp1.DependencyRecord;

namespace ConsoleApp1
{
    public interface ImyInterface
    {

    }

    public interface i2
    {

    }
    public interface i3
    {

    }
    public interface i4
    {
        public i3 getI3();
    }
    public interface i5
    {
        public IEnumerable<i3> getI3s();
    }

    class i2Impl : i2
    {
        private ImyInterface inter;

        public i2Impl(ImyInterface inter)
        {
            this.inter = inter;
        }
    }
    class i3Impl : i3
    {
        //private i2 impl;

        //public i3Impl(i2 im)
        //{
        //    this.impl = im;
        //}
        private IEnumerable enumerable;

        public i3Impl(IEnumerable en)
        {
            this.enumerable = en;
        }

    }
    class i3Impl2 : i3
    {
        // private i2 impl;

        public i3Impl2()
        {
            Console.WriteLine("i3Impl2 constructor called");
            // this.impl = im;
        }
    }

    class i3Impl3 : i3
    {
        // private i2 impl;

        public i3Impl3()
        {
            // this.impl = im;
        }
    }

    class i4Impl : i4
    {
        public i3 impl;

        public i4Impl([SpecialDep("name")]i3 im)
        {

            this.impl = im;
        }

        public i3 getI3()
        {
            return impl;
        }
    }

    class i5Impl : i5
    {
        public IEnumerable<i3> i3s;

        public i5Impl(IEnumerable<i3> i3s)
        {
            this.i3s = i3s;
        }

        public IEnumerable<i3> getI3s()
        {
            return i3s;
        }
    }

    class IMyInterfaceImpl : ImyInterface
    {

    }


    interface IService<TRepository> where TRepository : i2
    {
        i2 getI2();
    }
    class ServiceImpl<TRepository> : IService<TRepository>
    where TRepository : i2
    {
        private i2 aiTwo;
        public ServiceImpl(TRepository repository)
        {
            this.aiTwo = repository;
            Console.WriteLine("thing created");
        }

        public i2 getI2()
        {
            return this.aiTwo;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DependencyConfiguration dc = new DependencyConfiguration();
            dc.Register<ImyInterface, IMyInterfaceImpl>();
            dc.Register<i2, i2Impl>();


            //dc.Register<i3, i3Impl>();
            //dc.Register<i3, i3Impl2>();
            //dc.Register<i3, i3Impl3>(true, "name");
            //dc.Register<i4, i4Impl>();
            //dc.Register<i5, i5Impl>();

            //dc.Register<IService<i2>, ServiceImpl<i2>>();
            dc.Register(typeof(IService<>), typeof(ServiceImpl<>),true);


            DependencyContainer dcon = new DependencyContainer(dc);


            //Console.WriteLine(dcon.Resolve<ImyInterface>().GetType());
            //Console.WriteLine(dcon.Resolve<i2>().GetType());
            //Console.WriteLine(dcon.Resolve<i3>("name").GetType());
            //Console.WriteLine(dcon.Resolve<i4>().getI3().GetType());
            //Console.WriteLine("===");
            //foreach (i3 ithe in dcon.Resolve<IEnumerable<i3>>()) {
            //    Console.WriteLine((ithe==null)?("null"):(ithe.GetType().ToString()));
            //}
            //Console.WriteLine("===");
            //Console.WriteLine(dcon.Resolve<i5>().getI3s().Count());
            //Console.WriteLine("===");
            //foreach (i3 ithe in dcon.Resolve<i5>().getI3s())
            //{
            //    Console.WriteLine((ithe == null) ? ("null") : (ithe.GetType().ToString()));
            //}
            //Console.WriteLine("===");

             Console.WriteLine(dcon.Resolve<IService<i2>>().getI2().GetType());
           // Console.WriteLine(dcon.Resolve<IService<i2>>().getI2().GetType());

            //Console.WriteLine(dcon.Resolve<IEnumerable>().GetType());
        }
    }
}
