﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseWorkDB_DudasVI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SWEET_FACTORYEntities : DbContext
    {
        public SWEET_FACTORYEntities()
            : base("name=SWEET_FACTORYEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ADDRESS> ADDRESS { get; set; }
        public virtual DbSet<CATEGORY> CATEGORY { get; set; }
        public virtual DbSet<CLIENT> CLIENT { get; set; }
        public virtual DbSet<DELIVERY> DELIVERY { get; set; }
        public virtual DbSet<DELIVERY_ADDRESS> DELIVERY_ADDRESS { get; set; }
        public virtual DbSet<DEPARTMENT> DEPARTMENT { get; set; }
        public virtual DbSet<INGREDIENTS> INGREDIENTS { get; set; }
        public virtual DbSet<ORDER_PRODUCT> ORDER_PRODUCT { get; set; }
        public virtual DbSet<PACKAGE_DESCRIPTION> PACKAGE_DESCRIPTION { get; set; }
        public virtual DbSet<POST> POST { get; set; }
        public virtual DbSet<PRODUCT_INFO> PRODUCT_INFO { get; set; }
        public virtual DbSet<PRODUCT_PRICE> PRODUCT_PRICE { get; set; }
        public virtual DbSet<PRODUCTION_SCHEDULE> PRODUCTION_SCHEDULE { get; set; }
        public virtual DbSet<PROVIDER> PROVIDER { get; set; }
        public virtual DbSet<RAWSTUFF> RAWSTUFF { get; set; }
        public virtual DbSet<RAWSTUFF_CATEGORY> RAWSTUFF_CATEGORY { get; set; }
        public virtual DbSet<RAWSTUFF_DELIVERY> RAWSTUFF_DELIVERY { get; set; }
        public virtual DbSet<RAWSTUFF_INFO> RAWSTUFF_INFO { get; set; }
        public virtual DbSet<RAWSTUFF_ORDER> RAWSTUFF_ORDER { get; set; }
        public virtual DbSet<RAWSTUFF_STATE> RAWSTUFF_STATE { get; set; }
        public virtual DbSet<RELEASED_PRODUCT> RELEASED_PRODUCT { get; set; }
        public virtual DbSet<REQURIED_RAWSTUFF> REQURIED_RAWSTUFF { get; set; }
        public virtual DbSet<SALARY> SALARY { get; set; }
        public virtual DbSet<SALE_ORDER> SALE_ORDER { get; set; }
        public virtual DbSet<SCHEDULE_PRODUCT_INFO> SCHEDULE_PRODUCT_INFO { get; set; }
        public virtual DbSet<STAFF> STAFF { get; set; }
        public virtual DbSet<WAREHOUSE> WAREHOUSE { get; set; }
    }
}
