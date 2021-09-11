using System;
using System.Collections.Generic;
using keypr.Models;
using keypr.Repositories;

namespace keypr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;
        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }
        internal Keep Create(Keep newKeep)
        {
           return _repo.Create(newKeep);
        }

        internal List<Keep> Get()
        {
         return _repo.GetAll();  
        }

        internal Keep Get(int id)
        {
            Keep keep = _repo.Get(id);
            if (keep == null)
            {
                throw new Exception("Invalid Keep Id");
            }
            return keep;
        }
    }
}