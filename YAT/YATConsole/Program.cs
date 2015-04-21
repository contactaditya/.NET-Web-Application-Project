﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.Data.Entity;
using DataLayer;

namespace YATConsole
{
    class Program
    {
        public static void qryTester(UserSort sortby)
        {

            Builder b = new Builder();

            //Test searching users
            List<User> users = b.queryUsers(minAge: 20, maxAge: 30, gender: false, zipcode: 11791, SearcherID: 1, sortBy: sortby);
            foreach (User user in users)
            {
                Console.WriteLine(user.FirstName + " " + user.LastName);
            }

        }
        static void Main(string[] args)
        {
            Builder b = new Builder();
            b.putData();
            b.getData();
            
            Analytics a = new Analytics();
            a.movieRank();
            a.genderCount();

            qryTester(UserSort.LastJoin);
            qryTester(UserSort.LastLog);
            qryTester(UserSort.Match);
             
            Console.WriteLine("\nDone!");
            Console.ReadKey();
        }

        
    }
}