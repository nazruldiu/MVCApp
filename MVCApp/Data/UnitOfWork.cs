using MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext context;

        public UnitOfWork(AppDBContext db)
        {
            context = db;
        }

        private Repository<Designation> designationRepository;
        private Repository<Religion> religionRepository;

        public Repository<Designation> DesignationRepository
        {
            get
            {

                if (this.designationRepository == null)
                {
                    this.designationRepository = new Repository<Designation>(context);
                }
                return designationRepository;
            }
        }
        public Repository<Religion> ReligionRepository
        {
            get
            {

                if (this.religionRepository == null)
                {
                    this.religionRepository = new Repository<Religion>(context);
                }
                return religionRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
